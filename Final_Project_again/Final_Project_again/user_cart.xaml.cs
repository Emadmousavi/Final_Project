using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.CompilerServices;
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



namespace Final_Project_again
{
	/// <summary>
	/// Interaction logic for user_cart.xaml
	/// </summary>
	public partial class user_cart : Window
	{
		public static double current_order_cost = 0;
		Window creatingForm;
		public Window setCreatingForm
		{
			get { return creatingForm; }
			set { creatingForm = value; }
		}
		public static void lable_plus_click(object sender, RoutedEventArgs e)
		{
			string connection_string = " Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30";
			Label lb = e.Source as Label;
			StackPanel st = lb.Parent as StackPanel;
			StackPanel st1 = st.Parent as StackPanel;
			StackPanel st2 = st1.Parent as StackPanel;
			StackPanel st3 = st2.Parent as StackPanel;
			Border br = st3.Parent as Border;
			ScrollViewer sc = br.Parent as ScrollViewer;
			Grid grid = sc.Parent as Grid;
			Window window = grid.Parent as Window;

			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select Number_Food from Food_Menu where Name_Food=@Name_Food", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@Name_Food", ((Label)((StackPanel)((Border)st2.Children[1]).Child).Children[0]).Content.ToString().Substring(7));
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			sqlDataReader.Read();
			double current_Food_number = double.Parse(sqlDataReader.GetValue(0).ToString());
			sqlDataReader.Close();
			sqlCommand.Dispose();

			if (current_Food_number >= 1)
			{
				sqlCommand = new SqlCommand("update Users set Cart +=@Cart where  FullName=@FullName", sqlConnection);
				sqlCommand.Parameters.AddWithValue("@Cart", "," + ((Label)((StackPanel)((Border)st2.Children[1]).Child).Children[0]).Content.ToString().Substring(7));
				sqlCommand.Parameters.AddWithValue("@FullName", Current_user.FullName);
				sqlCommand.ExecuteNonQuery();
				sqlCommand.Dispose();
				double number = double.Parse(((Label)((StackPanel)((Border)st2.Children[1]).Child).Children[3]).Content.ToString().Substring(9));
				number++;
				((Label)((StackPanel)((Border)st2.Children[1]).Child).Children[3]).Content = "Number : " + number.ToString();
				sqlCommand = new SqlCommand("update Food_Menu set Number_Food =@Number_Food where Name_Food=@Name_Food", sqlConnection);
				sqlCommand.Parameters.AddWithValue("@Number_Food", current_Food_number - 1);
				sqlCommand.Parameters.AddWithValue("@Name_Food", ((Label)((StackPanel)((Border)st2.Children[1]).Child).Children[0]).Content.ToString().Substring(7));
				sqlCommand.ExecuteNonQuery();
				sqlCommand.Dispose();
				sqlConnection.Close();
			}

			else
			{
				MessageBox.Show("You Can't Order More Than Amount of Food in Menu");
			}
			user_cart w = new user_cart();
			w.Show();
			window.Close();


		}

