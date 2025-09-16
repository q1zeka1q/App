using Microsoft.Maui.Controls;

namespace _1projekt;

public partial class ValgusfoorPage : ContentPage
{
    // Простое перечисление текущего сигнала
    private enum Light { Red, Yellow, Green }

    // null = выключено; иначе — какой именно свет горит
    private Light? _state = null;

    public ValgusfoorPage()
    {
        InitializeComponent();
        UpdateUI();
    }

    // ===== Кнопки =====
    private void OnTurnOnClicked(object sender, EventArgs e)
    {
        _state = Light.Red;   // при включении начинаем с красного
        UpdateUI();
    }

    private void OnTurnOffClicked(object sender, EventArgs e)
    {
        _state = null;        // всё погасить
        UpdateUI();
    }

    // ===== Тапы по кружкам =====
    private void OnRedTapped(object sender, EventArgs e)
    {
        if (_state == null) return;
        _state = Light.Red;
        UpdateUI();
    }

    private void OnYellowTapped(object sender, EventArgs e)
    {
        if (_state == null) return;
        _state = Light.Yellow;
        UpdateUI();
    }

    private void OnGreenTapped(object sender, EventArgs e)
    {
        if (_state == null) return;
        _state = Light.Green;
        UpdateUI();
    }

    // ===== Обновление внешнего вида =====
    private void UpdateUI()
    {
        // цвета «вкл/выкл»
        var onRed = Colors.Red;
        var onYellow = Colors.Yellow;
        var onGreen = Colors.LimeGreen;
        var off = Colors.Gray;

        if (_state == null)
        {
            // выключено — все серые
            RedLight.BackgroundColor = off;
            YellowLight.BackgroundColor = off;
            GreenLight.BackgroundColor = off;
            TitleLabel.Text = "Lülita foor sisse";
            return;
        }

        // включено — светит только выбранный
        RedLight.BackgroundColor = (_state == Light.Red) ? onRed : off;
        YellowLight.BackgroundColor = (_state == Light.Yellow) ? onYellow : off;
        GreenLight.BackgroundColor = (_state == Light.Green) ? onGreen : off;

        // подсказка сверху
        TitleLabel.Text = _state switch
        {
            Light.Red => "Seisa",
            Light.Yellow => "Valmistu",
            Light.Green => "Sõida",
            _ => "Vali valgus"
        };
    }
}
