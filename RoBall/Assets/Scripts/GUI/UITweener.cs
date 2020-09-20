using System;
using UnityEngine;

public enum UIAnimationTypes {
	Move, Scale, ScaleX, ScaleY, Fade
}

public class UITweener : MonoBehaviour
{
	public GameObject ObjectToAnimate;

	public UIAnimationTypes AnimationType;
	public LeanTweenType EaseType;
	public float Duration;
	public float Delay;

	public bool Loop;
	public bool PingPong;

	public bool StartPositionOffset;
	public Vector3 From, To;

	private LTDescr _tweenObject;

	public bool ShowOnEnable;
	public bool WorkOnDisable;

	public void OnEnable() {
		if (ShowOnEnable) {
			Show();
		}
	}

	public void Show() {
		HandleTween();
	}

	public void HandleTween() {
		if (ObjectToAnimate == null) {
			ObjectToAnimate = gameObject;
		}

		switch (AnimationType) {
			case UIAnimationTypes.Fade:
				Fade();
				break;
			case UIAnimationTypes.Move:
				MoveAbsolute();
				break;
			case UIAnimationTypes.Scale:
				Scale();
				break;
			case UIAnimationTypes.ScaleX:
				Scale();
				break;
			case UIAnimationTypes.ScaleY:
				Scale();
				break;
		}

		_tweenObject.setDelay(Delay);
		_tweenObject.setEase(EaseType);

		if (Loop) {
			_tweenObject.loopCount = int.MaxValue;
		}

		if (PingPong) {
			_tweenObject.setLoopPingPong();
		}
	}

	public void Fade() {
		if (gameObject.GetComponent<CanvasGroup>() == null) {
			gameObject.AddComponent<CanvasGroup>();
		}

		if (StartPositionOffset) {
			ObjectToAnimate.GetComponent<CanvasGroup>().alpha = From.x;
		}
		_tweenObject = LeanTween.alphaCanvas(ObjectToAnimate.GetComponent<CanvasGroup>(), To.x, Duration);
	}

	public void MoveAbsolute() {
		ObjectToAnimate.GetComponent<RectTransform>().anchoredPosition = From;

		_tweenObject = LeanTween.move(ObjectToAnimate.GetComponent<RectTransform>(), To, Duration);
	}

	public void Scale() {
		if (StartPositionOffset) {
			ObjectToAnimate.GetComponent<RectTransform>().localScale = From;
		}
		_tweenObject = LeanTween.scale(ObjectToAnimate, To, Duration);
	}

	private void SwapDirection() {
		var temp = From;
		From = To;
		To = temp;
	}

	public void Disable() {
		SwapDirection();

		HandleTween();

		_tweenObject.setOnComplete(()=> {
			SwapDirection();
			gameObject.SetActive(false);
		});
	}

	public void Disable(Action onCompleteAction) {
		SwapDirection();

		HandleTween();

		_tweenObject.setOnComplete(()=> {
			SwapDirection();
			onCompleteAction();
		});
	}

	public void OnDisable() {
		//LeanTween.cancel(gameObject);
	}
}
