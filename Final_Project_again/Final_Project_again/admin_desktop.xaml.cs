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

			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select * from admin ", sqlConnection);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
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
