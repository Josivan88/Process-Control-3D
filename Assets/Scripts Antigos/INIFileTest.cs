using UnityEngine;
using System.Collections;
//conseguir data e hora
//Criar pastas
using System;
//criar pastas
using System.IO;
//chamar executavel
using System.Diagnostics;



public class INIFileTest : MonoBehaviour {
	// Use this for initialization
	void Start () {/*
		//arquivos ini
		string path = Application.dataPath + "/Config.ini";
		AP_INIFile ini = new AP_INIFile(path);
		//ini.WriteString("Section", "String", "This is a string.");
		//ini.WriteInt("Section", "Int", 12345678);
		//ini.WriteFloat("Section", "Float", 10345.53457f);
		//ini.WriteString("Display", "Quality", "Fine");
		//ini.WriteInt("Display", "ResolutionWidth", Screen.width);
		//ini.WriteInt("Display", "ResolutionHeight", Screen.height); 
		
		//string s = ini.ReadString("Section", "String");
		//int i = ini.ReadInt("Section", "Int");
		//float f = ini.ReadFloat("Section", "Float");
		//Debug.Log(s + " " + i.ToString() + " " + f.ToString());
		//Debug.Log(System.DateTime.Now.ToString("hh:mm:ss"));
		
		//criar pastas
		if (!Directory.Exists(Application.dataPath+"/Screenshots")){
		System.IO.Directory.CreateDirectory(Application.dataPath+"/Screenshots");
		}*/
		
		/*chamar executavel
		var stringPath = "/"; 
		var myProcess = new Process(); 
		myProcess.StartInfo.FileName = "Quantum 3.08.exe"; 
		myProcess.StartInfo.Arguments = stringPath; 
		myProcess.Start();*/
	
	}
	
	void Update() {
		/*//capturar a tela
		if (Input.GetKey("f10"))
		if (Directory.Exists(Application.dataPath+"/Screenshots")){
		if (!File.Exists(Application.dataPath+"Screenshots/Screenshot "+System.DateTime.Now.ToString("hh.mm.ss")+" "+System.DateTime.Now.ToString("MM-dd-yyyy")+".png")){
		Application.CaptureScreenshot("Screenshots/Screenshot "+System.DateTime.Now.ToString("hh.mm.ss")+" "+System.DateTime.Now.ToString("MM-dd-yyyy")+".png");
		}
		}*/
	  
    }
}
