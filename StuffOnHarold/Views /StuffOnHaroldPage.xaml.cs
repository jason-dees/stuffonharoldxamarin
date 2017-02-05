using System;
using System.Collections.Generic;
using StuffOnHarold.CustomRenderers;
using Xamarin.Forms;

namespace StuffOnHarold.Views {
	
	public partial class StuffOnHaroldPage : ContentPage {

		private List<string> _haroldImageSources = new List<string>{
			"img/1.jpg",
			"img/2.jpg",
			"img/3.jpg",
			"img/4.jpg",
			"img/5.jpg",
			"img/6.jpg",
			"img/7.jpg",
			"img/8.jpg",
			"img/File Feb 26, 1 55 30 PM.jpeg",
			"img/IMG_0251.JPG","img/IMG_0268.JPG"
		};

		private Animation disappearingAnimation;

		public StuffOnHaroldPage(bool isBase = false) {
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			if (!isBase)
			{
				HaroldImage.Source = NextImage();
				HaroldImage.SwipeEvent += HandleSwipeEvent;
			}
		}


		private string NextImage(){
			var index = (new Random()).Next(_haroldImageSources.Count - 1);
			return $"http://stuffonharold.jhdees.com/{_haroldImageSources[index]}";
		}

		private async void HandleSwipeEvent(object sender, EventArgs e)
		{
			HaroldImage.Source = NextImage();
			var nextPage = new StuffOnHaroldPage();
			Navigation.InsertPageBefore(nextPage, this);
			await Navigation.PopAsync();
		}
	}
}
