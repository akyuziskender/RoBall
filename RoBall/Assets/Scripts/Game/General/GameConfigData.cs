using UnityEngine;

public class GameConfigData : MonoBehaviour {
	private static GameConfigData _instance;
	public static GameConfigData Instance {
		get {
			if (_instance == null)
				_instance = (Instantiate(Resources.Load("GameConfigData")) as GameObject).GetComponent<GameConfigData>();
			return _instance;
		}
	}

	public enum Scene {
		MainMenu, Game
	}
	
	public int NumOfLevels;
	public GameObject[] Levels;
	public GameObject LevelButton;

	public SoundAudioClip[] Audios;

	[System.Serializable]
	public class SoundAudioClip {
		public SoundManager.Audio audio;
		public AudioClip audioClip;
		[Range(0f, 1f)]
		public float volume = 1f;
	}
}
