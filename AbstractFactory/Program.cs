using System;

namespace AbstractFactory
{
    // Factory Method Design Pattern ine ek olarak
    // Toplu nesne kullanımı ihtiyaçlarında hem nesne kullanımını kolaylaştırmak hem de standart nesneler için bir mantık oluşturmak amaçlanır.

    // Örneğin bir iş katmanında loglama caching vb. çeşitli teknikler kullanıyoruz. Bunların da farklı farklı yöntemleri var.
    // Diyelim ki bir emlak takip uygulamasında bazı kullanıcılara mail göndermek bazılarına ise mesaj göndermek istiyoruz.

    // Unutmamalıyız ki somut bir nesne mutlaka bir inheritance veya bir implementasyon görmelidir. 

    class Program
    {
        static void Main(string[] arg)
        {
            ProductManager productManager = new ProductManager(new Factory2());
            productManager.GetAll();
            


            Console.ReadLine();
        }
    }

    public abstract class Logging
    {
        public abstract void Log(string message);
    }

    public class Log4NetLogger : Logging
    {
        public override void Log(string message)
        {
            Console.WriteLine("Logged with log4net.");
        }
    }

    public class NLogger : Logging
    {
        public override void Log(string message)
        {
            Console.WriteLine("Logged with nlogger.");
        }
    }

    public abstract class Caching
    {
        public abstract void Cache(string data);

    }

    public class MemCache : Caching
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cached with MemCache.");
        }
    }

    public class RedisCache : Caching
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cached with Rediscache.");
        }
    }

    // Bu sayede ileride yeni bir loglama yada caching yöntemi eklemek mümkündür.

    // Bunları üreten fabrikaları kuralım.

    public abstract class CrossCuttingConcernsFactory // Bu bir abstract class, fabrikalar da artabilir.
    {
        public abstract Logging CreateLogger();
        public abstract Caching CreateCaching();
    }

    public class Factory1 : CrossCuttingConcernsFactory
    {
        public override Caching CreateCaching()
        {
            return new RedisCache(); // İstediğimiz loglamayı kullanabiliriz.
        }

        public override Logging CreateLogger()
        {
            return new NLogger(); // İstediğimiz Caching i kullanabiliriz.
        }
    }

    public class Factory2 : CrossCuttingConcernsFactory
    {
        public override Caching CreateCaching()
        {
            return new MemCache(); // İstediğimiz loglamayı kullanabiliriz.
        }

        public override Logging CreateLogger()
        {
            return new Log4NetLogger(); // İstediğimiz Caching i kullanabiliriz.
        }
    }

    // Buradaki farklı factory ler bizim iş sürecimiz.
    // Sürekli if kalıpları ile işleri belirtmektense farklı fabrikalar ile farklı işlemler yaptırabiliyoruz.

    public class ProductManager
    {

        private CrossCuttingConcernsFactory _crossCuttingConcernsFactory;
        
        private Logging _logging;
        private Caching _caching;
        
        public ProductManager(CrossCuttingConcernsFactory crossCuttingConcernsFactory)
        {
            _crossCuttingConcernsFactory = crossCuttingConcernsFactory;
            _logging = _crossCuttingConcernsFactory.CreateLogger();
            _caching = _crossCuttingConcernsFactory.CreateCaching();
        }

        public void GetAll()
        {
            _logging.Log("Logged");
            _caching.Cache("Data");
            Console.WriteLine("Products listed!");
            

        }
    }

}
