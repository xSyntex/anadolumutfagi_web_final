using System;
using System.ComponentModel.DataAnnotations;

namespace Web_Programlama_Proje.Models
{
    // Menü öğesini temsil eden sınıf
    public class MenuItem
    {
        // Benzersiz kimlik numarası (Primary Key)
        public int Id { get; set; }

        // Yemek adı (Zorunlu alan)
        [Required(ErrorMessage = "Yemek adı zorunludur.")]
        [Display(Name = "Yemek Adı")]
        public string Title { get; set; } = string.Empty;

        // Yemek açıklaması
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        // Fiyat bilgisi
        [Required(ErrorMessage = "Fiyat zorunludur.")]
        [Display(Name = "Fiyat")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        // Yemek resmi URL'si
        [Display(Name = "Resim URL")]
        public string? ImageUrl { get; set; }

        // Kategori (Başlangıç, Ana Yemek, Tatlı vb.)
        [Display(Name = "Kategori")]
        public string? Category { get; set; }

        // Oluşturulma tarihi
        [Display(Name = "Eklenme Tarihi")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
