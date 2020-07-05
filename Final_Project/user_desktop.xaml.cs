using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Org.BouncyCastle.Math.EC;
using PersianDate;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace Final_Project
{
	/// <summary>
	/// Interaction logic for user_desktop.xaml
	/// </summary>
	public partial class user_desktop : Window
	{
		user_menu w;
		bool open = false;
		public user_desktop()
		{
			InitializeComponent();
			DateTime startDate = DateTime.Today;
			DateTime endDate = startDate.AddMonths(1);
			calendar.DisplayDateEnd = endDate;
			
		}

		private void menu_Click(object sender, RoutedEventArgs e)
		{
			
			
			if (open)
			{
				w.Close();
				open = false;
			}

			else
			{
				w = new user_menu();
				w.Show();
				open = true;
			}
		}

		
	}
}
