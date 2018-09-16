// ====================================================
// More Templates: https://www.ebenmonney.com/templates
// Email: support@ebenmonney.com
// ====================================================

import { Component, OnInit, AfterViewInit, TemplateRef, ViewChild, Input } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { StudentRegistration } from '../../models/student-registration-model';
import { AlertService, MessageSeverity } from '../../services/alert.service';
import { ConfigurationService } from '../../services/configuration.service';
import { StudentRegistrationService } from '../../services/student-registration-service';
import { User } from '../../models/user.model';
import { AppTranslationService } from '../../services/app-translation.service';
import { Utilities } from '../../services/utilities';
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
  constructor(private alertService: AlertService, private translationService: AppTranslationService, private configurations: ConfigurationService, private studentService: StudentRegistrationService) {

  }

  ngOnInit() {
    debugger;
    let gT = (key: string) => this.translationService.getTranslation(key);

    this.columns = [
      { prop: "index", name: '#', width: 40, canAutoResize: false },
      { prop: 'Token', name: "Token", width: 50, cellTemplate: this.tokenTemplate},
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

  
  loadData() {
    debugger;
    this.alertService.startLoadingMessage();
    this.loadingIndicator = true;
    this.studentService.getAllStudent().subscribe(students => this.onDataLoadSuccessful(students)), error => this.onDataLoadFailed(error);
  }


  onDataLoadSuccessful(students: StudentRegistration[]) {
    debugger;
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
  onSearchChanged(value: string) {
    debugger;
    this.rows = this.rowsCache.filter(r => Utilities.searchArray(value, false, r.firstName, r.lastName,r.token));
  }

}
