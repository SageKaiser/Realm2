using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public class Main
    {
        public enum GameState
        {
            Main,
            JustMoved,
            PreBattle,
            Battle,
            AwaitingInput,
            GettingPlayerInfo,
            Dead,
            End
        }
        public Random rand;
        public GameState gm;
        public Player player;
        public Map map;
        public List<Book> mainBookList;
        public List<Item> mainItemList;
        public MainWindow mainWindow;

        private Enemy currentEnemy;
        private BackpackWindow bw;
        private ClassRaceChoiceWindow cw;
        public Main(MainWindow _mainWindow, BackpackWindow bpwindow, ClassRaceChoiceWindow crcwindow)
        {
            mainWindow = _mainWindow;
            bw = bpwindow;
            cw = crcwindow;
            player = new Player();
            rand = new Random();
            map = new Map(7);
        }
        public void HandleInput(string input)
        {
            switch(gm)
            {
                case GameState.GettingPlayerInfo:
                    player.name = mainWindow.inputText.Text;
                    write("Welcome, ", "Black", true);
                    write(player.name, "CadetBlue", true);
                    write(" to Realm 2.", "Black");
                    cw.Show();
                    break;
            }
        }
        public void write(string input, string color)
        {
            mainWindow.mainText.AppendText(input, color);
        }
        public void write(string input, string color, bool sameLine)
        {
            mainWindow.mainText.AppendText(input, color, sameLine);
        }
    }
}
