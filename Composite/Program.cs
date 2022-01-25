using System;
using System.Collections;
using System.Collections.Generic;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            // Şirketin çalışanlarını ekleyelim.
            Employee serif = new Employee { Name = "Serif Haras" };
            Employee afra = new Employee { Name = "Afra İldes" };

            //Hiyerarşiyi oluşturmaya başlayalım.

            serif.AddSubordinate(afra); // Şerif için bir alt çalışan ekle, o da Afra

            // Bir çalışan daha ekleyelim.

            Employee alper = new Employee { Name = "Alper Inbat" };

            serif.AddSubordinate(alper); // Şerif için yeni bir alt çalışan eklendi, Alper.

            Employee onur = new Employee { Name = "Onur Kahraman" };

            afra.AddSubordinate(onur); // Onur da Afra'nın bir alt çalışanı.

            // Hiyerarşiyi yazdırmak için:
            Console.WriteLine(serif.Name);
            foreach(Employee manager
                in serif)
            {
                Console.WriteLine("  {0}",manager.Name); // Afra ve Alper i verir.
                foreach (var employee in manager) // Alt hiyerarşileri de vermesi için.
                {
                    Console.WriteLine("    {0}",employee.Name) ;
                }
            
            } // Şerif, Serif in alt çalışanı Afra ve Alper, Afranın da alt çalışanı Onur şeklinde hiyerarşiyi verecektir. 



        }
    }

    interface IPerson
    {
        string Name { get; set; }
    }

    class Employee : IPerson, IEnumerable<IPerson> // Employee bir IPersondır ve bunda foreach le gezebileceğimiz bir ortam oluşturduk.
    {
        // Hiyerarşiyi yazalım.
        List<IPerson> _subordinates = new List<IPerson>(); // subortinates: hiyerarşik yapının altı.

        // Hiyerarşiye ekleme ve çıkarma methodlarını oluşturalım.
        public void AddSubordinate(IPerson person)
        {
            _subordinates.Add(person); // Referans tip methodları yardımıyla ekleme çıkarma işlemlerini gerçekleştirebiliyoruz.
        }

        public void RemoveSubordinate(IPerson person)
        {
            _subordinates.Remove(person); // Referans tip methodları yardımıyla ekleme çıkarma işlemlerini gerçekleştirebiliyoruz.
        }

        public IPerson GetSubordinate(int index) // Subordinates in index inci elemanını bize veren methodumuz.
        {
            return _subordinates[index];
        }
        public string Name { get; set; }

        public IEnumerator<IPerson> GetEnumerator()
        {
            foreach (var subordinate in _subordinates)
            {
                yield return subordinate;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
