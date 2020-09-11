using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
	public GameManager GameManager;
	private float _screenCenterX;
	private float _slowdownZoneOffset;
	private float _slowdownZoneStart, _slowdownZoneEnd;
	private int _horizontalMovement = 0;
	[SerializeField] private float _horizontalSpeed = 0f;
	[SerializeField] private float _verticalSpeed = 0f;
	[SerializeField] private float _verticalSlowDownSpeed = 0f;
	private float _verticalSpeedStore;
	private Rigidbody _rb;

	private int _cubeCount = 0;
	private bool _isFalling = false;
	private bool _reachedEnd = false;

	public int CubeCount {
		get { return _cubeCount; }
	}

	public bool IsFalling {
		get { return _isFalling; }
	}

	public bool ReachedEnd {
		get { return _reachedEnd; }
	}

	private void Start() {
		_screenCenterX = Screen.width * 0.5f;	// save the horizontal center of the screen
		_slowdownZoneOffset = Screen.width / 6;

		_slowdownZoneStart = _screenCenterX - _slowdownZoneOffset;
		_slowdownZoneEnd = _screenCenterX + _slowdownZoneOffset;

		_rb = gameObject.GetComponent<Rigidbody>();

		_verticalSpeedStore = _verticalSpeed;		// storing the value of vertical speed
	}

	// Update is called once per frame
	private void Update() {
		if (!GameManager.IsGamePaused)
			GetInputs();
	}

	private void FixedUpdate() {
		if (_rb != null) {
			Vector3 movement = new Vector3(_horizontalMovement * _horizontalSpeed * Time.deltaTime, 0f, _verticalSpeed * Time.deltaTime);

			_rb.AddForce(movement, ForceMode.Force);
		}
	}

	private void GetInputs() {
		if (Input.touchCount == 0) {
			_verticalSpeed = _verticalSpeedStore;
			_horizontalMovement = 0;
			return;
		}

		for (int tapCount = 0; tapCount < Input.touchCount; tapCount++) {
			Touch touch = Input.GetTouch(tapCount);

			if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) {
				// you touched at least one UI element
				continue;
			}
 
			if (touch.phase == TouchPhase.Began) {
				if (touch.position.x > _slowdownZoneEnd) {		// controlling horizontal movement
					_horizontalMovement = 1;
				}
				else if (touch.position.x < _slowdownZoneStart) {
					_horizontalMovement = -1;
				}
				else if (touch.position.x >= _slowdownZoneStart && touch.position.x <= _slowdownZoneEnd) { 	// controlling vertical movement
					_verticalSpeed = _verticalSlowDownSpeed;
				}
			}
			else if (touch.phase == TouchPhase.Ended) {
				if (touch.position.x > _slowdownZoneEnd || touch.position.x < _slowdownZoneStart) {		// controlling horizontal movement
					_horizontalMovement = 0;
				}
				else if (touch.position.x >= _slowdownZoneStart && touch.position.x <= _slowdownZoneEnd) { 	// controlling vertical movement
					_verticalSpeed = _verticalSpeedStore;
				}
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Collectible")) {
			_cubeCount++;
			SoundManager.PlaySound(SoundManager.Audio.CubeCollect);
			other.gameObject.SetActive(false);
		}
		else if (other.gameObject.CompareTag("DeathZone")) {
			_isFalling = true;
		}
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("FinishPlatform")) {
			_reachedEnd = true;
		}
		else if (other.gameObject.CompareTag("MovingPlatform")) {
			other.gameObject.transform.parent.GetComponent<Mover>().Moving = true;
		}
	}
}
