

using System.Text;
using System.Runtime.Serialization;
using System;
using System.Net.Mail;
using System.Diagnostics;
using System.IO.Ports;
using System.Globalization;
using System.Runtime.InteropServices;
using EncryptStringSample;

namespace Reusable
{
    using UnityEngine;
    using System.Collections;
    using System.IO;
    using System.Net;
    using UnityEngine.Networking;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;

    [System.Serializable]
    public class GrabFilePath
    {
        public Vector2 ScrollPosition = new Vector2();
        public string Path = string.Empty;
        public string FileName = string.Empty;
        public string TargetFile = string.Empty;
        public string OutputPath = string.Empty;
        public string PreviousPath = string.Empty;
        public string NextPath = string.Empty;
        public string[] allDrives = null;
        public string[] allDrivesAndLabels = null;
        public float MouseX = 0f;
        public float MouseY = 0f;
        public bool clicked = false;
        public bool clicked2 = false;
        public bool clicked3 = false;
        public int Ind = 0;

    }

    public class Common
    {

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        public static Texture2D FolderIcon = Resources.Load<Texture2D>("Textures/FolderIcon");
        public static Texture2D FileIcon = Resources.Load<Texture2D>("Textures/FileIcon");

        [DllImport("KERNEL32.DLL")]
        public static extern int GetSystemDefaultLCID();

