import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  isEdit = false;
  originalToDo: ToDoList;
  errorMessages: string;

  constructor(private todoApi: TodoApiService, private taskApi: TaskApiService, private router: Router) {
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
    });
  }

  showAddFormFunc() {
    this.showAddForm = !this.showAddForm;
  }
  submitForm() {
    this.showAddForm = false;
    this.showTaskForm = false;

    this.newToDo.Tasks = this.newTasks;
    this.todoApi.postToDoList(this.newToDo, this.isEdit).subscribe(
      (data) => {
        window.location.reload();
        this.errorMessages = null;
      },

      (error) => {
        this.errorMessages = 'Failed to update To-Do List. Try again Later.';
      }
    );
  }

  clearForm() {
    this.showTaskForm = false;
    this.showAddForm = false;

    this.newToDo = new ToDoList();
    this.newTask = new Task();
    this.newTasks = [];
    if (this.isEdit) {
      this.todoLists.push(this.originalToDo);
      this.isEdit = false;
    }
  }

  showTaskFormFunc() {
    this.showTaskForm = true;
    this.newTask.ToDoListId = this.newToDo.Id;
    this.newTask.DueDate = new Date((new Date).toDateString());
    this.newTask.RemindDate = new Date((new Date).toDateString());
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

  deleteTaskFormEle(task: Task) {
    this.newTasks.splice(this.newTasks.findIndex(t => t === task), 1);
  }

  editTaskFormEle(task: Task) {
    this.showTaskForm = true;
    this.newTasks.splice(this.newTasks.findIndex(t => t === task), 1);
    this.newTask = task;
  }

  deleteTask(taskId: number, todoList: ToDoList) {
    this.taskApi.deleteTask(taskId).subscribe(
      (data) => {
        todoList.Tasks.splice(todoList.Tasks.findIndex(t => t.Id === taskId), 1);
        this.errorMessages = null;
      },
      (error) => {
        this.errorMessages = 'Failed to delete the task. Try again Later.';
      }
    );
  }

  editToDoList(todoList: ToDoList) {
    this.todoLists.splice(this.todoLists.findIndex(t => t === todoList), 1);

    this.isEdit = true;
    this.showAddForm = true;
    this.originalToDo = Object.assign({}, todoList);
    this.newToDo = todoList;
    this.newTasks = todoList.Tasks;
    window.scrollTo(0, 0);
  }

  deleteToDoList(todoListId: number) {
    this.todoApi.deleteToDoList(todoListId).subscribe(
      (data) => {
        this.todoLists.splice(this.todoLists.findIndex(t => t.Id === todoListId), 1);
        this.errorMessages = null;
      },
      (error) => {
        this.errorMessages = 'Failed to delete the To-Do List. Try again Later.';
      }
    );
  }

  markTaskComplete(task: Task) {
    this.taskApi.markTaskComplete(task.Id).subscribe(
      (data) => {
        this.errorMessages = null;
      },
      (error) => {
        task.IsCompleted = false;
        this.errorMessages = 'Failed to mark the task complete. Try again later.';
      }
    );
  }
}
