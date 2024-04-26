import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PropertyType } from '../../../shared/interfaces/PropertyType';

@Injectable({
  providedIn: 'root'
})
export class AccommodationService {
  private accommodationsPath: string = environment.accommodationsApiUrl + 'Accommodations/';
  private reservationsPath: string = environment.accommodationsApiUrl + 'Reservations/';
  private visibilityPath: string = environment.accommodationsApiUrl + 'Accommodations/ChangeVisibility/';
  private imagesPath: string = environment.accommodationsApiUrl + 'Images/';
  private roomsPath: string = environment.accommodationsApiUrl + 'Rooms/';
  private mealsPath: string = environment.accommodationsApiUrl + 'Meals/';
  private accommodationTypesPath: string = environment.accommodationsApiUrl + 'AccommodationTypes/';

  constructor(private http: HttpClient) { }

  getAccommodationTypes(): Observable<Array<PropertyType>> {
    return this.http.get<Array<PropertyType>>(this.accommodationTypesPath);
  }

  listing(data: any): Observable<any> {
    return this.http.get(this.accommodationsPath, { params: data });
  }

  filter(data: any): Observable<any> {
    return this.http.get(this.accommodationsPath + 'Filter', { params: data });
  }

  reserve(data: any): Observable<any> {
    return this.http.post(this.reservationsPath, data);
  }

  getReservations(): Observable<Array<any>> {
    return this.http.get<Array<any>>(this.reservationsPath + 'Mine');
  }

  getReservationsByAccommodationId(id: number): Observable<Array<any>> {
    return this.http.get<Array<any>>(this.reservationsPath + 'ByAccommodationId/' + id);
  }

  deleteImage(id: number): Observable<any> {
    return this.http.delete(this.imagesPath + '?id=' + id);
  }

  details(data: any): Observable<any> {
    if (data.town !== undefined) {
      return this.http.get(this.accommodationsPath + data.id + '/Offers', { params: data });
    }

    return this.http.get(this.accommodationsPath + data.id, { params: data });
  }

  create(data: any): Observable<number> {
    return this.http.post<number>(this.accommodationsPath, data);
  }

  changeVisibility(id: number): Observable<boolean> {
    return this.http.put<boolean>(this.visibilityPath + id, null);
  }

  createRoom(data: any): Observable<number> {
    return this.http.post<number>(this.roomsPath, data);
  }

  createMeal(data: any): Observable<number> {
    return this.http.post<number>(this.mealsPath, data);
  }

  edit(id: number, data: any): Observable<any> {
    return this.http.put(this.accommodationsPath + id, data);
  }

  uploadImage(data: any): Observable<number> {
    return this.http.post<number>(this.imagesPath, data);
  }

  mine(): Observable<Array<any>> {
    return this.http.get<Array<any>>(this.accommodationsPath + 'Mine');
  }
}
