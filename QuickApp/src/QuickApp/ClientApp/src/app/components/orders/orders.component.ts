import { Component } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { StudentRegistration } from '../../models/student-registration-model';
import { AlertService, MessageSeverity } from '../../services/alert.service';
import { ConfigurationService } from '../../services/configuration.service';
import { StudentRegistrationService } from '../../services/student-registration-service';
@Component({
  selector: 'orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
  animations: [fadeInOut]
})
export class OrdersComponent {
  student = new StudentRegistration();
  isLoading = false;
  private isSaving = false;
  constructor(private alertService: AlertService, private configurations: ConfigurationService, private studentService: StudentRegistrationService) {

  }
  private save() {
    debugger;
    this.isSaving = true;
    this.alertService.startLoadingMessage("Saving changes...");
    debugger;
    this.studentService.saveStudent(this.student).subscribe(student => this.saveSuccessHelper(student), error => this.saveFailedHelper(error));

  }
  private saveSuccessHelper(student?: StudentRegistration) {
    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage("Save Success", "Your Registration has been successed:", MessageSeverity.success);
  }


  private saveFailedHelper(error: any) {
    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage("Save Error", "The below errors occured whilst saving your changes:", MessageSeverity.error, error);
  }

}
