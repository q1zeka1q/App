using Microsoft.Maui.Controls;

namespace MauiApptargv;

// ВАЖНО: класс partial и то же имя/пространство имён, что в XAML (x:Class)
public partial class TripsTrapsTrull : ContentPage
{
    // Храним, чей ход сейчас ("X" или "O")
    private string current = "X";

    public TripsTrapsTrull()
    {
        InitializeComponent(); 
    }

    // Нажали на любую клетку поля
    private void OnCellClicked(object sender, EventArgs e)
    {
        // sender — это та кнопка, по которой кликнули
        if (sender is Button btn && string.IsNullOrEmpty(btn.Text))
        {
            // Ставим символ текущего игрока на кнопку
            btn.Text = current;

            // Проверяем победу
            if (CheckWin(current))
            {
                DisplayAlert("Победа!", $"{current} выиграл!", "OK");
                return; // игра окончена
            }

            // Проверяем ничью (все клетки заняты)
            if (IsDraw())
            {
                DisplayAlert("Ничья", "Свободных клеток нет.", "OK");
                return;
            }

            // Меняем игрока: X -> O -> X ...
            current = current == "X" ? "O" : "X";
            TurnLabel.Text = $"Ход: {current}";
        }
    }

    // Кнопка "Новая игра": очистить поле и начать заново
    private void OnNewGameClicked(object sender, EventArgs e)
    {
        // Проходим по всем детям Grid и чистим текст у кнопок
        foreach (var child in Board.Children)
        {
            if (child is Button btn)
                btn.Text = "";
        }

        // Снова начинает X
        current = "X";
        TurnLabel.Text = "Ход: X";
    }

    // Проверка победы: 3 одинаковых символа в строке, колонке или диагонали
    private bool CheckWin(string player)
    {
        // Считываем текущее поле в массив 3x3
        string?[,] board = new string?[3, 3]
        {
            { B00.Text, B01.Text, B02.Text },
            { B10.Text, B11.Text, B12.Text },
            { B20.Text, B21.Text, B22.Text }
        };

        // Проверяем все строки и столбцы
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) return true;
            if (board[0, i] == player && board[1, i] == player && board[2, i] == player) return true;
        }

        // Проверяем две диагонали
        if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) return true;
        if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player) return true;

        return false; // победы нет
    }

    // Проверка на ничью: нет пустых кнопок
    private bool IsDraw()
    {
        foreach (var child in Board.Children)
        {
            if (child is Button btn && string.IsNullOrEmpty(btn.Text))
                return false; // нашли пустую — ещё играем
        }
        return true; // пустых нет — ничья
    }
}

