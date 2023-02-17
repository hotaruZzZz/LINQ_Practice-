using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LINQ;
using System.Xml.Linq;

namespace _1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = CreateList();
            // 1. 所有商品的總價格  
            decimal total = 0; 
            foreach (var item in list)
            {
                total += item.Price * item.Quantity;
            }
            Console.WriteLine($"所有商品的總價格: {total:C}");
            Console.WriteLine("------------------------------");

            // 2. 商品的平均價格 
            decimal avg = total/list.Sum(x => x.Quantity);
            Console.WriteLine($"商品的平均價格: {avg:f2} 元");
            Console.WriteLine("------------------------------");

            // 3. 商品的總數量
            int amount = list.Sum((x) => x.Quantity);
            Console.WriteLine($"商品的總數量: {amount}");
            Console.WriteLine("------------------------------");

            // 4. 商品的平均數量
            double avg_amount = list.Average((x) => x.Quantity);
            Console.WriteLine($"商品的平均數量: {avg_amount:f2}");
            Console.WriteLine("------------------------------");

            // 5. 哪一項商品最貴
            var expensives = list.FirstOrDefault((x) => x.Price == list.Max((y) => y.Price)); 
            Console.WriteLine($"{expensives.Name} 商品最貴，{expensives.Price:C}");
            Console.WriteLine("------------------------------");


            // 6. 哪一項商品最便宜
            var cheap = list.FirstOrDefault((x) => x.Price == list.Min((y) => y.Price)); 
            Console.WriteLine($"{cheap.Name} 商品最便宜，{cheap.Price:C}");
            Console.WriteLine("------------------------------");

            // 7. 類別為 3C 的商品總價
            var t = list.GroupBy((x) => x.Type); // 使用group分類 
            decimal ccc = 0; // 3C商品總價
            decimal food_drink_tal = 0; // 飲料及食品的商品總價
            foreach (var item in t) 
            {
                
                if (item.Key == "3C")
                {
                    foreach(var p in item)
                    {
                        ccc += p.Quantity * p.Price;
                    }
                    Console.WriteLine($"\n3C的商品總價為 {ccc:C}");
                }

                if(item.Key == "食品")
                {
                    // 9. 找出所有商品類別為食品，而且商品數量大於 100 的商品
                    var food = item.Where(x => x.Quantity >= 100);
                    Console.WriteLine("\n所有商品類別為食品，而且商品數量大於 100 的商品:");
                    foreach(var p in food)
                    {
                        Console.WriteLine($"{p.Name} {p.Quantity}個");
                    }
                    Console.WriteLine();
                }
                // 8. 計算產品類別為飲料及食品的商品總價
                if(item.Key == "食品" || item.Key == "飲料")
                {
                    foreach(var i in item)
                    {
                        food_drink_tal += i.Quantity * i.Price;
                    }
                }
            }
            Console.WriteLine($"產品類別為飲料及食品的商品總價 {food_drink_tal:C}");
            // 10. 找出各個商品類別底下有哪些商品的價格是大於 1000 的商品
            foreach (var item in t)
            {
                Console.WriteLine($"{item.Key} 商品的價格是大於1000的有:");
                foreach(var p in item)
                {
                    if (p.Price > 1000) Console.WriteLine($"{p.Name} {p.Price:C}");
                }
                Console.WriteLine();
            }
            Console.WriteLine("------------------------------");

            // 11. 請計算該類別底下所有商品的平均價格 *** 呈上題，請計算該類別底下所有商品的平均價格 
            foreach (var item in t)
            {
                Console.Write($"{item.Key} 的平均價格: ");
                Console.WriteLine($"{(item.Sum(x => x.Price))} {(item.Sum(x => x.Quantity))}");
            }
            Console.WriteLine("------------------------------");

            // 12. 依照商品價格由高到低排序
            var hToL = list.OrderByDescending(x => x.Price);
            Console.WriteLine("商品價格由高到低排序:");
            foreach (var item in hToL) Console.WriteLine($"{item.Name} {item.Price:C}");
            Console.WriteLine("------------------------------");

            // 13. 依照商品數量由低到高排序
            var lToH = list.OrderBy(x => x.Quantity);
            Console.WriteLine("商品數量由低到高排序:");
            foreach (var item in lToH) Console.WriteLine($"{item.Name} {item.Quantity}個");
            Console.WriteLine("------------------------------");

            // 14. 找出各商品類別底下，最貴的商品
            Console.WriteLine("找出各商品類別底下，最貴的商品");
            foreach (var item in t)
            {
                Console.WriteLine($"\n{item.Key} 最貴的商品的有:");
                var expensive = item.Where(x => x.Price == (item.Max(y => y.Price))); //最高價錢
                foreach(var e in expensive) Console.WriteLine($"{e.Name} {e.Price:C}");
            }
            

            // 15. 找出各商品類別底下，最cheap的商品
            Console.WriteLine("------------------------------\n找出各商品類別底下，最便宜的商品");
            foreach (var item in t)
            {
                Console.WriteLine($"\n{item.Key} 最便宜的商品的有:");
                var cheaps = item.Where(x => x.Price == (item.Max(y => y.Price))); //最便宜價錢
                foreach (var c in cheaps) Console.WriteLine($"{c.Name} {c.Price:C}");
            }

            // 16. 找出價格小於等於 10000 的商品
            Console.WriteLine("------------------------------\n找出價格小於等於 10000 的商品:");
            var p10000 = list.Where(x => x.Price <= 10000);
            foreach(var p in p10000) Console.WriteLine($"{p.Name} {p.Price:C}");

            // 17. 製作一頁 4 筆總共 5 頁的分頁選擇器
            Console.WriteLine("------------------------------");
            string select;
            do
            {
                Console.Write("請輸入想看的頁數(1~5)，退出請打「e」:"); 
                select = Console.ReadLine(); //選擇想看的頁數
                for (int i = (int.Parse(select) - 1) * 4; i < (int.Parse(select)) * 4; i++) Console.WriteLine($"{list[i].Id} {list[i].Name} {list[i].Price} {list[i].Quantity} {list[i].Type}");
                
            } while (select != "e");
            Console.ReadKey();
        }

        static List<Product> CreateList()
        {
            List<Product> list = new List<Product>();
            
            var reader = new StreamReader(@"../../product.csv");
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                
                if (values[0] == "商品編號") { continue; }
                else
                {
                    list.Add(new Product { Id = values[0], Name = values[1], Quantity = int.Parse(values[2]), Price = Decimal.Parse(values[3]), Type = values[4] });
                }

            }
            return list;
        }
    }
}
