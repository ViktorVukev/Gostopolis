import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../features/auth/services/auth.service';
import { Router } from '@angular/router';
import { PartnersService } from '../../../features/partners/services/partners.service';
import { UserService } from '../../../features/auth/services/user.service';

@Component({
  selector: 'app-app-header',
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.css']
})
export class AppHeaderComponent implements OnInit {
  isAuthenticated: boolean = this.authService.isAuthenticated();
  user: any;
  hasApplied: boolean = false;
  isPartner: boolean = false;

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private partnersService: PartnersService,
    private router: Router) { }

  ngOnInit(): void {
    if (this.isAuthenticated) {
      this.userService
        .getCurrentUser()
        .subscribe(res => this.user = res);

      this.partnersService
        .isPartner()
        .subscribe(res => this.isPartner = res);

      this.partnersService
        .hasApplied()
        .subscribe(res => this.hasApplied = res);
    }
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
