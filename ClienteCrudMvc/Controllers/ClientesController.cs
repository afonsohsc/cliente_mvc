using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClienteCrudMvc.Models;

namespace ClienteCrudMvc.Controllers
{
    public class ClientesController : Controller
    {
        private ClienteContext db = new ClienteContext();

        // GET: Clientes
        public ActionResult Index()
        {
			return View(db.Clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes
								.Include(c => c.Telefones)
								.FirstOrDefault(c => c.Id == id);
			if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,CPF,DataNascimento,Genero,Telefones")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			Cliente cliente = db.Clientes.Find(id);
								//.Include(c => c.Telefones)
								//.FirstOrDefault(c => c.Id == id);
			if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,CPF,DataNascimento,Genero,Telefones")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {

				if (cliente.Telefones != null)
				{
					foreach (var telefone in cliente.Telefones)
					{
						telefone.IdCliente = cliente.Id;
						if (telefone.DDD != null || telefone.Numero != null)
						{
							if (telefone.Id > 0)
								db.Entry(telefone).State = EntityState.Modified;
							else
								db.Entry(telefone).State = EntityState.Added;
						}
					
					}
				}
				db.Entry(cliente).State = EntityState.Modified;
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes
								.Include(c => c.Telefones)
								.FirstOrDefault(c => c.Id == id);
			if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes
								.Include(c => c.Telefones)
								.FirstOrDefault(c => c.Id == id);
			db.Telefones.RemoveRange(cliente.Telefones);
			db.Clientes.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

		[HttpPost]
		public void RemoverTelefone(int id)
		{
			var telefone = db.Telefones.Find(id);

			db.Entry(telefone).State = EntityState.Deleted;

			db.SaveChanges();
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
