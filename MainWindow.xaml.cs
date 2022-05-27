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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double accumulator = 0;
        public double currOp = 0;

        public enum State { FirstOp, SecondOp };
        public State currState = State.FirstOp;
        
        public enum Sign { None, Plus, Minus};
        public Sign currSign = Sign.None;

        public bool IsOver = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btNum_Click(object sender, RoutedEventArgs e)
        {
            if (IsOver)
            {
                textBox.Text = (sender as Button).Content.ToString();
                IsOver = false;
            }
            else
                textBox.Text += (sender as Button).Content.ToString();
            currOp = double.Parse(textBox.Text);
        }

        private void BtPlus_Click(object sender, RoutedEventArgs e)
        {
            if(currState == State.FirstOp)
            {
                accumulator = double.Parse(textBox.Text);
                textBox.Text = "";
                currState = State.SecondOp;
            }
            else
            {
                accumulator += currOp;
                textBox.Text = accumulator.ToString();                
            }
            history.Text += currOp.ToString() + " + " ;
        }

        private void BtEqual_Click(object sender, RoutedEventArgs e)
        {
            IsOver = true;
            currState = State.FirstOp;
            history.Text = "";
            textBox.Text = accumulator.ToString();
        }
    }
}
