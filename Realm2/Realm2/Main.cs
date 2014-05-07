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
            Battle,
            AwaitingInput,
            GettingPlayerInfo,
            AskingPassword,
            AskingFinalPassword,
            Dead,
            PreBattle,
            PreFinalBattle,
            End
        }
        public GameState gm;
        public Player player;
        public Random rand;
        public Enemy enemy;
        MainWindow mw;
        BackpackWindow bw;
        ClassRaceChoiceWindow cw;
        public Main(MainWindow mainWindow, BackpackWindow bpwindow, ClassRaceChoiceWindow crcwindow)
        {
            mw = mainWindow;
            bw = bpwindow;
            cw = crcwindow;
            player = new Player();
            rand = new Random();
        }
        public void HandleInput(string input)
        {
            switch(gm)
            {
                case GameState.GettingPlayerInfo:
                    player.name = mw.inputText.Text;
                    mw.mainText.AppendText("Welcome, ", "Black", true);
                    mw.mainText.AppendText(player.name, "CadetBlue", true);
                    mw.mainText.AppendText(" to Realm 2.", "Black");
                    cw.Show();
                    break;
            }
        }
        public void addBackpackItem(Item i)
        {

        }
    }
}
