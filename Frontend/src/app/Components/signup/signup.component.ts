import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from 'src/app/Services/auth.service';
import { Router } from '@angular/router';
import { AppRoutingModule } from 'src/app/app-routing.module';
@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  signupForm!: FormGroup;
  fb!:FormBuilder;
  constructor(fb:FormBuilder,private auth:AuthService,private router:Router){
    this.fb=fb;
  }
  ngOnInit(): void {
    this.signupForm=this.fb.group({
      FirstName:['',Validators.required],
      LastName:['',Validators.required],
      Email:['',Validators.required],
      Username:['',Validators.required],
      Password:['',Validators.required],
    })
  }
  onSignup(): void {
    if (this.signupForm.valid) {
      this.auth.Register(this.signupForm.value).subscribe({
        next: (res) => {
          alert("Success"+ res);
          this.router.navigate(['login']);
        },
        error: (err) => {
          console.error(err)
          alert("Error " + err.error.message);
        }
      });
    } else {
      alert('Invalid form');
    }
  }

}
