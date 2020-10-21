using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{
	public class LevelGUI : MonoBehaviour
	{
		public TextMeshProUGUI CollectibleCountText;
		public Slider TimeSlider;

		public void SetTimerSlider(float value) {
			TimeSlider.value = value; 
		}
	}
}