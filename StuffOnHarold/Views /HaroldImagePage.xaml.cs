using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StuffOnHarold.CustomRenderers;
using StuffOnHarold.Interfaces;
using Xamarin.Forms;

namespace StuffOnHarold.Views {
	
	public partial class HaroldImagePage : ContentPage {

		List<string> _haroldImageSources = new List<string>();

		readonly IHelpWithFiles _fileHelper;

		SwipeImage _currentHarold => HaroldGrid.Children.LastOrDefault() as SwipeImage;
		SwipeImage _nextHarold => HaroldGrid.Children.FirstOrDefault() as SwipeImage;

		string _nextFileName {
			get {
				var index = (new Random()).Next(_haroldImageSources.Count - 1);

				return _haroldImageSources[index];
			}
		}

		public HaroldImagePage(List<string> haroldImages) {
			InitializeComponent();

			NavigationPage.SetHasNavigationBar(this, false);
			_haroldImageSources = haroldImages;

			_fileHelper = DependencyService.Get<IHelpWithFiles>();

		}

		protected override void OnAppearing(){
			(Content as Grid).RowDefinitions.First().Height = new GridLength(this.Content.Height * 1);

			HaroldGrid.GestureRecognizers.Add(SetUpTap());

		}

		public async Task SetNextImage() {
			HaroldGrid.Children.Add(new SwipeImage() {
				Source = await NextImage(),
				Aspect = Aspect.AspectFill
			}, 0, 0);

			HaroldGrid.Children.Add(new SwipeImage() {
				Source = await NextImage(),
				Aspect = Aspect.AspectFill
			}, 0, 0);

			_currentHarold.SwipeEvent += HandleSwipeEvent;
		}

		public async Task ShowStats() {
			await Navigation.PushModalAsync(new StuffPage());
		}

		async Task AddNextHarold() {
			var nextHarold = new SwipeImage(){
				Aspect = Aspect.AspectFill
			};

			if (_nextHarold != null) {
				while (((nextHarold.Source = await NextImage()) as FileImageSource).File == (_nextHarold.Source as FileImageSource).File) {
				}
			}
			else{
				nextHarold.Source = await NextImage();
			}

			HaroldGrid.Children.Insert(0, nextHarold);
		}

		async Task<ImageSource> NextImage() {
			var fileName = _nextFileName;

			var unslashedName = new List<string>(fileName.Split('/')).FindLast(o => o is string);

			if (!_fileHelper.DoesFileExist(unslashedName)) {
				var uri = $"http://stuffonharold.jhdees.com/{fileName}";
				var imageBytes = await DependencyService.Get<ITalkToServers>().DownloadImage(uri);
				_fileHelper.SaveFile(unslashedName, imageBytes);
			}

			return ImageSource.FromFile(_fileHelper.GetFullPath(unslashedName));
		}

		async void HandleSwipeEvent(object sender, EventArgs e) {

			await AddNextHarold();

			var direction = _currentHarold.IsRightSwipe ? 1 : -1;
			await Task.WhenAll(
				_currentHarold.RotateTo(15 * direction, 100, Easing.Linear),
				_currentHarold.TranslateTo(Width * direction * 1.1, Height * .1, 300, Easing.Linear)
			);

			HaroldGrid.Children.Remove(_currentHarold);

			_currentHarold.SwipeEvent += HandleSwipeEvent;

		}

		TapGestureRecognizer SetUpTap(){
			return new TapGestureRecognizer() {
				Command = new Command(async () => await ShowStats())
			};
		}
	}
}
