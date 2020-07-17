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

namespace Final_Project
{
	/// <summary>
	/// Interaction logic for user_shopping_list.xaml
	/// </summary>
	public partial class user_shopping_list : Window
	{
		public static int counter = 2;
		public static WrapPanel main_wrap;
		public class Properties
		{
			public  string order_Number;
			public  string Order_Food;
			public string Order_Cost;
			public string Order_Food_Number;
		}

		public class Order
		{
			public List<Properties> properties { get; set; }
			public string Order_Number;
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
				lable1.Content =item.Order_Food;
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
			double total_cost =0;
			foreach (var item in obj.properties)
			{
				total_cost += double.Parse(item.Order_Cost) * double.Parse(item.Order_Food_Number);
			}
			lable6.Content = total_cost.ToString();
			lable6.FontSize = 15;
			lable6.Margin = new Thickness(10, 0, 10, 0);
			Grid.SetColumn(lable6, 1);
			grid1.Children.Add(lable6);
			grid1.Children.Add(lable5);

			
			stackpanel.Children.Add(grid1);
			Button btn = new Button();
			btn.Content = "Cancel";
			bc = new BrushConverter();
			btn.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFD9AD");
			btn.Margin = new Thickness(80, 20, 80, 10);
			stackpanel.Children.Add(btn);
			user_shopping_list.counter++;
			Border border = new Border();
			bc = new BrushConverter();
			border.BorderBrush = (System.Windows.Media.Brush)bc.ConvertFrom("#FF3A250D");
			border.BorderThickness = new Thickness(2,2,2,2);
			border.CornerRadius = new CornerRadius(10, 10, 10, 10);
			border.Margin = new Thickness(30,0,0,0);
			border.Child = stackpanel;
			border.Padding = new Thickness(5, 5, 5, 5);
			main_wrap.Children.Add(border);
			return main_wrap;
		}
		public user_shopping_list()
		{
			InitializeComponent();
			SqlConnection sqlConnection = new SqlConnection(" Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project\\Final_Project\\database.mdf\"; Integrated Security = True; Connect Timeout = 30");
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select Orders from Users where FullName=@FullName", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@FullName", Current_user.FullName);
			List<Order> orders = new List<Order>();
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			sqlDataReader.Read();
			List<string> order = new List<string>();
			order = sqlDataReader.GetValue(0).ToString().Split('/').ToList();
			foreach (var item in order)
			{
				if (item != "")
				{
					List<string> Order_Food = new List<string>();
					List<string> Order_Cost = new List<string>();
					List<string> Order_Food_Number = new List<string>();
					Order_Food = item.Split(':')[1].Split(',').ToList();
					Order_Cost = item.Split(':')[2].Split(',').ToList();
					Order_Food_Number = item.Split(':')[3].Split(',').ToList();
					string order_number = item.Split(':')[0];
					Order a = new Order();
					a.Order_Number = order_number;
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
