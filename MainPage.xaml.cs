namespace _1projekt
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        string text = "";

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Painettu {count} kerran";
            else
                if (count<10) {text = "1.----numbers";}
                else if (count < 20) { text = "2.---- numbers"; }
                else if (count < 30) { text = "3.---- numbers"; }
                else if (count < 40) { text = "4.---- numbers"; }
                else if (count < 50) { text = "5.---- numbers"; }
                CounterBtn.Text = $"Painettu {count} kertaa {text}";
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
