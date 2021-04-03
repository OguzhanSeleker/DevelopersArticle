using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopersArticle.BLL.Utilities.Constants
{
    public static class Messages
    {
        public static string Error = "Bir hata ile karşılaşıldı.";
        public static string SucceededArticle = "Makale Ekleme İşlemi Başarılı";
        public static string UnsucceededArticle = "Makale Ekleme İşlemi Başarısız!";
        public static string UploadImageError = "Yüklenen dosya resim dosyası olmalı ve boyutu 200kb'tan az olmalıdır.";
        public static string NotListedCategories = "Kategoriler listenemedi. Lütfen sayfayı yenileyin";
        public static string NotListedDevelopers = "Yazarlar listenemedi. Lütefen sayfayı yenileyin";
        public static string SucceededCategory = "Kategori Ekleme İşlemi Başarılı";
        public static string UnSucceededCategory = "Kategori Ekleme İşlemi Başarısız";
        public static string ErrorDevelopersFullname = "Yazar isimleri listenemedi";
        public static string ErrorArticlesInCategory = "Yazılar listenemedi";
        public static string SuccessUpdateCategory = "Kategori başarılı bir şekilde güncellendi.";
        public static string ErrorUpdateCategory = "Kategori güncellenemedi.";
        public static string ErrorDeleteCategory = "Kategori Silinemedi. Çünkü kategoride yazan veya yazı bulunmakta.";
        public static string SuccessDeleteCategory = "Kategori başarılı bir şekilde silindi.";
        public static string SuccessAddDeveloper = "Yazar başarılı bir şekilde eklendi.";
        public static string ErrorAddDeveloper = "Yazar ekleme başarısız.";
        public static string ErrorGetCategories = "Yazar kategori bilgisi alınamadı.";
        public static string ErrorGetArticles = "Yazar yazı bilgisi alınamadı.";
        public static string SuccessUpdateDeveloper = "Yazar başarılı bir şekilde güncellendi.";
        public static string ErrorUpdateDeveloper = "Yazar bilgileri güncellenirken bir hata ile karşılaşıldı.";
        public static string SuccessDeleteDeveloper = "Yazar başarılı bir şekilde silindi.";
        public static string ErrorDeleteDeveloper = "Yazar silinemedi.";
        public static string ErrorDeleteDeveloperDueToArticles = "Yazarın yazısı olduğu için silinemedi.";
        public static string ErrorGetAllArticles = "Yazılar Listelenemdi.";
        public static string ErrorGetArticle = "Yazı getirilirken bir sıkıntı ile karşılaşıldı.";
        public static string ErrorAddComment = "Yorum eklenemedi.";
        public static string SuccessDeleteArticle = "Yazı Başarılı bir şekilde silindi.";
        public static string ErrorDeleteArticle = "Yazı Silinirken sorun yaşandı. Hata : ";
        public static string CheckNullId = "Yazının Id'si bulunamadı!";
        public static string EmptyCatArt = "Kategoriye ait yazı bulunmamaktadır.";
        public static string SuccessUpdateArticle = "Yazı başarılı bir şekilde güncellendi.";
        public static string ErrorUpdateArticle = "Yazı güncellenemedi. Hata : ";
        public static string ErrorDeleteComment = "Yorum silinemdi. Hata : ";
        public static string SuccessUpdateComment = "Yorum Başarılı bir şekilde güncellendi. ";
        public static string ErrorUpdateComment = "Yorum güncellenemedi. Hata : ";
    }
}
