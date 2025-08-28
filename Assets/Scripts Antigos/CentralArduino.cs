using UnityEngine;
using System.Collections;
//Interface de usuario
using UnityEngine.UI;
//Conexão com as portas
using System.IO.Ports;
//Conseguir Data e Hora
using System;
//criar pastas
using System.IO;
//chamar executavel
using System.Diagnostics;
//Arquivos de texto
using System.Text;
//E-mail
using System.Net;
using System.Net.Mail;
//Cubic interpolation
using TestMySpline;


public class CentralArduino : MonoBehaviour
{

    public string ProjectName;   //Nome do equipamento
    private bool ClipboardOcupado = false;

    public static bool Speaking = true;    //Fala de Eva, ativada ou nao pelo usuario.
    public static bool ControlSpeaking = true; //Controle pelo programa

    public static SerialPort sp; //Porta do Arduino
    public string[] ports;       //Todas as portas

    public bool Demonstrative = false;
    public bool Arduino = false;          //E Arduino
    public bool ArduinoConectado = false; //Arduino Conectado
    public string ArduinoPort;          //Porta confirmada do arduino
    public float TempoConexao;          //Tempo desde a ultima informaçao
    public bool ReadToGO = false;       //tudo pronto
    private bool ReadToGOMark = false;  //tudo pronto marcador
    public bool Simulation;             //Modo Simulaçao
    public bool RemoteControl = false;  //Modo controle remoto
    private int PassoCounter;           //Contador para geraçao de passo simulado
    public string COMPortResult;        //Resultado da porta COM
    public string DadoSimulado;         //Dado simulado
    public string RemoteData;           //Dado simulado

    public AudioSource[] Audios;
    public static AudioSource[] StaticAudios;

    public bool HUD = true;
    public GameObject[] HUDObjects;

    //EQUIPAMENTOS
    public string[] Equipamentos;               //Nomes dos equipamentos
    public string[] DescricaoEquipamentos;      //Descriçao dos equipamentos
    public bool[] StatusEquipamentos;           //Status dos equipamentos, Ligado\Desligado
    public Coroutine[] CorrotinasEquipamentos;
    public bool[] MarkStatusEquipamentos;       //Marcador do Status dos equipamentos, Ligado\Desligado
    public float[] TempoEquipamentos;           //Tempos de operaçao dos equipamentos
    public bool[] EquipamentosModulados;        //Equipamentos que usam PWM
    public bool[] EquipamentosLargePWM;         //Equipamentos que usam PWM simulado de baixa frequencia
    public bool[] RealStatusPWM;                //um equipamento de large PWM esta ligado ou desligado
    public float[] PorcentagemEquipamentos;     //Valores em porcentagem
    public float[] ValoresEquipamentos;         //Valores atualizados dos equipamentos
    public float[] ValoresAntigosEquipamentos;  //Valores antigos dos equipamentos
    public float[] LimiteMinEquipamentos;       // Limites minimos de operad
    public float[] LimiteMaxEquipamentos;       // Limites maximos de segurança dos sensores

    public GameObject[] Indicador3DEquipamentos; //Indicadores 3D
    public TextMesh[] Titulo3DIndEquipamentos;   //titulos dos indicadores 3D
    public TextMesh[] Status3DIndEquipamentos;   //Status dos indicadores 3D
    public TextMesh[] Info1Ind3DEquipamentos;    //Informaçao adicional 1 dos indicadores 3D
    public TextMesh[] Info2Ind3DEquipamentos;    //Informaçao adicional 2 dos indicadores 3D

    public string[] TextButtonEquip;           //Texto para o HUD
    public string[] StatusEquipString;         //Texto para o HUD

    //BATELADA
    public bool Batelada = false;                //Modo Batelada
    public float TempoInicioBatelada;          //Tempo do inicio da batelada
    private bool MarkBatelada = false;           //Marcador do inicio da batelada
    public bool[] EquipConcluidoBatelada;      //Equipamentos em batelada
    public string[] TempoInicialEquip;         //Tempo inicial para cada equipamento
    public string[] TempoCicloEquip;
    public string[] TempoHUDEquipamentos;      //Tempos de operaçao no HUD
    public string[] TempoEquipamentosInicial;  //Tempos inicial de operaçao no HUD
    public bool HUDChanged;                    //O HUD foi modificado
    public int ContHUDChange;                  //Contador de atualizaçao
    public float TimeHUDChange;                //Tempo para o contador de atualizaçao

    //SENSORES
    public string[] Sensores;              //Nomes dos Sensores
    public string[] DescriçaoSensor;       //Descriçao dos Sensores
    public float[] ValoresAtuaisSensores;  //Ultimo valor para cada sensor
    public string[] TipoSensores;          //Tipo dos Sensores
    public string[] UnidadeSensores;       //Unidade das medidas dos Sensores
    public string[] UnidadeFaladaSensores; //Unidade falada das medidas dos Sensores
    public float[,,] ValoresSensores;      //Resultados dos sensores [sensor I] [intervalos de tempo J (10min, 1h, 1dia, 1semana)] [Tempos da mediçao K (10 seg, 1 min, 24 min, 168 min (2h48min))]
    public float[,,] TempoValoresSensores; //Escalas de tempo para os valores registrados
    public bool[] SensorPrincipal;
    public bool[] SensoresImportantes;
    //Intervalos utilizados
    public float TVerificarSensores = 30;
    public bool VerifiqueTempoAtualizacao = false;
    public float[] SensTempoUtimaAtualizacao;
    public float[] SensTempoDesdeUtimaAtualizacao;
    private float TAddResultsMark = 7f;
    private float TAddResults = 17f;
    private float TSendDataMark = 3f;
    public float TWatchBridge = 7f;
    public int SendDataCounter = 0;
    private float T10minMark = 10f;
    private float T10min = 10f;
    private float T1horMark = 60f;
    private float T1hor = 60f;
    private float T1diaMark = 1440f;
    private float T1dia = 1440f;
    private float T1semMark = 10080f;
    private float T1sem = 10080f;
    public TextMesh[] TituloHUDSensores;    //Titulos dos Mostradores 3D do titulo
    public TextMesh[] ValorHUDSensores;     //Valor nos Mostradores 3D dos sensores
    public TextMesh[] UnidadeHUDSensores;   //Unidade dos Mostradores 3D dos sensores
    public GameObject[] Obj3DSensores;
    public TextMesh[] ValorObj3DSensores;   //Valor no objeto no indicador 3D
    public TextMesh[] StatusObj3DSensores;  //Status de cada sensor no indicador 3D
    public TextMesh[] TituloObj3DSensores;  //Titulo de cada sensor no indicador 3D
    public float[][] ValoresSensoresDesdeOInicio;  //Valores dos sensores desde o inicio (100 pontos)
    public float[] LimiteMinSensores;       // Limites minimos de segurança dos sensores
    public float[] LimiteMaxSensores;       // Limites maximos de segurança dos sensores

    public GameObject[] CircIndiSensores;  //Medidores circulares do sensor I
    public GameObject[] MedIndiSensores;  //Medidores circulares da media no sensor I

    public float[] ValoresMediaSensores; //media ao longo da ultima hora do sensor I
    public String[] MediaDataSensores;  //data da primeira mediçao da media
    public float[] ValoresMinSensores;  //valor minimo do sensor I desde o inicio
    public String[] MinDataSensores;    //data do momento de minimo
    public float[] ValoresMaxSensores;  //valor maximo do sensor I desde o inicio
    public String[] MaxDataSensores;    //data do momento de maximo
    public bool[] RelatFaladoSensorI;      //Utilizar relatorio falado para o sensor I

    public Texture Save_Icon;             //Textura disquete
    public Texture TexturaFundoGraph;
    public Texture LinhasoGraph;
    public Texture TexturaChart;
    public Texture TexturaIndicators;
    public float[,,] AxisXToPlot;
    public float[,,] AxisYToPlot;
    [HideInInspector]
    public float[] XInterp;
    [HideInInspector]
    public float[] YInterp;

    //REDE
    public string StatusRPC = "";
    public string testNetworkStatus = "";
    public string testNetworkMessage = "";
    public string shouldEnableNatMessage = "";

    //E-MAILS
    public int EmailsEnviados = 0;
    private string AssuntoEmail = "";
    private string CorpoEmail = "";
    public string[] destinatarios;
    public string[] AtachedFiles;

    public float FPScounter;
    public float updateInterval = 0.5f;
    private float accum = 0; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval

    public GameObject MainCam;
    public Text Alerta1;
    public Text Alerta2;
    public Boolean Alerta = false;
    public float TempoDeAtualizacao = 0.1f;
    private Color DefinedGuiColor = Color.gray;

    public GameObject[] HUDs;

    public float Tempo;
    public float TempoRelatorio = 30f;
    private float TempoRelatorioMark = 600f;

    public bool Chrono1Running;
    public float InicialTempoChrono1;
    public float TempoChrono1;
    public bool Chrono2Running;
    public float InicialTempoChrono2;
    public float TempoChrono2;

    //programa
    public Boolean Desligando = false;
    public Boolean Desligar = false;

    //Parar processo
    public Boolean PararTudo = false;

    // GUI
    private int PosicaoXNaTela;
    private int PosicaoYNaTela;

    public float TempoDeDesligamento = 3.0f;
    private float TempoDeDesligar;

    private Boolean Stats = true;
    private Boolean StatsAux = false;

    public bool Graficos = false;
    private int LarguraDoGrafico;
    public bool Console = false;
    private bool ConsoleAux = false;
    public bool WebCam = false;
    public Image[] WebCamTex;
    public bool BigWebCam = false;
    public bool SmallWebCam = true;
    public Texture TextureClose;
    public Texture TextureDisquete;
    public Texture TexturePlay;
    public Texture TexturePause;
    public Texture TextureZero;

    public string ConsoleString = "INICIANDO ";
    public bool DebugSerial = false;
    public bool AutoScroll = false;

    private string Abertura = "";

    public bool HUDoperacao = false;

    [HideInInspector]
    public Vector2 scrollPosition;
    //[HideInInspector]
    public Vector2 scrollGrafPosition;
    [HideInInspector]
    public Vector2 scrollEquipPosition;

    public string URLContent;

    //	IEnumerator GetTextWeb(string url) {
    //		WWW www = new WWW(url);
    //		yield return www;
    //		URLContent = www.text;
    ////		UnityEngine.Debug.Log(URLContent);
    //		var xmlDoc = new XmlDocument();
    //		xmlDoc.LoadXml(URLContent);
    //		UnityEngine.Debug.Log(xmlDoc.InnerText);
    //	}

    // START|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    void Start()
    {

        //Setando os audios estaticos
        StaticAudios = new AudioSource[Audios.Length];
        for (int i = 0; i < Audios.Length; i++)
        {
            StaticAudios[i] = Audios[i];
        }



        //Contador de fps
        timeleft = updateInterval;

        //Desempenho
        //Profiler.enabled = true;

        //Graficos dos sensores
        LarguraDoGrafico = Mathf.RoundToInt(0.41f * (float)Screen.width - 35f);

        //Criar Pastas
        if (!Directory.Exists(Application.dataPath + "/Screenshots"))
        {
            System.IO.Directory.CreateDirectory(Application.dataPath + "/Screenshots");
        }
        //Speak
        if (Directory.Exists(Application.dataPath + "/TempSpeak"))
        {
            DeleteFolderContent(Application.dataPath + "/TempSpeak");
        } //Limpa tudo que houver}
        if (!Directory.Exists(Application.dataPath + "/TempSpeak"))
        {
            System.IO.Directory.CreateDirectory(Application.dataPath + "/TempSpeak");
        }
        //Resultados
        if (!Directory.Exists(Application.dataPath + "/Results"))
        {
            System.IO.Directory.CreateDirectory(Application.dataPath + "/Results");
        }
        String Results = Application.dataPath + "/Results/Resultados.txt";
        if (!File.Exists(Results)) { File.Create(Results); }
        //Criar arquivos
        String Log = Application.dataPath + "/Log.txt";
        if (!File.Exists(Log)) { File.Create(Log); }

        //Iniciando console
        ConsoleString = "INICIANDO CONSOLE" + "  " + System.DateTime.Now.ToString("dd-MM-yyyy");
        //Iniciando Log
        AddText(Log, "");
        AddText(Log, "[PROGRAMA INICIADO]");
        AddText(Log, "");
        //Iniciando Resultados
        AddText(Results, "");
        AddText(Results, "[PROGRAMA INICIADO]");
        AddText(Results, "");
        AddText(Results, ShowArrayStrings(Sensores) + "   " + ShowArrayStrings(Equipamentos));
        AddText(Results, "");
        //Iniciando Eva

        //Tentando se conectar ao arduino
        if (PlayerPrefs.GetString("Mode") == "LocalControl") { ConnectToArduino(true); }
        if (PlayerPrefs.GetString("Mode") == "RemoteControl") { ConnectToArduino(false); RemoteControl = true; }
        if (PlayerPrefs.GetString("Mode") == "Simulation") { ConnectToArduino(false); Simulation = true; }

        //Testar conexao com a internet
        if (CheckForInternetConnection())
        {
            UnityEngine.Debug.Log("Computador conectado com a internet");
            AddText(Log, "Computador conectado com a internet");
            ConsoleString += "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + "  " + "Computador conectado com a internet";
        }
        if (!CheckForInternetConnection())
        {
            UnityEngine.Debug.Log("Computador desconectado com a internet");
            AddText(Log, "Internet indisponivel");
            ConsoleString += "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + "  " + "Internet indisponivel";
            Abertura += "A internet está indisponivel. verifique a conexão ...";
        }

        //setando os indicadores 3D
        for (int i = 0; i < Equipamentos.Length; i++)
        {
            int k = i + 1;
            Titulo3DIndEquipamentos[i].text = Equipamentos[i] + " " + k.ToString("D2");  //titulos dos indicadores 3D
            Status3DIndEquipamentos[i].text = StatusEquipString[i];  //Status dos indicadores 3D
            Info1Ind3DEquipamentos[i].text = "";  //Informaçao adicional 1 dos indicadores 3D
            Info2Ind3DEquipamentos[i].text = "";
        }

        //Tempo inicial de envio dos e-mails
        TempoRelatorioMark = TempoRelatorio;
        //Arquivos anexados
        AtachedFiles[0] = "Log.txt";
        AtachedFiles[1] = "Results/Resultados.txt";

        //Atribuir titulos e as unidades aos sensores
        for (int i = 0; i < Sensores.Length; i++)
        {
            TituloHUDSensores[i].text = Sensores[i] + " " + i.ToString("D2");
            UnidadeHUDSensores[i].text = UnidadeSensores[i];
        }

        //Desligando tudo
        if (!Simulation && !RemoteControl)
        {
            if (sp.IsOpen)
            {
                for (int i = 0; i < Equipamentos.Length; i++)
                {
                    StartCoroutine(ReliableSetEquipment("e" + i.ToString("D2") + "000")); //Codigo e=equipamento, numero do equipamento com dois digitos, "d" de desligado (alterar codigo no arduino)
                }
            }
        }

        //Preenchimento com os dados estatisticos iniciais
        for (int i = 0; i < Sensores.Length; i++)
        {
            ValoresMediaSensores[i] = 0.5f * LimiteMinSensores[i] + 0.5f * LimiteMaxSensores[i];
            ValoresMinSensores[i] = LimiteMaxSensores[i];
            ValoresMaxSensores[i] = LimiteMinSensores[i];
        }

        //Preenchimento dos estados dos relatorios
        for (int i = 0; i < Sensores.Length; i++)
        {
            RelatFaladoSensorI[i] = false;
        }

        //Prenchendo as corrotinas dos equipamentos
        CorrotinasEquipamentos = new Coroutine[Equipamentos.Length];
        for (int i = 0; i < Equipamentos.Length; i++)
        {
            CorrotinasEquipamentos[i] = null;
        }

        XInterp = new float[LarguraDoGrafico - 120];
        YInterp = new float[LarguraDoGrafico - 120];
        ValoresSensores = new float[Sensores.Length, 4, 60];
        TempoValoresSensores = new float[Sensores.Length, 4, 60];
        for (int l = 0; l < Sensores.Length; l++)
        {
            for (int m = 0; m < 4; m++)
            {
                for (int n = 0; n < 60; n++)
                {
                    TempoValoresSensores[l, m, n] = 0.1667f * (float)n;
                    ValoresSensores[l, m, n] = LimiteMinSensores[l];
                }
            }
        }
        float num2 = (TempoValoresSensores[1, 1, TempoValoresSensores.GetLength(2) - 1] - TempoValoresSensores[1, 1, 0]) / (float)(LarguraDoGrafico - 120 - 1);
        AxisXToPlot = new float[Sensores.Length, 4, LarguraDoGrafico - 120];
        AxisYToPlot = new float[Sensores.Length, 4, LarguraDoGrafico - 120];
        for (int num3 = 0; num3 < Sensores.Length; num3++)
        {
            for (int num4 = 0; num4 < 4; num4++)
            {
                for (int num5 = 0; num5 < LarguraDoGrafico - 120; num5++)
                {
                    AxisXToPlot[num3, num4, num5] = (float)num5 * num2;
                    AxisYToPlot[num3, num4, num5] = LimiteMinSensores[num3];
                }
            }
        }

        Speak(Abertura);

    }

