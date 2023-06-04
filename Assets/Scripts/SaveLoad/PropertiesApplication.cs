using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.SaveLoad
{
    public class PropertiesApplication
    {
        public static bool RequestLoadGame { get; set; } = false;
        public static bool RequestLoadEnemy { get; set; } = false;
        public static List<EnemyProperties> enemyProperties { set; get; }
        public static List<TurretProperties> turretProperties { set; get; }
        public static int TotalCoins { set; get; } = -10;
        public static int currentWave { set; get; } = 0;
        public static int TotalLives { set; get; } = -10;
        public static int enemiesSpawned { set; get; }
        public static int enemiesRemain { set; get; }
    }
}
