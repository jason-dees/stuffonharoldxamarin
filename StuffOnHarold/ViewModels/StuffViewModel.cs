using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using StuffOnHarold.Interfaces;
using StuffOnHarold.Models;
using Xamarin.Forms;

namespace StuffOnHarold.ViewModels {
	public class StuffViewModel : INotifyPropertyChanged {

		readonly ITalkToServers _serverTalker;

		List<Stuff> _stuffList;

		List<StuffGroup> _stuffGroups;

		public event PropertyChangedEventHandler PropertyChanged;

		public List<StuffGroup> StuffGroups { get { return _stuffGroups; } }

		public Command FillInStuffListCommand { get; }

		int HaroldsWeight = 4581;

		public StuffViewModel() {

			_serverTalker = DependencyService.Get<ITalkToServers>();

			FillInStuffListCommand = new Command(async () => await FillInStuffList());

		}

		async Task FillInStuffList() {

			_stuffList = (await _serverTalker.GetStuffList()).OrderBy(s => s.Name.ToUpper()).ToList();

			var firstGroup = new StuffGroup("Overview") {
				new Stuff{
					Name = "Harold's Weight",
					Weight = HaroldsWeight
				},
				new Stuff{
					Name = "Stuff's Total Weight",
					Weight = _stuffList.Sum((arg) => arg.Weight)
				}
			};

			var secondGroup = new StuffGroup("All the Stuff", _stuffList);

			_stuffGroups = new List<StuffGroup>{
				firstGroup,
				secondGroup
			};

			PropertyChanged(this, new PropertyChangedEventArgs("StuffGroups"));
		}
	}

	public class StuffGroup : List<Stuff>{
		public string Title { get; set; }

		public StuffGroup(string title, List<Stuff> stuff) : base(stuff) {
			Title = title;
		}
		public StuffGroup(string title){
			Title = title;
		}
	}
}
