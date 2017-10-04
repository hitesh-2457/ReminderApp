import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/mergeMap';

import { ToDoList } from '../../Models/todolist';

@Injectable()
export class TodoApiService {
  baseUrl = 'http://localhost:5000/api/todolist';
  constructor(private http: Http) { }

  getAllToDoLists() {
    return this.http.get(this.baseUrl).map((res: Response) => res.json());
  }

  postToDoList(todoList: ToDoList, isEdit: boolean) {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    let body = JSON.stringify(todoList);

    event.preventDefault();

    if (isEdit) {
      return this.http.post(this.baseUrl + '/UpdateList/' + todoList.Id, body, options)
        .map((res: Response) => (res.status === 200));
    }
    return this.http.post(this.baseUrl, body, options)
      .map((res: Response) => (res.status === 200));

  }

  deleteToDoList(todoListId: number) {
    return this.http.delete(this.baseUrl + '/' + todoListId)
      .map((res: Response) => (res.status === 200));
  }
}
