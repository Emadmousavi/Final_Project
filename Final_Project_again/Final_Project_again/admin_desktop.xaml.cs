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
using System.Text.RegularExpressions;

namespace Final_Project_again
{
	public static class Autocomplete
	{
		static string connection_string = " Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30";

		public static List<string> GetData_Food_Name()
		{
			List<string> data = new List<string>();
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select Name_Food from Food_Menu", sqlConnection);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				data.Add(sqlDataReader.GetValue(0).ToString());
			}
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlConnection.Close();
			return data;
		}

		public static List<string> GetData_Ingridient()
		{
			List<string> data = new List<string>();
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select Information_Food from Food_Menu", sqlConnection);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				foreach (var item in sqlDataReader.GetValue(0).ToString().Split(','))
				{
					data.Add(item);
				}
				
			}
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlConnection.Close();
			return data;
		}

	}
	public static class menu
	{
		private static void ellipse_MouseUp(object sender, MouseButtonEventArgs e)
		{
			Ellipse ellipse = e.Source as Ellipse;
			StackPanel st = ellipse.Parent as StackPanel;
			SqlConnection sqlConnection = new SqlConnection(" Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30");
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select * from Food_Menu where Name_Food =@Name_Food", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@Name_Food", ((Label)st.Children[1]).Content.ToString().Substring(1));
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			sqlDataReader.Read();
			Edit_Window w = new Edit_Window(sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString(), sqlDataReader.GetValue(2).ToString(), sqlDataReader.GetValue(3).ToString(), sqlDataReader.GetValue(4).ToString());
			w.Show();
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlConnection.Close();
		}
		public static string RemoveWhitespace(this string input)
		{
			return new string(input.ToCharArray()
				.Where(c => !Char.IsWhiteSpace(c))
				.ToArray());
		}
		public static string uploaded_image_uri = @"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\-res1.jpg";
		public static int counter = 3;
		public static WrapPanel main_wrap;
		public static WrapPanel construct_food(string Name_Food, string Cost_Food, string Information_Food, string Date_Food, string Uri_Food, string Kind_Food)
		{
			if (counter == 3)
			{
				counter = 0;
				main_wrap = new WrapPanel();
				main_wrap.HorizontalAlignment = HorizontalAlignment.Center;

			}
			Border border = new Border();
			border.BorderBrush = Brushes.Gray;
			border.BorderThickness = new Thickness(4, 4, 4, 4);
			border.CornerRadius = new CornerRadius(9, 9, 9, 9);
			border.Width = 170;
			border.Height = 220;
			border.Margin = new Thickness(30, 15, 0, 25);
			StackPanel stackPanel = new StackPanel();
			stackPanel.Name = RemoveWhitespace(Name_Food);
			var bc = new BrushConverter();
			stackPanel.Background = (Brush)bc.ConvertFrom("#FF68491E");
			stackPanel.Height = 210;
			stackPanel.Width = 163;
			Ellipse ellipse = new Ellipse();
			ellipse.MouseUp += ellipse_MouseUp;
			ellipse.Name = "ellipse";
			ellipse.Width = 100;
			ellipse.Height = 100;
			ellipse.Margin = new Thickness(10, 10, 10, 10);
			ImageBrush ellipse_image = new ImageBrush();
			ellipse_image.ImageSource = new BitmapImage(new Uri(Uri_Food));
			ellipse.Fill = ellipse_image;
			Label label = new Label();
			bc = new BrushConverter();
			label.Foreground = (Brush)bc.ConvertFrom("#FFFFDC89");
			label.Name = "food_name";
			label.Content = RemoveWhitespace(Name_Food);
			label.HorizontalAlignment = HorizontalAlignment.Center;
			Label label2 = new Label();
			bc = new BrushConverter();
			label2.Foreground = (Brush)bc.ConvertFrom("#FFFFDC89");
			label2.Name = "Date";
			label2.Content = Date_Food;
			label2.HorizontalAlignment = HorizontalAlignment.Center;
			stackPanel.Children.Add(ellipse);
			stackPanel.Children.Add(label);
			stackPanel.Children.Add(label2);
			WrapPanel wrapPanel = new WrapPanel();
			wrapPanel.Name = "wrappanel";
			wrapPanel.Margin = new Thickness(0, 5, 0, 0);
			wrapPanel.Height = 40;
			wrapPanel.Width = 160;
			Label label4 = new Label();
			bc = new BrushConverter();
			label4.Foreground = (Brush)bc.ConvertFrom("#FFFFDC89");
			label4.Content = "Price Of Food";
			label4.HorizontalAlignment = HorizontalAlignment.Left;
			label4.FontSize = 10;
			Label label3 = new Label();
			bc = new BrushConverter();
			label3.Foreground = (Brush)bc.ConvertFrom("#FFFFDC89");
			label3.Name = "food_price";
			label3.Content = Cost_Food + "$";
			label3.HorizontalAlignment = HorizontalAlignment.Right;
			label3.HorizontalContentAlignment = HorizontalAlignment.Right;
			label3.VerticalAlignment = VerticalAlignment.Center;
			label3.Margin = new Thickness(50, 0, 0, 0);
			wrapPanel.Children.Add(label4);
			wrapPanel.Children.Add(label3);
			stackPanel.Children.Add(wrapPanel);
			border.Child = stackPanel;
			main_wrap.Children.Add(border);
			counter++;
			return main_wrap;
		}

	}
	public static class Orders
	{
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
		}

		public static void cash_cancel(object sender, RoutedEventArgs e)
		{
			Button btn = e.Source as Button;
			StackPanel st = btn.Parent as StackPanel;
			string s_total = ((Label)((Grid)st.Children[st.Children.Count - 2]).Children[0]).Content.ToString();
			s_total = s_total.Substring(0, s_total.Length - 2);
			double total = double.Parse(s_total);
			MessageBox.Show($"your order canceled\n please pay {total * 0.1}$ as tax");

		}

		public static void online_cancel(object sender, RoutedEventArgs e)
		{
			Button btn = e.Source as Button;
			StackPanel st = btn.Parent as StackPanel;
			string s_total = ((Label)((Grid)st.Children[st.Children.Count - 2]).Children[0]).Content.ToString();
			s_total = s_total.Substring(0, s_total.Length - 2);
			double total = double.Parse(s_total);
			MessageBox.Show($"your order canceled\n {total * 0.9}$ will be refunded to your account\n{total * 0.1}$ will be deducated for tax");
		}
		public static Border construct(Order obj)
		{
			
			StackPanel stackpanel = new StackPanel();
			foreach (var item in obj.properties)
			{
				StackPanel st = new StackPanel();
				Label lable1 = new Label();
				var br = new BrushConverter();
				lable1.Foreground = (System.Windows.Media.Brush)br.ConvertFrom("#FFFFDBCB");
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
				lable2.Foreground = (System.Windows.Media.Brush)br.ConvertFrom("#FFFFDBCB");
				lable2.Content = (double.Parse(item.Order_Cost) * double.Parse(item.Order_Food_Number.ToString())) + "$";
				lable2.FontSize = 15;
				lable2.Margin = new Thickness(10, 0, 10, 0);
				Grid.SetColumn(lable2, 1);
				Label lable3 = new Label();
				br = new BrushConverter();
				lable3.Foreground = (System.Windows.Media.Brush)br.ConvertFrom("#FFFFDBCB");
				lable3.Content = double.Parse(item.Order_Food_Number.ToString());
				lable3.FontSize = 15;
				lable3.Margin = new Thickness(10, 0, 10, 0);
				Grid.SetColumn(lable3, 0);
				grid.Children.Add(lable3);
				grid.Children.Add(lable2);
				Label lable4 = new Label();
				br = new BrushConverter();
				lable4.Foreground = (System.Windows.Media.Brush)br.ConvertFrom("#FFFFDBCB");
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
			lable5.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFDBCB");
			lable5.Content = "Total Cost";
			lable5.FontSize = 15;
			lable5.Margin = new Thickness(10, 0, 10, 0);
			Grid.SetColumn(lable5, 0);
			Label lable6 = new Label();
			bc = new BrushConverter();
			lable6.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFDBCB");
			double total_cost = 0;
			if (int.Parse(obj.Order_Number) <= 4)
			{
				foreach (var item in obj.properties)
				{
					total_cost += double.Parse(item.Order_Cost) * double.Parse(item.Order_Food_Number);
					total_cost += double.Parse(item.Order_Cost) * double.Parse(item.Order_Food_Number) * (100.0 / 124.0) * 0.09;
				}
			}
			else if (int.Parse(obj.Order_Number) >= 5 && int.Parse(obj.Order_Number) <= 8)
			{
				foreach (var item in obj.properties)
				{
					total_cost += double.Parse(item.Order_Cost) * double.Parse(item.Order_Food_Number) * 0.95;
					total_cost += double.Parse(item.Order_Cost) * double.Parse(item.Order_Food_Number) * (100.0 / 124.0) * 0.07;
				}
			}
			else if (int.Parse(obj.Order_Number) >= 9 && int.Parse(obj.Order_Number) <= 12)
			{
				foreach (var item in obj.properties)
				{
					total_cost += double.Parse(item.Order_Cost) * double.Parse(item.Order_Food_Number) * 0.92;
					total_cost += double.Parse(item.Order_Cost) * double.Parse(item.Order_Food_Number) * (100.0 / 124.0) * 0.05;
				}
			}
			else if (int.Parse(obj.Order_Number) >= 13)
			{
				foreach (var item in obj.properties)
				{
					total_cost += double.Parse(item.Order_Cost) * double.Parse(item.Order_Food_Number) * 0.90;

				}
			}

			lable6.Content = $"{ total_cost:N2}$";
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
			btn.Background = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFDBCB");
			btn.Margin = new Thickness(80, 20, 80, 10);
			stackpanel.Children.Add(btn);
			user_shopping_list.counter++;
			Border border = new Border();
			bc = new BrushConverter();
			border.BorderBrush = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFDBCB");
			border.BorderThickness = new Thickness(2, 2, 2, 2);
			border.CornerRadius = new CornerRadius(10, 10, 10, 10);
			border.Margin = new Thickness(30, 0, 0, 10);
			border.Child = stackpanel;
			border.Padding = new Thickness(5, 5, 5, 5);
			return border;
		}
		public static WrapPanel GetData(string obj,string Name)
		{
			if (counter == 2)
			{
				counter = 0;
				main_wrap = new WrapPanel();
				main_wrap.HorizontalAlignment = HorizontalAlignment.Center;
				main_wrap.Margin = new Thickness(0, 40, 0, 0);

			}
			ScrollViewer scr = new ScrollViewer();
			StackPanel stack = new StackPanel();
			List<Order> orders = new List<Order>();
			List<string> order = new List<string>();
			Label name = new Label();
			name.Content = Name;
			name.FontSize = 20;
			var br = new BrushConverter();
			name.Foreground = (System.Windows.Media.Brush)br.ConvertFrom("#FFFFDBCB");
			name.Margin = new Thickness(25,0,0,5);
			stack.Children.Add(name);
			order = obj.Split('/').ToList();
			foreach (var item in order)
			{
				if (item != "")
				{
					List<string> Order_Food = new List<string>();
					List<string> Order_Cost = new List<string>();
					List<string> Order_Food_Number = new List<string>();
					string order_number = item.Split(':')[0];
					Order_Food = item.Split(':')[1].Split(',').ToList();
					Order_Cost = item.Split(':')[2].Split(',').ToList();
					Order_Food_Number = item.Split(':')[3].Split(',').ToList();
					string payment = item.Split(':')[4];
					Order a = new Order();
					a.Order_Number = order_number;
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
			}

			foreach (var item in orders)
			{

				stack.Children.Add(construct(item));

			}
			stack.Margin = new Thickness(0, 0, 0, 10);
			scr.Content = stack;
			scr.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
			scr.Height = 350;
			main_wrap.Children.Add(scr);
			Orders.counter++;
			return main_wrap;
		}
		
	}
		

	public static class List
	{
		private static void ellipse_MouseUp(object sender, MouseButtonEventArgs e)
		{
			Ellipse ellipse = e.Source as Ellipse;
			StackPanel st = ellipse.Parent as StackPanel;
			SqlConnection sqlConnection = new SqlConnection(" Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30");
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select * from Food_Menu where Name_Food =@Name_Food", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@Name_Food", ((Label)st.Children[1]).Content.ToString());
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			sqlDataReader.Read();
			Edit_Window w = new Edit_Window(sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString(), sqlDataReader.GetValue(2).ToString(), sqlDataReader.GetValue(3).ToString(), sqlDataReader.GetValue(4).ToString());
			w.Show();
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlConnection.Close();
		}
		public static string RemoveWhitespace(this string input)
		{
			return new string(input.ToCharArray()
				.Where(c => !Char.IsWhiteSpace(c))
				.ToArray());
		}
		public static string connection_string = " Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30";

		public static void limit(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		public static void Is_NUmber_changed(object sender, KeyboardFocusChangedEventArgs e)
		{

			TextBox txt = e.Source as TextBox;
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("update Food_Menu set Number_Food=@Number_Temp where Name_Food=@F", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@F", txt.Name);
			sqlCommand.Parameters.AddWithValue("@Number_Temp", txt.Text);
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
			sqlCommand.Dispose();
		}

		public static string uploaded_image_uri = @"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\-res1.jpg";
		public static int counter = 3;
		public static WrapPanel main_wrap;
		public static WrapPanel construct_food(string Name_Food, string Cost_Food, string Information_Food, string Date_Food, string Uri_Food, string Kind_Food, string Number_Food)
		{
			if (counter == 3)
			{
				counter = 0;
				main_wrap = new WrapPanel();
				main_wrap.HorizontalAlignment = HorizontalAlignment.Center;

			}
			Border border = new Border();
			border.BorderBrush = Brushes.Gray;
			border.BorderThickness = new Thickness(4, 4, 4, 4);
			border.CornerRadius = new CornerRadius(9, 9, 9, 9);
			border.Width = 170;
			border.Height = 220;
			border.Margin = new Thickness(30, 15, 0, 25);
			StackPanel stackPanel = new StackPanel();
			stackPanel.Name = "_" + RemoveWhitespace(Name_Food);
			var bc = new BrushConverter();
			stackPanel.Background = (Brush)bc.ConvertFrom("#FF68491E");
			stackPanel.Height = 210;
			stackPanel.Width = 163;
			Ellipse ellipse = new Ellipse();
			ellipse.MouseUp += ellipse_MouseUp;
			ellipse.Name = "ellipse";
			ellipse.Width = 100;
			ellipse.Height = 100;
			ellipse.Margin = new Thickness(10, 10, 10, 10);
			ImageBrush ellipse_image = new ImageBrush();
			ellipse_image.ImageSource = new BitmapImage(new Uri(Uri_Food));
			ellipse.Fill = ellipse_image;
			Label label = new Label();
			bc = new BrushConverter();
			label.Foreground = (Brush)bc.ConvertFrom("#FFFFDC89");
			label.Name = "food_name";
			label.Content = RemoveWhitespace(Name_Food);
			label.HorizontalAlignment = HorizontalAlignment.Center;
			Label label2 = new Label();
			bc = new BrushConverter();
			label2.Foreground = (Brush)bc.ConvertFrom("#FFFFDC89");
			label2.Name = "Date";
			label2.Content = Date_Food;
			label2.HorizontalAlignment = HorizontalAlignment.Center;
			var b = new BrushConverter();
			stackPanel.Children.Add(ellipse);
			stackPanel.Children.Add(label);
			stackPanel.Children.Add(label2);
			WrapPanel wrapPanel = new WrapPanel();
			wrapPanel.Name = "wrappanel";
			wrapPanel.Margin = new Thickness(0, 5, 0, 0);
			wrapPanel.Height = 40;
			wrapPanel.Width = 160;
			TextBox textBox = new TextBox();
			textBox.Name = RemoveWhitespace(Name_Food);
			textBox.Text = Number_Food;
			textBox.FontSize = 10;
			textBox.Margin = new Thickness(15, 5, 10, 13);
			textBox.MaxLength = 2;
			textBox.PreviewTextInput += new TextCompositionEventHandler(List.limit);
			textBox.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(Is_NUmber_changed);

			Label label4 = new Label();
			bc = new BrushConverter();
			label4.Foreground = (Brush)bc.ConvertFrom("#FFFFDC89");
			label4.Name = "food_price";
			label4.Content = Cost_Food + "$";
			label4.HorizontalAlignment = HorizontalAlignment.Right;
			label4.HorizontalContentAlignment = HorizontalAlignment.Right;
			label4.VerticalContentAlignment = VerticalAlignment.Center;
			label4.VerticalAlignment = VerticalAlignment.Center;
			label4.Margin = new Thickness(70, 0, 0, 10);
			wrapPanel.Children.Add(textBox);
			wrapPanel.Children.Add(label4);
			stackPanel.Children.Add(wrapPanel);
			border.Child = stackPanel;
			main_wrap.Children.Add(border);
			counter++;
			return main_wrap;
		}
	}
	public partial class admin_desktop : Window
	{
		
		string connection_string = " Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30";
		public bool check_add_menu_condition()
		{
			if (hamburger.IsChecked == false && pizza.IsChecked == false && sandwitch.IsChecked == false && traditional.IsChecked == false)
			{
				return false;
			}

			if (Name_Food.Text == "Name Of Food" || Name_Food.Text == "")
			{
				return false;
			}

			if (Cost_Food.Text == "Made Cost Of Food" || Name_Food.Text == "")
			{
				return false;
			}

			if (Date_Food.Text == "Date Of Food" || Name_Food.Text == "")
			{
				return false;
			}

			if (Information_Food.Text == "information" || Name_Food.Text == "")
			{
				return false;
			}

			return true;
		}
		public admin_desktop()
		{
			InitializeComponent();
			Orders.counter = 2;
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select Orders,FullName from Users", sqlConnection);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				if (Orders.counter == 2)
				{
					All_Orders.Children.Add(Orders.GetData(sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString()));
				}

				else
				{
					Orders.GetData(sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString());
				}
			}

			sqlDataReader.Close();
			sqlCommand.Dispose();

			sqlCommand = new SqlCommand("select * from admin ", sqlConnection);
			sqlDataReader = sqlCommand.ExecuteReader();
			sqlDataReader.Read();
			sqlCommand.Dispose();
			admin_Name.Text = sqlDataReader.GetValue(0).ToString();
			admin_Restaurant_region.Text = sqlDataReader.GetValue(1).ToString();
			admin_Restaurant_kind.Text = sqlDataReader.GetValue(2).ToString();
			admin_Restaurant_Address.Text = sqlDataReader.GetValue(3).ToString();
			var brush = new ImageBrush();
			brush.ImageSource = new BitmapImage(new Uri(sqlDataReader.GetValue(4).ToString()));
			restaurant_menu.Fill = brush;
			sqlDataReader.Close();

			menu.counter = 3;
			List.counter = 3;
			sqlCommand = new SqlCommand("select * from Food_Menu", sqlConnection);
			sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				if (menu.counter == 3)
				{
					main_stack21.Children.Add(menu.construct_food("_" + sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString(), sqlDataReader.GetValue(2).ToString(), sqlDataReader.GetValue(3).ToString(), sqlDataReader.GetValue(4).ToString(), sqlDataReader.GetValue(5).ToString()));
					main_stack.Children.Add(List.construct_food(sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString(), sqlDataReader.GetValue(2).ToString(), sqlDataReader.GetValue(3).ToString(), sqlDataReader.GetValue(4).ToString(), sqlDataReader.GetValue(5).ToString(), sqlDataReader.GetValue(6).ToString()));
					menu.uploaded_image_uri = @"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\-res1.jpg";
					List.uploaded_image_uri = @"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\-res1.jpg";

				}

				else
				{
					menu.construct_food("_" + sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString(), sqlDataReader.GetValue(2).ToString(), sqlDataReader.GetValue(3).ToString(), sqlDataReader.GetValue(4).ToString(), sqlDataReader.GetValue(5).ToString());
					List.construct_food(sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString(), sqlDataReader.GetValue(2).ToString(), sqlDataReader.GetValue(3).ToString(), sqlDataReader.GetValue(4).ToString(), sqlDataReader.GetValue(5).ToString(), sqlDataReader.GetValue(6).ToString());
					menu.uploaded_image_uri = @"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\-res1.jpg";
					List.uploaded_image_uri = @"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\-res1.jpg";
				}
			}
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlConnection.Close();
		}

		private void upload_Click(object sender, RoutedEventArgs e)
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
					menu.uploaded_image_uri = open.FileName;
					List.uploaded_image_uri = open.FileName;
					uploaded_image.Fill = brush;
				}
			}

		}

		private void add_Click(object sender, RoutedEventArgs e)
		{
			if (!check_add_menu_condition())
			{
				MessageBox.Show("Some Food Fields are incompelete To Add in Menu");
				return;
			}
			if (Name_Food.Text.Contains(" "))
			{
				MessageBox.Show("Food Name Can't contain whitespace");
				return;
			}
			if (!Regex.IsMatch(Date_Food.Text, @"\d\d/\d\d/\d\d\d\d"))
			{
				MessageBox.Show(@"Date format must be like DD/MM/YYYY");
				return;
			}

			if (DateTime.Compare(DateTime.Parse(Date_Food.Text), DateTime.Today) < 0)
			{
				MessageBox.Show(@"You can't set Date earlier than today");
				return;
			}
			bool Is_added = false;
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select Name_Food from Food_Menu", sqlConnection);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				if (Name_Food.Text == sqlDataReader.GetValue(0).ToString())
				{
					MessageBox.Show("Item with same Name Has been already added to Menu");
					Is_added = true;
					sqlConnection.Close();
					sqlCommand.Dispose();
					sqlDataReader.Close();
					break;
				}
			}
			sqlConnection.Close();
			sqlCommand.Dispose();
			sqlDataReader.Close();

			if (!Is_added)
			{
				sqlConnection = new SqlConnection(connection_string);
				sqlConnection.Open();
				sqlCommand = new SqlCommand("INSERT INTO Food_Menu (Name_Food, Cost_Food, Information_Food,Date_Food,Uri_Food,Kind_Food,Number_Food) VALUES (@Name_Food,@Cost_Food,@Information_Food,@Date_Food,@Uri_Food,@Kind_Food,@Number_Food)", sqlConnection);
				sqlCommand.Parameters.AddWithValue("@Name_Food", Name_Food.Text);
				sqlCommand.Parameters.AddWithValue("@Cost_Food", (double.Parse(Cost_Food.Text) * 1.24).ToString());
				sqlCommand.Parameters.AddWithValue("@Information_Food", Information_Food.Text);
				string date1 = Date_Food.Text.Substring(0, 2);
				string date2 = Date_Food.Text.Substring(3, 2);
				string date3 = Date_Food.Text.Substring(6, 4);
				date1 = date1.StartsWith("0") ? date1.Substring(1, 1) : date1;
				date2 = date2.StartsWith("0") ? date2.Substring(1, 1) : date2;
				Date_Food.Text = date1 + "/" + date2 + "/" + date3;
				sqlCommand.Parameters.AddWithValue("@Date_Food", Date_Food.Text);
				sqlCommand.Parameters.AddWithValue("@Uri_Food", menu.uploaded_image_uri);
				sqlCommand.Parameters.AddWithValue("@Number_Food", 1);
				if (hamburger.IsChecked == true)
				{
					sqlCommand.Parameters.AddWithValue("@Kind_Food", "hamburger");
				}
				else if (pizza.IsChecked == true)
				{
					sqlCommand.Parameters.AddWithValue("@Kind_Food", "pizza");

				}
				else if (sandwitch.IsChecked == true)
				{
					sqlCommand.Parameters.AddWithValue("@Kind_Food", "sandwitch");

				}
				else if (traditional.IsChecked == true)
				{
					sqlCommand.Parameters.AddWithValue("@Kind_Food", "traditional");

				}
				sqlCommand.ExecuteNonQuery();
				sqlConnection.Close();
				sqlCommand.Dispose();
				sqlConnection = new SqlConnection(connection_string);
				sqlConnection.Open();
				sqlCommand = new SqlCommand("select * from Food_Menu where Name_Food=@Name_Food", sqlConnection);
				sqlCommand.Parameters.AddWithValue("@Name_Food", Name_Food.Text);
				sqlDataReader = sqlCommand.ExecuteReader();
				sqlDataReader.Read();
				if (menu.counter == 3)
				{
					main_stack21.Children.Add(menu.construct_food("_" + sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString(), sqlDataReader.GetValue(2).ToString(), sqlDataReader.GetValue(3).ToString(), sqlDataReader.GetValue(4).ToString(), sqlDataReader.GetValue(5).ToString()));
					main_stack.Children.Add(List.construct_food(sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString(), sqlDataReader.GetValue(2).ToString(), sqlDataReader.GetValue(3).ToString(), sqlDataReader.GetValue(4).ToString(), sqlDataReader.GetValue(5).ToString(), "1"));
					menu.uploaded_image_uri = @"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\-res1.jpg";
					List.uploaded_image_uri = @"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\-res1.jpg";
					var brush = new ImageBrush();
					brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\-res1.jpg"));
					uploaded_image.Fill = brush;
					Name_Food.Text = "Name Of Food";
					Cost_Food.Text = "Cost Of Food";
					Information_Food.Text = "Information Of Food";
					Date_Food.Text = "Date Of Food";
				}

				else
				{
					menu.construct_food("_" + sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString(), sqlDataReader.GetValue(2).ToString(), sqlDataReader.GetValue(3).ToString(), sqlDataReader.GetValue(4).ToString(), sqlDataReader.GetValue(5).ToString());
					List.construct_food(sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString(), sqlDataReader.GetValue(2).ToString(), sqlDataReader.GetValue(3).ToString(), sqlDataReader.GetValue(4).ToString(), sqlDataReader.GetValue(5).ToString(), "1");
					menu.uploaded_image_uri = @"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\-res1.jpg";
					List.uploaded_image_uri = @"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\-res1.jpg";
					var brush = new ImageBrush();
					brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\-res1.jpg"));
					uploaded_image.Fill = brush;
					Name_Food.Text = "Name Of Food";
					Cost_Food.Text = "Cost Of Food";
					Information_Food.Text = "Information Of Food";
					Date_Food.Text = "Date Of Food";
				}
			}
			sqlConnection.Close();
			sqlCommand.Dispose();
			sqlDataReader.Close();

		}

		private void exit_Selected(object sender, RoutedEventArgs e)
		{
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("update admin set FullName=@FullName ,Restaurant_Region=@Restaurant_Region ,Restaurant_Kind=@Restaurant_Kind ,Restaurant_Address=@Restaurant_Address", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@FullName", admin_Name.Text);
			sqlCommand.Parameters.AddWithValue("@Restaurant_Region", admin_Restaurant_region.Text);
			sqlCommand.Parameters.AddWithValue("@Restaurant_Kind", admin_Restaurant_kind.Text);
			sqlCommand.Parameters.AddWithValue("@Restaurant_Address", admin_Restaurant_Address.Text);
			sqlCommand.ExecuteNonQuery();
			sqlCommand.Dispose();
			sqlConnection.Close();
			MainWindow we = new MainWindow();
			MessageBox.Show("You Exited Successfully");
			we.Show();
			this.Close();
		}

		private void Upload_Menu_image_Click(object sender, RoutedEventArgs e)
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
					restaurant_menu.Fill = brush;
					SqlConnection sqlConnection = new SqlConnection(connection_string);
					sqlConnection.Open();
					SqlCommand sqlCommand = new SqlCommand("update admin set Menu_Uri=@Menu_Uri ", sqlConnection);
					sqlCommand.Parameters.AddWithValue("@Menu_Uri", open.FileName);
					sqlCommand.ExecuteNonQuery();
					sqlCommand.Dispose();
					sqlConnection.Close();
				}
			}
		}

		private void hamburger_Checked(object sender, RoutedEventArgs e)
		{
			traditional.IsChecked = sandwitch.IsChecked = pizza.IsChecked = false;
		}

		private void traditional_Checked(object sender, RoutedEventArgs e)
		{
			hamburger.IsChecked = sandwitch.IsChecked = pizza.IsChecked = false;
		}

		private void sandwitch_Checked(object sender, RoutedEventArgs e)
		{
			hamburger.IsChecked = traditional.IsChecked = pizza.IsChecked = false;
		}

		private void pizza_Checked(object sender, RoutedEventArgs e)
		{
			hamburger.IsChecked = traditional.IsChecked = sandwitch.IsChecked = false;
		}

		private void Name_Food_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^A-Za-z]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void Cost_Food_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void Information_Food_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{

		}

		private void Date_Food_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex(@"[^0-9/]");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void refresh_btn_Click(object sender, RoutedEventArgs e)
		{
			admin_desktop w = new admin_desktop();
			w.Show();
			this.Close();
		}
	}
}
