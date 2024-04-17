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
  selector: 'app-anunciante-register',
  templateUrl: './anunciante-register.component.html',
  styleUrl: './anunciante-register.component.scss',
})
export class AnuncianteRegisterComponent {
  anunciante?: IRegister;
  anuncianteForm: FormGroup = new FormGroup({});

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.anuncianteForm = this.fb.group({
      username: ['', [Validators.required, Validators.maxLength(250)]],
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

  onSubmit(){
    console.log(this.anuncianteForm.value);
    this.anuncianteForm.reset();
  }

}


