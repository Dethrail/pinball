using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

// TODO: Test
public class XmlResource
{
	private const string BACKUP_NAME_POSTFIX = ".bak";
	private const string EDITOR_TEMP_FOLDER = "assets/Resources/";

	public static T LoadFromResources<T>(string path) where T:class, new()
	{
		TextAsset textAsset = null;
		StringReader stringReader = null;
		XmlSerializer xmlSerializer = null;
		T deserializedObject = null;

		try {
			textAsset = Resources.Load(path, typeof(TextAsset)) as TextAsset;
			if(textAsset == null)
				throw new System.Exception("null resource");

			stringReader = new StringReader(textAsset.text);
			xmlSerializer = new XmlSerializer(typeof(T));
			deserializedObject = xmlSerializer.Deserialize(stringReader) as T;
			stringReader.Close();
		} catch(System.Exception e) {
			Debug.LogWarning("Unable to load file at : " + path + "\n" + e.Message);
			if(stringReader != null)
				stringReader.Close();
		} finally {
			textAsset = null;
			Resources.UnloadUnusedAssets();
		}

		return deserializedObject;
	}

	public static T LoadFromFile<T>(string path) where T:class, new()
	{
		return LoadFromFile<T>(path, true);
	}

	public static T LoadFromFile<T>(string path, bool backup) where T:class, new()
	{
		string filename;
		StreamReader streamReader = null;
		XmlSerializer xmlSerializer = null;
		T deserializedObject = null;

		//if(Application.platform == RuntimePlatform.WindowsEditor ||
		//	Application.platform == RuntimePlatform.OSXEditor)
		//	filename = EDITOR_TEMP_FOLDER + path;
		//else
		//	filename = Application.persistentDataPath + "/" + path;
		filename = path;
		try {
			streamReader = new StreamReader(filename);
			xmlSerializer = new XmlSerializer(typeof(T));
			deserializedObject = xmlSerializer.Deserialize(streamReader) as T;
			streamReader.Close();
		} catch(System.Exception e) {
			Debug.LogWarning("Unable to load file at : " + path + "\n" + e.Message);

			if(streamReader != null)
				streamReader.Close();
			if(File.Exists(filename))
				File.Delete(filename);

			if(backup) {
				Debug.LogWarning("Attempt to restore from backup");

				string backupFilename = filename + BACKUP_NAME_POSTFIX;
				if(File.Exists(backupFilename)) {
					File.Copy(backupFilename, filename, true);
					deserializedObject = LoadFromFile<T>(path, false);
				} else {
					Debug.LogWarning("Unable to find backup file at : " + backupFilename);
				}
			}
		} finally {
			//
		}

		return deserializedObject;
	}

	public static bool SaveToFile(string path, object data)
	{
		return SaveToFile(path, data, true);
	}

	public static bool SaveToFile(string path, object data, bool backup)
	{
		bool saved = false;
		string filepath;

		//if (Application.platform == RuntimePlatform.WindowsEditor ||
		//    Application.platform == RuntimePlatform.OSXEditor)
		//	filepath = EDITOR_TEMP_FOLDER + path;
		//else
		//{
		//	filepath = Application.persistentDataPath + "/" + path;
		//}
		filepath = path;
		if(backup && File.Exists(filepath)) {
			try {
				File.Copy(filepath, filepath + BACKUP_NAME_POSTFIX, true);
			} catch(System.Exception e) {
				Debug.LogWarning("Unable to create backup file at : " + path + "\n" + e.Message);
			}
		}

		StreamWriter streamWriter = null;
		XmlSerializer xmlSerializer = null;

		try {
			streamWriter = new StreamWriter(filepath, false);
			xmlSerializer = new XmlSerializer(data.GetType());
			xmlSerializer.Serialize(streamWriter, data);
			streamWriter.Flush();
			streamWriter.Close();

			saved = true;
			//Debug.LogWarning("Saved at: " + filepath);
		} catch(System.Exception e) {
			Debug.LogError("Unable to save file at : " + path + "\n" + e.Message);
			if(streamWriter != null)
				streamWriter.Close();
		} finally {
			//
		}

		return saved;
	}
}
