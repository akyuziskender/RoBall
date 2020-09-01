[System.Serializable]
public class PlayerData {
	public int CurrentLevel;
	public int[] Scores;

	public PlayerData(int level, int stars, PlayerData previousData) {
		if (previousData != null) {
			int preCurrentLevel = previousData.CurrentLevel;

			if (level < preCurrentLevel) {		// there is an update on previous scores
				CurrentLevel = preCurrentLevel;
				Scores = new int[preCurrentLevel];
			}
			else if (level == preCurrentLevel) {	// there is a new score
				CurrentLevel = level + 1;
				Scores = new int[level + 1];
			}

			// copying the previous scores
			for (int i = 0; i < preCurrentLevel; i++) {
				Scores[i] = previousData.Scores[i];
			}
		}
		else {
			// no save file, create the object with default values
			CurrentLevel = 1;
			Scores = new int[1];
		}

		// save the new score in the array
		Scores[level] = stars;
	}
}
