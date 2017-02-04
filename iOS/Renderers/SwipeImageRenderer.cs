using System;
using StuffOnHarold.CustomRenderers;
using StuffOnHarold.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SwipeImage), typeof(SwipeImageRenderer))]
namespace StuffOnHarold.iOS.Renderers {
	
	public class SwipeImageRenderer : ImageRenderer {

        UISwipeGestureRecognizer swipeGestureRecognizer;

		public SwipeImageRenderer() {
		}


		protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged(e);


			if (e.NewElement == null) {
				this.RemoveGestureRecognizer(swipeGestureRecognizer);
			}

            if (e.OldElement == null) {
				var swipeImage = e.NewElement as SwipeImage;

				swipeGestureRecognizer = new UISwipeGestureRecognizer(swipeImage.SwipeEventTriggered);

				this.AddGestureRecognizer(swipeGestureRecognizer);
			}
		}
	}
}
