import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppLayoutComponent } from './layouts/app-layout/app-layout.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { AppHeaderComponent } from './components/app-header/app-header.component';
import { AppFooterComponent } from './components/app-footer/app-footer.component';
import { RouterModule } from '@angular/router';
import { AuthSideComponent } from './components/auth-side/auth-side.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { CompareDatesDirective } from './directives/compare-dates.directive';
import { ComparePasswordDirective } from './directives/compare-password.directive';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatMenuModule } from '@angular/material/menu';
import { MatDividerModule } from '@angular/material/divider';
@NgModule({
  declarations: [
    AppLayoutComponent,
    AuthLayoutComponent,
    AppHeaderComponent,
    AppFooterComponent,
    AuthSideComponent,
    PageNotFoundComponent,
    CompareDatesDirective,
    ComparePasswordDirective
  ],
  imports: [
    CommonModule,
    RouterModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatMenuModule,
    MatDividerModule
  ],
  exports: [
    AppFooterComponent,
    AppHeaderComponent,
    ComparePasswordDirective,
    CompareDatesDirective
  ]
})
export class SharedModule { }
