using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SignalRChat.Models;
using SignalRChat.QueryEngine;

namespace SignalRChat.Controllers
{
    public class ConfessionsController : Controller
    {
        private ConfessionDbContext db = new ConfessionDbContext();

        // GET: Confessions
        public async Task<ActionResult> Index(int? id)
        {
            int x = 5;
            if (id == null)
            {
                id = 0;
            }

            var q = new Queryer();
            var confessions = await q.NextXConfessions(x, (int)id);
            var confessionsKvp = new KeyValuePair<int, IEnumerable<Confession>>((int)id+x, confessions); 
            return View(confessionsKvp);
        }

        // GET: Confessions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Confession confession = await db.Confessions.FindAsync(id);
            if (confession == null)
            {
                return HttpNotFound();
            }
            return View(confession);
        }

        public async Task<ActionResult> All()
        {
            var q = new Queryer();
            var confessions = await q.GetAllConfessions();
            return View(confessions);
        }

        public async Task<ActionResult> TopRated()
        {
            return View();
        }

        #region Create Actions

        // GET: Confessions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Confessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TheConfession,Submitter")] Confession confession)
        {
            try
            {
                confession.Rank = 0;
                confession.Comments = null;

                if (ModelState.IsValid)
                {
                    await InnerCreate(confession);
                }
            }
            catch (DataException dex )
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", new DataException("Unable to save changes.  Try again, and if the problem persists, please contact us.", dex));
            }

            return Redirect("Index");
        }

        // This method will serve as a way to post confessions internally, such as from SignalR
        private async Task<ActionResult> InnerCreate(Confession confession)
        {
            try
            {
                db.Confessions.Add(confession);
                await db.SaveChangesAsync();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, e.Message);
            }
            
        }

        #endregion

        #region Edit Actions

        // GET: Confessions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Confession confession = await db.Confessions.FindAsync(id);
            if (confession == null)
            {
                return HttpNotFound();
            }
            return View(confession);
        }

        // POST: Confessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TheConfession,Submitter,Rank")] Confession confession)
        {
            if (ModelState.IsValid)
            {
                db.Entry(confession).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(confession);
        }

        #endregion

        #region Delete actions

        public async Task<ActionResult> DevDelete()
        {
            return View(await db.Confessions.ToListAsync());
        }

        // GET: Confessions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            return new HttpStatusCodeResult(HttpStatusCode.MethodNotAllowed);

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Confession confession = await db.Confessions.FindAsync(id);
            //if (confession == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(confession);
        }

        private async Task<ActionResult> InnerDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Confession confession = await db.Confessions.FindAsync(id);
            if (confession == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("DevDelete");
        }

        // POST: Confessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            return new HttpStatusCodeResult(HttpStatusCode.MethodNotAllowed);

            //Confession confession = await db.Confessions.FindAsync(id);
            //db.Confessions.Remove(confession);
            //await db.SaveChangesAsync();
            //return RedirectToAction("Index");
        }


        // To delete data, make this method public and then browse to ../Confessions/DevDelete
        //  then make the method private again so this cannot be done on the live site.
        private async Task<ActionResult> InnerDeleteConfirmed(int id)
        {
            Confession confession = await db.Confessions.FindAsync(id);
            db.Confessions.Remove(confession);
            await db.SaveChangesAsync();
            return RedirectToAction("DevDelete");
        }

        #endregion

        #region Dispose method

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
