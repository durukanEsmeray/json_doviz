using Doviz.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
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
                    ID = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                    Code = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    Tanim = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                });
            }
            reader.Close();
            DLL.BaglantıIslemleri();
            return ParaBirimleri;
        }

        public List<Kur> KurListe()
        {
            List<Kur> KurDegerleri = new List<Kur>();

            SqlDataReader reader = DLL.KurListe();
            while (reader.Read())
            {
                KurDegerleri.Add(new Kur()
                {
                    ID = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                    ParaBirimiID = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                    Alis = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                    Satis = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                    OlusturmaTarih = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4)
                });
            }
            reader.Close();
            DLL.BaglantıIslemleri();

            return KurDegerleri;
        }

        public Kur KurListe(Guid ParaBirimiID)
        {
            Kur Kur = new Kur();

            SqlDataReader reader = DLL.KurListe();
            while (reader.Read())
            {
                Kur = new Kur()
                {
                    ID = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                    ParaBirimiID = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                    Alis = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                    Satis = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                    OlusturmaTarih = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4)
                };
            }
            reader.Close();
            DLL.BaglantıIslemleri();

            return Kur;
        }

        public List<KurGecmis> KurGecmisListe()
        {
            List<KurGecmis> KurGecmisListe = new List<KurGecmis>();
            SqlDataReader reader = DLL.KurGecmisListe();
            while (reader.Read())
            {
                KurGecmisListe.Add(new KurGecmis()
                {
                    ID = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                    KurID = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                    ParaBirimiID = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2),
                    Alis = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                    Satis = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4),
                    OlusturmaTarih = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5)
                });
            }
            reader.Close();
            DLL.BaglantıIslemleri();
            return KurGecmisListe;
        }

        public List<KurGecmis> KurGecmisListe(Guid ParaBirimiID)
        {
            List<KurGecmis> KurGecmisListe = new List<KurGecmis>();
            SqlDataReader reader = DLL.KurGecmisListe(ParaBirimiID);
            while (reader.Read())
            {
                KurGecmisListe.Add(new KurGecmis()
                {
                    ID = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0),
                    KurID = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1),
                    ParaBirimiID = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2),
                    Alis = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                    Satis = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4),
                    OlusturmaTarih = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5)
                });
            }
            reader.Close();
            DLL.BaglantıIslemleri();
            return KurGecmisListe;
        }

        public void KurKayitEkle(Guid ID, Guid ParaBirimiID, decimal Alis, decimal Satis, DateTime OlusturmaTarih)
        {
            if(ID != Guid.Empty && ParaBirimiID != Guid.Empty && Alis != 0 &&  Satis != 0 &&OlusturmaTarih > DateTime.MinValue)
            {
                DLL.KurKayitEkle(new Kur()
                {
                    ID = ID,
                    ParaBirimiID = ParaBirimiID,
                    Alis = Alis,
                    Satis = Satis,
                    OlusturmaTarih = OlusturmaTarih
                });
            }
            else
            {
                //ekleme işlemi gerçekleştirilmedi.
            }
            
        }

        public void KurBilgileriniGuncelle()
        {
            WebClient webClient = new WebClient();
            string JsonDataTxt = webClient.DownloadString("https://api.genelpara.com/embed/altin.json");
            List<JsonDataType> DovizKurBilgileri = JsonConvert.DeserializeObject<List<JsonDataType>>(JsonDataTxt);

            List<ParaBirimi> ParaBirimiListe = ParaBirimiListesi();
            for (int i = 0; i < ParaBirimiListe.Count; i++)
            {
               JsonDataType BulunanKur = DovizKurBilgileri.FirstOrDefault(I => I.alis == ParaBirimiListe[i].Code);
                KurKayitEkle(Guid.NewGuid(), ParaBirimiListe[i].ID, decimal.Parse(BulunanKur.alis), decimal.Parse(BulunanKur.satis), DateTime.Now);
            }
        }
    }
}
