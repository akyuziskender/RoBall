using UnityEngine;

namespace Game
{
	public class RandomDoorsController : MonoBehaviour
	{
		public Rigidbody[] Doors;
		public int NumOfWorkingDoors;
		private void Start() {
			int[] doorIndexes = {0, 1, 2, 3, 4};
			Randomize(ref doorIndexes, doorIndexes.Length);

			for (int i = 0; i < NumOfWorkingDoors; i++) {
				Doors[doorIndexes[i]].constraints = RigidbodyConstraints.None;
			}
		}

		private void Randomize(ref int[] arr, int n) { 		  
			// Start from the last element and swap one by one. We don't need to run for the first element that's why i > 0 
			for (int i = n - 1; i > 0; i--) { 
				int j = Random.Range(0, i+1); 	// Pick a random index from 0 to i 
				
				// Swap arr[i] with the element at random index 
				int temp = arr[i]; 
				arr[i] = arr[j]; 
				arr[j] = temp; 
			} 
		} 
	}
}