using MobileApp.Views;
using System;
using Xamarin.Forms;

namespace MobileApp
{
	public partial class App : Application
	{
		public App()
		{
			MainPage = new ToDoPage();
		}
	}
}