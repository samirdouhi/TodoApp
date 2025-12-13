using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TodoApp.Filters;
using TodoApp.Mappers;
using TodoApp.Models;
using TodoApp.Services;
using TodoApp.ViewModels;

namespace TodoApp.Controllers
{
    [LogFilter]
    [AuthFilter]
    public class TodoController : Controller
    {
        ISessionManagerService session;//INJECTION DE DEPENDANCE

        public IActionResult Index()
        {
            List<Todo> list = session.Get<List<Todo>>("Todos", HttpContext);
            ViewBag.count = list.Count;
            return View(list);
        }

        public TodoController(ISessionManagerService session)
        {
            this.session = session;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(TodoAddVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            //traitement 

            List<Todo> list;
            if (HttpContext.Session.GetString("Todos") == null)//pas d'objet todo
            {
                list = new List<Todo>();//on cree un 
            }
            else //objet todo existant et enregistrer dans la session sous forme de chaine de caractere donc on dois le deserialiser et le tranformer en un objet pour permettre a la list de faire son travail 
            {
                list = session.Get<List<Todo>>("Todos", HttpContext);
            }

            Todo todo = TodoMapper.GetTodosFromTodoAddVm(vm);
            list.Add(todo);

            session.Add("Todos", list, HttpContext);
            //on a enregistrer une chaine de caractere dans la session et pour lire une autre fois la list des todos on dois faire le contraire 
            //cet fonction accepte deux paramettre en chaine de caractere 

            return RedirectToAction(nameof(Index));
        }

        
        // EDIT (GET)
      
        public IActionResult Edit(int index)
        {
            List<Todo> list = session.Get<List<Todo>>("Todos", HttpContext);

            if (index < 0 || index >= list.Count)
            {
                return NotFound();
            }

            ViewBag.index = index;
            return View(list[index]);
        }

       
        // EDIT (POST)
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int index, Todo todo)
        {
            List<Todo> list = session.Get<List<Todo>>("Todos", HttpContext);

            if (index < 0 || index >= list.Count)
            {
                return NotFound();
            }

            list[index] = todo;
            session.Add("Todos", list, HttpContext);

            return RedirectToAction(nameof(Index));
        }

       
        //  DELETE (GET)
       
        public IActionResult Delete(int index)
        {
            List<Todo> list = session.Get<List<Todo>>("Todos", HttpContext);

            if (index < 0 || index >= list.Count)
            {
                return NotFound();
            }

            ViewBag.index = index;
            return View(list[index]);
        }

       
        //  DELETE (POST)
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int index)
        {
            List<Todo> list = session.Get<List<Todo>>("Todos", HttpContext);

            if (index < 0 || index >= list.Count)
            {
                return NotFound();
            }

            list.RemoveAt(index);
            session.Add("Todos", list, HttpContext);

            return RedirectToAction(nameof(Index));
        }
    }
}


