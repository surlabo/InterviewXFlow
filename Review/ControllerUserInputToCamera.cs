using Lime;
using Lime.KineticMotionStrategy;
using Robot.Core.Common.Camera;
using Robot.Core.Common.Utils;
using Robot.Layer1.Common.ActivitiesSystem;
using Robot.Layer2.Common;

namespace Exmaple
{
	public class ControllerUserInputToCamera : IController
	{
		private const float WheelZoomFactor = 1.1f;

		private readonly Widget inputOwner;
		private readonly Camera camera;
		private readonly DragGesture dragGesture;
		private readonly PinchGesture pinchGesture;

		public bool subscribed;
		
		public bool Enabled;
		
		public ControllerUserInputToCamera(Camera camera, Widget inputOwner)
		{
			this.inputOwner = inputOwner;
			this.camera = camera;

			dragGesture = new KineticDragGesture(new DeceleratingKineticMotionStrategy(0.97f, 1.002f));
			pinchGesture = new PinchGesture(exclusive: true);
		}

		void IController.OnStart()
		{
			inputOwner.Gestures.Add(dragGesture);
			inputOwner.Gestures.Add(pinchGesture);
		}

		void IController.OnStop()
		{
			inputOwner.Gestures.Remove(dragGesture);
			inputOwner.Gestures.Remove(pinchGesture);
			if (subscribed)
			{
				subscribed = false;
				UnsubscribeFromGestures();
			}
		}

		void IController.OnUpdate()
		{
			if (Enabled)
			{
				if (!subscribed)
				{
					subscribed = true;
					SubscribeOnGestures();
				}
			}
			else
			{
				if (subscribed)
				{
					subscribed = false;
					UnsubscribeFromGestures();
				}
			}
		}

		private void SubscribeOnGestures()
		{
			dragGesture.Changed += OnDragged;
			pinchGesture.Changed += OnPinched;
		}

		private void UnsubscribeFromGestures()
		{
			dragGesture.Changed -= OnDragged;
			pinchGesture.Changed -= OnPinched;
		}

		private void OnPinched()
		{
			camera.Position -= pinchGesture.LastDragDistance / camera.Zoom;
			float zoom = ClampZoom(camera.Zoom * pinchGesture.LastPinchScale);
			ZoomOrigin(pinchGesture.MousePosition, zoom);
		}

		private void OnDragged()
		{
			camera.Position -= dragGesture.LastDragDistance / camera.Zoom;
		}

		private void ZoomOrigin(in Vector2 origin, float zoom)
		{
			float zoomDelta = zoom / camera.Zoom;
			var originInViewport = origin * camera.CalcGlobalToLocalTransform();
			var offset = originInViewport * (1.0f - 1.0f / zoomDelta);
			camera.Position += offset;
			camera.Zoom = zoom;
		}

		private float ClampZoom(float desiredZoom)
		{
			return desiredZoom.Clamp(settings.MinZoom, settings.MaxZoom);
		}
	}
}
