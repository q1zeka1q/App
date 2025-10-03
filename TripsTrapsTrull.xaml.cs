using Microsoft.Maui.Controls;

namespace MauiApptargv;

// �����: ����� partial � �� �� ���/������������ ���, ��� � XAML (x:Class)
public partial class TripsTrapsTrull : ContentPage
{
    // ������, ��� ��� ������ ("X" ��� "O")
    private string current = "X";

    public TripsTrapsTrull()
    {
        InitializeComponent(); 
    }

    // ������ �� ����� ������ ����
    private void OnCellClicked(object sender, EventArgs e)
    {
        // sender � ��� �� ������, �� ������� ��������
        if (sender is Button btn && string.IsNullOrEmpty(btn.Text))
        {
            // ������ ������ �������� ������ �� ������
            btn.Text = current;

            // ��������� ������
            if (CheckWin(current))
            {
                DisplayAlert("������!", $"{current} �������!", "OK");
                return; // ���� ��������
            }

            // ��������� ����� (��� ������ ������)
            if (IsDraw())
            {
                DisplayAlert("�����", "��������� ������ ���.", "OK");
                return;
            }

            // ������ ������: X -> O -> X ...
            current = current == "X" ? "O" : "X";
            TurnLabel.Text = $"���: {current}";
        }
    }

    // ������ "����� ����": �������� ���� � ������ ������
    private void OnNewGameClicked(object sender, EventArgs e)
    {
        // �������� �� ���� ����� Grid � ������ ����� � ������
        foreach (var child in Board.Children)
        {
            if (child is Button btn)
                btn.Text = "";
        }

        // ����� �������� X
        current = "X";
        TurnLabel.Text = "���: X";
    }

    // �������� ������: 3 ���������� ������� � ������, ������� ��� ���������
    private bool CheckWin(string player)
    {
        // ��������� ������� ���� � ������ 3x3
        string?[,] board = new string?[3, 3]
        {
            { B00.Text, B01.Text, B02.Text },
            { B10.Text, B11.Text, B12.Text },
            { B20.Text, B21.Text, B22.Text }
        };

        // ��������� ��� ������ � �������
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) return true;
            if (board[0, i] == player && board[1, i] == player && board[2, i] == player) return true;
        }

        // ��������� ��� ���������
        if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) return true;
        if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player) return true;

        return false; // ������ ���
    }

    // �������� �� �����: ��� ������ ������
    private bool IsDraw()
    {
        foreach (var child in Board.Children)
        {
            if (child is Button btn && string.IsNullOrEmpty(btn.Text))
                return false; // ����� ������ � ��� ������
        }
        return true; // ������ ��� � �����
    }
}

