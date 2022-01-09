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

        ViewBag.Success = TempData["Success"];

        string soc = HttpContext.Session.GetString("login")!;
        ViewBag.voitures = DataBase.getCtxDb().autocars?.Where(v => v.owner!.login == HttpContext.Session.GetString("login")).ToArray();
        ViewBag.cities = DataBase.getCtxDb().villes?.ToArray();
        ViewBag.abos = DataBase.getCtxDb().abonnements?.Where(a => a.soc!.login == soc).ToArray();

        return View();
    }

    [HttpPost]
    public IActionResult AjouterAbo(int x = 0)
    {




        DateTime date_debut = DateTime.Parse(Request.Form["d_debut"].ToString());
        DateTime date_fin = DateTime.Parse(Request.Form["d_fin"]);
        string h_depart = Request.Form["hd"];
        string h_arrv = Request.Form["ha"];
        string type = Request.Form["type"];
        decimal prix = decimal.Parse(Request.Form["prix"]);
        Ville villeDepart = DataBase.getCtxDb().villes!.Where(v => v.ID == Int32.Parse(Request.Form["villedp"])).First();
        Ville villeDarr = DataBase.getCtxDb().villes!.Where(v => v.ID == Int32.Parse(Request.Form["villeda"])).First();
        Autocar voiture = DataBase.getCtxDb().autocars!.Where(a => a.ID == Int32.Parse(Request.Form["autocar"])).First();
        Societe soc = DataBase.getCtxDb().societes!.Where(s => s.login == HttpContext.Session.GetString("login")).First();
        Abonnement ab = new Abonnement(date_debut, date_fin, h_depart, h_arrv, prix, type, villeDepart, villeDarr, voiture, soc);
        DataBase.getCtxDb().Add(ab);
        DataBase.getCtxDb().SaveChanges();
        TempData["Success"] = "Abonnement Ajouté avec success";
        return RedirectToAction("AjouterAbo", "Soc");
    }



}