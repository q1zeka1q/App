using System;
using System.Collections.Generic;
using Microsoft.Maui.Layouts;

namespace MauiApptargv;

public partial class DateTimePage : ContentPage
{
    Label mis_on_valitud;
    DatePicker datePicker;
    TimePicker timePicker;
    Picker picker;
    Slider slider;
    Stepper stepper;
    AbsoluteLayout al;

    public DateTimePage()
    {
        BackgroundColor = Color.FromRgb(245, 246, 250); // мягкий светлый фон

        mis_on_valitud = new Label
        {
            Text = "Siin kuvatakse valitud kuupäev või kellaaeg",
            FontSize = 20,
            TextColor = Colors.Black,
            FontFamily = "Lovin Kites 400",
            HorizontalTextAlignment = TextAlignment.Center
        };

        datePicker = new DatePicker
        {
            FontSize = 20,
            TextColor = Colors.Black,
            BackgroundColor = Colors.White,
            FontFamily = "Lovin Kites 400",
            MinimumDate = DateTime.Now.AddDays(-7),
            MaximumDate = new DateTime(2100, 12, 31),
            Date = DateTime.Now,
            Format = "D"
        };
        datePicker.DateSelected += Kuupäeva_valimine;

        timePicker = new TimePicker
        {
            FontSize = 20,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            FontFamily = "Lovin Kites 400",
            Time = new TimeSpan(12, 0, 0),
            Format = "T"
        };
        timePicker.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == TimePicker.TimeProperty.PropertyName)
                mis_on_valitud.Text = $"Valisite kellaaja: {timePicker.Time:hh\\:mm}";
        };

        picker = new Picker
        {
            Title = "Vali üks",
            FontSize = 20,
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            FontFamily = "Lovin Kites 400",
            ItemsSource = new List<string> { "üks", "kaks", "kolm", "neli", "viis" }
        };
        picker.SelectedIndexChanged += (s, e) =>
        {
            if (picker.SelectedIndex != -1 && picker.SelectedItem is string valik)
                mis_on_valitud.Text = $"Valisite: {valik}";
        };

        slider = new Slider
        {
            Minimum = 0,
            Maximum = 100,
            Value = 50,
            BackgroundColor = Colors.White,
            ThumbColor = Colors.Red,
            MinimumTrackColor = Colors.Green,
            MaximumTrackColor = Colors.Blue
        };
        slider.ValueChanged += (s, e) =>
        {
            mis_on_valitud.FontSize = e.NewValue;  // 0..100 → размер шрифта
            mis_on_valitud.Rotation = e.NewValue;  // 0..100° → поворот
        };

        stepper = new Stepper
        {
            Minimum = 0,
            Maximum = 100,
            Value = 20,
            Increment = 1,
            BackgroundColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center
        };
        stepper.ValueChanged += (s, e) =>
        {
            mis_on_valitud.Text = $"Stepper value: {e.NewValue}";
        };

        al = new AbsoluteLayout();

        // добавляем ВСЕ элементы
        al.Children.Add(mis_on_valitud);
        al.Children.Add(datePicker);
        al.Children.Add(timePicker);
        al.Children.Add(picker);
        al.Children.Add(slider);
        al.Children.Add(stepper); 

        // позиционирование (X, Y, Width, Height в долях; -1 = авто-высота)
        AbsoluteLayout.SetLayoutBounds(mis_on_valitud, new Rect(0.5, 0.10, 0.9, -1));
        AbsoluteLayout.SetLayoutFlags(mis_on_valitud, AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.YProportional);

        AbsoluteLayout.SetLayoutBounds(datePicker, new Rect(0.5, 0.28, 0.9, -1));
        AbsoluteLayout.SetLayoutFlags(datePicker, AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.YProportional);

        AbsoluteLayout.SetLayoutBounds(timePicker, new Rect(0.5, 0.46, 0.9, -1));
        AbsoluteLayout.SetLayoutFlags(timePicker, AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.YProportional);

        AbsoluteLayout.SetLayoutBounds(picker, new Rect(0.5, 0.64, 0.9, -1));
        AbsoluteLayout.SetLayoutFlags(picker, AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.YProportional);

        AbsoluteLayout.SetLayoutBounds(slider, new Rect(0.5, 0.80, 0.9, -1));
        AbsoluteLayout.SetLayoutFlags(slider, AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.YProportional);

        AbsoluteLayout.SetLayoutBounds(stepper, new Rect(0.5, 0.92, 0.9, -1));
        AbsoluteLayout.SetLayoutFlags(stepper, AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.XProportional | AbsoluteLayoutFlags.YProportional);

        Content = al;

        // единый «карточный» стиль
        ApplyCardStyle(datePicker);
        ApplyCardStyle(timePicker);
        ApplyCardStyle(picker);
        ApplyCardStyle(slider);
        ApplyCardStyle(stepper);
    }

    void ApplyCardStyle(View v)
    {
        v.Margin = new Thickness(16, 8);
        v.Shadow = new Shadow { Opacity = 0.15f, Radius = 8, Offset = new Point(0, 3) };
        v.HeightRequest = 48;
    }

    private void Kuupäeva_valimine(object sender, DateChangedEventArgs e)
    {
        mis_on_valitud.Text = $"Valisite kuupäeva: {e.NewDate:D}";
    }
}
