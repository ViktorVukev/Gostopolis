import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GeneralInformationDialogComponent } from './general-information-dialog.component';

describe('GeneralInformationDialogComponent', () => {
  let component: GeneralInformationDialogComponent;
  let fixture: ComponentFixture<GeneralInformationDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GeneralInformationDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(GeneralInformationDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
