import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private usersPath: string = environment.identityApiUrl + 'Users/';

  constructor(
    private http: HttpClient) { }

  getCurrentUser(): Observable<any> {
    return this.http.get(this.usersPath + 'Current');
  }

  getUser(id: string): Observable<any> {
    return this.http.get(this.usersPath + id);
  }

  update(id: string, data: any): Observable<any> {
    return this.http.put(this.usersPath + id, data);
  }

  updateImage(data: any): Observable<any> {
    return this.http.put(this.usersPath + 'Image', data);
  }

  updateEmailPreferences(data: any): Observable<any> {
    return this.http.put(this.usersPath + 'EmailPreferences', data);
  }
}
