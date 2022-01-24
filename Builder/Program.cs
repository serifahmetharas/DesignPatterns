using System;

namespace Builder
{
    // Ortaya bir nesne örneği çıkarmayı hedefler. Bu nesne örneği birbiri ardına atılacak adımların işlenmesi ile ortaya çıkar.
    // Genellikle iş katmanları ve arayüz katmanlarında sürekli if ler kullanmak yerine,
    // İlgili üreticinin enjekte edilmesi ve ona göre ortaya bir nesne çıkarılması şeklinde örneklendiririz.

    
    class Program
    {
        static void Main(string[] args)
        {
            ProductDirector director = new ProductDirector();
            var builder = new NewCustomerProductBuilder();
            director.GenerateProduct(builder);
            var model = builder.GetModel();
            
            // Müşteri tipine göre ürünün yapısını oluşturduk, GetModel ile getirdik ve istediğimiz şekilde kullanabiliriz.
            // If kullanımını engelleyen bir tasarım oluştu. 

            Console.WriteLine(model.Id);
            Console.WriteLine(model.CategoryName);
            Console.WriteLine(model.DiscountApplied);
            Console.WriteLine(model.DiscountedPrice);
            Console.WriteLine(model.ProductName);
            Console.WriteLine(model.UnitPrice);

            Console.ReadLine();
        }
    }

    class ProductViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public bool DiscountApplied { get; set; }
    }

    abstract class ProductBuilder
    {
        public abstract void GetProductData();
        public abstract void ApplyDiscount();
        public abstract ProductViewModel GetModel();
        
    }

    class NewCustomerProductBuilder : ProductBuilder
    {
        ProductViewModel model = new ProductViewModel();
        public override void ApplyDiscount()
        {
            model.DiscountedPrice = model.UnitPrice*(decimal)0.90;  
            model.DiscountApplied = true; // Yeni müşterilere indirim sağlama.
        }


        public override void GetProductData()
        {
            model.Id = 1;
            model.CategoryName = "Beverages";
            model.ProductName = "Chai";
            model.UnitPrice = 20;

        }
        public override ProductViewModel GetModel()
        {
            return model; // Ürettiğimiz modeli döndüren method.
        }
    }

    class OldCustomerProductBuilder : ProductBuilder
    {
        ProductViewModel model = new ProductViewModel();
        public override void ApplyDiscount()
        {
            model.DiscountedPrice= model.UnitPrice;
            model.DiscountApplied = false;
        }

        public override void GetProductData()
        {
            model.Id = 1;
            model.CategoryName = "Beverages";
            model.ProductName = "Chai";
            model.UnitPrice = 20;

        }
        public override ProductViewModel GetModel()
        {
            return model; // Ürettiğimiz modeli döndüren method.
        }
    }

    class ProductDirector
    {
        public void GenerateProduct(ProductBuilder productBuilder)
        {
            productBuilder.GetProductData();
            productBuilder.ApplyDiscount();

        }
    }

}
