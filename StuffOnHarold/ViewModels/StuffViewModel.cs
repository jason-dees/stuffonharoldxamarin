using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using StuffOnHarold.Interfaces;
using StuffOnHarold.Models;
using Xamarin.Forms;

namespace StuffOnHarold.ViewModels {
	public class StuffViewModel : INotifyPropertyChanged {

		private readonly ITalkToServers _serverTalker;

		private List<StuffStruct> _stuffList;

		public event PropertyChangedEventHandler PropertyChanged;

		public List<StuffStruct> StuffList { get { return _stuffList; } }

		public Command FillInStuffListCommand { get; }

		public int TotalWeight {
			get {
				return _stuffList.Sum((arg) => arg.Weight);
			}
		}

		public StuffViewModel() {

			_serverTalker = DependencyService.Get<ITalkToServers>();

			FillInStuffListCommand = new Command(async () => await FillInStuffList());

		}

		async Task FillInStuffList() {
			_stuffList = await _serverTalker.GetStuffList();
			_stuffList = _stuffList.OrderBy(s => s.Name.ToUpper()).ToList();
			PropertyChanged(this, new PropertyChangedEventArgs("StuffList"));
		}
	}
}
