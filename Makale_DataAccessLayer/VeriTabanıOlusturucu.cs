using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_DataAccessLayer
{
    public class VeriTabanıOlusturucu : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            Kullanici admin = new Kullanici()
            {
                Ad = "Duygu",
                Soyad = "Kaya",
                Email = "duygukaya@gmail.com",
                Sifre = "123",
                Aktif = true,
                Admin = true,
                AktifGuid = Guid.NewGuid(),
                KullaniciAdi = "duygukaya",
                KayitTarihi = DateTime.Now,
                DegistirmeTarihi = DateTime.Now.AddMinutes(5),
                DegistirenKullanici = "duygukaya"   //kullanıcı adıyla aynı
            };
            context.Kullanicilar.Add(admin);
            for (int i = 0; i < 6; i++)
            {
                Kullanici user = new Kullanici()
                {
                    Ad = FakeData.NameData.GetFirstName(),
                    Soyad = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    Sifre = "1234",
                    KullaniciAdi = "user" + i,
                    Aktif = true,
                    Admin = false,
                    AktifGuid = Guid.NewGuid(),
                    KayitTarihi = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    DegistirmeTarihi = DateTime.Now,
                    DegistirenKullanici = "user" + i
                };

                context.Kullanicilar.Add(user);
            }
            context.SaveChanges();
            List<Kullanici> kullanicilistesi=context.Kullanicilar.ToList();

            //FAKE KATEGORİ EKLEME

            for (int i = 0; i < 10; i++)
            {
                Kategori kat = new Kategori()
                {
                    Baslik = FakeData.PlaceData.GetStreetName(),
                    Aciklama = FakeData.PlaceData.GetAddress(),
                    KayitTarihi = DateTime.Now,
                    DegistirmeTarihi=DateTime.Now,
                    DegistirenKullanici = "duygu"
                };
                context.Kategoriler.Add(kat);

                ///FAKE NOT EKLEME

                for (int j = 1; j < 6; j++)
                {
                    Not not = new Not()
                    {
                        Baslik = FakeData.TextData.GetAlphabetical(20),
                        Text = FakeData.TextData.GetSentences(3),
                        Taslak = false,
                        BegeniSayisi = FakeData.NumberData.GetNumber(1, 9),
                        Kullanici = kullanicilistesi[FakeData.NumberData.GetNumber(0, 5)], 
                        //rastgele seçecek kkulannıcıyı
                        KayitTarihi = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        DegistirmeTarihi = DateTime.Now,
                        DegistirenKullanici = kullanicilistesi[FakeData.NumberData.GetNumber(0, 5)].KullaniciAdi
                    };
                    kat.Notlar.Add(not);     /// bu kategorinin notlarını insert et demek 
                    // yani her bir notun bir kategorisi var

                    //FAKE YORUM EKLE

                    for (int k = 1; k < 4; k++)
                    {
                        Yorum y = new Yorum()
                        {
                            Text = FakeData.TextData.GetSentences(3),
                            Kullanici = kullanicilistesi[FakeData.NumberData.GetNumber(0, 5)],
                            KayitTarihi = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            DegistirmeTarihi = DateTime.Now,
                            DegistirenKullanici = kullanicilistesi[FakeData.NumberData.GetNumber(0, 5)].KullaniciAdi

                        };

                        not.Yorumlar.Add(y);
                    }// yorum for

                    //FAKE BEĞENİ EKLE YORUMUN BEĞENİSİ
                    for (int x = 0; x < not.BegeniSayisi; x++)
                    {
                        Begeni b=new Begeni()
                        {
                            Kullanici=kullanicilistesi[FakeData.NumberData.GetNumber(0,5)]
                        };
                        not.Begeniler.Add(b);
                    } //like for

                } //makale not for

            }//kategori for
            context.SaveChanges();
        }
    }
}