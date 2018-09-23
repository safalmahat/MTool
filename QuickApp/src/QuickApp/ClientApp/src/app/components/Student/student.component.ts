// ====================================================
// More Templates: https://www.ebenmonney.com/templates
// Email: support@ebenmonney.com
// ====================================================

import { Component, OnInit, AfterViewInit, TemplateRef, ViewChild, Input } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { fadeInOut } from '../../services/animations';
import { StudentRegistration } from '../../models/student-registration-model';
import { AlertService, DialogType, MessageSeverity } from '../../services/alert.service';
import { ConfigurationService } from '../../services/configuration.service';
import { StudentRegistrationService } from '../../services/student-registration-service';
import { AppTranslationService } from '../../services/app-translation.service';
import { Utilities } from '../../services/utilities';
import { StudentdetailregistrationComponent } from "../studentdetailregistration/studentdetailregistration.component";
@Component({
  selector: 'student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css'],
  animations: [fadeInOut]
})
export class StudentComponent {
  student = new StudentRegistration();
  isLoading = false;
  columns: any[] = [];
  rows: StudentRegistration[] = [];
  rowsCache: StudentRegistration[] = [];
  editingUserName: { name: string };
  editedStudent: StudentRegistration;
  sourceStudent: StudentRegistration;
  loadingIndicator: boolean;
  private isSaving = false;

  @ViewChild('indexTemplate')
  indexTemplate: TemplateRef<any>;

  @ViewChild('tokenTemplate')
  tokenTemplate: TemplateRef<any>;

  @ViewChild('firstNameTemplate')
  firstNameTemplate: TemplateRef<any>;

  @ViewChild('middleNameTemplate')
  middleNameTemplate: TemplateRef<any>;

  @ViewChild('lastNameTemplate')
  lastNameTemplate: TemplateRef<any>;

  @ViewChild('emailTemplate')
  emailTemplate: TemplateRef<any>;


  @ViewChild('addressTemplate')
  addressTemplate: TemplateRef<any>;

  @ViewChild('actionsTemplate')
  actionsTemplate: TemplateRef<any>;

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  @ViewChild('studentDetail')
  studentDetail: StudentdetailregistrationComponent;

  constructor(private alertService: AlertService, private translationService: AppTranslationService, private configurations: ConfigurationService, private studentService: StudentRegistrationService) {

  }

  ngOnInit() {

    let gT = (key: string) => this.translationService.getTranslation(key);

    this.columns = [
      { prop: "index", name: '#', width: 40, canAutoResize: false },
      { prop: 'Token', name: "Token", width: 50, cellTemplate: this.tokenTemplate },
      { prop: 'FirstName', name: "FirstName", width: 90, cellTemplate: this.firstNameTemplate },
      { prop: 'MiddleName', name: "MiddleName", width: 120, cellTemplate: this.middleNameTemplate },
      { prop: 'LastName', name: "LastName", width: 120, cellTemplate: this.middleNameTemplate },
      { prop: 'Email', name: "Email", width: 140, cellTemplate: this.emailTemplate },
      { prop: 'Address', name: "Address", width: 140, cellTemplate: this.addressTemplate }
    ];


    this.columns.push({ name: '', width: 130, cellTemplate: this.actionsTemplate, resizeable: false, canAutoResize: false, sortable: false, draggable: false });

    this.loadData();
  }

  //get canManageUsers() {
  //  return this.accountService.userHasPermission(Permission.manageUsersPermission);
  //}
  ngAfterViewInit() {

    this.studentDetail.changesSavedCallback = () => {
       this.addNewStudentrToList();
        this.editorModal.hide();
    };

    this.studentDetail.changesCancelledCallback = () => {
        this.editedStudent = null;
        this.sourceStudent = null;  
        this.editorModal.hide();
    };
}
addNewStudentrToList() {
  debugger;
  if (this.sourceStudent) {
      Object.assign(this.sourceStudent, this.editedStudent);

      let sourceIndex = this.rowsCache.indexOf(this.sourceStudent, 0);
      if (sourceIndex > -1)
          Utilities.moveArrayItem(this.rowsCache, sourceIndex, 0);

      sourceIndex = this.rows.indexOf(this.sourceStudent, 0);
      if (sourceIndex > -1)
          Utilities.moveArrayItem(this.rows, sourceIndex, 0);

      this.editedStudent = null;
      this.sourceStudent = null;
  }
  else {
      let user = new StudentRegistration();
      Object.assign(user, this.editedStudent);
      this.editedStudent = null;

      let maxIndex = 0;
      for (let u of this.rowsCache) {
          if ((<any>u).index > maxIndex)
              maxIndex = (<any>u).index;
      }

      (<any>user).index = maxIndex + 1;

      this.rowsCache.splice(0, 0, user);
      this.rows.splice(0, 0, user);
      this.rows = [...this.rows];
  }
}
  loadData() {
    this.alertService.startLoadingMessage();
    this.loadingIndicator = true;
    this.studentService.getAllStudent().subscribe(students => this.onDataLoadSuccessful(students)), error => this.onDataLoadFailed(error);
  }

  importStudentData() {

    this.alertService.startLoadingMessage();
    this.loadingIndicator = true;
    this.studentService.importStudents().subscribe(students => this.onDataLoadSuccessful(students)), error => this.onDataLoadFailed(error);
  }

  onDataLoadSuccessful(students: StudentRegistration[]) {

    this.alertService.stopLoadingMessage();
    this.loadingIndicator = false;

    students.forEach((student, index, students) => {
      (<any>student).index = index + 1;
    });
    this.rowsCache = [...students];
    this.rows = students;
  }


  onDataLoadFailed(error: any) {
    this.alertService.stopLoadingMessage();
    this.loadingIndicator = false;

  }

  onEditorModalHidden() {
   // this.editingUserName = null;
    this.studentDetail.resetForm(true);
   }
  onSearchChanged(value: string) {
    this.rows = this.rowsCache.filter(r => Utilities.searchArray(value, false, r.FirstName, r.LastName, r.Token));
  }

  newStudent() {
    this.editingUserName = null;
    this.sourceStudent = null;
    this.editedStudent = this.studentDetail.newUser();
    this.editorModal.show();
}

}
