using System;

namespace FactoryMethod

// Amaç yazılımda değişimi kontrol altında tutmaktır.

{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager(new LoggerFactory2()); // Hangi factory ile çalışılacaksa burada çağırılıyor.
            customerManager.Save();

            Console.ReadLine();

        }
    }

    // Bir factory (fabrika) üretelim. 

    public class LoggerFactory : ILoggerFactory // Bir class asla çıplak kalmamalı.
    {
        public ILogger CreateLogger() // Logger üretir. Böylece CustomerManager da tek bir logger a bağlı kalmadan newlememizi Logger üreterek gerçekleştirebiiriz.
        {
            // Business to decide factory 
            return new ShLogger();
        }
    }

    // Ancak belki birden fazla Factory ile çalışacak da olabiliriz.
    public class LoggerFactory2 : ILoggerFactory
    {
        public ILogger CreateLogger()
        {
            //  Burası da başka bir mantıkla çalışıyor olabilir.
            return new LogfNetLogger();
        }
    }

    // İhtiyaç duyulan interface ler

    public interface ILoggerFactory
    {
        public ILogger CreateLogger(); // Birden fazla fabrika kullandığında implemente etmek yeterli olacak böylece.
    }

    public interface ILogger
    {
        void Log();
    }

    // Bir Logger ımız olduğunu düşünelim.

    public class ShLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged with ShLogger!");
        }
    }

    public class LogfNetLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged with LogfNetLogger!");
        }
    }

    public class CustomerManager
    {
        private ILoggerFactory _loggerFactory;

        public CustomerManager(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public void Save()
        {
            Console.WriteLine("Saved!");
            // ILogger logger = new LoggerFactory().CreateLogger(); // Tek factory ile çalışsaydık böyle de yazabilirdik,
                                                                    // Ancak genelde birden fazla factory ile çalışacağımız için aşağıdaki kod tercih edilir.
            
            ILogger logger = _loggerFactory.CreateLogger(); // Yukarıdaki CustomerManager class ındaki constructor sayesinde:
                                                            // Farklı factorylere bu şekilde bağımsız olarak erişebileceğimiz bir ortam oluşturmuş olduk. 
                                                            // Ana programda CustomerManager çağırırken hangi factory kullanacağımızı bizden isteyecek.
                                                            // Ve ona göre loglama işlemi gerçekleşecek.
            
            logger.Log(); // Shlogger loglaması gerçekleşti. 
        }
    }

}
