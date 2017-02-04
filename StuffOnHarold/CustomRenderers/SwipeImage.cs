using System;
using Xamarin.Forms;

namespace StuffOnHarold.CustomRenderers {
	
	public class SwipeImage : Image {

		public delegate void SwipeEventHandler(object sender, EventArgs args);
		public event EventHandler SwipeEvent = delegate { };

		public SwipeImage() {
		}

		public void SwipeEventTriggered(){
			SwipeEvent(this, null);
		}
	}

}
