using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CosmicAdventureDTO;

namespace WcfServiceLibrary1
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        private static List<SpaceSystem> _systems = new List<SpaceSystem>();
        public void InitializeGame()
        {
            Random rnd = new Random();
            SpaceSystem tmp;
            for (int i = 0; i < 4; i++)
            {
                tmp = new SpaceSystem();
                tmp.Name = "System numer " + (i + 1);
                tmp.MinShipPower = rnd.Next(10, 41);
                tmp.BaseDistance = rnd.Next(20, 121);
                tmp.Gold = rnd.Next(3000, 7001);
                _systems.Add(tmp);
            }
        }
        public Starship SendStarship(Starship starship, string systemName)
        {
            Boolean systemFound = false;
            foreach (SpaceSystem spaceSys in _systems)
            {
                // Checking if system exists in _systems List
                if (spaceSys.Name == systemName)
                {
                    systemFound = true;
                    foreach (Person p in starship.Crew)
                    {
                        if (starship.ShipPower <= 20)
                        {
                            p.Age += (2 * spaceSys.BaseDistance) / 12;
                        }
                        else if (starship.ShipPower > 20 && starship.ShipPower <= 30)
                        {
                            p.Age += (2 * spaceSys.BaseDistance) / 6;
                        }
                        else if (starship.ShipPower > 30)
                        {
                            p.Age += (2 * spaceSys.BaseDistance) / 4;
                        }
                        // Checking if over 90 years old
                        if (p.Age >= 90)
                        {
                            starship.Crew.Remove(p);
                        }
                    }
                    // Checking if gold has been gathered
                    if (starship.ShipPower >= spaceSys.MinShipPower)
                    {
                        starship.Gold += spaceSys.Gold;
                        // Deleting system from the list bc no gold left
                        _systems.Remove(spaceSys);
                    }
                    // If system has been found, no need to check others
                    break;
                }

            }
            // if no system found then remove whole crew
            if (systemFound == false)
            {
                starship.Crew.Clear();
            }
            return starship;
        }
        public SpaceSystem GetSystem()
        {
            if (_systems.Count != 0)
            {
                return _systems.First();
            }
            else
            {
                return null;
            }
        }
        public Starship GetStarship(int money)
        {
            Random rnd = new Random();
            Starship newStarShip = new Starship();
            newStarShip.Gold = 0;
            newStarShip.Crew = new List<Person>();
            if (money > 1000 && money <= 3000)
            {
                newStarShip.ShipPower = rnd.Next(10, 26);
                newStarShip.Crew.Add(new Person() { Name = "Jerry", Nick = "Buffalo", Age = 20 });
                newStarShip.Crew.Add(new Person() { Name = "John", Nick = "Invincible", Age = 20 });
                newStarShip.Crew.Add(new Person() { Name = "Borch", Nick = "Trzy Kawki", Age = 20 });
            }
            else if (money > 3000 && money <= 10000)
            {
                newStarShip.ShipPower = rnd.Next(20, 36);
                newStarShip.Crew.Add(new Person() { Name = "Jet", Nick = "Li", Age = 20 });
                newStarShip.Crew.Add(new Person() { Name = "Jimmy", Nick = "Scorpion", Age = 20 });
                newStarShip.Crew.Add(new Person() { Name = "Bruce Lee", Nick = "Dragon", Age = 20 });
                newStarShip.Crew.Add(new Person() { Name = "Ip Man", Nick = "Yip", Age = 20 });
            }
            else if (money > 10000)
            {
                newStarShip.ShipPower = rnd.Next(35, 61);
                newStarShip.Crew.Add(new Person() { Name = "Garrus", Nick = "Vakarian", Age = 20 });
                newStarShip.Crew.Add(new Person() { Name = "Shepard", Nick = "ChoosenOne", Age = 20 });
                newStarShip.Crew.Add(new Person() { Name = "Liara", Nick = "T'soni", Age = 20 });
                newStarShip.Crew.Add(new Person() { Name = "Urdnot", Nick = "Wrex", Age = 20 });
            }

            return newStarShip;
        }
    }
}
