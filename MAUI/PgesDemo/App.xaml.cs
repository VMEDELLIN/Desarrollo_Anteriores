﻿namespace PgesDemo;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		var navPage=new NavigationPage(new MainPage());
		navPage.BarBackground = Colors.Chocolate;
		navPage.BarTextColor = Colors.White;
        //MainPage = navPage;
        //MainPage = new FlyoutPageDemo();
        MainPage = new TabbetPageDemo();

    }
}
