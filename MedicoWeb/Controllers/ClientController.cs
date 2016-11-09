using MedicoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicoWeb.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult TestAjax()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Client C) //il vise le client du modele pour créer un client
        {
            if (ModelState.IsValid) //si tous les champs du formulaire sont true, alors on fait la methode .save qui est dans le model
                                    //il occupe toutes les contraintes donc il peut passer a la suite
            {
                if (C.Save())
                {
                    ViewBag.Success = 1;
                    return View();
                }
                else
                {
                    ViewBag.ErrorMsg = "Erreur lors de l'enregistrement";
                    return View();
                }
            }
            else
            {


                ViewBag.ErrorMsg = "Vous ne respectez pas les contraintes";
                return View();
            }
        }
        
        [HttpPost]
        public ActionResult LogMe(LoginModel lm)
        {
            if (lm.Verif())
            {
                ViewBag.SuccessLogin = "Welcome";
                return View("Index");
            }
            else
            {
                ViewBag.ErrorLogin = "Invalid login/Mot de passe";
                return View("Index");
            }
        }
    
    }

}

