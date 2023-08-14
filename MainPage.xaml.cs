using System.Globalization;

namespace SimpleCalculator
{
    public partial class MainPage : ContentPage
    {
        int currentState = 1;
        string operatorMath;
        double firstNum, secondNum;

        NumberFormatInfo culture = Thread.CurrentThread.CurrentCulture.NumberFormat;

        public MainPage()
        {
            InitializeComponent();
            onClear(this, null);
        }

        void onNumberSelection(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string btnPressed = button.Text;

            if (this.result.Text == "0" || currentState < 0)
            {
                this.result.Text = string.Empty;
                if (btnPressed == "00") { btnPressed = "0"; }
                if (currentState < 0) { currentState *= -1; }
            }

            this.result.Text += btnPressed;

            double number;
            if (double.TryParse(this.result.Text, culture, out number))
            {
                this.result.Text = number.ToString(culture);
                if (currentState == 1)
                {
                    firstNum = number;
                }
                else
                {
                    secondNum = number;
                }
            }
        }


        void onClear(object sender, EventArgs e)
        {
            firstNum = 0;
            secondNum = 0;
            currentState = 1;
            this.result.Text = "0";
        }

        void onPercent(object sender, EventArgs e)
        {
            double numberP;
            if (double.TryParse(this.result.Text, culture, out numberP))
            {
                numberP /= 100;
                this.result.Text = numberP.ToString(culture);
            }
        }

        void onColcon(object sender, EventArgs e)
        {
            string colconed = this.result.Text;
            string dec = culture.NumberDecimalSeparator;
            int place = -1;

            place = colconed.IndexOf(dec);

            if (place > 0)
            {
                colconed = colconed.Remove(colconed.IndexOf(dec), 1);
            }
            this.result.Text = string.Empty;
            double numberC;
            if (double.TryParse(colconed, out numberC))
            {
                
                this.result.Text += numberC.ToString(culture);
                this.result.Text += dec;
            }
        }

        void onOperatorSelect(object sender, EventArgs e)
        {
            currentState = -2;
            Button button = (Button)sender;
            string btnPressed = button.Text;
            operatorMath = btnPressed;
        }

        void onCalculate(object sender, EventArgs e)
        {
            if (currentState == 2)
            {
                var result = Calculate.DoCalculation(firstNum, secondNum, operatorMath);
                this.result.Text = result.ToString(culture);
                firstNum = result;
                currentState = -1;
            }
        }

        void onDelete(object sender, EventArgs e)
        {
            string newResult = this.result.Text;
            int len = newResult.Length;
            string outPut = "0";

            if (len > 1)
            {
                outPut = newResult.Remove(len - 1, 1);
            }
            this.result.Text = string.Empty;
            this.result.Text += outPut;
        }

    }
}