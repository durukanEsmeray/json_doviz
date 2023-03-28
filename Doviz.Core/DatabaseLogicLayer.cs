using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Doviz.Entities;
using System.Data;

namespace Doviz.Core
{
    public class DatabaseLogicLayer : BaseClass
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;

        public DatabaseLogicLayer()
        {
            con = new SqlConnection("Data source = DESKTOP-I299EIT\\SQLEXPRESS; initial catalog= Doviz; user id = Durukan; password = 123456; ");
        }

        public void BaglantıIslemleri()
        {
            if(con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            else
            {
                con.Open();
            }

            
        }

        public SqlDataReader ParaBirimiListesi()
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("Select * from ParaBirimi", con);
                BaglantıIslemleri();
                reader = cmd.ExecuteReader();
            });
            return reader;
        } 
        public SqlDataReader KurListe()
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("select * from Kur", con);
                BaglantıIslemleri();
                reader = cmd.ExecuteReader();
            });
            return reader;
        }

        public SqlDataReader KurListe(Guid ParabirimiID)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("select * from Kur where ParaBirimiID = @ParaBirimiID", con);
                cmd.Parameters.Add("@ParaBirimiID", System.Data.SqlDbType.UniqueIdentifier).Value = ParabirimiID;
                BaglantıIslemleri();
                reader = cmd.ExecuteReader();
            });
            return reader;

            
        }

        public SqlDataReader KurGecmisListe()
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("Select * from KurGecmis", con);
                BaglantıIslemleri();
                reader = cmd.ExecuteReader();
            });
            return reader;
        }

        public SqlDataReader KurGecmisListe(Guid ParaBirimiID)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("Select * from KurGecmis where ParaBirimiID = @ParaBirimiID", con);
                cmd.Parameters.Add("@ParaBirimiID", System.Data.SqlDbType.UniqueIdentifier).Value = ParaBirimiID;
                BaglantıIslemleri();
                reader = cmd.ExecuteReader();
            });
            return reader;
        }

        public void KurKayitEkle(Kur kur)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("KurKayitEkle", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = kur.ID;
                cmd.Parameters.Add("@ParaBirimiID", SqlDbType.UniqueIdentifier).Value = kur.ParaBirimiID;
                cmd.Parameters.Add("@Alis", SqlDbType.Decimal).Value = kur.Alis;
                cmd.Parameters.Add("@Satis", SqlDbType.Decimal).Value = kur.Satis;
                cmd.Parameters.Add("@OlusturmaTarih", SqlDbType.DateTime).Value = kur.OlusturmaTarih;
                BaglantıIslemleri();
                cmd.ExecuteNonQuery();
                
            });
            BaglantıIslemleri();

        }

    }
}
