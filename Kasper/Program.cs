using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kasper
{
    class Program
    {
        static public int maxWidth = 40;
        static public int maxHeight = 20;

        public double X = maxWidth / 2;
        public double Y = maxHeight / 2;
        public double speedStart = 1;
        public double progresBar = 0;
        public double numberCilEnum = 0;
        List<List<double>> XNYNPatron = new List<List<double>>();

        List<List<double>> XNYN = new List<List<double>>();
        public double XN = 1;
        public double YN = 1;
        public int timeEventsN = 0;
        public int timeEventsPulsN = 0;
        public double speedStartN = 1;
        public bool eventH = false;

        Random r = new Random();

        public void SpaunEnum()
        {
            List<double> n = new List<double>();
            n.Add(r.Next(0,maxHeight));
            n.Add(r.Next(0,maxWidth));
            this.XNYN.Add(n);
        }

        public void ProgresBarStart()
        {
            if (this.progresBar < 100) this.progresBar += 10;
            else this.progresBar = 0;

            this.SpaunPatron();
        }

        public void SpaunPatron()
        {
            if(progresBar == 100)
            {
                List<double> l = new List<double>();
                l.Add(this.X);
                l.Add(this.Y);
                this.XNYNPatron.Add(l);
            }
        }

        public void MovePatron()
        {
            if (this.timeEventsN > 1)
            {
                for (int num = 0; num < this.XNYNPatron.Count; num++)
                {
                    bool ev = false;
                    List<double> n = new List<double>();

                    this.XN = this.XNYNPatron[num][0];
                    this.YN = this.XNYNPatron[num][1];

                    if (this.X < this.XN)
                    {
                        this.XN += speedStartN;
                    }
                    else if (this.X > this.XN)
                    {
                        this.XN -= speedStartN;
                    }

                    if (this.Y < this.YN)
                    {
                        this.YN += speedStartN;
                    }
                    else if (this.Y > this.YN)
                    {
                        this.YN -= speedStartN;
                    }

                    if (ev == false)
                    {
                        n.Add(this.XN);
                        n.Add(this.YN);
                        this.XNYNPatron[num] = n;
                    }
                }
            }
        }

        public void MoveEnum()
        {
            if(this.timeEventsPulsN > 4)
            {
                if (this.XNYN.Count <= 1)
                {
                    this.SpaunEnum();
                    this.timeEventsPulsN = 0;
                }
            }
            else
            {
                this.timeEventsPulsN += 1;
            }

            if (this.timeEventsN > 1)
            {
                for (int num = 0; num < this.XNYN.Count; num++)
                {
                    bool ev = false;
                    List<double> n = new List<double>();

                    this.XN = this.XNYN[num][0];
                    this.YN = this.XNYN[num][1];

                    for (int i = 0; i < this.XNYN.Count; i++) //Проверка на столкновение
                    {
                        if (i != num)
                        {
                            if (this.XN == this.XNYN[i][0] && this.YN == this.XNYN[i][1])
                            {
                                this.XNYN.Remove(this.XNYN[i]);
                                if (num == 0) this.XNYN.Remove(this.XNYN[num]);
                                else this.XNYN.Remove(this.XNYN[num - 1]);
                                this.numberCilEnum += 2;
                                ev = true;
                                break;
                            }
                        }
                    }

                    if (this.X > this.XN)
                    {
                        this.XN += speedStartN;
                    }
                    else if (this.X < this.XN)
                    {
                        this.XN -= speedStartN;
                    }

                    if (this.Y > this.YN)
                    {
                        this.YN += speedStartN;
                    }
                    else if (this.Y < this.YN)
                    {
                        this.YN -= speedStartN;
                    }

                    this.timeEventsN = 0;

                    if(ev == false)
                    {
                        n.Add(this.XN);
                        n.Add(this.YN);
                        this.XNYN[num] = n;
                    }
                }
            }else
            {
                this.timeEventsN += 1;
            }
        }

        public void Input()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(false).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    this.Y -= speedStart;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    this.Y += speedStart;
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    this.X -= speedStart;
                }
                else if (key == ConsoleKey.RightArrow)
                {
                    this.X += speedStart;
                }
            }
            else
            {
                return;
            }
        }

        public void PrintPole()
        {
            Console.Clear();

            for (int i = 0; i < maxHeight; i++)
            {
                for (int u = 0; u < maxWidth; u++)
                {
                    if (u == this.X - 1 && i == this.Y - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("0");
                        Console.ResetColor();
                    }

                    for (int num = 0; num < this.XNYN.Count; num++)
                    {
                        if (u == this.XNYN[num][0] - 1 && i == this.XNYN[num][1] - 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("=");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(" ");
                        }

                        if (this.X == this.XNYN[num][0] && this.Y == this.XNYN[num][1])
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Проиграли!!");
                            Console.ResetColor();
                            Console.ReadLine();
                        }
                    }
                }

                if(i == maxHeight / 2)
                {
                    Console.Write($"    Заряд: [{this.progresBar}%] Уничтоженно призраков: [{this.numberCilEnum}]");
                }

                Console.WriteLine("");
            }
            Thread.Sleep(10);
        }

        static void Main(string[] args)
        {
            Program pr = new Program();

            while (true)
            {
                pr.MoveEnum();
                pr.Input();
                pr.PrintPole();
            }
        }
    }
}
