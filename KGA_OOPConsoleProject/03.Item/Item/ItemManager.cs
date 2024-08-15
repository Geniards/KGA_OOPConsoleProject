using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject._03.Item.Item
{
    public class ItemManager
    {
        private Items potion;
        private Items mapResearch;
        private Items jump;

        private List<Items> items;

        public bool bCreateItem;


        public ItemManager()
        {
            bCreateItem = false;
            items = new List<Items>();
            Init();
        }

        private void Init()
        {
            potion = ItemFactory.Instantiate("Potion");
            mapResearch = ItemFactory.Instantiate("MapReSearch");
            jump = ItemFactory.Instantiate("Jump");

            items.Add(potion);
            items.Add(mapResearch);
            items.Add(jump);
        }

        private Items RandomItem()
        {
            Random rd = new Random();
            int num = rd.Next(0, items.Count);

            return items[num]; 
        }

        public void Create()
        {
            bCreateItem = true;
        }

        public Items GetItem()
        {
            if (bCreateItem)
            {
                bCreateItem = false;
                return RandomItem();
            }
            else
                return null;
        }
    }
}
