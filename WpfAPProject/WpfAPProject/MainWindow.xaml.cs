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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace WpfAPProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static object locker = new object();

        //create an instance of MainWindow class
        public static MainWindow theMainWindow;

        //2 Threads for each poker player
        static Thread thread1 = new Thread(new ParameterizedThreadStart(DealHand));
        static Thread thread2 = new Thread(new ParameterizedThreadStart(DealHand));
        static Thread thread3 = new Thread(new ParameterizedThreadStart(UpcomingCards));

        static string[] deck = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};



        static int[] player1Cards = new int[2];
        static int[] player2Cards = new int[2];
        static int[] upcomingCards = new int[5];

        public MainWindow()
        {
            InitializeComponent();
            theMainWindow = this;

            thread3.Start();

        }
        static void DealHand(Object o)
        {
            Monitor.Enter(locker);
            
        }
        static void UpComingCards(Object o)
        {
            Monitor.Enter(locker);
            try
            {
                /*Deal Deck*/
                for (int i = 0; i < 5; i++)
                {
                    Random random = new Random();
                    upcomingCards = random.Next(deck.Length);
                    player2Cards[i] = random.Next(deck.Length);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            /*Deal Hand*/
            for (int i = 0; i < 2; i++)
            {
                Random random = new Random();
                player1Cards[i] = random.Next(deck.Length);
                player2Cards[i] = random.Next(deck.Length);
            }

            MainWindow.theMainWindow.Dispatcher.Invoke(new Action(
                                        delegate ()
                                        {
                                            MainWindow.theMainWindow.lblP1Card1.Content = deck[player1Cards[0]];
                                            MainWindow.theMainWindow.lblP1Card2.Content = deck[player1Cards[1]];
                                            MainWindow.theMainWindow.lblP2Card1.Content = deck[player2Cards[0]];
                                            MainWindow.theMainWindow.lblP2Card2.Content = deck[player2Cards[1]];

                                        }
                                        ));
        }
    }
}
