using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Realm2
{
    /// <summary>
    /// Interaction logic for CombatWindow.xaml
    /// </summary>
    public partial class CombatWindow : Window
    {
        public Enemy enemy;
        public bool isPlayerTurn;
        public CombatWindow(Enemy e)
        {
            InitializeComponent();
            Program.main.mainWindow.IsEnabled = false;
            enemy = e;
            abilityPicker.ItemsSource = Program.main.player.combatAbilities;
            write("You have entered combat with a ", "Black", true);
            write(enemy.name, "LawnGreen", true);
            write(".", "Black", true);
            enemyName.Content = enemy.name;
            writeEnemyStats();
            writePlayerStats();
            isPlayerTurn = Program.main.player.spd >= enemy.spd;
            if (isPlayerTurn)
                goButton.IsEnabled = true;
            else
            {
                goButton.IsEnabled = false;
                if(enemy.canAttack)
                    enemy.Attack(Program.main.player);
                isPlayerTurn = true;
                goButton.IsEnabled = true;
            }
        }
        private void writePlayerStats()
        {
            pStats.Text = "\r\n\r\n";
            pStats.Text += "Health: " + Program.main.player.hp + "/" + Program.main.player.maxhp;
            pStats.Text += "\r\nAttack: " + Program.main.player.atk;
            pStats.Text += "\r\nDefense: " + Program.main.player.def;
            pStats.Text += "\r\nSpeed: " + Program.main.player.spd;
            pStats.Text += "\r\nIntelligence: " + Program.main.player.intl;
        }
        private void writeEnemyStats()
        {
            eStats.Text = "\r\n\r\n";
            eStats.Text += "Health: " + enemy.hp + "/" + enemy.maxhp;
            eStats.Text += "\r\nAttack: " + enemy.atk;
            eStats.Text += "\r\nDefense: " + enemy.def;
            eStats.Text += "\r\nSpeed: " + enemy.spd;
            eStats.Text += "\r\nIntelligence: " + enemy.intl;
        }
        private void write(string text, string color, bool sameLine)
        {
            combatText.AppendText(text, color, sameLine);
        }
        private void write(string text, string color)
        {
            combatText.AppendText(text, color);
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (enemy.hp > 0 && Program.main.player.hp > 0)
                e.Cancel = true;
        }
        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            if (isPlayerTurn)
            {
                foreach (StatusEffect s in Program.main.player.effects)
                    s.ApplyEffect(Program.main.player);
                if (Program.main.player.canAttack)
                {
                    int prevehp = enemy.hp;
                    ((Ability)abilityPicker.SelectedItem).Execute(enemy);
                    write(Program.main.player.name + " used ", "Black");
                    write(((Ability)abilityPicker.SelectedItem).ToString(), "Indigo", true);
                    write(".", "Black", true);
                    int damage = enemy.canBeHit ? (prevehp - enemy.hp) : 0;
                    write(enemy.name + " took " + damage + " damage.", "Aqua");
                    if (enemy.hp <=  0)
                    {
                        Program.main.write(Program.main.player.name + " has defeated ", "Orange");
                        Program.main.write(enemy.name, "LawnGreen", true);
                        Program.main.write("!", "Orange", true);
                        Program.main.mainWindow.IsEnabled = true;
                        this.Close();
                    }
                }
                writePlayerStats();
                writeEnemyStats();
                isPlayerTurn = false;
                foreach (StatusEffect s in enemy.effects)
                    s.ApplyEffect(enemy);
                if (enemy.canAttack)
                {
                    int prevPlayerhp = Program.main.player.hp;
                    write(enemy.name + " used " + enemy.Attack(Program.main.player) + ".", "Red");
                    int damage = Program.main.player.canBeHit ? (prevPlayerhp - Program.main.player.hp) : 0;
                    write(Program.main.player.name + " took " + damage + " damage.", "Red");
                }
                writeEnemyStats();
                writePlayerStats();
                isPlayerTurn = true;
            }
                
        }

        private void combatText_TextChanged(object sender, TextChangedEventArgs e)
        {
            combatText.ScrollToEnd();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Program.main.mainWindow.IsEnabled = true;
        }
    }
}
