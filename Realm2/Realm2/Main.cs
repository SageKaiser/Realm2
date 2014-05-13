using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public enum GameState
    {
        Main,
        GettingPlayerInfo,
        SunPalace,
        Frozen,
        Dead
    }
    public class Main
    {
        public GameState gm;
        public Player player;
        public Map map;
        public List<Book> mainBookList;
        public List<Item> mainItemList;
        public List<PlayerClass> mainClassList;
        public List<Race> mainRaceList;
        public MainWindow mainWindow;

        private BackpackWindow bw;
        private ClassRaceChoiceWindow cw;
        public StatWindow sw;
        private Place currentplace;

        /// <summary>
        /// Constructs the Main class.
        /// </summary>
        /// <param name="_mainWindow">Instance of the MainWindow.</param>
        /// <param name="bpwindow">Instance of the BackpackWindow</param>
        /// <param name="crcwindow">Instance of the ClassRaceChoiceWindow</param>
        /// <param name="swindow">Instance of the StatWindow</param>
        public Main(MainWindow _mainWindow, BackpackWindow bpwindow, ClassRaceChoiceWindow crcwindow, StatWindow swindow)
        {
            //save the windows
            mainWindow = _mainWindow;
            bw = bpwindow;
            cw = crcwindow;
            sw = swindow;
            //instantiate the player
            player = new Player();
            //make a new map
            map = new Map(7);

            //these are for data binding in the ClassRaceChoiceWindow. And any other time you might want a list with all of the Classes and Races
            mainClassList = new List<PlayerClass>() { new Knight(), new Lancer(), new Brawler(), new Mage(), new Ranger(), new Rogue(), new BladeDancer(), new Assassin(), new DreadKnight(), new Jester() };
            mainRaceList = new List<Race>() { new Human(), new Elf(), new Dwarf(), new Orc(), new Lycanthrope(), new Halfdragon(), new Revenant(), new Djinn(), new Vampire(), new Demon() };
        }
        /// <summary>
        /// This function is called when the user enters a command.
        /// </summary>
        /// <param name="input">The string to be operated upon.</param>
        public void HandleInput(string input)
        {
            //execute depending on the GameState
            switch (gm)
            {
                case GameState.GettingPlayerInfo:
                    //say hello
                    player.name = mainWindow.inputText.Text;
                    write("Welcome, ", "Black");
                    write(player.name, "MediumOrchid", true);
                    write(" to Realm 2.", "Black", true);
                    cw.Show();
                    mainWindow.IsEnabled = false;
                    break;
                case GameState.Main:
                    //write the current place name and description
                    write("Current Place: " + currentplace.name, "Black");
                    write(currentplace.desc, "Black");
                    writeStats();
                    //execute the command entered
                    currentplace.ExecuteCommand(input.Split()[0], input.Split()[1]);
                    break;
                case GameState.SunPalace:
                    
                case GameState.Dead:
                    //close the game
                    Environment.Exit(0);
                    break;
                case GameState.Frozen:
                    //LEAVE THIS BLANK
                    break;
            }
            //if the Player's position isn't null, set the currentPlace varable to the Player's current location
            if (player.position != null)
                currentplace = map.getPlace(new Tuple<int, int>(player.position.x, player.position.y));
        }
        /// <summary>
        /// Write out the player's stats
        /// </summary>
        public void writeStats()
        {
            sw.statText.Document.Blocks.Clear();
            sw.statText.AppendText(player.name + "(" + player.pRace + ")", "Black", true);
            sw.statText.AppendText("Level " + player.level + " " + player.pClass, "Black");
            sw.statText.AppendText("HP: " + player.hp + "/" + player.maxhp, "Black");
            sw.statText.AppendText("Mana: " + player.mana + "/" + player.maxmana, "Black");
            sw.statText.AppendText("Attack: " + player.atk, "Black");
            sw.statText.AppendText("Defense: " + player.def, "Black");
            sw.statText.AppendText("Speed: " + player.spd, "Black");
            sw.statText.AppendText("Intelligence: " + player.intl, "Black");
        }
        /// <summary>
        /// Write a string to the main text box
        /// </summary>
        /// <param name="input">The string to be written to the box</param>
        /// <param name="color">A color defined by the Color class</param>
        public void write(string input, string color)
        {
            mainWindow.mainText.AppendText(input, color);
        }
        /// <summary>
        /// Write a string to the main text box
        /// </summary>
        /// <param name="input">The string to be written to the box</param>
        /// <param name="color">A color defined by the Color class</param>
        /// <param name="sameLine">If true, the function will not write to the same line as the previous</param>
        public void write(string input, string color, bool sameLine)
        {
            mainWindow.mainText.AppendText(input, color, sameLine);
        }
        /// <summary>
        /// Enter combat with the specified Enemy.
        /// </summary>
        /// <param name="e">The Enemy to be fought.</param>
        public void EnterCombat(Enemy e)
        {
            player.baseattack = player.atk;
            player.basedef = player.def;
            player.basespeed = player.spd;
            player.baseintl = player.intl;
            CombatWindow cw = new CombatWindow(e);
            cw.Show();
        }
        /// <summary>
        /// Focuses the input textbox
        /// </summary>
        public void focusWindow()
        {
            mainWindow.inputText.Focus();
        }
    }
}
