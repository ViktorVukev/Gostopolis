import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  private filePath = environment.identityApiUrl + 'files/';

  constructor(private http: HttpClient) { }

  upload(data: string): Observable<any> {
    let file = {
      base64: data
    };

    return this.http.post(this.filePath, file, {responseType: 'text'});
  }

}
