using AppCore.DataAccess.Configs;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class ETicaretContext : DbContext
    {
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Magaza> Magazalar { get; set; }
        public DbSet<UrunMagaza> UrunMagazalar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<KullaniciDetayi> KullaniciDetaylari { get; set; }
        public DbSet<Ulke> Ulkeler { get; set; }
        public DbSet<Sehir> Sehirler { get; set; }
        public DbSet<Rol> Roller { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //windows authentication
            //string connectionString = @"server=.\SQLEXPRESS;database=ETicaretCore;trusted_connection=true;multipleactiveresultsets=true;";
            //sql server authentication
            string connectionString = @"server=.\SQLEXPRESS;database=ETicaretCore;user id=sa;password=sa;multipleactiveresultsets=true;";

            if (!string.IsNullOrWhiteSpace(ConnectionConfig.ConnectionString))
                connectionString = ConnectionConfig.ConnectionString;

            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one to many iliþkiler 
            modelBuilder.Entity<Urun>()
                /*.ToTable("ETicaretUrunler");*/ // sql deki tablo adýný deðiþtirmek için/ yazmazsan Dbset teki entity neyse o olur
                .HasOne(u => u.Kategori)
                .WithMany(k => k.Urunler)
                .HasForeignKey(u => u.KategoriId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Urun>()
                .HasMany(u => u.UrunMagazalar)
                .WithOne(um => um.Urun)
                .HasForeignKey(um => um.UrunId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Magaza>()
                .HasMany(m => m.UrunMagazalar)
                .WithOne(um => um.Magaza)
                .HasForeignKey(um => um.MagazaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UrunMagaza>()
                .HasKey(um => new { um.UrunId, um.MagazaId });

            modelBuilder.Entity<Kullanici>()                
                .HasOne(kullanici => kullanici.Rol)
                .WithMany(rol => rol.Kullanicilar)
                .HasForeignKey(kullanici => kullanici.RolId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KullaniciDetayi>()                
                .HasOne(kullaniciDetayi => kullaniciDetayi.Kullanici)
                .WithOne(kullanici => kullanici.KullaniciDetayi)
                .HasForeignKey<KullaniciDetayi>(kullaniciDetayi => kullaniciDetayi.KullaniciId) //burada delegenin tipini söylemek gerekti, hata alýrsa dikkat et
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KullaniciDetayi>()
                .HasOne(kullaniciDetayi => kullaniciDetayi.Ulke)
                .WithMany(ulke => ulke.KullaniciDetaylari)
                .HasForeignKey(kullaniciDetayi => kullaniciDetayi.UlkeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KullaniciDetayi>()
                .HasOne(kullaniciDetayi => kullaniciDetayi.Sehir)
                .WithMany(sehir => sehir.KullaniciDetaylari)
                .HasForeignKey(kullaniciDetayi => kullaniciDetayi.SehirId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sehir>()                
                .HasOne(sehir => sehir.Ulke)
                .WithMany(ulke => ulke.Sehirler)
                .HasForeignKey(sehir => sehir.UlkeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