    // UPDATE|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    void Update()
    {

        //string bridge = Application.dataPath + "/Eva/bridge.ini";
        //AP_INIFile ini = new AP_INIFile(bridge);

        if (!Simulation && !RemoteControl)
        {
            if (!sp.IsOpen)
            {
                OpenConnection();
            }
        }

        String Results = Application.dataPath + "/Results/Resultados.txt";
        String Log = Application.dataPath + "/Log.txt";

        //Ler entradas de sensores
        if (!RemoteControl && !Simulation)
        {
            if (sp.IsOpen)
            {
                try
                {
                    COMPortResult = sp.ReadLine();
                }
                catch
                {
                }
            }
        }

        //Gerador de dados simulados
        if (Simulation)
        {
            if (Time.time >= Tempo)
            {
                int l = PassoCounter;
                //DadoSimulado=Sensores [l] + l.ToString ("D2")+"="+((LimiteMaxSensores[l]-LimiteMinSensores[l])*0.25f*(Mathf.Cos (0.001f*(l+1)*Tempo)+1)*(Mathf.Sin(0.005f*Tempo)+1f)/(l+1)+UnityEngine.Random.Range (-0.01f*(LimiteMaxSensores[l]-LimiteMinSensores[l]), 0.01f*(LimiteMaxSensores[l]-LimiteMinSensores[l]))).ToString();
                //DadoSimulado=Sensores [l] + l.ToString ("D2")+"="+(LimiteMaxSensores[l]-LimiteMinSensores[l])*(1f-Mathf.Exp(-0.005f*Tempo))+UnityEngine.Random.Range (-0.01f*(LimiteMaxSensores[l]-LimiteMinSensores[l]), 0.01f*(LimiteMaxSensores[l]-LimiteMinSensores[l]))).ToString();
                //DadoSimulado=Sensores [l] + l.ToString ("D2")+"="+((LimiteMaxSensores[l]-LimiteMinSensores[l])*1.5f*Mathf.Sin(0.01f*Tempo)+UnityEngine.Random.Range (-0.1f*(LimiteMaxSensores[l]-LimiteMinSensores[l]), 0.1f*(LimiteMaxSensores[l]-LimiteMinSensores[l]))).ToString();
                //DadoSimulado=Sensores [l] + l.ToString ("D2")+"="+(LimiteMinSensores[l]+(LimiteMaxSensores[l]-LimiteMinSensores[l])*1.0f*(1f-Mathf.Exp(-0.05f*Tempo))+UnityEngine.Random.Range (-0.01f*(LimiteMaxSensores[l]-LimiteMinSensores[l]), 0.01f*(LimiteMaxSensores[l]-LimiteMinSensores[l]))).ToString();

                if (PassoCounter != 9 && PassoCounter != 6 && PassoCounter != 7 && PassoCounter != 8 && PassoCounter != 10)
                {
                    DadoSimulado = Sensores[l] + l.ToString("D2") + "=" + ((LimiteMinSensores[l] + LimiteMaxSensores[l]) / 2f).ToString();
                }

                if (PassoCounter == 4)
                {
                    DadoSimulado = Sensores[l] + l.ToString("D2") + "=" + (LimiteMinSensores[l] + (LimiteMaxSensores[l] - LimiteMinSensores[l]) * (ValoresEquipamentos[7] / LimiteMaxEquipamentos[7]) + 0.011f).ToString();
                }
                if (PassoCounter == 5)
                {
                    DadoSimulado = Sensores[l] + l.ToString("D2") + "=" + (LimiteMinSensores[l] + (LimiteMaxSensores[l] - LimiteMinSensores[l]) * (ValoresEquipamentos[6] / LimiteMaxEquipamentos[6]) + 0.011f).ToString();
                }

                PassoCounter += 1;
            }
            if (PassoCounter > Sensores.Length - 1) { PassoCounter = 0; }
        }


        //Arduino conectado
        if (!Simulation && Arduino)
        {
            GrabResult(COMPortResult);//distribuir resultados em seus devidos lugares
        }
        //Simulaçao habilitada
        if (Simulation)
        {
            GrabResult(DadoSimulado);//distribuir resultados em seus devidos lugares
        }
        //Controle Remoto habilitado
        if (RemoteControl)
        {
            GrabResult(RemoteData);//distribuir resultados em seus devidos lugares
        }


        //UnityEngine.Debug.Log (COMPortResult);

        if (!Simulation && !Arduino && String.Equals(COMPortResult, "Arduino"))
        {//receber ex: Termometro01 (alterar codigo no arduino)
            Speak("confirmaçao efetuada, arduino conectado a porta " + ports[ports.Length - 1].ToString() + "");
            AddText(Log, "confirmaçao efetuada, arduino conectado a porta " + ports[ports.Length - 1].ToString());
            ConsoleString += "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + "  " + "confirmaçao efetuada, arduino conectado a porta " + ports[ports.Length - 1].ToString();
            scrollPosition += new Vector2(0, 15000);
            ArduinoPort = COMPortNumber(ports[ports.Length - 1]).ToString();
            Arduino = true;
        }

        //Status da conexao para o console e para o Log
        if (testNetworkStatus != "")
        {
            AddText(Log, testNetworkStatus);
            AddText(Log, testNetworkMessage);
            AddText(Log, shouldEnableNatMessage);
            ConsoleString += "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + "  " + testNetworkStatus;
            ConsoleString += "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + "  " + testNetworkMessage;
            ConsoleString += "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + "  " + shouldEnableNatMessage;
            scrollPosition += new Vector2(0, 15000);
            testNetworkStatus = "";
        }

        //Atualizaçao dos dados a cada 17 segundos

        if (Time.time >= TAddResultsMark)
        {
            //Adiciona resultados em resultados.txt
            if (Arduino || RemoteControl)
            {
                AddText(Results, (ShowArray(ValoresAtuaisSensores) + "   " + ShowArray(PorcentagemEquipamentos)).Replace(".", ","));
            }
            //Verifica status da conexao com o arduino
            if (TempoConexao >= 16f)
            {
                ArduinoConectado = false;
            }
            if (Arduino & !ArduinoConectado)
            {
                Speak("Arduino desconectado. tentando se reconectar");
                if (sp.IsOpen) { sp.Close(); }
                OpenConnection();
            }

            TAddResultsMark = TAddResults + Time.time;
        }

        //verificar status dos sensores a cada 120 segundos
        if (Time.time >= TVerificarSensores)
        {
            if (VerifiqueTempoAtualizacao)
            {
                CheckSensorStatus();
            }
            //Lembretes pre programados
            if (Time.time >= 400f) { Lembretes(); }

            TVerificarSensores = 240f + Time.time;
        }

        // Envio de dados para os clientes a cada 1.1 segundo
        if (Time.time >= TSendDataMark)
        {
            if (Simulation || Arduino)
            {
                if (true)
                {//network is ok
                    int i = SendDataCounter;
                    //GetComponent<NetworkView>().RPC("SendData", RPCMode.Others,Sensores [i] + i.ToString ("D2")+"="+ValoresAtuaisSensores[i]);
                    //UnityEngine.Debug.Log(Sensores [i] + i.ToString ("D2")+"="+ValoresAtuaisSensores[i]);
                    SendDataCounter += 1;
                    if (SendDataCounter > Sensores.Length - 1) { SendDataCounter = 0; }
                }
            }

            //Atualiza o tempo de funcionamento dos sensores
            for (int i = 0; i < Sensores.Length; i++)
            {
                SensTempoDesdeUtimaAtualizacao[i] = Time.time - SensTempoUtimaAtualizacao[i];
            }
            //Atualiza Status da conexao com o arduino
            TempoConexao += 1.1f;

            //resultados calculados em funçao de outras variaveis
            ResultadosCalculados();

            TSendDataMark = 1.1f + Time.time;
        }

        //Verificar o Bridge a cada 0,7 segundos
        if (Time.time >= TWatchBridge)
        {
            //StartCoroutine(BridgeWatcher());
            TWatchBridge += 0.7f;

            //Atualiza indicador de media
            for (int j = 0; j < ValoresSensores.GetLength(0); j++)
            {
                float ValuePercentMedia = (Media(PartOfArray(AxisYToPlot, j, 0)) - LimiteMinSensores[j]) / (LimiteMaxSensores[j] - LimiteMinSensores[j]);
                if (!float.IsNaN(ValuePercentMedia) && !float.IsInfinity(ValuePercentMedia))
                {
                    MedIndiSensores[j].transform.localEulerAngles = new Vector3(0.0f, 180.0f, 360f * ValuePercentMedia);
                }
            }

        }


        if (Time.time >= T10minMark)
        {
            for (int j = 0; j < ValoresSensores.GetLength(0); j++)
            {
                for (int k = 0; k < ValoresSensores.GetLength(2) - 1; k++)
                {
                    ValoresSensores[j, 0, k] = ValoresSensores[j, 0, k + 1];
                }
                ValoresSensores[j, 0, ValoresSensores.GetLength(2) - 1] = ValoresAtuaisSensores[j];
                InterpolationToPlot(j, 0, TempoValoresSensores, ValoresSensores, AxisXToPlot, AxisYToPlot, out AxisXToPlot, out AxisYToPlot);
            }

            //Tudo pronto?--------------------------------------------------------------
            if (!ReadToGOMark && Time.time > 100f && Arduino && ArduinoConectado)
            {
                int LocalErrorCounter = 0;
                for (int i = 0; i < Sensores.Length; i++)
                {
                    if (SensTempoDesdeUtimaAtualizacao[i] > 50f)
                    {
                        LocalErrorCounter += 1;
                    }
                }
                if (LocalErrorCounter == 0)
                {
                    Speak("Prerrequisitos de operação e automação atingidos. a coluna está pronta para operar");
                    ReadToGO = true;
                    ReadToGOMark = true;
                }
                if (LocalErrorCounter > 0)
                {
                    Speak("Temos " + LocalErrorCounter.ToString() + " sensores com mal funcionamento.");
                    ReadToGOMark = true;
                }
            }

            T10minMark = T10min + Time.time;
        }
        if (Time.time >= T1horMark)
        {
            for (int l = 0; l < ValoresSensores.GetLength(0); l++)
            {
                for (int m = 0; m < ValoresSensores.GetLength(2) - 1; m++)
                {
                    ValoresSensores[l, 1, m] = ValoresSensores[l, 0, m + 1];
                }
                ValoresSensores[l, 1, ValoresSensores.GetLength(2) - 1] = ValoresAtuaisSensores[l];
            }
            //Relatorios das variaveis importantes

            for (int i = 0; i < Sensores.Length; i++)
            {
                if (SensorPrincipal[i] && Time.time > 3.1f * T1hor)
                {
                    if (Arduino || Simulation)
                    {
                        StartCoroutine(RelatorioSensor(i, "previsao"));
                    }
                }
            }

            T1horMark = T1hor + Time.time;
        }
        if (Time.time >= T1diaMark)
        {
            for (int n = 0; n < ValoresSensores.GetLength(0); n++)
            {
                for (int num3 = 0; num3 < ValoresSensores.GetLength(2) - 1; num3++)
                {
                    ValoresSensores[n, 2, num3] = ValoresSensores[n, 0, num3 + 1];
                }
                ValoresSensores[n, 2, ValoresSensores.GetLength(2) - 1] = ValoresAtuaisSensores[n];
            }

            Speak("Aconselho a checagem visual completa da coluna");

            T1diaMark = T1dia + Time.time;
        }
        if (Time.time >= T1semMark)
        {
            for (int num4 = 0; num4 < ValoresSensores.GetLength(0); num4++)
            {
                for (int num5 = 0; num5 < ValoresSensores.GetLength(2) - 1; num5++)
                {
                    ValoresSensores[num4, 3, num5] = ValoresSensores[num4, 0, num5 + 1];
                }
                ValoresSensores[num4, 3, ValoresSensores.GetLength(2) - 1] = ValoresAtuaisSensores[num4];
            }
            T1semMark = T1sem + Time.time;
        }

        //Relatorio falado se clicado no indicador do sensor
        for (int i = 0; i < Sensores.Length; i++)
        {
            if (RelatFaladoSensorI[i])
            {
                string FalaTemp = "";
                FalaTemp = "No " + Sensores[i] + " " + i.ToString("D2") + " " + DescriçaoSensor[i] + " a " + TipoSensores[i] + "é de " + ValoresAtuaisSensores[i].ToString("F2") + " " + UnidadeFaladaSensores[i] + "...";
                if (Media(PartOfArray(AxisYToPlot, i, 0)) < LimiteMaxSensores[i] && Media(PartOfArray(AxisYToPlot, i, 0)) > LimiteMinSensores[i]) { FalaTemp += ". A " + TipoSensores[i] + " média está dentro dos limites previstos" + "..."; }
                if (Media(PartOfArray(AxisYToPlot, i, 0)) < LimiteMinSensores[i]) { FalaTemp += ". A " + TipoSensores[i] + " média está abaixo do limite inferior. a variável está fora do controle" + "..."; }
                if (Media(PartOfArray(AxisYToPlot, i, 0)) > LimiteMaxSensores[i]) { FalaTemp += ". A " + TipoSensores[i] + " média está acima do limite superior. a variável está fora do controle" + "..."; }

                if (ValoresMaxSensores[i] < LimiteMinSensores[i]) { FalaTemp += ". A " + TipoSensores[i] + " máxima está abaixo do limite inferior. a variável está fora do controle" + "..."; }
                if (ValoresMaxSensores[i] > LimiteMaxSensores[i]) { FalaTemp += ". A " + TipoSensores[i] + " máxima está acima do limite superior." + "..."; }

                if (ValoresMinSensores[i] > LimiteMaxSensores[i]) { FalaTemp += ". A " + TipoSensores[i] + " mínima está acima do limite superior. a variável está fora do controle" + "..."; }
                if (ValoresMinSensores[i] < LimiteMinSensores[i]) { FalaTemp += ". A " + TipoSensores[i] + " mínima está abaixo do limite inferior." + "..."; }

                if (TaxaFinal(PartOfArray(TempoValoresSensores, i, 0), PartOfArray(ValoresSensores, i, 0)) > 0.1f) { FalaTemp += ". seu valor está aumentando" + "..."; }
                if (TaxaFinal(PartOfArray(TempoValoresSensores, i, 0), PartOfArray(ValoresSensores, i, 0)) > -0.1f && TaxaFinal(PartOfArray(TempoValoresSensores, i, 0), PartOfArray(ValoresSensores, i, 0)) < 0.1f) { FalaTemp += ". seu valor está se mantendo estável" + "..."; }
                if (TaxaFinal(PartOfArray(TempoValoresSensores, i, 0), PartOfArray(ValoresSensores, i, 0)) < -0.1f) { FalaTemp += ". seu valor está diminuindo" + "..."; }

                Speak(FalaTemp);
                RelatFaladoSensorI[i] = false;

            }
        }
        //RELATORIO ENVIADO A CADA INTERVALO "TempoRelatorio"
        if (Time.time >= TempoRelatorioMark)
        {
            EnviarResultadosViaEmail();
            TempoRelatorioMark = TempoRelatorio + Time.time;
        }

        if (Time.time >= Tempo)
        { //A cada decimo de segundo

            //Equipamento i a cada Tempo de atualização
            for (int i = 0; i < Equipamentos.Length; i++)
            {
                if (StatusEquipamentos[i])
                {
                    TempoEquipamentos[i] = Mathf.RoundToInt(100.0f * (float.Parse(TempoHUDEquipamentos[i]) - TempoDeAtualizacao)) / 100.0f;
                    TempoHUDEquipamentos[i] = TempoEquipamentos[i].ToString();
                    if (TempoEquipamentos[i] <= 0)
                    {
                        //if (Network.isServer){GetComponent<NetworkView>().RPC("LigaDesligaEquipamento", RPCMode.Others, i,0);}
                        ValoresEquipamentos[i] = 0f;
                        MarkStatusEquipamentos[i] = true;
                        TempoEquipamentos[i] = 0.0f;
                        TempoHUDEquipamentos[i] = Mathf.RoundToInt(TempoEquipamentos[i]).ToString();
                        Speak(Equipamentos[i] + " " + i.ToString("D2") + " " + DescricaoEquipamentos[i] + " desligado depois de " + IntuitiveTime(float.Parse(TempoEquipamentosInicial[i])));
                        AddText(Log, Equipamentos[i] + " " + i.ToString("D2") + " desligado apos: " + TempoEquipamentosInicial[i] + " seg");
                        ConsoleString += "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + "  " + Equipamentos[i] + " " + i.ToString("D2") + " desligado apos: " + TempoEquipamentosInicial[i] + " seg";
                        scrollPosition += new Vector2(0, 15000);
                    }
                }
            }

            //Desabilita o Alerta (GUI.text) quando 'Alerta' = false
            if (!Alerta)
            {
                Alerta1.enabled = false;
                Alerta2.enabled = false;
            }

            Tempo = TempoDeAtualizacao + Time.time;

        }

        //Aplica modificaçoes quando o HUD e modificado-------------------------------------------------------------------------------------------------
        if (HUDChanged)
        {

            if (Time.time >= TimeHUDChange)
            {
                if (true)
                {//Network.isServer || Network.isClient
                 //GetComponent<NetworkView> ().RPC ("UpdateCicloEquip", RPCMode.Others, TempoCicloEquip[ContHUDChange], ContHUDChange);
                 //GetComponent<NetworkView> ().RPC ("UpdateEquipTime", RPCMode.Others, TempoHUDEquipamentos[ContHUDChange], ContHUDChange);
                }
                ContHUDChange += 1;
                TimeHUDChange = Time.time + 0.1f;
                if (ContHUDChange > Equipamentos.Length - 1)
                {
                    HUDChanged = false;
                    ContHUDChange = 0;

                    //Atualiza valores do slider e usa como gatilho para ativar o equipamento (Importante)
                    for (int i = 0; i < Equipamentos.Length; i++)
                    {
                        if (ValoresAntigosEquipamentos[i] != ValoresEquipamentos[i])
                        {
                            //if (Network.isServer || Network.isClient){GetComponent<NetworkView>().RPC("LigaDesligaEquipamento", RPCMode.Others, i, Mathf.RoundToInt(ValoresEquipamentos[i]));}
                            MarkStatusEquipamentos[i] = true;
                        }
                    }

                }
            }
        }

        //Batelada
        if (MarkBatelada && !Batelada)
        {
            TempoInicioBatelada = Tempo;
            Batelada = true;
            MarkBatelada = false;
        }
        if (MarkBatelada && Batelada)
        {
            Batelada = false;
            PararTudo = true;
            MarkBatelada = false;
        }

        if (Batelada)
        {
            for (int i = 0; i < Equipamentos.Length; i++)
            {
                if (Tempo - TempoInicioBatelada >= float.Parse(TempoInicialEquip[i]) && !EquipConcluidoBatelada[i])
                {
                    MarkStatusEquipamentos[i] = true;
                    //if (Network.isServer || Network.isClient){GetComponent<NetworkView>().RPC("LigaDesligaEquipamento", RPCMode.Others, i,LimiteMaxEquipamentos[i]);}
                    EquipConcluidoBatelada[i] = true;
                }
            }
        }

        //Desligando Alarme
        if (Tempo > 30)
        {
            MainCam.GetComponent<AudioSource>().mute = true;
        }

        for (int i = 0; i < Equipamentos.Length; i++)
        {
            //Ligar equipamento "I"	
            if (MarkStatusEquipamentos[i] && ValoresEquipamentos[i] != 0f)
            {
                StatusEquipamentos[i] = true;
                if ((Arduino || Simulation) && EquipamentosLargePWM[i])
                {
                    if (CorrotinasEquipamentos[i] != null) { StopCoroutine(CorrotinasEquipamentos[i]); }
                    CorrotinasEquipamentos[i] = StartCoroutine(LargePWM(i, ValoresEquipamentos[i]));
                }
                if ((Arduino || Simulation) && EquipamentosModulados[i]) { StartCoroutine(ReliableSetEquipment("e" + i.ToString("D2") + Mathf.RoundToInt(ValoresEquipamentos[i]).ToString("D3"))); }
                //UnityEngine.Debug.Log ("e" + i.ToString ("D2") + Mathf.RoundToInt(ValoresEquipamentos[i]).ToString("D3"));
                TempoEquipamentosInicial[i] = TempoHUDEquipamentos[i];
                MarkStatusEquipamentos[i] = false;
                if (ValoresAntigosEquipamentos[i] == 0f) { Speak(Equipamentos[i] + " " + i.ToString("D2") + " " + DescricaoEquipamentos[i] + " ligado"); }
                AddText(Log, Equipamentos[i] + " " + i.ToString("D2") + " ligado");
                ConsoleString += "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + "  " + Equipamentos[i] + " " + i.ToString("D2") + " ligado a " + ((ValoresEquipamentos[i] / LimiteMaxEquipamentos[i]) * 100f).ToString("F2") + "%";
                scrollPosition += new Vector2(0, 15000);

                Status3DIndEquipamentos[i].text = "Ligado";
                Info1Ind3DEquipamentos[i].text = "";
                Info2Ind3DEquipamentos[i].text = " ";

                TextButtonEquip[i] = "Desligar";
                StatusEquipString[i] = "Ligada";

                ValoresAntigosEquipamentos[i] = ValoresEquipamentos[i];
            }

            //Desligar equipamento "I"		
            if (MarkStatusEquipamentos[i] && StatusEquipamentos[i] && ValoresEquipamentos[i] == 0f)
            {
                StatusEquipamentos[i] = false;
                RealStatusPWM[i] = false;
                if (CorrotinasEquipamentos[i] != null) { StopCoroutine(CorrotinasEquipamentos[i]); }
                if (Arduino || Simulation) { StartCoroutine(ReliableSetEquipment("e" + i.ToString("D2") + "000")); }
                MarkStatusEquipamentos[i] = false;
                Speak(Equipamentos[i] + " " + i.ToString("D2") + " " + DescricaoEquipamentos[i] + " desligado");
                AddText(Log, Equipamentos[i] + " " + i.ToString("D2") + " Desligada");
                ConsoleString += "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + "  " + Equipamentos[i] + " " + i.ToString("D2") + " Desligada";
                scrollPosition += new Vector2(0, 15000);

                Status3DIndEquipamentos[i].text = "Desligado";
                Info1Ind3DEquipamentos[i].text = "";
                Info2Ind3DEquipamentos[i].text = " ";

                TextButtonEquip[i] = "ligar";
                StatusEquipString[i] = "Desligada";

                ValoresAntigosEquipamentos[i] = ValoresEquipamentos[i];
            }
        }

        /////////////////////////

        //Clipboard
        StartCoroutine(WatchClipboard());
        StartCoroutine(PauseSpeakTimer(10f));

        /////////////////////////

        //Estatísticas

        if (Input.GetKeyDown("f12"))
        {
            StatsAux = true;
            if (!Stats && StatsAux)
            {
                Speak("Mostrando estatísticas do programa");
                Stats = true; StatsAux = false;
            }
            if (Stats && StatsAux)
            {
                Speak("Desabilitando estatísticas do programa");
                Stats = false; StatsAux = false;
            }
        }

        //Console via botões
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            ConsoleAux = true;
            if (!Console && ConsoleAux) { Console = true; ConsoleAux = false; }
            if (Console && ConsoleAux) { Console = false; ConsoleAux = false; }
        }