        public static void Minimize()
        {
            ShowWindow(GetActiveWindow(), 2);
        }
        public static void FullScreen(bool fullscreen)
        {
            //Screen.SetResolution(Screen.width, Screen.height, fullscreen);
            Screen.fullScreen = !Screen.fullScreen;
            //Screen.fullScreenMode = FullScreenMode.Windowed;
            if (fullscreen) Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            else Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        //Checa o pais de origem do programa, online
        public static IEnumerator CheckLocalInfo(string Country)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get("https://www.ip-adress.com/what-is-my-ip-address"))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    UnityEngine.Debug.Log(webRequest.error);
                }
                else
                {
                    // Show results as text
                    UnityEngine.Debug.Log(GetCountryFromIPAdress(webRequest.downloadHandler.text));
                }
            }
        }

        public static string GetCountryFromIPAdress(string text)
        {
            int lineNo = 322;
            string[] lines = text.Replace("\r", "").Split('\n');
            return lines.Length >= lineNo ? lines[lineNo - 1] : null;
            //return Country;
        }

        //Checa licença por ano, online
        public static IEnumerator CheckLicenceYear(int FinishYear)
        {

            using (UnityWebRequest webRequest = UnityWebRequest.Get("http://www.whattimeisit.com/"))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Log(webRequest.error);
                }
                else
                {
                    if (GetYearFromWhatTimeIsIt(webRequest.downloadHandler.text) >= FinishYear)
                    {
                        Debug.Log("Licensa vencida");
                        Application.Quit();
                    }
                    else
                    {
                        Debug.Log("Licensa Ainda válida");
                    }
                }
            }
        }

        public static int GetYearFromWhatTimeIsIt(string text)
        {

            int i = text.IndexOf("<BR>", 850);
            int year = int.Parse(text.Substring(i - 4, 4));
            Debug.Log(year);
            return year;
        }

        //Endereço de IP
        public static string LocalIpAddress()
        {
            IPHostEntry host;
            string LocalIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    LocalIP = ip.ToString();
                    break;
                }
            }
            return LocalIP;
        }

        //regiao do usuario
        public static string PlayerCountry()
        {
            CultureInfo culture = CultureInfo.GetCultureInfo(GetSystemDefaultLCID());
            return culture.DisplayName;
        }

        public static float[] PartOfArray(float[,,] InputArray, int Line, int Column)
        {
            float[] array = new float[InputArray.GetLength(2)];
            for (int i = 0; i < InputArray.GetLength(2); i++)
            {
                array[i] = InputArray[Line, Column, i];
            }
            return array;
        }

        public static string ShowArray(float[] InputArray)
        {
            string text = string.Empty;
            for (int i = 0; i < InputArray.GetLength(0); i++)
            {
                text = text + InputArray[i].ToString("f4") + " ";
            }
            return text;
        }

        public static string ShowArrayStrings(string[] InputArray)
        {
            string text = string.Empty;
            for (int i = 0; i < InputArray.GetLength(0); i++)
            {
                text = text + InputArray[i] + i.ToString("D2") + " ";
            }
            return text;
        }

        public static float Media(float[] Entrada)
        {
            float CumResult = 0;
            for (int i = 0; i < Entrada.Length; i++)
            {
                CumResult += Entrada[i];
            }
            CumResult = CumResult / Entrada.Length;
            return CumResult;
        }

        //Arquivos de texto
        public static void AddTextToFile(string adress, string value)
        {
            FileStream fs = new FileStream(adress, FileMode.Append, FileAccess.Write, FileShare.Write);
            fs.Close();
            StreamWriter sw = new StreamWriter(adress, true, Encoding.Unicode);
            string NextLine = "\r\n" + value;
            sw.Write(NextLine);
            sw.Close();
        }

        //Arquivos de texto
        public static void AddText(string adress, string value)
        {
            FileStream fs = new FileStream(adress, FileMode.Append, FileAccess.Write, FileShare.Write);
            fs.Close();
            StreamWriter sw = new StreamWriter(adress, true, Encoding.UTF8);
            string NextLine = "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + " " + System.DateTime.Now.ToString("dd-MM-yyyy") + " " + value;
            sw.Write(NextLine);
            sw.Close();
        }

        //Fala 
        public static void Speak(string texto)
        {
            if (!Directory.Exists(Application.dataPath + "/TempSpeak"))
            {
                Directory.CreateDirectory(Application.dataPath + "/TempSpeak");
            }
            string str = UnityEngine.Random.Range(0, 999999999).ToString("D9");
            string text = Application.dataPath + "/TempSpeak/Temp" + str + ".vbs";
            AddTextToFile(text, "\r\nSet voice = CreateObject(\"SAPI.Spvoice\")");
            AddTextToFile(text, "\r\nvoice.Rate = 1.2");
            AddTextToFile(text, "\r\nvoice.Volume = 100");
            AddTextToFile(text, "\r\nvoice.Speak\"" + texto + "\"");
            AddTextToFile(text, "Set obj = CreateObject(\"Scripting.FileSystemObject\")");
            AddTextToFile(text, "obj.DeleteFile(\"" + text + "\")");
            new Process
            {
                StartInfo =
                        {
                            FileName = "cscript",
                            Arguments = "/B /Nologo \"" + text + "\"",
                            WindowStyle = ProcessWindowStyle.Hidden
                        }
            }.Start();
        }

        //Testar conexao com a internet
        //using System.Net;
        public static bool CheckForInternetConnection()
        {
            //return !(Application.internetReachability == NetworkReachability.NotReachable);
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    { return true; }
                }
            }
            catch
            { return false; }
        }

        //Tempo intuitivo
        public static string IntuitiveTime(float TimeInSeconds)
        {
            string Result = "algum tempo";
            if (TimeInSeconds >= 0f && TimeInSeconds < 60f) Result = TimeInSeconds.ToString("F1") + " segundos";
            if (TimeInSeconds >= 60f && TimeInSeconds < 3600f) Result = (TimeInSeconds / 60f).ToString("F1") + " minutos";
            if (TimeInSeconds >= 3600f && TimeInSeconds < 86400f) Result = (TimeInSeconds / 3600f).ToString("F1") + " horas";
            if (TimeInSeconds >= 86400f && TimeInSeconds < 604800f) Result = (TimeInSeconds / 86400f).ToString("F1") + " dias";
            if (TimeInSeconds >= 604800f && TimeInSeconds < 2721600f) Result = (TimeInSeconds / 604800f).ToString("F1") + " semanas";
            if (TimeInSeconds >= 2721600f && TimeInSeconds < 31536000f) Result = (TimeInSeconds / 2721600f).ToString("F1") + " meses";
            if (TimeInSeconds >= 31536000f) Result = (TimeInSeconds / 31536000f).ToString("F1") + " anos";
            return Result;
        }

        //Tempo formatado

        public static string FormatTime(float TimeInSeconds)
        {

            TimeSpan t = TimeSpan.FromSeconds(TimeInSeconds);

            string Result = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                          t.Hours,
                                          t.Minutes,
                                          t.Seconds);
            return Result;
        }

        //Conseguir versao do windows
        public static string WindowsVersion()
        {
            int foundS1 = SystemInfo.operatingSystem.IndexOf("(");
            return SystemInfo.operatingSystem.Substring(0, foundS1 - 2);
        }

        //Deleta todo o conteudo de uma pasta
        public static void DeleteFolderContent(string LocalPath)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(LocalPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        //enviar e-mail
        public static void SendEmail(string[] receivers, string Assunto, string Corpo, string[] PathAtachedFile, bool isHtml)
        {
            
            using (var mail = new MailMessage
            {
                From = new MailAddress(StringCipher.Decrypt("JXM9azfLzrSqaBht6Jmg4VrSPY8IxHe2U8q2aft8OM0GNR3xkBS828xLyCJThcYoGKOUzv1Rq/N6byk2+Nkm9tMgosFJ0SYhC5Pr5WvT/Z619RJRRrJ1Jk/RpORqBGje", "5a1l4k2m325b")),
                Subject = Assunto,
                Body = Corpo, 
                IsBodyHtml = isHtml
            })
            {
                
                for (int i = 0; i < receivers.Length; i++)
                {
                    if (receivers[i] != "" && receivers[i] != string.Empty)
                    {
                        if (receivers[i].Contains("@"))
                            mail.To.Add(receivers[i]);
                    }
                }

                for (int j = 0; j < PathAtachedFile.Length; j++)
                {
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(PathAtachedFile[j]);
                    mail.Attachments.Add(attachment);
                }

                var smtpServer = new SmtpClient(StringCipher.Decrypt("XKr8K+qRnCRI9XlJVaIXT6dIXm9ajGhGC0atggQ1Gy/znP3LxxfTqIMqF7zYioMzbwL+zi/X4cvGxYGSjD0KJNssYYPrISvq4m26GN7CmgLT6WMatf1oo8c0VEoVxf6e", "5a1l4k2m325b"))
                {
                    Port = 587,
                    Credentials = (ICredentialsByHost)new NetworkCredential(
                        StringCipher.Decrypt("JXM9azfLzrSqaBht6Jmg4VrSPY8IxHe2U8q2aft8OM0GNR3xkBS828xLyCJThcYoGKOUzv1Rq/N6byk2+Nkm9tMgosFJ0SYhC5Pr5WvT/Z619RJRRrJ1Jk/RpORqBGje", "5a1l4k2m325b"),
                        StringCipher.Decrypt("ztgV/UR2Tq3UtWm1wvWRnA0jE70Palb4nj3EU5FFD8Iv955fAL86rKfRy0cdVGAiEZmV2f4ewm7Rb3QsDdEI5sh7lHJnhUPNZpj07NwILiemA8fTb0fA/o65FP5U/KFZ", "5a1l4k2m325b"))
                };
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                //ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback();
                smtpServer.EnableSsl = true;
                smtpServer.Send(mail);
            }
        }

        public bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            bool isOk = true;
            // If there are errors in the certificate chain, look at each error to determine the cause.
            if (sslPolicyErrors != SslPolicyErrors.None)
            {
                for (int i = 0; i < chain.ChainStatus.Length; i++)
                {
                    if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
                    {
                        chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                        chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                        chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                        chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                        bool chainIsValid = chain.Build((X509Certificate2)certificate);
                        if (!chainIsValid)
                        {
                            isOk = false;
                        }
                    }
                }
            }
            return isOk;
        }

        //Log de atividades
        public static void Log(string Text)
        {
            string adress = Application.dataPath + "/Log.txt";
            if (!File.Exists(adress))
            {
                File.Create(adress);
            }
            Debug.Log(Text);
            AddTextToFile(adress, System.DateTime.Now.ToString("hh.mm.ss") + " " + System.DateTime.Now.ToString("dd-MM-yyyy") + " " + Text);
        }

        public static string GetPath(int PosX, int PosY, int SizeX, int SizeY, string extension, GrabFilePath Info, out bool visible)
        {

            visible = true;
            Info.OutputPath = string.Empty;
            GUI.skin.label.fontSize = 12;

            Event e = Event.current;
            Info.MouseX = e.mousePosition.x - PosX - 200;
            Info.MouseY = e.mousePosition.y - PosY - 75;
            Info.Ind = Mathf.RoundToInt((Info.MouseY + Info.ScrollPosition.y - 23f) / 45f);
            //Debug.Log(Ind.ToString());
            //GUI.Label(new Rect(e.mousePosition.x, e.mousePosition.y, 20, 25), Info.Ind.ToString());

            string[] allfiles = Directory.GetFiles(Info.Path, "*." + extension);
            string[] allfolders = Directory.GetDirectories(Info.Path);
            string[] AllinPath = new string[allfiles.Length + allfolders.Length];
            allfolders.CopyTo(AllinPath, 0);
            allfiles.CopyTo(AllinPath, allfolders.Length);

            Texture2D gray20Texture = CreateTexture((4 * Color.black + Color.white) / 5);
            Texture2D gray75Texture = CreateTexture((Color.gray + Color.white) / 2);
            Texture2D gray33Texture = CreateTexture((2 * Color.black + Color.white) / 3);

            //fundo
            GUI.DrawTexture(new Rect(PosX, PosY, SizeX, SizeY), gray20Texture);
            GUI.DrawTexture(new Rect(PosX, PosY, SizeX, 30), gray75Texture);

            GUI.DrawTexture(new Rect(PosX + 10, PosY + 75, 180, 440), gray33Texture);
            GUI.DrawTexture(new Rect(PosX + 200, PosY + 75, 590, 440), gray33Texture);

            if (GUI.Button(new Rect(PosX + 20, PosY + 80, 160, 25), "Desktop")) { Info.PreviousPath = Info.Path; Info.Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); }
            if (GUI.Button(new Rect(PosX + 20, PosY + 110, 160, 25), "Documentos")) { Info.PreviousPath = Info.Path; Info.Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); }
            if (GUI.Button(new Rect(PosX + 20, PosY + 140, 160, 25), "Dados do programa")) { Info.PreviousPath = Info.Path; Info.Path = Application.dataPath; }
            //if (GUI.Button(new Rect(PosX + 20, PosY + 230, 160, 40), "C:\\")) { Info.PreviousPath = Info.Path; Info.Path = "C:\\"; }

            for (int i = 0; i < Info.allDrives.Length; i++)
            {
                if (GUI.Button(new Rect(PosX + 20, PosY + 170 + i * 30, 160, 25), Info.allDrives[i])) { Info.PreviousPath = Info.Path; Info.Path = Info.allDrives[i] + "//"; }
            }


            GUILayout.BeginArea(new Rect(PosX + 200, PosY + 75, 590, 440));
            Info.ScrollPosition = GUILayout.BeginScrollView(Info.ScrollPosition, GUILayout.Width(590), GUILayout.Height(440));

            for (int i = 0; i < 4 * AllinPath.Length / 3; i++)
            {
                GUILayout.Label("\r\n");
            }


            for (int i = 0; i < AllinPath.Length; i++)
            {
                //GUI.DrawTexture (new Rect (10,10+45*i,500, 40), gray75Texture);
                if (i < allfolders.Length)
                {
                    if (i == Info.Ind && Info.MouseX > 0f && Info.MouseX < 580 && Info.MouseY > 0 && Info.MouseY < 430) GUI.DrawTexture(new Rect(5, 5 + 45 * i, 590, 45), gray20Texture);
                    GUI.DrawTexture(new Rect(10, 10 + 45 * i, 35, 35), FolderIcon);
                    GUI.Label(new Rect(60, 20 + 45 * i, 520, 25), new DirectoryInfo(AllinPath[i]).Name);
                }
                if (i >= allfolders.Length)
                {
                    if (i == Info.Ind && Info.MouseX > 0f && Info.MouseX < 580 && Info.MouseY > 0 && Info.MouseY < 430) GUI.DrawTexture(new Rect(5, 5 + 45 * i, 590, 45), gray20Texture);
                    GUI.DrawTexture(new Rect(10, 10 + 45 * i, 35, 35), FileIcon);
                    GUI.Label(new Rect(60, 20 + 45 * i, 520, 25), Path.GetFileName(AllinPath[i]));
                }

            }


            GUILayout.EndScrollView();
            GUILayout.EndArea();

            if (Input.GetMouseButtonDown(0) && Info.clicked && Info.clicked2 && Info.MouseX > 0f && Info.MouseX < 580 && Info.MouseY > 0 && Info.MouseY < 430)
            {
                if (Info.Ind < allfolders.Length)
                {
                    Info.PreviousPath = Info.Path.Replace(@"\", @"/");
                    Info.Path = AllinPath[Info.Ind];
                }
                if (Info.Ind >= allfolders.Length)
                {
                    Info.TargetFile = AllinPath[Info.Ind].Replace(@"\", @"/");
                    Info.FileName = Path.GetFileName(Info.TargetFile).Replace(@"\", @"/");
                    Log(Info.TargetFile.Replace(@"\", @"/"));
                }
                Info.clicked = false;
                Info.clicked2 = false;
            }
            Info.clicked3 = false;

            if (Input.GetMouseButtonDown(0) && Info.clicked && Info.clicked2 && Info.MouseX > 0f && Info.MouseX < 580 && Info.MouseY > 0 && Info.MouseY < 430)
            {
                Info.clicked3 = true;
            }

            if (Input.GetMouseButtonDown(0) && Info.clicked && Info.MouseX > 0f && Info.MouseX < 580 && Info.MouseY > 0 && Info.MouseY < 430)
            {
                Info.clicked2 = true;
            }

            if (Input.GetMouseButtonDown(0) && Info.MouseX > 0f && Info.MouseX < 580 && Info.MouseY > 0 && Info.MouseY < 430)
            {
                Info.clicked = true;
            }

            GUI.color = Color.black;
            GUI.Label(new Rect(PosX + 10, PosY + 5, 80, 25), "Carregar");
            GUI.color = Color.white;
            if (GUI.Button(new Rect(PosX + SizeX - 30, PosY + 5, 20, 20), "X")) { visible = false; }
            if (GUI.Button(new Rect(PosX + 10, PosY + 40, 25, 25), "<"))
            {
                Info.NextPath = Info.Path.Replace(@"\", @"/");
                Info.Path = Info.PreviousPath.Replace(@"\", @"/");
            }
            if (GUI.Button(new Rect(PosX + 45, PosY + 40, 25, 25), ">"))
            {
                Info.PreviousPath = Info.Path.Replace(@"\", @"/");
                Info.Path = Info.NextPath.Replace(@"\", @"/");
            }
            GUI.Label(new Rect(PosX + 80, PosY + 40, 710, 25), Info.TargetFile);
            //Info.TargetFile = GUI.TextField (new Rect (PosX+80,PosY+40, 710, 25), Info.TargetFile, 25);
            //criar quadro
            DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX, PosY + SizeY), gray75Texture, 1);
            DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX + SizeX, PosY), gray75Texture, 1);
            DrawLineStretched(new Vector2(PosX + SizeX, PosY), new Vector2(PosX + SizeX, PosY + SizeY), gray75Texture, 1);
            DrawLineStretched(new Vector2(PosX, PosY + SizeY), new Vector2(PosX + SizeX, PosY + SizeY), gray75Texture, 1);

            GUI.Label(new Rect(PosX + 150, PosY + SizeY - 70, 50, 25), "Nome");
            Info.FileName = GUI.TextField(new Rect(PosX + 200, PosY + SizeY - 70, 540, 25), Info.FileName, 25);
            GUI.Label(new Rect(PosX + SizeX - 50, PosY + SizeY - 70, 30, 25), "*." + extension);

            if (GUI.Button(new Rect(PosX + SizeX - 260, PosY + SizeY - 35, 120, 25), "Abrir"))
            {
                Info.OutputPath = Info.TargetFile.Replace(@"\", @"/");
                Log("Arquivo aberto: " + Info.TargetFile.Replace(@"\", @"/"));
                visible = true;
            }

            if (GUI.Button(new Rect(PosX + SizeX - 130, PosY + SizeY - 35, 120, 25), "Cancelar"))
            {
                visible = false;
            }

            return Info.OutputPath;
        }


        public static Texture2D CreateTexture(Color TextColor)
        {
            Texture2D OutTexture = new Texture2D(1, 1);
            OutTexture.SetPixel(0, 0, TextColor);
            OutTexture.Apply();
            return OutTexture;
        }

        public static void DrawLineStretched(Vector2 lineStart, Vector2 lineEnd, Texture2D texture, int thickness)
        {
            Vector2 lineVector = lineEnd - lineStart;
            float angle = Mathf.Rad2Deg * Mathf.Atan(lineVector.y / lineVector.x);
            if (lineVector.x < 0)
            {
                angle += 180;
            }

            if (thickness < 1)
            {
                thickness = 1;
            }

            // The center of the line will always be at the center
            // regardless of the thickness.
            int thicknessOffset = (int)Mathf.Ceil(thickness / 2);

            GUIUtility.RotateAroundPivot(angle,
                                         lineStart);
            GUI.DrawTexture(new Rect(lineStart.x,
                                     lineStart.y - thicknessOffset,
                                     lineVector.magnitude,
                                     thickness),
                            texture);
            GUIUtility.RotateAroundPivot(-angle, lineStart);
        }

        //Regressao linear de dados
        public static void RegressaoLinear(float[] X, float[] Y, out float a, out float b, out float Rsquare)
        {
            float Xmedio = Media(X);
            float Ymedio = Media(Y);
            float Numerador = 0f;
            float Denominador = 0f;
            for (int i = 0; i < X.Length; i++)
            {
                Numerador += (X[i] - Xmedio) * (Y[i] - Ymedio);
                Denominador += Mathf.Pow((X[i] - Xmedio), 2f);
            }
            a = Numerador / Denominador;
            b = Ymedio - a * Xmedio;
            float Sresidual = 0f;
            float Stotal = 0f;
            for (int i = 0; i < Y.Length; i++)
            {
                Sresidual += Mathf.Pow((Y[i] - a * X[i] - b), 2f);
                Stotal += Mathf.Pow((Y[i] - Ymedio), 2f);
            }
            Rsquare = 1f - Sresidual / Stotal;
        }

        //Abrir endereço no explorer
        public static void ShowExplorerSelected(string itemPath)
        {
            itemPath = itemPath.Replace(@"/", @"\");   // explorer doesn't like front slashes
            System.Diagnostics.Process.Start("explorer.exe", "/select," + itemPath);
        }

        //Abrir endereço no explorer
        public static void ShowExplorerInside(string itemPath)
        {
            itemPath = itemPath.Replace(@"/", @"\");   // explorer doesn't like front slashes
            System.Diagnostics.Process.Start("explorer.exe", itemPath);
        }

        //Salva parte da tela
        public static void SavePartOfScreen(int PositionX, int PositionY, int Largura, int Altura, String Name)
        {
            //criar pastas
            if (!Directory.Exists(Application.dataPath + "/Screenshots")) { System.IO.Directory.CreateDirectory(Application.dataPath + "/Screenshots"); }
            // Create a texture the size of the screen, RGB24 format
            int width = Largura;
            int height = Altura;
            int X = PositionX;
            int Y = PositionY;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
            // Read screen contents into the texture
            tex.ReadPixels(new Rect(X, Y, width, height), 0, 0);
            tex.Apply();
            // Encode texture into PNG
            byte[] bytes = tex.EncodeToPNG();
            Texture2D.DestroyImmediate(tex, true);
            //also write to a file in the project folder
            File.WriteAllBytes(Application.dataPath + "/Screenshots/" + Name + ".png", bytes);

        }

        //		public void ConnectToArduino(bool Connect){
        //			String Log = Application.dataPath+"/Log.txt";
        //			
        //			// Get a list of serial port names.
        //			ports = SerialPort.GetPortNames();
        //			
        //			//Iniciando a conexao com o Arduino
        //			if (ports.Length > 0 && !Simulation) {
        //				if (COMPortNumber(ports[ports.Length - 1]) < 10)
        //				{
        //					sp = new SerialPort(ports[ports.Length - 1], 9600);
        //				}
        //				if (COMPortNumber(ports[ports.Length - 1]) >= 10)
        //				{
        //					sp = new SerialPort("\\\\.\\" + ports[ports.Length - 1], 9600);
        //				}
        //				Abertura+="Tentando se conectar a porta: " + ports[ports.Length - 1].ToString()+" ...";
        //			}
        //			if (Connect) {OpenConnection ();}
        //			if (ports.Length == 0) {
        //				UnityEngine.Debug.Log("Nao ha portas seriais abertas disponiveis, o Arduino esta desconectado");
        //				AddText(Log,"Arduino desconectado");
        //				ConsoleString += "\r\n"+System.DateTime.Now.ToString("hh.mm.ss")+"  "+"Arduino desconectado";
        //				Abertura+="Arduino desconectado ...";
        //			}
        //		}
        //		
        //		public void OpenConnection()
        //		{
        //			// Get a list of serial port names.
        //			ports = SerialPort.GetPortNames();
        //			
        //			if (sp !=null){
        //				if (sp.IsOpen){
        //					sp.Close();
        //					UnityEngine.Debug.Log("Fechando Porta, pois ja esta aberta");
        //				}
        //				else{
        //					if (COMPortNumber(ports[ports.Length - 1]).ToString()==ArduinoPort || ArduinoPort=="")
        //					{
        //						sp.Open();
        //						sp.ReadTimeout = 1;
        //						UnityEngine.Debug.Log("Porta Aberta!");
        //						if (Arduino & !ArduinoConectado & ArduinoPort!=""){
        //							Speak("Arduino reconectado");
        //						}
        //					}
        //				}
        //			}
        //			else{
        //				if (sp.IsOpen){UnityEngine.Debug.Log("Porta Aberta!");}
        //				else{UnityEngine.Debug.Log("Porta Nula");}
        //			}
        //		}
    }

}