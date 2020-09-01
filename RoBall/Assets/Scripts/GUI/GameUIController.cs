using System.Collections;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
	public GameManager GameManager;
	public GameObject PauseMenuUI;
	public GameObject SettingsPanel;
	public GameObject LevelCompletePanel;
	public GameObject TutorialPanel;
	public LevelGUI LevelUI;
	private int _cubeCount = -1;
	public PlayerController PlayerController;

	private void Update() {
		// Collectibles Count Update
		if (_cubeCount != PlayerController.CubeCount) {
			_cubeCount = PlayerController.CubeCount;
			LevelUI.CollectibleCountText.text = "GOAL: " + _cubeCount + "/" + GameManager.LevelGoal;
		}

		// Level Timer Update
		if (GameManager.TimerCountdown > 0) {
			LevelUI.SetTimerSlider(GameManager.TimerCountdown / GameManager.LevelTime);
			if (GameManager.TimerCountdown <= 0)
				LevelUI.SetTimerSlider(0);
		}
	}

	/// <summary> Pauses/Resumes the game by toggling the current situation </summary>
	public void TogglePauseMenu() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		GameManager.TogglePause();
		PauseMenuUI.SetActive(GameManager.IsGamePaused);		// activate/deactivate the Pause Menu
	}

	public void ResumeGame() {
		TogglePauseMenu();
	}

	public void ReturnToMainMenu() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		GameManager.LoadScene(GameConfigData.Scene.MainMenu);
	}

	public void DisplayTutorialPanel() {
		TutorialPanel.SetActive(true);
	}

	public void CloseTutorialPanel() {
		TutorialPanel.SetActive(false);
		GameManager.TogglePause();
	}

	public void OpenSettingsPanel() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		SettingsPanel.SetActive(true);
	}

	public void ReturnToLevelSelect() {
		DataManager.Instance.OpenLevelSelect = true;
		GameManager.LoadScene(GameConfigData.Scene.MainMenu);
	}

	public void RestartLevel() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		StartCoroutine(GameManager.RestartLevel(0f));
	}

	public void GoToNextLevel() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		StartCoroutine(GameManager.LoadNextLevel(0f));
	}

	public IEnumerator DisplayLevelCompleteScreen(int numOfStars, float delay) {
		yield return new WaitForSeconds(delay);
		LevelCompleteGUI levelCompleteScript = LevelCompletePanel.GetComponent<LevelCompleteGUI>();
		levelCompleteScript.DisplayObjects(numOfStars);
		levelCompleteScript.SetGoalText(PlayerController.CubeCount, GameManager.LevelGoal);
		levelCompleteScript.SetResultMessage(numOfStars > 0 ? "WELL DONE!" : "TRY AGAIN!");
		LevelCompletePanel.SetActive(true);
	}
}
