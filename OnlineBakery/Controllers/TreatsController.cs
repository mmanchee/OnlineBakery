using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineBakery.Models;


namespace OnlineBakery.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly OnlineBakeryContext _db;
    public TreatsController(OnlineBakeryContext db)
    {
      _db = db;
    }
    // Index *****************
    public ActionResult Index()
    {
      List<Treat> model = _db.Treats.OrderBy(treat => treat.Name).ToList();
      return View(model);
    }
    // Create ****************
    public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View();
    }
    [HttpPost]
    public ActionResult Create(Treat treat, int FlavorId)
    {
      _db.Treats.Add(treat);
      if (FlavorId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { TreatId = treat.TreatId, FlavorId = FlavorId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    // Details **************
    public ActionResult Details(int id)
    {
      Treat model = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(model);
    }
    // Delete ******************
    public ActionResult Delete(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    // Edit **************
    public ActionResult Edit(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
      var thisFlavorTreat = _db.FlavorTreat.FirstOrDefault(x => x.TreatId == thisTreat.TreatId);
      if(_db.Flavors.Any())
      {
        ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name", thisFlavorTreat.FlavorId);
      }
      return View(thisTreat);
    }
    [HttpPost]
    public ActionResult Edit(Treat treat, int FlavorId)
    {
      if (FlavorId != 0)
      {
        bool tf = _db.FlavorTreat.Any(x => x.FlavorId == FlavorId && x.TreatId == treat.TreatId);
        if (!tf)
        {
          _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
        }
      }
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}