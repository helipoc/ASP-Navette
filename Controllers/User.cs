using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ASP.Controllers;

public class User : Controller
{






    [HttpGet]
    public IActionResult Signup()
    {
        if (HttpContext.Session.GetInt32("logged") == 1)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    public IActionResult Signup(int x = 0)
    {
        string nom = Request.Form["nom"];
        string log = Request.Form["login"];
        string mp = Request.Form["mdp"];
        string tel = Request.Form["telephone"];
        string type = Request.Form["type"];

        Utilisateur? u = DataBase.getCtxDb().utilisateurs?.Where(u => u.login == log).FirstOrDefault();

        if (u != null)
        {
            ViewBag.Error = "Email exist déja";
            return View();
        }

        Societe? s = DataBase.getCtxDb().societes?.Where(u => u.login == log).FirstOrDefault();

        if (s != null)
        {
            ViewBag.Error = "Email exist déja";
            return View();
        }

        if (type == "user")
        {


            Utilisateur user = new Utilisateur(nom, tel, log, mp);

            DataBase.getCtxDb().Add(user);
            DataBase.getCtxDb().SaveChanges();
            ViewBag.Success = "Compte Créé avec successs";
        }
        else
        {
            Societe sc = new Societe(nom, tel, log, mp);


            DataBase.getCtxDb().Add(sc);
            DataBase.getCtxDb().SaveChanges();
            ViewBag.Success = "Compte Créé avec successs";

        }

        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (HttpContext.Session.GetInt32("logged") == 1)
        {
            return RedirectToAction("Index", "home");
        }
        return View();
    }

    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
    [HttpPost]

    public IActionResult Login(int x = 0)
    {

        string login = Request.Form["login"];
        string password = Request.Form["mdp"];

        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            string hashedMdp = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            Utilisateur? user = DataBase.getCtxDb().utilisateurs?.Where(u => u.login == login)?.FirstOrDefault();
            Societe? soc = DataBase.getCtxDb().societes?.Where(u => u.login == login)?.FirstOrDefault();
            if (user?.mdp == null && soc?.mdp == null)
            {
                ViewBag.Error = "utilisateur / societe n'exist pas ";
                return View();
            }

            else if (hashedMdp.Equals(user?.mdp))
            {

                HttpContext.Session.SetInt32("logged", 1);
                HttpContext.Session.SetString("type", "user");
                HttpContext.Session.SetString("login", user.login ?? "");
                HttpContext.Session.SetString("name", user.nom ?? "");
                return RedirectToAction("Index", "Home");

            }
            else if (hashedMdp.Equals(soc?.mdp))
            {
                HttpContext.Session.SetInt32("logged", 1);
                HttpContext.Session.SetString("type", "soc");
                HttpContext.Session.SetString("login", soc.login ?? "");
                HttpContext.Session.SetString("name", soc.nom ?? "");
                return RedirectToAction("Index", "Home");
            }
            else ViewBag.Warn = "Login et/Ou mdp incorrect";
        }
        return View();
    }


    public IActionResult Abonner()
    {
        if (HttpContext.Session.GetString("type") != "user")
        {
            return RedirectToAction("Index", "Home");
        }
        Utilisateur? User = DataBase.getCtxDb().utilisateurs?.Where(a => a.login == HttpContext.Session
        .GetString("login")
        ).FirstOrDefault();

        string? aboId = HttpContext.Request.RouteValues["id"]?.ToString();


        if (aboId == null)
        {
            return RedirectToAction("Index", "Home");
        }



        Abonnement? a = DataBase.getCtxDb().abonnements?.Where(a => a.ID == Int32.Parse(aboId!)).FirstOrDefault();
        if (a == null)
        {
            return RedirectToAction("Index", "Home");
        }
        if (a.d_Fin <= DateTime.Now)
        {
            return RedirectToAction("Index", "Home");
        }
        if (User!.adhere.Contains(a))
        {
            return RedirectToAction("Index", "Home");
        }
        User!.adhere.Add(a);
        DataBase.getCtxDb().SaveChanges();
        TempData["Success"] = "Abonnez Avec success";
        return RedirectToAction("Utilisateur", "Home");
    }


    public IActionResult Abonnements()
    {
        if (HttpContext.Session.GetString("type") != "user")
        {
            return RedirectToAction("Index", "Home");
        }

        string? lo = HttpContext.Session.GetString("login");
        DataBase.getCtxDb().villes?.ToArray();
        Utilisateur u = DataBase.getCtxDb().utilisateurs!.Where(x => x.login == lo).First();

        ViewBag.abos = u.adhere.ToArray();

        ViewBag.Success = TempData["Success"];

        return View();
    }


