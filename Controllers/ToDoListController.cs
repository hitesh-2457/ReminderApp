using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ReminderApi.Repository;
using ReminderApi.Model;
using System.Net.Http;
using System.Net;

namespace ReminderApi.Controllers
{
    [Route("api/[controller]")]
    public class ToDoListController : Controller
    {
        // GET api/todolist
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new ToDoListRepository(new ReminderContext()).GetAll().ToList());
        }

        // GET api/ToDoList/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(new ToDoListRepository(new ReminderContext()).GetById(id));
        }

        // POST api/ToDoList
        [HttpPost]
        public ActionResult Post([FromBody]ToDoList toDoList)
        {
            using(var context = new ReminderContext()){
                var toDoListRepo = new ToDoListRepository(context);
                toDoListRepo.Add(toDoList.Name);
                context.SaveChanges();
            }
            return Ok();
        }

        // PUT api/todolist/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]ToDoList toDoList)
        {
            using (ReminderContext context = new ReminderContext())
            {
                ToDoListRepository toDoListRepo = new ToDoListRepository(context);
                var result = toDoListRepo.Update(id,toDoList);
                context.SaveChanges();
                if(result!=null)
                    return Ok(result);
                return NoContent();
            }
        }

        // DELETE api/todolist/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (ReminderContext context = new ReminderContext())
            {
                ToDoListRepository toDoListRepo = new ToDoListRepository(context);
                var result = toDoListRepo.RemoveById(id);
                context.SaveChanges();
                if(result)
                    return Ok(result);
                return NoContent();
            }
        }
    }
}
