using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
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
using Org.BouncyCastle.Asn1.X509;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text.RegularExpressions;
using Renci.SshNet.Messages;

namespace Final_Project
{
	/// <summary>
	/// Interaction logic for Edit_Window.xaml
	/// </summary>
	public partial class Edit_Window : Window
	{
		public static string Name_Food;
		public Edit_Window(string Name, string Cost, string Information, string Date,string Uri)
		{
			InitializeComponent();
			Name_Food = Name;
			Name_txt.Text = Name;
			Cost_txt.Text = Cost;
			Information_txt.Text = Information;
			Date_txt.Text = Date;
			var brush = new ImageBrush();
			brush.ImageSource = new BitmapImage(new Uri(Uri));
			uploaded_image.Fill = brush;

		}

		private void save_btn_Click(object sender, RoutedEventArgs e)
		{
			SqlConnection sqlConnection = new SqlConnection(" Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project\\Final_Project\\database.mdf\"; Integrated Security = True; Connect Timeout = 30");
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("update Food_Menu set Name_Food=@Name_Food ,Cost_Food=@Cost_Food ,Date_Food=@Date_Food ,Information_Food=@Information_Food  where Name_Food =@Name_Food1", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@Name_Food", Name_txt.Text);
			sqlCommand.Parameters.AddWithValue("@Name_Food1",Name_Food);
			sqlCommand.Parameters.AddWithValue("@Cost_Food", Cost_txt.Text);
			sqlCommand.Parameters.AddWithValue("@Date_Food", Date_txt.Text);
			sqlCommand.Parameters.AddWithValue("@Information_Food", Information_txt.Text);
			sqlCommand.ExecuteNonQuery();
			sqlCommand.Dispose();
			sqlConnection.Close();
			this.Close();
		}

		private void cancel_btn_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void upload_image_Click(object sender, RoutedEventArgs e)
		{

			OpenFileDialog open = new OpenFileDialog();
			open.Title = "select an image";
			open.InitialDirectory = @"C:\Users\emad&javad\Desktop";
			open.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
			open.FilterIndex = 1;
			if (open.ShowDialog() == true)
			{
				if (open.CheckFileExists)
				{
					var brush = new ImageBrush();
					brush.ImageSource = new BitmapImage(new Uri(open.FileName));
					uploaded_image.Fill = brush;
					SqlConnection sqlConnection = new SqlConnection(" Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project\\Final_Project\\database.mdf\"; Integrated Security = True; Connect Timeout = 30");
					sqlConnection.Open();
					SqlCommand sqlCommand = new SqlCommand("update Food_Menu set Uri_Food=@Uri_Food where Name_Food =@Name_Food", sqlConnection);
					sqlCommand.Parameters.AddWithValue("@Name_Food", Name_Food);
					sqlCommand.Parameters.AddWithValue("@Uri_Food", open.FileName);
					sqlCommand.ExecuteNonQuery();
					sqlCommand.Dispose();
					sqlConnection.Close();
				}
			}
		}
	}
}
