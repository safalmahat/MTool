import { Component,OnInit,ViewChild,Input } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { StudentRegistration } from '../../models/student-registration-model';
import { AlertService, MessageSeverity } from '../../services/alert.service';
import { ConfigurationService } from '../../services/configuration.service';
import { StudentRegistrationService } from '../../services/student-registration-service';

@Component({
  selector: 'app-studentdetailregistration',
  templateUrl: './studentdetailregistration.component.html',
  styleUrls: ['./studentdetailregistration.component.css'],
  animations: [fadeInOut]
})
export class StudentdetailregistrationComponent implements OnInit {
  @Input()
  isGeneralEditor = false;
  private isEditMode = false;
  private showValidationErrors = false;
  private isEditingSelf = false;
  private student: StudentRegistration = new StudentRegistration();
  private studentEdit: StudentRegistration;
  public changesSavedCallback: () => void;
  public changesFailedCallback: () => void;
  public changesCancelledCallback: () => void;
  public formResetToggle = true;
  private isChangePassword = false;
  isLoading = false;
  private isSaving = false;


  @ViewChild('f')
  private form;

  constructor(private alertService: AlertService, private configurations: ConfigurationService, private studentService: StudentRegistrationService) {

  }
  ngOnInit() {
  }
  private save() {
    debugger;
    this.isSaving = true;
    this.alertService.startLoadingMessage("Saving changes...");
    debugger;
    this.studentService.saveStudent(this.student).subscribe(student => this.saveSuccessHelper(student), error => this.saveFailedHelper(error));

  }
  private saveSuccessHelper(student?: StudentRegistration) {
    debugger;
    this.isSaving = false;
    if (student)
    Object.assign(this.studentEdit, student);
    Object.assign(this.student, this.studentEdit);
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage("Save Success", "Your Registration has been successed:", MessageSeverity.success);
    if (this.changesSavedCallback)
    this.changesSavedCallback();
  }


  private saveFailedHelper(error: any) {
    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage("Save Error", "The below errors occured whilst saving your changes:", MessageSeverity.error, error);
  }
  resetForm(replace = false) {
    this.isChangePassword = false;

    if (!replace) {
        this.form.reset();
    }
    else {
        this.formResetToggle = false;

        setTimeout(() => {
            this.formResetToggle = true;
        });
    }
}
newUser() {
  debugger;
  this.isGeneralEditor = true;
  this.edit();
  return this.studentEdit;
}
private edit() {
  debugger;
  if (!this.isGeneralEditor) {
      this.studentEdit = new StudentRegistration();
      Object.assign(this.studentEdit, this.student);
  }
  else {
      if (!this.studentEdit)
          this.studentEdit = new StudentRegistration();
  }

  this.isEditMode = true;
  this.showValidationErrors = true;
}

}
