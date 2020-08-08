using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleArcade
{
    class Program
    {

        public static int maxRows = 15;
        public static int maxColumns = 30;

        public static int score = 0;
        public static int ammo = 0;

        static void DrawSeveralInLine(List<MovableObject> objects)
        {

            string newLine = "";

            for (int i = 0; i < maxColumns; i++)
            {
                MovableObject obj = objects.Find(o => o.column == i);

                if (obj != null)
                {
                    newLine += obj.symbol;
                }
                else
                {
                    newLine += " ";
                }
            }

            Console.WriteLine(newLine);
        }

        static void DrawScore()
        {

            string scr = $"Score: {score}";

            string buffer = "";

            for (int i = 0; i < maxColumns; i++)
            {
                buffer += "_";
            }
            
            Console.WriteLine(buffer);

            Console.WriteLine();

            Console.WriteLine(scr);
        }

        static void DrawAmmo()
        {

            string amm = $"Eldritch Blast Count: {ammo}";

            Console.WriteLine(amm);
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Console Arcade - Project by Cedric Zwahlen");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Press '⌘+⇧+1' to resize the window.");
            Console.WriteLine("");
            Console.WriteLine("Any key to start.");

            ConsoleKey key = Console.ReadKey().Key;

            Console.Clear();

            // One million ticks = 1 sec / ticks not used in cursor

            MovableObject cursor = new MovableObject(maxRows, maxRows / 2, "🍤", new TimeSpan(50_000_000));

            cursor.lastUpdate = DateTime.Now;

            List<MovableObject> movableObjects = new List<MovableObject>();

            bool gameOver = false;

            
            do
            {

                // updates the game

                while (Console.KeyAvailable == false && gameOver == false)
                {

                    Spawner.SpawnAutomatically(movableObjects);

                    Console.Clear();

                    // updates the position of the objects
                    foreach (MovableObject obj in movableObjects)
                    {

                        if (DateTime.Now > obj.lastUpdate + obj.updateInterval)
                        {

                            obj.lastUpdate = DateTime.Now;

                            obj.column = obj.column + obj.directionColumn;
                            obj.row = obj.row + obj.directionRow;

                        }
                    }

                    // mark objects to be removed
                    for (int i = 0; i < movableObjects.Count; i++)
                    {
                        if (movableObjects[i].row > maxRows || movableObjects[i].row < 0)
                        {
                            //movableObjects.RemoveAt(i);
                            movableObjects[i].remove = true;
                        }

                        MovableObject collisionWithCursor = movableObjects.Find(o => o.column == cursor.column && o.row == cursor.row);

                        // if a collision with the cursor occurred
                        if (collisionWithCursor != null)
                        {

                            if (collisionWithCursor.isTreat)
                            {
                                ammo += 5;
                                collisionWithCursor.remove = true;
                                break;
                            }
                            else
                            {
                                gameOver = true;
                                break;
                            }
                        }

                            MovableObject mvblObj = movableObjects.Find(o => o.column == movableObjects[i].column && o.row == movableObjects[i].row);

                        if (mvblObj != movableObjects[i] && mvblObj != null)
                        {
                            mvblObj.remove = true;
                            movableObjects[i].remove = true;
                            score++;
                        }
                    }

                    // remove all marked items

                    List<MovableObject> ToRemove = movableObjects.FindAll(m => m.remove == true);

                    foreach (MovableObject obj in ToRemove)
                    {
                        movableObjects.Remove(obj);
                    }

                    DrawAmmo();

                    // draws the lines again
                    for (int i = 0; i <= maxRows; i++)
                    {


                        List<MovableObject> objectsInLine = movableObjects.FindAll(m => m.row == i);

                        if (i == cursor.row)
                        {
                            objectsInLine.Add(cursor);

                        }

                        DrawSeveralInLine(objectsInLine);

                    }

                    DrawScore();

                    Thread.Sleep(50);
                }


                if (gameOver)
                { break; }


                // reacts to user input

                key = Console.ReadKey().Key;


                if (key == ConsoleKey.RightArrow)
                {
                    cursor.column = cursor.column >= maxColumns - 1 ? maxColumns - 1 : cursor.column + 1;
                   
                }

                if (key == ConsoleKey.LeftArrow)
                {

                    cursor.column = cursor.column <= 0 ? 0 : cursor.column - 1;

                }

                if (key == ConsoleKey.Spacebar && ammo > 0)
                {

                    ammo -= 1;

                    MovableObject missile = new MovableObject(cursor.row - 1, cursor.column, "⚡️", new TimeSpan(1_000_000));

                    missile.directionRow = -1;

                    movableObjects.Add(missile);

                }

                

            } while (key != ConsoleKey.Escape);

            Console.Clear();

            Console.WriteLine("Game Over!");

        }
    }
}
