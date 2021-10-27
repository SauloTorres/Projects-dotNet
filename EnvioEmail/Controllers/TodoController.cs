using EnvioEmail.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net;

namespace EnvioEmail.Controllers
{
    public class TodoController : Controller
    {
        // GET: Todo
        public ActionResult Index()
        {
            IEnumerable<TodoViewModel> todos = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349/v1/todos");

                //HTTP GET
                var responseTask = client.GetAsync("todos");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TodoViewModel>>();
                    readTask.Wait();
                    todos = readTask.Result;
                }
                else
                {
                    todos = Enumerable.Empty<TodoViewModel>();
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }
                return View(todos);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TodoViewModel todo)
        {
            if (todo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349/v1/todos");
                //HTTP POST
                var postTask = client.PostAsJsonAsync<TodoViewModel>("todos", todo);
                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Erro no Servidor. Contacte o Administrador.");
            return View(todo);
        }

        //[HttpGet]
        //public ActionResult Swap(int id)
        //{

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://localhost:44349/v1/todos/"+ id);
        //        //HTTP POST
        //        var putTask = result.Content.ReadAsAsync<IList<TodoViewModel>>();
        //        putTask.Wait();
        //        var result = putTask.Result;

        //        if (result.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    ModelState.AddModelError(string.Empty, "Erro no Servidor. Contacte o Administrador.");
        //    return View("index");
        //}
    }
}

