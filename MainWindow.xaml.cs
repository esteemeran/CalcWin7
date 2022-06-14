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

        public enum TextState { Clear, Res, Enter};
        public TextState IsOver = TextState.Clear;

        public char DotCh = ",";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btNum_Click(object sender, RoutedEventArgs e)
        {
            currOp = double.Parse(textBox.Text);
            if (IsOver == TextState.Clear || IsOver == TextState.Res || currOp == 0)
            {
                textBox.Text = (sender as Button).Content.ToString();
                IsOver = TextState.Enter;
            }
            else
                textBox.Text += (sender as Button).Content.ToString();
            //currOp = double.Parse(textBox.Text);
        }

        private void BtPlus_Click(object sender, RoutedEventArgs e)
        {
            currOp = double.Parse(textBox.Text);
            if (currState == State.FirstOp)
            {
                accumulator = currOp;
                textBox.Text = accumulator.ToString();
                currState = State.SecondOp;
                IsOver = TextState.Res;
            }
            else
            {
                accumulator += currOp;
                textBox.Text = accumulator.ToString();
                IsOver = TextState.Clear;
            }
            history.Text += currOp + " + " ;
            currSign = Sign.Plus;
        }
        private void BtMinus_Click(object sender, RoutedEventArgs e)
        {
            currOp = double.Parse(textBox.Text);
            if (currState == State.FirstOp)
            {
                accumulator = currOp;
                textBox.Text = accumulator.ToString();
                currState = State.SecondOp;
                IsOver = TextState.Res;
            }
            else
            {
                accumulator -= currOp;
                textBox.Text = accumulator.ToString();
                IsOver = TextState.Clear;
            }
            history.Text += currOp + " - ";
            currSign = Sign.Minus;
        }
        private void BtPower_Click(object sender, RoutedEventArgs e)
        {
            currOp = double.Parse(textBox.Text);
            if (currState == State.FirstOp)
            {
                accumulator = currOp;
                textBox.Text = accumulator.ToString();
                currState = State.SecondOp;
                IsOver = TextState.Res;
            }
            else
            {
                accumulator *= currOp;
                textBox.Text = accumulator.ToString();
                IsOver = TextState.Clear;
            }
            history.Text += currOp + " * ";
            currSign = Sign.Power;
        }
        private void BtDevide_Click(object sender, RoutedEventArgs e)
        {
            currOp = double.Parse(textBox.Text);
            if (currState == State.FirstOp)
            {
                accumulator = currOp;
                textBox.Text = accumulator.ToString();
                currState = State.SecondOp;
                IsOver = TextState.Res;
            }
            else
            {
                accumulator /= currOp;
                textBox.Text = accumulator.ToString();
                IsOver = TextState.Clear;
            }
            history.Text += currOp + " / ";
            currSign = Sign.Devide;
        }

        private void BtEqual_Click(object sender, RoutedEventArgs e)
        {
            if (currState == State.SecondOp)
                currOp = double.Parse(textBox.Text);

            IsOver = TextState.Clear;
            currState = State.FirstOp;
            history.Text = "";

            switch(currSign)
            {
                case Sign.Plus: accumulator += currOp; break;
                case Sign.Minus: accumulator -= currOp; break;
                case Sign.Devide: accumulator /= currOp; break;
                case Sign.Power: accumulator *= currOp; break;
            }
            //currOp = accumulator;
            textBox.Text = accumulator.ToString();

        }

        private void BtDot_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtReciproc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtPersent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtSqrt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtNegate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length == 1) textBox.Text = "0";
            else textBox.Text = textBox.Text.Remove(textBox.Text.Length);
        }

        private void BtCleanCurr_Click(object sender, RoutedEventArgs e)
        {
            textBox.Text = "0";
        }

        private void BtCleanAll_Click(object sender, RoutedEventArgs e)
        {
            accumulator = 0;
            currState = State.FirstOp;
            currSign = Sign.None;
            IsOver = TextState.Clear;

            textBox.Text = accumulator.ToString();
            history.Text = "";
        }
    }
}
