using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
	public class LevelSelectGUI : MonoBehaviour
	{
		public GameObject Levels;
		private PlayerData _playerData;

		private void Start() {
			_playerData = SaveSystem.LoadData();
			LevelButton.Count = 0;
			CreateLevelButtons();
		}

		private void CreateLevelButtons() {
			for (int i = 0; i < GameConfigData.Instance.NumOfLevels; i++) {
				GameObject instance = Instantiate(GameConfigData.Instance.LevelButton, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent(Levels.transform);
				if (_playerData != null) {
					if (i < _playerData.CurrentLevel) {
						int stars = _playerData.Scores[i];
						instance.GetComponent<LevelButton>().Setup(i + 1, stars, true);
					}
					else if (i == _playerData.CurrentLevel) {
						instance.GetComponent<LevelButton>().Setup(i + 1, 0, true);
					}
					else {
						instance.GetComponent<LevelButton>().Setup(i + 1, 0, false);
					}
				}
				else if (i == 0) {
					instance.GetComponent<LevelButton>().Setup(1, 0, true);
				}
			}
		}

		public void RefreshGUI() {
			int i = 0;
			foreach (Transform levelButton in Levels.transform) {
				LevelButton _levelButtonScript = levelButton.GetComponent<LevelButton>();
				if (i == 0) {
					_levelButtonScript.Reset(false);
				}
				else {
					_levelButtonScript.Reset(true);
				}
				i++;
			}
		}

		public void ContinuePlaying() {
			SoundManager.PlaySound(SoundManager.Audio.Click);
			PlayerData data = SaveSystem.LoadData();
			DataManager.Instance.SelectedLevel = (data != null) ? data.CurrentLevel : 0;
			SceneManager.LoadScene((int)GameConfigData.Scene.Game);
		}
	}
}