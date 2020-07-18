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

namespace Final_Project_again
{
	/// <summary>
	/// Interaction logic for Food_information.xaml
	/// </summary>
	public partial class Food_information : Window
	{
		public static string Name_Food;
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

		}

		private void Exit_btn_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
