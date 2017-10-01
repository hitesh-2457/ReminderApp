using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ReminderApi;
using ReminderApi.Model;
using System;

namespace ReminderApi.Repository
{
    /// <summary>
    /// Repository to handle all operations on Tasks
    /// </summary>
    public class TaskRepository
    {
        private ReminderContext _ctx;
        public TaskRepository(ReminderContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Get all the Tasks from DataBase
        /// </summary>
        /// <returns>all the Tasks as an IEnumerable of Task</returns>
        public IEnumerable<Task> GetAll()
        {
            return _ctx.Tasks.AsEnumerable();
        }

        /// <summary>
        /// Get Task by Id
        /// </summary>
        /// <param name="id">Id of the Task</param>
        /// <returns>the Task object, can be null</returns>
        public Task GetById(int id)
        {
            return _ctx.Tasks.FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Get Task by Name.
        /// </summary>
        /// <param name="name">Name of the Task</param>
        /// <returns>the Task object, can be null</returns>
        public Task GetByName(string name)
        {
            return _ctx.Tasks.FirstOrDefault(t => t.Name == name);
        }

        /// <summary>
        /// Get all tasks by ToDoList's Id.
        /// </summary>
        /// <param name="toDoListId">Id of the ToDoList</param>
        /// <returns>all the Tasks of the ToDoList as an IEnumerable of Task</returns>
        public IEnumerable<Task> GetAllTasksByListId(int toDoListId)
        {
            return _ctx.Tasks.Where(t => t.ToDoListId == toDoListId);
        }

        /// <summary>
        /// Add a Task with its details
        /// </summary>
        /// <param name="toDoListId">Id of the List it belongs to</param>
        /// <param name="name">Name of the Task</param>
        /// <param name="note">Task Note</param>
        /// <param name="dueDate">Due DateTime for the Task</param>
        /// <param name="remindDate">DateTime to be Reminded at</param>
        /// <param name="isCompleted">Is the task completed</param>
        /// <returns>Task object added</returns>
        public Task Add(int toDoListId, string name, string note, DateTime? dueDate, DateTime? remindDate, bool isCompleted)
        {
            var item = new Task(toDoListId, name, note, dueDate, remindDate, isCompleted);
            _ctx.Tasks.Add(item);

            return item;
        }

        /// <summary>
        /// Add a task object
        /// </summary>
        /// <param name="task">Task object to be added</param>
        /// <returns>Task object that was added</returns>
        public Task Add(Task task)
        {
            _ctx.Tasks.Add(task);
            return task;
        }

        /// <summary>
        /// Remove a Task by Id
        /// </summary>
        /// <param name="Id">Id of the Task to be removed</param>
        /// <returns>Boolean value based on success</returns>
        public bool RemoveById(int Id)
        {
            var item = GetById(Id);
            if (item != null)
            {
                _ctx.Tasks.Remove(item);

                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove a Task by name
        /// </summary>
        /// <param name="name">Name of the Task to be removed</param>
        /// <returns>Boolean value based on success</returns>
        public bool RemoveByName(string name)
        {
            var item = GetByName(name);
            if (item != null)
            {
                _ctx.Tasks.Remove(item);

                return true;
            }
            return false;
        }

        /// <summary>
        /// Update a Task by Id
        /// </summary>
        /// <param name="id">Id of the Task to be updated</param>
        /// <param name="name">New Name for the Task</param>
        /// <param name="note">Updated Task Note</param>
        /// <param name="dueDate">Updated DueDate</param>
        /// <param name="remindDate">Updated Reminder Date</param>
        /// <param name="isCompleted">Updated status</param>
        /// <returns>Updated Task object</returns>
        public Task Update(int id, string name, string note, DateTime? dueDate, DateTime? remindDate, bool isCompleted)
        {
            var item = GetById(id);
            if (item != null)
            {
                item.Name = name;
                item.Note = note;
                item.DueDate = dueDate;
                item.RemindDate = remindDate;
                item.IsCompleted = isCompleted;

                return item;
            }
            return null;
        }

        public Task Update(int id, Task task)
        {
            var item = GetById(id);
            if (item != null)
            {
                item.Name = task.Name;
                item.Note = task.Note;
                item.RemindDate = task.RemindDate;
                item.DueDate = task.DueDate;
                item.IsCompleted = task.IsCompleted;

                return item;
            }
            return null;
        }

        /// <summary>
        /// Mark a task complete
        /// </summary>
        /// <param name="id">Id of the task to be marked complete</param>
        public void MarkComplete(int id)
        {
            var item = GetById(id);
            if (item != null)
            {
                item.IsCompleted = true;
            }
        }

        /// <summary>
        /// Remove a Task
        /// </summary>
        /// <param name="task">Task object to be removed</param>
        public void RemoveTask(Task task)
        {
            _ctx.Tasks.Remove(task);
        }

        /// <summary>
        /// Remove all the Tasks Related to a ToDoList
        /// </summary>
        /// <param name="toDoListId">Id of the List whose tasks are to be removed</param>
        public void RemoveTasksByListId(int toDoListId)
        {
            var tasks = _ctx.Tasks.Where(t => t.ToDoListId == toDoListId).ToList();
            foreach (var task in tasks)
            {
                RemoveTask(task);
            }
        }
    }
}