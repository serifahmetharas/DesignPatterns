using System;

namespace Singleton
{
    // Singleton Design Pattern bir nesne örneğinin sadece bir kere üretilip her zaman kullanılmasını öngören bir patterndir.
    // Nesne örneğininin kullanıcı değişse de aynı şekilde kullanılabilmesini amaçlar.
    
    // Katmanlar arası geçişlerde sadece işlem yapan, bir değer tutmayan nesnelerde bu patternden yararlanmakta fayda vardır.
    // İş katmanındaki Manager nesnesini düşünelim, ekleme silme güncelleme listeleme gibi işlemler yapar ve bunu sadece bir method şeklinde yapar.
    // Her kullanıcı bu işlemleri yaparken ayrı ayrı newlemek zorunda kalmasın, sisteme yük olmasın diye bu pattern design tercih edilir.

  

    class Program
    {
        static void Main(string[] args)
        {
            // Artık CustomerManager nesnesi hiçbir zaman normal olarak new lenemez.
            // Singleton olarak new leme işlemini gerçekleştirelim.
            // Artık bu nesnenin sadece bir kere new lenmiş olacağından emin olabiliriz.
            var customerManager = CustomerManager.CreateAsSingleton();

            // New leme işlemi tamamlandıktan sonra da dilediğimiz gibi class içindeki methodlarımızı çağırarak programımızı yazabiliriz.
            customerManager.Save();


        }
    }

    /// Singleton Pattern design öğrenmek için kullanımını gerçekleştirelim ancak aslında gerçekte sürekli yeni singleton üretecek classlar yaratmayız. 
    /// Onun yerine Factory Design Pattern ile ortak bir çalışma gerçekleştirerek direkt o nesne üzerinden singleton üretme işlemlerini yapabiliriz.

    // Bir CustomerManager classı oluşturalım.
    class CustomerManager
    {
        // Sadece add-update-delete gibi işlemler yapacağı için singleton yapılabilir bir hale getirmek istiyoruz.

        // Önce bir private constructor oluştururuz. (Yani constructor ı var ama dışarıdan erişilemiyor.)

        private static CustomerManager _customerManager;
        private CustomerManager() // private constructor ımız.
        {

        }

        // Singleton örneğini oluşturacağımız classın kendisini döndürecek bir static bir method yazarız. 
        // Bu methodun içinde CustomerManager daha önce oluşturulmuşsa oluşturulmuşu vermesini oluşturulmamışsa oluşturup öyle vermesini sağlayacağız.
        // Bu yüzden yöneteceğimiz static bir nesneye ihtiyacımız var. (Yukarıda private static CustomerManager _CostumerManager nesnesi bu yüzden üretildi.)
        // Daha sonra methodta bunun sorgulamasını yaptırız ve ona göre new leme işlemini gerçekleştir. Böylece gereksiz new leme işlemlerinden kurtulmuş oluruz.
        public static CustomerManager CreateAsSingleton()
        {

            return _customerManager ?? (_customerManager = new CustomerManager()); // _customerManager yani bir CustomerManager new lenmiş mi?
                                                                                   // Newlenmişse onu kullan, değilse de new le.

        }

        public void Save()
        {
            Console.WriteLine("Saved!");
        }
    }
} 
