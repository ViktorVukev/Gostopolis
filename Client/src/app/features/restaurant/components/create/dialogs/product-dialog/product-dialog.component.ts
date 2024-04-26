import { Component, ElementRef, ViewChild, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Observable, map, startWith } from 'rxjs';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatChipInputEvent } from '@angular/material/chips';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatDialogRef } from '@angular/material/dialog';
import { TableType } from '../../../../../../shared/interfaces/TableType';
import { Product } from '../../../../../../shared/interfaces/Product';
import { ToastrService } from "ngx-toastr";
import { Constants } from '../../../../../../shared/interfaces/Constants';

@Component({
  selector: 'app-product-dialog',
  templateUrl: './product-dialog.component.html',
  styleUrl: './product-dialog.component.css'
})
export class ProductDialogComponent {
  @ViewChild('fileInput') fileInput!: ElementRef;
  fileName: string = 'Избери файл';

  categoriesList: string[] = Constants.CategoriesList;

  separatorKeysCodes: number[] = [ENTER, COMMA];
  ingredientCtrl = new FormControl('');
  filteredIngredients: Observable<string[]> = new Observable<string[]>();
  ingredients: string[] = [];
  allIngredients: string[] = Constants.IngredientsList;

  @ViewChild('ingredientInput') ingredientInput!: ElementRef<HTMLInputElement>;
  announcer = inject(LiveAnnouncer);

  createForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<ProductDialogComponent>,
    private toastrService: ToastrService,
    private formBuilder: FormBuilder) {
    this.createForm = this.formBuilder.group({
      'name': ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      'price': [null, Validators.required],
      'weight': [null, Validators.required],
      'type': [null, Validators.required],
      'ingredients': [''],
      'imageBase64': ['']
    });

    this.filteredIngredients = this.ingredientCtrl.valueChanges.pipe(
      startWith(null),
      map((ingredient: string | null) => (ingredient ? this._filter(ingredient) : this.allIngredients.slice())),
    );
  }

  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    // Add ingredient
    if (value) {
      this.ingredients.push(value);
    }

    // Clear the input value
    event.chipInput!.clear();

    this.ingredientCtrl.setValue(null);
  }

  remove(ingredient: string): void {
    const index = this.ingredients.indexOf(ingredient);

    if (index >= 0) {
      this.ingredients.splice(index, 1);
      this.announcer.announce(`Премахват ${ingredient}`);
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.ingredients.push(event.option.viewValue);
    this.ingredientInput.nativeElement.value = '';
    this.ingredientCtrl.setValue(null);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.allIngredients.filter(ingredient => ingredient.toLowerCase().includes(filterValue));
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
      this.createForm.patchValue({ imageBase64: reader.result as string });
    };

    event.target.files = null;
    this.toastrService.info('Файлът е прикачен.');
  }

  onSubmit(): void {
    if (this.createForm.invalid) {
      return;
    }

    let formData = this.createForm.value;

    this.ingredients = this.ingredients.map(i => i.toLowerCase());
    this.allIngredients.push(...this.ingredients);
    Constants.IngredientsList = [...new Set(this.allIngredients)];

    let product: Product = {
      name: formData.name,
      price: formData.price,
      weight: formData.weight,
      type: formData.type,
      imageBase64: formData.imageBase64,
      ingredients: this.ingredients.join(", "),
    };

    this.dialogRef.close(product);

  }

  // Get form fields
  get name() {
    return this.createForm.get('name');
  }

  get price() {
    return this.createForm.get('price');
  }

  get weight() {
    return this.createForm.get('weight');
  }

  get type() {
    return this.createForm.get('type');
  }
}
