import { Component, OnInit,Input } from '@angular/core';
import { StudentFollowUpModel } from '../../models/student-followup-model';
import { AccountService } from '../../services/account.service';
import { Permission } from '../../models/permission.model';
@Component({
  selector: 'app-studentfollowup',
  templateUrl: './studentfollowup.component.html',
  styleUrls: ['./studentfollowup.component.css']
})
export class StudentfollowupComponent implements OnInit {
  @Input()
  isGeneralEditor = false;
  public formResetToggle = true;
  private isSaving = false;
  private followup: StudentFollowUpModel = new StudentFollowUpModel();
  constructor( private accountService: AccountService) {

  }


  ngOnInit() {
  }
  followUpStudent(row  : StudentFollowUpModel) {
    this.isGeneralEditor = true;
 
    return new StudentFollowUpModel;
  }
  get canAssignStudent() {
    var result = this.accountService.userHasPermission(Permission.assignStudentsPermission); //eg. viewProductsPermission
    return result;
  }

}
