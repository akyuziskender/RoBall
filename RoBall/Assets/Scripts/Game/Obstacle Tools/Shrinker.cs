using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrinker : MonoBehaviour
{
	public enum ShrinkDirection { Horizontal, Vertical };
	public ShrinkDirection Direction;
	private Vector3 _targetScale = new Vector3(1f, 1f, 1f);
	[SerializeField] private float _scaleDuration = 1f;
	private Vector3 _initialScale;
	private bool _shrinked = true;
	private bool _scaling = false;

	private void Start() {
		_initialScale = transform.localScale;

		switch (Direction) {
			case ShrinkDirection.Horizontal:
				_targetScale = new Vector3(0.01f, transform.localScale.y, transform.localScale.z);
				break;
			case ShrinkDirection.Vertical:
				_targetScale = new Vector3(transform.localScale.x, 0.01f, transform.localScale.z);
				break;
		}
	}

	private void FixedUpdate() {
		if (!_scaling) {
			if (!_shrinked)
				ScaleToTarget(_targetScale, _scaleDuration);
			else
				ScaleToTarget(_initialScale, _scaleDuration);
		}
	}
 
	public void ScaleToTarget(Vector3 targetScale, float duration) {
		_scaling = true;
		StartCoroutine(ScaleToTargetCoroutine(targetScale, duration));
	}
 
	private IEnumerator ScaleToTargetCoroutine(Vector3 targetScale, float duration) {
		Vector3 startScale = transform.localScale;
		float timer = 0.0f;
 
		while(timer < duration) {
			timer += Time.deltaTime;
			float t = timer / duration;
			//smoother step algorithm
			t = t * t * t * (t * (6f * t - 15f) + 10f);
			transform.localScale = Vector3.Lerp(startScale, targetScale, t);
			yield return null;
		}
		_scaling = false;
		_shrinked = Vector3.Distance(transform.localScale, _targetScale) == 0;
		yield return null;
	}	
}
