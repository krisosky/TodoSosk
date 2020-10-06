using Dapper;
using Todotest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Todotest.Controllers
{
    public class TodoController : Controller
    {
        // GET: Todo
        public ActionResult Index()
        {

            return View(TodoORM.ReturnList<Todo>("TodoViewAll"));
        }

        [HttpGet]
        public ActionResult AddorEdit(int id =0)
        {
            if (id == 0)
                return View();
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TodoId", id);
                return View(TodoORM.ReturnList<Todo>("TodoViewByID", param).FirstOrDefault<Todo>());
            }

        }
        [HttpPost]
        public ActionResult AddorEdit(Todo td)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@TodoId", td.TodoId);
            param.Add("@Description", td.Description);
            TodoORM.ExecutedWithoutReturn("TodoAddorEdit", param);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {

            DynamicParameters param = new DynamicParameters();
            param.Add("@TodoId", id);

            TodoORM.ExecutedWithoutReturn("TodoDeleteByID", param);

            return RedirectToAction("Index");

        }
    }
}