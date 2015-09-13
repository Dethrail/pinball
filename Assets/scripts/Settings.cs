using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

[XmlType("settings")]
public sealed class Settings
{
	public static Settings Instance;
	[XmlAttribute("lives-count")]
	public int LivesCount;

	[XmlArray("obstacles")]
	public List<XMLObstacleType> ObstacleScore;

	public static void Create()
	{
		if(!LocalFolderExists()) {
			WriteToLocalStorage();
		}
		var resourceDirectory = GetTextFilesFromDirectory(GetLocalFilesDirectory());
		foreach(var settings in resourceDirectory) {

			Settings s = XmlResource.LoadFromFile<Settings>(settings);
			if(s != null) {
				Instance = s;
			}
		}

		if(Instance == null) {
			Instance = new Settings();
			Instance.ObstacleScore = new List<XMLObstacleType>((byte)ObstacleType.Count);
			Instance.LivesCount = 3;
			foreach(ObstacleType suit in Enum.GetValues(typeof(ObstacleType))) {
				XMLObstacleType elem = new XMLObstacleType();
				elem.Name = suit.ToString();
				elem.Score = 25;
				Instance.ObstacleScore.Add(elem);
			}
		}
	}

	public static bool LocalFolderExists()
	{
		return GetTextFilesFromDirectory(GetLocalFilesDirectory()).Length > 0;
	}

	public static string[] GetTextFilesFromDirectory(string directory)
	{
		return Directory.GetFiles(directory, "*.xml");
	}

	public static string GetSourceDirectory()
	{
		return Application.streamingAssetsPath + "/";
	}

	public static string GetLocalFilesDirectory()
	{
#if UNITY_EDITOR
		return Application.streamingAssetsPath + "/";
#else
		return Application.persistentDataPath;
#endif
	}

	public static void WriteToLocalStorage()
	{
		string srcDir = GetSourceDirectory();
		string dstDir = GetLocalFilesDirectory();
		var villageInputFiles = GetTextFilesFromDirectory(GetSourceDirectory()).Select(x => x.Substring(srcDir.Length + 1));
		foreach(var villageFile in villageInputFiles) {
			File.Copy(Path.Combine(srcDir, villageFile), Path.Combine(dstDir, villageFile), true);
		}
	}

	public void Save()
	{
		XmlResource.SaveToFile(Path.Combine(GetLocalFilesDirectory(), "settings.xml"), Instance);
	}
}

[XmlType("obstacle")]
public sealed class XMLObstacleType
{
	[XmlAttribute("name")]
	public string Name;
	[XmlAttribute("score")]
	public int Score;
}