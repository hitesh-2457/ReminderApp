using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ReminderApi.Repository;
using ReminderApi.Model;
using System.Net.Http;
using System.Net;
using System.Web;

namespace ReminderApi.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        // GET api/task
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new TaskRepository(new ReminderContext()).GetAll().ToList());
        }

        // GET api/task/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(new TaskRepository(new ReminderContext()).GetById(id));
        }

        // POST api/task
        [HttpPost]
        public ActionResult Post([FromBody]Task task)
        {
            using (var context = new ReminderContext())
            {
                var taskRepo = new TaskRepository(context);
                taskRepo.Add(task);
                context.SaveChanges();
            }
            return Ok();
        }

        // PUT api/task/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Task task)
        {
            using (ReminderContext context = new ReminderContext())
            {
                TaskRepository taskRepo = new TaskRepository(context);
                var result = taskRepo.Update(id, task);
                context.SaveChanges();
                if (result != null)
                    return Ok(result);
                return NoContent();
            }
        }

        // DELETE api/task/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            using (ReminderContext context = new ReminderContext())
            {
                TaskRepository taskRepo = new TaskRepository(context);
                var result = taskRepo.RemoveById(id);
                context.SaveChanges();
                if (result)
                    return Ok();
                return NoContent();
            }
        }

        [Route("GetByToDoList/{id}")]
        [HttpGet("{id}")]
        public ActionResult GetByToDoList(int id)
        {
            return Ok(new TaskRepository(new ReminderContext()).GetAllTasksByListId(id));
        }
    }
}
