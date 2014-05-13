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
        //possible directions
        north,
        south,
        east,
        west
    }
    public class Map
    {
        //list of all places for map generation
        private List<Place> places = new List<Place>() { new SunKingdom() }; 
        //main map (2D array of places)
        public Place[,] placeMap;

        /// <summary>
        /// Faster to type than placeMap.GetUpperBound()
        /// </summary>
        /// <param name="direction">0 for x, 1 for y</param>
        /// <returns>size of map in specified direction</returns>
        public int GetSize(int direction)
        {
            if (direction == 0)
                return placeMap.GetUpperBound(0);
            else
                return placeMap.GetUpperBound(1);
        }
        /// <summary>
        /// Gets the coordinate of the specified Place.
        /// NOTE: DOES NOT WORK WITH DEFAULT PLACE
        /// </summary>
        /// <param name="p">Place that needs locating</param>
        /// <returns>Returns a Tuple of ints containing the coordinates, or (-1, -1) if the place could not be found. </returns>
        public Tuple<int, int> getCoordsOf(Place p)
        {
            foreach(Place place in placeMap)
                if (place.location == p.location)
                    return place.location;
            return new Tuple<int, int>(-1, -1);
        }
        /// <summary>
        /// Given coordinates in Tuple form, returns the Place at that location
        /// </summary>
        /// <param name="location">A Tuple of ints that is the location.</param>
        /// <returns>A Place at the specified location. If the coordinates are outside the bounds of the array, it returns null.</returns>
        public Place getPlace(Tuple<int, int> location)
        {
            try
            {
                return placeMap[location.Item1, location.Item2];
            }
            catch(IndexOutOfRangeException)
            {
                return null;
            }
        }
        /// <summary>
        /// Constructor for Map class. Makes a new map with random Places.
        /// </summary>
        /// <param name="size">THe size (x and y) of the map.</param>
        public Map(int size)
        {
            placeMap = new Place[size, size];
            for (int i = 0; i < places.Count; i++)
            {
                Tuple<int, int> coords = new Tuple<int, int>(Program.random.Next(0, GetSize(0)), Program.random.Next(0, GetSize(1)));
                if (placeMap[coords.Item1, coords.Item2] == null || placeMap[coords.Item1, coords.Item2] is Place)
                {
                    placeMap[coords.Item1, coords.Item2] = places[i];
                    places.Remove(places[i]);
                }
                else
                    i--;
            }
        }
    }
    public class Position
    {
        private int X;
        public int x
        {
            get { return X; }
            set 
            {
                //check if the value is between 0 and the map size
                if (value >= 0 && value < Program.main.map.GetSize(1))
                    X = value;
            }
        }
        private int Y;
        public int y
        {
            get { return Y; }
            set
            {
                //check if the value is between 0 and the map size
                if (value >= 0 && value < Program.main.map.GetSize(0))
                    Y = value;
            }
        }
    }
    public class Place
    {
        //the coordinates of the Place
        public Tuple<int, int> location;
        //the name and descirption(to be typed to the mainwindow)
        public string name, desc;
        //a list of Commands avaiable at this Place
        public List<Command> commands;
        /// <summary>
        /// Gets an Enemy of similar level to the Player.
        /// </summary>
        /// <returns>An Enemy of similar level to the Player</returns>
        private Enemy getRelevantEnemy()
        {
            if (Program.main.player.level < 5)
                return new Slime();
            else if (Program.main.player.level < 10)
                return new List<Enemy>() { new Slime() /*new Enemy()*/}[Program.random.Next(0, 2)];
            else if (Program.main.player.level < 15)
                return new List<Enemy>() { new Slime() /*new Enemy()*/}[Program.random.Next(0, 2)];
            else if (Program.main.player.level < 20)
                return new List<Enemy>() { new Slime() /*new Enemy()*/}[Program.random.Next(0, 2)];
            else
                return new List<Enemy>() { new Slime() /*new Enemy()*/}[Program.random.Next(0, 2)];
            //etc...
        }
        /// <summary>
        /// Gets a collection of Items relevant to the Player's level.
        /// </summary>
        /// <returns>An ObservableCollection of relevant Items.</returns>
        public ObservableCollection<Item> getRelevantItemSet()
        {
            ObservableCollection<Item> items = new ObservableCollection<Item>();
            //gets a random number of Items between 1 and 5.
            int numberOfItems = Program.random.Next(1, 6);
            for (int i = 0; i < numberOfItems; i++)
            {

            }
            return items;
        }
        /// <summary>
        /// 20% of putting the player into combat with a relevant Enemy.
        /// </summary>
        public void checkCombat()
        {
            Dice d = new Dice();
            if (d.roll(1, 5) == 1)
            {
                Enemy e = getRelevantEnemy();
                Program.main.EnterCombat(e);
            }
        }
        /// <summary>
        /// Executes the command given.
        /// </summary>
        /// <param name="cmd">The string to be converted to a Command.</param>
        /// <param name="obj">The object to be executed upon.</param>
        /// <returns>false if it fails, true if it succeeds.</returns>
        public virtual bool ExecuteCommand(string cmd, string obj)
        {
            //convert input to lowercase
            cmd = cmd.ToLower();
            obj = obj.ToLower();
            //set target to null
            object target = null;

            //convert cmd into a Command
            Command command = (Command)Activator.CreateInstance(Type.GetType("Realm2." + cmd));
            //execute depending on what obj is
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
            //if you can use the Command and the target isn't null
            if (commands.Contains(command) && target != null)
            {
                //If the Command is 'interact', interact with the target
                if (command is interact)
                    ((interact)command).Execute(target);

                //otherwise go in the specified direction
                else if (command is go)
                {
                    //return false if it isn't a valid direction
                    if (obj != "north" && obj != "south" && obj != "east" && obj != "west")
                        return false;
                    ((go)command).Execute(Enum.Parse(typeof(Direction), obj));
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
            desc = "As you step into the glorious golden gates of the Kingdom of the Sun, you are blinded by the eternal burning star that hangs over the city, eternally bathing it in light. You see a glowing palace up ahead. This city has a Merchant, an Inn, and a Library.";
            commands = new List<Command>() { new go(), new interact() };
        }
        public override bool ExecuteCommand(string cmd, string obj)
        {
            //if the base ExecuteCommand function fails, try this
            if (!base.ExecuteCommand(cmd, obj))
            {
                obj = obj.ToLower();
                Command command = (Command)Activator.CreateInstance(Type.GetType("Realm2." + cmd));
                
                //check if the command was 'interact palace'
                if (command is interact && obj == "palace")
                {
                    Program.main.gm = GameState.SunPalace;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}
