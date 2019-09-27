using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Data.Common
{
    public class RandomNumber
    {
        public static object _lock = new object();
        public static int count = 1;

        public string GetRandom1()
        {
            lock (_lock)
            {
                if (count >= 10000)
                {
                    count = 1;
                }
                var number = "P" + DateTime.Now.ToString("yyMMddHHmmss") + count.ToString("0000");
                count++;
                return number;
            }
        }


        public string GetRandom2()
        {
            lock (_lock)
            {
                return "T" + DateTime.Now.Ticks;

            }
        }

        public string GetRandom3()
        {
            lock (_lock)
            {
                Random ran = new Random();
                return "U" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ran.Next(1000, 9999).ToString();
            }
        }
    }
}
