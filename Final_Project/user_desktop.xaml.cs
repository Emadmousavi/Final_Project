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
using System.Text.RegularExpressions;
using System.Reflection;
using System.Globalization;
using Renci.SshNet.Messages;
using System.Windows.Controls.Primitives;
using System.Runtime.CompilerServices;

namespace Final_Project
{
	public class Search_item
	{
		public string Name_Food { get; set; }
		public string Cost_Food { get; set; }
		public string Date_Food { get; set; }
		public string Information_Food { get; set; }
		public string Uri_Food { get; set; }
		public string Kind_Food { get; set; }
		public string Number_Food { get; set; }


	}

	public static class User_List
	{
		public static string GetParents(Object element, int parentLevel)
		{
			string returnValue = String.Format("[{0}] {1}", parentLevel, element.GetType());
			if (element is FrameworkElement)
			{
				if (((FrameworkElement)element).Parent != null)
					returnValue += String.Format("{0}{1}",
						Environment.NewLine, GetParents(((FrameworkElement)element).Parent, parentLevel + 1));
			}
			return returnValue;
		}

		public static void btn_click(object sender, RoutedEventArgs e)
		{
			string connection_string = " Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project\\Final_Project\\database.mdf\"; Integrated Security = True; Connect Timeout = 30";
			MessageBox.Show("Item Added to Cart");
			Button btn = e.Source as Button;
			WrapPanel wp = btn.Parent as WrapPanel;
			StackPanel st = wp.Parent as StackPanel;
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("update Users set Cart +=@Cart where  FullName=@FullName", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@Cart",","+ st.Name);
			sqlCommand.Parameters.AddWithValue("@FullName", Current_user.FullName);
			sqlCommand.ExecuteNonQuery();
			sqlCommand.Dispose();
			sqlConnection.Close();
		}

		public static void stack_click(object sender, RoutedEventArgs e)
		{
			string connection_string = " Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project\\Final_Project\\database.mdf\"; Integrated Security = True; Connect Timeout = 30";
			Button btn = e.Source as Button;
			WrapPanel wp = btn.Parent as WrapPanel;
			StackPanel st = wp.Parent as StackPanel;
			int number = int.Parse((((TextBlock)st.Children[3]).Text));
			number--;	
			((TextBlock)st.Children[3]).Text = number.ToString();
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("update Food_Menu set Number_Food=@Number_Food where Name_Food=@Name_Food or Name_Food=@Name_Food1 or Name_Food=@Name_Food2", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@Number_Food", number.ToString());
			sqlCommand.Parameters.AddWithValue("@Name_Food", st.Name);
			sqlCommand.Parameters.AddWithValue("@Name_Food1", "_"+st.Name);
			sqlCommand.Parameters.AddWithValue("@Name_Food2",st.Name.Substring(1));
			sqlCommand.ExecuteNonQuery();
			sqlCommand.Dispose();
			sqlConnection.Close();

		}

		public static string RemoveWhitespace(this string input)
		{
			return new string(input.ToCharArray()
				.Where(c => !Char.IsWhiteSpace(c))
				.ToArray());
		}
		public static int counter = 2;
		public static WrapPanel main_wrap;
		public static WrapPanel construct_food(string Name_Food, string Cost_Food, string Information_Food, string Date_Food,string Uri_Food,string Kind_Food,string Number_Food)
		{
			if (counter == 2)
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
			stackPanel.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(stack_click));
			stackPanel.Name = RemoveWhitespace(Name_Food);
			var bc = new BrushConverter();
			stackPanel.Background = (Brush)bc.ConvertFrom("#FFB28E6A");
			stackPanel.Height = 210;
			stackPanel.Width = 163;
			Ellipse ellipse = new Ellipse();
			ellipse.Name = "ellipse";
			ellipse.Width = 100;
			ellipse.Height = 100;
			ellipse.Margin = new Thickness(10, 10, 10, 10);
			ImageBrush ellipse_image = new ImageBrush();
			ellipse_image.ImageSource = new BitmapImage(new Uri(Uri_Food));
			ellipse.Fill = ellipse_image;
			Label label = new Label();
			label.Name = "food_name";
			label.Content = RemoveWhitespace(Name_Food);
			label.HorizontalAlignment = HorizontalAlignment.Center;
			Label label2 = new Label();
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
			Button btn = new Button();
			btn.Name = "increase";
			var a = new ImageBrush();
			a.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\plus.png"));
			btn.Background = a;
			btn.Width = 15;
			btn.Height = 15;
			btn.Margin = new Thickness(5, -1, 10, 20);
			btn.Click += new RoutedEventHandler(btn_click);
			TextBlock textBlock = new TextBlock();
			textBlock.Name = "Number";
			textBlock.Text = Number_Food;
			textBlock.FontSize = 10;
			textBlock.Margin = new Thickness(8, -5, 10, 0);
			Label label4 = new Label();
			label4.Name = "food_price";
			label4.Content = Cost_Food+"$";
			label4.HorizontalAlignment = HorizontalAlignment.Right;
			label4.HorizontalContentAlignment = HorizontalAlignment.Right;
			label4.VerticalContentAlignment = VerticalAlignment.Center;
			label4.VerticalAlignment = VerticalAlignment.Center;
			label4.Margin = new Thickness(85, -10, 0, 20);
			stackPanel.Children.Add(textBlock);
			wrapPanel.Children.Add(btn);
			wrapPanel.Children.Add(label4);
			stackPanel.Children.Add(wrapPanel);
			border.Child = stackPanel;
			main_wrap.Children.Add(border);
			counter++;
			return main_wrap;
		}
	}
	public partial class user_desktop : Window
	{
		Window creatingForm;
		public Window setCreatingForm
		{
			get { return creatingForm; }
			set { creatingForm = value; }
		}
		string connection_string = " Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project\\Final_Project\\database.mdf\"; Integrated Security = True; Connect Timeout = 30";

