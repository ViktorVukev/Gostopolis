import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Application } from '../../../shared/interfaces/Application';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccommodationService } from '../../accommodation/services/accommodation.service';
import { RestaurantService } from '../../restaurant/services/restaurant.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class PartnersService {
  private applicationsPath: string = environment.identityApiUrl + 'Applications/';
  private partnersPath: string = environment.identityApiUrl + 'Users/';

  constructor(
    private http: HttpClient,
    private accommodationService: AccommodationService,
    private restaurantService: RestaurantService,
    private toastrService: ToastrService,
    private router: Router) { }

  create(data: any): Observable<any> {
    return this.http.post(this.applicationsPath, data);
  }

  isPartner(): Observable<any> {
    return this.http.get(this.partnersPath + 'IsPartner');
  }

  hasApplied(): Observable<any> {
    return this.http.get(this.applicationsPath + 'HasApplied');
  }

  hasPendingApplication(): Observable<any> {
    return this.http.get(this.applicationsPath + 'HasPendingApplication');
  }

  applications(): Observable<Array<Application>> {
    return this.http.get<Array<Application>>(this.applicationsPath + 'Mine');
  }

  validateCreateForm(form: FormGroup, photos: Array<string>): void {
    if (form.value.typeId === 0) {
      this.toastrService.error('Моля, изберете тип на обекта.');
      return;
    }

    if (form.value.ownershipDocumentBase64 === '') {
      this.toastrService.error('Моля, прикачете документ, с който да удостоверите, че притежавате обекта.');
      return;
    }

    if (form.value.address === '') {
      this.toastrService.error('Моля, изберете адреса, на който се намира обекта.');
      return;
    }

    if (photos.length < 5) {
      this.toastrService.error('Прикачете поне 5 изображения преди да създадете обект.');
      return;
    }
  }

  createProperty(category: string, form: FormGroup, photos: Array<any>): void {
    if (category === 'accommodation') {
      this.accommodationService
        .create(form.value)
        .subscribe(res => {
          this.uploadPhotos(category, res, photos);
        });
    } else if (category === 'restaurant') {
      this.restaurantService
        .create(form.value)
        .subscribe(res => {
          this.uploadPhotos(category, res.data, photos);
        });
    }
  }

  uploadPhotos(category: string, propertyId: number, photos: Array<any>): void {
    if (category === 'accommodation') {
      for (const file of <Array<File>>photos) {
        if (file.size > 10485760) {
          this.toastrService.error('Снимката не трябва да надвишава 10MB.');
          return;
        }

        this.accommodationService
          .uploadImage({
            imageBase64: file,
            accommodationId: propertyId
          })
          .subscribe();
      }

      this.router.navigate(['/accommodation/create'], { queryParams: { id: propertyId } });
    } else if (category === 'restaurant') {
      for (const file of <Array<File>>photos) {
        if (file.size > 10485760) {
          this.toastrService.error('Снимката не трябва да надвишава 10MB.');
          return;
        }

        this.restaurantService
          .uploadImage({
            imageBase64: file,
            restaurantId: propertyId
          })
          .subscribe();
      }

      this.router.navigate(['/restaurant/create'], { queryParams: { id: propertyId } });
    }
  }
}
