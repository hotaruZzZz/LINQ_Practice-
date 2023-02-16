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
            string c1 = r.Next(0, 10).ToString();
            string c2 = r.Next(0, 10).ToString();
            while (c2 == c1) c2 = r.Next(0, 10).ToString();
            string c3 = r.Next(0, 10).ToString();
            while (c3 == c1 || c3 == c2) c3 = r.Next(0, 10).ToString();
            string c4 = r.Next(0, 10).ToString();
            while (c4 == c1 || c4 == c2 || c4 == c3) c4 = r.Next(0, 10).ToString();
            string computer = c1 + c2 + c3 + c4;
            //Console.WriteLine(computer);
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
                    for (int i = 0; i < player.Length; i++)
                    {
                        for (int j = 0; j < computer.Length; j++)
                        {
                            if (player[i] == computer[j])
                            {
                                if (i == j) a++;
                                else b++;
                            }
                        }
                    }
                    Console.WriteLine($"判定結果是 {a}A{b}B");

                } while (computer != player);
                Console.WriteLine("恭喜你！猜對了！！\r");
                Console.WriteLine("你要繼續玩嗎？(y/n): ");
            } while (Console.ReadLine() == "y");
        }
    }
}
