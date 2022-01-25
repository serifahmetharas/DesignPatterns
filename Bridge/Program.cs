using System;

namespace Bridge
{

    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager();
            customerManager._messageSenderBase = new SMSSender();
            customerManager.UpdateCustomer();
            
            Console.ReadLine();
        }
    }

    // Soyut Base sınıfımızı oluşturalım:
    abstract class MessageSenderBase
    {
        // Bir adet sabit Save methodu ve bir adet değişken Send methodu.
        public void Save()
        {
            Console.WriteLine("Message Saved!");
        }

        public abstract void Send(Body body);
    }

    // Soyutladığımız sınıfın Concrete sınıfını oluşturalım:

    class Body
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
    
    // Birden fazla, ihtiyaç dahilinde artan Sender classlarımız olabilir. 
    // MessageSenderBase li ve Body parametreli method içeren Sender classları:
    class SMSSender : MessageSenderBase
    {
        public override void Send(Body body)
        {
            Console.WriteLine("{0} was sent via SMSSender",body.Title);
           
        }
    }

    
     
    class EMailSender : MessageSenderBase
    {
        public override void Send(Body body)
        {
            Console.WriteLine("{0} was sent via EMailSender", body.Title);
        }
    }

    //...

    class CustomerManager // Gerekli işlemleri yaptıracak class.
    {

        public MessageSenderBase _messageSenderBase;
        public void UpdateCustomer()
        {   _messageSenderBase.Send(new Body { Title= "About the course"});
            Console.WriteLine("Customer Updated!");
        }

    }
}
