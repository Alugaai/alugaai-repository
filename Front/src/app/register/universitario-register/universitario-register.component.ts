import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../_services/auth.service';
import { IRegister } from '../../_models/IRegister';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-universitario-register',
  templateUrl: './universitario-register.component.html',
  styleUrls: ['./universitario-register.component.scss'],
})
export class UniversitarioRegisterComponent implements OnInit {
  student?: IRegister;
  userForm: FormGroup = new FormGroup({});

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.userForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(250)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      birthDate: ['', Validators.required],
      phoneNumber: [
        '',
        [
          Validators.required,
          Validators.minLength(11),
          Validators.maxLength(11),
        ],
      ],
    });
  }

  register() {
    if (this.userForm.valid) {
      this.student = this.userForm.value;
      if (this.student) {
        this.authService.registerStudent(this.student).subscribe({
          next: (response: any) => {
            console.log(response);
            this.toastr.success('Estudante criado com sucesso!');
            this.router.navigate(['/entrar']);
          },
        });
      }
    }
  }
}
