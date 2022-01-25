using System;

namespace Decorator
{
    // Elimizde temel bir nesne mevcut iken bu nesneye farklı koşullarda daha farklı anlamlar yüklemek için kullanılan pattern.
    class Program
    {
        static void Main(string[] args)
        {
            var personalCar = new PersonalCar { Make = "BMW", Model = "3.20", HirePrice = 2500 };

            SpecialOffer specialOffer = new SpecialOffer(personalCar); // CarBase ister, personalCar verirsin. Ve buna göre özel teklifler vs. gelir.
            specialOffer.DiscountPercentage = 10;


            Console.WriteLine("Concrete: {0}",personalCar.HirePrice); // personalCar normal kiralama ücreti.
            Console.WriteLine("Special offer: {0}",specialOffer.HirePrice); // personalCar tabanlı üretilen specialOffer kira ücreti

            Console.ReadLine();
        }
    }

    // Bir RentACar uygulaması yazdığımızı düşünelim.
    abstract class CarBase
    {
        public abstract string Make { get; set; }
        public abstract string Model { get; set; }
        public abstract decimal HirePrice { get; set; }

    }

    class PersonalCar : CarBase
    {
        public override string Make { get; set; }
        public override string Model { get; set; }
        public override decimal HirePrice { get; set; }
    }

    class CommercialCar : CarBase
    {
        public override string Make { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string Model { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override decimal HirePrice { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    abstract class CarDecoratorBase : CarBase
    {
        private CarBase _carBase;

        protected CarDecoratorBase(CarBase carBase)
        {
            _carBase = carBase;
        }
    }

    class SpecialOffer : CarDecoratorBase
    {
        public int DiscountPercentage { get; set; }
        private readonly CarBase _carBase; // Hangi CarBase i gönderirsek onu kullanalım diye bunu oluştururuz.

        // CarBase ile base alacağımız bir constructor a ihtiyaç duyarız:
        public SpecialOffer(CarBase carBase) : base(carBase)
        {
            _carBase = carBase;
        } 

        // CarBase property leri direk implemente edilir.
        public override string Make { get; set; }
        public override string Model { get; set; }
        // SpecialOffer için HirePrice teklifimizi göndermek için bu HirePrice set edilmelidir:
        public override decimal HirePrice {
            get { return _carBase.HirePrice - _carBase.HirePrice * DiscountPercentage / 100; } // Girilen CarBase in indirimli ücretini elde edelim.
            set { }
        }
    }

}
