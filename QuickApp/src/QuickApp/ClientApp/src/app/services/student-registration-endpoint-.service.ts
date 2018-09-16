// ====================================================
// More Templates: https://www.ebenmonney.com/templates
// Email: support@ebenmonney.com
// ====================================================

import { Injectable, Injector } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { EndpointFactory } from './endpoint-factory.service';
import { ConfigurationService } from './configuration.service';


@Injectable()
export class StudentEndpoint extends EndpointFactory {

  private readonly _studentUrl: string = "/api/student";
  private readonly _userByUserNameUrl: string = "/api/account/users/username";
  private readonly _currentUserUrl: string = "/api/account/users/me";
  private readonly _currentUserPreferencesUrl: string = "/api/account/users/me/preferences";
  private readonly _unblockUserUrl: string = "/api/account/users/unblock";
  private readonly _rolesUrl: string = "/api/account/roles";
  private readonly _roleByRoleNameUrl: string = "/api/account/roles/name";
  private readonly _permissionsUrl: string = "/api/account/permissions";

  get studentUrl() { return this.configurations.baseUrl + this._studentUrl; }
  get userByUserNameUrl() { return this.configurations.baseUrl + this._userByUserNameUrl; }
  get currentUserUrl() { return this.configurations.baseUrl + this._currentUserUrl; }
  get currentUserPreferencesUrl() { return this.configurations.baseUrl + this._currentUserPreferencesUrl; }
  get unblockUserUrl() { return this.configurations.baseUrl + this._unblockUserUrl; }
  get rolesUrl() { return this.configurations.baseUrl + this._rolesUrl; }
  get roleByRoleNameUrl() { return this.configurations.baseUrl + this._roleByRoleNameUrl; }
  get permissionsUrl() { return this.configurations.baseUrl + this._permissionsUrl; }



  constructor(http: HttpClient, configurations: ConfigurationService, injector: Injector) {

    super(http, configurations, injector);
  }

  save<T>(studentObject: any): Observable<T> {

    return this.http.post<T>(this.studentUrl, JSON.stringify(studentObject), this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.save(studentObject));
      }));
  }
  getAllStudentEndpoint<T>(page?: number, pageSize?: number): Observable<T> {
    return this.http.get<T>(this.studentUrl, this.getRequestHeaders()).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getAllStudentEndpoint(page, pageSize));
      }));
  }

}
