import { AfterViewInit, Component, ElementRef, NgZone, OnInit, ViewChild, inject } from '@angular/core';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { GoogleMap } from '@angular/google-maps';
import { ToastrService } from 'ngx-toastr';
import { defineComponents, IgcRatingComponent } from 'igniteui-webcomponents';
import { Constants } from '../../../../shared/interfaces/Constants';
import { PropertyType } from '../../../../shared/interfaces/PropertyType';
import { RestaurantService } from '../../../restaurant/services/restaurant.service';
import { AccommodationService } from '../../../accommodation/services/accommodation.service';
import { PartnersService } from '../../services/partners.service';
import { Observable, map, startWith } from 'rxjs';
import { MatChipInputEvent } from '@angular/material/chips';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';

@Component({
  selector: 'app-create-property',
  templateUrl: './create-property.component.html',
  styleUrl: './create-property.component.css'
})
export class CreatePropertyComponent implements OnInit, AfterViewInit {
  selectedCategory: string = '';
  languageCtrl = new FormControl('');
  languagesList: string[] = Constants.LanguagesList;
  filteredLanguages!: Observable<string[]>;
  languages: string[] = [];
  separatorKeysCodes: number[] = [ENTER, COMMA];
  accommodationTypes!: Array<PropertyType>;
  restaurantTypes!: Array<PropertyType>;
  createForm: FormGroup;

  @ViewChild('fruitInput') languageInput!: ElementRef<HTMLInputElement>;
  announcer = inject(LiveAnnouncer);

  @ViewChild('search')
  public searchElementRef!: ElementRef;
  @ViewChild(GoogleMap)
  public map!: GoogleMap;
  @ViewChild('fileInput') fileInput!: ElementRef;
  fileName: string = 'Избери файл';

  photos: any = [];
  index: number = 0;
  zoom = 16;
  center!: google.maps.LatLngLiteral;
  options: google.maps.MapOptions = {
    scrollwheel: false,
    disableDefaultUI: true,
    disableDoubleClickZoom: true,
    mapTypeId: 'terrain',
  };
  latitude!: any;
  longitude!: any;

  constructor(
    private ngZone: NgZone,
    private partnersService: PartnersService,
    private restaurantService: RestaurantService,
    private accommodationService: AccommodationService,
    private toastrService: ToastrService,
    private fb: FormBuilder) {

    this.filteredLanguages = this.languageCtrl.valueChanges.pipe(
      startWith(null),
      map((fruit: string | null) => (fruit ? this._filter(fruit) : this.languagesList.slice())));

    this.createForm = this.fb.group({
      'name': ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      'typeId': [0, Validators.required],
      'identificationNumber': ['', Validators.required],
      'ownershipDocumentBase64': ['', Validators.required],
      'coverPhotoBase64': ['', Validators.required],
      'address': ['', [Validators.required]],
      'stars': [0, [Validators.required]],
      'hasParking': [false, [Validators.required]],
      'hasPosTerminal': [false, [Validators.required]],
      'acceptOnlinePayments': [false, [Validators.required]],
      'acceptPets': [false, [Validators.required]],
      'languages': [''],
      'phoneNumber': ['', [Validators.required]],
      'description': ['', [Validators.required, Validators.minLength(300)]],
      'workingHours': ['1']
    });
  }

  ngAfterViewInit(): void {
    // Binding autocomplete to search input control
    let autocomplete = new google.maps.places.Autocomplete(
      this.searchElementRef.nativeElement, {
        types: ['address']
      });
    // Align search box to center
    this.map.controls[google.maps.ControlPosition.TOP_CENTER].push(
      this.searchElementRef.nativeElement
    );
    autocomplete.addListener('place_changed', () => {
      this.ngZone.run(() => {
        //get the place result
        let place: google.maps.places.PlaceResult = autocomplete.getPlace();

        //verify result
        if (place.geometry === undefined || place.geometry === null) {
          return;
        }

        this.createForm.patchValue({ address: place.formatted_address });
        this.createForm.addControl('latitude', new FormControl(place.geometry.location?.lat()));
        this.createForm.addControl('longitude', new FormControl(place.geometry.location?.lng()));
        if (place.address_components !== undefined) {
          this.createForm.addControl('town', new FormControl(place.address_components[place.address_components.length - 1 - 3].long_name));
        }
        this.createForm.addControl('locationUrl', new FormControl(place.url));

        this.latitude = place.geometry.location?.lat();
        this.longitude = place.geometry.location?.lng();
        this.center = {
          lat: this.latitude,
          lng: this.longitude,
        };
      });
    });
  }

  ngOnInit(): void {
    this.accommodationService.getAccommodationTypes().subscribe(res => {
      this.accommodationTypes = res;
    });

    this.restaurantService.getRestaurantTypes().subscribe(res => {
      this.restaurantTypes = res.data;
    });

    /* Details section */
    defineComponents(IgcRatingComponent);

    /* Location section */
    navigator.geolocation.getCurrentPosition((position) => {
      this.center = {
        lat: position.coords.latitude,
        lng: position.coords.longitude,
      };
    });
  }

  selectCategory(option: string): void {
    this.selectedCategory = option;
  }

  selectType(id: number, generalType: string): void {
    this.createForm.patchValue({ typeId: id });
    this.selectedCategory = generalType;
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
      this.createForm.patchValue({ ownershipDocumentBase64: reader.result as string });
    };

    event.target.files = null;
    this.toastrService.info('Файлът е прикачен.');
  }

  ratingChanged(event: CustomEvent): void {
    this.createForm.patchValue({ stars: event.detail });
  }

  /* Gallery section */
  onSelect(event: any): void {
    if (event.target.files) {
      for (let i = 0; i < event.target.files.length; i++) {
        if (event.target.files[i].size > 10485760) {
          this.toastrService.error('Файлът не трябва да надвишава 10MB.');
          continue;
        }

        let reader = new FileReader();
        reader.readAsDataURL(event.target.files[i]);
        reader.onload = (events: any) => {
          this.photos.push(events.target.result);
        }
      }
    }
  }

  setCoverPhoto(index: number): void {
    this.index = index;
    this.toastrService.info('Снимката е успешно зададена като основна снимка на обекта.')
  }

  onDelete(index: number): void {
    if (index >= 0 && index < this.photos.length) {
      this.photos.splice(index, 1);
    }
  }

  create(): void {
    this.partnersService.validateCreateForm(this.createForm, this.photos);

    this.createForm.removeControl('languages');
    this.createForm.addControl('spokenLanguages', new FormControl(this.languages.join(', '), Validators.required));
    this.createForm.patchValue({ coverPhotoBase64: this.photos[this.index] });
    this.createForm.patchValue({ phoneNumber: '+359' + this.createForm.value.phoneNumber });

    this.partnersService.createProperty(this.selectedCategory, this.createForm, this.photos);
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

  // Get form fields
  get name() {
    return this.createForm.get('name');
  }

  get identificationNumber() {
    return this.createForm.get('identificationNumber');
  }
}
