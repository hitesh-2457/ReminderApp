using System;
using System.Collections;
using System.Collections.Generic;

namespace ReminderApi.Model
{
    public class Task : BaseEntity
    {
        public Task()
        {

        }

        public Task(int toDoListId, string name, string note, DateTime? dueDate, DateTime? remindDate, bool isCompleted)
        {
            ToDoListId = toDoListId;
            Name = name;
            Note = note;
            DueDate = dueDate;
            RemindDate = remindDate;
            IsCompleted = isCompleted;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ToDoListId { get; set; }
        public string Note { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? RemindDate { get; set; }
        public bool IsCompleted { get; set; }
        public ToDoList ToDoList { get; set; }
    }
}