import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/servicios/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm!:FormGroup;

  constructor(
    private readonly fb:FormBuilder,
    private readonly router:Router,
    private readonly loginService:LoginService
  ) 
  { }

  ngOnInit(): void {
    this.loginForm = this.initForm(); 
  }

  initForm(): FormGroup {
    return this.fb.group(
      {
        user:[''],
        password:['']
      }
    );
  }

  onSubmit(): void {
    console.log('Login form -> submit event');

    const user = {
      user: this.loginForm.get('user')?.value,
      password: this.loginForm.get('password')?.value
    }

    //llamada al servicio
    this.loginService.loginUser(user.user, user.password).subscribe( response => {
      console.log('JWT Token -> ', response['value']);
      var bearerToken = response['value'];
      sessionStorage.setItem('auth-token', bearerToken);
      setTimeout(() => {
        //Navegación hacia el componente home
        this.router.navigate(['/gestion-heroes']);
      }, 1000)

    }, (err:HttpErrorResponse)=>{

      alert("Error: "+err.error);

    });

  }

}
