using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace _1projekt; 

public partial class Lumememm : ContentPage
{
    private CancellationTokenSource? _cts;
        
    public Lumememm()
    {
        InitializeComponent();         // Появится, когда совпадёт полное имя класса
        ActionPicker.SelectedIndex = 1; // "Näita"
        UpdateOpacityLabel();
        UpdateSpeedLabel();
    }
    // Показать/скрыть снеговика
    private void SetSnowmanVisible(bool visible) => Canvas.IsVisible = visible;
    // Установить прозрачность всем частям
    private void SetSnowmanOpacity(double value)
    {
        Head.Opacity = value;
        Body.Opacity = value;
        Bucket.Opacity = value;
    }
    // Обновить подпись у слайдера
    private void UpdateOpacityLabel() => OpacityValueLabel.Text = OpacitySlider.Value.ToString("0.00");
    // Обновить подпись у степпера
    private void UpdateSpeedLabel() => SpeedLabel.Text = $"{(int)SpeedStepper.Value} ms";
    // Текущее значение скорости
    private int SpeedMs => (int)SpeedStepper.Value;
    // Остановить все анимации и вернуть в исходное состояние
    private void StopAnimations()
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        Head.TranslationX = Body.TranslationX = Bucket.TranslationX = 0;
        Head.Scale = Body.Scale = Bucket.Scale = 1.0;
        SetSnowmanOpacity(OpacitySlider.Value);
    }
    // Событие слайдера
    private void OnOpacityChanged(object sender, ValueChangedEventArgs e)
    {
        SetSnowmanOpacity(e.NewValue);
        UpdateOpacityLabel();
    }
    // Событие степпера
    private void OnSpeedChanged(object sender, ValueChangedEventArgs e) => UpdateSpeedLabel();
    // Кнопка "Tee"
    private async void OnDoActionClicked(object sender, EventArgs e)
    {
        var action = ActionPicker.SelectedItem as string ?? "";
        InfoLabel.Text = $"Tegevus: {action}";
        StopAnimations();

        switch (action)
        {
            case "Peida": SetSnowmanVisible(false); break;
            case "Näita":
                SetSnowmanVisible(true);
                Canvas.Opacity = 0;
                await Canvas.FadeTo(1, (uint)SpeedMs);
                break;
            case "Muuda värvi": await ChangeColorAsync(); break;
            case "Sulata": await MeltAsync(); break;
            case "Tantsi": await DanceAsync(); break;
        }
    }
    // Сменить цвет ведра
    private async Task ChangeColorAsync()
    {
        bool ok = await DisplayAlert("Подтверждение", "Сменить цвет?", "Да", "Нет");
        if (!ok) return;
        var rnd = new Random();
        Color RandomColor() => Color.FromRgb(rnd.Next(20, 236), rnd.Next(20, 236), rnd.Next(20, 236));
        Bucket.BackgroundColor = RandomColor();
        await Bucket.ScaleTo(1.1, (uint)(SpeedMs / 2));
        await Bucket.ScaleTo(1.0, (uint)(SpeedMs / 2));
    }
    // Растопить снеговика
    private async Task MeltAsync()
    {
        SetSnowmanVisible(true);
        uint d = (uint)SpeedMs;
        await Task.WhenAll(
            Head.FadeTo(0, d), Body.FadeTo(0, d), Bucket.FadeTo(0, d),
            Head.ScaleTo(0.7, d), Body.ScaleTo(0.7, d), Bucket.ScaleTo(0.7, d)
        );
        SetSnowmanVisible(false);
        // вернуть в нормальное состояние
        Head.Opacity = Body.Opacity = Bucket.Opacity = OpacitySlider.Value;
        Head.Scale = Body.Scale = Bucket.Scale = 1.0;
    }
    // Танец
    private async Task DanceAsync()
    {
        SetSnowmanVisible(true);
        _cts ??= new CancellationTokenSource();
        var token = _cts.Token;
        double dx = 30;
        uint d = (uint)SpeedMs;

        try
        {
            while (!token.IsCancellationRequested)
            {
                await Task.WhenAll(
                    Head.TranslateTo(-dx, 0, d),
                    Body.TranslateTo(-dx, 0, d),
                    Bucket.TranslateTo(-dx, 0, d)
                );
                await Task.WhenAll(
                    Head.TranslateTo(dx, 0, d),
                    Body.TranslateTo(dx, 0, d),
                    Bucket.TranslateTo(dx, 0, d)
                );
                await Task.WhenAll(
                    Head.TranslateTo(0, 0, d),
                    Body.TranslateTo(0, 0, d),
                    Bucket.TranslateTo(0, 0, d)
                );
            }
        }
        catch (TaskCanceledException) { }
    }
}
