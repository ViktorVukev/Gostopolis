import { Component, ElementRef } from '@angular/core';
import { AddTableDialogComponent } from './dialogs/add-table-dialog/add-table-dialog.component';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ProductDialogComponent } from './dialogs/product-dialog/product-dialog.component';
import { MenuDialogComponent } from './dialogs/menu-dialog/menu-dialog.component';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Product } from "../../../../shared/interfaces/Product";
import { Constants } from "../../../../shared/interfaces/Constants";
import { Menu } from "../../../../shared/interfaces/Menu";
import { TableType } from "../../../../shared/interfaces/TableType";
import { RestaurantService } from "../../services/restaurant.service";
import { Table } from "../../../../shared/interfaces/Table";

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrl: './create.component.css'
})
export class CreateComponent {
  createForm: FormGroup;
  daysOfWeek: Array<any> = Constants.DaysOfWeek;
  timeOptions: Array<string> = Constants.TimeOptions;
  productCategories: Array<string> = Constants.CategoriesList;
  isOpen: boolean[] = [];
  menus: Array<Menu> = [];
  products: Array<Product> = [];
  tableTypes: Array<TableType> = [];
  restaurantId!: number;

  constructor(
    public dialog: MatDialog,
    private route: ActivatedRoute,
    private toastrService: ToastrService,
    private restaurantService: RestaurantService,
    private router: Router,
    private fb: FormBuilder) {
    this.createForm = this.fb.group({
      'MondayOpenTime': [null],
      'MondayCloseTime': [null],
      'TuesdayOpenTime': [null],
      'TuesdayCloseTime': [null],
      'WednesdayOpenTime': [null],
      'WednesdayCloseTime': [null],
      'ThursdayOpenTime': [null],
      'ThursdayCloseTime': [null],
      'FridayOpenTime': [null],
      'FridayCloseTime': [null],
      'SaturdayOpenTime': [null],
      'SaturdayCloseTime': [null],
      'SundayOpenTime': [null],
      'SundayCloseTime': [null]
    });
  }

  remainingTimeOptions(day: string, filter: string): any {
    switch (filter) {
      case 'earlier':
        if (this.createForm.get(day + 'CloseTime')?.value !== null) {
          return this.timeOptions.filter(to => this.timeOptions.indexOf(to) < this.timeOptions.indexOf(this.createForm.get(day + 'CloseTime')?.value));
        } else {
          return this.timeOptions;
        }
      case 'later':
        return this.timeOptions.filter(to => this.timeOptions.indexOf(to) > this.timeOptions.indexOf(this.createForm.get(day + 'OpenTime')?.value));
    }
  }

  ngOnInit(): void {
    this.daysOfWeek.forEach(() => {
      this.isOpen.push(false);
    });

    this.restaurantService.getProductIngredients().subscribe((res: any) => Constants.IngredientsList = res.data);

    this.route.queryParams.subscribe(res => {
      this.createForm.addControl("restaurantId", new FormControl(res['id']))
      this.restaurantId = res['id'];
    });
  }

  // Function to update validators based on whether the restaurant is open or not
  updateValidators(day: string, index: number): void {
    const openTime = this.createForm.get(day + 'OpenTime');
    const closeTime = this.createForm.get(day + 'CloseTime');

    if (openTime && closeTime) {
      // Clear existing validators
      openTime.clearValidators();
      closeTime.clearValidators();

      // Add validators based on the condition
      if (this.isOpen[index]) {
        openTime.setValidators([Validators.required]);
        closeTime.setValidators([Validators.required]);
      }

      // Update control's validity
      openTime.updateValueAndValidity();
      closeTime.updateValueAndValidity();
    }
  }

  createWorkingTime(): void {
    this.restaurantService
      .createWorkingTime(this.createForm.value)
      .subscribe();
  }

  openAddTableDialog(): void {
    const dialogRef: MatDialogRef<AddTableDialogComponent> = this.dialog.open(AddTableDialogComponent, {
      width: '20rem'
    });

    dialogRef.afterClosed().subscribe((result: TableType) => {
      console.log(result)
      if (result) {
        // Checks if table type with these parameters already exists
        let index: number = this.tableTypes.findIndex(t => t.capacity === result.capacity && t.isOutdoor === result.isOutdoor && t.isSmokingAllowed == result.isSmokingAllowed);
        if (index === -1) {
          this.tableTypes.push(result);
        } else {
          this.tableTypes[index].tables.push(...result.tables);
        }
      }
    });
  }

  deleteTable(type: TableType, table: Table): void {
    let typeIndex = this.tableTypes.indexOf(type);

    let index = this.tableTypes[typeIndex].tables.indexOf(table);
    if (index !== -1) {
      type.tables.splice(index, 1);
    }
  }

  createTables(): void {
    for (let tableType of this.tableTypes) {
      for (let table of tableType.tables) {
        this.restaurantService
          .createTable(this.restaurantId, table)
          .subscribe();
      }
    }
  }

  createMenusWithProducts(): void {
    for (let menu of this.menus) {
      this.restaurantService.createMenu(this.restaurantId, menu).subscribe((res: any) => {
        for (let product of menu.products) {
          this.restaurantService
            .createProduct(this.restaurantId, product, res.data)
            .subscribe();
        }
      });
    }
  }

  createProductsWithNoMenu(): void {
    for (let product of this.products) {
      this.restaurantService
        .createProduct(this.restaurantId, product)
        .subscribe();
    }
  }

  /* Check if category is empty */
  hasProducts(category: string): boolean {
    return this.products.some(product => product.type === this.productCategories.indexOf(category) + 1);
  }

  getProductsForCategory(index: number): Product[] {
    return this.products.filter(product => product.type === index + 1);
  }

  openProductDialog(): void {
    const dialogRef: MatDialogRef<ProductDialogComponent> = this.dialog.open(ProductDialogComponent, {
      width: '30rem'
    });

    dialogRef.afterClosed().subscribe((result: Product) => {
      if (result) {
        this.products.push(result);
      }
    });
  }

  deleteProduct(product: Product): void {
    let index: number = this.products.indexOf(product);

    this.products.splice(index, 1);
  }

  openMenuDialog(): void {
    const dialogRef: MatDialogRef<MenuDialogComponent> = this.dialog.open(MenuDialogComponent, {
      width: '30rem'
    });

    dialogRef.afterClosed().subscribe((result: Menu) => {
      if (result) {
        this.menus.push(result);
      }
    });
  }

  deleteMenu(menu: Menu): void {
    if (menu.products !== undefined) {
      for (let product of menu.products) {
        this.products.push(product);
      }
    }

    let index: number = this.menus.indexOf(menu);

    this.menus.splice(index, 1);
  }

  addToMenu(menu: Menu, product: Product): void {
    menu.products.push(product);

    let index: number = this.products.indexOf(product);
    this.products.splice(index, 1);
  }

  removeFromMenu(menu: Menu, product: Product): void {
    this.products.push(product);

    let index = menu.products?.indexOf(product);
    if (index !== undefined) {
      menu.products?.splice(index, 1);
    }
  }

  finish(): void {
    this.createWorkingTime();
    this.createTables();
    this.createMenusWithProducts();
    this.createProductsWithNoMenu();
    this.router.navigate(['/']).then(() => this.toastrService.success('Успешно добавихте всичко необходимо, за да стартирате Вашия онлайн бизнес.'));
  }
}
