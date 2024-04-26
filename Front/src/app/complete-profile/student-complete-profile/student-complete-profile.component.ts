import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from 'express';
import { IStudentCompleteProfile } from '../../_models/IStudentCompleteProfile';
import { AuthService } from '../../_services/auth.service';
import { IUserDetails } from '../../_models/IUserDetails';

@Component({
  selector: 'app-student-complete-profile',
  templateUrl: './student-complete-profile.component.html',
  styleUrl: './student-complete-profile.component.scss'
})
export class StudentCompleteProfileComponent {

  userLogged: boolean = false;
  emailUserLogged: string = '';
  user?: IUserDetails;
  student?: IStudentCompleteProfile;
  studentForm: FormGroup = new FormGroup({});




}
