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

        return View();
    }






}