using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public PlayerController Player;
	public GameUIController GameUIController;
	private bool _isGamePaused = false;
	private bool _levelCompleteScreenDisplayed = false;
	private float _levelCompleteScreenDelay = 1f;
	private int _currentLevel = 0;
	public int LevelGoal, NumOfCubes;
	public float LevelTime, TimerCountdown;
	private LevelData _currLevelData;
	private PlayerData _playerData;

	public bool IsGamePaused {
		get { return _isGamePaused; }
	}

	private void Start() {
		SoundManager.Initialize();
		SoundManager.PlayMusic(SoundManager.Audio.InGameMusic);

		LoadData();
		CreateLevel();

		if (PlayerPrefManager.GetTutorialSeen() == false) {
			DisplayTutorialPanel();
		}
	}

	private void Update() {
		if (!Player.ReachedEnd)
			TimerCountdown -= Time.deltaTime;

		if (!_levelCompleteScreenDisplayed) {
			if (Player.IsFalling) {		// Player fell from the platform
				LevelComplete(0, 0f);
			}
			else if (Player.ReachedEnd) {
				_isGamePaused = true;
				if (CheckGoalReached()) {		// Level completed successfully!
					int numOfStars = CalculateNumOfStarsToDisplay();
					LevelComplete(numOfStars, _levelCompleteScreenDelay);
				}
				else {		// Not enough cubes are collected!
					LevelComplete(0, _levelCompleteScreenDelay);
				}
			}
			else if (TimerCountdown <= 0) {		// Time is up!
				LevelComplete(0, 0.1f);
			}
		}
	}

	private void DisplayTutorialPanel() {
		GameUIController.DisplayTutorialPanel();
		PlayerPrefManager.SetTutorialSeen(true);
		TogglePause();
	}

	private void LoadData() {
		_currentLevel = DataManager.Instance.SelectedLevel;

		if (_currentLevel == GameConfigData.Instance.NumOfLevels) {
			_currentLevel--;
		}
	}

	private void SaveData(int passedLevel, int stars) {
		_playerData = SaveSystem.LoadData();

		if (_playerData != null && _playerData.CurrentLevel != passedLevel) {
			// checking if the score is less than the previous one
			if (_playerData.Scores[passedLevel] >= stars)
				return;		// return and don't save this score
		}

		SaveSystem.SaveData(passedLevel, stars);
	}

	private void LevelComplete(int numOfStars, float panelDelay) {
		StartCoroutine(GameUIController.DisplayLevelCompleteScreen(numOfStars, panelDelay));
		_levelCompleteScreenDisplayed = true;

		if (numOfStars > 0) {
			StartCoroutine(SoundManager.PlaySoundWithDelay(SoundManager.Audio.LevelWin, panelDelay));
			SaveData(_currentLevel, numOfStars);
		}
		else {
			StartCoroutine(SoundManager.PlaySoundWithDelay(SoundManager.Audio.LevelLose, panelDelay));
		}
	}

	private void CreateLevel() {
		Debug.Log("Curr level: " + _currentLevel);
		GameObject currLevel = Instantiate(GameConfigData.Instance.Levels[_currentLevel], new Vector3 (0, 0, 0f), Quaternion.identity) as GameObject;
		_currLevelData = currLevel.GetComponent<LevelData>();

		LevelGoal = _currLevelData.Goal;
		NumOfCubes = _currLevelData.NumOfCubes;
		LevelTime = _currLevelData.Time;
		TimerCountdown = LevelTime;
	}

	public bool CheckGoalReached() {
		return Player.CubeCount >= LevelGoal;
	}

	// Go to the next level
	public IEnumerator LoadNextLevel(float time) {
		yield return new WaitForSeconds(time);
		DataManager.Instance.SelectedLevel++;
		// All level are completed, return to main menu
		if (DataManager.Instance.SelectedLevel == GameConfigData.Instance.NumOfLevels)
			LoadScene(GameConfigData.Scene.MainMenu);
		else
			SceneManager.LoadScene((int)GameConfigData.Scene.Game);
	}

	public IEnumerator RestartLevel(float time) {
		yield return new WaitForSeconds(time);
		if (Time.timeScale == 0)            // if the game is paused, resume it and then, go to main menu
			TogglePause();
		SceneManager.LoadScene((int)GameConfigData.Scene.Game);
	}

	public void LoadScene(GameConfigData.Scene scene) {
		if (Time.timeScale == 0)            // if the game is paused, resume it and then, go to main menu
			TogglePause();
		SceneManager.LoadScene((int)scene);
	}

	public void TogglePause() {
		Time.timeScale = (Time.timeScale == 1) ? 0 : 1;
		_isGamePaused = (Time.timeScale != 1);
	}

	private int CalculateNumOfStarsToDisplay() {
		float cubePercentage = ((float)(Player.CubeCount - LevelGoal) + 1) / (float)((NumOfCubes - LevelGoal) + 1);
		int numOfStars = Mathf.RoundToInt(cubePercentage * 3);

		// make sure that max number of star is 3 and min is 1
		if (numOfStars > 3)
			numOfStars = 3;
		else if (numOfStars < 1)
			numOfStars = 1;

		return numOfStars;
	}
}
