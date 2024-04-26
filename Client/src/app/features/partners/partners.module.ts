import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { AsyncPipe, CommonModule } from '@angular/common';
import { PartnersRoutingModule } from './partners-routing.module';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatStepperModule } from '@angular/material/stepper';
import { ApplicationsComponent } from './components/applications/applications.component';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { ApplyComponent } from './components/apply/apply.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { CreatePropertyComponent } from './components/create-property/create-property.component';
import { GoogleMapsModule } from '@angular/google-maps';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTooltipModule } from '@angular/material/tooltip';
import { QuillEditorComponent, QuillModule } from 'ngx-quill';
import { MatRadioModule } from '@angular/material/radio';
import { MatCardModule } from '@angular/material/card';
import { PartnersService } from './services/partners.service';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { ReservationsComponent } from './components/reservations/reservations.component';

@NgModule({
  declarations: [
    ApplicationsComponent,
    ApplyComponent,
    CreatePropertyComponent,
    ReservationsComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    PartnersRoutingModule,
    MatStepperModule,
    MatInputModule,
    MatButtonModule,
    MatRadioModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    MatToolbarModule,
    MatSlideToggleModule,
    MatFormFieldModule,
    MatCardModule,
    MatSelectModule,
    MatTooltipModule,
    QuillModule.forRoot(),
    GoogleMapsModule,
    MatFormFieldModule,
    MatProgressBarModule,
    MatToolbarModule,
    MatTableModule,
    MatChipsModule,
    MatExpansionModule,
    GoogleMapsModule,
    MatOptionModule,
    MatSlideToggleModule,
    QuillEditorComponent,
    MatChipsModule,
    MatAutocompleteModule,
    AsyncPipe
  ],
  providers: [
    PartnersService
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class PartnersModule { }
