using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace July1
{
    class Program
    {
        static void Main(string[] args)
        {
            Service service = new Service();
            var MenuClass = new MenuClass(service.GetID, service.GetMethods());
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                MenuClass.Input(key);
            }
        }
    }
}
