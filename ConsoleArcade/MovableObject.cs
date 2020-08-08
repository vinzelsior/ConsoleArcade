using System;
namespace ConsoleArcade
{
    public class MovableObject
    {

        public int row;
        public int column;
        public string symbol;

        public bool remove = false;

        public bool isTreat = false;

        public TimeSpan updateInterval;
        public DateTime lastUpdate;

        public int directionRow = 0;
        public int directionColumn = 0;

        public MovableObject(int row, int column, string symbol, TimeSpan updateInterval)
        {
            this.lastUpdate = DateTime.Now;
            this.updateInterval = updateInterval;
            this.row = row;
            this.column = column;
            this.symbol = symbol;

        }
    }
}
