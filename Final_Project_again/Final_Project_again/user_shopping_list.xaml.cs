using Renci.SshNet.Messages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
using System.Xml.Linq;

namespace Final_Project_again
{
	/// <summary>
	/// Interaction logic for user_shopping_list.xaml
	/// </summary>
	public partial class user_shopping_list : Window
	{
		
		public static void cash_cancel(object sender,RoutedEventArgs e)
		{
			Button btn = e.Source as Button;
			StackPanel st = btn.Parent as StackPanel;
			Grid grid = (Grid)((StackPanel)st.Children[st.Children.Count - 2]).Children[0];
			string Order_Tracking = ((Label)grid.Children[0]).Content.ToString();
			SqlConnection sqlConnection = new SqlConnection(" Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30");
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select Total_Cost from Orders where Order_Tracking=@Order_Tracking", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@Order_Tracking", Order_Tracking);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			sqlDataReader.Read();
			MessageBox.Show($"your order canceled\n please pay {double.Parse(sqlDataReader.GetValue(0).ToString()) * 0.1}$ as tax");
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlCommand = new SqlCommand("delete from Orders where Order_Tracking=@Order_Tracking", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@Order_Tracking", Order_Tracking);
			sqlCommand.ExecuteNonQuery();
			sqlCommand.Dispose();
			sqlConnection.Close();
			Border border = st.Parent as Border;
			border.Height = 0;

		}

		public static void online_cancel(object sender, RoutedEventArgs e)
		{
			Button btn = e.Source as Button;
			StackPanel st = btn.Parent as StackPanel;
			Grid grid = (Grid)((StackPanel)st.Children[st.Children.Count - 2]).Children[0];
			string Order_Tracking = ((Label)grid.Children[0]).Content.ToString();
			SqlConnection sqlConnection = new SqlConnection(" Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30");
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select Total_Cost from Orders where Order_Tracking=@Order_Tracking", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@Order_Tracking", Order_Tracking);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			sqlDataReader.Read();
			MessageBox.Show($"your order canceled\n {double.Parse(sqlDataReader.GetValue(0).ToString()) * 0.9}$ will be refunded to your account\n{double.Parse(sqlDataReader.GetValue(0).ToString()) * 0.1}$ will be deducated for tax");
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlCommand = new SqlCommand("delete from Orders where Order_Tracking=@Order_Tracking", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@Order_Tracking", Order_Tracking);
			sqlCommand.ExecuteNonQuery();
			sqlCommand.Dispose();
			sqlConnection.Close();
			Border border = st.Parent as Border;
			border.Height = 0;
		}
		public static int counter = 2;
		public static WrapPanel main_wrap;
		public class Properties
		{
			public string order_Number;
			public string Order_Food;
			public string Order_Cost;
			public string Order_Food_Number;
		}

