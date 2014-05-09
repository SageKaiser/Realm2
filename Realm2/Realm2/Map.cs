using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Realm2
{
    public enum Direction
    {
        north,
        south,
        east,
        west
    }
    public class Map
    {
        private List<Place> places; 
        public Place[,] placeMap;

        public int GetSize(int direction)
        {
            if (direction == 0)
                return placeMap.GetUpperBound(0);
            else
                return placeMap.GetUpperBound(1);
        }
        public Tuple<int, int> getCoordsOf(Place p)
        {
            foreach(Place place in placeMap)
                if (place.location == p.location)
                    return place.location;
            return new Tuple<int, int>(-1, -1);
        }
        public Map(int size)
        {
            places = new List<Place>() { };
            placeMap = new Place[size, size];
            for (int h = 0; h < placeMap.GetUpperBound(1); h++ )
            {
                for (int w = 0; w < placeMap.GetUpperBound(0); w++)
                {
                    placeMap[h, w] = new Place();
                    placeMap[h, w].location = new Tuple<int,int>(h, w);
                }
            }
        }
    }
    public class Position
    {
        public int x
        {
            get { return x; }
            set 
            {
                if (value >= Program.main.map.GetSize(1) || value < 0)
                    x = x;
                else
                    x = value;
            }
        }
        public int y
        {
            get { return y; }
            set
            {
                if (value >= Program.main.map.GetSize(0) || value < 0)
                    x = x;
                else
                    x = value;
            }
        }
    }
    public class Place
    {
        public Tuple<int, int> location;
        public string name, desc;
        public List<Command> commands;

        public ObservableCollection<Item> getRelevantItemSet()
        {
            ObservableCollection<Item> items = new ObservableCollection<Item>();
            int numberOfItems = Program.main.rand.Next(0, 6);
            for (int i = 0; i < numberOfItems; i++)
            {

            }
            return items;
        }
        public bool checkCombat()
        {
            Dice d = new Dice();
            if (d.roll(1, 5) == 1)
                return true;
            else
                return false;
        }
        public virtual bool ExecuteCommand(string cmd, string obj)
        {
            cmd = cmd.ToLower();
            obj = obj.ToLower();
            object target = null;
            Type t = Type.GetType("Realm2." + cmd);
            Command command = (Command)Activator.CreateInstance(t);
            switch(obj)
            {
                case "library":
                    target = new Library();
                    break;
                case "merchant":
                    target = new Merchant(getRelevantItemSet());
                    break;
                case "inn":
                    target = new Innkeeper();
                    break;
                case "innkeeper":
                    target = new Innkeeper();
                    break;
            }
            if (commands.Contains(command) && target != null)
            {
                if (command is interact)
                {
                    interact i = (interact)command;
                    i.Execute(target);
                }
                else if (command is go)
                {
                    if (obj != "north" && obj != "south" && obj != "east" && obj != "west")
                        return false;
                    go g = (go)command;
                    g.Execute(Enum.Parse(typeof(Direction), obj));
                }
                return true;
            }
            else
                return false;
        }
    }
    public class SunKingdom : Place
    {
        public SunKingdom()
        {
            name = "Kingdom of the Sun";
            desc = "As you step into the glorious golden gates of the Kingdom of the Sun, you are blinded by the eternal burning star that hangs over the city, eternally bathing it in light. This city has a Merchant, an Inn, and a Library.";
            commands = new List<Command>() { new go(), new interact() };
        }
    }
}
