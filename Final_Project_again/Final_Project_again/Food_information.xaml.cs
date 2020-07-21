using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Final_Project_again
{
	/// <summary>
	/// Interaction logic for Food_information.xaml
	/// </summary>
	public partial class Food_information : Window
	{
		public static string Name_Food;
		string connection_string = " Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30";
		StackPanel Message(string Name,string message)
		{
			StackPanel st = new StackPanel();
			st.Orientation = Orientation.Horizontal;
			Border border1 = new Border();
			border1.CornerRadius = new CornerRadius(10,10,10,10);
			border1.Background = Brushes.White;
			border1.Margin = new Thickness(0,5,0,5);
			border1.Height = 30;
			Label lable1 = new Label();
			lable1.Content = Name;
			lable1.Background = Brushes.White;
			lable1.Margin = new Thickness(5, 5, 10, 2);
			lable1.FontSize = 9;
			border1.Child = lable1;
			st.Children.Add(border1);

			Border border2 = new Border();
			border2.CornerRadius = new CornerRadius(15, 15, 15, 15);
			border2.Background = Brushes.White;
			border2.Margin = new Thickness(10, 0, 0, 0);
			border2.Height = 30;
			border2.Padding = new Thickness(2, 2, 2, 2);
			Label lable2 = new Label();
			lable2.Content = message;
			lable2.VerticalContentAlignment = VerticalAlignment.Center;
			lable2.FontSize = 10;
			border2.Child = lable2;
			st.Children.Add(border2);



			return st;
		}
		public Food_information(string Name, string Cost, string Information, string Date, string Uri)
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

			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand($"select Customer_Name,Message from {Name_Food}",sqlConnection);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				Feedback.Children.Add(Message(sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString()));
			}
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlConnection.Close();
			
			}

		private void Exit_btn_Click(object sender, RoutedEventArgs e)
		{
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand($"delete from {Name_Food}",sqlConnection);
			sqlCommand.ExecuteNonQuery();
			sqlCommand.Dispose();
			foreach (var item in Feedback.Children)
			{
				StackPanel st = item as StackPanel;
				string Name = ((Label)((Border)st.Children[0] as Border).Child).Content.ToString();
				string message = ((Label)((Border)st.Children[1] as Border).Child).Content.ToString();

				sqlCommand = new SqlCommand($"insert into {Name_Food} (Customer_Name,Message) values(@Customer_Name,@Message)",sqlConnection);
				sqlCommand.Parameters.AddWithValue("@Customer_Name", Name);
				sqlCommand.Parameters.AddWithValue("@Message", message);
				sqlCommand.ExecuteNonQuery();
				sqlCommand.Dispose();
			}

			
			sqlConnection.Close();
			this.Close();
		}

		private void Go_Click(object sender, RoutedEventArgs e)
		{
			Feedback.Children.Add(Message(Current_user.FullName, message.Text));
			message.Text = "";
		}
	}
}
