// todo.component.ts
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { RowInsertingEvent, RowRemovingEvent, RowUpdatingEvent } from 'devextreme/ui/data_grid';
import { Todo } from 'src/app/Models/Todo';
import { TodoService } from 'src/app/Services/todo.service';


@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css'],
})
export class TodoComponent implements OnInit {
onUpdating($event: RowUpdatingEvent) {
var oldTodo = this.todoList.find(x =>x.title==$event.oldData.title)||this.todoList.find(x =>x.description==$event.oldData.description);

if($event.newData.title!=null){
  oldTodo!.title = $event.newData.title
}
if($event.newData.description!=null){
  oldTodo!.description = $event.newData.description
}
this.todoList = this.todoList.filter(x=>x.title != $event.oldData.title)
this.todoList.push(oldTodo!)
this.editTodo(oldTodo!)
}
onDeleting($event: RowRemovingEvent) {
  var todo = this.todoList.find(x=>x.title == $event.data.title)
  this.todoList = this.todoList.filter(x=>x.title != $event.data.title)
 this.deltodo(todo!)
}
onAdding($event: RowInsertingEvent) {
var todo:Todo ={
  title : $event.data.title,
  description : $event.data.description
}
this.addtodo(todo) 
}
  todoList!: Todo[];
  constructor(private todo: TodoService) { }
  addtodo(todo:Todo){
    this.todoList.push(todo);  
  }
  deltodo(todos:Todo){
    this.todo.DeleteTodo(todos.title).subscribe({
      next:(res)=>{
        var idx=this.todoList.indexOf(todos);
        this.todoList.splice(idx,1);
        alert("todo has been successfully deleted")
      },
      error:(err)=>{
        console.log(err.error);
        alert("something went wrong")
      }
    })
  }
  editTodo(todos:Todo){
    this.todo.EditTodo(todos.title,todos.description).subscribe({
      next:(res)=>{
        this.todo.GetTodo().subscribe({
          next: (res) => {
            this.todoList = res;
          },
          error: (err) => {
            console.error(err);
            alert('Something went wrong');
          },
        });
        alert("edit triggered")
      },
      error:(err)=>{
        alert("something went wronmg")
      }
    })
  }
  ngOnInit(): void {
    console.log("Init...");
    this.todo.GetTodo().subscribe({
      next: (res) => {
        this.todoList = res;
        console.log(res);
      },
      error: (err) => {
        console.error(err);
        alert('Something went wrong');
      },
    });

}
  
}
