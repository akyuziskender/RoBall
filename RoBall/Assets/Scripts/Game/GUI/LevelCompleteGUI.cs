using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{
	public class LevelCompleteGUI : MonoBehaviour
	{
		public GameObject[] Stars;
		public GameObject Crown;
		public GameObject ContinueButton;
		public TextMeshProUGUI GoalText;
		public TextMeshProUGUI ResultMessage;

		public void DisplayObjects(int numberOfStars) {
			for (int i = 0; i < numberOfStars; i++) {
				Stars[i].SetActive(true);
			}

			Crown.SetActive(numberOfStars > 0);
			ContinueButton.SetActive(numberOfStars > 0);
		}

		public void SetGoalText(int currentCubes, int goal) {
			GoalText.text = currentCubes + "/" + goal;
		}

		public void SetResultMessage(string text) {
			ResultMessage.text = text;
		}
	}
}