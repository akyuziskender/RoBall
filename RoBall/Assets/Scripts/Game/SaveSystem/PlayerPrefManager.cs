using UnityEngine;

namespace Game
{
	public static class PlayerPrefManager {

		public static bool GetMusicOn() {
			if (PlayerPrefs.HasKey("MusicOn")) {
				return PlayerPrefs.GetInt("MusicOn") == 1;
			} else {
				return true;
			}
		}

		public static void SetMusicOn(bool isMusicOn) {
			PlayerPrefs.SetInt("MusicOn", isMusicOn ? 1 : 0);
		}

		public static bool GetSoundOn() {
			if (PlayerPrefs.HasKey("SoundOn")) {
				return PlayerPrefs.GetInt("SoundOn") == 1;
			} else {
				return true;
			}
		}

		public static void SetSoundOn(bool isSoundOn) {
			PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);
		}

		public static bool GetTutorialSeen() {
			if (PlayerPrefs.HasKey("TutorialSeen")) {
				return PlayerPrefs.GetInt("TutorialSeen") == 1;
			} else {
				return false;
			}
		}

		public static void SetTutorialSeen(bool tutorialSeen) {
			PlayerPrefs.SetInt("TutorialSeen", tutorialSeen ? 1 : 0);
		}

		public static void SaveGameSettings(bool isMusicOn, bool isSoundOn) {
			PlayerPrefs.SetInt("MusicOn", isMusicOn ? 1 : 0);
			PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);
		}
		
		public static void ResetGameSettings() {
			PlayerPrefs.SetInt("MusicOn", 1);
			PlayerPrefs.SetInt("SoundOn", 1);
			PlayerPrefs.SetInt("TutorialSeen", 0);
		}

		// output the defined Player Prefs to the console
		public static void ShowPlayerPrefs() {
			// store the PlayerPref keys to output to the console
			string[] values = {"MusicOn", "SoundOn"};

			// loop over the values and output to the console
			foreach(string value in values) {
				if (PlayerPrefs.HasKey(value)) {
					Debug.Log (value + " = " + PlayerPrefs.GetInt(value));
				} else {
					Debug.Log (value + " is not set.");
				}
			}
		}
	}
}