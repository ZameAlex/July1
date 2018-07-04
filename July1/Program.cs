using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace July1
{
    class Program
    {
        static void Main(string[] args)
        {
            Service service = new Service();
            service.CreateHierarhy();
            Console.ReadKey();
        }
    }
}
