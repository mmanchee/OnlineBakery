using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineBakery.Models;
using OnlineBakery.ViewModels;


namespace OnlineBakery.Controllers
{
  [Authorize]
  public class FlavorsController : Controller
  {
    private readonly OnlineBakeryContext _db;
    public FlavorsController(OnlineBakeryContext db)
    {
      _db = db;
    }
    // Index *****************
    public ActionResult Index()
    {
      List<Flavor> model = _db.Flavors.OrderBy(x => x.Name).ToList();
      return View(model);
    }
    // Create ****************
    public ActionResult Create()
    {
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      return View();
    }
    [HttpPost]
    public ActionResult Create(Flavor flavor, int TreatId)
    {
      _db.Flavors.Add(flavor);
      if (TreatId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    // Details **************
    public ActionResult Details(int id)
    {
      Flavor model = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(model);
    }
    // Delete ******************
    public ActionResult Delete(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(x => x.FlavorId == id);
      return View(thisFlavor);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(x => x.FlavorId == id);
      _db.Flavors.Remove(thisFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    // Edit **************
    public ActionResult Edit(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavors => flavors.FlavorId == id);
      var thisFlavorTreat = _db.FlavorTreat.FirstOrDefault(x => x.FlavorId == thisFlavor.FlavorId);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name", thisFlavorTreat.TreatId);
      return View(thisFlavor);
    }
    [HttpPost]
    public ActionResult Edit(Flavor flavor, int TreatId)
    {
      if (TreatId != 0)
      {
        bool tf = _db.FlavorTreat.Any(x => x.FlavorId == flavor.FlavorId && x.TreatId == TreatId);
        if (!tf)
        {
          _db.FlavorTreat.Add(new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorId });
        }
      }
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}