using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TodoListWebApplication.Models;

namespace TodoListWebApplication.Controllers
{
    public class TodoListTablesController : Controller
    {
        private TodoListDatabaseEntities db = new TodoListDatabaseEntities();

        // GET: TodoListTables
        public ActionResult Index()
        {
            bool allDone = true;
            foreach(var item in db.TodoListTable) {
                if(!item.Wykonane)
                    allDone = false;
            }
            if(db.TodoListTable.ToList().Count == 0)
                ViewBag.Message = "Brak zadań.";
            else if(allDone)
                ViewBag.Message = "Wszystkie zadania wykonane.";
            else
                ViewBag.Message = "Masz jeszcze coś do roboty.";


            return View(db.TodoListTable.ToList());
        }

        // GET: TodoListTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoListTable todoListTable = db.TodoListTable.Find(id);
            if (todoListTable == null)
            {
                return HttpNotFound();
            }
            return View(todoListTable);
        }

        // GET: TodoListTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoListTables/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Zadanie,Wykonane")] TodoListTable todoListTable)
        {
            if (ModelState.IsValid)
            {
                db.TodoListTable.Add(todoListTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(todoListTable);
        }

        // GET: TodoListTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoListTable todoListTable = db.TodoListTable.Find(id);
            if (todoListTable == null)
            {
                return HttpNotFound();
            }
            return View(todoListTable);
        }

        // POST: TodoListTables/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Zadanie,Wykonane")] TodoListTable todoListTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todoListTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todoListTable);
        }

        // GET: TodoListTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoListTable todoListTable = db.TodoListTable.Find(id);
            if (todoListTable == null)
            {
                return HttpNotFound();
            }
            return View(todoListTable);
        }

        // POST: TodoListTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TodoListTable todoListTable = db.TodoListTable.Find(id);
            db.TodoListTable.Remove(todoListTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
