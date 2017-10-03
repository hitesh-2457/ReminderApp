import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/map';

import { Task } from '../../Models/task';

@Injectable()
export class TaskApiService {
  baseUrl = 'http://localhost:5000/api/task';
  constructor(private http: Http) { }

  getAllTasks(todoListId: number) {
    return this.http.get(this.baseUrl + '/GetByToDoList/' + todoListId).map((res: Response) => res.json());
  }
}
