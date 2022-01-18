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

        Autocar v = new Autocar(cl, wf, mod, cap, currentSoc!);

        DataBase.getCtxDb().Add(v);
        DataBase.getCtxDb().SaveChanges();

        return RedirectToAction("AjouterAuto", "Soc");
    }


    public IActionResult prendEnCharge()
    {
        string? propId = HttpContext.Request.RouteValues["id"]?.ToString();


        if (propId == null)
        {
            return RedirectToAction("Index", "Home");
        }
        Proposition? p = DataBase.getCtxDb().propositions?.Where(pr => pr.ID == int.Parse(propId)).FirstOrDefault();
        if (p == null)
        {
            return RedirectToAction("Index", "Home");

        }

        DataBase.getCtxDb().villes?.ToArray();
        ViewBag.voitures = ViewBag.voitures = DataBase.getCtxDb().autocars?.Where(v => v.owner!.login == HttpContext.Session.GetString("login")).ToArray();

        ViewBag.vd = p.villeDep!.nom;
        ViewBag.pid = p.ID;
        ViewBag.va = p.villeDar!.nom;
        ViewBag.hd = p.h_Dep;
        ViewBag.ha = p.h_Arr;

        return View();


    }

    [HttpPost]
    public IActionResult prendEnCharge(int x = 0)
    {
        string? propId = Request.Form["pid"];



        Proposition? p = DataBase.getCtxDb().propositions?.Where(pr => pr.ID == int.Parse(propId)).FirstOrDefault();
        if (p == null)
        {
            return RedirectToAction("Index", "Home");

        }

        DateTime date_debut = DateTime.Parse(Request.Form["d_debut"].ToString());
        DateTime date_fin = DateTime.Parse(Request.Form["d_fin"]);
        string h_depart = p.h_Dep!;
        string h_arrv = p.h_Arr!;
        decimal prix = decimal.Parse(Request.Form["prix"]);
        Ville villeDepart = DataBase.getCtxDb().villes!.Where(v => v.ID == p.villeDep!.ID).First();
        Ville villeDarr = DataBase.getCtxDb().villes!.Where(v => v.ID == p.villeDar!.ID).First();
        Autocar voiture = DataBase.getCtxDb().autocars!.Where(a => a.ID == Int32.Parse(Request.Form["autocar"])).First();
        Societe soc = DataBase.getCtxDb().societes!.Where(s => s.login == HttpContext.Session.GetString("login")).First();
        Abonnement ab = new Abonnement(date_debut, date_fin, h_depart, h_arrv, prix, villeDepart, villeDarr, voiture, soc);
        DataBase.getCtxDb().Add(ab);

        var v = DataBase.getCtxDb().votes?.Where(v => v.proposition!.ID == p.ID);
        foreach (var vote in v!)
        {
            DataBase.getCtxDb().Remove(vote);
        }
        DataBase.getCtxDb().propositions?.Remove(p);
        DataBase.getCtxDb().SaveChanges();
        TempData["Success"] = "Proposition pris en charge ! ";
        return RedirectToAction("Societe", "Home");
    }
    public IActionResult AjouterAbo()
    {

        if (HttpContext.Session.GetString("type") != "soc")
        {
            return RedirectToAction("Login", "User");
        }

        ViewBag.Success = TempData["Success"];

        string soc = HttpContext.Session.GetString("login")!;
        DataBase.getCtxDb().utilisateurs?.ToArray();
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
        decimal prix = decimal.Parse(Request.Form["prix"]);
        Ville villeDepart = DataBase.getCtxDb().villes!.Where(v => v.ID == Int32.Parse(Request.Form["villedp"])).First();
        Ville villeDarr = DataBase.getCtxDb().villes!.Where(v => v.ID == Int32.Parse(Request.Form["villeda"])).First();
        Autocar voiture = DataBase.getCtxDb().autocars!.Where(a => a.ID == Int32.Parse(Request.Form["autocar"])).First();
        Societe soc = DataBase.getCtxDb().societes!.Where(s => s.login == HttpContext.Session.GetString("login")).First();
        Abonnement ab = new Abonnement(date_debut, date_fin, h_depart, h_arrv, prix, villeDepart, villeDarr, voiture, soc);
        DataBase.getCtxDb().Add(ab);
        DataBase.getCtxDb().SaveChanges();
        TempData["Success"] = "Abonnement Ajouté avec success";
        return RedirectToAction("AjouterAbo", "Soc");
    }



}