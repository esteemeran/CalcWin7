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
        
        public enum Sign { None, Plus, Minus, Power, Devide, MC, MR, MS, MP, MM, Negate, Sqrt, Persent, Reciproc}; //backspace, clean curr, clean all
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
                IsOver = true;
            }
            history.Text += currOp.ToString() + " + " ;
            currSign = Sign.Plus;
        }
        private void BtMinus_Click(object sender, RoutedEventArgs e)
        {
            if (currState == State.FirstOp)
            {
                accumulator = double.Parse(textBox.Text);
                textBox.Text = "";
                currState = State.SecondOp;
            }
            else
            {
                accumulator -= currOp;
                textBox.Text = accumulator.ToString();
                IsOver = true;
            }
            history.Text += currOp.ToString() + " - ";
            currSign = Sign.Minus;
        }
        private void BtPower_Click(object sender, RoutedEventArgs e)
        {
            if (currState == State.FirstOp)
            {
                accumulator = double.Parse(textBox.Text);
                textBox.Text = "";
                currState = State.SecondOp;
            }
            else
            {
                accumulator *= currOp;
                textBox.Text = accumulator.ToString();
                IsOver = true;
            }
            history.Text += currOp.ToString() + " * ";
            currSign = Sign.Power;
        }
        private void BtDevide_Click(object sender, RoutedEventArgs e)
        {
            if (currState == State.FirstOp)
            {
                accumulator = double.Parse(textBox.Text);
                textBox.Text = "";
                currState = State.SecondOp;
            }
            else
            {
                accumulator /= currOp;
                textBox.Text = accumulator.ToString();
                IsOver = true;
            }
            history.Text += currOp.ToString() + " / ";
            currSign = Sign.Devide;
        }

        private void BtEqual_Click(object sender, RoutedEventArgs e)
        {
            IsOver = true;
            currState = State.FirstOp;
            history.Text = "";

            switch(currSign)
            {
                case Sign.Plus: accumulator += currOp; break;
                case Sign.Minus: accumulator -= currOp; break;
                case Sign.Devide: accumulator /= currOp; break;
                case Sign.Power: accumulator *= currOp; break;
            }
            currOp = accumulator;
            textBox.Text = accumulator.ToString();

        }

        private void BtDot_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtReciproc_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
