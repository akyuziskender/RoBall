using UnityEngine;

namespace Game
{
	/// <summary>Manages data for persistance between levels.</summary>
	public class DataManager : MonoBehaviour 
	{
		/// <summary>Static reference to the instance of our DataManager</summary>
		private static DataManager _instance;
		[SerializeField] private int _selectedLevel = 0;
		public bool OpenLevelSelect = false;

		public static DataManager Instance { get { return _instance; } }
		public int SelectedLevel {
			get { return _selectedLevel; }
			set { _selectedLevel = value; }
		}

		/// <summary>Awake is called when the script instance is being loaded.</summary>
		private void Awake() {
			// If the instance reference has not been set, yet, 
			if (_instance == null) {
				// Set this instance as the instance reference.
				_instance = this;
			}
			else if (_instance != this) {
				// If the instance reference has already been set, and this is not the
				// the instance reference, destroy this game object.
				Destroy(gameObject);
			}
			// Do not destroy this object, when we load a new scene.
			DontDestroyOnLoad(gameObject);
		}
	}
}