    public IActionResult Desabonner()
    {

        if (HttpContext.Session.GetString("type") != "user")
        {
            return RedirectToAction("Index", "Home");
        }
        Utilisateur? User = DataBase.getCtxDb().utilisateurs?.Where(a => a.login == HttpContext.Session
        .GetString("login")
        ).FirstOrDefault();

        string? aboId = HttpContext.Request.RouteValues["id"]?.ToString();


        if (aboId == null)
        {
            return RedirectToAction("Index", "Home");
        }


        Abonnement? a = DataBase.getCtxDb().abonnements?.Where(a => a.ID == Int32.Parse(aboId!)).FirstOrDefault();

        User!.adhere.Remove(a!);
        TempData["Success"] = "Abonnement supprimer !";
        return RedirectToAction("Abonnements", "User");



    }

    public IActionResult Voter()
    {

        if (HttpContext.Session.GetString("type") != "user")
        {
            return RedirectToAction("Index", "Home");
        }
        Utilisateur? User = DataBase.getCtxDb().utilisateurs?.Where(a => a.login == HttpContext.Session
        .GetString("login")
        ).FirstOrDefault();

        string? pId = HttpContext.Request.RouteValues["id"]?.ToString();


        if (pId == null)
        {
            return RedirectToAction("Proposer", "User");
        }


        Proposition? p = DataBase.getCtxDb().propositions?.Where(pr => pr.ID == Int32.Parse(pId!)).FirstOrDefault();

        Vote? v = DataBase.getCtxDb().votes?.Where(v => v.user == User! && v.proposition == p!).FirstOrDefault();

        if (v == null)
        {
            p!.nbVote += 1;
            Vote vt = new Vote(User!, p!);
            DataBase.getCtxDb().votes?.Add(vt);
            DataBase.getCtxDb().SaveChanges();
            TempData["Success"] = "Vous Avez Voté pour " + p.villeDep!.nom + " - " + p.villeDar!.nom;

        }
        else
        {
            TempData["Error"] = "Vous avez déja voté";
        }
        return RedirectToAction("Proposer", "User");



    }

    public IActionResult Proposer()
    {
        if (HttpContext.Session.GetString("type") != "user")
        {
            return RedirectToAction("Index", "Home");
        }
        Utilisateur? User = DataBase.getCtxDb().utilisateurs?.Where(a => a.login == HttpContext.Session
        .GetString("login")
        ).FirstOrDefault();


        ViewBag.cities = DataBase.getCtxDb().villes?.ToArray();
        ViewBag.props = DataBase.getCtxDb().propositions?.ToArray();

        ViewBag.Success = TempData["Success"];
        ViewBag.Error = TempData["Error"];
        return View();
    }

    [HttpPost]
    public IActionResult Proposer(int x = 0)
    {

        if (HttpContext.Session.GetString("type") != "user")
        {
            return RedirectToAction("Index", "Home");
        }

        Utilisateur? User = DataBase.getCtxDb().utilisateurs?.Where(a => a.login == HttpContext.Session
        .GetString("login")
        ).FirstOrDefault();

        string vd = Request.Form["villedp"];
        string va = Request.Form["villeda"];
        string hd = Request.Form["hd"];
        string ha = Request.Form["ha"];
        Ville? villedp = DataBase.getCtxDb()
        .villes?.Where(v => v.ID == int.Parse(vd)).FirstOrDefault();
        Ville? villeda = DataBase.getCtxDb()
        .villes?.Where(v => v.ID == int.Parse(va)).FirstOrDefault();

        Proposition? p = DataBase.getCtxDb()
        .propositions?.Where(pr => pr.villeDep!.nom == villedp!.nom
        && pr.villeDar!.nom == villeda!.nom && pr.h_Dep == hd && pr.h_Arr == ha
        ).FirstOrDefault();

        if (p == null)
        {
            Proposition pr = new Proposition(hd, ha, villedp!, villeda!);
            DataBase.getCtxDb().Add(pr);
            Vote v = new Vote(User!, pr);
            DataBase.getCtxDb().Add(v);
            DataBase.getCtxDb().SaveChanges();

        }
        else
        {

            Vote? v = DataBase.getCtxDb().votes?.Where(x =>
            x.user == User! && x.proposition == p!).FirstOrDefault();

            if (v == null)
            {
                p.nbVote += 1;
                Vote vt = new Vote(User!, p);
                DataBase.getCtxDb().Add(vt);
                DataBase.getCtxDb().SaveChanges();
            }

            else
            {
                TempData["Error"] = "Vous avez déja voté";
                return RedirectToAction("Proposer", "User");
            }




        }










        ViewBag.cities = DataBase.getCtxDb().villes?.ToArray();


        TempData["Success"] = "Proposition Ajouté";

        return RedirectToAction("Proposer", "User");

    }
}