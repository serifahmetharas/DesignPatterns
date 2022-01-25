using System;
using System.Threading;

namespace Proxy
{
    // Bir sınıftaki işlem ikinci kez çağırılmışsa o işlemi hızlandırma, iyileştirme işlemidir.
    class Program
    {
        static void Main(string[] args)
        {
            /* 
            CreditManager manager = new CreditManager();
            Console.WriteLine(manager.Calculate());
            Console.WriteLine(manager.Calculate()); // Burada ikinci kez yapılırken de 5 sn bekler. Ancak proxy kullanırsak direk çalışır.

            Proxy design kullanılmışsa bunların yerine Proxy çağırılarak işlem yapılır.
            */

            CreditBase manager = new CreditManagerProxy();
            Console.WriteLine(manager.Calculate());
            Console.WriteLine(manager.Calculate());

            // Bu Proxy sayesinde ikinci işlem direk gelir, gereksiz beklemeden kurtarır.

            Console.ReadLine();
        }
    }

    abstract class CreditBase
    {
        public abstract int Calculate();
    }

    class CreditManager : CreditBase
    {
        public override int Calculate()
        {
            int result = 1;
            for (int i = 1; i < 5; i++)
            {
                result *= i;
                Thread.Sleep(1000); // Her operasyon arasında 1 sn bekleyelim.
            }

            return result;
        }
    }

    class CreditManagerProxy : CreditBase
    {
        private CreditManager _creditManager;
        private int _cachedValue;
        public override int Calculate()
        {
            if (_creditManager == null)
            {
                _creditManager = new CreditManager();
                _cachedValue = _creditManager.Calculate();
            }

            return _cachedValue;
        }
    }
}
