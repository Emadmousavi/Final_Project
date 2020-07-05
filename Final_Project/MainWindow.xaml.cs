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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

namespace Final_Project
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		
			
		}


		public bool email_check(string email)
		{
			string pattern = @"[a-zA-Z0-9]*@[a-zA-Z0-9]*\.[a-zA-Z0-9]*";
			Regex re = new Regex(pattern);
			return re.IsMatch(email);
		}
		private void txt1_LostFocus(object sender, RoutedEventArgs e)
		{
			if (txt1.Text != "")
				lb1.Foreground = Brushes.Green;
			
		}

		private void txt2_LostFocus(object sender, RoutedEventArgs e)
		{
			if (email_check(txt2.Text))
				lb2.Foreground = Brushes.Green;
			else
				lb2.Foreground = Brushes.Red;
			if(txt2.Text=="")
				lb2.Foreground = Brushes.Gray;
		}

		private void txt3_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				var i = int.Parse(txt3.Text);
				lb3.Foreground = Brushes.Green;
			}
			catch
			{
				lb3.Foreground = Brushes.Red;
				if (txt3.Text == "")
					lb3.Foreground = Brushes.Gray;
			}
		}

		private void txt4_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				var i = int.Parse(txt4.Text);
				lb4.Foreground = Brushes.Green;
			}
			catch
			{
				lb4.Foreground = Brushes.Red;
				if (txt4.Text == "")
					lb4.Foreground = Brushes.Gray;
			}
		}

		private void txt5_LostFocus(object sender, RoutedEventArgs e)
		{
			if(txt5.Password != "")
				lb5.Foreground = Brushes.Green;
			else
				lb5.Foreground = Brushes.Red;
			if (txt5.Password == "")
				lb5.Foreground = Brushes.Gray;
		}

		private void txt6_LostFocus(object sender, RoutedEventArgs e)
		{
			if (txt6.Password == txt5.Password && txt5.Password!="")
				lb6.Foreground = Brushes.Green;
			else
				lb6.Foreground = Brushes.Red;
			if (txt6.Password == txt5.Password && txt5.Password == "")
				lb6.Foreground = Brushes.Gray;
			
		}

		private void txt7_LostFocus(object sender, RoutedEventArgs e)
		{

		}

		private void txt8_LostFocus(object sender, RoutedEventArgs e)
		{

		}
	}
}
