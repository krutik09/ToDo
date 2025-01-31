import { Component, NgModule, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { HttpHeaders } from '@angular/common/http';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})

export class LoginComponent implements OnInit {
  loginForm!:FormGroup
  fb: any;
  constructor(fb:FormBuilder,private auth:AuthService,private router:Router){
    this.fb=fb;
  }
  ngOnInit(): void {
    this.loginForm=this.fb.group({
      username:['',Validators.required],
      password:['',Validators.required],
    })

  }
  onLogin(){
    if(this.loginForm.valid){
     // alert("form is valid"+this.loginForm.value);
      this.auth.Login(this.loginForm.value).subscribe
      ({
        next:(res)=>{
          alert("success")
          localStorage.setItem("token",res.token)
          this.router.navigate(['todo']);
        },
        error:(err)=>{
          alert(err.error.message);
        }
        
    })
    }
    else{
      alert("Invalid form");
    }
  }

}
