using System;
using StuffOnHarold.CustomRenderers;
using StuffOnHarold.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SwipeImage), typeof(SwipeImageRenderer))]
namespace StuffOnHarold.iOS.Renderers {
	
	public class SwipeImageRenderer : ImageRenderer {

        UISwipeGestureRecognizer swipeRightGestureRecognizer;
		UISwipeGestureRecognizer swipeLeftGestureRecognizer;

		nfloat _startX = 0;

		public SwipeImageRenderer() {
		}


		protected override void OnElementChanged(ElementChangedEventArgs<Image> e) {
			base.OnElementChanged(e);


			if (e.OldElement != null) {
				this.RemoveGestureRecognizer(swipeRightGestureRecognizer);
				this.RemoveGestureRecognizer(swipeLeftGestureRecognizer);
			}

            if (e.NewElement != null) {
				
				var swipeImage = e.NewElement as SwipeImage;

				swipeRightGestureRecognizer = new UISwipeGestureRecognizer((swipe) => RotateImage(swipe, swipeImage));
				swipeLeftGestureRecognizer = new UISwipeGestureRecognizer((swipe) => RotateImage(swipe, swipeImage));
				swipeLeftGestureRecognizer.Direction = UISwipeGestureRecognizerDirection.Left;

				this.AddGestureRecognizer(swipeRightGestureRecognizer);
				this.AddGestureRecognizer(swipeLeftGestureRecognizer);

			}
		}

		private void RotateImage(UISwipeGestureRecognizer swipe, SwipeImage image){
			var view = swipe.View;
			nfloat x = 0;
			if(swipe.Direction == UISwipeGestureRecognizerDirection.Right){
				x = 100;
			}
			else{
				x = -100;
			}
			//image.TranslateTo(x, 0, 1000);
			image.SwipeEventTriggered(); 
		}
	}
}
