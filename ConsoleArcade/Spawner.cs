using System;
using System.Collections.Generic;

namespace ConsoleArcade
{
    public class Spawner
    {

        private static Random rand = new Random();
        private static TimeSpan downTime = new TimeSpan(0);
        private static DateTime lastSpawn = DateTime.Now;

        private static int downTimeModifier = 0;
        private static TimeSpan speedModifier = new TimeSpan(0);

        public static int totalSpawned = 0;
        public static int difficulty = 0;

        //private static int tolerance = 0;
        private static int dwnTme = 15_000_000;

        private static List<string> symbols = Program.currentDetail.foes;


        public static void SpawnAutomatically(List<MovableObject> into)
        {
            if (DateTime.Now > lastSpawn + downTime)
            {
                lastSpawn = DateTime.Now;

                if (dwnTme - downTimeModifier - (difficulty * 200_000) <= 0)
                {
                    //downTime = new TimeSpan(rand.Next(200_000, 1_500_000));
                    downTime = new TimeSpan(10_000_000);
                }
                else
                {
                    //downTime = new TimeSpan(rand.Next(dwnTme - tolerance - downTimeModifier - (difficulty * 200_000), dwnTme + tolerance - downTimeModifier - (difficulty * 200_000)));
                    downTime = new TimeSpan(dwnTme - downTimeModifier);
                }

                // spawn rare
                if (rand.Next(0, 40) == 1)
                {
                    // haha lol
                    into.Add(spawnTreat());
                }
                else 
                {
                    // easy to confuse
                    into.Add(spawnThreat());
                }

                

                totalSpawned += 1;

                float multiplier = totalSpawned / 10;

                if ((int)multiplier * 1 > difficulty)
                {
                    difficulty = (int)multiplier * 1 + (int)( Math.Sqrt((Program.maxRows * Program.maxRows) + (Program.maxColumns * Program.maxColumns)) / 10 );
                    downTimeModifier = downTimeModifier + 6_000_000;
                }
            }
        }

        private static MovableObject spawnThreat()
        {

            MovableObject threat = new MovableObject(0, rand.Next(0, Program.maxColumns - 1), symbols[rand.Next(symbols.Count)], new TimeSpan(3_500_000 - (difficulty * 200_000)));

            threat.directionRow = 1;

            return threat;
        }

        private static MovableObject spawnTreat()
        {

            MovableObject treat = new MovableObject(0, rand.Next(0, Program.maxColumns - 1), Program.currentDetail.powerUp, new TimeSpan(3_500_000 - (difficulty * 200_000)));

            treat.directionRow = 1;
            treat.isTreat = true;

            return treat;
        }
    }
}
