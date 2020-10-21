using UnityEngine;

namespace Game
{
	public class Rotator : MonoBehaviour
	{
		[SerializeField] private Vector3 _rotationVector = new Vector3(15, 30, 45);
		[SerializeField] private float _rotationSpeed = 1f;

		private void FixedUpdate() {
			transform.Rotate(_rotationVector * Time.deltaTime * _rotationSpeed);
		}
	}
}