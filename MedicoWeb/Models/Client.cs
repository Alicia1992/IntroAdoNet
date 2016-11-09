using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicoWeb.Models
{
    public class Client
    {
        #region prop
        private int _IdClient;
        private string _nom;
        private string _prenom;
        private string _numero;
        private DateTime _dateNaissance;
        private string _login;
        private string _password;
        private string _adresse;
        private bool? _sexe;

        public bool? Sexe
        {
            get { return _sexe; }
            set { _sexe = value; }
        }
        
        #endregion

        #region full 
        //acceder a privé
        [Required]
        public string Adresse
        {
            get { return _adresse; }
            set { _adresse = value; }
        }


        [Required]
        [Display(Name = "Password")]
        [MaxLength(15, ErrorMessage = "Maximum 15 caractères")]
        [MinLength(3, ErrorMessage = "Minimum 3 caractères")]
        [DataType(DataType.Password)] //pour avoir un champ caché
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        [Required]
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        [Required]
        [DataType(DataType.DateTime)] //check box de date
        [Display(Name = "Date de Naissance")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")] //format européen
        public DateTime DateNaissance
        {
            get { return _dateNaissance; }
            set { _dateNaissance = value; }
        }

        [Required]
        [Display(Name = "Numéro")]
        public string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }

        [Required]
        [Display(Name = "Prénom")]
        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }

        [Required]
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        [KeyAttribute] //definit comme clé primaire
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //permet de ne pas remplir le champ
        public int IdClient
        {
            get { return _IdClient; }
            set { _IdClient = value; }
        }
        #endregion
        
        public bool Save()
        {

            List<HoraireModel> laliste = new List<HoraireModel>();

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
                    new SqlCommand(@"INSERT INTO [dbo].[Client]
           ([Nom]
           ,[Prenom]
           ,[DateNaissance]
           ,[Numero]
           ,[Adresse]
           ,[Login]
           ,[Password]
           ,[Sexe])
     VALUES
           (@Nom
           ,@Prenom
           ,@DateNaissance
           ,@Numero
           ,@Adresse
           ,@Login
           ,@Password
           ,@Sexe)"
                                   , oConn);


                //3 - Ajout des paramètres

                SqlParameter pNom = new SqlParameter();
                pNom.ParameterName = "@Nom";
                pNom.Value = this.Nom;
                SqlParameter pPrenom = new SqlParameter();
                pPrenom.ParameterName = "@Prenom";
                pPrenom.Value = this.Prenom;
                SqlParameter pDateNaissance = new SqlParameter();
                pDateNaissance.ParameterName = "@DateNaissance";
                pDateNaissance.Value = this.DateNaissance;
                SqlParameter pNumero = new SqlParameter();
                pNumero.ParameterName = "@Numero";
                pNumero.Value = this.Numero;
                SqlParameter pAdresse = new SqlParameter();
                pAdresse.ParameterName = "@Adresse";
                pAdresse.Value = this.Adresse;
                SqlParameter pLogin = new SqlParameter();
                pLogin.ParameterName = "@Login";
                pLogin.Value = this.Login;
                SqlParameter pPassword = new SqlParameter();
                pPassword.ParameterName = "@Password";
                pPassword.Value = this.Password;
                SqlParameter pSexe = new SqlParameter();
                pSexe.ParameterName = "@Sexe";
                if (this.Sexe.Value)
                {
                    pSexe.Value = this.Sexe;
                }

                else
                {

                }


                oCmd.Parameters.Add(pNom);
                oCmd.Parameters.Add(pPrenom);
                oCmd.Parameters.Add(pDateNaissance);
                oCmd.Parameters.Add(pNumero);
                oCmd.Parameters.Add(pAdresse);
                oCmd.Parameters.Add(pLogin);
                oCmd.Parameters.Add(pPassword);
                oCmd.Parameters.Add(pSexe);

                oCmd.ExecuteNonQuery();

                //4 - Fermer la connexion
                oConn.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}