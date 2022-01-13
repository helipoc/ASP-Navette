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
        /*if (HttpContext.Session.GetInt32("logged") != 1)
        {
            return RedirectToAction("Index", "Home");
        }
*/
        int? cU = HttpContext.Session.GetInt32("id");
        DataBase.getCtxDb().villes?.ToArray();
        DataBase.getCtxDb().utilisateurs?.ToArray();
        ViewBag.abos = DataBase.getCtxDb().abonnements?.ToArray();
        Console.WriteLine(ViewBag.abos[0]);
        ViewBag.Success = TempData["Success"];
        return View();
    }





}