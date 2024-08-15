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
        Maze maze = Maze.Instance;

        List<Items> invens;

        public Inventory(int num = 10)
        {
            invens = new List<Items>(num);
        }

        public void Inven_LookAt()
        {
            if (invens.Count() == 0)
            {
                Console.WriteLine("인벤토리 창이 비어 있습니다.");
                Thread.Sleep(500);
                return;
            }
            int i = 0;

            Console.WriteLine($"<현재 인벤토리 상태>");
            Console.WriteLine("================================================");
            Console.WriteLine();
            foreach (var item in invens)
            {
                Console.WriteLine($"{i + 1}. {item.name,-8} \t효과 : {item.info}");
                i++;
            }
            Console.WriteLine();
        }

        public void Item_PickUp(Items item)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{item.name}을/를 발견했습니다");
                Console.WriteLine($"아이템을 주우시려면 P를 눌러주세요");
                Console.WriteLine("================================================");
                Inven_LookAt();
                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.P)
                {
                    if (invens.Count < invens.Capacity)
                    {
                        invens.Add(item);
                        Console.WriteLine($"{item.name}을/를 주었습니다.");
                        Console.WriteLine($"{item.info}");
                        Thread.Sleep(500);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("인벤토리의 공간이 가득 찼습니다.");
                        Thread.Sleep(500);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("다시 입력해주세요");
                    Thread.Sleep(1000);
                }
            }
        }

        public void Item_Use(int num, Player player)
        {
            if (invens[num - 1].name == "Potion")
            {
                player.hp = invens[num - 1].Use(player.hp);
                Console.WriteLine($"{invens[num - 1].name}을/를 사용합니다.");
                Console.WriteLine($"{invens[num - 1].effect}만큼 이동횟수가 찼습니다.");

                invens.RemoveAt(num - 1);
            }
            else if (invens[num - 1].name == "MapReSearch")
            {
                invens[num - 1].Use();
                Console.WriteLine($"{invens[num - 1].name}을/를 사용합니다.");
                invens.RemoveAt(num - 1);
            }
            else if (invens[num - 1].name == "Jump")
            {
                Console.WriteLine("이동할 곳을 선택하세요");
                Console.WriteLine("-------------------------");
                Console.WriteLine($"1. 위쪽");
                Console.WriteLine($"2. 아래쪽");
                Console.WriteLine($"3. 왼쪽");
                Console.WriteLine($"4. 오른쪽");
                Console.WriteLine("-------------------------");
                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        if (maze.search(player.pos.Item1 - 2, player.pos.Item2))
                        {
                            player.pos = (player.pos.Item1 - invens[num - 1].Use(), player.pos.Item2);
                            Console.WriteLine("위로 점프!");
                            invens.RemoveAt(num - 1);
                        }
                        else
                        {
                            Console.WriteLine("이동 불가 합니다.");
                        }
                        break;
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        if (maze.search(player.pos.Item1 + 2, player.pos.Item2))
                        {
                            player.pos = (player.pos.Item1 + invens[num - 1].Use(), player.pos.Item2);
                            Console.WriteLine("아래로 점프!");
                            invens.RemoveAt(num - 1);
                        }
                        else
                        {
                            Console.WriteLine("이동 불가 합니다.");
                        }
                        break;
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.D3:
                        if (maze.search(player.pos.Item1, player.pos.Item2 - 2))
                        {
                            player.pos = (player.pos.Item1, player.pos.Item2 - invens[num - 1].Use());
                            Console.WriteLine("왼쪽으로 점프!");
                            invens.RemoveAt(num - 1);
                        }
                        else
                        {
                            Console.WriteLine("이동 불가 합니다.");
                        }
                        break;
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.D4:
                        if (maze.search(player.pos.Item1, player.pos.Item2 + 2))
                        {
                            player.pos = (player.pos.Item1, player.pos.Item2 + invens[num - 1].Use());
                            Console.WriteLine("오른쪽으로 점프!");
                            invens.RemoveAt(num - 1);
                        }
                        else
                        {
                            Console.WriteLine("이동 불가 합니다.");
                        }
                        break;
                    default:
                        Console.WriteLine("잘못된 입력 입니다.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            Thread.Sleep(500);
        }
    }


}
