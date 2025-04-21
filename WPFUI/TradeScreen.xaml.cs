using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Engine.Models;
using Engine.ViewModels;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for TradeScreen.xaml
    /// </summary>
    public partial class TradeScreen : Window
    {
        public GameSession Session => DataContext as GameSession;

        public TradeScreen()
        {
            InitializeComponent();
        }

        private void OnClick_Sell(object sender, RoutedEventArgs e)
        {
            GameItem item = ((FrameworkElement)sender).DataContext as GameItem;

            if (item != null)
            {
                GameItem tempItem;
                if (item is Weapon)
                {
                    tempItem = (item as Weapon).Clone();
                }
                else
                {
                    tempItem = item.Clone();
                }
                tempItem.Quantity = 1;
                Session.CurrentPlayer.Gold += item.Price;
                Session.CurrentTrader.AddItemToInventory(tempItem);
                Session.CurrentPlayer.RemoveItemFromInventory(tempItem);
            }
        }

        private void OnClick_Buy(object sender, RoutedEventArgs e)
        {
            GameItem item = ((FrameworkElement)sender).DataContext as GameItem;

            if (item != null)
            {
                GameItem tempItem;
                if (item is Weapon)
                {
                    tempItem = (item as Weapon).Clone();
                }
                else
                {
                    tempItem = item.Clone();
                }
                tempItem.Quantity = 1;
                if (Session.CurrentPlayer.Gold >= item.Price)
                {
                    Session.CurrentPlayer.Gold -= item.Price;
                    Session.CurrentTrader.RemoveItemFromInventory(tempItem);
                    Session.CurrentPlayer.AddItemToInventory(tempItem);

                    if (item.ItemTypeID == 2001)
                    {
                        Session.CurrentPlayer.HitPoints = Session.CurrentPlayer.MaximumHitPoints;
                        MessageBox.Show("You restored to full health!");
                    }
                }
                else
                {
                    MessageBox.Show("You do not have enough gold");
                }
            }
        }

        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
