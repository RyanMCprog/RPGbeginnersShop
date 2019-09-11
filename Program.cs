using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RPG_BeginnersShop
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int days = 1;
            Player PChealth = new Player();
            int Fightread = 0;
            int ShopNum = -1;
            int RareShopNum = -1;
            int Rndnum = 0;
            int InventSpace = 0;
            Items Sold = new Items();
            
            Random Rnd = new Random();
            
            string cmd = "";
            string lcmd = "";
            Items AT = new Items();
            Player Money = new Player();
            int Tired = 0;
            //making a list of shop items for an excel sheet
            List<Items> MyInventory = new List<Items>();
            List<Items> ShopInventory = new List<Items>();
            List<Items> RareStoneInventory = new List<Items>();
            using (StreamReader si = new StreamReader("ShopItems.csv"))
            {
                si.ReadLine();

                
                while(!si.EndOfStream)
                {
                    string Line = si.ReadLine();
                    string[] Values = Line.Split(',');
                    Items tmpItem = new Items();
                    tmpItem.ItemId = int.Parse(Values[0]);
                    tmpItem.ItemName = Values[1];
                    tmpItem.Damage = int.Parse(Values[2]);
                    tmpItem.Defense = int.Parse(Values[3]);
                    tmpItem.rarity = int.Parse(Values[4]);
                    tmpItem.ShopSell = int.Parse(Values[5]);
                    tmpItem.ItemValue = int.Parse(Values[6]);
                    ShopInventory.Add(tmpItem);
                    ShopNum++;
                }
            }
            //reading out the text file for the rare stone shop
            using (StreamReader se = new StreamReader("RareStoneShop.csv"))
            {
                se.ReadLine();


                while (!se.EndOfStream)
                {
                    string RareLine = se.ReadLine();
                    string[] RareValues = RareLine.Split(',');
                    Items RmpItem = new Items();
                    RmpItem.ItemId = int.Parse(RareValues[0]);
                    RmpItem.ItemName = RareValues[1];
                    RmpItem.Damage = int.Parse(RareValues[2]);
                    RmpItem.Defense = int.Parse(RareValues[3]);
                    RmpItem.rarity = int.Parse(RareValues[4]);
                    RmpItem.ShopSell = int.Parse(RareValues[5]);
                    RmpItem.ItemValue = int.Parse(RareValues[6]);
                    RareStoneInventory.Add(RmpItem);
                    RareShopNum++;
                }
            }
            //main loop where the player chooses what to do
            while (cmd.ToLower() != "leave")
            {
                cmd = AT.Ask("Want do you want? Buy, sell, inventory, fight, mine, sleep, RareStones?");
                lcmd = cmd.ToLower();
                //purchasing items from the shop
                if (lcmd == "buy")
                {
                   foreach(Items it in ShopInventory)
                   {
                        Console.WriteLine(it.ItemId + " " + it.ItemName + " Damage: " + it.Damage + " Defense: " + it.Defense + " Rarity: " + it.rarity + " price: " + it.ShopSell);
                   }
                    int.TryParse(AT.Ask("Please enter the number by the item you want to purchase"), out int tmp);
                    if(tmp > ShopNum || tmp < 0)
                    {
                        tmp = ShopNum;
                    }
                    string YesorNo = AT.Ask("Buy " + ShopInventory[tmp].ItemName + "? (Y/N)");
                    if (YesorNo == "y")
                    {
                       if(Money.coins >= ShopInventory[tmp].ShopSell)
                       {
                            Items Bought = new Items();
                            Bought.ItemId = ShopInventory[tmp].ItemId;
                            Bought.ItemName = ShopInventory[tmp].ItemName;
                            Bought.Damage = ShopInventory[tmp].Damage;
                            Bought.Defense = ShopInventory[tmp].Defense;
                            Bought.rarity = ShopInventory[tmp].rarity;
                            Bought.ShopSell = ShopInventory[tmp].ShopSell;
                            Bought.ItemValue = ShopInventory[tmp].ItemValue;
                            MyInventory.Add(Bought);
                            Money.coins -= ShopInventory[tmp].ShopSell;
                            Console.WriteLine("You have bought " + ShopInventory[tmp].ItemName);
                            Console.WriteLine("You now have " + Money.coins + " coins");
                            Fightread++;
                       }
                    }
                }
                //selling items from the shop
                if (lcmd == "sell")
                {
                    if (Fightread != 0)
                    {
                        InventSpace = -1;
                        foreach (Items so in MyInventory)
                        {
                            InventSpace++;
                            Console.WriteLine(InventSpace + " " + so.ItemName + " Damage: " + so.Damage + " Defense: " + so.Defense + " Rarity: " + so.rarity + " sell price: " + so.ItemValue);
                        }
                        int.TryParse(AT.Ask("Please enter the number by the item you want to sell"), out int tmp);
                        if (tmp > InventSpace || tmp < 0)
                        {
                            tmp = InventSpace;
                        }
                        string YorN = AT.Ask("Sell " + MyInventory[tmp].ItemName + "? (Y/N)");
                        if (YorN == "y")
                        {
                            Console.WriteLine("You have sold a " + MyInventory[tmp].ItemName);
                            Money.coins = Money.coins + MyInventory[tmp].ItemValue;
                            Console.WriteLine("You now have " + Money.coins + " coins");
                            Fightread--;
                            MyInventory.RemoveAt(tmp);
                        }
                    }
                    else
                    {
                        Console.WriteLine("You do not have an item to sell");
                    }
                }
                //Fighting the shopkeeper 
                if (lcmd == "fight")
                {
                    if (Fightread != 0)
                    {
                        string AreYouReady = AT.Ask("So, you think you are ready to go on your quest? (Y/N)");
                        if (AreYouReady == "y")
                        {
                            PChealth.EnemyHealth = 100;
                            PChealth.Health = 100;
                            Console.WriteLine("Than face me!");
                            while (PChealth.Health > 0 && PChealth.EnemyHealth > 0)
                            {
                                InventSpace = -1;
                                foreach (Items so in MyInventory)
                                {
                                    InventSpace++;
                                    Console.WriteLine(InventSpace + " " + so.ItemName + " Damage: " + so.Damage + " Defense: " + so.Defense + " Rarity: " + so.rarity + " sell price: " + so.ItemValue);
                                }
                                int.TryParse(AT.Ask("Please enter the number by the item you want to Attack with"), out int tmp);
                                if(tmp > InventSpace || tmp < 0)
                                {
                                    tmp = InventSpace;
                                }
                                Console.WriteLine("You swing with " + MyInventory[tmp].ItemName + " and...");
                                Rndnum = Rnd.Next(1, 20);
                                if (Rndnum < 3)
                                {
                                    Console.WriteLine("You missed");
                                }
                                else if (Rndnum < 10)
                                {
                                    Console.WriteLine("You hit...  a weak swing");
                                    PChealth.EnemyHealth -= (MyInventory[tmp].Damage + PChealth.playerbaseDamage - PChealth.EnemyDefense) / 2;
                                    Console.WriteLine("The Shopkeeper now has " + PChealth.EnemyHealth + " health left.");
                                }
                                else if (Rndnum > 17)
                                {
                                    Console.WriteLine("You got a Critical hit");
                                    PChealth.EnemyHealth -= (MyInventory[tmp].Damage + PChealth.playerbaseDamage - PChealth.EnemyDefense) * 2;
                                    Console.WriteLine("The Shopkeeper now has " + PChealth.EnemyHealth + " health left.");
                                }
                                else
                                {
                                    Console.WriteLine("You hit your swing");
                                    PChealth.EnemyHealth -= (MyInventory[tmp].Damage + PChealth.playerbaseDamage - PChealth.EnemyDefense);
                                    Console.WriteLine("The Shopkeeper now has " + PChealth.EnemyHealth + " health left.");
                                }

                                if (PChealth.EnemyHealth > 0)
                                {
                                    InventSpace = -1;
                                    foreach (Items so in MyInventory)
                                    {
                                        InventSpace++;
                                        Console.WriteLine(InventSpace + " " + so.ItemName + " Damage: " + so.Damage + " Defense: " + so.Defense + " Rarity: " + so.rarity + " sell price: " + so.ItemValue);
                                    }
                                    int.TryParse(AT.Ask("Please enter the number by the item you want to defend with"), out int dmp);
                                    if (dmp > InventSpace || dmp < 0)
                                    {
                                        dmp = InventSpace;
                                    }
                                    Console.WriteLine("You hide behind with " + MyInventory[dmp].ItemName + " while the shopkeeper fire the fire staff and...");
                                    Rndnum = Rnd.Next(1, 20);
                                    if (Rndnum < 3)
                                    {
                                        Console.WriteLine("He missed");
                                    }
                                    else if (Rndnum < 10)
                                    {
                                        Console.WriteLine("He hit...  a weak swing");
                                        PChealth.Health -= (40 + PChealth.EnemyBaseDamage - MyInventory[dmp].Defense) / 2;
                                        Console.WriteLine("You now have " + PChealth.Health + " health left.");
                                    }
                                    else if (Rndnum > 18)
                                    {
                                        Console.WriteLine("He got a Critical hit");
                                        PChealth.Health -= (40 + PChealth.EnemyBaseDamage - MyInventory[dmp].Defense) * 2;
                                        Console.WriteLine("You now have " + PChealth.Health + " health left.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("He hit his shot");
                                        PChealth.Health -= (40 + PChealth.EnemyBaseDamage - MyInventory[dmp].Defense);
                                        Console.WriteLine("You now have " + PChealth.Health + " health left.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("The Shopkeeper is down");
                                }
                            }
                            if (PChealth.Health < 0)
                            {
                                Console.WriteLine("You were knocked unconscious and lost all items");
                                Money.coins = 0;
                                Money.RareStone = 0;
                                MyInventory.RemoveRange(0, Fightread);
                                Fightread = 0;
                            }
                            else
                            {
                                Console.WriteLine("You have won and are ready to go on your quest");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("You are not ready, get an item then you can challenge me");
                    }
                }
                //gathering coins and sometimes items
                if (lcmd == "mine")
                {
                   if (Tired != 5)
                   {
                        Rndnum = Rnd.Next(0, 50);
                        if (Rndnum < 5)
                        {
                            Items Found = new Items();
                            Found.ItemId = ShopInventory[0].ItemId;
                            Found.ItemName = ShopInventory[0].ItemName;
                            Found.Damage = ShopInventory[0].Damage;
                            Found.Defense = ShopInventory[0].Defense;
                            Found.rarity = ShopInventory[0].rarity;
                            Found.ShopSell = ShopInventory[0].ShopSell;
                            Found.ItemValue = ShopInventory[0].ItemValue;
                            MyInventory.Add(Found);
                            Console.WriteLine("You found an unimpressive Branch.");
                            Fightread++;
                        }
                        else if(Rndnum > 40)
                        {
                            Items FoundBt = new Items();
                            Rndnum = Rnd.Next(0, ShopNum);
                            FoundBt.ItemId = ShopInventory[Rndnum].ItemId;
                            FoundBt.ItemName = ShopInventory[Rndnum].ItemName;
                            FoundBt.Damage = ShopInventory[Rndnum].Damage;
                            FoundBt.Defense = ShopInventory[Rndnum].Defense;
                            FoundBt.rarity = ShopInventory[Rndnum].rarity;
                            FoundBt.ShopSell = ShopInventory[Rndnum].ShopSell;
                            FoundBt.ItemValue = ShopInventory[Rndnum].ItemValue;
                            MyInventory.Add(FoundBt);
                            Console.WriteLine("You managed to find something in a bush");
                            Fightread++;
                        }
                        else if(Rndnum < 20 && Rndnum > 10)
                        {
                            Rndnum = Rnd.Next(1, 20);
                            Money.RareStone = Money.RareStone + Rndnum;
                            Console.WriteLine("you now have " + Money.RareStone + " Rare Stones(these are not coins)");
                        }
                        else
                        {
                            Money.coins = Money.coins + Rndnum;
                            Console.WriteLine("you now have " + Money.coins + " coins.");
                        }
                        Tired++;
                        
                   }
                   else
                   {
                        Console.WriteLine("You are to tired to mine.");
                   }
                }
                //getting the ability to mine back
                if (lcmd == "sleep")
                {
                    if(days < 5)
                    {
                        Tired = 0;
                        Console.WriteLine("You are now fully rested.");
                        days++;
                    }
                    else
                    {
                        Console.WriteLine("Days are limited, you don't have all the time in the world to do this.");
                    }
                }
                //check items the player is currently in their inventory
                if (lcmd == "inventory")
                {
                    Console.WriteLine("You have a base damage of 11");
                    InventSpace = 0;
                    foreach (Items so in MyInventory)
                    {
                        Console.WriteLine(InventSpace + " " + so.ItemName + " Damage: " + so.Damage + " Defense: " + so.Defense + " Rarity: " + so.rarity + " sell price: " + so.ItemValue);
                        InventSpace++;
                    }
                }
                //rare stones 
                if(lcmd == "rarestones")
                {
                    foreach (Items rt in RareStoneInventory)
                    {
                        Console.WriteLine(rt.ItemId + " " + rt.ItemName + " Damage: " + rt.Damage + " Defense: " + rt.Defense + " Rarity: " + rt.rarity + " price: " + rt.ShopSell);
                    }
                    int.TryParse(AT.Ask("Please enter the number by the item you want to purchase"), out int tmp);
                    if (tmp > RareShopNum || tmp < 0)
                    {
                        tmp = RareShopNum;
                    }
                    string YesorNoR = AT.Ask("Buy " + RareStoneInventory[tmp].ItemName + "? (Y/N)");
                    if (YesorNoR == "y")
                    {
                        if (Money.RareStone >= RareStoneInventory[tmp].ShopSell)
                        {
                            Items Bought = new Items();
                            Bought.ItemId =RareStoneInventory[tmp].ItemId;
                            Bought.ItemName = RareStoneInventory[tmp].ItemName;
                            Bought.Damage = RareStoneInventory[tmp].Damage;
                            Bought.Defense = RareStoneInventory[tmp].Defense;
                            Bought.rarity = RareStoneInventory[tmp].rarity;
                            Bought.ShopSell = RareStoneInventory[tmp].ShopSell;
                            Bought.ItemValue = RareStoneInventory[tmp].ItemValue;
                            MyInventory.Add(Bought);
                            Money.RareStone -= RareStoneInventory[tmp].ShopSell;
                            Console.WriteLine("You have bought " + RareStoneInventory[tmp].ItemName);
                            Console.WriteLine("You now have " + Money.RareStone + " Rare Stones");
                            Fightread++;
                        }
                    }
                }
            }
        }
    }
}
