import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TodoService } from '../../Services/todo.service';
import { Todo } from 'src/app/Models/Todo';

@Component({
  selector: 'app-add-todo',
  templateUrl: './add-todo.component.html',
  styleUrls: ['./add-todo.component.css']
})
export class AddTodoComponent implements OnInit {
  addtodoform!:FormGroup
  fb!:FormBuilder
  todoserve!:TodoService
  @Output() addedtodo:EventEmitter<Todo>=new EventEmitter();
  constructor(fb:FormBuilder,todoserve:TodoService) {
    this.fb=fb;
    this.todoserve=todoserve;
   }

  ngOnInit(): void {
    this.addtodoform=this.fb.group({
      title:['',Validators.required],
      description:['',Validators.required]
    })
  }
  onAddTodo(){
    if(this.addtodoform.valid){
      this.todoserve.AddTodo(this.addtodoform.value).subscribe({
        next:(res)=>{
          this.addedtodo.emit(this.addtodoform.value);
        },
        error:(err)=>{
          alert(err.error);
          this.addtodoform.reset;
        }
      })
    }
    else{
      alert("There is an error in form");
    }
    
  }
  
}
