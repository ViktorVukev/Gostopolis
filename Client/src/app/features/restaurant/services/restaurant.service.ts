import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PropertyType } from '../../../shared/interfaces/PropertyType';
import {Table} from "../../../shared/interfaces/Table";
import {Product} from "../../../shared/interfaces/Product";
import {Menu} from "../../../shared/interfaces/Menu";

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
  private restaurantsPath: string = environment.restaurantsApiUrl + 'Restaurants/';
  private reservationsPath: string = environment.restaurantsApiUrl + 'Reservations/';
  private visibilityPath: string = environment.restaurantsApiUrl + 'Restaurants/ChangeVisibility/';
  private workingTimesPath: string = environment.restaurantsApiUrl + 'WorkingTimes/';
  private tablesPath: string = environment.restaurantsApiUrl + 'Tables/';
  private productsPath: string = environment.restaurantsApiUrl + 'Products/';
  private imagesPath: string = environment.restaurantsApiUrl + 'Images/';
  private ingredientsPath: string = environment.restaurantsApiUrl + 'Ingredients/';
  private menusPath: string = environment.restaurantsApiUrl + 'Menus/';
  private restaurantTypesPath: string = environment.restaurantsApiUrl + 'RestaurantTypes/';

  constructor(private http: HttpClient) { }

  getRestaurantTypes(): Observable<any> {
    return this.http.get<any>(this.restaurantTypesPath);
  }

  getProductIngredients(): Observable<any> {
    return this.http.get<any>(this.ingredientsPath);
  }

  getReservations(): Observable<any> {
    return this.http.get<any>(this.reservationsPath + 'Mine');
  }

  getReservationsByRestaurantId(id: number): Observable<any> {
    return this.http.get<any>(this.reservationsPath + 'ByRestaurantId/' + id);
  }


  listing(data: any): Observable<any> {
    return this.http.get(this.restaurantsPath, { params: data });
  }

  filter(data: any): Observable<any> {
    return this.http.get(this.restaurantsPath + 'Filter', { params: data });
  }

  reserve(data: any): Observable<any> {
    return this.http.post(this.reservationsPath, data);
  }

  details(data: any): Observable<any> {
    if (data.town !== undefined) {
      return this.http.get(this.restaurantsPath + data.id + '/Offers', { params: data });
    }

    return this.http.get(this.restaurantsPath + data.id, { params: data });
  }

  create(data: any): Observable<any> {
    return this.http.post<any>(this.restaurantsPath, data);
  }

  update(id: any, data: any): Observable<any> {
    return this.http.put<any>(this.restaurantsPath + id, data);
  }

  changeVisibility(id: number): Observable<boolean> {
    return this.http.put<boolean>(this.visibilityPath + id, null);
  }

  createWorkingTime(data: any): Observable<number> {
    return this.http.post<number>(this.workingTimesPath, data);
  }

  updateWorkingTime(data: any): Observable<any> {
    return this.http.put(this.workingTimesPath, data);
  }

  createTable(restaurantId: number, data: Table): Observable<number> {
    return this.http.post<number>(this.tablesPath, {
      restaurantId: restaurantId,
      ...data
    });
  }

  deleteTable(id: any): Observable<any> {
    return this.http.delete(this.tablesPath + '?id=' + id);
  }

  createMenu(restaurantId: number, data: Menu): Observable<any> {
    return this.http.post<any>(this.menusPath, {
      restaurantId: restaurantId,
      name: data.name
    });
  }

  createProduct(restaurantId: number, data: Product, menuId?: number): Observable<number> {
    return this.http.post<number>(this.productsPath, {
      restaurantId: restaurantId,
      menuId: menuId,
      ...data
    });
  }

  deleteProduct(id: any): Observable<any> {
    return this.http.delete(this.productsPath + '?id=' + id);
  }

  uploadImage(data: any): Observable<number> {
    return this.http.post<number>(this.imagesPath, data);
  }

  deleteImage(id: number): Observable<any> {
    return this.http.delete(this.imagesPath + '?id=' + id);
  }

  mine(): Observable<Array<any>> {
    return this.http.get<Array<any>>(this.restaurantsPath + 'Mine');
  }

  checkIfRestaurantIsOpen(workingTime: any): boolean {
    const date = new Date();

    switch (date.getDay()) {
      case 0:
        return this.compareTime(date, workingTime.sundayOpenTime, workingTime.sundayCloseTime);
      case 1:
        return this.compareTime(date, workingTime.mondayOpenTime, workingTime.mondayCloseTime);
      case 2:
        return this.compareTime(date, workingTime.tuesdayOpenTime, workingTime.tuesdayCloseTime);
      case 3:
        return this.compareTime(date, workingTime.wednesdayOpenTime, workingTime.wednesdayCloseTime);
      case 4:
        return this.compareTime(date, workingTime.thursdayOpenTime, workingTime.thursdayCloseTime);
      case 5:
        return this.compareTime(date, workingTime.fridayOpenTime, workingTime.fridayCloseTime);
      case 6:
        return this.compareTime(date, workingTime.saturdayOpenTime, workingTime.saturdayCloseTime);
      default:
        return false;
    }
  }

  private compareTime(currentTime: Date, openTime: any, closeTime: any): boolean {
    if (openTime === null && closeTime === null) return false;

    let openTimeDate = new Date(`${currentTime.getFullYear()}-${((currentTime.getMonth() + 1) < 10 ? '0' : '') + (currentTime.getMonth() + 1)}-${(currentTime.getDate() < 10 ? '0' : '') + currentTime.getDate()}T${openTime}`);
    let closeTimeDate = new Date(`${currentTime.getFullYear()}-${((currentTime.getMonth() + 1) < 10 ? '0' : '') + (currentTime.getMonth() + 1)}-${(currentTime.getDate() < 10 ? '0' : '') + currentTime.getDate()}T${closeTime}`);

    return currentTime >= openTimeDate && currentTime <= closeTimeDate;
  }
}
