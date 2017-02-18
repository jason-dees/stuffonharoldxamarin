using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StuffOnHarold.CustomRenderers;
using StuffOnHarold.Implementations;
using Xamarin.Forms;

namespace StuffOnHarold.Views {
	
	public partial class StuffOnHaroldPage : ContentPage {

		private List<string> _haroldImageSources = new List<string>{ };

		public StuffOnHaroldPage(List<string> haroldImages) {
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			_haroldImageSources = haroldImages;
			HaroldImage.Source = NextImage();
			HaroldImage.SwipeEvent += HandleSwipeEvent;
		}

		private string NextImage(){
			var index = (new Random()).Next(_haroldImageSources.Count - 1);
			return $"http://stuffonharold.jhdees.com/{_haroldImageSources[index]}";
		}

		private async void HandleSwipeEvent(object sender, EventArgs e)
		{
			HaroldImage.Source = NextImage();
			var nextPage = new StuffOnHaroldPage(_haroldImageSources);
			Navigation.InsertPageBefore(nextPage, this);
			await Navigation.PopAsync();
		}
	}
}
