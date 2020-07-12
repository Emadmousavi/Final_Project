using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting;
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

namespace Final_Project
{
	/// <summary>
	/// Interaction logic for user_cart.xaml
	/// </summary>
	public partial class user_cart : Window
	{
		public static void btn_click(object sender, RoutedEventArgs e)
		{
			string connection_string = " Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project\\Final_Project\\database.mdf\"; Integrated Security = True; Connect Timeout = 30";
			Button btn = e.Source as Button;
			WrapPanel wp = btn.Parent as WrapPanel;
			StackPanel st = wp.Parent as StackPanel;
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("update Users set Cart +=@Cart where  FullName=@FullName", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@Cart", "," + st.Name);
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
			sqlCommand.Parameters.AddWithValue("@Name_Food1", "_" + st.Name);
			sqlCommand.Parameters.AddWithValue("@Name_Food2", st.Name.Substring(1));
			sqlCommand.ExecuteNonQuery();
			sqlCommand.Dispose();
			sqlConnection.Close();

		}

		public class Food_Item
		{
			public string Name_Food { get; set; }
			public string Cost_Food { get; set; }
			public string Date_Food { get; set; }
			public string Information_Food { get; set; }
			public string Uri_Food { get; set; }
			public string Kind_Food { get; set; }
			public string Number_Food { get; set; }


		}
		string connection_string = " Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project\\Final_Project\\database.mdf\"; Integrated Security = True; Connect Timeout = 30";
		public static class Cart
		{
			public static StackPanel Construct(string Name_Food, string Cost_Food, string Information_Food, string Date_Food, string Uri_Food, string Kind_Food, string Number_Food,List<string> cart)
			{
				StackPanel stackPanel = new StackPanel();
				stackPanel.Orientation = Orientation.Horizontal;
				stackPanel.Height = 200;
				Border border = new Border();
				border.BorderThickness = new Thickness(2, 2, 2, 2);
				border.CornerRadius = new CornerRadius(20, 20, 20, 20);
				border.Width = 480;
				border.Margin = new Thickness(14, 14, 14, 14);
				var bc = new BrushConverter();
				border.BorderBrush = (Brush)bc.ConvertFrom("#FF686464");
				StackPanel stack = new StackPanel();
				Label lable1 = new Label();
				lable1.Content = "Price : ";
				lable1.FontWeight = FontWeights.Bold;
				lable1.FontStyle = FontStyles.Italic;
				lable1.Content += Cost_Food + "$";
				lable1.FontSize = 15;
				lable1.Margin = new Thickness(-18, 0, 0, 0);
				lable1.HorizontalContentAlignment = HorizontalAlignment.Left;
				lable1.Margin = new Thickness(30, 5, 30, 5);
				Label lable2 = new Label();
				lable2.Margin = new Thickness(-8, 0, 0, 0);
				lable2.Content = "Ingridient : ";
				lable2.FontWeight = FontWeights.Bold;
				lable2.FontStyle = FontStyles.Italic;
				lable2.Content += Information_Food;
				lable2.FontSize = 15;
				lable2.HorizontalContentAlignment = HorizontalAlignment.Left;
				lable2.Margin = new Thickness(30, 5, 30, 5);
				Label lable3 = new Label();
				lable3.Margin = new Thickness(-18, 0, 0, 0);
				lable3.Content = "Name : ";
				lable3.FontWeight = FontWeights.Bold;
				lable3.FontStyle = FontStyles.Italic;
				lable3.Content += Name_Food;
				lable3.FontWeight = FontWeights.Bold;
				lable3.FontStyle = FontStyles.Italic;
				lable3.FontSize = 15;
				lable3.HorizontalContentAlignment = HorizontalAlignment.Left;
				Label lable6 = new Label();
				lable6.Margin = new Thickness(-18, 0, 0, 0);
				lable6.Content = "Number : ";
				lable6.FontWeight = FontWeights.Bold;
				lable6.FontStyle = FontStyles.Italic;
				lable6.Content +=((int)cart.Where(x=>x==Name_Food).Count()).ToString() ;
				lable6.FontSize = 15;
				lable6.HorizontalContentAlignment = HorizontalAlignment.Left;
				lable6.Margin = new Thickness(30, 5, 30, 5);
				lable3.Margin = new Thickness(30, 5, 30, 5);
				stack.Children.Add(lable3);
				stack.Children.Add(lable2);
				stack.Children.Add(lable1);
				stack.Children.Add(lable6);
				border.Child = stack;
				StackPanel stack2 = new StackPanel();
				stack2.Margin = new Thickness(0, 40, 0, 0);
				Ellipse ellipse = new Ellipse();
				ellipse.Width = 100;
				ellipse.Height = 100;
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(Uri_Food));
				ellipse.Fill = brush;
				StackPanel stack3 = new StackPanel();
				stack3.Orientation = Orientation.Horizontal;
				Label lable4 = new Label();
				lable4.Content = "+";
				lable4.FontSize = 35;
				bc = new BrushConverter();
				lable4.Foreground = (Brush)bc.ConvertFrom("#FFC14F25");
				lable4.Margin = new Thickness(15, -10, 5, 0);
				Label lable5 = new Label();
				lable5.Content = "-";
				lable5.FontSize = 35;
				bc = new BrushConverter();
				lable5.Foreground = (Brush)bc.ConvertFrom("#FFC14F25");
				lable5.Margin = new Thickness(5, -10, 5, 0);
				stack3.Children.Add(lable4);
				stack3.Children.Add(lable5);
				stack2.Children.Add(ellipse);
				stack2.Children.Add(stack3);
				stackPanel.Children.Add(stack2);
				stackPanel.Children.Add(border);

				return stackPanel;
			}
		}
		public user_cart()
		{
			InitializeComponent();
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select Cart from Users where FullName=@FullName", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@FullName", Current_user.FullName);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			sqlDataReader.Read();
			List<string> cart = sqlDataReader.GetValue(0).ToString().Split(',').ToList();
			sqlCommand.Dispose();
			sqlDataReader.Close();
			sqlCommand = new SqlCommand("select * from Food_Menu", sqlConnection);
		    sqlDataReader = sqlCommand.ExecuteReader();
			List<Food_Item> Food_List = new List<Food_Item>();
			while (sqlDataReader.Read())
			{
				var item = new Food_Item();
				item.Name_Food = sqlDataReader.GetValue(0).ToString();
				item.Cost_Food = sqlDataReader.GetValue(1).ToString();
				item.Information_Food = sqlDataReader.GetValue(2).ToString();
				item.Date_Food = sqlDataReader.GetValue(3).ToString();
				item.Uri_Food = sqlDataReader.GetValue(4).ToString();
				item.Kind_Food = sqlDataReader.GetValue(5).ToString();
				item.Number_Food = sqlDataReader.GetValue(6).ToString();
				Food_List.Add(item);
			}
			foreach (var item in Food_List.Where(x => cart.Contains(x.Name_Food)))
			{
				main_stack.Children.Add(Cart.Construct(item.Name_Food,item.Cost_Food,item.Information_Food,item.Date_Food,item.Uri_Food,item.Kind_Food,item.Number_Food,cart));
			}
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlConnection.Close();
		}

		private void exit_btn_Click(object sender, RoutedEventArgs e)
		{
			user_desktop w = new user_desktop();
			w.Show();
			this.Close();
		}
	}
}
