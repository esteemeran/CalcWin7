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
        public double memory = 0;
        public double currOp = 0;

        public enum State { FirstOp, SecondOp };
        public State currState = State.FirstOp;
        
        public enum Sign { None, Plus, Minus, Power, Devide}; //backspace, clean curr, clean all, MC, MR, MS, MP, MM, Negate, Sqrt, Persent, Reciproc
        public Sign currSign = Sign.None;

        public enum TextState { Clear, Res, Enter};
        public TextState IsOver = TextState.Clear;

        public char DotCh = ',';
        public MainWindow()
        {
            InitializeComponent();
        }

        private double getValue()
        {
            return double.Parse(textBox.Text);
        }
        private void setValue(double x)
        {
            textBox.Text = x.ToString();
        }

        private void btNum_Click(object sender, RoutedEventArgs e)
        {
            currOp = getValue();
            if (IsOver == TextState.Clear || IsOver == TextState.Res || currOp == 0)
            {
                textBox.Text = (sender as Button).Content.ToString();
                IsOver = TextState.Enter;
            }
            else
                textBox.Text += (sender as Button).Content.ToString();
            //currOp = getValue();
        }
        private void BtDot_Click(object sender, RoutedEventArgs e)
        {
            if (!textBox.Text.Contains(DotCh)) textBox.Text += DotCh;
        }

        private void BtPlus_Click(object sender, RoutedEventArgs e)
        {
            currOp = getValue();
            if (currState == State.FirstOp)
            {
                accumulator = currOp;
                setValue(accumulator);
                currState = State.SecondOp;
                IsOver = TextState.Res;
            }
            else
            {
                accumulator += currOp;
                setValue(accumulator);
                IsOver = TextState.Clear;
            }
            history.Text += currOp + " + " ;
            currSign = Sign.Plus;
        }
        private void BtMinus_Click(object sender, RoutedEventArgs e)
        {
            currOp = getValue();
            if (currState == State.FirstOp)
            {
                accumulator = currOp;
                setValue(accumulator);
                currState = State.SecondOp;
                IsOver = TextState.Res;
            }
            else
            {
                accumulator -= currOp;
                setValue(accumulator);
                IsOver = TextState.Clear;
            }
            history.Text += currOp + " - ";
            currSign = Sign.Minus;
        }
        private void BtPower_Click(object sender, RoutedEventArgs e)
        {
            currOp = getValue();
            if (currState == State.FirstOp)
            {
                accumulator = currOp;
                setValue(accumulator);
                currState = State.SecondOp;
                IsOver = TextState.Res;
            }
            else
            {
                accumulator *= currOp;
                setValue(accumulator);
                IsOver = TextState.Clear;
            }
            history.Text += currOp + " * ";
            currSign = Sign.Power;
        }
        private void BtDevide_Click(object sender, RoutedEventArgs e)
        {
            currOp = getValue();
            if (currOp == 0)
            {
                IsOver = TextState.Clear;
                textBox.Text = "Impossible";
            }
            else
            {
                if (currState == State.FirstOp)
                {
                    accumulator = currOp;
                    setValue(accumulator);
                    currState = State.SecondOp;
                    IsOver = TextState.Res;
                }
                else
                {
                    accumulator /= currOp;
                    setValue(accumulator);
                    IsOver = TextState.Clear;
                }
                history.Text += currOp + " / ";
                currSign = Sign.Devide;
            }
        }
        
        private void BtEqual_Click(object sender, RoutedEventArgs e)
        {
            if (currState == State.SecondOp)
                currOp = getValue();

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
            setValue(accumulator);
        }


        private void BtReciproc_Click(object sender, RoutedEventArgs e)
        {
            double t = getValue();
            if(t == 0)
            {
                IsOver = TextState.Clear;
                textBox.Text = "Impossible";
            }
            else
            {
                setValue(1 / t);
                history.Text += "reciproc(" + t + ")";
            }
        }

        private void BtPersent_Click(object sender, RoutedEventArgs e)
        {
            currOp = getValue();
            setValue(accumulator * currOp / 100);
        }

        private void BtSqrt_Click(object sender, RoutedEventArgs e)
        {
            double t = getValue();
            setValue(Math.Sqrt(t));
            history.Text += "sqrt(" + t + ")";
        }

        private void BtNegate_Click(object sender, RoutedEventArgs e)
        {
            double t = getValue();
            setValue(0-t);
           // history.Text += "reciproc(" + t + ")";
        }

        private void BtBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length == 1) textBox.Text = "0";
            else if (!textBox.Text.Contains("E")) textBox.Text = textBox.Text.Remove(textBox.Text.Length);
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

            setValue(accumulator);
            history.Text = "";
        }


        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Divide: BtDevide_Click(sender, e); break;
                case Key.Multiply: BtPower_Click(sender, e); break;
                case Key.Subtract: BtMinus_Click(sender, e); break;
                case Key.Add: BtPlus_Click(sender, e); break;
                case Key.Enter: BtEqual_Click(sender, e); break;
            }
            if (e.Key < Key.D0 || e.Key > Key.D9)
            {
                // Determine whether the keystroke is a number from the keypad.
                if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
                {
                    // Determine whether the keystroke is a backspace.
                    if (e.Key != Key.Back || e.Key != Key.Delete || e.Key == Key.Space || e.Key != Key.Decimal)
                    {
                        // A non-numerical keystroke was pressed.
                        // Set the flag to true and evaluate in KeyPress event.
                        e.Handled = true; // отклоняем ввод;
                    }
                }
            }

        }


        private void BtMClean_Click(object sender, RoutedEventArgs e)
        {
            memory = 0;
            MemoryFlag.Text = "";
        }

        private void BtMR_Click(object sender, RoutedEventArgs e)
        {
            setValue(memory);
        }

        private void BtMS_Click(object sender, RoutedEventArgs e)
        {
            memory = getValue();
            MemoryFlag.Text = "M";
        }

        private void BtMPlus_Click(object sender, RoutedEventArgs e)
        {
            memory += getValue();
        }

        private void BtMMinus_Click(object sender, RoutedEventArgs e)
        {
            memory -= getValue();
        }

    }
}
