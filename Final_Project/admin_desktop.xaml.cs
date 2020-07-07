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

namespace Final_Project
{
	public static class menu
	{
		public static string uploaded_image_uri = @"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\-res1.jpg";
		public static int counter=3;
		public static WrapPanel main_wrap;
		public static WrapPanel construct_food(string Name_Food, string Cost_Food, string Information_Food, string Date_Food)
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
			stackPanel.Name = Name_Food;
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
			ellipse_image.ImageSource = new BitmapImage(new Uri(uploaded_image_uri));
			ellipse.Fill = ellipse_image;
			Label label = new Label();
			label.Name = "food_name";
			label.Content = Name_Food;
			label.HorizontalAlignment = HorizontalAlignment.Center;
			Label label2 = new Label();
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
			Button button1 = new Button();
			button1.Name = "plus";
			button1.Margin = new Thickness(7, 8, 0, 8);
			var brush = new ImageBrush();
			brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\plus.png"));
			brush.Stretch = Stretch.Uniform;
			button1.Background = brush;
			button1.Height = 25;
			button1.Width = 25;
			Button button2 = new Button();
			button2.Name = "minus";
			button2.Margin = new Thickness(5, 8, 5, 8);
		    brush = new ImageBrush();
			brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\min.png"));
			brush.Stretch = Stretch.Uniform;
			button2.Background = brush;
			button2.Height = 25;
			button2.Width = 25;
			Label label3 = new Label();
			label3.Name = "food_price";
			label3.Content = Cost_Food;
			label3.HorizontalAlignment = HorizontalAlignment.Right;
			label3.HorizontalContentAlignment = HorizontalAlignment.Right;
			label3.VerticalAlignment = VerticalAlignment.Center;
			wrapPanel.Children.Add(button1);
			wrapPanel.Children.Add(button2);
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
		public static string uploaded_image_uri = @"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\-res1.jpg";
		public static int counter = 6;
		public static WrapPanel main_wrap;
		public static WrapPanel construct_food(string Name_Food, string Cost_Food, string Information_Food, string Date_Food)
		{
			if (counter == 6)
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
			stackPanel.Name = Name_Food;
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
			ellipse_image.ImageSource = new BitmapImage(new Uri(uploaded_image_uri));
			ellipse.Fill = ellipse_image;
			Label label = new Label();
			label.Name = "food_name";
			label.Content = Name_Food;
			label.HorizontalAlignment = HorizontalAlignment.Center;
			Label label2 = new Label();
			label2.Name = "Date";
			label2.Content = Date_Food;
			label2.HorizontalAlignment = HorizontalAlignment.Center;
			Label label3 = new Label();
			label3.Name = "Number";
			label3.Content = "1";
			label3.Margin = new Thickness(10, -15,10,-15);
			label3.HorizontalContentAlignment = HorizontalAlignment.Left;
			label3.FontSize = 15;
			var b = new BrushConverter();
			label3.Foreground = (Brush)b.ConvertFrom("#FF7C0707");
			stackPanel.Children.Add(ellipse);
			stackPanel.Children.Add(label);
			stackPanel.Children.Add(label2);
			stackPanel.Children.Add(label3);
			WrapPanel wrapPanel = new WrapPanel();
			wrapPanel.Name = "wrappanel";
			wrapPanel.Margin = new Thickness(0, 5, 0, 0);
			wrapPanel.Height = 40;
			wrapPanel.Width = 160;
			Button button1 = new Button();
			button1.Name = "plus";
			button1.Margin = new Thickness(7, 8, 0, 8);
			var brush = new ImageBrush();
			brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\plus.png"));
			brush.Stretch = Stretch.Uniform;
			button1.Background = brush;
			button1.Height = 25;
			button1.Width = 25;
			Button button2 = new Button();
			button2.Name = "minus";
			button2.Margin = new Thickness(5, 8, 5, 8);
			brush = new ImageBrush();
			brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\min.png"));
			brush.Stretch = Stretch.Uniform;
			button2.Background = brush;
			button2.Height = 25;
			button2.Width = 25;
			Label label4 = new Label();
			label4.Name = "food_price";
			label4.Content = Cost_Food;
			label4.HorizontalAlignment = HorizontalAlignment.Right;
			label4.HorizontalContentAlignment = HorizontalAlignment.Right;
			label4.VerticalAlignment = VerticalAlignment.Center;
			wrapPanel.Children.Add(button1);
			wrapPanel.Children.Add(button2);
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
		string connection_string = " Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project\\Final_Project\\database.mdf\"; Integrated Security = True; Connect Timeout = 30";

		public admin_desktop()
		{
			InitializeComponent();
			
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
					brush.ImageSource= new BitmapImage(new Uri(open.FileName));
					menu.uploaded_image_uri = open.FileName;
					List.uploaded_image_uri = open.FileName;
					uploaded_image.Fill = brush;
				}
			}

		}

		private void add_Click(object sender, RoutedEventArgs e)
		{
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("INSERT INTO Food_Menu (Name_Food, Cost_Food, Information_Food,Date_Food,Uri_Food) VALUES (@Name_Food,@Cost_Food,@Information_Food,@Date_Food,@Uri_Food)", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@Name_Food", Name_Food.Text);
			sqlCommand.Parameters.AddWithValue("@Cost_Food", Cost_Food.Text);
			sqlCommand.Parameters.AddWithValue("@Information_Food", Information_Food.Text);
			sqlCommand.Parameters.AddWithValue("@Date_Food", Date_Food.Text);
			sqlCommand.Parameters.AddWithValue("@Uri_Food", menu.uploaded_image_uri);
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
			sqlCommand.Dispose();
			if (menu.counter == 3)
			{
				main_stack21.Children.Add(menu.construct_food("_"+Name_Food.Text, "_" + Cost_Food.Text, "_" + Information_Food.Text, "_" + Date_Food.Text));
				main_stack.Children.Add(List.construct_food(Name_Food.Text, Cost_Food.Text, Information_Food.Text, Date_Food.Text));
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
				menu.construct_food("_" + Name_Food.Text, "_" + Cost_Food.Text, "_" + Information_Food.Text, "_" + Date_Food.Text);
				List.construct_food(Name_Food.Text, Cost_Food.Text, Information_Food.Text, Date_Food.Text);
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

		private void exit_Selected(object sender, RoutedEventArgs e)
		{
			MainWindow w = new MainWindow();
			MessageBox.Show("you exited successfully");
			w.Show();
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
				}
			}
		}
	}
}
