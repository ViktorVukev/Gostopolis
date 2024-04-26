import {Component, ElementRef, NgZone, ViewChild, inject, OnInit, Inject} from '@angular/core';
import { Constants } from '../../../../../../shared/interfaces/Constants';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {map, Observable, startWith} from 'rxjs';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatChipInputEvent } from '@angular/material/chips';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {ToastrService} from "ngx-toastr";
import {RestaurantService} from "../../../../../restaurant/services/restaurant.service";
import {defineComponents, IgcRatingComponent} from "igniteui-webcomponents";
import {AccommodationService} from "../../../../services/accommodation.service";

@Component({
  selector: 'app-general-information-dialog',
  templateUrl: './general-information-dialog.component.html',
  styleUrl: './general-information-dialog.component.css'
})
export class GeneralInformationDialogComponent implements OnInit {
  fileName: string = 'Избери файл';
  updateForm: FormGroup;
  accommodation: any;
  languagesList: string[] = Constants.LanguagesList;
  languageCtrl = new FormControl('');
  filteredLanguages!: Observable<string[]>;
  accommodationTypes: Array<any> = [];
  languages: string[] = [];
  separatorKeysCodes: number[] = [ENTER, COMMA];
  announcer = inject(LiveAnnouncer);
  @ViewChild('fruitInput') languageInput!: ElementRef<HTMLInputElement>;

  @ViewChild('search') public searchElementRef!: ElementRef;

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    private elementRef: ElementRef,
    private toastrService: ToastrService,
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<GeneralInformationDialogComponent>,
    private accommodationService: AccommodationService,
    private ngZone: NgZone) {

    this.filteredLanguages = this.languageCtrl.valueChanges.pipe(
      startWith(null),
      map((fruit: string | null) => (fruit ? this._filter(fruit) : this.languagesList.slice())));

    this.accommodation = data;
    this.languages = data.spokenLanguages.split(', ')

    this.updateForm = this.fb.group({
      'name': [data.name, [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      'typeId': [data.typeId, Validators.required],
      'identificationNumber': [data.identificationNumber, Validators.required],
      'ownershipDocumentBase64': [null],
      'coverPhotoUrl': [data.coverPhotoUrl, [Validators.required]],
      'address': [data.address, [Validators.required]],
      'locationUrl': [data.locationUrl, [Validators.required]],
      'longitude': [data.longitude, [Validators.required]],
      'latitude': [data.latitude, [Validators.required]],
      'town': [data.town, [Validators.required]],
      'stars': [data.stars, [Validators.required]],
      'hasParking': [data.hasParking, [Validators.required]],
      'hasPosTerminal': [data.hasPosTerminal, [Validators.required]],
      'acceptOnlinePayments': [data.acceptOnlinePayments, [Validators.required]],
      'acceptPets': [data.acceptPets, [Validators.required]],
      'languages': [data.languages],
      'checkInTime': [data.checkInTime],
      'checkOutTime': [data.checkInTime],
      'facilities': [data.facilities],
      'phoneNumber': [data.phoneNumber.substring(4), [Validators.required]],
      'description': [data.description, [Validators.required, Validators.minLength(50)]]
    });
  }

  ngOnInit() {
    this.accommodationService.getAccommodationTypes().subscribe(res => {
      this.accommodationTypes = res;
    });

    defineComponents(IgcRatingComponent);
  }

  ngAfterViewInit(): void {
    // Binding autocomplete to search input control
    let autocomplete = new google.maps.places.Autocomplete(
      this.searchElementRef.nativeElement, {
        types: ['address']
      });

    autocomplete.addListener('place_changed', () => {
      this.ngZone.run(() => {
        //get the place result
        let place: google.maps.places.PlaceResult = autocomplete.getPlace();

        //verify result
        if (place.geometry === undefined || place.geometry === null) {
          return;
        }

        this.updateForm.patchValue({ address: place.formatted_address });
        this.updateForm.patchValue({ latitude: place.geometry.location?.lat() });
        this.updateForm.patchValue({ longitude: place.geometry.location?.lng() });
        this.updateForm.patchValue({ address: place.formatted_address });
        if (place.address_components !== undefined) {
          this.updateForm.patchValue({ town: place.address_components[place.address_components.length - 1 - 3].long_name });
        }
        this.updateForm.patchValue({ locationUrl: place.url });
      });
    });
  }

  uploadFile(event: any): void {
    const file: File = <File>event.target.files[0];

    if (file.size > 10485760) {
      this.toastrService.error('Файлът не трябва да надвишава 10MB.');
      return;
    }

    this.fileName = file.name;

    let reader = new FileReader();
    reader.readAsDataURL(file);

    reader.onload = (_event) => {
      this.updateForm.patchValue({ ownershipDocumentBase64: reader.result as string });
    };

    event.target.files = null;
    this.toastrService.info('Файлът е прикачен.');
  }

  ratingChanged(event: CustomEvent): void {
    this.updateForm.patchValue({ stars: event.detail });
  }

  /* Language autocomplete functionality */
  addLanguage(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();
    if (value) {
      this.languages.push(value);
    }
    event.chipInput!.clear();
    this.languageCtrl.setValue(null);
  }

  removeLanguage(language: string): void {
    const index = this.languages.indexOf(language);
    if (index >= 0) {
      this.languages.splice(index, 1);
      this.announcer.announce(`Премахнат ${language}`);
    }
  }

  selectedLanguage(event: MatAutocompleteSelectedEvent): void {
    this.languages.push(event.option.viewValue);
    this.languageInput.nativeElement.value = '';
    this.languageCtrl.setValue(null);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.languagesList.filter(language => language.toLowerCase().includes(filterValue));
  }

  update(): void {
    this.updateForm.removeControl('languages');
    this.updateForm.addControl('spokenLanguages', new FormControl(this.languages.join(', '), Validators.required));
    this.updateForm.patchValue({ phoneNumber: '+359' + this.updateForm.value.phoneNumber });

    console.log(this.updateForm);
    if (this.updateForm.invalid) {
      this.toastrService.error('Една или повече валидации не преминаха успешно.');
      return;
    }

    this.accommodationService.edit(this.accommodation.id, this.updateForm.value).subscribe(() => {
      this.toastrService.success('Общата информация е променена успешно.');
      this.toastrService.warning('Обектът е временно скрит, докато премине процес на повторна верификация.');
      this.dialogRef.close();
    });
  }

  // Get form fields
  get name() {
    return this.updateForm.get('name');
  }

  get identificationNumber() {
    return this.updateForm.get('identificationNumber');
  }

  get address() {
    return this.updateForm.get('address');
  }

  get phoneNumber() {
    return this.updateForm.get('phoneNumber');
  }

  get description() {
    return this.updateForm.get('description');
  }
}
