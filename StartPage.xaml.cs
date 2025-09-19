using MauiApptargv;
// при желании можно добавить:
// using Microsoft.Maui.Controls;
// using Microsoft.Maui.Graphics;

namespace _1projekt;

public partial class StartPage : ContentPage
{
    public List<ContentPage> lehed = new List<ContentPage>()
    {
        new TekstPage(),
        new FigurePage(),
        new TimerPage(),
        new ValgusfoorPage(),
        new DateTimePage(),
        new Lumememm()
    };

    public List<string> tekstid = new List<string>()
    {
        "Tee lahti leht Tekst'ga",
        "Tee lahti Figure leht",
        "Käivita timer page",
        "Käivita valgusfoor page",
        "Ava DateTime leht",
        "Ava lumememm"
    };

    ScrollView sv;
    VerticalStackLayout vsl;

    public StartPage()
    {
        // InitializeComponent(); // не нужен, если без XAML
        Title = "Avaleht";

        vsl = new VerticalStackLayout
        {
            // Был сплошной цвет — сделал мягкий вертикальный градиент
            Background = new LinearGradientBrush(
                new GradientStopCollection
                {
                    new GradientStop(Color.FromRgb(120, 30, 50), 0.0f),
                    new GradientStop(Color.FromRgb(70, 20, 35), 1.0f)
                },
                new Point(0, 0),
                new Point(0, 1)
            ),
            Padding = new Thickness(16, 24, 16, 24),
            Spacing = 12
        };

        for (int i = 0; i < lehed.Count; i++)
        {
            var nupp = new Button
            {
                Text = tekstid[i],
                FontSize = 20,
                // Был плоский цвет — оставим базовый, но добавим обводку и тень ниже
                BackgroundColor = Color.FromRgb(230, 230, 140),
                TextColor = Colors.Black,
                CornerRadius = 18,
                FontFamily = "Lovin Kites 400",
                ZIndex = i,

                // Визуальные мелочи
                Margin = new Thickness(6, 4),
                Padding = new Thickness(18, 14),
                HeightRequest = 56,
                BorderColor = Color.FromRgba(0, 0, 0, 0.10f),
                BorderWidth = 1,
                Shadow = new Shadow
                {
                    Opacity = 0.25f,
                    Radius = 10,
                    Offset = new Point(0, 6)
                },
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start
            };

            // Эффект нажатия — лёгкая анимация масштаба
            nupp.Pressed += (s, e) => nupp.ScaleTo(0.98, 80);
            nupp.Released += (s, e) => nupp.ScaleTo(1.0, 80);

            nupp.Clicked += Nupp_Clicked;
            vsl.Add(nupp);
        }

        // Скролл с упругими краями и скрытыми полосами для аккуратности
        sv = new ScrollView
        {
            Content = vsl,
            Orientation = ScrollOrientation.Vertical,
            VerticalScrollBarVisibility = ScrollBarVisibility.Never
        };

        Content = sv;
    }

    private async void Nupp_Clicked(object? sender, EventArgs e)
    {
        var nupp = (Button)sender;
        await Navigation.PushAsync(lehed[nupp.ZIndex]);
    }
}
