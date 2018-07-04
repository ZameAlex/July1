using July1.IO;
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
            var service = new IOService();
            var MenuClass = new MenuClass(service.GetID, service.GetMethods());
            MenuClass.SetDisplayNames(service.GetNames());
            MenuClass.Show();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                MenuClass.Input(key);
            }
        }
    }
}
