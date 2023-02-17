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
            foreach (var item in list) total += item.Price * item.Quantity;
            Console.WriteLine($"所有商品的總價格: {total:C}");
            Console.WriteLine("------------------------------");

            // 2. 商品的平均價格 
            Console.WriteLine($"商品的平均價格: {total / list.Sum(x => x.Quantity):f2} 元");
            Console.WriteLine("------------------------------");

            // 3. 商品的總數量
            Console.WriteLine($"商品的總數量: {list.Sum((x) => x.Quantity)} 個");
            Console.WriteLine("------------------------------");

            // 4. 商品的平均數量
            Console.WriteLine($"商品的平均數量: {list.Average((x) => x.Quantity):f2} 個");
            Console.WriteLine("------------------------------");

            // 5. 哪一項商品最貴
            Console.WriteLine($"{list.FirstOrDefault((x) => x.Price == list.Max((y) => y.Price)).Name} " +
                $"商品最貴，{list.FirstOrDefault((x) => x.Price == list.Max((y) => y.Price)).Price:C}");
            Console.WriteLine("------------------------------");


            // 6. 哪一項商品最便宜
            var cheap = list.FirstOrDefault((x) => x.Price == list.Min((y) => y.Price)); 
            Console.WriteLine($"{cheap.Name} 商品最便宜，{cheap.Price:C}");
            Console.WriteLine("------------------------------");


            var food = list.Where(x => x.Type == "食物");  // 食物
            var drink = list.Where(x => x.Type == "飲料"); // 飲料
            decimal food_drink_tal = food.Sum(x => x.Price) + drink.Sum(x => x.Price); // 飲料及食品的商品總價

            // 7. 類別為 3C 的商品總價 ***
            Console.WriteLine($"3C 的商品總價為: {list.Where(x => x.Type == "3C").Sum(x => x.Price):C}");
            Console.WriteLine("------------------------------");

            // 8. 計算產品類別為飲料及食品的商品總價 ***
            Console.WriteLine($"產品類別為飲料及食品的商品總價為: {food_drink_tal:C}");
            Console.WriteLine("------------------------------");

            // 9. 找出所有商品類別為食品，而且商品數量大於 100 的商品
            Console.WriteLine("商品類別為食品，而且商品數量大於 100 的商品:");
            foreach(var f in list.Where(x => x.Type == "食物" || x.Quantity > 100)) Console.WriteLine($"{f.Name} {f.Quantity}個");
            Console.WriteLine("------------------------------");

            // 10. 找出各個商品類別底下有哪些商品的價格是大於 1000 的商品
            foreach (var item in list.Where(x => x.Price > 1000).GroupBy(x => x.Type))
            {
                Console.WriteLine($"{item.Key} 商品的價格是大於1000的有:");
                foreach(var p in item) Console.WriteLine($"{p.Name} {p.Price:C}");
                // 11. 呈上題，請計算該類別底下所有商品的平均價格
                Console.WriteLine($"平均價格 {item.Average(x => x.Price)}");
            }
            Console.WriteLine("------------------------------");

            // 12. 依照商品價格由高到低排序
            Console.WriteLine("商品價格由高到低排序:");
            foreach (var item in list.OrderByDescending(x => x.Price)) Console.WriteLine($"{item.Name} {item.Price:C}");
            Console.WriteLine("------------------------------");

            // 13. 依照商品數量由低到高排序
            Console.WriteLine("商品數量由低到高排序:");
            foreach (var item in list.OrderBy(x => x.Quantity)) Console.WriteLine($"{item.Name} {item.Quantity}個");
            Console.WriteLine("----------------------------------------------------------");

            // 14. 找出各商品類別底下，最貴的商品
            Console.WriteLine("找出各商品類別底下，最貴的商品");
            foreach (var item in list.GroupBy(x => x.Type))
            {
                Console.WriteLine($"\n{item.Key} 最貴的商品的有:");
                var expensive = item.Where(x => x.Price == (item.Max(y => y.Price))); //最高價錢
                foreach(var e in expensive) Console.WriteLine($"{e.Name} {e.Price:C}");
            }
            

            // 15. 找出各商品類別底下，最cheap的商品
            Console.WriteLine("------------------------------\n找出各商品類別底下，最便宜的商品");
            foreach (var item in list.GroupBy(x => x.Type))
            {
                Console.WriteLine($"\n{item.Key} 最便宜的商品的有:");//最便宜價錢
                foreach (var c in item.Where(x => x.Price == (item.Max(y => y.Price)))) Console.WriteLine($"{c.Name} {c.Price:C}");
            }

            // 16. 找出價格小於等於 10000 的商品
            Console.WriteLine("------------------------------\n找出價格小於等於 10000 的商品:");
            foreach(var p in list.Where(x => x.Price <= 10000)) Console.WriteLine($"{p.Name} {p.Price:C}");

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
