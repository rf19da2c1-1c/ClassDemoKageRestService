using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ClassDemoKageLib.model;

namespace ClassDemoKageREST.DBUtil
{
    public class ManagerKager
    {
        /*
         * Skal kunne minst fem metoder
         *
         * Hente alle, Hente en specific, slette, opdatere, indsætte
         *
         */

        private const String connString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=ClassDemo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string GET_ALL_SQL = "select * from Kage";
        public IList<Kage> HentAlle()
        {
            IList<Kage> retListe = new List<Kage>();

            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(GET_ALL_SQL, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        retListe.Add(ReadNextKage(reader));
                    }
                }
            }

            return retListe;
        }

        private const string GET_ONE_SQL = "select * from Kage where Id = @ID";
        public Kage HentEn(int id)
        {
            Kage kage = new Kage();

            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(GET_ONE_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        kage = ReadNextKage(reader);
                    }
                }
            }

            return kage;
        }

        private Kage ReadNextKage(SqlDataReader reader)
        {
            Kage kage = new Kage();

            kage.Name = reader.GetString(0);
            kage.Price = reader.GetDouble(1);
            kage.NoOfPieces = reader.GetInt32(2);
            kage.Id = reader.GetInt32(3);

            return kage;
        }


        private const string INSERT_SQL = "insert into Kage (Name, Price, NoOfPieces) values (@NAME, @PRICE, @NOOFPIECES)";
        public bool OpretKage(Kage kage)
        {
            bool OK = true;
            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(INSERT_SQL, conn))
                {
                    cmd.Parameters.AddWithValue("@NAME", kage.Name);
                    cmd.Parameters.AddWithValue("@PRICE", kage.Price);
                    cmd.Parameters.AddWithValue("@NOOFPIECES", kage.NoOfPieces);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        OK = rows == 1;
                    }
                    catch (Exception ex)
                    {
                        OK = false;
                    }
                }
            }

            return OK;
        }


        private const string UPDATE_SQL = "update Kage set Name = @NAME, Price = @PRICE, NoOfPieces= @NOOFPIECES where Id = @ID";
        public bool OpdaterKage(int id, Kage kage)
        {
            bool OK = true;
            // forbindelse til database
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                // sql kald
                using (SqlCommand cmd = new SqlCommand(UPDATE_SQL, conn))
                {
                    // NB In this case never set the id to a new value 
                    cmd.Parameters.AddWithValue("@NAME", kage.Name);
                    cmd.Parameters.AddWithValue("@PRICE", kage.Price);
                    cmd.Parameters.AddWithValue("@NOOFPIECES", kage.NoOfPieces);
                    cmd.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        OK = rows == 1;
                    }
                    catch (Exception ex)
                    {
                        OK = false;
                    }
                }
            }

            return OK;
        }


        private const string DELETE_SQL = "delete from Kage where Id = @ID";
        public Kage SletKage(int id)
        {
            Kage kage = HentEn(id);

            if (kage.Id != -1)
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(DELETE_SQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        int rows = cmd.ExecuteNonQuery();
                    }
                }
            }

            return kage;
        }

    }
}