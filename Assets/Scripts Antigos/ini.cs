
using System.Text;
using System.IO;
using System.Globalization;

public class AP_INIFile {

	//Variables//
	private string my_file;
	
	//Constructor//
	public AP_INIFile (string filePath) {
		my_file = filePath;
	}
	
	//Methods//
	public void WriteString(string section, string key, string value) {

		//adicionar varios testes, o arquivo existe? a chave e a seção existe? em caso nulo o que fazer?
		if (File.Exists(my_file))
		{
			string[] lines = File.ReadAllLines(my_file);
			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i].Equals("[" + section + "]"))
				{
					for (int j = i; j < lines.Length; j++)
					{
						if (lines[j].Contains(key + "="))
						{
							int index = lines[j].IndexOf("=");
							lines[j] = lines[j].Substring(0, index + 1) + value;
							File.WriteAllLines(my_file, lines, Encoding.UTF8);
							break;
						}
						if (j==lines.Length-1) UnityEngine.Debug.Log("A chave " + key + " da seção " + section + " não foi encontrada.");
					}
					break;
				}
				if (i == lines.Length - 1) UnityEngine.Debug.Log("A seção " + section + " não foi encontrada.");
			}
		}
        else
        {
			UnityEngine.Debug.Log("Arquivo " + my_file + " não foi encontrado.");
		}
	}
	public string ReadString(string section, string key) {

		string result = "";
		if (File.Exists(my_file))
		{
			string[] lines = File.ReadAllLines(my_file);
			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i].Equals("[" + section + "]"))
				{
					for (int j = i; j < lines.Length; j++)
					{
						if (lines[j].Contains(key + "="))
						{
							int index = lines[j].IndexOf("=");
							result = lines[j].Substring(index + 1, lines[j].Length - index-1);
							break;
						}
						if (j == lines.Length - 1) UnityEngine.Debug.Log("A chave " + key + " da seção " + section + " não foi encontrada.");
					}
					break;
				}
				if (i == lines.Length - 1) UnityEngine.Debug.Log("A seção " + section + " não foi encontrada.");
			}
		}
		else
		{
			UnityEngine.Debug.Log("Arquivo " + my_file + " não foi encontrado.");
		}


		return result;
	}
	public void WriteInt(string section, string key, int value) {
		WriteString(section, key, value.ToString());
	}
	public int ReadInt(string section, string key) {
		int result = 0;
		int.TryParse(ReadString(section, key), NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
		return result;
	}
	public void WriteFloat(string section, string key, float value) {
		WriteString(section, key, value.ToString());
	}
	public float ReadFloat(string section, string key) {
		float result = 0f;
		float.TryParse(ReadString(section, key), NumberStyles.Float, CultureInfo.InvariantCulture, out result);
		return result;
	}
}