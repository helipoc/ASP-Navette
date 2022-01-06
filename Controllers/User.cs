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

        Console.WriteLine(type);
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

                ViewBag.Success = "Login success";
                HttpContext.Session.SetInt32("logged", 1);
                HttpContext.Session.SetString("type", "user");
                HttpContext.Session.SetString("name", user.nom ?? "");
                return RedirectToAction("Index", "Home");

            }
            else if (hashedMdp.Equals(soc?.mdp))
            {
                ViewBag.Success = "Login success";
                HttpContext.Session.SetInt32("logged", 1);
                HttpContext.Session.SetString("type", "soc");
                HttpContext.Session.SetString("name", soc.nom ?? "");
                return RedirectToAction("Index", "Home");
            }
            else ViewBag.Warn = "Login et/Ou mdp incorrect";
        }
        return View();
    }


}