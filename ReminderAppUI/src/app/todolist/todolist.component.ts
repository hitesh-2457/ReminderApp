import { Component, OnInit } from '@angular/core';
import { CalendarModule } from 'primeng/primeng';

import { TodoApiService } from '../Services/todo-api/todo-api.service';
import { TaskApiService } from '../Services/task-api/task-api.service';

import { ToDoList } from '../Models/todolist';
import { Task } from '../Models/task';

@Component({
  selector: 'app-todolist',
  templateUrl: './todolist.component.html',
  styleUrls: ['./todolist.component.css']
})
export class TodolistComponent implements OnInit {

  public todoLists: ToDoList[];
  public showAddForm = false;
  public showTaskForm = false;
  newToDo: ToDoList;
  newTask: Task;
  newTasks: Task[];
  constructor(private todoApi: TodoApiService, private taskApi: TaskApiService) {
    this.newToDo = new ToDoList();
    this.newTask = new Task();
    this.newTasks = [];
  }

  ngOnInit() {
    this.todoLists = [];
    this.todoApi.getAllToDoLists().subscribe((res) => {
      res.forEach(element => {
        this.todoLists.push(new ToDoList().populate(element));
      });
      this.todoLists.forEach(todo => {
        this.taskApi.getAllTasks(todo.Id).subscribe((resp) => {
          if (resp != null) {
            resp.forEach(task => {
              todo.Tasks.push(new Task().populate(task, todo));
            });
          }
        });
      });
      // console.log(this.todoLists);
      // console.log(this.todoLists[0].Name);
    });
  }

  submitForm() {
    this.showAddForm = false;
    this.showTaskForm = false;

    this.todoApi.postToDoList(this.newToDo);
  }

  clearForm() {
    this.showAddForm = false;
    this.showTaskForm = false;

    this.newToDo = new ToDoList();
    this.newTask = new Task();
    this.newTasks = [];
  }

  submitTaskForm() {
    this.showTaskForm = false;

    this.newTasks.push(this.newTask);
    this.newTask = new Task();
  }

  clearTaskForm() {
    this.showTaskForm = false;

    this.newTask = new Task();
  }

  showTaskFormFunc() {
    this.showTaskForm = true;
    this.newTask.DueDate = new Date((new Date).toDateString());
    this.newTask.RemindDate = new Date((new Date).toDateString());
  }
}
