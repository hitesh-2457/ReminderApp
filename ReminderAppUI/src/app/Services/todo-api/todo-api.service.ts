import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/map';

import { ToDoList } from '../../Models/todolist';

@Injectable()
export class TodoApiService {
  baseUrl = 'http://localhost:5000/api/todolist';
  constructor(private http: Http) { }

  getAllToDoLists() {
    return this.http.get(this.baseUrl).map((res: Response) => res.json());
  }

  postToDoList(todoList: ToDoList) {
    this.http.post(this.baseUrl + '').map
  }
}
