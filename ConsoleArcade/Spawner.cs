using System;
using System.Collections.Generic;

namespace ConsoleArcade
{
    public class Spawner
    {

        private static Random rand = new Random();
        private static TimeSpan downTime = new TimeSpan(0);
        private static DateTime lastSpawn = DateTime.Now;

        public static int level = 1;

        private static TimeSpan triggerLevelUp = new TimeSpan(200_000_000);
        private static DateTime lastLevelUp = DateTime.Now;

        public static List<string> symbols = Program.currentDetail.foes;

        public static void SpawnAutomatically(List<BaseGameObject> into)
        {
            if (DateTime.Now > lastSpawn + downTime)
            {
                lastSpawn = DateTime.Now;

                int val = (20_000_000 - (level * 1_000_000)) / (int)(Program.maxColumns / 5);

                downTime = new TimeSpan(val);

                level = level > 7 ? 8 : level;

                for (int i = 0; i < level; i++)
                {
                    // spawn rare
                    if (rand.Next(0, 20) == 1)
                    {
                        // haha lol
                        into.Add(SpawnPowerUp());
                    }
                    else
                    {
                        // easy to confuse
                        into.Add(SpawnThreat());
                    }
                }

                if (DateTime.Now > lastLevelUp + triggerLevelUp)
                {
                    level++;
                    lastLevelUp = DateTime.Now;
                }
            }
        }

        private static BaseGameObject SpawnThreat()
        {
            BaseGameObject threat = new Threat(0, rand.Next(0, Program.maxColumns - 1), symbols[rand.Next(symbols.Count)], 3_500_000 - (level * 300_000))
            {
                directionRow = 1
            };

            return threat;
        }

        private static BaseGameObject SpawnPowerUp()
        {
            BaseGameObject powerUp = new PowerUp(0, rand.Next(0, Program.maxColumns - 1), Program.currentDetail.powerUp, 3_500_000 - (level * 300_000))
            {
                directionRow = 1
            };

            return powerUp;
        }

        public static void Reset()
        {
            
            level = 1;

            lastLevelUp = DateTime.Now;
        }
    }
}
