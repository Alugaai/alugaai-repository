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

  constructor(private fb: FormBuilder,
    private router: Router, private authService: AuthService) { this.authService.userLoggedToken$.subscribe({
      next: (userToken) => {
        this.userLogged = userToken !== null ? true : false;
        if (userToken?.email) {
          this.emailUserLogged = userToken.email;
        }
      },
    });}

    ngOnInit(): void {

      this.authService.userDetailsByEmail(this.emailUserLogged).subscribe({
        next: (response) => {
          this.user = response;
        },
        error: (error) => {
          console.error(error);
        },
      });

      this.studentForm = this.fb.group({
        username: ['', [Validators.maxLength(250)]],
        gender: ['', [Validators.required]],
        birthDate: [''],
        phoneNumber: [
          '',
          [
            Validators.minLength(11),
            Validators.maxLength(11),
          ],
        ],
        collegeId: [ undefined, [Validators.required]],
      });
    }


}
