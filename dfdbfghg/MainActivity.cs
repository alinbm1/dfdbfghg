using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;

namespace nrprim
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        private TextView calculatorText;
        private TextView resultText;
        private LinearLayout v;

        private string[] number = new string[1];

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            v = FindViewById<LinearLayout>(Resource.Id.ll);
            calculatorText = FindViewById<TextView>(Resource.Id.textViewNumar);
            resultText = FindViewById<TextView>(Resource.Id.textViewResult);
            v.SetBackgroundColor(Android.Graphics.Color.Gray);
        }

        [Java.Interop.Export("ButtonClick")]
        public void ButtonClick(View view)
        {
            Button button = (Button)view;
            if ("0123456789".Contains(button.Text))
                AddDigit(button.Text);
            else if ("prim" == button.Text)
                Calculate();
            else
                Reset();
        }

        private void AddDigit(string value)
        {
            if (number[0] == "0")
            {
                number[0] = null;
                calculatorText.Text = "0";
                resultText.Text = "Va rugam introduceti un numar valid.";
                UpdateCalculatorText();
                return;
            }

            number[0] += value;

            UpdateCalculatorText();
        }

        private void Calculate()
        {

            if (number[0] == "0")
            {
                number[0] = null;
                calculatorText.Text = "0";
                resultText.Text = "Va rugam introduceti un numar valid.";
                UpdateCalculatorText();
                return;
            }

            if (number[0] == null)
            {
                resultText.Text = "Numarul nu este prim!";
                UpdateCalculatorText();
                return;
            }

            if (number[0].Length >= 7)
            {
                resultText.Text = "Introduceti un numar mai mic";
                calculatorText.Text = "0";
                UpdateCalculatorText();
                return;
            }

            int prim = int.Parse(number[0]);

            if (IsPrime(prim))
            {
                v.SetBackgroundColor(Android.Graphics.Color.Green);
                resultText.Text = "Numarul este prim!";
            }
            else
            {
                v.SetBackgroundColor(Android.Graphics.Color.Red);
                resultText.Text = "Numarul nu este prim!";
            }
        }

        public static bool IsPrime(int nr)
        {
            if (nr <= 1) return false;
            if (nr == 2) return true;
            if (nr % 2 == 0) return false;

            for (int i = 3; i <= nr / 2; i += 1)
                if (nr % i == 0)
                    return false;

            return true;
        }

        private void Reset()
        {
            number[0] = null;
            resultText.Text = "";
            UpdateCalculatorText();
            v.SetBackgroundColor(Android.Graphics.Color.Gray);
        }

        private void UpdateCalculatorText()
        {
            calculatorText.Text = $"{number[0]}";
        }
    }
}