		public static void lable_minus_click(object sender, RoutedEventArgs e)
		{
			string connection_string = " Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30";
			Label lb = e.Source as Label;
			StackPanel st = lb.Parent as StackPanel;
			StackPanel st1 = st.Parent as StackPanel;
			StackPanel st2 = st1.Parent as StackPanel;
			StackPanel st3 = st2.Parent as StackPanel;
			Border br = st3.Parent as Border;
			ScrollViewer sc = br.Parent as ScrollViewer;
			Grid grid = sc.Parent as Grid;
			Window window = grid.Parent as Window;

			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select Cart from Users where FullName=@FullName", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@FullName", Current_user.FullName);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			sqlDataReader.Read();
			string cart = sqlDataReader.GetValue(0).ToString();
			string Food_name = ((Label)((StackPanel)((Border)st2.Children[1]).Child).Children[0]).Content.ToString().Substring(7);
			int food_index = cart.IndexOf(Food_name);
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlCommand = new SqlCommand("update Users set Cart=@Cart where FullName=@FullName", sqlConnection);
			int endIndex = cart.Length - Food_name.Length != food_index ? Food_name.Length + 1 : Food_name.Length;
			sqlCommand.Parameters.AddWithValue("@Cart", cart.Remove(food_index, endIndex));
			sqlCommand.Parameters.AddWithValue("@FullName", Current_user.FullName);
			sqlCommand.ExecuteNonQuery();
			sqlCommand.Dispose();
			double number = double.Parse(((Label)((StackPanel)((Border)st2.Children[1]).Child).Children[3]).Content.ToString().Substring(9));
			number--;
			((Label)((StackPanel)((Border)st2.Children[1]).Child).Children[3]).Content = "Number : " + number.ToString();
			sqlCommand = new SqlCommand("select Number_Food from Food_Menu where Name_Food=@Name_Food", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@Name_Food", ((Label)((StackPanel)((Border)st2.Children[1]).Child).Children[0]).Content.ToString().Substring(7));
			sqlDataReader = sqlCommand.ExecuteReader();
			sqlDataReader.Read();
			double current_Food_number = double.Parse(sqlDataReader.GetValue(0).ToString());
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlCommand = new SqlCommand("update Food_Menu set Number_Food =@Number_Food where Name_Food=@Name_Food", sqlConnection);
			sqlCommand.Parameters.AddWithValue("@Number_Food", current_Food_number + 1);
			sqlCommand.Parameters.AddWithValue("@Name_Food", ((Label)((StackPanel)((Border)st2.Children[1]).Child).Children[0]).Content.ToString().Substring(7));
			sqlCommand.ExecuteNonQuery();
			sqlCommand.Dispose();
			sqlConnection.Close();
			if (number == 0)
			{
				st2.Children.Clear();
				st2.Height = 0;
			}
			user_cart w = new user_cart();
			w.Show();
			window.Close();

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
		string connection_string = " Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30";
		public static class Cart
		{
			public static bool signed = false;
			public static StackPanel Total_Price()
			{
				SqlConnection sqlConnection = new SqlConnection(" Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30");
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand("select Name_Food,Cost_Food,Number_Food from Receipt", sqlConnection);
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				List<Food_Item> Food = new List<Food_Item>();
				while (sqlDataReader.Read())
				{
					Food_Item a = new Food_Item();
					a.Name_Food = sqlDataReader.GetValue(0).ToString();
					a.Cost_Food = sqlDataReader.GetValue(1).ToString();
					a.Number_Food = sqlDataReader.GetValue(2).ToString();
					Food.Add(a);
				}
				sqlDataReader.Close();
				sqlCommand.Dispose();
				//order price
				StackPanel stackPanel = new StackPanel();
				Grid grid = new Grid();
				ColumnDefinition c1 = new ColumnDefinition();
				c1.Width = new GridLength(2.5, GridUnitType.Star);
				ColumnDefinition c2 = new ColumnDefinition();
				c2.Width = new GridLength(1, GridUnitType.Star);
				grid.ColumnDefinitions.Add(c1);
				grid.ColumnDefinitions.Add(c2);
				Label lable1 = new Label();
				var bc = new BrushConverter();
				lable1.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFEFDD");
				lable1.Content = "Order Cost";
				lable1.FontWeight = FontWeights.Bold;
				lable1.FontStyle = FontStyles.Italic;
				lable1.FontSize = 16;
				lable1.Margin = new Thickness(0, 0, 10, 0);
				Grid.SetColumn(lable1, 0);
				Label lable2 = new Label();
				bc = new BrushConverter();
				lable2.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFEFDD");
				double total = Food.Select(a => (double.Parse(a.Cost_Food) * double.Parse(a.Number_Food))).Sum(x => x);
				lable2.Content = $"{ total.ToString():N2}$";
				lable2.FontSize = 16;
				lable2.FontWeight = FontWeights.Bold;
				lable2.FontStyle = FontStyles.Italic;
				lable2.Margin = new Thickness(5, 0, 10, 0);
				Grid.SetColumn(lable2, 1);
				grid.Children.Add(lable2);
				grid.Children.Add(lable1);
				grid.Margin = new Thickness(0, 10, 0, 0);
				stackPanel.Children.Add(grid);
				current_order_cost = total;
				//order price

				sqlCommand = new SqlCommand("select Order_Number from Users where FullName=@FullName", sqlConnection);
				sqlCommand.Parameters.AddWithValue("@FullName", Current_user.FullName);
				sqlDataReader = sqlCommand.ExecuteReader();
				sqlDataReader.Read();
				int order_num = int.Parse(sqlDataReader.GetValue(0).ToString());
				sqlDataReader.Close();
				sqlCommand.Dispose();
				sqlConnection.Close();
				StackPanel stackPanel2 = new StackPanel();
				StackPanel stackPanel3 = new StackPanel();
				if (order_num <= 4)
				{
					//tax
					stackPanel2 = new StackPanel();
					Grid grid2 = new Grid();
					ColumnDefinition c21 = new ColumnDefinition();
					c21.Width = new GridLength(2.5, GridUnitType.Star);
					ColumnDefinition c22 = new ColumnDefinition();
					c22.Width = new GridLength(1, GridUnitType.Star);
					grid2.ColumnDefinitions.Add(c21);
					grid2.ColumnDefinitions.Add(c22);
					Label lable21 = new Label();
					var bc2 = new BrushConverter();
					lable21.Foreground = (System.Windows.Media.Brush)bc2.ConvertFrom("#FFFFEFDD");
					lable21.Content = "Tax Cost";
					lable21.FontWeight = FontWeights.Bold;
					lable21.FontStyle = FontStyles.Italic;
					lable21.FontSize = 16;
					lable21.Margin = new Thickness(0, 0, 10, 0);
					Grid.SetColumn(lable21, 0);
					Label lable22 = new Label();
					bc2 = new BrushConverter();
					lable22.Foreground = (System.Windows.Media.Brush)bc2.ConvertFrom("#FFFFEFDD");
					double total2 = Food.Select(a => (double.Parse(a.Cost_Food) * double.Parse(a.Number_Food))).Sum(x => x) * (100.0 / 124.0) * 0.09;
					lable22.Content = $"{ total2:N2}$";
					lable22.FontSize = 16;
					lable22.FontWeight = FontWeights.Bold;
					lable22.FontStyle = FontStyles.Italic;
					lable22.Margin = new Thickness(5, 0, 10, 0);
					Grid.SetColumn(lable22, 1);
					grid2.Children.Add(lable22);
					grid2.Children.Add(lable21);
					grid2.Margin = new Thickness(0, 10, 0, 0);
					stackPanel2.Children.Add(grid2);
					current_order_cost += total2;
					//tax

					//Amazing Offer
					stackPanel3 = new StackPanel();
					Grid grid3 = new Grid();
					ColumnDefinition c31 = new ColumnDefinition();
					c31.Width = new GridLength(2.5, GridUnitType.Star);
					ColumnDefinition c32 = new ColumnDefinition();
					c32.Width = new GridLength(1, GridUnitType.Star);
					grid3.ColumnDefinitions.Add(c31);
					grid3.ColumnDefinitions.Add(c32);
					Label lable31 = new Label();
					var bc3 = new BrushConverter();
					lable31.Foreground = (System.Windows.Media.Brush)bc3.ConvertFrom("#FFFFEFDD");
					lable31.Content = "Amazing Offer";
					lable31.FontWeight = FontWeights.Bold;
					lable31.FontStyle = FontStyles.Italic;
					lable31.FontSize = 16;
					lable31.Margin = new Thickness(0, 0, 10, 0);
					Grid.SetColumn(lable31, 0);
					Label lable32 = new Label();
					bc3 = new BrushConverter();
					lable32.Foreground = (System.Windows.Media.Brush)bc3.ConvertFrom("#FFFFEFDD");
					double total3 = 0;
					lable32.Content = total3.ToString() + "$";
					lable32.FontSize = 16;
					lable32.FontWeight = FontWeights.Bold;
					lable32.FontStyle = FontStyles.Italic;
					lable32.Margin = new Thickness(5, 0, 10, 0);
					Grid.SetColumn(lable32, 1);
					grid3.Children.Add(lable32);
					grid3.Children.Add(lable31);
					grid3.Margin = new Thickness(0, 10, 0, 0);
					stackPanel3.Children.Add(grid3);
					current_order_cost -= total3;
					//Amazing Offer
				}

				else if (order_num >= 5 && order_num <= 8)
				{
					//tax
					stackPanel2 = new StackPanel();
					Grid grid2 = new Grid();
					ColumnDefinition c21 = new ColumnDefinition();
					c21.Width = new GridLength(2.5, GridUnitType.Star);
					ColumnDefinition c22 = new ColumnDefinition();
					c22.Width = new GridLength(1, GridUnitType.Star);
					grid2.ColumnDefinitions.Add(c21);
					grid2.ColumnDefinitions.Add(c22);
					Label lable21 = new Label();
					var bc2 = new BrushConverter();
					lable21.Foreground = (System.Windows.Media.Brush)bc2.ConvertFrom("#FFFFEFDD");
					lable21.Content = "Tax Cost";
					lable21.FontWeight = FontWeights.Bold;
					lable21.FontStyle = FontStyles.Italic;
					lable21.FontSize = 16;
					lable21.Margin = new Thickness(0, 0, 10, 0);
					Grid.SetColumn(lable21, 0);
					Label lable22 = new Label();
					bc2 = new BrushConverter();
					lable22.Foreground = (System.Windows.Media.Brush)bc2.ConvertFrom("#FFFFEFDD");
					double total2 = Food.Select(a => (double.Parse(a.Cost_Food) * double.Parse(a.Number_Food))).Sum(x => x) * (100.0 / 124.0) * 0.07;
					lable22.Content = $"{ total2:N2}$";
					lable22.FontSize = 16;
					lable22.FontWeight = FontWeights.Bold;
					lable22.FontStyle = FontStyles.Italic;
					lable22.Margin = new Thickness(5, 0, 10, 0);
					Grid.SetColumn(lable22, 1);
					grid2.Children.Add(lable22);
					grid2.Children.Add(lable21);
					grid2.Margin = new Thickness(0, 10, 0, 0);
					stackPanel2.Children.Add(grid2);
					current_order_cost += total2;
					//tax

					//Amazing Offer
					stackPanel3 = new StackPanel();
					Grid grid3 = new Grid();
					ColumnDefinition c31 = new ColumnDefinition();
					c31.Width = new GridLength(2.5, GridUnitType.Star);
					ColumnDefinition c32 = new ColumnDefinition();
					c32.Width = new GridLength(1, GridUnitType.Star);
					grid3.ColumnDefinitions.Add(c31);
					grid3.ColumnDefinitions.Add(c32);
					Label lable31 = new Label();
					var bc3 = new BrushConverter();
					lable31.Foreground = (System.Windows.Media.Brush)bc3.ConvertFrom("#FFFFEFDD");
					lable31.Content = "Amazing Offer";
					lable31.FontWeight = FontWeights.Bold;
					lable31.FontStyle = FontStyles.Italic;
					lable31.FontSize = 16;
					lable31.Margin = new Thickness(0, 0, 10, 0);
					Grid.SetColumn(lable31, 0);
					Label lable32 = new Label();
					bc3 = new BrushConverter();
					lable32.Foreground = (System.Windows.Media.Brush)bc3.ConvertFrom("#FFFFEFDD");
					double total3 = Food.Select(a => (double.Parse(a.Cost_Food) * double.Parse(a.Number_Food))).Sum(x => x) * 0.05;
					lable32.Content = $"{ total3:N2}$";
					lable32.FontSize = 16;
					lable32.FontWeight = FontWeights.Bold;
					lable32.FontStyle = FontStyles.Italic;
					lable32.Margin = new Thickness(5, 0, 10, 0);
					Grid.SetColumn(lable32, 1);
					grid3.Children.Add(lable32);
					grid3.Children.Add(lable31);
					grid3.Margin = new Thickness(0, 10, 0, 0);
					stackPanel3.Children.Add(grid3);
					current_order_cost -= total3;
					//Amazing Offer
				}

				else if (order_num >= 9 && order_num <= 12)
				{
					//tax
					stackPanel2 = new StackPanel();
					Grid grid2 = new Grid();
					ColumnDefinition c21 = new ColumnDefinition();
					c21.Width = new GridLength(2.5, GridUnitType.Star);
					ColumnDefinition c22 = new ColumnDefinition();
					c22.Width = new GridLength(1, GridUnitType.Star);
					grid2.ColumnDefinitions.Add(c21);
					grid2.ColumnDefinitions.Add(c22);
					Label lable21 = new Label();
					var bc2 = new BrushConverter();
					lable21.Foreground = (System.Windows.Media.Brush)bc2.ConvertFrom("#FFFFEFDD");
					lable21.Content = "Tax Cost";
					lable21.FontWeight = FontWeights.Bold;
					lable21.FontStyle = FontStyles.Italic;
					lable21.FontSize = 16;
					lable21.Margin = new Thickness(0, 0, 10, 0);
					Grid.SetColumn(lable21, 0);
					Label lable22 = new Label();
					bc2 = new BrushConverter();
					lable22.Foreground = (System.Windows.Media.Brush)bc2.ConvertFrom("#FFFFEFDD");
					double total2 = Food.Select(a => (double.Parse(a.Cost_Food) * double.Parse(a.Number_Food))).Sum(x => x) * (100.0 / 124.0) * 0.05;
					lable22.Content = $"{ total2:N2}$";
					lable22.FontSize = 16;
					lable22.FontWeight = FontWeights.Bold;
					lable22.FontStyle = FontStyles.Italic;
					lable22.Margin = new Thickness(5, 0, 10, 0);
					Grid.SetColumn(lable22, 1);
					grid2.Children.Add(lable22);
					grid2.Children.Add(lable21);
					grid2.Margin = new Thickness(0, 10, 0, 0);
					stackPanel2.Children.Add(grid2);
					current_order_cost += total2;
					//tax

					//Amazing Offer
					stackPanel3 = new StackPanel();
					Grid grid3 = new Grid();
					ColumnDefinition c31 = new ColumnDefinition();
					c31.Width = new GridLength(2.5, GridUnitType.Star);
					ColumnDefinition c32 = new ColumnDefinition();
					c32.Width = new GridLength(1, GridUnitType.Star);
					grid3.ColumnDefinitions.Add(c31);
					grid3.ColumnDefinitions.Add(c32);
					Label lable31 = new Label();
					var bc3 = new BrushConverter();
					lable31.Foreground = (System.Windows.Media.Brush)bc3.ConvertFrom("#FFFFEFDD");
					lable31.Content = "Amazing Offer";
					lable31.FontWeight = FontWeights.Bold;
					lable31.FontStyle = FontStyles.Italic;
					lable31.FontSize = 16;
					lable31.Margin = new Thickness(0, 0, 10, 0);
					Grid.SetColumn(lable31, 0);
					Label lable32 = new Label();
					bc3 = new BrushConverter();
					lable32.Foreground = (System.Windows.Media.Brush)bc3.ConvertFrom("#FFFFEFDD");
					double total3 = Food.Select(a => (double.Parse(a.Cost_Food) * double.Parse(a.Number_Food))).Sum(x => x) * 0.08;
					lable32.Content = $"{ total3:N2}$";
					lable32.FontSize = 16;
					lable32.FontWeight = FontWeights.Bold;
					lable32.FontStyle = FontStyles.Italic;
					lable32.Margin = new Thickness(5, 0, 10, 0);
					Grid.SetColumn(lable32, 1);
					grid3.Children.Add(lable32);
					grid3.Children.Add(lable31);
					grid3.Margin = new Thickness(0, 10, 0, 0);
					stackPanel3.Children.Add(grid3);
					current_order_cost -= total3;
					//Amazing Offer
				}

				else if (order_num >= 13)
				{
					//tax
					stackPanel2 = new StackPanel();
					Grid grid2 = new Grid();
					ColumnDefinition c21 = new ColumnDefinition();
					c21.Width = new GridLength(2.5, GridUnitType.Star);
					ColumnDefinition c22 = new ColumnDefinition();
					c22.Width = new GridLength(1, GridUnitType.Star);
					grid2.ColumnDefinitions.Add(c21);
					grid2.ColumnDefinitions.Add(c22);
					Label lable21 = new Label();
					var bc2 = new BrushConverter();
					lable21.Foreground = (System.Windows.Media.Brush)bc2.ConvertFrom("#FFFFEFDD");
					lable21.Content = "Tax Cost";
					lable21.FontWeight = FontWeights.Bold;
					lable21.FontStyle = FontStyles.Italic;
					lable21.FontSize = 16;
					lable21.Margin = new Thickness(0, 0, 10, 0);
					Grid.SetColumn(lable21, 0);
					Label lable22 = new Label();
					bc2 = new BrushConverter();
					lable22.Foreground = (System.Windows.Media.Brush)bc2.ConvertFrom("#FFFFEFDD");
					double total2 = 0;
					lable22.Content = $"{ total2:N2}$";
					lable22.FontSize = 16;
					lable22.FontWeight = FontWeights.Bold;
					lable22.FontStyle = FontStyles.Italic;
					lable22.Margin = new Thickness(5, 0, 10, 0);
					Grid.SetColumn(lable22, 1);
					grid2.Children.Add(lable22);
					grid2.Children.Add(lable21);
					grid2.Margin = new Thickness(0, 10, 0, 0);
					stackPanel2.Children.Add(grid2);
					current_order_cost += total2;
					//tax

					//Amazing Offer
					stackPanel3 = new StackPanel();
					Grid grid3 = new Grid();
					ColumnDefinition c31 = new ColumnDefinition();
					c31.Width = new GridLength(2.5, GridUnitType.Star);
					ColumnDefinition c32 = new ColumnDefinition();
					c32.Width = new GridLength(1, GridUnitType.Star);
					grid3.ColumnDefinitions.Add(c31);
					grid3.ColumnDefinitions.Add(c32);
					Label lable31 = new Label();
					var bc3 = new BrushConverter();
					lable31.Foreground = (System.Windows.Media.Brush)bc3.ConvertFrom("#FFFFEFDD");
					lable31.Content = "Amazing Offer";
					lable31.FontWeight = FontWeights.Bold;
					lable31.FontStyle = FontStyles.Italic;
					lable31.FontSize = 16;
					lable31.Margin = new Thickness(0, 0, 10, 0);
					Grid.SetColumn(lable31, 0);
					Label lable32 = new Label();
					bc3 = new BrushConverter();
					lable32.Foreground = (System.Windows.Media.Brush)bc3.ConvertFrom("#FFFFEFDD");
					double total3 = Food.Select(a => (double.Parse(a.Cost_Food) * double.Parse(a.Number_Food))).Sum(x => x) * 0.10;
					lable32.Content = $"{ total3:N2}$";
					lable32.FontSize = 16;
					lable32.FontWeight = FontWeights.Bold;
					lable32.FontStyle = FontStyles.Italic;
					lable32.Margin = new Thickness(5, 0, 10, 0);
					Grid.SetColumn(lable32, 1);
					grid3.Children.Add(lable32);
					grid3.Children.Add(lable31);
					grid3.Margin = new Thickness(0, 10, 0, 0);
					stackPanel3.Children.Add(grid3);
					current_order_cost -= total3;
					//Amazing Offer
				}

				StackPanel stackPanel4 = new StackPanel();
				Grid grid4 = new Grid();
				ColumnDefinition c41 = new ColumnDefinition();
				c41.Width = new GridLength(2.5, GridUnitType.Star);
				ColumnDefinition c42 = new ColumnDefinition();
				c42.Width = new GridLength(1, GridUnitType.Star);
				grid4.ColumnDefinitions.Add(c41);
				grid4.ColumnDefinitions.Add(c42);
				Label lable41 = new Label();
				var bc4 = new BrushConverter();
				lable41.Foreground = (System.Windows.Media.Brush)bc4.ConvertFrom("#FFFFEFDD");
				lable41.Content = "Total Cost";
				lable41.FontWeight = FontWeights.Bold;
				lable41.FontStyle = FontStyles.Italic;
				lable41.FontSize = 16;
				lable41.Margin = new Thickness(0, 0, 10, 0);
				Grid.SetColumn(lable41, 0);
				Label lable42 = new Label();
				bc4 = new BrushConverter();
				lable42.Foreground = (System.Windows.Media.Brush)bc4.ConvertFrom("#FFFFEFDD");
				double total4 = current_order_cost;
				lable42.Content = $"{current_order_cost:N2}$";
				lable42.FontSize = 16;
				lable42.FontWeight = FontWeights.Bold;
				lable42.FontStyle = FontStyles.Italic;
				lable42.Margin = new Thickness(5, 0, 10, 0);
				Grid.SetColumn(lable42, 1);
				grid4.Children.Add(lable42);
				grid4.Children.Add(lable41);
				grid4.Margin = new Thickness(0, 10, 0, 0);
				stackPanel4.Children.Add(grid4);

				StackPanel main = new StackPanel();
				main.Children.Add(stackPanel);
				main.Children.Add(stackPanel2);
				main.Children.Add(stackPanel3);
				main.Children.Add(stackPanel4);
				return main;

			}
			public static StackPanel receipt(string Name_Food, string Cost_Food, List<string> cart)
			{
				StackPanel stackpanel = new StackPanel();
				Label lable1 = new Label();
				var bc = new BrushConverter();
				lable1.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFEFDD");
				lable1.Content = Name_Food;
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
				bc = new BrushConverter();
				lable2.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFEFDD");
				lable2.Content = (double.Parse(Cost_Food) * (int)cart.Where(x => x == Name_Food).Count()).ToString() + "$";
				lable2.FontSize = 15;
				lable2.Margin = new Thickness(5, 0, 10, 0);
				Grid.SetColumn(lable2, 1);
				Label lable3 = new Label();
				bc = new BrushConverter();
				lable3.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFEFDD");
				lable3.Content = ((int)cart.Where(x => x == Name_Food).Count()).ToString();
				lable3.FontSize = 15;
				lable3.Margin = new Thickness(10, 0, 10, 0);
				Grid.SetColumn(lable3, 0);
				grid.Children.Add(lable3);
				grid.Children.Add(lable2);
				Label lable4 = new Label();
				bc = new BrushConverter();
				lable4.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFEFDD");
				lable4.Content = "_______________________________________________________________________________";
				lable4.Margin = new Thickness(0, -7, 0, 0);
				lable4.FontSize = 15;
				lable4.Margin = new Thickness(10, 0, 10, 0);
				stackpanel.Children.Add(lable1);
				stackpanel.Children.Add(grid);
				stackpanel.Children.Add(lable4);
				SqlConnection sqlConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30");
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand(" if not exists (select * from Receipt where Name_Food=@Name_Food) INSERT INTO Receipt (Name_Food, Cost_Food,Number_Food) VALUES (@Name_Food,@Cost_Food,@Number_Food)", sqlConnection);
				sqlCommand.Parameters.AddWithValue("@Name_Food", Name_Food);
				sqlCommand.Parameters.AddWithValue("@Cost_Food", Cost_Food);
				sqlCommand.Parameters.AddWithValue("@Number_Food", (int)cart.Where(x => x == Name_Food).Count());
				sqlCommand.ExecuteNonQuery();
				sqlCommand.Dispose();
				sqlCommand = new SqlCommand("update Receipt set Number_Food=@Number_Food where Name_Food=@Name_Food", sqlConnection);
				sqlCommand.Parameters.AddWithValue("@Name_Food", Name_Food);
				sqlCommand.Parameters.AddWithValue("@Number_Food", (int)cart.Where(x => x == Name_Food).Count());
				sqlCommand.ExecuteNonQuery();
				sqlCommand.Dispose();
				sqlConnection.Close();
				return stackpanel;

			}
			public static StackPanel Construct(string Name_Food, string Cost_Food, string Information_Food, string Date_Food, string Uri_Food, string Kind_Food, string Number_Food, List<string> cart)
			{
				StackPanel stackPanel = new StackPanel();
				stackPanel.Orientation = Orientation.Horizontal;
				stackPanel.Height = 200;
				Border border = new Border();
				border.BorderThickness = new Thickness(2, 2, 2, 2);
				border.CornerRadius = new CornerRadius(20, 20, 20, 20);
				border.Width = 392;
				border.Margin = new Thickness(14, 14, 14, 14);
				var bc = new BrushConverter();
				border.BorderBrush = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFEFDD");
				StackPanel stack = new StackPanel();
				Label lable1 = new Label();
				bc = new BrushConverter();
				lable1.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFEFDD");
				lable1.Content = "Price : ";
				lable1.FontWeight = FontWeights.Bold;
				lable1.FontStyle = FontStyles.Italic;
				lable1.Content += Cost_Food + "$";
				lable1.FontSize = 15;
				lable1.Margin = new Thickness(-18, 0, 0, 0);
				lable1.HorizontalContentAlignment = HorizontalAlignment.Left;
				lable1.Margin = new Thickness(30, 5, 30, 5);
				Label lable2 = new Label();
				bc = new BrushConverter();
				lable2.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFEFDD");
				lable2.Margin = new Thickness(-8, 0, 0, 0);
				lable2.Content = "Ingridient : ";
				lable2.FontWeight = FontWeights.Bold;
				lable2.FontStyle = FontStyles.Italic;
				lable2.Content += Information_Food;
				lable2.FontSize = 15;
				lable2.HorizontalContentAlignment = HorizontalAlignment.Left;
				lable2.Margin = new Thickness(30, 5, 30, 5);
				Label lable3 = new Label();
				bc = new BrushConverter();
				lable3.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFEFDD");
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
				bc = new BrushConverter();
				lable6.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFEFDD");
				lable6.Margin = new Thickness(-18, 0, 0, 0);
				lable6.Content = "Number : ";
				lable6.FontWeight = FontWeights.Bold;
				lable6.FontStyle = FontStyles.Italic;
				lable6.Content += ((int)cart.Where(x => x == Name_Food).Count()).ToString();
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
				bc = new BrushConverter();
				lable4.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFEFDD");
				lable4.Content = "+";
				lable4.MouseDown += new MouseButtonEventHandler(lable_plus_click);
				lable4.FontSize = 25;
				bc = new BrushConverter();
				lable4.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFEFDD");
				lable4.Margin = new Thickness(15, -10, 0, 0);
				Label lable5 = new Label();
				bc = new BrushConverter();
				lable5.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFEFDD");
				lable5.Content = "-";
				lable5.MouseDown += new MouseButtonEventHandler(lable_minus_click);
				lable5.FontSize = 30;
				bc = new BrushConverter();
				lable5.Foreground = (System.Windows.Media.Brush)bc.ConvertFrom("#FFFFEFDD");
				lable5.Margin = new Thickness(0, -10, 5, 0);
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
			Cart.signed = false;
			bool receipt_empty = true;
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("delete from Receipt", sqlConnection);
			sqlCommand.ExecuteNonQuery();
			sqlCommand.Dispose();
			sqlCommand = new SqlCommand("select Cart from Users where FullName=@FullName", sqlConnection);
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
				receipt_empty = false;
				main_stack.Children.Add(Cart.Construct(item.Name_Food, item.Cost_Food, item.Information_Food, item.Date_Food, item.Uri_Food, item.Kind_Food, item.Number_Food, cart));
				receipt.Children.Add(Cart.receipt(item.Name_Food, item.Cost_Food, cart));
			}
			if (!receipt_empty)
			{
				Customer_Name_cart.Content = "Customer : " + Current_user.FullName;
				receipt.Children.Add(Cart.Total_Price());
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

		private void Submit_Click(object sender, RoutedEventArgs e)
		{
			if (Cart.signed && (online_pay.IsChecked == true || cash_pay.IsChecked == true) && receipt.Children.Count != 0)
			{
				MessageBox.Show("order is submited");
				if (cash_pay.IsChecked == true)
				{
					MessageBox.Show("Thank you for your Buy your order will be delivered soon");
					PrintDialog printDialog = new PrintDialog();
					if (printDialog.ShowDialog() == true)
					{


						printDialog.PrintVisual(receipt, "Stack panel description");



					}

				}
				if (online_pay.IsChecked == true)
				{
					string total_price = current_order_cost.ToString();
					MessageBox.Show($"{total_price:N2}  was deducted from your account");
					MessageBox.Show("Thank you for your Buy your order will be delivered soon");
					PrintDialog printDialog = new PrintDialog();
					if (printDialog.ShowDialog() == true)
					{
						//StackPanel st = new StackPanel();
						//foreach (UIElement item in receipt.Children)
						//{
						//	st.Children.Add(item);
						//}
						//st.Children.Add(discount);
						//st.Children.Add(signiture);
						printDialog.PrintVisual(factor, "Stack panel description");

					}
				}
				SqlConnection sqlConnection = new SqlConnection(" Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30");
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand("update Users set Order_Number=Order_Number+1 where FullName=@FullName", sqlConnection);
				sqlCommand.Parameters.AddWithValue("@FullName", Current_user.FullName);
				sqlCommand.ExecuteNonQuery();
				sqlCommand.Dispose();
				sqlCommand = new SqlCommand("select Order_Number from Users where FullName=@FullName", sqlConnection);
				sqlCommand.Parameters.AddWithValue("@FullName", Current_user.FullName);
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				sqlDataReader.Read();
				double order_num = double.Parse(sqlDataReader.GetValue(0).ToString());
				sqlDataReader.Close();
				sqlCommand.Dispose();
				sqlCommand = new SqlCommand("select * from Receipt", sqlConnection);
				sqlDataReader = sqlCommand.ExecuteReader();
				List<Food_Item> order = new List<Food_Item>();
				string Name_Food = "";
				string Cost_Food = "";
				string Number_Food = "";
				while (sqlDataReader.Read())
				{
					Food_Item a = new Food_Item();
					a.Name_Food = sqlDataReader.GetValue(0).ToString();
					a.Cost_Food = sqlDataReader.GetValue(1).ToString();
					a.Number_Food = sqlDataReader.GetValue(2).ToString();
					order.Add(a);
				}
				foreach (var item in order)
				{
					Name_Food += item.Name_Food + ",";
					Cost_Food += item.Cost_Food + ",";
					Number_Food += item.Number_Food + ",";

				}
				Name_Food = Name_Food.Substring(0, Name_Food.Length - 1);
				Cost_Food = Cost_Food.Substring(0, Cost_Food.Length - 1);
				Number_Food = Number_Food.Substring(0, Number_Food.Length - 1);
				sqlDataReader.Close();
				sqlCommand.Dispose();
				sqlCommand = new SqlCommand("update Users set Orders+=@Orders where FullName=@FullName ", sqlConnection);
				sqlCommand.Parameters.AddWithValue("@FullName", Current_user.FullName);
				sqlCommand.Parameters.AddWithValue("@Orders", order_num.ToString() + ":" + Name_Food + ":" + Cost_Food + ":" + Number_Food + "/");
				sqlCommand.ExecuteNonQuery();
				sqlCommand.Dispose();
				sqlCommand = new SqlCommand("update Users set Cart=@Cart where FullName=@FullName ", sqlConnection);
				sqlCommand.Parameters.AddWithValue("@FullName", Current_user.FullName);
				sqlCommand.Parameters.AddWithValue("@Cart", ",");
				sqlCommand.ExecuteNonQuery();
				sqlConnection.Close();
				receipt.Children.Clear();
				main_stack.Children.Clear();
				signiture.Strokes.Clear();
				online_pay.IsChecked = cash_pay.IsChecked = false;

			}

			else if (online_pay.IsChecked == false && cash_pay.IsChecked == false)
				MessageBox.Show("you have choose payment way");
			else if (receipt.Children.Count == 0)
				MessageBox.Show("Nothing to Buy");
			else
				MessageBox.Show("you have to sign before submit");


		}

		private void signiture_LostMouseCapture(object sender, MouseEventArgs e)
		{
			Cart.signed = true;

		}

		private void online_pay_Checked(object sender, RoutedEventArgs e)
		{

		}

		private void cash_pay_Checked(object sender, RoutedEventArgs e)
		{

		}

		private void Apply_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
