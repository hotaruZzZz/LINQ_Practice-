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
            // 所有商品的總價格  ***
            decimal total = 0; 
            foreach (var item in list)
            {
                total += item.Price * item.Quantity;
            }
            Console.WriteLine($"所有商品的總價格: {total}");
            // 商品的平均價格 ***
            decimal avg = list.Sum(x => x.Price)/list.Sum(x => x.Quantity);
            Console.WriteLine($"商品的平均價格: {avg:f2}"); 
            int amount = list.Sum((x) => x.Quantity);
            Console.WriteLine($"商品的總數量: {avg:f2}"); // 商品的總數量
            double avg_amount = list.Average((x) => x.Quantity);
            Console.WriteLine($"商品的平均數量: {avg_amount:f2}"); // 商品的平均數量
            var expensives = list.FirstOrDefault((x) => x.Price == list.Max((y) => y.Price)); // 哪一項商品最貴
            Console.WriteLine($"{expensives.Name} 商品最貴，{expensives.Price}元"); 
            var cheap = list.FirstOrDefault((x) => x.Price == list.Min((y) => y.Price)); //哪一項商品最便宜
            Console.WriteLine($"{cheap.Name} 商品最便宜，{cheap.Price}元");
            // 類別為 3C 的商品總價
            var t = list.GroupBy((x) => x.Type);
            decimal ccc = 0;
            
            foreach(var item in t) 
            {
                if (item.Key == "3C")
                {
                    foreach(var p in item)
                    {
                        ccc += p.Quantity * p.Price;
                    }
                    Console.WriteLine($"\n3C的商品總價為 {ccc} 元");
                }
                if(item.Key == "食品")
                {
                    var food = item.Where(x => x.Quantity >= 100);
                    Console.WriteLine("\n所有商品類別為食品，而且商品數量大於 100 的商品:");
                    foreach(var p in food)
                    {
                        Console.WriteLine($"{p.Name} {p.Quantity}個");
                    }
                    
                }
            }
            
            // 8?計算產品類別為飲料及食品的商品價格

            //找出所有商品類別為食品，而且商品數量大於 100 的商品

            //找出各個商品類別底下有哪些商品的價格是大於 1000 的商品


            //請計算該類別底下所有商品的平均價格

            //依照商品價格由高到低排序

            //依照商品數量由低到高排序

            //找出各商品類別底下，最貴的商品

            // 找出各商品類別底下，最cheap的商品

            //找出價格小於等於 10000 的商品

            //製作一頁 4 筆總共 5 頁的分頁選擇器

            Console.ReadKey();
        }
        static List<Product> CreateList()
        {
            List<Product> list = new List<Product>();
            
            var reader = new StreamReader(File.OpenRead(@"../../product.csv"));
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
