using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_BeginnersShop
{

    public class MostBasicItem
    {
        public int ItemId;
        public int ItemValue;
    }

    public class Items : MostBasicItem
    {
        public string ItemName;
        public int Damage;
        public int Defense;
        public int rarity;
        public int ShopSell;
        
        public string Ask (string _val)
        {
            Console.Write(_val);

            return Console.ReadLine();
        }

        public Items()
        {

        }
        
        public Items(string itemName, int damage, int defense, int _rarity, int shopSell)
        {
            ItemName = itemName;
            Damage = damage;
            Defense = defense;
            rarity = _rarity;
            ShopSell = shopSell;
        }
    }
}
