using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MedicoWeb.Models
{
    public class HoraireModel
    {
        #region Fields
        private int _idHoraire;
        private string _jour;
        private TimeSpan _debMat;
        private TimeSpan _finMat;
        private TimeSpan _debAprem;
        private TimeSpan _finAprem;
        private DateTime _debDate;
        private DateTime _finDate;
        #endregion

        #region Properties
        public DateTime FinDate
        {
            get { return _finDate; }
            set { _finDate = value; }
        }
        
        public DateTime DebDate
        {
            get { return _debDate; }
            set { _debDate = value; }
        }


        public TimeSpan FinAprem
        {
            get { return _finAprem; }
            set { _finAprem = value; }
        }


        public TimeSpan DebAprem
        {
            get { return _debAprem; }
            set { _debAprem = value; }
        }

        public TimeSpan FinMat
        {
            get { return _finMat; }
            set { _finMat = value; }
        }

        public TimeSpan DebMat
        {
            get { return _debMat; }
            set { _debMat = value; }
        }
        
        public string Jour
        {
            get { return _jour; }
            set { _jour = value; }
        }
        
        public int IdHoraire
        {
            get { return _idHoraire; }
            set { _idHoraire = value; }
        }
        #endregion

        public List<HoraireModel> GetAll()
        {
            List<HoraireModel> laListe = new List<HoraireModel>();

            // 1 - Connexion
            SqlConnection oConn = new SqlConnection();
            
                // 1.1 Chemin vers le serveur ==> connectionString
                oConn.ConnectionString = @"Data Source=26R2-4\WADSQL;Initial Catalog=MedicoDB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
                // 1.2 Ouvrir la connexion
                try 
	            {	        
		            oConn.Open();
                    
                    // 2 - Commande
                    SqlCommand oCmd = new SqlCommand(@"select *
                                                       from Horaire
                                                       where DebDate<= GETDATE()
                                                       and FinDate>= GETDATE()"
                                                       ,oConn);
                    //Forme longue
                    //SqlCommand oCmd = new SqlCommand();
                    //oCmd.Connection = oConn;
                    //oCmd.CommandText = "select..";
                    
                    // 3 - Execute
                    SqlDataReader oDr = oCmd.ExecuteReader();
                    // 3.1 - Lire toutes les lignes
                        while(oDr.Read())
                        {
                            HoraireModel HM = new HoraireModel();

                            HM.IdHoraire = (int)oDr["idHoraire"];
                            HM.Jour = (string)oDr["Jour"];
                            HM.DebMat = (TimeSpan)oDr["DebMat"];
                            HM.FinMat = (TimeSpan)oDr["FinMat"];
                            HM.DebAprem = (TimeSpan)oDr["DebAprem"];
                            HM.FinAprem = (TimeSpan)oDr["FinAprem"];
                            HM.DebDate = (DateTime)oDr["DebDate"];
                            HM.FinDate = (DateTime)oDr["FinDate"];
                            laListe.Add(HM);
                        }
                    // 3.2 - Fermer le reader
                        oDr.Close();
                    // 4 - Fermer la connexion
                        oConn.Close();
	            }
	            catch (Exception)
	            {
		
		            throw;
	            }
                return laListe;
        }
    }
}