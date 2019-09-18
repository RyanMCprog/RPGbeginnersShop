using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_BeginnersShop
{
    public class Enemy
    {
        public int EnemyHealth = 100;
        public int EnemyDefense = 15;
        public int EnemyBaseDamage = 30;
    }

    public class Player : Enemy
    {
        public int RareStone = 0;
        public int coins = 0;
        public int Health = 100;
        public int playerbaseDamage = 11;
    }
}
