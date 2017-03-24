using System.Collections.Generic;
using System.Threading.Tasks;
using StuffOnHarold.Interfaces;
using StuffOnHarold.Views;
using Xamarin.Forms;

namespace StuffOnHarold.ViewModels {
	public class InitialViewModel {

		private List<string> _haroldImagesList;

		private readonly ITalkToServers _serverTalker;

		public List<string> HaroldImageList => _haroldImagesList;

		public Command FillInHaroldListCommand { get; }

		public InitialViewModel() {
			_serverTalker = DependencyService.Get<ITalkToServers>();

			FillInHaroldListCommand = new Command(async () => await FillInHaroldList());
		}


		async Task FillInHaroldList(){
			_haroldImagesList = await _serverTalker.GetImageList();
			var firstHarold = new HaroldImagePage(HaroldImageList);
			await firstHarold.SetNextImage();

			await Application.Current.MainPage.Navigation.PushAsync(firstHarold);
		}

	}
}
