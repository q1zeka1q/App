using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace _1projekt; 

public partial class Lumememm : ContentPage
{// Нужно, чтобы можно было остановить "танец"
    private CancellationTokenSource? _cts;
        
    public Lumememm()
    {
        InitializeComponent();          // Загружаем разметку со страницы
        ActionPicker.SelectedIndex = 1; // По умолчанию выбрано "Показать"
        UpdateOpacityLabel();           // Обновляем надпись прозрачности
        UpdateSpeedLabel();             // Обновляем надпись скорости
    }
    // Показать или спрятать снеговика
    private void SetSnowmanVisible(bool visible) => Canvas.IsVisible = visible;
    // Сделать все части снеговика одинаково прозрачными
    private void SetSnowmanOpacity(double value)
    {
        Head.Opacity = value;
        Body.Opacity = value;
        Bucket.Opacity = value;
    }
    // Поменять надпись рядом со слайдером прозрачности
    private void UpdateOpacityLabel() => OpacityValueLabel.Text = OpacitySlider.Value.ToString("0.00");
    // Поменять надпись рядом со степпером скорости
    private void UpdateSpeedLabel() => SpeedLabel.Text = $"{(int)SpeedStepper.Value} ms";
    // Значение скорости (в миллисекундах)
    private int SpeedMs => (int)SpeedStepper.Value;
    // Остановить анимации и вернуть снеговика как был
    private void StopAnimations()
    {
        _cts?.Cancel();                       // Остановить старый танец
        _cts = new CancellationTokenSource(); // Создать новый "стопер"
        Head.TranslationX = Body.TranslationX = Bucket.TranslationX = 0; // Вернуть на место
        Head.Scale = Body.Scale = Bucket.Scale = 1.0;                    // Вернуть обычный размер
        SetSnowmanOpacity(OpacitySlider.Value);
    }
    // Когда двигаем слайдер прозрачности
    private void OnOpacityChanged(object sender, ValueChangedEventArgs e)
    {
        SetSnowmanOpacity(e.NewValue);
        UpdateOpacityLabel();
    }
    // Когда меняем скорость
    private void OnSpeedChanged(object sender, ValueChangedEventArgs e) => UpdateSpeedLabel();
    // Когда нажимаем кнопку "Сделать"
    private async void OnDoActionClicked(object sender, EventArgs e)
    {
        var action = ActionPicker.SelectedItem as string ?? ""; // Что выбрано
        InfoLabel.Text = $"Tegevus: {action}";                  // Показать действие
        StopAnimations();                                       // Сначала всё остановить

        switch (action)
        {
            case "Peida": SetSnowmanVisible(false); break;      // Спрятать
            case "Näita":                                     // Показать плавно
                SetSnowmanVisible(true);
                Canvas.Opacity = 0;
                await Canvas.FadeTo(1, (uint)SpeedMs);
                break;
            case "Muuda värvi": await ChangeColorAsync(); break; // Сменить цвет ведра
            case "Sulata": await MeltAsync(); break;             // Растопить
            case "Tantsi": await DanceAsync(); break;            // Танцевать
        }
    }
    // Сменить цвет ведра
    private async Task ChangeColorAsync()
    {
        bool ok = await DisplayAlert("Подтверждение", "Сменить цвет?", "Да", "Нет");
        if (!ok) return;
        var rnd = new Random();
        Color RandomColor() => Color.FromRgb(rnd.Next(20, 236), rnd.Next(20, 236), rnd.Next(20, 236));
        Bucket.BackgroundColor = RandomColor();         // Присвоить цвет
        await Bucket.ScaleTo(1.1, (uint)(SpeedMs / 2)); // Увеличить немного
        await Bucket.ScaleTo(1.0, (uint)(SpeedMs / 2)); // Вернуть назад
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
