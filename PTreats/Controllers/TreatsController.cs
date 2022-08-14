using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using PTreats.Models;

namespace PTreats.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly PTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(PTreatsContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      ViewBag.PageTitle = "Treats";
      var treats = _db.Treats.ToList();
      return View(treats);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat treat)
    {
      _db.Treats.Add(treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      var treat = _db.Treats.FirstOrDefault(t => t.TreatId == id);
      ViewBag.Flavors = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View(treat);
    }

    public ActionResult Edit(int id)
    {
      var treat = _db.Treats.FirstOrDefault(t => t.TreatId == id);
      ViewBag.Flavors = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View(treat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat)
    {
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = treat.TreatId });
    }

    public ActionResult Delete(int id)
    {
      var treat = _db.Treats.FirstOrDefault(t => t.TreatId == id);
      return View(treat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult Deleted(int id)
    {
      var treat = _db.Treats.FirstOrDefault(t => t.TreatId == id);
      _db.Treats.Remove(treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddFlavor(int treatId, int flavorId)
    {
      if (_db.TreatFlavors.FirstOrDefault(tf => tf.TreatId == treatId && tf.FlavorId == flavorId) == null)
      {
        _db.TreatFlavors.Add(new TreatFlavor() { TreatId = treatId, FlavorId = flavorId });
        _db.SaveChanges();
      }
      return RedirectToAction("Edit", new { id = treatId });
    }

    public ActionResult DeleteFlavor(int treatId, int treatflavorId)
    {
      var treatflavor = _db.TreatFlavors.FirstOrDefault(tf => tf.TreatFlavorId == treatflavorId);
      _db.TreatFlavors.Remove(treatflavor);
      _db.SaveChanges();
      return RedirectToAction("Edit", new { id = treatId });
    }
  }
}