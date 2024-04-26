import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeContactDialogComponent } from './change-contact-dialog.component';

describe('ChangeContactDialogComponent', () => {
  let component: ChangeContactDialogComponent;
  let fixture: ComponentFixture<ChangeContactDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ChangeContactDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ChangeContactDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
