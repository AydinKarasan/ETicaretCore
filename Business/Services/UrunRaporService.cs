
using AppCorev1.Business.Models;
using AppCorev1.Business.Models.Ordering;
using AppCorev1.Business.Models.Paging;
using AppCorev1.DataAccess.Entityframework;
using AppCorev1.DataAccess.Entityframework.Bases;
using Business.Models.Filters;
using Business.Models.Report;

using DataAccess.Contexts;
using DataAccess.Entities;
using System.Globalization;

namespace Business.Services
{
    public interface IUrunRaporService
    {
        public Result<List<UrunRaporModel>> List(UrunRaporFiltreModel filtre , PageModel sayfa, OrderModel sira);
        public List<string> GetSiralar() ;
    }

    public class UrunRaporService : IUrunRaporService
    {
        public Result<List<UrunRaporModel>> List(UrunRaporFiltreModel filtre, PageModel sayfa, OrderModel sira)
        {
            return new SuccessResult<List<UrunRaporModel>>(Query(filtre, sayfa,sira).ToList());
        }

        private IQueryable<UrunRaporModel> Query(UrunRaporFiltreModel filtre, PageModel sayfa, OrderModel sira)
        {
            ETicaretContext db = new ETicaretContext();
            RepoBase<Urun, ETicaretContext> urunRepo = new Repo<Urun, ETicaretContext>(db);
            RepoBase<Kategori, ETicaretContext> kategoriRepo = new Repo<Kategori, ETicaretContext>(db);
            RepoBase<UrunMagaza, ETicaretContext> urunMagazaRepo = new Repo<UrunMagaza, ETicaretContext>(db);
            RepoBase<Magaza, ETicaretContext> magazaRepo = new Repo<Magaza, ETicaretContext>(db);

            // select u.Adi, k.Adi,... from Urunler u inner join Kategoriler k on u.KategoriId = k.Id
            var urunQuery = urunRepo.Query();
            var kategoriQuery = kategoriRepo.Query();
            var urunMagazaQuery = urunMagazaRepo.Query();
            var magazaQuery = magazaRepo.Query();

            #region Inner Join Comment
            // inner join
            //var query = from urun in urunQuery
            //            join kategori in kategoriQuery
            //            on urun.KategoriId equals kategori.Id
            //            join urunMagaza in urunMagazaQuery
            //            on urun.Id equals urunMagaza.UrunId
            //            join magaza in magazaQuery
            //            on urunMagaza.MagazaId equals magaza.Id
            //            //where urun.Id == 7
            //            //orderby kategori.Adi, urun.Adi
            //            select new UrunModel()
            //            {
            //                Aciklamasi = urun.Aciklamasi,
            //                Adi = urun.Adi,
            //                BirimFiyati = urun.BirimFiyati,
            //                Id = urun.Id,
            //                KategoriId = urun.KategoriId,
            //                SonKullanmaTarihi = urun.SonKullanmaTarihi,
            //                StokMiktari = urun.StokMiktari,

            //                BirimFiyatiDisplay = urun.BirimFiyati.ToString("C2", new CultureInfo("tr-TR")),
            //                SonKullanmaTarihiDisplay = urun.SonKullanmaTarihi.HasValue ? urun.SonKullanmaTarihi.Value.ToString("yyyy.MM.dd") : "",
            //                KategoriAdiDisplay = kategori.Adi,
            //                MagazaDisplay = magaza.Adi
            //                MagazaId = magazaId
            //            };
            // left outer join
            #endregion
            /*
            select * from urunler u left join kategoriler k on u.KategoriId = k.Id left join urunmagazalar um on u.Id = um.UrunId
            left join magazalar m on um.MagazaId = m.Id 
            */
            var query = from urun in urunQuery
                        join kategori in kategoriQuery
                        on urun.KategoriId equals kategori.Id into kategoriler
                        from subKategoriler in kategoriler.DefaultIfEmpty()
                        join urunMagaza in urunMagazaQuery
                        on urun.Id equals urunMagaza.UrunId into urunMagazalar
                        from subUrunMagazalar in urunMagazalar.DefaultIfEmpty()
                        join magaza in magazaQuery
                        on subUrunMagazalar.MagazaId equals magaza.Id into magazalar
                        from subMagazalar in magazalar.DefaultIfEmpty()
                        select new UrunRaporModel()
                        {
                            Aciklama = urun.Aciklama,
                            urunAdi = urun.Adi,
                            BirimFiyati = urun.BirimFiyati,
                            KategoriId = urun.KategoriId,
                            SonKullanmaTarihi = urun.SonKullanmaTarihi,
                            StokMiktari = urun.StokMiktari,

                            BirimFiyatiDisplay = urun.BirimFiyati.ToString("C2", new CultureInfo("tr-TR")),
                            SonKullanmaTarihiDisplay = urun.SonKullanmaTarihi.HasValue ? urun.SonKullanmaTarihi.Value.ToString("yyyy.MM.dd") : "",
                            KategoriAdiDisplay = subKategoriler.Adi,
                            MagazaDisplay = subMagazalar != null ? subMagazalar.Adi : "",
                            MagazaId = subMagazalar != null ? subMagazalar.Id : 0
                        };

            //query = query.Where(q => q.Id == 7);
            //query = query.OrderBy(q => q.KategoriAdiDisplay).ThenBy(q => q.urunAdi); // buradaki sýralamaya gerek kalmadý 

            switch(sira.Expression)
            {
                case "Birim Fiyatý":
                    query = sira.IsDirectionAscending ? query.OrderBy(q => q.BirimFiyati) : query.OrderByDescending(q => q.BirimFiyati);
                    break;
                case "Stok Miktarý":
                    query = sira.IsDirectionAscending ? query.OrderBy(q => q.StokMiktari) : query.OrderByDescending(q => q.StokMiktari);
                    break;
                case "Son Kullanma Tarihi":
                    query = sira.IsDirectionAscending ? query.OrderBy(q => q.SonKullanmaTarihi) : query.OrderByDescending(q => q.SonKullanmaTarihi);
                    break;
                case "Kategori":
                    query = sira.IsDirectionAscending ? query.OrderBy(q => q.KategoriAdiDisplay) : query.OrderByDescending(q => q.KategoriAdiDisplay);
                    break;
                case "Maðaza":
                    query = sira.IsDirectionAscending ? query.OrderBy(q => q.MagazaDisplay) : query.OrderByDescending(q => q.MagazaDisplay);
                    break;
                default:
                    query = sira.IsDirectionAscending ? query.OrderBy(q => q.urunAdi) : query.OrderByDescending(q => q.urunAdi);
                    break;
            }

            if (filtre.KategoriId != null)
                query = query.Where(q => q.KategoriId == filtre.KategoriId);
            if (!string.IsNullOrWhiteSpace(filtre.UrunAdi))
                query = query.Where(q => q.urunAdi.ToLower().Contains(filtre.UrunAdi.ToLower().Trim()));
            if(filtre.BirimFiyatiMininmum.HasValue)
                query = query.Where(q => q.BirimFiyati >= filtre.BirimFiyatiMininmum);
            if (filtre.BirimFiyatiMaximum.HasValue)
                query = query.Where(q => q.BirimFiyati <= filtre.BirimFiyatiMaximum);
            if (filtre.SonKullanmaTarihiMinimum.HasValue)
                query = query.Where(q => q.SonKullanmaTarihi >= filtre.SonKullanmaTarihiMinimum);
            if (filtre.SonKullanmaTarihiMaximum.HasValue)
                query = query.Where(q => q.SonKullanmaTarihi <= filtre.SonKullanmaTarihiMaximum);
            if (filtre.MagazaIdleri != null && filtre.MagazaIdleri.Count > 0 )
                query = query.Where(q => filtre.MagazaIdleri.Contains(q.MagazaId));
            if (!string.IsNullOrWhiteSpace(filtre.UrunMagazaAdi))
                //query = query.Where(q => q.urunAdi.Contains(filtre.UrunMagazaAdi.Trim(), StringComparison.OrdinalIgnoreCase) || q.MagazaDisplay.Contains(filtre.UrunMagazaAdi.Trim(),StringComparison.OrdinalIgnoreCase));
                query = query.Where(q => q.urunAdi.ToLower().Contains(filtre.UrunMagazaAdi.ToLower().Trim()) || q.MagazaDisplay.ToLower().Contains(filtre.UrunMagazaAdi.ToLower().Trim()));
            sayfa.TotalRecordsCount = query.Count();
            query = query.Skip((sayfa.PageNumber - 1) * sayfa.RecordsPerPageCount).Take(sayfa.RecordsPerPageCount);

            return query;
        }
        public List<string> GetSiralar() => new List<string>() { "Ürün Adý", "Birim Fiyatý" , "Stok Miktarý", "Son Kullanma Tarihi", "Maðaza" };
    }
}