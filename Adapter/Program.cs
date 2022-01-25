using System;

namespace Adapter
{
    // Farklı sistemlerin kendi sistemlerimize entegre edilmesi durumunda,
    // Kendi sistemimizin bozulmadan diğer sistemin sanki bizim sistemimizmiş gibi davranmasını sağlamak.

    class Program
    {
        static void Main(string[] args)
        {
            // Adapter sayesinde farklı yerlerden getirdiğimiz sistemleri direk kendi sistemlerimize entegre edip,
            // Ekstra değişiklikler yapmadan sadece adını çağırarak kullanabiliyoruz.
            ProductManager productManager = new ProductManager(new Log4NetAdapter()); // ShLogger yada Log4NetAdapter arasında seçimi yazıp direk kullanacağız.
            productManager.Save();

            Console.ReadLine();

            
        }
    }

    class ProductManager
    {
        private ILogger _logger;

        public ProductManager(ILogger logger)
        {
            _logger = logger;
        }

        public void Save()
        {
            _logger.Log("User Data");
            Console.WriteLine("Saved!");
        }
    }

    interface ILogger
    {
        void Log(string message);
    }

    class ShLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("Logged with ShLogger, {0}", message);
        }
    }

    // Yeni bir loglama yöntemi kullanılmak istendiğinde,
    // Bu Log4Net in Nuget üzerinden indirdiğimizi varsayalım.
    class Log4Net
    {
        public void LogMessage(string message)
        {
            Console.WriteLine("Logged with Log4Net, {0}",message);
        }
    }

    // ProductManager da Log4Net i nasıl çağırırız?
    // Log4Net bir dll ve değişiklik yapamıyoruz, Nuget üzerinden indirdik.
    // O zaman devreye Adapter Patter Desing meydana girer.

    class Log4NetAdapter : ILogger
    {
        public void Log(string message)
        {
            Log4Net log4net = new Log4Net();
            log4net.LogMessage(message);
        }
    }


}
