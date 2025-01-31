import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
HttpClient
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl:string="https://localhost:7208/api/User/"
  constructor(private http:HttpClient) { 
  }
  Register(signupuser:any){
    return this.http.post<any>(`${this.baseUrl}Register`,signupuser)
  }
  Login(loginuser:any){
    return this.http.post<any>(`${this.baseUrl}Login`,loginuser)
  }
}
