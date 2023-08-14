using Microsoft.Maui.Graphics.Text;

namespace SimpleCalculator
{
    public partial class MainPage : ContentPage
    {
        int currentState = 1;
        string operatorMath;
        double firstNum, secondNum;

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
            if(double.TryParse(this.result.Text, out number))
            {
                this.result.Text = number.ToString("N0");
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

        void onPercent(object sender, EventArgs e) { }

        void onColcon(object sender, EventArgs e) { }

        void onOperatorSelect(object sender, EventArgs e)
        {
            currentState = -2;
            Button button = (Button)sender;
            string btnPressed = button.Text;
            operatorMath = btnPressed;
        }

        void onCalculate(object sender, EventArgs e)
        {
            if(currentState == 2)
            {
                var result = Calculate.DoCalculation(firstNum, secondNum, operatorMath);
                this.result.Text = result.ToString();
                firstNum = result;
                currentState = -1;
            }
        }

        void onDelete(object sender, EventArgs e)
        {
            string newResult = this.result.Text;
            newResult.Remove(newResult.Length - 1, 1);
            this.result.Text = string.Empty;
            this.result.Text += newResult; 
        }

    }
}