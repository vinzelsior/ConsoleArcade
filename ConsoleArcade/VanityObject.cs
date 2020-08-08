using System;
namespace ConsoleArcade
{
    public class VanityObject
    {

        public int row;
        public int column;
        public string symbol;

        public TimeSpan lifeTime;
        public DateTime spawnedAt;

        public bool remove = false;

        public VanityObject(int row, int column, string symbol, TimeSpan lifeTime)
        {
            this.spawnedAt = DateTime.Now;
            this.row = row;
            this.column = column;
            this.symbol = symbol;
            this.lifeTime = lifeTime;

        }

    }
}
