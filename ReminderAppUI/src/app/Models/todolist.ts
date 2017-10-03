import { Task } from './task';

export class ToDoList {
  Id: number;
  Name: string;
  Tasks: Task[];
  DateCreated: Date;
  DateModified: Date;

  public populate(res: any) {
    this.Id = res.id;
    this.Name = res.name;
    this.Tasks = [];
    if (res.tasks != null) {
      res.tasks.forEach(element => {
        this.Tasks.push(new Task().populate(element, this));
      });
    }
    this.DateCreated = res.dateCreated;
    this.DateModified = res.dateModified;
    return this;
  }
}
