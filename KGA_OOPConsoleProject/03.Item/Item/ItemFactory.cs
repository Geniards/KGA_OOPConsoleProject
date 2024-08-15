using KGA_OOPConsoleProject.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KGA_OOPConsoleProject._03.Item.Item
{
    public class ItemFactory
    {
        public static T Instantiate<T>(string Name) where T : Items
        {
            if (Name == "Potion")
            {
                Potion potion = new Potion();
                potion.name = "이동포션";
                potion.info = "+10 만큼의 이동횟수가 찬다.";
                potion.type = Util.EItemType.Potion;
                potion.gold = 100;
                potion.effect = 10;

                return potion as T;
            }
            else if (Name == "MapReSearch")
            {
                MapReSearch mapReSearch = new MapReSearch();
                mapReSearch.name = "맵 리서치";
                mapReSearch.info = "최단경로를 보여준다.";
                mapReSearch.type = Util.EItemType.MapReSearch;
                mapReSearch.gold = 300;
                mapReSearch.effect = 5;

                return mapReSearch as T;
            }
            else if (Name == "Jummper")
            {
                Jump jump = new Jump();
                jump.name = "점퍼";
                jump.info = "벽을 한칸 건너 뛸 수 있다.";
                jump.type = Util.EItemType.Jump;
                jump.gold = 150;
                jump.effect = 1;

                return jump as T;
            }
            else
            {
                Console.WriteLine("해당 이름의 아이템은 없습니다.");
                Thread.Sleep(1000);

                return null;
            }
        }

        public static Items Instantiate(string name)
        {
            if(name == "Potion")
            {
                Potion potion = new Potion();
                potion.name = "이동포션";
                potion.info = "+10 만큼의 이동횟수가 찬다.";
                potion.type = Util.EItemType.Potion;
                potion.gold = 100;
                potion.effect = 10;

                return potion;
            }
            else if(name == "MapReSearch")
            {
                MapReSearch mapReSearch = new MapReSearch();
                mapReSearch.name = "맵 리서치";
                mapReSearch.info = "최단경로를 보여준다.";
                mapReSearch.type = Util.EItemType.MapReSearch;
                mapReSearch.gold = 300;
                mapReSearch.effect = 5;

                return mapReSearch;
            }
            else if(name == "Jump")
            {
                Jump jump = new Jump();
                jump.name = "점퍼";
                jump.info = "벽을 한칸 건너 뛸 수 있다.";
                jump.type = Util.EItemType.Jump;
                jump.gold = 150;
                jump.effect = 1;

                return jump;
            }
            else
            {
                Console.WriteLine("해당 이름의 아이템은 없습니다.");
                Thread.Sleep(1000);
                return null;
            }
        }
    }
}
