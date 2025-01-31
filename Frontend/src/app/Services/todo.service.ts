import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { Todo } from '../Models/Todo';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  private baseUrl:string="https://localhost:7208/api/Todo/"
  constructor(private http:HttpClient,private auth:AuthService) { }
  GetTodo():Observable<Array<any>>{
    const headers=new HttpHeaders().set('Authorization',`Bearer ${localStorage.getItem("token")}`)
    return this.http.get<Array<any>>(`${this.baseUrl}GetTodo`,{headers});
  }
  AddTodo(addtodoform:any){
    const headers=new HttpHeaders().set('Authorization',`Bearer ${localStorage.getItem("token")}`)
    return this.http.post<any>(`${this.baseUrl}AddTodo`,addtodoform,{headers});
  }
  DeleteTodo(title:any){
    const headers=new HttpHeaders().set('Authorization',`Bearer ${localStorage.getItem("token")}`)
    return this.http.delete<any>(`${this.baseUrl}DeleteTodo/${title}`,{headers},);
  }
  EditTodo(title:any,description:any){
    const headers=new HttpHeaders().set('Authorization',`Bearer ${localStorage.getItem("token")}`)
    return this.http.post<any>(`${this.baseUrl}EditTodo/${title}/${description}`,{title,description},{headers});
  }
}
