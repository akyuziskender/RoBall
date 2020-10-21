using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game
{
	public static class SaveSystem {
		private static string filename = "player.rtg";
		public static void SaveData(int level, int stars) {
			BinaryFormatter formatter = new BinaryFormatter();
			string path = Path.Combine(Application.persistentDataPath, filename);

			PlayerData previousData = LoadData();
		
			FileStream stream = new FileStream(path, FileMode.Create);

			PlayerData data = new PlayerData(level, stars, previousData);

			formatter.Serialize(stream, data);
			stream.Close();
		}

		public static PlayerData LoadData() {
			string path = Path.Combine(Application.persistentDataPath, filename);

			if (File.Exists(path)) {
				BinaryFormatter formatter = new BinaryFormatter();
				FileStream stream = new FileStream(path, FileMode.Open);

				PlayerData data = formatter.Deserialize(stream) as PlayerData;
				stream.Close();

				return data;
			}
			else {
				// Debug.LogError("Save file not found in " + path);
				return null;
			}
		}

		public static void DeleteData() {
			string path = Path.Combine(Application.persistentDataPath, filename);
			File.Delete(path);
		}
	}
}