		user_menu w;
		bool open = false;
		List<Search_item> search_Table = new List<Search_item>();
		public user_desktop()
		{
			InitializeComponent();
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select * from Food_Menu", sqlConnection);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				var item = new Search_item();
				item.Name_Food = sqlDataReader.GetValue(0).ToString();
				item.Cost_Food = sqlDataReader.GetValue(1).ToString();
				item.Information_Food = sqlDataReader.GetValue(2).ToString();
				item.Date_Food = sqlDataReader.GetValue(3).ToString();
				item.Uri_Food = sqlDataReader.GetValue(4).ToString();
				item.Kind_Food = sqlDataReader.GetValue(5).ToString();
				item.Number_Food = sqlDataReader.GetValue(6).ToString();
				search_Table.Add(item);
			}
			sqlCommand.Dispose();
			sqlConnection.Close();
			User_List.counter = 2;
			DateTime startDate = DateTime.Today;
			DateTime endDate = startDate.AddMonths(1);
			calendar.DisplayDateEnd = endDate;
			user_name.Text = Current_user.FullName;
			sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			sqlCommand = new SqlCommand("select * from Food_Menu", sqlConnection);
			sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				if (User_List.counter == 2)
				{
					Main_stack.Children.Add(User_List.construct_food(sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString(), sqlDataReader.GetValue(2).ToString(), sqlDataReader.GetValue(3).ToString(), sqlDataReader.GetValue(4).ToString(), sqlDataReader.GetValue(5).ToString(), sqlDataReader.GetValue(6).ToString()));
				}

