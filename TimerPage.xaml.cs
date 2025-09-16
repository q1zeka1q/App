using System.Threading.Tasks;

namespace _1projekt;

public partial class TimerPage : ContentPage
{
	public TimerPage()
	{
		InitializeComponent();
	}
	bool on_off = true;

	private void Klik_pealdise_peal(object sender, TappedEventArgs e)
	{
		if (on_off)
		{
			on_off = false;
		}
		else
		{
			on_off = true;
			Näita_aeg();
		}
	}
	private async void Näita_aeg()
	{
		while (on_off)
		{
			label.Text = DateTime.Now.ToString("HH:mm:ss");
			label.FontSize = 50;
			await Task.Delay(1000);
		}
	}
}