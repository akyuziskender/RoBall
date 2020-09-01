using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
	public GameObject PopUpWindow;
	public LevelSelectGUI LevelSelectUI;
	public Button MusicButton, SoundButton;
	public Sprite MusicOnSprite, MusicOffSprite;
	public Sprite SoundOnSprite, SoundOffSprite;

	private void Start() {
		bool isMusicOn = PlayerPrefManager.GetMusicOn();
		bool isSoundOn = PlayerPrefManager.GetSoundOn();

		MusicButton.image.sprite = isMusicOn ? MusicOnSprite : MusicOffSprite;
		SoundButton.image.sprite = isSoundOn ? SoundOnSprite : SoundOffSprite;
	}

	public void OpenPopUpWindow() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		PopUpWindow.SetActive(true);
	}

	public void DeleteData() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		SaveSystem.DeleteData();
		LevelSelectUI.RefreshGUI();		// refreshing level select gui after deleting the data
		PopUpWindow.SetActive(false);
	}

	public void ToggleMusic() {
		bool isMusicOn = PlayerPrefManager.GetMusicOn();
		isMusicOn = !isMusicOn;
		SoundManager.ToggleMusic(isMusicOn);
		MusicButton.image.sprite = isMusicOn ? MusicOnSprite : MusicOffSprite;
	}

	public void ToggleSound() {
		bool isSoundOn = PlayerPrefManager.GetSoundOn();
		isSoundOn = !isSoundOn;
		SoundManager.ToggleSound(isSoundOn);
		SoundButton.image.sprite = isSoundOn ? SoundOnSprite : SoundOffSprite;
	}

	public void ReturnToSettingsMenu() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		PopUpWindow.SetActive(false);
	}

	public void CloseSettingsMenu() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		gameObject.SetActive(false);
	}
}
