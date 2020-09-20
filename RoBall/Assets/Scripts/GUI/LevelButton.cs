using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
	public static int Count = 0;
	[SerializeField] private int _levelNo;
	public GameObject[] Stars;
	public GameObject LockedImage;
	public GameObject Button;

	private void Start() {
		this.transform.localScale = Vector3.one;
		_levelNo = Count;
		Count++;
	}

	public void Setup(int levelNo, int stars, bool isUnlocked) {
		if (isUnlocked) {
			LockedImage.SetActive(false);
			Button.SetActive(true);
			Button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + levelNo;
		}

		for (int i = 0; i < stars; i++) {
			Stars[i].SetActive(true);
		}
	}

	public void Reset(bool isLocked) {
		if (isLocked) {
			Button.SetActive(false);
			LockedImage.SetActive(true);
		}
		
		for (int i = 0; i < 3; i++) {
				Stars[i].SetActive(false);		// disable all stars
		}
	}

	public void StartLevel() {
		SoundManager.PlaySound(SoundManager.Audio.Click);
		DataManager.Instance.SelectedLevel = _levelNo;
		SceneManager.LoadScene((int)GameConfigData.Scene.Game);
	}
}
