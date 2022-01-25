using System;

namespace Facade
{
    
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager();
            customerManager.Save(); // İçinde Logging/Caching/Authorize işlemlerinin olduğu Interfaceleri tek class içinde newlediğimiz bir method.
                                    // Ana programda gelip direk bu şekilde kullanabiliyoruz.
            
            // Artık bu sayede yaptıracağımız işlemler arttıkça interface ekleyerek, onu da class içine tanımlayarak,
            // O class içinde istediğimiz methoduyla beraber çağırarak bu methodumuzu geliştirip kullanabiliriz.

            Console.ReadLine();
        }
    }
    
    // Normal classlarımız olduğunu düşünelim.

    class Logging : ILogging
    {
        public void Log()
        {
            Console.WriteLine("Logged!");
        }
    }

    class Caching : ICaching
    {
        public void Cache()
        {
            Console.WriteLine("Cached!");
        }
    }

    class Authorize : IAuthorize
    {
        public void CheckUser()
        {
            Console.WriteLine("User checked!");
        }
    }

    // Bunların birer interface i olmalı.

    interface ILogging
    {
        void Log();
    }

    interface ICaching
    {
        void Cache();
    }

    interface IAuthorize
    {
        void CheckUser();
    }

    // Bunları kullandığımız bir iş sınıfı olsun.
    class CustomerManager
    {
        /*Bunları bu şekilde kullanabiliriz ancak Facade Design a göre bunları ayrı bir class açıp orada new lemek daha doğrudur.
         * 
         * private ILogging _logging;
         private ICaching _caching;
         private IAuthorize _authorize;

         public CustomerManager(ILogging logging,ICaching caching, IAuthorize authorize)
         {
             _logging = logging;
             _caching = caching;
             _authorize = authorize;
         }

         *Facade e göre aşağıdaki gibi yapılır:
         */
        CrossCuttingConcernsFacade _concerns;
        public CustomerManager()
        {
            _concerns = new CrossCuttingConcernsFacade();
        }

        public void Save()
        {
            /* _logging.Log();
            _caching.Cache();
            _authorize.CheckUser();

            Üstteki yorum satırlarındaki gibi kullansaydık böyle kullanılacaktılar ancak 
            Facade Design a göre daha temiz bir koda uyarladığımızda şu şekilde kullanılır:
            */

            _concerns.Caching.Cache();
            _concerns.Authorize.CheckUser();
            _concerns.Logging.Log();
             
            Console.WriteLine("Saved!");
        }
         
    }

    class CrossCuttingConcernsFacade
    {
        public ILogging Logging;
        public ICaching Caching;
        public IAuthorize Authorize;

       
        public CrossCuttingConcernsFacade()
        {
            Logging = new Logging();
            Caching = new Caching();
            Authorize = new Authorize(); 
        }
    }


}
