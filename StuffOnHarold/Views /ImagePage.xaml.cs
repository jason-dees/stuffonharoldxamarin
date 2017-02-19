using System;
using System.Collections.Generic;
using StuffOnHarold.Interfaces;
using Xamarin.Forms;

namespace StuffOnHarold.Views {
	
	public partial class ImagePage : ContentPage {

		private List<string> _haroldImageSources = new List<string>();

		private readonly IHelpWithFiles _fileHelper;

		public ImagePage(List<string> haroldImages) {
			InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);
			_haroldImageSources = haroldImages;

			HaroldImage.SwipeEvent += HandleSwipeEvent;

			_fileHelper = DependencyService.Get<IHelpWithFiles>();
			var src = NextImage().Result;
			HaroldImage.Source = src;
		}

		private async System.Threading.Tasks.Task<ImageSource> NextImage() {

			var index = (new Random()).Next(_haroldImageSources.Count - 1);
			var fileName = _haroldImageSources[index];

			var unslashedName = new List<string>(fileName.Split('/')).FindLast(o => o is string);

			if (!_fileHelper.DoesFileExist(unslashedName)) {
				var uri = $"http://stuffonharold.jhdees.com/{fileName}";
				var imageBytes = await DependencyService.Get<ITalkToServers>().DownloadImage(uri);
				_fileHelper.SaveFile(unslashedName, imageBytes);
			}

			return ImageSource.FromFile(_fileHelper.GetFullPath(unslashedName));
		}

		private async void HandleSwipeEvent(object sender, EventArgs e) {
			var nextPage = new ImagePage(_haroldImageSources);

			Navigation.InsertPageBefore(nextPage, this);

			await Navigation.PopAsync();
		}
	}
}
