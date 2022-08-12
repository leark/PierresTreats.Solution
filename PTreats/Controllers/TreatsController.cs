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
    private readonly TreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(TreatsContext db, UserManager<ApplicationUser> userManager)
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
      return View(treat);
    }

    public ActionResult Edit(int id)
    {
      var treat = _db.Treats.FirstOrDefault(t => t.TreatId == id);
      return View(treat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat)
    {
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details");
    }

    public ActionResult Delete(int id)
    {
      var treat = _db.Treats.FirstOrDefault(t => t.TreatId == id);
      return View(treat);
    }

    [HttpPost]
    public ActionResult Deleted(int id)
    {
      var treat = _db.Treats.FirstOrDefault(t => t.TreatId == id);
      _db.Treats.Remove(treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}