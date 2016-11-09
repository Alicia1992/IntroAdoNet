using MedicoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicoWeb.Infra
{
    public static class MesSessions
    {
        public static LoginModel Patient
        {
            get { return (LoginModel)HttpContext.Current.Session["user"]; }
            set { HttpContext.Current.Session["user"] = value; }
        }
    }
}