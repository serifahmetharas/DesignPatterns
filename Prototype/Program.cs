using System;

namespace Prototype
{
    // Prototype desing da amaç nesne üretim maliyetlerini minimize etmektir.
    // İhtiyaçlar dahilinde kullanılır.

    class Program
    {
        static void Main(string[] args)
        {
            Customer customer1 = new Customer { FirstName ="Serif",LastName="Haras",City="İzmir",Id=1};
        

            Customer customer2 = (Customer)customer1.Clone();
            customer2.FirstName = "Afra";   // Sıradaki müşterilerimizi Clone kullanarak direk girebiliyoruz.

            Console.WriteLine(customer1.FirstName);
            Console.WriteLine(customer2.FirstName); // Görüldüğü gibi müşteri özelliğini kaybetmeden ayrı bir nesne olarak 2.müşterimiz de oluşturuldu.
                                                    // Yeni bir nesne ama new leme maliyetinden kurtuldu.
                                                    // Kaynağın ortak kullanımı gibi görülebilir.

            Console.ReadLine();
        }
    }

    //  Müşteri takip sistemi veritabanındaki Person temel nesnesi üzerinden gidelim. (Protoype temel nesne üzerinden yapılır.)

    public abstract class Person // Temel nesnemiz.
    {
        // Temel nesnemizi bir prototype haline getirebilmek için soyut bir Clone methodu ile besleyelim.
        public abstract Person Clone(); // Person döndürür.

        // Propertyler verilir.
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

    public class Customer : Person
    {
        // Person implementasyonu yapılır. Yani bu sayede elimizdeki Customer ı bir Person olarak Clone larız.
        // Bu olay bizim new leme maliyetlerimizi azaltır.
        public override Person Clone()
        {
            return (Person) MemberwiseClone();
        }
        
        // Sadece Customerda bulunan özellikler de bulunabilir:

        public string City { get; set; }
        
    }

    // Customer dışında Employee gibi başka nesnelerimiz de olabilir. 
    public class Employee : Person
    {
        public override Person Clone()
        {
            return (Person)MemberwiseClone();
        }

        public decimal Salary { get; set; } // Sadece Employee e ait bir property.
    }
}
