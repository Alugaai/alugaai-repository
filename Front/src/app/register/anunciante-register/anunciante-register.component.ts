import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';


@Component({
  selector: 'app-anunciante-register',
  templateUrl: './anunciante-register.component.html',
  styleUrl: './anunciante-register.component.scss',
})
export class AnuncianteRegisterComponent {
    anuncianteForm = this.fb.group({
    Username: ['', [Validators.required, Validators.minLength(3)]] ,
    Email : ['', Validators.required],
    Password : ['', Validators.required],
    BirthDate: ['', Validators.required],
    PhoneNumber : [''],
  })

  constructor(private fb: FormBuilder){}

  onSubmit(){
    console.log(this.anuncianteForm.value);
    this.anuncianteForm.reset();
  }

}


