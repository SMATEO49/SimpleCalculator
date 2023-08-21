using System.Globalization;

namespace SimpleCalculator
{
    public partial class MainPage : ContentPage
    {
        private int currentState = 0;
        private string operatorMath, firstString, secondString;
        private double procentor;
        private bool ujemna, procentowy;

        readonly NumberFormatInfo culture = Thread.CurrentThread.CurrentCulture.NumberFormat;

        public MainPage()
        {
            InitializeComponent();
            OnClear(this, null);
        }

        void OnNumberSelection(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string btnPressed = button.Text;

            if (currentState == 0 & btnPressed != "0" & btnPressed != "00")
            {
                currentState = 1;
                if ( ujemna )
                {
                    firstString = "-";
                }
                else
                {
                    firstString = string.Empty;
                }
            }

            if (currentState == 2 & btnPressed != "0" & btnPressed != "00")
            {
                currentState = 3;
            }



            if (currentState == 1)
            {
                if (firstString == "0")
                {
                    firstString = string.Empty;
                }
                firstString += btnPressed;
                if (double.TryParse(firstString, culture, out double number))
                {
                    this.result.Text = number.ToString(culture);
                }
            }
            if (currentState == 3)
            {
                secondString += btnPressed;
                if (double.TryParse(secondString, culture, out double number))
                {
                    this.result.Text = number.ToString(culture);
                }
            }
            this.calculation.Text = firstString + operatorMath + secondString;
        }


        void OnClear(object sender, EventArgs e)
        {
            procentowy = false;
            procentor = 0;
            secondString = string.Empty;
            firstString = "0";
            currentState = 0;
            operatorMath = string.Empty;
            ujemna = false;
            this.result.Text = "0";
            this.calculation.Text = firstString;
        }

        void OnPercent(object sender, EventArgs e)
        {
            var result = 0.0;
            if (currentState == 3)
            {
                if (double.TryParse(firstString, culture, out double numberA) & double.TryParse(secondString, culture, out double numberB))
                {
                    numberB /= 100;
                    if (operatorMath == "+" | operatorMath == "-")
                    {
                        procentor = Calculate.DoCalculation(numberA, numberB, "×");
                        result = Calculate.DoCalculation(numberA, procentor, operatorMath);
                    }
                    else
                    {

                        procentor = numberB;
                        result = Calculate.DoCalculation(numberA, procentor, operatorMath);
                    }
                    

                    this.calculation.Text = firstString + operatorMath + secondString + "%";
                    firstString = result.ToString(culture);
                    this.result.Text = firstString;
                    secondString = string.Empty;
                    currentState = 2;
                    procentowy = true;
                }
            }
            else
            {
                operatorMath = string.Empty;
                this.result.Text = "0";
                currentState = 0;
                firstString = string.Empty;
                secondString = string.Empty;
                this.calculation.Text = "0";
            }
        }

        void OnColcon(object sender, EventArgs e)
        {
            string colconed = this.result.Text;
            string dec = culture.NumberDecimalSeparator;
            int place;

            place = colconed.IndexOf(dec);

            if (place > 0)
            {
                colconed = colconed.Remove(colconed.IndexOf(dec), 1);
            }
            this.result.Text = string.Empty;
            if (double.TryParse(colconed, out double numberC))
            {
                
                this.result.Text += numberC.ToString(culture);
                this.result.Text += dec;
            }
        }

        void OnOperatorSelect(object sender, EventArgs e)//gotowy
        {
            Button button = (Button)sender;
            string btnPressed = button.Text;
            if (currentState == 0 & btnPressed == "-")
            {
                
                if (this.result.Text == "0")
                {
                    firstString = "-0";
                    this.result.Text = firstString;
                    ujemna = true;
                }
                else
                {
                    firstString = "0";
                    this.result.Text = firstString;
                    ujemna = false;
                }
                this.calculation.Text = firstString;
            }
            else
            {
                if (currentState == 3)//obliczenie przyciskeim dzialania Ans continue
                {
                    if(double.TryParse(firstString, culture, out double numberA) & double.TryParse(secondString, culture, out double numberB))
                    {
                        var result = Calculate.DoCalculation(numberA, numberB, operatorMath);
                        operatorMath = btnPressed;
                        firstString = result.ToString(culture);
                        secondString = string.Empty;
                        this.result.Text = firstString;
                    }
                }
                if (currentState == 2 | currentState == 1)//zmiana dzialania
                {
                    operatorMath = btnPressed;
                    this.result.Text = "0";
                    currentState = 2;
                    secondString = string.Empty;
                    this.calculation.Text = firstString + operatorMath;
                }
            }            
        }

        void OnCalculate(object sender, EventArgs e)
        {
            if (currentState == 3 | (currentState == 2 & !secondString.Equals(string.Empty, StringComparison.Ordinal) & procentowy == false ))
            {
                if (double.TryParse(firstString, culture, out double numberA) & double.TryParse(secondString, culture, out double numberB))
                {
                    var result = Calculate.DoCalculation(numberA, numberB, operatorMath);
                    firstString = result.ToString(culture);
                    this.result.Text = firstString;
                    currentState = 2;
                }
                this.calculation.Text = firstString;
            }
            if (procentowy == true)
            {
                if (double.TryParse(firstString, culture, out double numberA))
                {
                    var result = Calculate.DoCalculation(numberA, procentor, operatorMath);

                    this.calculation.Text = firstString + operatorMath + procentor;
                    firstString = result.ToString(culture);
                    this.result.Text = firstString;
                    secondString = string.Empty;
                    currentState = 2;
                    procentowy = true;
                }
            }

        }

        void OnDelete(object sender, EventArgs e)
        {
            int len;
            if (currentState == 1)
            {
                len = firstString.Length;
                if (ujemna == false)
                {
                    if (len > 1)
                    {
                        firstString = firstString.Remove(len - 1, 1);
                    }
                    else
                    {
                        currentState = 0;
                        firstString = "0";
                    }
                }
                else
                {
                    if (len > 2)
                    {
                        firstString = firstString.Remove(len - 1, 1);
                    }
                    if (len == 2)
                    {
                        firstString = "-0";
                    }
                }
                this.result.Text = firstString;
            }
            
            if (currentState == 2)
            {
                operatorMath = string.Empty;
                currentState = 1;
                this.result.Text = firstString;
            }

            if (currentState == 3)
            {
                len = secondString.Length;
                if (len > 1)
                {
                   secondString = secondString.Remove(len - 1, 1);
                }
                else
                {
                    currentState = 2;
                    secondString = string.Empty;
                }
                this.result.Text = secondString;
                
            }
            this.calculation.Text = firstString + operatorMath + secondString;
        }

    }
}