using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PTreats.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace PTreats.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly TreatsContext _db;

    public FlavorsController(TreatsContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      ViewBag.PageTitle = "Flavors";
      return View();
    }

    public ActionResult Create()
    {
      return View();
    }

    public ActionResult Details()
    {
      return View();
    }

    public ActionResult Edit()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor)
    {
      return RedirectToAction("Details");
    }

    public ActionResult Delete()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Delete(Flavor flavor)
    {
      return RedirectToAction("Index");
    }
  }
}