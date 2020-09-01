using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {
	public enum Audio {
		MainMenuMusic, InGameMusic, CubeCollect, LevelWin, LevelLose, Click
	}

	private static Dictionary<Audio, float> _audioTimerDictionary;
	private static GameObject _oneShotGameObject;
	private static GameObject _musicPlayer;
	private static AudioSource _oneShotAudioSource;
	private static AudioSource _musicPlayerAudioSource;
	private static float _pitch = 1f;
	private static float _randomPitch = 1f;
	
	public static void Initialize() {
		_audioTimerDictionary = new Dictionary<Audio, float>();
	}

	/// <summary> Plays a music repeatedly </summary>
	public static void PlayMusic(Audio audio) {
		if (_musicPlayer == null) {
			_musicPlayer = new GameObject("Music Player");
			_musicPlayerAudioSource = _musicPlayer.AddComponent<AudioSource>();
			ToggleMusic(PlayerPrefManager.GetMusicOn());
		}
		
		_musicPlayerAudioSource.loop = true;
		_musicPlayerAudioSource.volume = GameConfigData.Instance.Audios[(int)audio].volume;
		_musicPlayerAudioSource.clip = GameConfigData.Instance.Audios[(int)audio].audioClip;
		_musicPlayerAudioSource.Play(0);
	}

	/// <summary> Plays a general sound </summary>
	public static void PlaySound(Audio audio) {
		if (CanPlayAudio(audio)) {
			if (_oneShotGameObject == null) {
				_oneShotGameObject = new GameObject("One Shot Sound");
				_oneShotAudioSource = _oneShotGameObject.AddComponent<AudioSource>();
				ToggleSound(PlayerPrefManager.GetSoundOn());
			}
			_oneShotAudioSource.volume = GameConfigData.Instance.Audios[(int)audio].volume;
			_oneShotAudioSource.PlayOneShot(GameConfigData.Instance.Audios[(int)audio].audioClip);
		}
	}

	/// <summary> Plays a general audio with delay </summary>
	public static IEnumerator PlaySoundWithDelay(Audio audio, float delay) {
		yield return new WaitForSeconds(delay);
		PlaySound(audio);
	}

	/// <summary> Plays an audio at an exact position </summary>
	public static void PlaySound(Audio audio, Vector3 position) {
		if (CanPlayAudio(audio)) {
			GameObject soundGameObject = new GameObject("Audio");
			soundGameObject.transform.position = position;
			AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
			audioSource.clip = GameConfigData.Instance.Audios[(int)audio].audioClip;
			
			RandomizeSound(ref audioSource, audio);
			audioSource.Play();

			Object.Destroy(soundGameObject, audioSource.clip.length);       // destroy the sound after playing
		}
	}

	private static bool CanPlayAudio(Audio audio) {
		if (_audioTimerDictionary.ContainsKey(audio)) {
			return CheckLastTimePlayed(audio, GameConfigData.Instance.Audios[(int)audio].audioClip.length);
		}
		else {
			return true;
		}
	}

	private static bool CheckLastTimePlayed(Audio audio, float timerMax) {
		float lastTimePlayed = _audioTimerDictionary[audio];
		if (lastTimePlayed + timerMax < Time.time) {
			_audioTimerDictionary[audio] = Time.time;
			return true;
		}
		else {
			return false;
		}
	}

	private static void RandomizeSound(ref AudioSource audioSource, Audio audio) {
		// Sound settings
		audioSource.maxDistance = 4f;
		audioSource.spatialBlend = 1f;
		audioSource.rolloffMode = AudioRolloffMode.Linear;
		audioSource.dopplerLevel = 0f;

		// add some randomness to the sound in order to prevent repetitiveness
		float volume = GameConfigData.Instance.Audios[(int)audio].volume;
		float randomVolume = volume;
		audioSource.volume = volume * (1 + Random.Range(-randomVolume / 3f, randomVolume / 3f));
		audioSource.pitch = _pitch * (1 + Random.Range(-_randomPitch / 3f, _randomPitch / 3f));
	}

	public static void ToggleMusic(bool turnedOn) {
		_musicPlayerAudioSource.mute = !turnedOn;
		PlayerPrefManager.SetMusicOn(turnedOn);
	}

	public static void ToggleSound(bool turnedOn) {
		_oneShotAudioSource.mute = !turnedOn;
		PlayerPrefManager.SetSoundOn(turnedOn);
	}
}