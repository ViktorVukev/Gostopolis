import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import {ToastrService} from "ngx-toastr";

@Injectable({
  providedIn: 'root'
})

export class ErrorInterceptorService implements HttpInterceptor {
  constructor(
    private toastrService: ToastrService,
    private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      retry(1),
      catchError((err) => {
        if (err.status === 401) {
          this.router.navigate(['/auth/login']);
        } else if (err.status === 404) {
          this.router.navigate(['/not-found']);
        } else {
          console.log(err.error);
          for (let error of err.error) {
            this.toastrService.error(error);
          }
        }

        return throwError(err);
      })
    );
  }
}
