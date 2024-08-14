using KGA_OOPConsoleProject._03.Item.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject._03.Item.Inventory
{
    public class Inventory
    {
        List<Items> invens;

        public Inventory(int num = 10)
        {
            invens = new List<Items>(num);
        }

        public void Inven_LookAt(int Gold)
        {
            if (invens.Count() <= 0)
            {
                Console.WriteLine("인벤토리 창이 비어 있습니다.");
                Thread.Sleep(500);
                return;
            }
            int i = 0;

            Console.WriteLine($"현재 인벤토리 상태\t현재 소지금 : {Gold,-8}G");
            Console.WriteLine("================================================");
            Console.WriteLine();
            foreach (var item in invens)
            {
                Console.WriteLine($"{i + 1}. {item}");
                i++;
            }
            Console.WriteLine();
        }

        public void Item_PickUp(Items item)
        {
            if (invens.Count < invens.Capacity)
            {
                invens.Add(item);
                Console.WriteLine($"{item.name}을/를 주었습니다.");
                Console.WriteLine($"{item.info}");
                Thread.Sleep(500);
            }
            else
            {
                Console.WriteLine("인벤토리의 공간이 가득 찼습니다.");
                Thread.Sleep(500);
            }
        }

        public void Item_Use(int num)
        {
            if(invens[num - 1].name != "Jumpper")
                invens[num-1].Use();
        }
        
    }
}
