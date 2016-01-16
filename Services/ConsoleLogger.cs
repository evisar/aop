using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.Services
{
    public class ConsoleLogger: ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void Exception(Exception ex)
        {
            while(ex!=null)
            {
                Console.WriteLine(ex.Message);
                ex = ex.InnerException;
            }
        }
    }
}
