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
            //disable the main window
            Program.main.mainWindow.IsEnabled = false;
            //save the enemy
            enemy = e;
            //bind the Abililty picker ComboBox to the Player's Abilities.
            abilityPicker.ItemsSource = Program.main.player.combatAbilities;
            write("You have entered combat with a ", "Black", true);
            write(enemy.name, "LawnGreen", true);
            write(".", "Black", true);
            //update the enemt name label
            enemyName.Content = enemy.name;
            //if the Player is Human and the Enemy is stronger, buff him
            if (Program.main.player.pRace is Human && enemy.level > Program.main.player.level)
            {
                write(Program.main.player.name + "'s", "Black");
                write(" Spark of Hope ", "Aqua", true);
                write(" has given " + Program.main.player.name + " determination to win!", "Black", true);
                int difference = Convert.ToInt32((enemy.level - Program.main.player.level) * 1.5);
                Program.main.player.atk += difference;
                Program.main.player.def += difference;
                Program.main.player.spd += difference;
                Program.main.player.intl += difference;
            }
            //write the stats
            writeEnemyStats();
            Program.main.writeStats();
            //give the first turn to whoever is faster
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
        private void writeEnemyStats()
        {
            eStats.Text = "";
            eStats.Text += "Level: " + enemy.level;
            eStats.Text += "\r\n\r\nHealth: " + enemy.hp + "/" + enemy.maxhp;
            eStats.Text += "\r\n\r\nAttack: " + enemy.atk;
            eStats.Text += "\r\n\r\nDefense: " + enemy.def;
            eStats.Text += "\r\n\r\nSpeed: " + enemy.spd;
            eStats.Text += "\r\n\r\nIntelligence: " + enemy.intl;
        }
        private void write(string text, string color, bool sameLine) { combatText.AppendText(text, color, sameLine); }
        private void write(string text, string color) { combatText.AppendText(text, color); }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if neither the Enemy nor the Player are dead, stop the window from closing
            if (enemy.hp > 0 && Program.main.player.hp > 0)
                e.Cancel = true;
        }
        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            if (isPlayerTurn)
            {
                //apply the status effects to the player. If they've expired, get rid of them
                for (int i = 0; i < Program.main.player.effects.Count; i++ )
                {
                    if (Program.main.player.effects[i].turns >= 0)
                    {
                        Program.main.player.effects[i].Expire();
                        Program.main.player.effects.Remove(Program.main.player.effects[i]);
                    }
                    else
                        Program.main.player.effects[i].ApplyEffect(Program.main.player);
                }
                //check to see that nothing's blocking the player from attacking
                if (Program.main.player.canAttack)
                {
                    //use the selected Ability
                    Ability used = (Ability)abilityPicker.SelectedItem;
                    int prevehp = enemy.hp;
                    //change target based on the type of Ability
                    switch (used.TargetType)
                    {
                        case targetType.Enemy:
                            used.Execute(enemy);
                            break;
                        case targetType.Self:
                            used.Execute(Program.main.player);
                            break;
                    }
                    write(Program.main.player.name + " used ", "Black");
                    write(used.name, "Indigo", true);
                    write(".", "Black", true);
                    //set damage to be 0 if the enemy cannot be hit
                    int damage = enemy.canBeHit ? (prevehp - enemy.hp) : 0;
                    write(enemy.name + " took " + damage + " damage.", "Aqua");
                }
                //update stats
                Program.main.writeStats();
                writeEnemyStats();
                isPlayerTurn = false;
                //apply Enemy status effects
                for (int i = 0; i < enemy.effects.Count; i++)
                {
                    if (enemy.effects[i].turns <= 0)
                    {
                        enemy.effects[i].Expire();
                        enemy.effects.Remove(enemy.effects[i]);
                    }
                    else
                        enemy.effects[i].ApplyEffect(enemy);
                }
                //attack the Player
                if (enemy.canAttack)
                {
                    int prevPlayerhp = Program.main.player.hp;
                    write(enemy.name + " used " + enemy.Attack(Program.main.player) + ".", "Red");
                    int damage = Program.main.player.canBeHit ? (prevPlayerhp - Program.main.player.hp) : 0;
                    write(Program.main.player.name + " took " + damage + " damage.", "Red");
                }
                writeEnemyStats();
                Program.main.writeStats();
                isPlayerTurn = true;
            }
                
        }

        private void combatText_TextChanged(object sender, TextChangedEventArgs e) { combatText.ScrollToEnd(); }

        private void Window_Closed(object sender, EventArgs e) 
        { 
            //reenable main window
            Program.main.mainWindow.IsEnabled = true;
            //reset all stats changed from battle
            Program.main.player.atk = Program.main.player.baseattack;
            Program.main.player.def = Program.main.player.basedef;
            Program.main.player.spd = Program.main.player.basespeed;
            Program.main.player.intl = Program.main.player.baseintl;
        }
    }
}
