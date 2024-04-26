import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable, NgModule } from '@angular/core';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { environment } from '../../../../environments/environment';

@NgModule({
  imports: [HttpClientModule]
})

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private registerPath: string = environment.identityApiUrl + 'Identity/Register/';
  private resendPath: string = environment.identityApiUrl + 'Identity/ResendVerification';
  private confirmEmailPath: string = environment.identityApiUrl + 'Identity/ConfirmEmail';
  private resetPasswordPath: string = environment.identityApiUrl + 'Identity/ResetPassword';
  private changePasswordPath: string = environment.identityApiUrl + 'Identity/ChangePassword';
  private loginPath: string = environment.identityApiUrl + 'Identity/Login/';
  private resetEmailPath: string = environment.identityApiUrl + 'Identity/ResetEmail';
  private changeEmailPath: string = environment.identityApiUrl + 'Identity/ChangeEmail';

  constructor(
    private http: HttpClient,
    private cookies: CookieService) { }

  register(data: any): Observable<any> {
    return this.http.post(this.registerPath, data);
  }

  resend(userId: any): Observable<any> {
    return this.http.post(this.resendPath + "?userId=" + userId, userId);
  }

  confirmEmail(data: any): Observable<any> {
    return this.http.post(this.confirmEmailPath, data);
  }

  resetPassword(data: any): Observable<any> {
    return this.http.post(this.resetPasswordPath, data);
  }

  changePassword(data: any): Observable<any> {
    return this.http.put(this.changePasswordPath, data);
  }

  login(data: any): Observable<any> {
    return this.http.post(this.loginPath, data);
  }

  resetEmail(data: any): Observable<any> {
    return this.http.post(this.resetEmailPath, data);
  }

  changeEmail(data: any): Observable<any> {
    return this.http.put(this.changeEmailPath, data);
  }

  logout(): void {
    this.cookies.delete('auth');
  }

  saveToken(token: string): void {
    this.cookies.set('auth', token);
  }

  getToken(): string {
    return this.cookies.get('auth');
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }
}
