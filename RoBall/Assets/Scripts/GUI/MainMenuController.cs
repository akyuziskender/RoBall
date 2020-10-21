using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	public GameObject MenuPanel;
	public GameObject GameModeOptions;
	public GameObject SettingsPanel;
	public GameObject LevelSelectPanel;

	private void Start() {
		SoundManager.Initialize();
		SoundManager.PlayMusic(SoundManager.Audio.MainMenuMusic);

		if (DataManager.Instance.OpenLevelSelect) {
			DataManager.Instance.OpenLevelSelect = false;
			OpenLevelSelectPanel();
		}
	}

	public void DisplayGameModeOptions() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		GameModeOptions.SetActive(true);
	}

	public void HideGameModeOptions() {
		GameModeOptions.SetActive(false);
	}

	public void OpenLevelSelectPanel() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		HideGameModeOptions();
		MenuPanel.SetActive(false);
		LevelSelectPanel.SetActive(true);
	}

	public void OpenSettingsPanel() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		HideGameModeOptions();
		SettingsPanel.SetActive(true);
	}

	public void ReturnToMenuPanel() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		LevelSelectPanel.SetActive(false);
		MenuPanel.SetActive(true);
	}
}