		public class Order
		{
			public List<Properties> properties { get; set; }
			public string Order_Number;
			public string Payment;
			public string Order_Tracking;
		}
		public static WrapPanel Shopping_List(Order obj)
		{
			if (counter == 2)
			{
				counter = 0;
				main_wrap = new WrapPanel();
				main_wrap.HorizontalAlignment = HorizontalAlignment.Center;
				main_wrap.Margin = new Thickness(0, 40, 0, 0);

			}
			StackPanel stackpanel = new StackPanel();
			foreach (var item in obj.properties)
			{
				StackPanel st = new StackPanel();
				Label lable1 = new Label();
				var br = new BrushConverter();
				lable1.Foreground = (System.Windows.Media.Brush)br.ConvertFrom("#FF3A250D");
				lable1.Content = item.Order_Food;
				lable1.FontSize = 20;
				lable1.FontWeight = FontWeights.Bold;
				lable1.FontStyle = FontStyles.Italic;
				Grid grid = new Grid();
				ColumnDefinition c1 = new ColumnDefinition();
				c1.Width = new GridLength(2.5, GridUnitType.Star);
				ColumnDefinition c2 = new ColumnDefinition();
				c2.Width = new GridLength(1, GridUnitType.Star);
				grid.ColumnDefinitions.Add(c1);
				grid.ColumnDefinitions.Add(c2);
				Label lable2 = new Label();
				br = new BrushConverter();
				lable2.Foreground = (System.Windows.Media.Brush)br.ConvertFrom("#FF3A250D");
				lable2.Content = (double.Parse(item.Order_Cost) * double.Parse(item.Order_Food_Number.ToString())) + "$";
				lable2.FontSize = 15;
				lable2.Margin = new Thickness(10, 0, 10, 0);
				Grid.SetColumn(lable2, 1);
				Label lable3 = new Label();
				br = new BrushConverter();
				lable3.Foreground = (System.Windows.Media.Brush)br.ConvertFrom("#FF3A250D");
				lable3.Content = double.Parse(item.Order_Food_Number.ToString());
				lable3.FontSize = 15;
				lable3.Margin = new Thickness(10, 0, 10, 0);
				Grid.SetColumn(lable3, 0);
				grid.Children.Add(lable3);
				grid.Children.Add(lable2);
				Label lable4 = new Label();
				br = new BrushConverter();
				lable4.Foreground = (System.Windows.Media.Brush)br.ConvertFrom("#FF3A250D");
				lable4.Content = "_______________________________________________________________________________";
				lable4.Margin = new Thickness(0, -7, 0, 0);
				lable4.FontSize = 15;
				lable4.Margin = new Thickness(10, 0, 10, 0);
				st.Children.Add(lable1);
				st.Children.Add(grid);
				st.Children.Add(lable4);
				stackpanel.Children.Add(st);

			}
			Grid grid1 = new Grid();
			ColumnDefinition cc1 = new ColumnDefinition();
			cc1.Width = new GridLength(2.5, GridUnitType.Star);
			ColumnDefinition cc2 = new ColumnDefinition();
			cc2.Width = new GridLength(1, GridUnitType.Star);
			grid1.ColumnDefinitions.Add(cc1);
			grid1.ColumnDefinitions.Add(cc2);
			Label lable5 = new Label();
			var bc = new BrushConverter();
			lable5.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF3A250D");
			lable5.Content = "Total Cost";
			lable5.FontSize = 15;
			lable5.Margin = new Thickness(10, 0, 10, 0);
			Grid.SetColumn(lable5, 0);
			Label lable6 = new Label();
			bc = new BrushConverter();
			lable6.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FF3A250D");
			double total_cost = 0;
			SqlConnection sqlConnection = new SqlConnection(" Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30");
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select Total_Cost from Orders where Order_Tracking=@Order_Tracking", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@Order_Tracking", obj.Order_Tracking);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			sqlDataReader.Read();
			total_cost = double.Parse(sqlDataReader.GetValue(0).ToString());
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlConnection.Close();
			lable6.Content =$"{ total_cost:N2}$";
			lable6.FontSize = 15;
			lable6.Margin = new Thickness(10, 0, 10, 0);
			Grid.SetColumn(lable6, 1);
			grid1.Children.Add(lable6);
			grid1.Children.Add(lable5);


			stackpanel.Children.Add(grid1);
			Button btn = new Button();
			if (obj.Payment == "cash")
			{
				btn.Click += new RoutedEventHandler(cash_cancel);
			}
			else if (obj.Payment == "online")
			{
				btn.Click += new RoutedEventHandler(online_cancel);
			}

			btn.Content = "Cancel";
			bc = new BrushConverter();
			btn.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFD9AD");
			btn.Margin = new Thickness(80, 20, 80, 10);

