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
using System.Runtime.CompilerServices;
using System.IO;

namespace Final_Project_again
{
	/// <summary>
	/// Interaction logic for user_profile.xaml
	/// </summary>
	public partial class user_profile : Window
	{
		string connection_string = " Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30";

		public user_profile()
		{
			InitializeComponent();
			if (Current_user.image_uri != "")
			{
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(Current_user.image_uri));
				profile_image.Fill = brush;
			}

			else
			{
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\user_profile.png"));
				profile_image.Fill = brush;
			}

			FullName_txt.Text = Current_user.FullName;
			PhoneNumber_txt.Text = Current_user.PhoneNumber;
			E_mail_txt.Text = Current_user.E_mail;
			Password_txt.Text = Current_user.Password;
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
					SqlConnection sqlConnection = new SqlConnection(connection_string);
					sqlConnection.Open();
					SqlCommand sqlCommand = new SqlCommand("update Users set Image_Uri=@image_uri where FullName=@FullName and E_mail=@E_mail and _Password=@_Password", sqlConnection);
					sqlCommand.Parameters.AddWithValue("@FullName", Current_user.FullName);
					sqlCommand.Parameters.AddWithValue("@E_mail", Current_user.E_mail);
					sqlCommand.Parameters.AddWithValue("@_Password", Current_user.Password);
					sqlCommand.Parameters.AddWithValue("@image_uri", open.FileName);
					sqlCommand.ExecuteNonQuery();
					sqlConnection.Close();
					sqlCommand.Dispose();
					profile_image.Fill = brush;
				}
			}
		}

		private void exit_btn_Click(object sender, RoutedEventArgs e)
		{
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("update Users set FullName=@FullName,E_mail=@E_mail,_Password=@_Password,PhoneNumber=@PhoneNumber where FullName=@F and E_mail=@E and _Password=@_P", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@FullName", FullName_txt.Text);
			sqlCommand.Parameters.AddWithValue("@E_mail", E_mail_txt.Text);
			sqlCommand.Parameters.AddWithValue("@_Password", Password_txt.Text);
			sqlCommand.Parameters.AddWithValue("@PhoneNumber", PhoneNumber_txt.Text);
			sqlCommand.Parameters.AddWithValue("@F", Current_user.FullName);
			sqlCommand.Parameters.AddWithValue("@E", Current_user.E_mail);
			sqlCommand.Parameters.AddWithValue("@_P", Current_user.Password);
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
			sqlCommand.Dispose();
			user_desktop w = new user_desktop();
			w.Show();
			this.Close();
		}
	}
}
