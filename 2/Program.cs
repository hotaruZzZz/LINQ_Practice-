using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2
{
    internal class Program
    {
        public static string guess()
        {
            Random r = new Random();
            var guess = Enumerable.Range(0,9).OrderBy(i => r.Next()).Take(4);
            string computer = "";
            foreach(var i in guess) computer += i;
            Console.WriteLine(computer);
            return computer;
        }
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("歡迎來到 1A2B 猜數字的遊戲～\r\n");
                string computer = guess();
                string player = "";
                int a = 0;
                int b = 0;
                //Console.WriteLine(computer);
                do
                {
                    a = 0;
                    b = 0;
                    Console.Write("輸入 4 個數字： ");
                    player = Console.ReadLine();
                    var l = computer.Intersect(player);

                    foreach (var v in l)
                    {
                        if (computer.IndexOf(v) == player.IndexOf(v)) a++;
                        else b++;
                    }
                    Console.WriteLine($"判定結果是 {a}A{b}B");

                } while (computer != player);
                Console.WriteLine("恭喜你！猜對了！！\r");
                Console.WriteLine("你要繼續玩嗎？(y/n): ");
            } while (Console.ReadLine() == "y");
        }
    }
}
