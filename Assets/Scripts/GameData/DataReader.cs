using System;
using System.IO;
using UnityEngine;

public abstract class DataReader<T> : MonoBehaviour where T : class
{
	protected abstract string fileName { get; }
	private string saveFilePath => Application.persistentDataPath + fileName;
	public static T CustomData { get; private set; }

	public void LoadOnAwake(bool clearData)
	{
		if (clearData)
		{
			CustomData = (T)Activator.CreateInstance(typeof(T));
			SaveData();
		}
		else
		{
			GetData();
		}
	}

	public void SaveData()
	{
		if (!File.Exists(saveFilePath))
		{
			CreateNewSaveFile();
		}
		else
		{
			WriteDataFile();
		}
	}

	public void GetData()
	{
		if (!File.Exists(saveFilePath))
		{
			CreateNewSaveFile();
		}
		else
		{
			string text = File.ReadAllText(saveFilePath);
			CustomData = JsonUtility.FromJson<T>(text);
		}
	}

	private void CreateNewSaveFile()
	{
		CustomData = (T)Activator.CreateInstance(typeof(T));
		File.WriteAllText(saveFilePath, JsonUtility.ToJson(CustomData, prettyPrint: true));
	}

	private void WriteDataFile()
	{
		File.WriteAllText(saveFilePath, JsonUtility.ToJson(CustomData, prettyPrint: true));
	}
}
