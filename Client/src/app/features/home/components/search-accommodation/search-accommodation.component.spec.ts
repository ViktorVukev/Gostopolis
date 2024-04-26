import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SearchAccommodationComponent } from './search-accommodation.component';

describe('SearchComponent', () => {
  let component: SearchAccommodationComponent;
  let fixture: ComponentFixture<SearchAccommodationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SearchAccommodationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SearchAccommodationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
