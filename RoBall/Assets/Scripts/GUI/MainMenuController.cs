using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	public GameObject MenuPanel;
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

	public void OpenLevelSelectPanel() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		MenuPanel.SetActive(false);
		LevelSelectPanel.SetActive(true);
	}

	public void OpenSettingsPanel() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		SettingsPanel.SetActive(true);
	}

	public void ReturnToMenuPanel() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		LevelSelectPanel.SetActive(false);
		MenuPanel.SetActive(true);
	}
}
