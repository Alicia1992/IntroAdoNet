using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicoWeb.Models
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string password { get; set; }

        public bool Verif()
        {
            bool isverif = false;

            //1 - Connexion
            SqlConnection oConn = new SqlConnection();
            //1.1 - Chemin vers le serveur ==> ConnectionString
            oConn.ConnectionString = @"Data Source=26R2-4\WADSQL;Initial Catalog=MedicoDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
            //1.2 - Ouvrir la connexion
            try
            {
                oConn.Open();

                //2 - Commande
                SqlCommand oCmd =
                    new SqlCommand(@"select *
                                     from Client
                                     where [Login]= @login 
                                        and [Password]=@pass"
                                   , oConn);
                SqlParameter pLogin = new SqlParameter("login", this.Login);
                SqlParameter pPasswd = new SqlParameter("pass", this.password);
                oCmd.Parameters.Add(pLogin);
                oCmd.Parameters.Add(pPasswd);

                //3 - Execute
                SqlDataReader oDr = oCmd.ExecuteReader();
                //3.1 - lire toute les lignes
                if (oDr.Read())
                {
                    HttpContext.Current.Session["User"] = this.Login;
                    isverif = true;
                }
                //3.2 - Fermer le reader
                oDr.Close();
                //4 - Fermer la connexion
                oConn.Close();
            }
            catch (Exception ex)
            {
                isverif = false;
            }
            return isverif;
        }
    }
}