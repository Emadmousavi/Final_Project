using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Data;
using System.Data.SqlClient;

namespace Final_Project
{
	/// <summary>
	/// Interaction logic for admin_desktop.xaml
	/// </summary>
	public partial class admin_desktop : Window
	{
		public admin_desktop()
		{
			InitializeComponent();
		
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog open = new OpenFileDialog();
			open.InitialDirectory = @"C:\Users\emad&javad\Desktop";
			open.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
			open.FilterIndex = 1;
			if (open.ShowDialog() == true)
			{
				if (open.CheckFileExists)
				{
					string path = System.AppDomain.CurrentDomain.BaseDirectory;
					System.IO.File.Copy(open.FileName, path);
				}
			}

		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			string connection_string=" Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project\\Final_Project\\database.mdf\"; Integrated Security = True; Connect Timeout = 30";
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select * from [Table1]",sqlConnection);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				var data = sqlDataReader.GetValue(0);
				MessageBox.Show(data.ToString());
			}
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlConnection.Close();
		}
	}
}