				else
				{
					User_List.construct_food(sqlDataReader.GetValue(0).ToString(), sqlDataReader.GetValue(1).ToString(), sqlDataReader.GetValue(2).ToString(), sqlDataReader.GetValue(3).ToString(), sqlDataReader.GetValue(4).ToString(), sqlDataReader.GetValue(5).ToString(), sqlDataReader.GetValue(6).ToString());
				}
			}
			sqlConnection.Close();
			sqlCommand.Dispose();
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
				w.setCreatingForm = this;
				w.Show();
				open = true;
			}
		}

		private void Name_search_Click(object sender, RoutedEventArgs e)
		{
			Main_stack.Children.Clear();
			User_List.counter = 2;
			var list = search_Table.Where(x => x.Name_Food == Food_search_txt.Text).ToList();
			foreach (var item in list)
			{
				if (User_List.counter == 2)
				{
					Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
				}

				else
				{
					User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
				}
			}
		}

		private void Kind_search_Click(object sender, RoutedEventArgs e)
		{
			Main_stack.Children.Clear();
			User_List.counter = 2;
			var list = new List<Search_item>();

			//1
			if (traditional.IsChecked == false && pizza.IsChecked == false && hamburger.IsChecked == false && sandwitch.IsChecked == false)
			{
				list = search_Table.Where(x => x.Kind_Food=="").ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			//2
			else if (traditional.IsChecked == true && pizza.IsChecked == true && hamburger.IsChecked == true && sandwitch.IsChecked == true)
			{
				list = search_Table;
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			//3
			else if (traditional.IsChecked == true && pizza.IsChecked == true && hamburger.IsChecked == true && sandwitch.IsChecked == false)
			{
				list = search_Table.Where(x => x.Kind_Food !="sandwitch").ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			//4
			else if (traditional.IsChecked == true && pizza.IsChecked == true && hamburger.IsChecked == false && sandwitch.IsChecked == true)
			{
				list = search_Table.Where(x => x.Kind_Food != "hamburger").ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			//5
			else if (traditional.IsChecked == true && pizza.IsChecked == true && hamburger.IsChecked == false && sandwitch.IsChecked == false)
			{
				list = search_Table.Where(x => x.Kind_Food == "traditional" || x.Kind_Food == "pizza").ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			//6
			else if (traditional.IsChecked == true && pizza.IsChecked == false && hamburger.IsChecked == true && sandwitch.IsChecked == true)
			{
				list = search_Table.Where(x => x.Kind_Food != "pizza").ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			//7
			else if (traditional.IsChecked == true && pizza.IsChecked == false && hamburger.IsChecked == true && sandwitch.IsChecked == false)
			{
				list = search_Table.Where(x => x.Kind_Food == "traditional" || x.Kind_Food == "hamburger").ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			//8
			else if (traditional.IsChecked == true && pizza.IsChecked == false && hamburger.IsChecked == false && sandwitch.IsChecked == true)
			{
				list = search_Table.Where(x => x.Kind_Food == "traditional" || x.Kind_Food == "sandwitch").ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			//9
			else if (traditional.IsChecked == true && pizza.IsChecked == false && hamburger.IsChecked == false && sandwitch.IsChecked == false)
			{
				list = search_Table.Where(x => x.Kind_Food == "traditional" ).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			//10
			else if (traditional.IsChecked == false && pizza.IsChecked == true && hamburger.IsChecked == true && sandwitch.IsChecked == true)
			{
				list = search_Table.Where(x => x.Kind_Food != "traditional").ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			//11
			else if (traditional.IsChecked == false && pizza.IsChecked == true && hamburger.IsChecked == true && sandwitch.IsChecked == false)
			{
				list = search_Table.Where(x => x.Kind_Food == "pizza" || x.Kind_Food == "hamburger").ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			//12
			else if (traditional.IsChecked == false && pizza.IsChecked == true && hamburger.IsChecked == false && sandwitch.IsChecked == true)
			{
				list = search_Table.Where(x => x.Kind_Food == "sandwitch" || x.Kind_Food == "pizza").ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			//13
			else if (traditional.IsChecked == false && pizza.IsChecked == true && hamburger.IsChecked == false && sandwitch.IsChecked == false)
			{
				list = search_Table.Where(x => x.Kind_Food == "pizza" ).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			//14
			else if (traditional.IsChecked == false && pizza.IsChecked == false && hamburger.IsChecked == true && sandwitch.IsChecked == true)
			{
				list = search_Table.Where(x => x.Kind_Food == "hamburger" || x.Kind_Food == "sandwitch").ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			//15
			else if (traditional.IsChecked == false && pizza.IsChecked == false && hamburger.IsChecked == true && sandwitch.IsChecked == false)
			{
				list = search_Table.Where(x => x.Kind_Food == "hamburger" ).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			//16
			else if (traditional.IsChecked == false && pizza.IsChecked == false && hamburger.IsChecked == false && sandwitch.IsChecked == true)
			{
				list = search_Table.Where(x => x.Kind_Food == "sandwitch" ).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}
		}

		private void Date_search_Click(object sender, RoutedEventArgs e)
		{
			Main_stack.Children.Clear();
			User_List.counter = 2;
			var list = new List<Search_item>();

			//1
			if ((ingridient_txt.Text.Equals("") == true || ingridient_txt.Text.Equals("Ingridient") == true) && (price_txt.Text.Equals("") == true || price_txt.Text.Equals("Price") == true) &&  calendar.Text != "")
			{
				list = search_Table.Where(x => x.Date_Food == calendar.Text).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
				calendar.Text = "";
			}

			//2
			else if ((ingridient_txt.Text.Equals("") == true || ingridient_txt.Text.Equals("Ingridient") == true) && (price_txt.Text.Equals("") == true || price_txt.Text.Equals("Price") == true) && calendar.Text == "")
			{
				calendar.Text = "";
				return;
			}

			//3
			else if ((ingridient_txt.Text.Equals("") == true || ingridient_txt.Text.Equals("Ingridient") == true) && (price_txt.Text.Equals("") == false || price_txt.Text.Equals("Price") == false) &&  calendar.Text != "")
			{
				list = search_Table.Where(x => x.Date_Food == calendar.Text && int.Parse(x.Cost_Food) <= int.Parse(price_txt.Text)).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
				calendar.Text = "";
			}

			//4
			else if ((ingridient_txt.Text.Equals("") == true || ingridient_txt.Text.Equals("Ingridient") == true) && (price_txt.Text.Equals("") == false || price_txt.Text.Equals("Price") == false) &&  calendar.Text == "")
			{
				list = search_Table.Where(x=> int.Parse(x.Cost_Food) <= int.Parse(price_txt.Text)).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
				calendar.Text = "";
			}

			//5
			else if ((ingridient_txt.Text.Equals("") == false || ingridient_txt.Text.Equals("Ingridient") == false) && (price_txt.Text.Equals("") == true || price_txt.Text.Equals("Price") == true) &&  calendar.Text != "")
			{
				list = search_Table.Where(x => x.Date_Food == calendar.Text && x.Information_Food.Split(',').Any(y=> y==ingridient_txt.Text)).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
				calendar.Text = "";
			}

			//6
			else if ((ingridient_txt.Text.Equals("") == false || ingridient_txt.Text.Equals("Ingridient") == false) && (price_txt.Text.Equals("") == true || price_txt.Text.Equals("Price") == true) &&  calendar.Text == "")
			{
				list = search_Table.Where(x => x.Information_Food.Split(',').Any(y => y == ingridient_txt.Text)).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
				calendar.Text = "";
			}

			//7
			else if ((ingridient_txt.Text.Equals("") == false || ingridient_txt.Text.Equals("Ingridient") == false) && (price_txt.Text.Equals("") == false || price_txt.Text.Equals("Price") == false) &&  calendar.Text != "")
			{
				list = search_Table.Where(x => x.Date_Food == calendar.Text && int.Parse(x.Cost_Food) <= int.Parse(price_txt.Text) && x.Information_Food.Split(',').Any(y => y == ingridient_txt.Text)).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
				calendar.Text = "";
			}

			//8
			else if ((ingridient_txt.Text.Equals("") == false || ingridient_txt.Text.Equals("Ingridient") == false) && (price_txt.Text.Equals("") == false || price_txt.Text.Equals("Price") == false) &&  calendar.Text == "")
			{
				list = search_Table.Where(x=> x.Information_Food.Split(',').Any(y => y == ingridient_txt.Text) && int.Parse(x.Cost_Food) <= int.Parse(price_txt.Text)).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}
			calendar.Text = "";
		}
		private void week_search_Click(object sender, RoutedEventArgs e)
		{
			DateTime date = new DateTime();
			Main_stack.Children.Clear();
			User_List.counter = 2;
			List<Search_item> list = new List<Search_item>();

			if (saturday.IsChecked == true)
			{
				list = search_Table.Where(x => (int)DateTime.Parse(x.Date_Food, CultureInfo.InvariantCulture).DayOfWeek == 6).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			else if (sunday.IsChecked == true)
			{
				list = search_Table.Where(x => (int)DateTime.Parse(x.Date_Food, CultureInfo.InvariantCulture).DayOfWeek == 0).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			else if (monday.IsChecked == true)
			{
				list = search_Table.Where(x => (int)DateTime.Parse(x.Date_Food, CultureInfo.InvariantCulture).DayOfWeek == 1).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			else if (tuesday.IsChecked == true)
			{
				list = search_Table.Where(x => (int)DateTime.Parse(x.Date_Food, CultureInfo.InvariantCulture).DayOfWeek == 2).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			else if (wednesday.IsChecked == true)
			{
				list = search_Table.Where(x => (int)DateTime.Parse(x.Date_Food, CultureInfo.InvariantCulture).DayOfWeek == 3).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			else if (thursday.IsChecked == true)
			{
				list = search_Table.Where(x => (int)DateTime.Parse(x.Date_Food, CultureInfo.InvariantCulture).DayOfWeek == 4).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}

			else if (friday.IsChecked == true)
			{
				list = search_Table.Where(x => (int)DateTime.Parse(x.Date_Food, CultureInfo.InvariantCulture).DayOfWeek == 5).ToList();
				foreach (var item in list)
				{
					if (User_List.counter == 2)
					{
						Main_stack.Children.Add(User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food));
					}

					else
					{
						User_List.construct_food(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food);
					}
				}
			}
		}

		private void price_txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^0-9]");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void ingridient_txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			Regex regex = new Regex("[^a-zA-Z]");
			e.Handled = regex.IsMatch(e.Text);
		}


		private void saturday_Checked(object sender, RoutedEventArgs e)
		{
			 sunday.IsChecked = monday.IsChecked = tuesday.IsChecked = wednesday.IsChecked = thursday.IsChecked = friday.IsChecked = false;
		}
		private void sunday_Checked(object sender, RoutedEventArgs e)
		{
			saturday.IsChecked =  monday.IsChecked = tuesday.IsChecked = wednesday.IsChecked = thursday.IsChecked = friday.IsChecked = false;
		}

		private void monday_Checked(object sender, RoutedEventArgs e)
		{
			saturday.IsChecked = sunday.IsChecked =  tuesday.IsChecked = wednesday.IsChecked = thursday.IsChecked = friday.IsChecked = false;
		}

		private void tuesday_Checked(object sender, RoutedEventArgs e)
		{
			saturday.IsChecked = sunday.IsChecked = monday.IsChecked =  wednesday.IsChecked = thursday.IsChecked = friday.IsChecked = false;
		}

		private void wednesday_Checked(object sender, RoutedEventArgs e)
		{
			saturday.IsChecked = sunday.IsChecked = monday.IsChecked = tuesday.IsChecked = thursday.IsChecked = friday.IsChecked = false;
		}

		private void thursday_Checked(object sender, RoutedEventArgs e)
		{
			saturday.IsChecked = sunday.IsChecked = monday.IsChecked = tuesday.IsChecked = wednesday.IsChecked =  friday.IsChecked = false;
		}

		private void friday_Checked(object sender, RoutedEventArgs e)
		{
			saturday.IsChecked = sunday.IsChecked = monday.IsChecked = tuesday.IsChecked = wednesday.IsChecked = thursday.IsChecked = false;
		}

		private void cart_btn_Click(object sender, RoutedEventArgs e)
		{
			user_cart w = new user_cart();
			w.Show();
			if (creatingForm != null)
				creatingForm.Close();
			this.Close();	
		}
	}
}
