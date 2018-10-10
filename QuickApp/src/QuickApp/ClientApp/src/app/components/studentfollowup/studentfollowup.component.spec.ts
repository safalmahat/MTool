import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentfollowupComponent } from './studentfollowup.component';

describe('StudentfollowupComponent', () => {
  let component: StudentfollowupComponent;
  let fixture: ComponentFixture<StudentfollowupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentfollowupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentfollowupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
