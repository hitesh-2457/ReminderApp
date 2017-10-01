using System;
using System.Collections;
using System.Collections.Generic;

namespace ReminderApi.Model
{
    public class ToDoList : BaseEntity
    {

        public ToDoList()
        {

        }

        public ToDoList(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}