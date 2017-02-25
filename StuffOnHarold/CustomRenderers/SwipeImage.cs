using System;
using Xamarin.Forms;

namespace StuffOnHarold.CustomRenderers {
	
	public class SwipeImage : Image {

		public delegate void SwipeEventHandler(object sender, EventArgs args);
		public event EventHandler SwipeEvent = delegate { };

		public bool IsRightSwipe { get; set; }

		public SwipeImage() {
		}

		public void SwipeEventTriggered(bool isRightSwipe){
			IsRightSwipe = isRightSwipe;
			SwipeEvent(this, null);
		}
	}

}
