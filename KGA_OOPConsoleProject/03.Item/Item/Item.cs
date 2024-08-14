using KGA_OOPConsoleProject.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject._03.Item.Item
{
    public abstract class Item
    {
        public string name { get; set; }
        public string info { get; set; }
        public EItemType type { get; set; }
        public int gold { get; set; }
        public int effect { get; set; }

        public Item()
        {
            name = "아이템";
            info = "아이템입니다.";
            type = EItemType.EITEM_TYPE_MAX;
            gold = 0;
            effect = 0;
        }

        public abstract int Use(int count = 0);
    }
}
