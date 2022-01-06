using Microsoft.AspNetCore.Mvc;


namespace ASP.Controllers;

public class Soc : Controller
{

    public IActionResult AjouterAuto()
    {
        if (HttpContext.Session.GetString("type") != "soc")
        {

            return RedirectToAction("Login", "User");
        }
        Societe? currentSoc = DataBase.getCtxDb().societes?.Where(s => s.login == HttpContext.Session.GetString("login"))?.FirstOrDefault();
        ViewBag.cars = DataBase.getCtxDb().autocars?.Where(v => v.owner == currentSoc).ToArray();
        return View();
    }

    [HttpPost]
    public IActionResult AjouterAuto(int x = 0)
    {
        string mod = Request.Form["model"];
        int cap = Int32.Parse(Request.Form["cap"]);
        bool cl = Request.Form["clim"] == "1" ? true : false;
        bool wf = Request.Form["wifi"] == "1" ? true : false;
        Societe? currentSoc = DataBase.getCtxDb().societes?.Where(s => s.login == HttpContext.Session.GetString("login"))?.FirstOrDefault();

        Autocar v = new Autocar(cl, wf, "", mod, cap, currentSoc!);

        DataBase.getCtxDb().Add(v);
        DataBase.getCtxDb().SaveChanges();

        return RedirectToAction("AjouterAuto", "Soc");
    }


    public IActionResult AjouterAbo()
    {

        if (HttpContext.Session.GetString("type") != "soc")
        {
            return RedirectToAction("Login", "User");
        }
        ViewBag.voitures = DataBase.getCtxDb().autocars?.Where(v => v.owner!.login == HttpContext.Session.GetString("login")).ToArray();

        ViewBag.cities = DataBase.getCtxDb().villes?.ToArray();

        return View();
    }





}