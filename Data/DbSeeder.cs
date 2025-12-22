using Microsoft.EntityFrameworkCore;
using Web_Programlama_Proje.Models;

namespace Web_Programlama_Proje.Data
{
    public static class DbSeeder
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Veritabanı tablolarını oluştur (Migration'ları uygula)
                context.Database.Migrate();

                // Eğer veritabanında hiç ürün yoksa ekle
                if (!context.MenuItems.Any())
                {
                    context.MenuItems.AddRange(
                        // --- YEMEKLER ---
                        new MenuItem { Title = "Adana Kebap", Description = "Zırh ile çekilmiş acılı kuzu kıyması, közlenmiş sebzelerle.", Price = 320.00m, Category = "Yemekler", ImageUrl = null },
                        new MenuItem { Title = "Urfa Kebap", Description = "Zırh ile çekilmiş acısız kuzu kıyması, özel sunum.", Price = 320.00m, Category = "Yemekler", ImageUrl = null },
                        new MenuItem { Title = "İskender Kebap", Description = "Döner eti, pide, tereyağı ve özel domates sosu.", Price = 380.00m, Category = "Yemekler", ImageUrl = null },
                        new MenuItem { Title = "Beyti Sarma", Description = "Lavaşa sarılı kuzu kıyması, sarımsaklı yoğurt ve sos.", Price = 360.00m, Category = "Yemekler", ImageUrl = null },
                        new MenuItem { Title = "Kayseri Mantısı", Description = "El açması, yoğurtlu ve soslu geleneksel mantı.", Price = 280.00m, Category = "Yemekler", ImageUrl = null },
                        new MenuItem { Title = "Lahmacun", Description = "İnce hamurlu, bol malzemeli taş fırın lahmacun.", Price = 120.00m, Category = "Yemekler", ImageUrl = null },
                        new MenuItem { Title = "Karnıyarık", Description = "Kıymalı patlıcan yemeği, pilav eşliğinde.", Price = 250.00m, Category = "Yemekler", ImageUrl = null },
                        new MenuItem { Title = "Etli Ekmek", Description = "Konya usulü uzun ve ince kıymalı pide.", Price = 260.00m, Category = "Yemekler", ImageUrl = null },
                        new MenuItem { Title = "Hünkar Beğendi", Description = "Patlıcan beğendi yatağında yumuşak kuzu eti.", Price = 390.00m, Category = "Yemekler", ImageUrl = null },
                        new MenuItem { Title = "Zeytinyağılı Yaprak Sarma", Description = "Özel baharatlı pirinç ile sarılmış asma yaprağı.", Price = 180.00m, Category = "Yemekler", ImageUrl = null },

                        // --- İÇECEKLER ---
                        new MenuItem { Title = "Yayık Ayran", Description = "Bol köpüklü, naneli ev yapımı ayran.", Price = 40.00m, Category = "İçecekler", ImageUrl = null },
                        new MenuItem { Title = "Şalgam Suyu", Description = "Adana'dan özel, acılı veya acısız.", Price = 45.00m, Category = "İçecekler", ImageUrl = null },
                        new MenuItem { Title = "Türk Kahvesi", Description = "Közde pişmiş, lokum eşliğinde.", Price = 70.00m, Category = "İçecekler", ImageUrl = null },
                        new MenuItem { Title = "Demleme Çay", Description = "Rize çayı, ince belli bardakta.", Price = 25.00m, Category = "İçecekler", ImageUrl = null },
                        new MenuItem { Title = "Ev Yapımı Limonata", Description = "Taze naneli, buz gibi ferahlatıcı.", Price = 60.00m, Category = "İçecekler", ImageUrl = null },
                        new MenuItem { Title = "Osmanlı Şerbeti", Description = "Karanfil ve tarçın aromalı geleneksel şerbet.", Price = 55.00m, Category = "İçecekler", ImageUrl = null },

                        // --- TATLILAR ---
                        new MenuItem { Title = "Fıstıklı Baklava", Description = "Gaziantep fıstıklı, çıtır kat kat baklava.", Price = 220.00m, Category = "Tatlılar", ImageUrl = null },
                        new MenuItem { Title = "Künefe", Description = "Hatay peynirli, sıcak şerbetli ve fıstıklı.", Price = 240.00m, Category = "Tatlılar", ImageUrl = null },
                        new MenuItem { Title = "Fırın Sütlaç", Description = "Üzeri kızarmış, güveçte sütlaç.", Price = 140.00m, Category = "Tatlılar", ImageUrl = null },
                        new MenuItem { Title = "Kazandibi", Description = "Yanık tabanlı, hafif sütlü tatlı.", Price = 150.00m, Category = "Tatlılar", ImageUrl = null },
                        new MenuItem { Title = "Katmer", Description = "İnce hamur, kaymak ve fıstık şöleni.", Price = 280.00m, Category = "Tatlılar", ImageUrl = null },
                        new MenuItem { Title = "İrmik Helvası", Description = "Dondurma eşliğinde sıcak irmik helvası.", Price = 160.00m, Category = "Tatlılar", ImageUrl = null }
                    );
                    context.SaveChanges();
                }

                // Admin Kullanıcısı Ekle
                if (!context.Users.Any(u => u.Role == "Admin"))
                {
                    context.Users.Add(new User
                    {
                        FullName = "Sistem Yöneticisi",
                        Username = "admin",
                        Email = "admin@proje.com",
                        Password = "123", // Gerçek hayatta hashlenmeli!
                        Role = "Admin",
                        Address = "Sistem Merkezi"
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
