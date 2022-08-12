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
    private readonly TreatsContext _db;

    public HomeController(TreatsContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      ViewBag.PageTitle = "Home";
      return View();
    }
  }
}