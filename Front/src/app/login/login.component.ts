import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { ILogin } from '../_models/ILogin';
import { IUserToken } from '../_models/IUserToken';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'], // Corrected typo here
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup = new FormGroup({});

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  login() {
    if (this.loginForm.valid) {
      const loginBody: ILogin = this.loginForm.value;
      this.authService.login(loginBody).subscribe({
        next: (response: IUserToken) => {
          this.toastr.success('Login successful'); // Moved toastr inside success block
          this.router.navigate(['home']);
        },
      });
    }
  }
}
