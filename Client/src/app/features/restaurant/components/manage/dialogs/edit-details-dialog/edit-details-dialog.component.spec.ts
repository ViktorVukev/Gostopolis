import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditDetailsDialogComponent } from './edit-details-dialog.component';

describe('EditDetailsDialogComponent', () => {
  let component: EditDetailsDialogComponent;
  let fixture: ComponentFixture<EditDetailsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditDetailsDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditDetailsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
