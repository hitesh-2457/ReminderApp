import { ToDoList } from './todolist';

export class Task {
  Id: number;
  Name: string;
  ToDoListId: number;
  Note: string;
  DueDate: Date;
  RemindDate: Date;
  IsCompleted: boolean;
  ToDoList: ToDoList;
  DateCreated: Date;
  DateModified: Date;

  public populate(res: any, todo: ToDoList) {
    this.Id = res.id;
    this.Name = res.name;
    this.ToDoListId = res.toDoListId;
    this.Note = res.note;
    this.DueDate = res.dueDate;
    this.RemindDate = res.remindDate;
    this.IsCompleted = res.isCompleted;
    this.DateCreated = res.dateCreated;
    this.DateModified = res.dateModified;
    return this;
  }
}
