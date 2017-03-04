using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StuffOnHarold.CustomRenderers;
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

			_fileHelper = DependencyService.Get<IHelpWithFiles>();

		}

		protected override void OnAppearing(){
			(Content as Grid).RowDefinitions.First().Height = new GridLength(this.Content.Height * 1);
		}

		public async Task SetNextImage(ImageSource next = null){
			await SetUpImage(next);
		}

		async Task SetUpImage(ImageSource next) {
			HaroldImage.Source = next ?? await NextImage();

			HaroldImage.TranslationX = 0;
			HaroldImage.TranslationY = 0;

			HaroldImage.SwipeEvent += HandleSwipeEvent;

			while (((NextHaroldImage.Source = await NextImage()) as FileImageSource).File == (HaroldImage.Source as FileImageSource).File){
			}
		}

		private string _nextFileName {
			get {
				var index = (new Random()).Next(_haroldImageSources.Count - 1);

				return _haroldImageSources[index];
			}
		}


		private async Task<ImageSource> NextImage() {
			var fileName = _nextFileName;

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

			await nextPage.SetNextImage(NextHaroldImage.Source);

			Navigation.InsertPageBefore(nextPage, this);

			var direction = HaroldImage.IsRightSwipe ? 1 : -1;
			await Task.WhenAll(
				HaroldImage.RotateTo(15 * direction, 100, Easing.Linear),
				HaroldImage.TranslateTo(Width * direction*1.1, Height * .1, 300, Easing.Linear)
			);

			await Navigation.PopAsync(false);
		}
	}
}
