import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentdetailregistrationComponent } from './studentdetailregistration.component';

describe('StudentdetailregistrationComponent', () => {
  let component: StudentdetailregistrationComponent;
  let fixture: ComponentFixture<StudentdetailregistrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudentdetailregistrationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentdetailregistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
