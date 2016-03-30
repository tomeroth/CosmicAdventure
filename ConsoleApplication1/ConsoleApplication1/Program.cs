using ConsoleApplication1.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        private static List<Starship> _starships = new List<Starship>();
        private static bool _anySystem = true;
        private static int _gold = 1000;
        private static int _imperiumMoneyAskCount = 4;
        private static Service1Client servClient1;
        private static ServiceReference2.Service1Client servClient2;

        static void Main(string[] args)
        {
            servClient1 = new Service1Client();
            servClient2 = new ServiceReference2.Service1Client();
            servClient1.InitializeGame();
            Menu();
        }
        public static void Menu()
        {
            Console.WriteLine("###########################################################");
            Console.WriteLine("###                   Main Menu                         ###");
            Console.WriteLine("###########################################################");
            Console.WriteLine("###                Actual Gold:"+ _gold + "                     ###");
            Console.WriteLine("### Imperium Money Asks Left:" + _imperiumMoneyAskCount + "                          ###");
            Console.WriteLine("###                   Choose option:                    ###");
            Console.WriteLine("###########################################################");
            Console.WriteLine();
            Console.WriteLine("### A: Ask Imperium for Gold ");
            Console.WriteLine("### B: Buy ship with gold ");
            Console.WriteLine("### C: Send ship to system ");
            Console.WriteLine("### D: End Game");
            Console.WriteLine("### ESC: To leave");

            ConsoleKeyInfo choosenOption = Console.ReadKey();
            Console.ReadLine();

            switch (choosenOption.Key)
            {
                case ConsoleKey.A:
                    if (_imperiumMoneyAskCount > 0)
                    {
                        int goldFromImperium = servClient2.GetMoneyFromImperium();
                        _gold += goldFromImperium;
                        Console.WriteLine("### You have received "+ goldFromImperium + " gold from the Imperium ##");
                        _imperiumMoneyAskCount--;
                    }
                    else
                    {
                        Console.WriteLine("### You cannot ask for gold anymore ##");
                    }
                    break;
                case ConsoleKey.B:
                    Console.WriteLine();
                    Console.WriteLine("### Actual Gold: "+_gold+" ##");
                    Console.WriteLine("### Type how much u want to spend on new ship ##");
                    int choosenGold;
                    string goldForShipString = Console.ReadLine();
                    if (Int32.TryParse(goldForShipString, out choosenGold))
                    {
                        if (choosenGold <= _gold && choosenGold > 0)
                        {
                            _gold -= choosenGold;
                            _starships.Add(servClient1.GetStarship(choosenGold));
                            Console.WriteLine("### successfully bought a new ship!!! ##");
                        }else
                        {
                            Console.WriteLine("### Cannot spend that amount of gold ##");
                        }
                    }else
                    {
                        Console.WriteLine("### Not a number!!! ##");
                        break;
                    }
                    Console.WriteLine();
                    break;
                case ConsoleKey.C:
                    Console.WriteLine();
                    SpaceSystem tempSpaceSystem = servClient1.GetSystem();
                    if (tempSpaceSystem != null)
                    {
                        Console.WriteLine("### System " + tempSpaceSystem.Name + ", distance: " + tempSpaceSystem.BaseDistance + " ##");
                        if (_starships.Count != 0)
                        {
                            Console.WriteLine("### Ships ready to launch:" + _starships.Count);
                            Console.WriteLine("### Choose ship by typing its number, or leave by typing E:");
                            int i = 0;
                            foreach(Starship s in _starships)
                            {
                                i++;
                                Console.Write(i+". "+s.ShipPower+", ");
                                foreach(Person p in s.Crew)
                                {
                                    Console.Write(p.Name+" "+p.Nick+" "+p.Age+", ");
                                }
                                Console.WriteLine();
                            }

                            // Chosing ship to be send to system
                            int choosenShip;
                            ConsoleKeyInfo choosenShipString = Console.ReadKey();
                            if (int.TryParse(choosenShipString.KeyChar.ToString(), out choosenShip))
                            {
                                if (choosenShip > 0 && choosenShip <= _starships.Count)
                                {
                                    Starship sentStarship = servClient1.SendStarship(_starships.ToArray()[choosenShip-1], tempSpaceSystem.Name);
                                    if (sentStarship.Gold != 0)
                                    {
                                        _gold += sentStarship.Gold;
                                        sentStarship.Gold = 0;
                                        if (sentStarship.Crew.Count() > 0)
                                        {
                                            _starships.Add(sentStarship);
                                        }
                                    }
                                    _starships.RemoveAt(choosenShip-1);
                                }
                                else
                                {
                                    Console.WriteLine("### No ship of that number ##");
                                    break;
                                }
                            }
                            else
                            {
                                if (choosenShipString.Key == ConsoleKey.E)
                                {
                                    break;
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("### No starships ##");
                            break;
                        } 
                    }
                    else
                    {
                        Console.WriteLine("### No systems left ##");
                        _anySystem = false;
                        break;
                    }
                    
                    break;
                case ConsoleKey.D:
                    if(_anySystem == false)
                    {
                        Console.WriteLine("### Congratulations You have won ##");
                    }
                    else
                    {
                        Console.WriteLine("### Sorry, You have lost ##");
                    }
                    Console.ReadLine();
                    System.Environment.Exit(1);
                    break;
                case ConsoleKey.Escape:
                    System.Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("### There is no option like this ##");
                    break;
            }
            Console.WriteLine("### Press Any key to go back to menu ##");
            Console.ReadKey();
            Console.Clear();
            Menu();
        }
    }
}