        //Desligando tudo
        if (PararTudo)
        {
            //Desligando emergencialmente
            if (Arduino)
            {
                for (int i = 0; i < Equipamentos.Length; i++)
                {
                    if (CorrotinasEquipamentos[i] != null) { StopCoroutine(CorrotinasEquipamentos[i]); }
                    if (StatusEquipamentos[i]) StartCoroutine(ReliableSetEquipment("e" + i.ToString("D2") + "000")); //Codigo e=equipamento, numero do equipamento com dois digitos, "d" de desligado (alterar codigo no arduino)
                }
            }
            ConsoleString += "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + "  " + "Desligando os equipamentos...";
            scrollPosition += new Vector2(0, 15000);
            AddText(Log, "Desligando os equipamentos...");
            Speak("Desligando os equipamentos...");
            for (int i = 0; i < Equipamentos.Length; i++)
            {
                if (StatusEquipamentos[i]) { MarkStatusEquipamentos[i] = true; ValoresEquipamentos[i] = 0f; }
                //if ((Network.isServer || Network.isClient) && StatusEquipamentos[i]){GetComponent<NetworkView>().RPC("LigaDesligaEquipamento", RPCMode.Others, i, Mathf.RoundToInt(ValoresEquipamentos[i]));}
                ConsoleString += "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + "  " + Equipamentos[i] + " " + i.ToString("D2") + " Desligado";
                AddText(Log, Equipamentos[i] + " " + i.ToString("D2") + " Desligado");
            }

            PararTudo = false;
        }


        if (Desligar)
        {

            if (Desligando)
            {
                Speak("Fechando o programa...");
                TempoDeDesligar = Time.time + TempoDeDesligamento;
                PararTudo = true;
                Desligando = false;
            }

            if (Time.time >= TempoDeDesligar)
            {
                Application.Quit();
            }

        }


