using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ReminderApi;
using ReminderApi.Model;

namespace ReminderApi.Repository
{
    /// <summary>
    /// Repository to handle all operations on ToDoLists
    /// </summary>
    public class ToDoListRepository
    {
        private ReminderContext _ctx;
        private TaskRepository taskRepository;

        public ToDoListRepository(ReminderContext ctx)
        {
            _ctx = ctx;
            taskRepository = new TaskRepository(_ctx);
        }

        /// <summary>
        /// Get all the ToDoLists from DataBase
        /// </summary>
        /// <returns>all the ToDoLists as an IEnumerable of ToDoList</returns>
        public IEnumerable<ToDoList> GetAll()
        {
            return _ctx.ToDoLists.ToList();
        }

        /// <summary>
        /// Get ToDoList by Id
        /// </summary>
        /// <param name="id">Id of the ToDoList</param>
        /// <returns>the ToDoList object, can be null</returns>
        public ToDoList GetById(int id)
        {
            return _ctx.ToDoLists.FirstOrDefault(t => t.Id == id);
        }

        /// <summary>
        /// Get ToDoList by Name.
        /// </summary>
        /// <param name="name">Name of the ToDoList</param>
        /// <returns>the ToDoList object, can be null</returns>
        public ToDoList GetByName(string name)
        {
            return _ctx.ToDoLists.FirstOrDefault(t => t.Name == name);
        }

        /// <summary>
        /// Add a ToDoList
        /// </summary>
        /// <param name="name">Name of the ToDoList to be added</param>
        /// <returns>Id of the ToDoList added</returns>
        public ToDoList Add(string name)
        {
            var item = new ToDoList(name);
            _ctx.ToDoLists.Add(item);

            return item;
        }

        /// <summary>
        /// Add a ToDoList
        /// </summary>
        /// <param name="toDoList">List to be added</param>
        /// <returns>the ToDoList that was added</returns>
        public ToDoList Add(ToDoList toDoList)
        {
            _ctx.ToDoLists.Add(toDoList);
            return toDoList;
        }

        /// <summary>
        /// Remove a ToDoList by Id
        /// </summary>
        /// <param name="Id">Id of the ToDoList to be removed</param>
        /// <returns>Boolean value based on success</returns>
        public bool RemoveById(int Id)
        {
            var item = GetById(Id);
            if (item != null)
            {
                taskRepository.RemoveTasksByListId(item.Id);
                _ctx.ToDoLists.Remove(item);

                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove a ToDoList by name
        /// </summary>
        /// <param name="name">Name of the ToDoList to be removed</param>
        /// <returns>Boolean value based on success</returns>
        public bool RemoveByName(string name)
        {
            var item = GetByName(name);
            if (item != null)
            {
                taskRepository.RemoveTasksByListId(item.Id);
                _ctx.ToDoLists.Remove(item);

                return true;
            }
            return false;
        }

        /// <summary>
        /// Update a ToDoList by Id
        /// </summary>
        /// <param name="id">Id of the ToDoList to be updated</param>
        /// <param name="name">New Name of the ToDoList</param>
        /// <returns>Updated ToDoList object</returns>
        public ToDoList Update(int id, ToDoList toDoList)
        {
            var item = GetById(id);
            if (item != null)
            {
                item.Name = toDoList.Name;
                item.Tasks = toDoList.Tasks;

                return item;
            }
            return null;
        }
    }
}