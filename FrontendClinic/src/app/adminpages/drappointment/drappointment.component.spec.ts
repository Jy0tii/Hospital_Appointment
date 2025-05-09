import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DrappointmentComponent } from './drappointment.component';

describe('DrappointmentComponent', () => {
  let component: DrappointmentComponent;
  let fixture: ComponentFixture<DrappointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DrappointmentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DrappointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
