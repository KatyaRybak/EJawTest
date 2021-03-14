using System;
using System.IO;
using UnityEngine;

namespace DefaultNamespace
{
	public class JsonLoader : MonoBehaviour
	{
		[SerializeField]
		private JsonData _data;

		private string _filePath = "Assets/Resources/Info.json";

		public JsonData Data => _data;

		[ContextMenu("Save")]
		public void SaveFile()
		{
			string json = JsonUtility.ToJson(_data);
			using (FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate))
			{
				using (StreamWriter writer = new StreamWriter(fs))
				{
					writer.Write(json);
				}
			}

#if UNITY_EDITOR
			UnityEditor.AssetDatabase.Refresh();
#endif
		}

		public void ReadFile()
		{
			using (FileStream fs = new FileStream(_filePath, FileMode.Open))
			{
				using (StreamReader writer = new StreamReader(fs))
				{
					string json = writer.ReadToEnd();
					_data = JsonUtility.FromJson<JsonData>(json);
				}
			}
		}
	}

	[Serializable]
	public class JsonData
	{
		[SerializeField]
		private JsonDataItem[] _items = new JsonDataItem[0];

		public int Length => _items.Length;
		public JsonDataItem this[int i] => _items[i];
	}

	[Serializable]
	public class JsonDataItem
	{
		public string _bundleName;
		public string _prefabName;
	}
}