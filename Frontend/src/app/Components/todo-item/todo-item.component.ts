import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Todo } from '../../Models/Todo';

@Component({
  selector: 'app-todo-item',
  templateUrl: './todo-item.component.html',
  styleUrls: ['./todo-item.component.css']
})
export class TodoItemComponent implements OnInit {
  isEditing=false;
  editdesc='';
  @Input() todo!:Todo;
  @Output() DeleteTodo:EventEmitter<Todo>=new EventEmitter()
  @Output() EditTodo:EventEmitter<Todo>=new EventEmitter()
  constructor() { }

  ngOnInit(): void {
  }
  delete(todo:Todo){
    console.log("event is triggrerd");
    this.DeleteTodo.emit(todo);
  }
  edit(){
    this.isEditing=true;
  }
  save(todos:Todo,description:string){
    todos.description=description;
    this.isEditing=false;
    this.EditTodo.emit(todos);
  }
}
