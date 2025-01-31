import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Components/login/login.component';
import { SignupComponent } from './Components/signup/signup.component';
import { HttpClientModule, HttpHeaders } from '@angular/common/http';
import { TodoComponent } from './Components/todo/todo.component';
import { CommonModule } from '@angular/common';
import { TodoItemComponent } from './Components/todo-item/todo-item.component';
import { AddTodoComponent } from './Components/add-todo/add-todo.component';
import { DxButtonModule } from 'devextreme-angular/ui/button';
import { DevExtremeModule } from 'devextreme-angular';
@NgModule({
  declarations: [
    TodoItemComponent,
    AppComponent,
    LoginComponent,
    SignupComponent,
    TodoComponent,
    AddTodoComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    CommonModule,
   FormsModule,
   DxButtonModule,
   DevExtremeModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
