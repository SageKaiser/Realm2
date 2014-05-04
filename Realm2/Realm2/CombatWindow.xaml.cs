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
        public CombatWindow(Enemy e)
        {
            foreach (Ability a in Program.main.player.combatAbilities)
                abilityPicker.Items.Add(a.name);
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (enemy.hp > 0)
                e.Cancel = true;
        }

        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            if (abilityPicker.SelectedIndex != -1)
            {
                Type etype = Type.GetType("Realm2." + abilityPicker.SelectedItem);
                Ability a = (Ability)Activator.CreateInstance(etype);
                a.Execute(enemy);
            }
                
        }
    }
}
