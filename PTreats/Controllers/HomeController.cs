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
  public class HomeController : Controller
  {
    private readonly PTreatsContext _db;

    public HomeController(PTreatsContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      ViewBag.PageTitle = "Home";
      return View();
    }

    public ActionResult ShowAll()
    {
      var treats = _db.Treats.ToList();
      return View(treats);
    }
  }
}