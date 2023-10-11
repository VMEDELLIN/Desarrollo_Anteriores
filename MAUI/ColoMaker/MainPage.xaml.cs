using Android.Graphics;
using Android.OS;
using Microsoft.Maui.Graphics;
using Color = Microsoft.Maui.Graphics.Color;
using System.Diagnostics;
using Android.Widget;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
namespace ColoMaker;

public partial class MainPage : ContentPage
{
	int count = 0;
    bool IsRandom = false;
    string hexValue = string.Empty;

    public MainPage()
	{
		InitializeComponent();
	}

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (IsRandom)
            return;
		var red=sldRed.Value;
		var green= sldGreen.Value;	
		var blue= sldBlue.Value;

        Color color = Color.FromRgb(red, green, blue);

		SetColor(color);
    }
	private void SetColor(Color color)
	{
        System.Diagnostics.Debug.WriteLine(color.ToString());
		btnRandom.BackgroundColor = color;	
		Container.BackgroundColor = color;
        hexValue = color.ToHex();
        lblHex.Text= hexValue;	
	}

    private void btnRandom_Clicked(object sender, EventArgs e)
    {
        IsRandom = true;
        var random=new Random();
		var color = Color.FromRgb(
			random.Next(0,256),
            random.Next(0, 256),
            random.Next(0, 256)
            );
		
        SetColor(color);	
		sldRed.Value=color.Red;
        sldGreen.Value = color.Green;
        sldBlue.Value = color.Blue;

        IsRandom = false;
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Clipboard.SetTextAsync(hexValue);
        var toast = CommunityToolkit.Maui.Alerts.Toast.Make("Color copied", ToastDuration.Short, 12);
        await toast.Show();
    }
}

