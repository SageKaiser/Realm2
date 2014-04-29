using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Realm2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !String.IsNullOrEmpty(Main.form.inputText.Text))
                Main.HandleInput(Main.form.inputText.Text);
        }

        public void Application_Idle(object sender, EventArgs e)
        {
            if (hp <= 0)
                Main.gm = Main.GameState.Dead;

            if (Main.slimecounter == 100)
                Main.ach.Get("100slimes");
            if (Main.goblincounter == 100)
                Main.ach.Get("100goblins");
            if (Main.banditcounter == 100)
                Main.ach.Get("100bandits");
            if (Main.drakecounter == 100)
                Main.ach.Get("100drakes");

            Main.Player.levelup();

            Main.Player.applybonus();

            if (Main.Player.hp > Main.Player.maxhp)
                Main.Player.hp = Main.Player.maxhp;

            Item pc = new phantasmal_claymore();
            Item sb = new spectral_bulwark();
            Item ip = new illusory_plate();
            Item vc = new void_cloak();
            if ((Main.Player.backpack.Contains(pc) && Main.Player.backpack.Contains(sb) && Main.Player.backpack.Contains(ip) && Main.Player.backpack.Contains(vc)) || Main.devmode)
            {
                if (!Main.Player.abilities.commandChars.Contains('*'))
                    Main.Player.abilities.AddCommand(new Combat.EndtheIllusion("End the Illusion", '*'));
                Main.ach.Get("set");
            }

            if (Main.gbooks >= 3 && !Main.Player.abilities.commandChars.Contains('@'))
            {
                Interface.type("Having read all of the Ramsay books, you are enlightened in the ways of Gordon Ramsay.");
                Main.Player.abilities.AddCommand(new Combat.HellsKitchen("Hell's Kitchen", '@'));
            }
        }
    }
}
