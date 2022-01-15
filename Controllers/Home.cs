using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace ASP.Controllers;

public class Home : Controller
{

    public IActionResult Index()
    {
        string? type = HttpContext.Session.GetString("type");

        switch (type)
        {
            case "user":
                {
                    return RedirectToAction("Utilisateur", "Home");
                }
            case "soc":
                {
                    return RedirectToAction("Societe", "Home");
                }
        }


        return View();
    }


    public IActionResult Societe()
    {
        return View();
    }





    public IActionResult Utilisateur()
    {
        if (HttpContext.Session.GetInt32("logged") != 1)
        {
            return RedirectToAction("Index", "Home");
        }

        string? lo = HttpContext.Session.GetString("login");
        ViewBag.cities = DataBase.getCtxDb().villes?.ToArray();
        DataBase.getCtxDb().autocars?.ToArray();
        Utilisateur u = DataBase.getCtxDb().utilisateurs!.Where(x => x.login == lo).First();

        DataBase.getCtxDb().abonnements?.ToArray();
        ViewBag.abos = DataBase.getCtxDb().abonnements?
        .Where(a => !u.adhere.Contains(a))
        .ToArray();
        ViewBag.Success = TempData["Success"];
        return View();
    }

    [HttpPost]
    public IActionResult Utilisateur(int x = 0)
    {
        if (HttpContext.Session.GetInt32("logged") != 1)
        {
            return RedirectToAction("Index", "Home");
        }

        string? lo = HttpContext.Session.GetString("login");
        Utilisateur u = DataBase.getCtxDb().utilisateurs!.Where(x => x.login == lo).First();
        string vd = Request.Form["villedp"];
        string va = Request.Form["villeda"];

        ViewBag.cities = DataBase.getCtxDb().villes?.ToArray();
        ViewBag.abos = DataBase.getCtxDb().abonnements?
        .Where(a => !u.adhere.Contains(a))
        .Where(a => a.villeDep!.nom == vd && a.villeDar!.nom == va)
        .ToArray();
        ViewBag.Success = TempData["Success"];
        return View();
    }





}