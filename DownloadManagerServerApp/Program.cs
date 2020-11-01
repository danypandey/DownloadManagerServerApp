using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadManagerServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Service service = new Service();

            Uri baseAddress = new Uri("http://127.0.0.1:8001");

            service.Start(baseAddress);

            Console.ReadKey();
        }
    }
}
