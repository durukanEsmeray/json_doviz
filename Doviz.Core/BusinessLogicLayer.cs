using Doviz.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doviz.Core
{
    public class BusinessLogicLayer : BaseClass
    {
        DatabaseLogicLayer DLL;
        public BusinessLogicLayer()
        {
            DLL = new DatabaseLogicLayer();
        }

        public List<ParaBirimi> ParaBirimiListesi()
        {
            List<ParaBirimi> ParaBirimleri = new List<ParaBirimi>();
            SqlDataReader reader = DLL.ParaBirimiListesi();
            while (reader.Read())
            {
                ParaBirimleri.Add(new ParaBirimi() 
                {
                    ID = reader.IsDBNull(0)?Guid.Empty:reader.GetGuid(0),
                    Code =reader.IsDBNull(1)?string.Empty:reader.GetString(1),
                    Tanim =reader.IsDBNull(2)?string.Empty:reader.GetString(2),
                });
            }
            reader.Close();
            DLL.BaglantıIslemleri();
            return ParaBirimleri;
        }
    }
}
