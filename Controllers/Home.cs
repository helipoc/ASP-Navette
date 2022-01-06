using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace ASP.Controllers;

public class Home : Controller
{

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("type") != null)
        {
            ViewBag.type = HttpContext.Session.GetString("type");
            ViewBag.name = HttpContext.Session.GetString("name");
        }
        return View();
    }






}