using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using PTreats.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace PTreats.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly PTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(PTreatsContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }
    public ActionResult Index()
    {
      ViewBag.PageTitle = "Flavors";
      var flavors = _db.Flavors.ToList();
      return View(flavors);
    }

    [Authorize]
    public ActionResult Create()
    {
      return View();
    }

    [Authorize]
    [HttpPost]
    public ActionResult Create(Flavor flavor)
    {
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var flavor = _db.Flavors.FirstOrDefault(f => f.FlavorId == id);
      return View(flavor);
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
      var flavor = _db.Flavors.FirstOrDefault(f => f.FlavorId == id);
      ViewBag.Treats = new SelectList(_db.Treats, "TreatId", "Name");
      return View(flavor);
    }

    [Authorize]
    [HttpPost]
    public ActionResult Edit(Flavor flavor)
    {
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = flavor.FlavorId });
    }

    [Authorize]
    public ActionResult Delete(int id)
    {
      var flavor = _db.Flavors.FirstOrDefault(f => f.FlavorId == id);
      return View(flavor);
    }

    [Authorize]
    [HttpPost, ActionName("Delete")]
    public ActionResult Deleted(int id)
    {
      var flavor = _db.Flavors.FirstOrDefault(f => f.FlavorId == id);
      _db.Flavors.Remove(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult AddTreat(int treatId, int flavorId)
    {
      if (_db.TreatFlavors.FirstOrDefault(tf => tf.TreatId == treatId && tf.FlavorId == flavorId) == null)
      {
        _db.TreatFlavors.Add(new TreatFlavor() { TreatId = treatId, FlavorId = flavorId });
        _db.SaveChanges();
      }
      return RedirectToAction("Edit", new { id = flavorId });
    }

    [Authorize]
    public ActionResult DeleteTreat(int flavorId, int treatflavorId)
    {
      var treatflavor = _db.TreatFlavors.FirstOrDefault(tf => tf.TreatFlavorId == treatflavorId);
      _db.TreatFlavors.Remove(treatflavor);
      _db.SaveChanges();
      return RedirectToAction("Edit", new { id = flavorId });
    }
  }
}