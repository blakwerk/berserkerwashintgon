using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using SignalRChat.Models;

namespace SignalRChat.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. 
    Merge these statements into the Register method of the WebApiConfig class as applicable. 
    Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using SignalRChat.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Confession>("ConfessionsOData");
    builder.EntitySet<Comment>("Comments"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ConfessionsODataController : ODataController
    {
        private CommentDbContext db = new CommentDbContext();

        // GET: odata/ConfessionsOData
        [EnableQuery]
        public IQueryable<Confession> GetConfessionsOData()
        {
            return db.Confessions;
        }

        // GET: odata/ConfessionsOData(5)
        [EnableQuery]
        public SingleResult<Confession> GetConfession([FromODataUri] int key)
        {
            return SingleResult.Create(db.Confessions.Where(confession => confession.Id == key));
        }

        #region Disabled Actions/Verbs

        //// PUT: odata/ConfessionsOData(5)
        //public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Confession> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Confession confession = await db.Confessions.FindAsync(key);
        //    if (confession == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(confession);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ConfessionExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(confession);
        //}

        //// POST: odata/ConfessionsOData
        //public async Task<IHttpActionResult> Post(Confession confession)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Confessions.Add(confession);
        //    await db.SaveChangesAsync();

        //    return Created(confession);
        //}

        //// PATCH: odata/ConfessionsOData(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Confession> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Confession confession = await db.Confessions.FindAsync(key);
        //    if (confession == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(confession);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ConfessionExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(confession);
        //}

        //// DELETE: odata/ConfessionsOData(5)
        //public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        //{
        //    Confession confession = await db.Confessions.FindAsync(key);
        //    if (confession == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Confessions.Remove(confession);
        //    await db.SaveChangesAsync();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}
        #endregion

        // GET: odata/ConfessionsOData(5)/Comments
        [EnableQuery]
        public IQueryable<Comment> GetComments([FromODataUri] int key)
        {
            return db.Confessions.Where(m => m.Id == key).SelectMany(m => m.Comments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConfessionExists(int key)
        {
            return db.Confessions.Count(e => e.Id == key) > 0;
        }
    }
}
