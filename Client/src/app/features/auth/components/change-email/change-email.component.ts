import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-change-email',
  templateUrl: './change-email.component.html',
  styleUrl: './change-email.component.css'
})
export class ChangeEmailComponent implements OnInit {
  isAuthenticated: boolean = this.authService.isAuthenticated();

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(res => {
      this.authService
        .changeEmail({
          userId: res['id'],
          token: res['token'],
          newEmail: res['email']
        }).subscribe();
    });
  }
}