			StackPanel stackPanel5 = new StackPanel();
			Grid grid5 = new Grid();
			ColumnDefinition c51 = new ColumnDefinition();
			c51.Width = new GridLength(2.5, GridUnitType.Star);
			ColumnDefinition c52 = new ColumnDefinition();
			c52.Width = new GridLength(1, GridUnitType.Star);
			grid5.ColumnDefinitions.Add(c51);
			grid5.ColumnDefinitions.Add(c52);
			Label lable51 = new Label();
			var bc5 = new BrushConverter();
			lable51.Foreground = (System.Windows.Media.Brush)bc5.ConvertFrom("#FF3A250D");
			lable51.Content = "Order_Tracking";
			lable51.FontWeight = FontWeights.Bold;
			lable51.FontStyle = FontStyles.Italic;
			lable51.FontSize = 16;
			lable51.Margin = new Thickness(10, 0, 10, 0);
			Grid.SetColumn(lable51, 0);
			Label lable52 = new Label();
			bc5 = new BrushConverter();
			lable52.Foreground = (System.Windows.Media.Brush)bc5.ConvertFrom("#FF3A250D");
			lable52.Content = $"{obj.Order_Tracking}";
			lable52.FontSize = 16;
			lable52.FontWeight = FontWeights.Bold;
			lable52.FontStyle = FontStyles.Italic;
			lable52.Margin = new Thickness(5, 0, 10, 0);
			Grid.SetColumn(lable52, 1);
			grid5.Children.Add(lable52);
			grid5.Children.Add(lable51);
			grid5.Margin = new Thickness(0, 10, 0, 0);
			stackPanel5.Children.Add(grid5);

			stackpanel.Children.Add(stackPanel5);
			stackpanel.Children.Add(btn);
			user_shopping_list.counter++;
			Border border = new Border();
			bc = new BrushConverter();
			border.BorderBrush = (System.Windows.Media.Brush)bc.ConvertFrom("#FF3A250D");
			border.BorderThickness = new Thickness(2, 2, 2, 2);
			border.CornerRadius = new CornerRadius(10, 10, 10, 10);
			border.Margin = new Thickness(30, 0, 0, 0);
			border.Child = stackpanel;
			border.Padding = new Thickness(5, 5, 5, 5);
			main_wrap.Children.Add(border);
			return main_wrap;
		}
		public user_shopping_list()
		{
			InitializeComponent();
			user_shopping_list.counter = 2;
			SqlConnection sqlConnection = new SqlConnection(" Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30");
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select Name_Food,Cost_Food,Number_Food,Payment,Order_Number from Orders where ID=@ID", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@ID", Current_user.FullName);
			List<Order> orders = new List<Order>();
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			List<string> order = new List<string>();
			while (sqlDataReader.Read())
			{
				List<string> Order_Food = new List<string>();
				List<string> Order_Cost = new List<string>();
				List<string> Order_Food_Number = new List<string>();
				string order_number = sqlDataReader.GetValue(4).ToString();
				Order_Food = sqlDataReader.GetValue(0).ToString().Split(',').ToList();
				Order_Cost = sqlDataReader.GetValue(1).ToString().Split(',').ToList();
				Order_Food_Number = sqlDataReader.GetValue(2).ToString().Split(',').ToList();
				string payment = sqlDataReader.GetValue(3).ToString();
				Order a = new Order();
				a.Order_Number = order_number;
				a.Order_Tracking = Current_user.FullName + order_number.ToString();
				a.Payment = payment;
				a.properties = new List<Properties>();
				using (var e1 = Order_Food.GetEnumerator())
				using (var e2 = Order_Cost.GetEnumerator())
				using (var e3 = Order_Food_Number.GetEnumerator())
				{
					while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext())
					{
						Properties p = new Properties();
						p.Order_Food = e1.Current;
						p.Order_Cost = e2.Current;
						p.Order_Food_Number = e3.Current;
						a.properties.Add(p);
					}
				}
				orders.Add(a);
			}
			

			foreach (var item in orders)
			{
				if (user_shopping_list.counter == 2)
				{
					List.Children.Add(Shopping_List(item));
				}

				else
				{
					Shopping_List(item);
				}
			}
		}

		private void exit_btn_Click(object sender, RoutedEventArgs e)
		{
			user_desktop w = new user_desktop();
			w.Show();
			this.Close();
		}
	}
}
