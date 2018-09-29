// ====================================================
// More Templates: https://www.ebenmonney.com/templates
// Email: support@ebenmonney.com
// ====================================================

import { Injectable } from '@angular/core';
import { Router, NavigationExtras } from "@angular/router";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { map } from 'rxjs/operators';

import { LocalStoreManager } from './local-store-manager.service';
import { EndpointFactory } from './endpoint-factory.service';
import { ConfigurationService } from './configuration.service';
import { DBkeys } from './db-Keys';
import { JwtHelper } from './jwt-helper';
import { Utilities } from './utilities';
import { LoginResponse, IdToken } from '../models/login-response.model';
import { User } from '../models/user.model';
import { Permission, PermissionNames, PermissionValues } from '../models/permission.model';
import { StudentRegistration } from '../models/student-registration-model';
import { StudentEndpoint } from './student-registration-endpoint-.service';
import { MarketingStudentList } from '../models/marketingstudent.model';

@Injectable()
export class StudentRegistrationService {
  private readonly _studentUrl: string = "/api/student/users";
  private readonly _userByUserNameUrl: string = "/api/account/users/username";
  private readonly _currentUserUrl: string = "/api/account/users/me";
  private readonly _currentUserPreferencesUrl: string = "/api/account/users/me/preferences";
  private readonly _unblockUserUrl: string = "/api/account/users/unblock";
  private readonly _rolesUrl: string = "/api/account/roles";
  private readonly _roleByRoleNameUrl: string = "/api/account/roles/name";
  private readonly _permissionsUrl: string = "/api/account/permissions";

  get studentUrl() { return this.configurations.baseUrl + this._studentUrl; }


  constructor(private http: HttpClient, private router: Router, private configurations: ConfigurationService, private endpointFactory: EndpointFactory, private localStorage: LocalStoreManager, private studentEndpoint: StudentEndpoint) {
  }
  saveStudent(studuent: StudentRegistration) {
    return this.studentEndpoint.save<StudentRegistration>(studuent);
  }

  assignStudent(marketing: MarketingStudentList) {
    return this.studentEndpoint.assignStudent<MarketingStudentList>(marketing);
  }


  getAllStudent() {
    return this.studentEndpoint.getAllStudentEndpoint<StudentRegistration[]>();
  }
  importStudents() {
    return this.studentEndpoint.importStudentEndpoint<StudentRegistration[]>();
  }

}