        //Contador de FPS
        if (Stats)
        {
            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            ++frames;

            // Interval ended - update GUI text and start new interval
            if (timeleft <= 0.0)
            {
                // display two fractional digits (f2 format)
                FPScounter = accum / frames;
                timeleft = updateInterval;
                accum = 0.0F;
                frames = 0;
            }
        }

    }
    //ONGUI..............................................................................................................................................................................................
    void OnGUI()
    {

        if (Tempo > 3f)
        {

            PosicaoXNaTela = Mathf.RoundToInt(0.02f * Screen.width);
            PosicaoYNaTela = Mathf.RoundToInt(0.28f * Screen.height);

            //Graficos com ScrollBar
            if (Graficos)
            {
                //GUI.Box(new Rect(0.25f*Screen.width, 0.27f*Screen.height, 660, 620),"");
                GUILayout.BeginArea(new Rect(0.25f * Screen.width + 10, 0.27f * Screen.height + 10, 0.41f * Screen.width + 5, 0.69f * Screen.height - 50));
                scrollGrafPosition = GUILayout.BeginScrollView(scrollGrafPosition, GUILayout.Width(0.41f * Screen.width - 10), GUILayout.Height(0.69f * Screen.height - 60));
                for (int i = 0; i < Sensores.Length; i++)
                {
                    if (scrollGrafPosition.y <= 560 * (i + 1) && scrollGrafPosition.y >= 360 * (i - 1))
                    {//otimizaçao: so desenhe o que e mostrado
                        if (LimiteMinSensores[i] < ValoresMinSensores[i])
                        {
                            DisplayDebugGUI("Resultado do " + Sensores[i] + " " + i.ToString("D2"), "Tempo (m)", "Valor\n" + "(" + UnidadeSensores[i] + ")", 0, 40 + 480 * i, LarguraDoGrafico, 260, PartOfArray(AxisXToPlot, i, 0), PartOfArray(AxisYToPlot, i, 0), LinhasoGraph, TexturaFundoGraph, TexturaChart, LimiteMinSensores[i], ValoresMaxSensores[i], Color.white, Color.red, 0.6f * Color.blue + 0.3f * Color.green + 0.1f * Color.white);
                        }
                        if (LimiteMinSensores[i] > ValoresMinSensores[i])
                        {
                            DisplayDebugGUI("Resultado do " + Sensores[i] + " " + i.ToString("D2"), "Tempo (m)", "Valor\n" + "(" + UnidadeSensores[i] + ")", 0, 40 + 480 * i, LarguraDoGrafico, 260, PartOfArray(AxisXToPlot, i, 0), PartOfArray(AxisYToPlot, i, 0), LinhasoGraph, TexturaFundoGraph, TexturaChart, ValoresMinSensores[i], ValoresMaxSensores[i], Color.white, Color.red, 0.6f * Color.blue + 0.3f * Color.green + 0.1f * Color.white);
                        }
                        DisplaySensorStats(i, "Dados do " + Sensores[i] + " " + i.ToString("D2"), 0, 340 + 480 * i, LarguraDoGrafico, 140, PartOfArray(TempoValoresSensores, i, 0), PartOfArray(ValoresSensores, i, 0), ValoresMaxSensores[i], ValoresMinSensores[i], TexturaIndicators);
                    }
                }
                GUILayout.Label("Graficos");
                for (int i = 0; i < 14f * Sensores.Length; i++)
                {
                    GUILayout.Label("\r\n");
                }
                GUILayout.EndScrollView();
                GUILayout.EndArea();
            }

            String Log = Application.dataPath + "/Log.txt";

            GUI.backgroundColor = Color.white;
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperLeft;
            centeredStyle.fontSize = 10 + Screen.width / 1000;

            var gs = GUI.skin.GetStyle("Button");
            gs.fontSize = 10 + Screen.width / 1000;

            GUI.color = Color.white;

            if (Desligar && Stats)
            {
                GUI.Label(new Rect(0.15f * Screen.width, 0.18f * Screen.height, 500, 40), "Desligando...");
            }

            //Mostrar estatisticas
            if (Stats)
            {
                GUI.contentColor = Color.blue + 0.8f * Color.white;  //Cor
                GUI.Label(new Rect(0.15f * Screen.width, 0.08f * Screen.height, 130, 30), System.DateTime.Now.ToString("hh.mm.ss"));
                GUI.Label(new Rect(0.15f * Screen.width, 0.1f * Screen.height, 130, 30), Time.time.ToString("f2") + " Segundos");
                if (FPScounter < 30)
                    GUI.contentColor = Color.yellow + 0.5f * Color.white;  //Cor
                else
                    if (FPScounter < 20)
                    GUI.contentColor = Color.red + 0.5f * Color.white;  //Cor
                else
                    GUI.contentColor = Color.green + 0.5f * Color.white;  //Cor
                GUI.Label(new Rect(0.15f * Screen.width, 0.12f * Screen.height, 130, 30), "FPS: " + FPScounter.ToString("f2"));
                GUI.contentColor = Color.blue + 0.8f * Color.white;  //Cor
                if (!Simulation && !RemoteControl)
                {
                    GUI.Label(new Rect(0.15f * Screen.width, 0.14f * Screen.height, 500, 40), ports[ports.Length - 1].ToString());
                    GUI.Label(new Rect(0.15f * Screen.width, 0.16f * Screen.height, 500, 40), COMPortResult);
                }
                if (Simulation)
                {
                    GUI.Label(new Rect(0.15f * Screen.width, 0.14f * Screen.height, 500, 40), "Modo simulaçao - arduino indisponivel");
                }
                //if(Network.isClient || Network.isServer){GUI.Label (new Rect (0.26f * Screen.width, 0.08f * Screen.height, 130, 30), " Supervisores: " + (Network.connections.Length+1).ToString());}
                //if(Network.isClient){GUI.Label (new Rect (0.26f * Screen.width, 0.1f * Screen.height, 130, 30), " Ping: " + Network.GetAveragePing(Network.connections[0])+" ms" );}

                //Cronometros
                //1
                if (!Chrono1Running)
                {
                    GUI.Label(new Rect(0.375f * Screen.width, 0.08f * Screen.height, 130, 30), FormatTime(TempoChrono1));
                    if (GUI.Button(new Rect(0.375f * Screen.width + 75, 0.08f * Screen.height, 20, 20), TexturePlay))
                    {
                        InicialTempoChrono1 = Tempo - TempoChrono1;
                        Chrono1Running = true;
                    }
                }
                if (Chrono1Running)
                {
                    TempoChrono1 = Time.time - InicialTempoChrono1;
                    GUI.Label(new Rect(0.375f * Screen.width, 0.08f * Screen.height, 130, 30), FormatTime(Time.time - InicialTempoChrono1));
                    if (GUI.Button(new Rect(0.375f * Screen.width + 75, 0.08f * Screen.height, 20, 20), TexturePause))
                    {
                        Chrono1Running = false;
                    }
                }

                if (GUI.Button(new Rect(0.375f * Screen.width + 100, 0.08f * Screen.height, 20, 20), TextureZero))
                {
                    TempoChrono1 = 0.0f;
                    Chrono1Running = false;
                }
                //2
                if (!Chrono2Running)
                {
                    GUI.Label(new Rect(0.375f * Screen.width, 0.12f * Screen.height, 130, 30), FormatTime(TempoChrono2));
                    if (GUI.Button(new Rect(0.375f * Screen.width + 75, 0.12f * Screen.height, 20, 20), TexturePlay))
                    {
                        InicialTempoChrono2 = Tempo - TempoChrono2;
                        Chrono2Running = true;
                    }
                }
                if (Chrono2Running)
                {
                    TempoChrono2 = Time.time - InicialTempoChrono2;
                    GUI.Label(new Rect(0.375f * Screen.width, 0.12f * Screen.height, 130, 30), FormatTime(Time.time - InicialTempoChrono2));
                    if (GUI.Button(new Rect(0.375f * Screen.width + 75, 0.12f * Screen.height, 20, 20), TexturePause))
                    {
                        Chrono2Running = false;
                    }
                }

                if (GUI.Button(new Rect(0.375f * Screen.width + 100, 0.12f * Screen.height, 20, 20), TextureZero))
                {
                    TempoChrono2 = 0.0f;
                    Chrono2Running = false;
                }

                //GUI.Label (new Rect (0.28f * Screen.width, 0.08f * Screen.height, 500, 40), "Uso da CPU: "+Profiler.GetMonoUsedSize);
                //GUI.Label (new Rect (0.28f * Screen.width, 0.1f * Screen.height, 500, 40), "Uso da RAM: "+ Profiler.GetTotalAllocatedMemory());
                //Feed test
                //				WebClient Cliente = new WebClient();
                //				GUI.Label (new Rect (0.35f * Screen.width, 0.12f * Screen.height, 130, 30),Cliente.DownloadString("https://scholar.google.com.br/scholar?hl=pt-BR&q=biodiesel&btnG=&lr="));

                GUI.contentColor = Color.white;  //Cor
            }

            //Console
            if (Console)
            {
                //GUI.Box(new Rect(0.25f*Screen.width, 0.27f*Screen.height, 0.41f*Screen.width, 0.69f*Screen.height),"");
                GUILayout.BeginArea(new Rect(0.25f * Screen.width + 10, 0.27f * Screen.height + 10, 0.41f * Screen.width + 5, 0.69f * Screen.height - 100));
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(0.41f * Screen.width - 2), GUILayout.Height(0.69f * Screen.height - 110));
                GUILayout.Label(ConsoleString);
                GUILayout.EndScrollView();
                GUILayout.EndArea();

                if (GUI.Button(new Rect(0.25f * Screen.width + 40, 0.96f * Screen.height - 40, 120, 20), "Limpar texto"))
                    ConsoleString = "";
                if (GUI.Button(new Rect(0.25f * Screen.width + 10, 0.96f * Screen.height - 40, 20, 20), TextureDisquete))
                {
                    ConsoleString += "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + "  " + "Log salvo em: " + Log;
                    scrollPosition += new Vector2(0, 15000);
                }
                AutoScroll = GUI.Toggle(new Rect(0.25f * Screen.width + 170, 0.96f * Screen.height - 70, 120, 20), AutoScroll, "  Autoscroll");
                DebugSerial = GUI.Toggle(new Rect(0.25f * Screen.width + 10, 0.96f * Screen.height - 70, 180, 20), DebugSerial, "  Debug Serial COM");
                Speaking = GUI.Toggle(new Rect(0.25f * Screen.width + 170, 0.96f * Screen.height - 40, 120, 20), Speaking, "  Relatorio falado");

                if (GUI.Button(new Rect(0.25f * Screen.width + 300, 0.96f * Screen.height - 40, 100, 20), "Enviar Email"))
                {
                    EnviarResultadosViaEmail();
                }
            }


            //HUD do usuário
            if (HUDoperacao)
            {
                //GUI.Box(new Rect(0.25f*Screen.width, 0.27f*Screen.height, 660, 620),"");
                GUILayout.BeginArea(new Rect(PosicaoXNaTela, PosicaoYNaTela, 0.21f * Screen.width, 0.69f * Screen.height - 40));
                scrollEquipPosition = GUILayout.BeginScrollView(scrollEquipPosition, GUILayout.Width(0.21f * Screen.width), GUILayout.Height(0.69f * Screen.height - 50));

                GUI.Label(new Rect(5, 25, 500, 40), "Ciclo");
                //Equipamentos
                for (int i = 0; i < Equipamentos.Length; i++)
                {
                    int k = i;
                    GUI.Label(new Rect(55, 25 + 65 * i, 500, 40), Equipamentos[i] + " " + k.ToString("D2") + " " + DescricaoEquipamentos[i]);
                    TempoCicloEquip[i] = GUI.TextField(new Rect(5, 45 + 65 * i, 45, 22), TempoCicloEquip[i], 25);
                    TempoHUDEquipamentos[i] = GUI.TextField(new Rect(55, 45 + 65 * i, 45, 22), TempoHUDEquipamentos[i], 25);
                    GUI.Label(new Rect(110, 45 + 65 * i, 40, 22), "seg");
                    if (GUI.Button(new Rect(135, 45 + 65 * i, 56, 22), TextButtonEquip[i]))
                    {
                        if (!StatusEquipamentos[i]) ValoresEquipamentos[i] = LimiteMaxEquipamentos[i];
                        if (StatusEquipamentos[i]) ValoresEquipamentos[i] = 0f;
                        //if (Network.isServer || Network.isClient){GetComponent<NetworkView>().RPC("LigaDesligaEquipamento", RPCMode.Others, i, Mathf.RoundToInt(ValoresEquipamentos[i]));}
                        MarkStatusEquipamentos[i] = true;
                    }
                    GUI.Label(new Rect(198, 45 + 65 * i, 70, 22), StatusEquipString[i]);
                    //indicador cores
                    GUI.color = 0.8f * Color.red + 0.2f * Color.white;
                    if (StatusEquipamentos[i]) { GUI.color = Color.green; }
                    GUI.DrawTexture(new Rect(250, 45 + 65 * i, 3, 44), TexturaChart);
                    GUI.DrawTexture(new Rect(0, 45 + 65 * i, 1, 44), TexturaChart);

                    GUI.color = 0.8f * Color.red + 0.2f * Color.white;
                    if (RealStatusPWM[i]) { GUI.color = Color.green; }
                    if (EquipamentosLargePWM[i]) GUI.DrawTexture(new Rect(256, 45 + 65 * i, 3, 44), TexturaChart);
                    GUI.color = Color.white;
                    //Slider
                    if (EquipamentosModulados[i] || EquipamentosLargePWM[i]) { ValoresEquipamentos[i] = GUI.HorizontalSlider(new Rect(5, 75 + 65 * i, 186, 30), ValoresEquipamentos[i], 0.0F, LimiteMaxEquipamentos[i]); }
                    if (!EquipamentosModulados[i]) { GUI.enabled = false; ValoresEquipamentos[i] = GUI.HorizontalSlider(new Rect(5, 75 + 65 * i, 186, 30), ValoresEquipamentos[i], 0.0F, LimiteMaxEquipamentos[i]); GUI.enabled = true; }
                    PorcentagemEquipamentos[i] = ValoresEquipamentos[i] * (100f / LimiteMaxEquipamentos[i]);
                    GUI.Label(new Rect(198, 70 + 65 * i, 40, 22), PorcentagemEquipamentos[i].ToString("f1") + " %");

                }

                GUILayout.Label("Equipamentos");
                for (int i = 0; i < 2 * Equipamentos.Length - 1; i++)
                {
                    GUILayout.Label("\r\n");
                }
                GUILayout.EndScrollView();
                GUILayout.EndArea();


                //habilita/desabilita Batelada
                //				if (!Batelada) {
                //					if (GUI.Button (new Rect (Screen.width * 0.015f, Screen.height * 0.91f, 100, 22), "Iniciar Batelada")){
                //						Speak ("Batelada iniciada");
                //						MarkBatelada = true;}
                //				}
                //				
                //				if (Batelada) {
                //					if (GUI.Button (new Rect (Screen.width * 0.015f, Screen.height * 0.91f, 100, 22), "Parar Batelada")){
                //						Speak ("Batelada interrompida");
                //						MarkBatelada = true;}
                //				}

            }

            DefinedGuiColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.white;

            //Alerta
            if (Alerta)
            {
                GUI.depth = 1;
                GUI.Box(new Rect(Screen.width * 0.5f - 240, Screen.height * 0.5f - 30, 480, 140), "");
                if (GUI.Button(new Rect(Screen.width * 0.5f - 75, Screen.height * 0.5f + 60, 150, 30), "OK"))
                    Alerta = false;
            }

            GUI.backgroundColor = DefinedGuiColor;

            //Fechar programa
            if (GUI.Button(new Rect(Screen.width - 10 - Screen.height / 25, Screen.height / 60, Screen.height / 24, Screen.height / 24), TextureClose))
            {
                Desligar = true;
                Desligando = true;
            }

            //Simulaçao
            //		if (!Simulation) {
            //			if (GUI.Button (new Rect (Screen.width - 118, 5, 70, 25), "Simulation")) {
            //				Simulation = true;
            //				Speak ("Modo simulaçao habilitado");
            //			}
            //		}
            //		if (Simulation && !RemoteControl) {
            //			if (GUI.Button (new Rect (Screen.width - 118, 5, 70, 25), "Simulation")) {
            //				if (ports.Length > 0) {
            //					Simulation = false;
            //					Speak ("Modo simulaçao desabilitado");
            //				}
            //				if (ports.Length == 0) {
            //					Speak ("O Modo simulação não pode ser desabilitado, Não há arduino conectado");
            //				}
            //			}
            //		}

            //Parar processo
            GUI.backgroundColor = Color.red;
            if (HUDoperacao)
            {
                if (GUI.Button(new Rect(Screen.width * 0.015f, Screen.height * 0.9f, Screen.width * 0.215f, 26), "PARAR PROCESSO"))
                {
                    PararTudo = true;
                }
            }

        }
        //O HUD foi modificado
        if (GUI.changed)
        {
            HUDChanged = true;
            ContHUDChange = 0;

        }
    }

    void OnApplicationQuit()
    {
        String Log = Application.dataPath + "/Log.txt";
        if (ports.Length > 0)
        {
            Speak("Programa fechado, até a próxima");
            ConsoleString += "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + " Programa fechado";
            AddText(Log, "Programa fechado");

            if (!Simulation && !RemoteControl) { sp.Close(); }
        }
        //if (Network.isClient || Network.isServer) {Network.Disconnect();}
    }

    public void ConnectToArduino(bool Connect)
    {
        String Log = Application.dataPath + "/Log.txt";

        // Get a list of serial port names.
        ports = SerialPort.GetPortNames();

        //Iniciando a conexao com o Arduino
        if (ports.Length > 0 && !Simulation)
        {
            if (COMPortNumber(ports[ports.Length - 1]) < 10)
            {
                sp = new SerialPort(ports[ports.Length - 1], 9600);
            }
            if (COMPortNumber(ports[ports.Length - 1]) >= 10)
            {
                sp = new SerialPort("\\\\.\\" + ports[ports.Length - 1], 9600);
            }
            Abertura += "Tentando se conectar a porta: " + ports[ports.Length - 1].ToString() + " ...";
        }
        if (Connect) { OpenConnection(); }
        if (ports.Length == 0)
        {
            UnityEngine.Debug.Log("Nao ha portas seriais abertas disponiveis, o Arduino esta desconectado");
            AddText(Log, "Arduino desconectado");
            ConsoleString += "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + "  " + "Arduino desconectado";
            Abertura += "Arduino desconectado ...";
        }
    }

    public void OpenConnection()
    {
        // Get a list of serial port names.
        ports = SerialPort.GetPortNames();

        if (sp != null)
        {
            if (sp.IsOpen)
            {
                sp.Close();
                UnityEngine.Debug.Log("Fechando Porta, pois ja esta aberta");
            }
            else
            {
                if (COMPortNumber(ports[ports.Length - 1]).ToString() == ArduinoPort || ArduinoPort == "")
                {
                    sp.Open();
                    sp.ReadTimeout = 1;
                    UnityEngine.Debug.Log("Porta Aberta!");
                    if (Arduino & !ArduinoConectado & ArduinoPort != "")
                    {
                        Speak("Arduino reconectado");
                    }
                }
            }
        }
        else
        {
            if (sp.IsOpen) { UnityEngine.Debug.Log("Porta Aberta!"); }
            else { UnityEngine.Debug.Log("Porta Nula"); }
        }
    }

    //Arquivos de texto
    //	private static void AddTextToTxt(FileStream fs, string value) 
    //	{
    //		byte[] info = new UTF8Encoding(true).GetBytes(value);
    //		fs.Write(info, 0, info.Length);
    //	}


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



    //Fala 
    public static void Speak(string texto)
    {
        if (Speaking)
        {
            if (ControlSpeaking)
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
        }
    }

    //Seta o Tempo "largo" de PWM dos equipamentos com Large PWM
    IEnumerator LargePWM(int i, float CycleDuty)
    {
        float CycleTime = float.Parse(TempoCicloEquip[i]);//Segundos
        float TimeOn = (CycleDuty * CycleTime) / LimiteMaxEquipamentos[i];
        float TimeOff = CycleTime - TimeOn;
        while (StatusEquipamentos[i])
        {
            //Ligar
            if (TimeOn > 0f)
            {
                RealStatusPWM[i] = true;
                if (Arduino) { sp.Write("e" + i.ToString("D2") + Mathf.RoundToInt(LimiteMaxEquipamentos[i]).ToString("D3")); }
                yield return new WaitForSeconds(TimeOn);
            }
            //Desligar
            if (TimeOff > 0f)
            {
                RealStatusPWM[i] = false;
                if (Arduino) { sp.Write("e" + i.ToString("D2") + "000"); }
                yield return new WaitForSeconds(TimeOff);
            }
        }
        //Desligar
        RealStatusPWM[i] = false;
        if (Arduino)
        {
            sp.Write("e" + i.ToString("D2") + "000");
        }
        //UnityEngine.Debug.Log("Equipamento "+i.ToString("D2")+" Desligado");
    }

    //Tempo formatado

    public string FormatTime(float TimeInSeconds)
    {

        TimeSpan t = TimeSpan.FromSeconds(TimeInSeconds);

        string Result = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                      t.Hours,
                                      t.Minutes,
                                      t.Seconds);
        return Result;

        //		string Result = "";
        //		int Seconds = 0;
        //		int Minutes = 0;
        //		int Hours = 0;
        //		Seconds = Mathf.RoundToInt(TimeInSeconds) % 60;
        //		Minutes = Mathf.RoundToInt(TimeInSeconds) % 360;
        //		Hours = Mathf.RoundToInt(TimeInSeconds) % 3640;
        //		return Result=Hours.ToString("D2")+":"+Minutes.ToString("D2")+":"+Seconds.ToString("D2");
    }

    //Tempo intuitivo
    public string IntuitiveTime(float TimeInSeconds)
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

    IEnumerator PauseSpeakTimer(float Seconds)
    {
        if (ClipboardHelper.clipBoard == "EvaMicOcu")
        {
            ControlSpeaking = false;
            yield return new WaitForSeconds(Seconds);
            ControlSpeaking = true;
        }
    }

    IEnumerator FreeSpeakTimer()
    {
        if (ClipboardHelper.clipBoard == "EvaMicFre")
        {
            ControlSpeaking = true;
        }
        yield break;
    }

    //Verifica alteraçoes do clipboard para comunicaçao com Eva
    IEnumerator WatchClipboard()
    {
        if (ClipboardHelper.clipBoard.Length == 9 && !ClipboardOcupado)
        {
            if (ClipboardHelper.clipBoard.Substring(0, 3) == "Eva")
            {
                ClipboardOcupado = true;
                yield return new WaitForSeconds(0.1f);
                //Setar Equipamentos
                if (ClipboardHelper.clipBoard.Substring(3, 1) == "E" && ClipboardHelper.clipBoard != "EvaEnvEma")
                {
                    int EquipIntensity = int.Parse(ClipboardHelper.clipBoard.Substring(6, 3));
                    int EquipNumber = int.Parse(ClipboardHelper.clipBoard.Substring(4, 2));
                    ControlSpeaking = true;
                    ValoresEquipamentos[EquipNumber] = ((float)EquipIntensity * LimiteMaxEquipamentos[EquipNumber]) / 100f; //recebe valor em porcentagem
                    MarkStatusEquipamentos[EquipNumber] = true;
                }
                //Relatorio de sensores
                if (ClipboardHelper.clipBoard.Substring(3, 1) == "S")
                {
                    int SensNumber = int.Parse(ClipboardHelper.clipBoard.Substring(4, 2));
                    PlaySound(3);
                    ControlSpeaking = true;
                    RelatFaladoSensorI[SensNumber] = true;
                }
                if (ClipboardHelper.clipBoard.Substring(3, 1) == "D")
                {
                    Desligar = true;
                    Desligando = true;
                    PlaySound(3);
                    ControlSpeaking = true;
                }
                if (ClipboardHelper.clipBoard == "EvaRelGer")
                {
                    //ScriptEva.RelatorioGeral=true;
                    PlaySound(3);
                    ControlSpeaking = true;
                }
                if (ClipboardHelper.clipBoard == "EvaEnvEma")
                {
                    EnviarResultadosViaEmail();
                    PlaySound(3);
                    ControlSpeaking = true;
                }

                ClipboardHelper.clipBoard = "";
                ClipboardOcupado = false;
                yield break;
            }
        }
    }

    //Verifica alteraçoes no arquivo Eva/bridge.ini
    IEnumerator BridgeWatcher()
    {
        string bridge = Application.dataPath + "/Eva/bridge.ini";
        AP_INIFile ini = new AP_INIFile(bridge);
        string change = ini.ReadString("Control", "Change");
        yield return new WaitForSeconds(0.01f);
        if (change == "True")
        {

            //Relatorio geral
            string BridgeRelatorio = ini.ReadString("Control", "Relatorio");
            yield return new WaitForSeconds(0.01f);
            if (BridgeRelatorio == "True")
            {
                //ScriptEva.RelatorioGeral=true;
                ini.WriteString("Control", "Relatorio", "False");
                yield return new WaitForSeconds(0.01f);
            }

            //Enviando email
            string BridgeEmail = ini.ReadString("Control", "EnviarEmail");
            yield return new WaitForSeconds(0.01f);
            if (BridgeEmail == "True")
            {
                EnviarResultadosViaEmail();
                ini.WriteString("Control", "EnviarEmail", "False");
                yield return new WaitForSeconds(0.01f);
            }

            //Parando equipamentos
            string BridgeStopAll = ini.ReadString("Control", "DesligaTudo");
            yield return new WaitForSeconds(0.01f);
            if (BridgeStopAll == "True")
            {
                PararTudo = true;
                ini.WriteString("Control", "DesligaTudo", "False");
                yield return new WaitForSeconds(0.01f);
            }

            //Controlar equipamento I
            for (int i = 0; i < Equipamentos.Length; i++)
            {
                string BridgeEquipStatus = ini.ReadString("Control", "Equipamento" + i.ToString("D2"));
                yield return new WaitForSeconds(0.01f);
                if (BridgeEquipStatus == "True" && !StatusEquipamentos[i])
                {
                    MarkStatusEquipamentos[i] = true;
                    ValoresEquipamentos[i] = LimiteMaxEquipamentos[i];
                }
                if (BridgeEquipStatus == "False" && StatusEquipamentos[i])
                {
                    MarkStatusEquipamentos[i] = true;
                    ValoresEquipamentos[i] = 0;
                }
            }

            //Relatorio do sensor I
            for (int i = 0; i < Sensores.Length; i++)
            {
                string BridgeSensorRelat = ini.ReadString("Control", "Sensor" + i.ToString("D2"));
                yield return new WaitForSeconds(0.01f);
                if (BridgeSensorRelat == "True" && !RelatFaladoSensorI[i])
                {
                    RelatFaladoSensorI[i] = true;
                    ini.WriteString("Control", "Sensor" + i.ToString("D2"), "False");
                    yield return new WaitForSeconds(0.01f);
                }
            }

            ini.WriteString("Control", "Change", "False");
        }
    }

    //Call a vbs file
    public static void CallVbs()
    {
        String Voice = "C:/TempSpeak/Temp.vbs";
        if (File.Exists(Voice))
        {
            Process scriptProc = new Process();
            scriptProc.StartInfo.FileName = @"cscript";
            scriptProc.StartInfo.Arguments = "//B //Nologo " + "C://TempSpeak//Temp.vbs";
            scriptProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //evita de mostrar a janela de prompt
            scriptProc.Start();
        }
    }

    //Conseguir versao do windows
    public void WindowsVersion(out string Version)
    {
        int foundS1 = SystemInfo.operatingSystem.IndexOf("(");
        Version = SystemInfo.operatingSystem.Substring(0, foundS1 - 2);
    }

    public void DataArduino(string Data, out string DataVariable, out float DataValue, out Boolean Useful)
    {
        DataVariable = "";
        DataValue = 0f;
        Useful = false;
        if (DebugSerial)
        {
            ConsoleString += "\r\n" + Data;
        }//Debug cuidado!
        if (DebugSerial && AutoScroll)
        {
            scrollPosition += new Vector2(0, 15000);
        }//Debug cuidado!
        int foundS1 = Data.IndexOf("=");
        if (foundS1 > 0)
        {
            DataVariable = Data.Substring(0, foundS1);
            string Value = "";
            Value = Data.Substring(foundS1 + 1, Data.Length - 1 - foundS1);
            DataValue = float.Parse(Value);
            Useful = false;
            if (String.Equals(Data[foundS1].ToString(), "=")) { Useful = true; }
        }
    }

    //pegar os resultados e distribuir em seu devido lugar
    public void GrabResult(string Resultado)
    {
        string a = "";
        float b = 0.0f;
        Boolean c = false;
        DataArduino(Resultado, out a, out b, out c);
        if (Resultado == "Arduino") { ArduinoConectado = true; TempoConexao = 0.0f; }
        for (int i = 0; i < Sensores.Length; i++)
        {
            if (String.Equals(a, Sensores[i] + i.ToString("D2")) && c)
            {//receber ex: Termometro01 (alterar codigo no arduino)
                if (b > ValoresMaxSensores[i]) { ValoresMaxSensores[i] = b; MaxDataSensores[i] = System.DateTime.Now.ToString("hh.mm.ss") + "  " + System.DateTime.Now.ToString("dd/MM/yyyy"); }
                if (b < ValoresMinSensores[i]) { ValoresMinSensores[i] = b; MinDataSensores[i] = System.DateTime.Now.ToString("hh.mm.ss") + "  " + System.DateTime.Now.ToString("dd/MM/yyyy"); }
                if (i != 6)
                {
                    ValoresAtuaisSensores[i] = (ValoresAtuaisSensores[i] + b) / 2f;
                    ValorHUDSensores[i].text = ((ValoresAtuaisSensores[i] + b) / 2f).ToString("F2");
                    ValorObj3DSensores[i].text = ((ValoresAtuaisSensores[i] + b) / 2f).ToString("F2") + " " + UnidadeSensores[i];
                }
                if (i == 6)
                {
                    ValoresAtuaisSensores[i] = b;
                    ValorHUDSensores[i].text = b.ToString("F2");
                    ValorObj3DSensores[i].text = b.ToString("F2") + " " + UnidadeSensores[i];
                }
                StatusObj3DSensores[i].text = "Funcionando";
                TituloObj3DSensores[i].text = Sensores[i];
                SensTempoDesdeUtimaAtualizacao[i] = 0f;
                SensTempoUtimaAtualizacao[i] = Time.time;
                VerifiqueTempoAtualizacao = true;
                float ValuePercent = (ValoresAtuaisSensores[i] - LimiteMinSensores[i]) / (LimiteMaxSensores[i] - LimiteMinSensores[i]);
                CircIndiSensores[i].GetComponent<Renderer>().material.SetFloat("_Percent", ValuePercent);
            }
        }
    }

    //resultados calculados em funçao de outras variaveis
    public void ResultadosCalculados()
    {
        //Razao molar
        float MMOleo = 859.1648f; //massa molar media do oleo de algodao
        float MMAcool = 46.06844f; //massa molar do alcool utilizado (Etanol = 46.06844 Metanol = 32.04)
        float RoOleo = 912f; //Densidade media do oleo de algodao Kg/m3
        float RoAlcool = 789f; //Densidade Kg/m3 (Etanol = 789 Metanol = 792)
        float FluxoOleo = ValoresAtuaisSensores[4]; //(l/min)
        float FluxoAlcool = ValoresAtuaisSensores[5] + ValoresAtuaisSensores[10] / 1000f; //(Vazao de alcool mais vazao de catalisador em l/min)
                                                                                          //UnityEngine.Debug.Log(FluxoAlcool.ToString("f4"));
        float VazaoMolarOleo = RoOleo * FluxoOleo / MMOleo; //(mol/min)
        float VazaoMolarAlcool = RoAlcool * FluxoAlcool / MMAcool; //(mol/min)
        if (VazaoMolarOleo < 0.005) { VazaoMolarOleo = 0f; }
        if (VazaoMolarAlcool < 0.005) { VazaoMolarAlcool = 0f; }
        float Razao = (VazaoMolarAlcool / VazaoMolarOleo); //Razao molar alcool/oleo
        if (!(float.IsNaN(ValoresAtuaisSensores[6])) &&
            ValoresAtuaisSensores[6] != float.PositiveInfinity &&
            ValoresAtuaisSensores[6] != float.NegativeInfinity)
        {
            Razao = (ValoresAtuaisSensores[6] + Razao) / 2f;
        }
        //if (Razao != Razao) {Razao = 0.0f;}
        //UnityEngine.Debug.Log("Razao molar "+Razao.ToString("f4"));
        GrabResult("Razao Molar06=" + Razao.ToString());
        //Dosagem de catalisador
        float PorcCatalisador = 2.8f;//padrao devido ao vazamento
        if (StatusEquipamentos[1]) { PorcCatalisador = 55.5f * (ValoresEquipamentos[1] / LimiteMaxEquipamentos[1]); }
        if (StatusEquipamentos[3]) { PorcCatalisador = 75.5f * (ValoresEquipamentos[3] / LimiteMaxEquipamentos[3]); }
        if (StatusEquipamentos[1] && StatusEquipamentos[3]) { PorcCatalisador = 55.5f * (ValoresEquipamentos[1] / LimiteMaxEquipamentos[1]) + 75.5f * (ValoresEquipamentos[3] / LimiteMaxEquipamentos[3]); }
        GrabResult("Catalisador10=" + PorcCatalisador.ToString());
        //Estimativa da conversao
        float Conv = 0f;
        float x = ValoresEquipamentos[0] / LimiteMaxEquipamentos[0]; //Refluxo (Normalizado 0-1)
        float y = (ValoresEquipamentos[4] / LimiteMaxEquipamentos[4]) * 1000f; //Carga termica (Watts)
        float z = Razao;//Razao molar
        if (float.IsNaN(z)) { z = 0.0f; }//verifica se e NaN

        GrabResult("Potencia07=" + y.ToString());

        if (StatusEquipamentos[6] && (StatusEquipamentos[1] || StatusEquipamentos[3]) && StatusEquipamentos[7])
        {

            Conv = 2.9122f * x + 0.22245f * y + 3.1834f * z + 0.017755f * x * y - 0.008923f * y * z - 0.27153f * x * z;//ajuste dos dados experimentais no matlab
            Conv = Conv / (0.9f + (FluxoOleo + FluxoAlcool) / 1.85f);//minhas correçoes
            if (Conv < 0f) { Conv = 0f; }
            if (Conv > 0f && Conv < 99.9f)
            {
                if (!(float.IsNaN(ValoresAtuaisSensores[9])) &&
                    ValoresAtuaisSensores[9] != float.PositiveInfinity &&
                    ValoresAtuaisSensores[9] != float.NegativeInfinity)
                {
                    Conv = (0.05f * Conv + 0.95f * ValoresAtuaisSensores[9]);
                }
            }
            if (Conv > 99.9f) { Conv = 99.9f; }
        }
        GrabResult("Conversão09=" + Conv.ToString());

        //Estimativa do sabao
        float Sabao = 0f;
        if (StatusEquipamentos[6] && (StatusEquipamentos[1] || StatusEquipamentos[3]) && StatusEquipamentos[7])
        {
            Sabao = -0.0185f * x - 0.0000175f * y - 0.0010546f * z - 0.00000025f * x * y + 0.00000875f * y * z + 0.00055f * x * z + 0.010655f;//ajuste dos dados experimentais no matlab
            Sabao = Sabao / (0.9f + (FluxoOleo + FluxoAlcool) / 1.85f);//minhas correçoes
            if (Sabao < 0f) { Sabao = 0f; }
            if (Sabao > 0f)
            {
                if (!(float.IsNaN(ValoresAtuaisSensores[8])) &&
                    ValoresAtuaisSensores[8] != float.PositiveInfinity &&
                    ValoresAtuaisSensores[8] != float.NegativeInfinity)
                {
                    Sabao = (0.05f * Sabao + 0.95f * ValoresAtuaisSensores[8]);
                }
            }
        }
        GrabResult("Sabão08=" + Sabao.ToString());

    }

    //Lembretes ao usuario
    public void Lembretes()
    {

        string FalaLembrete = null;
        if (StatusEquipamentos[1] || StatusEquipamentos[3])
        {
            FalaLembrete += " Catalisador";
        }
        if (StatusEquipamentos[6])
        {
            if (StatusEquipamentos[1] || StatusEquipamentos[3]) { FalaLembrete += " e "; }
            FalaLembrete += "Alcool";
        }
        if (StatusEquipamentos[7])
        {
            if (StatusEquipamentos[6] || StatusEquipamentos[1] || StatusEquipamentos[3]) { FalaLembrete += " e "; }
            FalaLembrete += "Oleo";
        }
        if (ValoresAtuaisSensores[11] > LimiteMaxSensores[11])
        {//verificando tanque de resfriamento
            if (FalaLembrete != null) { FalaLembrete += "... e também"; }
            FalaLembrete += "Adicione mais gelô ao tanque de resfriamento";
        }
        if (FalaLembrete != null)
        {
            if (StatusEquipamentos[6] || StatusEquipamentos[1] || StatusEquipamentos[3] || StatusEquipamentos[7])
            {
                Speak("Verifique os níveis de" + FalaLembrete);
            }
        }
    }

    private int COMPortNumber(string COMPortName)
    {
        int num = COMPortName.IndexOf("M");
        string s = string.Empty;
        s = COMPortName.Substring(num + 1, COMPortName.Length - 1 - num);
        return int.Parse(s);
    }

    //Testar conexao com a internet
    //using System.Net;
    public static bool CheckForInternetConnection()
    {
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

    public void EnviarResultadosViaEmail()
    {
        String Log = Application.dataPath + "/Log.txt";
        //Com internet
        if (CheckForInternetConnection())
        {
            Speak("relatório enviado para os e-mails cadastrados");
            AddText(Log, "relatório enviado para os e-mails cadastrados");
            ConsoleString += "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + "  " + "relatório enviado para os e-mails cadastrados";
            scrollPosition += new Vector2(0, 15000);

            AssuntoEmail = "Relatorio do supervisório da " + ProjectName;
            CorpoEmail = "Resultados do supervisório da " + ProjectName;
            SendEmail(destinatarios, AssuntoEmail, CorpoEmail, AtachedFiles);
            EmailsEnviados += 1;
        }
        //Sem internet
        if (!CheckForInternetConnection())
        {
            Speak("relatório não enviado devido a problemas de conexão com a internet");
            AddText(Log, "relatório não enviado devido a problemas de conexão com a internet");
            ConsoleString += "\r\n" + System.DateTime.Now.ToString("hh.mm.ss") + "  " + "relatório não enviado devido a problemas de conexão com a internet";
            scrollPosition += new Vector2(0, 15000);
        }
    }

    //enviar e-mail
    public void SendEmail(string[] receivers, string Assunto, string Corpo, string[] PathAtachedFile)
    {
        using (var mail = new MailMessage
        {
            From = new MailAddress("eva.supervisorio@gmail.com"),
            Subject = Assunto,
            Body = Corpo
        })
        {
            for (int i = 0; i < receivers.Length; i++)
            {
                mail.To.Add(receivers[i]);
            }

            for (int j = 0; j < PathAtachedFile.Length; j++)
            {
                System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(Application.dataPath + "\\" + PathAtachedFile[j]);
                mail.Attachments.Add(attachment);
            }

            var smtpServer = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = (ICredentialsByHost)new NetworkCredential("eva.supervisorio@gmail.com", "5g9s3x65rtml4")
            };
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            smtpServer.EnableSsl = true;
            smtpServer.Send(mail);
        }
    }

    //Checar o status dos sensores
    public void CheckSensorStatus()
    {
        string FalaTemp = "";
        int SensoresSemResposta = 0;
        for (int i = 0; i < Sensores.Length; i++)
        {
            SensTempoDesdeUtimaAtualizacao[i] = Time.time - SensTempoUtimaAtualizacao[i];
            if (SensTempoDesdeUtimaAtualizacao[i] > 43f)
            {
                SensoresSemResposta += 1;
                FalaTemp += " O " + Sensores[i] + " " + i.ToString("D2") + " " + DescriçaoSensor[i] + " parou de funcionar a " + IntuitiveTime(SensTempoDesdeUtimaAtualizacao[i]) + "...";
            }
        }
        //Condiçoes especificas
        if (StatusEquipamentos[4] && ValoresAtuaisSensores[7] < 600f) { FalaTemp += " O refervedor está ligado, mas não há corrente significativa passando por ele..."; }
        if (StatusEquipamentos[7] && ValoresAtuaisSensores[4] == 0f) { FalaTemp += " A bomba de óleo está ligada, mas não há vazão no sensor de fluxo de óleo..."; }
        if (StatusEquipamentos[6] && ValoresAtuaisSensores[5] == 0f) { FalaTemp += " A bomba de álcool está ligada, mas não há vazão no sensor de fluxo de álcool..."; }

        if (SensoresSemResposta > 0 && SensoresSemResposta < 2) { Speak("Há " + SensoresSemResposta.ToString() + " sensor com mal funcionamento ..." + FalaTemp); }
        if (SensoresSemResposta > 1) { Speak("Há " + SensoresSemResposta.ToString() + " sensores com mal funcionamento ..." + FalaTemp); }

    }

    //Graficos
    public Rect DisplayDebugGUI(String Titulo, String Xlabel, String Ylabel, int PositionX, int PositionY, int Largura, int Altura, float[] Xvalues, float[] YValues, Texture LinhasoGraph, Texture TexturaFundoGraph, Texture graphTexture, float MinValue, float MaxValue, Color ColorDefault, Color ColorLow, Color ColorHigh)
    {
        if (graphTexture != null)
        {
            int labelWidth = 150;
            int labelHeight = 51;
            int ButtonSize = 32;
            int XLeftOver = 60;
            int YLeftOver = 40;
            int padding = 2;
            int frameWidth = Largura;
            int frameHeight = Altura;
            int barWidth = Mathf.RoundToInt((frameWidth - 2 * XLeftOver) / Xvalues.Length);
            int barHeight = frameHeight - 2 * YLeftOver;
            Rect frameRect = new Rect(PositionX - padding, PositionY - padding, frameWidth, frameHeight);

            GUI.BeginGroup(frameRect);

            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            centeredStyle.fontSize = labelHeight / 3 - 6;

            var gs = GUI.skin.GetStyle("Button");
            gs.fontSize = labelHeight / 3 - 7;

            GUI.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            GUI.DrawTexture(new Rect(0, 0, frameRect.width, frameRect.height), TexturaFundoGraph);
            GUI.color = Color.white;

            //Titulo
            GUI.Label(new Rect(XLeftOver, YLeftOver / 2 - labelHeight / 2, frameWidth - 2 * XLeftOver, labelHeight + 4), Titulo);

            //Valores do eixo Y
            GUI.Label(new Rect(XLeftOver / 2 - labelWidth / 2, frameHeight / 2 - labelHeight / 2, labelWidth, labelHeight + 3), Ylabel);
            GUI.Label(new Rect(XLeftOver / 2 - labelWidth / 2, YLeftOver - labelHeight / 2, labelWidth, labelHeight + 3), MaxValue.ToString("F2"));
            GUI.Label(new Rect(XLeftOver / 2 - labelWidth / 2, frameHeight - YLeftOver - labelHeight / 2, labelWidth, labelHeight + 3), MinValue.ToString("F2"));

            //Valores do eixo X
            GUI.Label(new Rect(frameWidth / 2 - labelWidth / 2, frameHeight - YLeftOver / 2 + labelHeight / 10 - labelHeight / 2, labelWidth, labelHeight + 3), Xlabel);
            GUI.Label(new Rect(XLeftOver - labelWidth / 2, frameHeight - YLeftOver + labelHeight / 6 - labelHeight / 2, labelWidth, labelHeight + 3), Xvalues[Xvalues.Length - 1].ToString("F2"));
            GUI.Label(new Rect(XLeftOver + (frameWidth - 2 * XLeftOver) / 4 - labelWidth / 2, frameHeight - YLeftOver + labelHeight / 6 - labelHeight / 2, labelWidth, labelHeight + 3), Xvalues[3 * Mathf.RoundToInt(Xvalues.Length / 4) - 1].ToString("F2"));
            GUI.Label(new Rect(frameWidth / 2 - labelWidth / 2, frameHeight - YLeftOver + labelHeight / 6 - labelHeight / 2, labelWidth, labelHeight + 3), Xvalues[Mathf.RoundToInt(Xvalues.Length / 2) - 1].ToString("F2"));
            GUI.Label(new Rect(XLeftOver + 3 * (frameWidth - 2 * XLeftOver) / 4 - labelWidth / 2, frameHeight - YLeftOver + labelHeight / 6 - labelHeight / 2, labelWidth, labelHeight + 3), Xvalues[Mathf.RoundToInt(Xvalues.Length / 4) - 1].ToString("F2"));
            GUI.Label(new Rect(frameWidth - XLeftOver - labelWidth / 2, frameHeight - YLeftOver + labelHeight / 6 - labelHeight / 2, labelWidth, labelHeight + 3), Xvalues[0].ToString("F2"));

            GUI.color = Color.white;
            //Grid
            //Horizontal
            GUI.DrawTexture(new Rect(XLeftOver, YLeftOver, frameWidth - 2 * XLeftOver, 1), LinhasoGraph);
            GUI.DrawTexture(new Rect(XLeftOver, frameHeight / 2, frameWidth - 2 * XLeftOver, 1), LinhasoGraph);
            GUI.DrawTexture(new Rect(XLeftOver, frameHeight - YLeftOver, frameWidth - 2 * XLeftOver, 1), LinhasoGraph);
            //Vertical
            GUI.DrawTexture(new Rect((frameWidth - 2 * XLeftOver) / 4 + XLeftOver, YLeftOver, 1, frameHeight - 2 * YLeftOver), LinhasoGraph);
            GUI.DrawTexture(new Rect(frameWidth / 2, YLeftOver, 1, frameHeight - 2 * YLeftOver), LinhasoGraph);
            GUI.DrawTexture(new Rect(3 * (frameWidth - 2 * XLeftOver) / 4 + XLeftOver, YLeftOver, 1, frameHeight - 2 * YLeftOver), LinhasoGraph);
            //Margins
            GUI.color = 0.8f * Color.blue + 0.5f * Color.green;
            GUI.DrawTexture(new Rect(0, 0, frameWidth, 1), LinhasoGraph);
            GUI.DrawTexture(new Rect(0, frameHeight - 1, frameWidth, 1), LinhasoGraph);
            GUI.DrawTexture(new Rect(0, 0, 1, frameHeight), LinhasoGraph);
            GUI.DrawTexture(new Rect(frameWidth - 1, 0, 1, frameHeight), LinhasoGraph);
            GUI.DrawTexture(new Rect(frameWidth - 1 - ButtonSize - 4 * padding, 0, 1, frameHeight), LinhasoGraph);

            GUI.color = 0.5f * Color.blue + 0.3f * Color.green + 0.3f * Color.white;
            //Botões
            if (GUI.Button(new Rect(frameWidth - ButtonSize - padding / 2, YLeftOver / 2 - ButtonSize / 2 + 4, ButtonSize - 4, ButtonSize - 4), Save_Icon)) { SavePartOfScreen(Mathf.RoundToInt(0.24f * Screen.width), Mathf.RoundToInt(0.04f * Screen.height + 5), Mathf.RoundToInt(0.43f * Screen.width), Mathf.RoundToInt(0.68f * Screen.height), Titulo + " " + System.DateTime.Now.ToString("hh.mm.ss") + "  " + System.DateTime.Now.ToString("dd-MM-yyyy")); }
            //Mathf.RoundToInt(0.25f * Screen.width + 10), Mathf.RoundToInt(0.7f * Screen.height - 10), Mathf.RoundToInt(0.41f * Screen.width + 5), Mathf.RoundToInt(0.69f * Screen.height - 50)
            if (GUI.Button(new Rect(frameWidth - ButtonSize - padding / 2, YLeftOver / 2 - ButtonSize / 2 + 4 + 2 * padding + ButtonSize, ButtonSize - 4, ButtonSize / 2 + 2), "10m")) { }
            if (GUI.Button(new Rect(frameWidth - ButtonSize - padding / 2, YLeftOver / 2 - ButtonSize / 2 + 4 + 3 * padding + 3 * ButtonSize / 2, ButtonSize - 4, ButtonSize / 2 + 2), "1h")) { }
            if (GUI.Button(new Rect(frameWidth - ButtonSize - padding / 2, YLeftOver / 2 - ButtonSize / 2 + 4 + 4 * padding + 4 * ButtonSize / 2, ButtonSize - 4, ButtonSize / 2 + 2), "1d")) { }
            if (GUI.Button(new Rect(frameWidth - ButtonSize - padding / 2, YLeftOver / 2 - ButtonSize / 2 + 4 + 5 * padding + 5 * ButtonSize / 2, ButtonSize - 4, ButtonSize / 2 + 2), "1s")) { }

            for (int i = 0; i < YValues.Length; i++)
            {
                float perc = ((YValues[i] - MinValue) / (MaxValue - MinValue));
                GUI.color = new Color(1f, 1f, 1f, 0.25f);
                //GUI.color = ColorDefault;
                if (perc <= 0.1f) { GUI.color = ColorLow; }
                if (perc >= 0.9f) { GUI.color = ColorHigh; }
                if (!float.IsNaN(perc) && !float.IsInfinity(perc))
                {
                    GUI.DrawTexture(new Rect(XLeftOver + barWidth * i, frameHeight - YLeftOver, barWidth, -(int)(((float)barHeight) * perc)), graphTexture);
                    GUI.color = Color.white;
                    GUI.DrawTexture(new Rect(XLeftOver + barWidth * i, frameHeight - YLeftOver - (int)(((float)barHeight) * perc), barWidth, 1), graphTexture);
                }
            }

            GUI.color = Color.white;

            GUI.EndGroup();

            return frameRect;
        }
        return new Rect(0, 0, 0, 0);
    }

    // Display sensor stats
    public Rect DisplaySensorStats(int SensorNumber, String Titulo, int PositionX, int PositionY, int Largura, int Altura, float[] Xvalues, float[] YValues, float Max, float Min, Texture ColorIndicator)
    {
        int labelWidth = 200;
        int labelHeight = 11 + Screen.width / 1000;
        int IndicatorSize = 32;
        int XLeftOver = 30;
        int YLeftOver = 30;
        int padding = 2;
        int frameWidth = Largura;
        int frameHeight = Altura;
        Rect frameRect = new Rect(PositionX - padding, PositionY - padding, frameWidth, frameHeight);

        GUI.BeginGroup(frameRect);

        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.MiddleCenter;
        centeredStyle.fontSize = 11 + Screen.width / 1000;

        GUI.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        GUI.DrawTexture(new Rect(0, 0, frameRect.width, frameRect.height), TexturaFundoGraph);
        GUI.color = Color.white;

        //Titulo
        GUI.Label(new Rect(XLeftOver, YLeftOver / 2 - labelHeight / 2, frameWidth - 2 * XLeftOver, labelHeight + 4), Titulo);

        centeredStyle.alignment = TextAnchor.MiddleLeft;
        //Informaçoes
        GUI.Label(new Rect(IndicatorSize + XLeftOver + 5, YLeftOver + 3f * labelHeight / 2, labelWidth, labelHeight + 3), "Media: " + Media(YValues).ToString("F3") + " " + UnidadeSensores[SensorNumber]);
        GUI.Label(new Rect(IndicatorSize + XLeftOver + 5, YLeftOver + 3f * labelHeight, labelWidth, labelHeight + 3), "Minimo: " + Min.ToString("F3") + " " + UnidadeSensores[SensorNumber]);
        GUI.Label(new Rect(IndicatorSize + XLeftOver + 5, YLeftOver + 9f * labelHeight / 2, labelWidth, labelHeight + 3), "Maximo: " + Max.ToString("F3") + " " + UnidadeSensores[SensorNumber]);
        GUI.Label(new Rect(IndicatorSize + XLeftOver + 5, YLeftOver + 6f * labelHeight, labelWidth, labelHeight + 3), "Taxa: " + TaxaFinal(Xvalues, YValues).ToString("F4") + " " + UnidadeSensores[SensorNumber] + "/min");
        //Datas
        centeredStyle.alignment = TextAnchor.MiddleRight;
        GUI.Label(new Rect(frameWidth / 2, YLeftOver + 3f * labelHeight / 2, labelWidth, labelHeight + 3), "Ultimos 10 minutos");
        GUI.Label(new Rect(frameWidth / 2, YLeftOver + 3f * labelHeight, labelWidth, labelHeight + 3), MinDataSensores[SensorNumber]);
        GUI.Label(new Rect(frameWidth / 2, YLeftOver + 9f * labelHeight / 2, labelWidth, labelHeight + 3), MaxDataSensores[SensorNumber]);
        centeredStyle.alignment = TextAnchor.MiddleCenter;
        GUI.color = Color.white;
        //Indicadores
        GUI.color = Color.green;
        if (Media(YValues) >= LimiteMaxSensores[SensorNumber] || Media(YValues) <= LimiteMinSensores[SensorNumber]) { GUI.color = Color.red; }
        GUI.DrawTexture(new Rect(XLeftOver, YLeftOver + 3f * labelHeight / 2, IndicatorSize, labelHeight), ColorIndicator);
        GUI.color = Color.green;
        if (Min >= LimiteMaxSensores[SensorNumber] || Min <= LimiteMinSensores[SensorNumber]) { GUI.color = Color.yellow; }
        GUI.DrawTexture(new Rect(XLeftOver, YLeftOver + 3f * labelHeight, IndicatorSize, labelHeight), ColorIndicator);
        GUI.color = Color.green;
        if (Max >= LimiteMaxSensores[SensorNumber] || Max <= LimiteMinSensores[SensorNumber]) { GUI.color = Color.yellow; }
        GUI.DrawTexture(new Rect(XLeftOver, YLeftOver + 9f * labelHeight / 2, IndicatorSize, labelHeight), ColorIndicator);
        GUI.color = Color.green;
        GUI.DrawTexture(new Rect(XLeftOver, YLeftOver + 6f * labelHeight, IndicatorSize, labelHeight), ColorIndicator);

        //Margins
        GUI.color = 0.8f * Color.blue + 0.5f * Color.green;
        //Horizontal
        GUI.DrawTexture(new Rect(0, YLeftOver, frameWidth, 1), LinhasoGraph);
        GUI.DrawTexture(new Rect(0, 0, frameWidth, 1), LinhasoGraph);
        GUI.DrawTexture(new Rect(0, frameHeight - 1, frameWidth, 1), LinhasoGraph);
        GUI.DrawTexture(new Rect(0, 0, 1, frameHeight), LinhasoGraph);
        GUI.DrawTexture(new Rect(frameWidth - 1, 0, 1, frameHeight), LinhasoGraph);

        GUI.color = Color.white;

        GUI.EndGroup();

        return frameRect;
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

    public static float TaxaFinal(float[] EntradaX, float[] EntradaY)
    {
        float Taxa = 0;
        Taxa = 0.5f * (EntradaY[EntradaY.Length - 1] - EntradaY[EntradaY.Length - 2]) / (EntradaX[EntradaX.Length - 1] - EntradaX[EntradaX.Length - 2]) + 0.5f * (EntradaY[EntradaY.Length - 2] - EntradaY[EntradaY.Length - 3]) / (EntradaX[EntradaX.Length - 2] - EntradaX[EntradaX.Length - 3]);
        return Taxa;
    }
    public float SegundaDerivadaFinal(float[] EntradaX, float[] EntradaY)
    {
        float Taxa = 0;
        Taxa = ((EntradaY[EntradaY.Length - 1] - EntradaY[EntradaY.Length - 2]) / (EntradaX[EntradaX.Length - 1] - EntradaX[EntradaX.Length - 2]) - (EntradaY[EntradaY.Length - 2] - EntradaY[EntradaY.Length - 3]) / (EntradaX[EntradaX.Length - 2] - EntradaX[EntradaX.Length - 3])) / (EntradaX[EntradaX.Length - 2] - EntradaX[EntradaX.Length - 3]);
        return Taxa;
    }

    public static void CubicInterpolation(float[] x, float[] y, out float[] Xout, out float[] Yout, int NumberOfPoints)
    {
        //using TestMySpline

        // Create the upsampled X values to interpolate
        int n = NumberOfPoints;
        Xout = new float[n];
        Yout = new float[n];
        float stepSize = (x[x.Length - 1] - x[0]) / (n - 1);

        for (int i = 0; i < n; i++)
        {
            Xout[i] = x[0] + i * stepSize;
        }

        // Fit and eval
        CubicSpline spline = new CubicSpline();
        Yout = spline.FitAndEval(x, y, Xout);

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
    public void InterpolationToPlot(int Linha, int Coluna, float[,,] XInputArray, float[,,] YInputArray, float[,,] XOutputArray, float[,,] YOutputArray, out float[,,] XResult, out float[,,] YResult)
    {
        XResult = XOutputArray;
        YResult = YOutputArray;
        float[] x = new float[YInputArray.GetLength(2)];
        float[] y = new float[YInputArray.GetLength(2)];
        float[] array = new float[YOutputArray.GetLength(2)];
        float[] array2 = new float[YOutputArray.GetLength(2)];
        x = PartOfArray(XInputArray, Linha, Coluna);
        y = PartOfArray(YInputArray, Linha, Coluna);
        CentralArduino.CubicInterpolation(x, y, out array, out array2, YOutputArray.GetLength(2));
        for (int i = 0; i < YOutputArray.GetLength(2); i++)
        {
            XResult[Linha, Coluna, i] = array[i];
            YResult[Linha, Coluna, i] = array2[i];
        }
    }
    private string ShowArrayLine(float[,,] InputArray, int Line, int Column)
    {
        string text = string.Empty;
        for (int i = 0; i < InputArray.GetLength(2); i++)
        {
            text = text + " " + InputArray[Line, Column, i].ToString();
        }
        return text;
    }

    private string ShowArray(float[] InputArray)
    {
        string text = string.Empty;
        for (int i = 0; i < InputArray.GetLength(0); i++)
        {
            text = text + InputArray[i].ToString("f4") + " ";
        }
        return text;
    }

    private string ShowArrayStrings(string[] InputArray)
    {
        string text = string.Empty;
        for (int i = 0; i < InputArray.GetLength(0); i++)
        {
            text = text + InputArray[i] + i.ToString("D2") + " ";
        }
        return text;
    }

    //Relatorio ddo sensor i (preditivo)
    IEnumerator ReliableSetEquipment(string CodToArduino)
    {
        if (Arduino)
        {
            sp.Write(CodToArduino);
        }
        yield return new WaitForSeconds(0.07f);
        //sp.Write ("\n");
    }

    //Relatorio ddo sensor i (preditivo)
    IEnumerator RelatorioSensor(int i, string Tipo)
    {

        float[] UtimosQuinzePontosX = { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
        float[] UtimosQuinzePontosY = { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
        for (int j = 0; j < 14; j++)
        {
            yield return new WaitForSeconds(0.01f);
            UtimosQuinzePontosX[j] = PartOfArray(TempoValoresSensores, i, 0)[j];
            UtimosQuinzePontosY[j] = PartOfArray(ValoresSensores, i, 0)[PartOfArray(ValoresSensores, i, 0).Length - 14 + j];
        }
        float[] ReamostragemQuatroPontosX = { 0, 0, 0, 0 };
        float[] ReamostragemQuatroPontosY = { 0, 0, 0, 0 };
        for (int j = 0; j < 4; j++)
        {
            yield return new WaitForSeconds(0.01f);
            ReamostragemQuatroPontosY[j] = (0.05f * UtimosQuinzePontosY[j] + 0.25f * UtimosQuinzePontosY[j + 1] + 0.4f * UtimosQuinzePontosY[j + 2] + 0.25f * UtimosQuinzePontosY[j + 3] + 0.05f * UtimosQuinzePontosY[j + 4]);
            ReamostragemQuatroPontosX[j] = (0.05f * UtimosQuinzePontosX[j] + 0.25f * UtimosQuinzePontosX[j + 1] + 0.4f * UtimosQuinzePontosX[j + 2] + 0.25f * UtimosQuinzePontosX[j + 3] + 0.05f * UtimosQuinzePontosX[j + 4]);
        }

        float TaxaAtual = TaxaFinal(ReamostragemQuatroPontosX, ReamostragemQuatroPontosY);
        float SegundaDerivadaAtual = SegundaDerivadaFinal(ReamostragemQuatroPontosX, ReamostragemQuatroPontosY);
        //UnityEngine.Debug.Log("SegundaDerivada "+SegundaDerivadaAtual.ToString("f4"));
        float TempoCritico = 0.0f;
        string FalaTemp = string.Empty;

        if (Tipo == "previsao")
        {

            float AssintotaY = -0.0f;
            float TempoDaAssintota = 0f;
            string Status = "";
            AssintotaFinal(PartOfArray(TempoValoresSensores, i, 0), PartOfArray(ValoresSensores, i, 0), out AssintotaY, out TempoDaAssintota, out Status);
            if (Status == "desestabilizando")
            {
                //dentro do limite
                if (ValoresAtuaisSensores[i] > LimiteMinSensores[i] && ValoresAtuaisSensores[i] < LimiteMaxSensores[i])
                {
                    if (TaxaAtual > 0.05f && SegundaDerivadaAtual > -0.1f)
                    {
                        TempoCritico = (LimiteMaxSensores[i] - ValoresAtuaisSensores[i]) / TaxaAtual;
                        FalaTemp = "Na velocidade atual a " + TipoSensores[i] + " máxima no " + Sensores[i] + " " + i.ToString("D2") + " " + DescriçaoSensor[i] + " será atingida em " + TempoCritico.ToString("f2") + " minutos";
                        yield return new WaitForSeconds(0.01f);
                    }
                    if (TaxaAtual < -0.05f && SegundaDerivadaAtual < 0.1f)
                    {
                        TempoCritico = (ValoresAtuaisSensores[i] - LimiteMinSensores[i]) / TaxaAtual;
                        FalaTemp = "Na velocidade atual a " + TipoSensores[i] + " mínima no " + Sensores[i] + " " + i.ToString("D2") + " " + DescriçaoSensor[i] + " será atingida em " + TempoCritico.ToString("f2") + " minutos";
                        yield return new WaitForSeconds(0.01f);
                    }
                }
                //abaixo do limite
                if (ValoresAtuaisSensores[i] < LimiteMinSensores[i])
                {
                    if (TaxaAtual < -0.05f && SegundaDerivadaAtual < 0.1f)
                    {
                        FalaTemp = "A " + TipoSensores[i] + " no " + Sensores[i] + " " + i.ToString("D2") + " " + DescriçaoSensor[i] + " está abaixo do limite inferior. e continua diminuindo";
                        yield return new WaitForSeconds(0.01f);
                    }
                    if (TaxaAtual > 0.05f && SegundaDerivadaAtual > -0.1f)
                    {
                        TempoCritico = (LimiteMinSensores[i] - ValoresAtuaisSensores[i]) / TaxaAtual;
                        FalaTemp = "A " + TipoSensores[i] + " no " + Sensores[i] + " " + i.ToString("D2") + " " + DescriçaoSensor[i] + " está abaixo do limite inferior. voltará ao valor mínimo em " + TempoCritico.ToString("f2") + " minutos";
                        yield return new WaitForSeconds(0.01f);
                    }
                }
                //acima do limite
                if (ValoresAtuaisSensores[i] > LimiteMaxSensores[i])
                {
                    if (TaxaAtual > 0.05f && SegundaDerivadaAtual > -0.1f)
                    {
                        FalaTemp = "A " + TipoSensores[i] + " no " + Sensores[i] + " " + i.ToString("D2") + " " + DescriçaoSensor[i] + " está acima do limite superior. e continua aumentando";
                        yield return new WaitForSeconds(0.01f);
                    }
                    if (TaxaAtual < -0.05f && SegundaDerivadaAtual < 0.1f)
                    {
                        TempoCritico = (ValoresAtuaisSensores[i] - LimiteMaxSensores[i]) / TaxaAtual;
                        FalaTemp = "A " + TipoSensores[i] + " no " + Sensores[i] + " " + i.ToString("D2") + " " + DescriçaoSensor[i] + " está acima do limite superior. voltará ao valor máximo em " + TempoCritico.ToString("f2") + " minutos";
                        yield return new WaitForSeconds(0.01f);
                    }
                }
            }
            if (Status != "desestabilizando")
            {
                if (Status == "crescendo e estabilizando" || Status == "decrescendo e estabilizando")
                {
                    if ((TaxaAtual > 0f && AssintotaY > ValoresAtuaisSensores[i]) || (TaxaAtual < 0f && AssintotaY < ValoresAtuaisSensores[i]))
                    {
                        FalaTemp = "A " + TipoSensores[i] + " no " + Sensores[i] + " " + i.ToString("D2") + " " + DescriçaoSensor[i] + " irá se estabilizar próximo de " + AssintotaY.ToString("f2") + " " + UnidadeFaladaSensores[i] + " . dentro de " + IntuitiveTime(TempoDaAssintota * 60f);
                        yield return new WaitForSeconds(0.01f);
                        if (AssintotaY > LimiteMaxSensores[i]) { FalaTemp += ". Este valor está acima do limite superior estabelecido"; yield return new WaitForSeconds(0.01f); }
                        if (AssintotaY < LimiteMinSensores[i]) { FalaTemp += ". Este valor está abaixo do limite inferior estabelecido"; yield return new WaitForSeconds(0.01f); }
                    }
                }
                if (Status == "estabilizando")
                {
                    FalaTemp = "A " + TipoSensores[i] + " no " + Sensores[i] + " " + i.ToString("D2") + " " + DescriçaoSensor[i] + " está se estabilizando em torno de " + AssintotaY.ToString("f2") + " " + UnidadeFaladaSensores[i];
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
        Speak(FalaTemp);
        yield return null;
    }
    //Encontrar assintota de uma serie de dados (preditivo)
    public void AssintotaFinal(float[] EntradaX, float[] EntradaY, out float AssintotaY, out float TempoDaAssintota, out string Status)
    {
        TempoDaAssintota = 0f;
        AssintotaY = -0.0f;
        Status = "";
        float TaxaAtual = TaxaFinal(EntradaX, EntradaY);
        float[] UtimosQuinzePontosX = { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
        float[] UtimosQuinzePontosY = { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
        for (int i = 0; i < 14; i++)
        {
            UtimosQuinzePontosX[i] = EntradaX[i];
            UtimosQuinzePontosY[i] = EntradaY[EntradaY.Length - 14 + i];
        }
        float[] ReamostragemQuatroPontosX = { 0, 0, 0, 0 };
        float[] ReamostragemQuatroPontosY = { 0, 0, 0, 0 };
        for (int i = 0; i < 4; i++)
        {
            ReamostragemQuatroPontosY[i] = (0.05f * UtimosQuinzePontosY[i] + 0.25f * UtimosQuinzePontosY[i + 1] + 0.4f * UtimosQuinzePontosY[i + 2] + 0.25f * UtimosQuinzePontosY[i + 3] + 0.05f * UtimosQuinzePontosY[i + 4]);
            ReamostragemQuatroPontosX[i] = (0.05f * UtimosQuinzePontosX[i] + 0.25f * UtimosQuinzePontosX[i + 1] + 0.4f * UtimosQuinzePontosX[i + 2] + 0.25f * UtimosQuinzePontosX[i + 3] + 0.05f * UtimosQuinzePontosX[i + 4]);
        }
        //UnityEngine.Debug.Log("Quinze pontos X "+ShowArray (ReamostragemQuatroPontosX));
        //UnityEngine.Debug.Log("Quinze pontos Y "+ShowArray (ReamostragemQuatroPontosY));

        float[] razao = { 0f, 0f };
        for (int i = 0; i < 2; i++)
        {
            razao[i] = (ReamostragemQuatroPontosY[i + 2] - ReamostragemQuatroPontosY[i + 1]) / (ReamostragemQuatroPontosY[i + 1] - ReamostragemQuatroPontosY[i]);
        }

        float q = Media(razao);

        if (q > 0f && q < 1f)
        {
            float diferenca = (ReamostragemQuatroPontosY[3] - ReamostragemQuatroPontosY[2]) / (1f - q);

            if (q > 0f && q < 1f && TaxaAtual > 0f)
            {
                AssintotaY = ReamostragemQuatroPontosY[2] + diferenca;
                Status = "crescendo e estabilizando";
            }
            if (q > 0f && q < 1f && TaxaAtual < 0f)
            {
                AssintotaY = ReamostragemQuatroPontosY[2] + diferenca;
                Status = "decrescendo e estabilizando";
            }
            if (TaxaAtual > -0.05f && TaxaAtual < 0.05f)
            {
                AssintotaY = (ReamostragemQuatroPontosY[3] + ReamostragemQuatroPontosY[2]) / 2f;
                Status = "estabilizando";
            }

            TempoDaAssintota = (ReamostragemQuatroPontosX[3] - ReamostragemQuatroPontosX[2]) * TaxaAtual * (1f + (Mathf.Log(0.05f)) / (Mathf.Log(q)));
        }
        if (q < 0f || q > 1f) { Status = "desestabilizando"; }

        //		UnityEngine.Debug.Log("Razoes "+ShowArray (razao));
        //		UnityEngine.Debug.Log("Taxa atual "+TaxaAtual.ToString("f4"));
        //		UnityEngine.Debug.Log("Razao "+q.ToString("f4"));
        //		UnityEngine.Debug.Log("Passos "+(n*TaxaAtual).ToString());
        //		UnityEngine.Debug.Log("Assintota: "+AssintotaY.ToString());
        //		UnityEngine.Debug.Log("status "+Status);

    }
    //Regressao linear de dados
    public void RegressaoLinear(float[] X, float[] Y, out float a, out float b, out float Rsquare)
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

    public static void SavePartOfScreen(int PositionX, int PositionY, int Largura, int Altura, String Name)
    {

        //using System;

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
        Destroy(tex);

        //also write to a file in the project folder
        File.WriteAllBytes(Application.dataPath + "/Screenshots/" + Name + ".png", bytes);

    }

    public void ShowHideHUD()
    {
        bool Marker = true;
        Audios[10].Play();
        if (HUD && Marker)
        {
            for (int i = 0; i < HUDObjects.Length; i++)
            {
                HUDObjects[i].SetActive(false);
            }
            Demonstrative = true;
            HUDoperacao = false;
            Console = false;
            Stats = false;
            Graficos = false;
            HUD = !HUD;
            Marker = false;
        }
        if (!HUD && Marker)
        {
            for (int i = 0; i < HUDObjects.Length; i++)
            {
                HUDObjects[i].SetActive(true);
            }
            Demonstrative = false;
            HUDoperacao = false;
            Console = false;
            Stats = true;
            Graficos = false;
            HUD = !HUD;
            Marker = false;
        }
    }

    public static void PlaySound(int i)
    {
        StaticAudios[i].Play();
    }

    //[RPC]
    private void LigaDesligaEquipamento(int Numero, int intensity)
    {
        MarkStatusEquipamentos[Numero] = true;
        ValoresEquipamentos[Numero] = intensity;
    }
    //[RPC]
    private void SendData(string ReceivedData)
    {
        RemoteData = ReceivedData;
    }
    //[RPC]
    private void UpdateEquipTime(string T, int Equip)
    {
        TempoHUDEquipamentos[Equip] = T;
    }
    //[RPC]
    private void UpdateCicloEquip(string T, int Equip)
    {
        TempoCicloEquip[Equip] = T;
    }
    //[RPC]
    private void UpdateEquipValues()
    {
        HUDChanged = true;
    }


}
