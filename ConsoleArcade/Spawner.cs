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

        public static void SpawnAutomatically(List<MovableObject> into)
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
                        into.Add(spawnTreat());
                    }
                    else
                    {
                        // easy to confuse
                        into.Add(spawnThreat());
                    }
                }

                if (DateTime.Now > lastLevelUp + triggerLevelUp)
                {
                    level++;
                    lastLevelUp = DateTime.Now;
                }
            }
        }

        private static MovableObject spawnThreat()
        {
            MovableObject threat = new MovableObject(0, rand.Next(0, Program.maxColumns - 1), symbols[rand.Next(symbols.Count)], new TimeSpan(3_500_000 - (level * 200_000)));

            threat.directionRow = 1;

            return threat;
        }

        private static MovableObject spawnTreat()
        {
            MovableObject treat = new MovableObject(0, rand.Next(0, Program.maxColumns - 1), Program.currentDetail.powerUp, new TimeSpan(3_500_000 - (level * 200_000)));

            treat.directionRow = 1;
            treat.isTreat = true;

            return treat;
        }

        public static void reset()
        {
            
            level = 1;

            lastLevelUp = DateTime.Now;
        }
    }
}
