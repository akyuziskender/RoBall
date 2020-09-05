using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
	public GameObject Object; // reference to the platform to move
	public GameObject[] Waypoints; // array of all the waypoints

	[Range(0.0f, 10.0f)] // create a slider in the editor and set limits on moveSpeed
	public float moveSpeed = 5f; // enemy move speed
	public float waitAtWaypointTime = 1f; // how long to wait at a waypoint before _moving to next waypoint

	public bool loop = true; // should it loop through the waypoints
	public bool NeedsTrigger = false;

	// private variables
	private Transform _transform;
	private int _waypointIndex = 0;		// used as index for My_Waypoints
	private float _moveTime;
	private bool _moving = true;

	private void Start () {
		_transform = Object.transform;
		_moveTime = 0f;
		_moving = !NeedsTrigger;
	}
	
	private void FixedUpdate () {
		// if beyond _moveTime, then start moving
		if (Time.time >= _moveTime) {
			Movement();
		}
	}

	private void Movement() {
		// if there isn't anything in My_Waypoints
		if ((Waypoints.Length != 0) && _moving) {

			// move towards waypoint
			_transform.position = Vector3.MoveTowards(_transform.position, Waypoints[_waypointIndex].transform.position, moveSpeed * Time.deltaTime);

			// if the object is close enough to waypoint, make it's new target the next waypoint
			if(Vector3.Distance(Waypoints[_waypointIndex].transform.position, _transform.position) <= 0) {
				_waypointIndex++;
				_moveTime = Time.time + waitAtWaypointTime;
			}
			
			// reset waypoint back to 0 for looping, otherwise flag not moving for not looping
			if(_waypointIndex >= Waypoints.Length) {
				if (loop)
					_waypointIndex = 0;
				else
					_moving = false;
			}
		}
	}
}
