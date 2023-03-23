using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Doviz.Core
{
    public class DatabaseLogicLayer : BaseClass
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;

        public DatabaseLogicLayer()
        {
            con = new SqlConnection("Data source = DESKTOP-I299EIT\\SQLEXPRESS; initial catalog= Doviz; username = Durukan; password = 123456; ");
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
    }
}
