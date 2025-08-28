using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using NeuralNetwork;
using Reusable;
using System.Globalization;

public class Nucleo : MonoBehaviour
{

    public GUISkin MyGUISkin;
    public bool Validate = true;
    public int FinishYearOfLicence = 3000;
    public bool SendEmailTrained = false;
    private string TempoTreino = "7800";
    public float TreinoTempo = 7800f;
    public string TaxaDeAprendizagemS = "0.5";
    public float TaxaDeAprendizagem = 0.5f;
    public string ReiniRangeS = "0";
    public float ReiniRange = 0f;
    public string NumeroDeRedesS = "30";
    public int NumeroDeRedes = 30;
    public int OutputLineToGraph = 0;
    public string OutputLineToGraphS = "0";
    public string ErroSimuS = "0.001";
    public float ErroSimu = 0.001f;
    public int CasosDeTreinamento;
    public string PCName;
    public string NetworkName = "Teste";
    public int LayersNum = 1;
    public string LayersNumS = "1";
    public int[] NetStructure = new int[3] { 1, 2, 1 };
    public string[] NetStructureS = new string[3] { "1", "2", "1" };
    public float[] InputToCompute = new float[1] { 1f };
    public string[] InputToComputeS = new string[1] { "1" };
    public string Email1;
    public string Email2;
    public bool InputAndNetworkProblem = false;

    public Texture[] ButtonTexture = new Texture[6];
    public string[] ButtonNames = new string[6] { "Inicio", "Dados", "Rede", "Treinamento", "Visualizaçao", "Calculo" };
    public int FontSize = 12;
    public bool ScreenShotOpen = false;

    //Dados
    public string Path;
    public bool RequestPathNN;
    public bool RequestPathData;
    public string PathToData;
    public string PathToNetwork;
    public Vector2 scrollPosition;
    public string DataName;
    public float[,] DataInput = new float[1, 1];
    public float[,] DataOutput = new float[1, 1];
    public bool Serie = false;
    public bool ShowSeriePlot = false;
    public int PredictedPoints = 50;
    public string PredictedPointsS = "50";

    public float[] ResultadoTest = new float[1];
    public float DiferencaMediaTest;
    public bool Verif = false;

    public NeuralNet[] Rede = new NeuralNet[4]; //definir o numero de redes
    public bool treinar = false;
    public bool[] treinando = new bool[4];  //definir o numero de redes

    public float[] TrainnedData = new float[1];

    public bool Seno = false;
    public bool Cosseno = false;
    public bool Sigmoid = false;
    public bool Relu = false;
    public bool Swish = true;
    public bool Linear = false;
    public bool Ramp = false;
    public bool Exp = false;
    public bool Gaussian = false;
    public bool Elu = false;

    public bool SenoCheck = false;
    public bool CossenoCheck = false;
    public bool SigmoidCheck = false;
    public bool ReluCheck = false;
    public bool SwishCheck = true;
    public bool LinearCheck = false;
    public bool RampCheck = false;
    public bool ExpCheck = false;
    public bool GaussianCheck = false;
    public bool EluCheck = false;

    public string AlphaS = "1";
    public float Alpha = 1f;

    public GAStatistics GAStat;
    public bool Complete = false;
    public bool GetData = true;
    public bool DefineNet = false;
    public bool Draw = false;
    public bool Simulate = false;
    public bool Compute = false;
    public Texture2D ScreenshotIcon;
    public Texture2D IconeFechar;
    public Texture2D InputTexture;
    public Texture2D NeuronTexture;
    public Texture2D blankTexture;
    public Texture2D grayTexture;
    public bool[] ButtonPressed = new bool[6];
    //graficos
    public float[] X = new float[1];
    public float[] X2 = new float[1];
    public float[] Y = new float[1];
    public float[,] TimeSerieAndPrediction = new float[1, 1];
    public int[] TimeSerieCounter = new int[1];

    public string CodeText;
    public string CodeLanguage;
    public Vector2 scrollPos;
    public float WeightPosition = 5f;

    //Timers
    public float Timer05s = 10f;
    public float Timer10s = 2f;
    public float Timer10min = 600f;

    public FPSStat FPS;
    public UIdata TimeDetails;
    public UIdata CPUandRAM;
    public UIdata FPSDetails;
    public UIdata NetDetails;
    public UIdata DataDetails;

    public DrawNetInfo DrawDetails;

    public GrabFilePath DataFileInfo;
    public GrabFilePath NNFileInfo;

    public bool Closing = false;

    void Awake()
    {

        StartCoroutine(Common.CheckLicenceYear(FinishYearOfLicence));

        //Application.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
        System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

        DataFileInfo.Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        NNFileInfo.Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        DataFileInfo.allDrives = Directory.GetLogicalDrives();
        NNFileInfo.allDrives = Directory.GetLogicalDrives();

        Functions.CreateNetwork(Rede[0], new int[3] { 1, 2, 1 }, "Sigmoid", "Teste");

        RequestPathData = true;
        PathToData = Application.dataPath + "/Dados/DefaultData.csv";

        //RequestPathNN = true;
        //PathToNetwork = Application.dataPath + "/Redes/international-airline-passengers.csv";

        //Common.Speak("Iniciando");

        FontSize = 12 + Mathf.RoundToInt(Screen.height * Screen.width / 1000000);

        //Dependencias
        if (!Directory.Exists(Application.dataPath + "/Backup"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Backup");
        }

        if (!Directory.Exists(Application.dataPath + "/TempSpeak"))
        {
            Directory.CreateDirectory(Application.dataPath + "/TempSpeak");
        }
        else
        {
            Common.DeleteFolderContent(Application.dataPath + "/TempSpeak");
        }
    }

    void Start()
    {
        //Verifica a validade da versão
        if (Validate) Certification();

        StartCoroutine(Functions.FPSCounter(FPS));
    }

    void OnGUI()
    {

        //GUI.skin = MyGUISkin;
        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperLeft;
        FontSize = 12 + Mathf.RoundToInt(Screen.height * Screen.width / 1000000);

        ButtonPressed = Functions.ButtonGroup(Screen.width / 7, Screen.height / 50, Screen.width * 6 / 7 - 30, Screen.height / 18, 6, ButtonNames, ButtonTexture);

        if (GUI.Button(new Rect(Screen.width - 60, Screen.height / 16 + 30, 30, 30), IconeFechar))
        {
            Closing = !Closing;
            //Application.Quit();
        }

        if (GUI.Button(new Rect(Screen.width - 120, Screen.height / 16 + 30, 30, 30), ScreenshotIcon))
        {
            Common.SavePartOfScreen(0, 0, Screen.width, Screen.height, "Screenshot " + Rede[0].Name + " " + System.DateTime.Now.ToString("hh.mm.ss") + "  " + System.DateTime.Now.ToString("dd-MM-yyyy"));
            if (!ScreenShotOpen)
            {
                ScreenShotOpen = true;
                Common.ShowExplorerInside(Application.dataPath + "/Screenshots");
            }
            Common.Speak("Captura de tela salva com sucesso");
        }

        GUI.skin.label.fontSize = FontSize;

        for (int i = 0; i < 5; i++)
        {
            if (ButtonPressed[1])
            {
                if (Rede[0].Trainning) Common.Speak("Rede em treinamento ... menu desabilitado");
                GetData = true; DefineNet = false; Simulate = false; Draw = false; Compute = false;
                ButtonPressed[1] = false;
            }
            if (ButtonPressed[2])
            {
                if (Rede[0].Trainning) Common.Speak("Rede em treinamento ... menu desabilitado");
                GetData = false; DefineNet = true; Simulate = false; Draw = false; Compute = false;
                ButtonPressed[2] = false;
            }
            if (ButtonPressed[3])
            {
                GetData = false; DefineNet = false; Simulate = true; Draw = false; Compute = false;
                ButtonPressed[3] = false;
            }
            if (ButtonPressed[4])
            {
                GetData = false; DefineNet = false; Simulate = false; Draw = true; Compute = false;
                ButtonPressed[4] = false;
            }
            if (ButtonPressed[5])
            {
                GetData = false; DefineNet = false; Simulate = false; Draw = false; Compute = true;
                GenerateCode();
                PrepareComputeAndInitialError();
                ButtonPressed[5] = false;
            }
        }

        Functions.TimeDetails(TimeDetails, Screen.width / 7, Screen.height / 14 + 20, Screen.width / 9, Screen.height / 15);
        Functions.CPUandRAMUsage(CPUandRAM, 2 * (Screen.width - 30) / 7, Screen.height / 14 + 20, Screen.width / 9, Screen.height / 15);
        Functions.FPSDetails(FPSDetails, 3 * (Screen.width - 30) / 7, Screen.height / 14 + 20, Screen.width / 9, Screen.height / 15, FPS);
        GUI.skin.label.fontSize = FontSize;

        if (GetData)
        {
            GUI.Label(new Rect(30, 0.2f * Screen.height, 90, 25), "Nome do PC: ");
            PCName = GUI.TextField(new Rect(120, 0.2f * Screen.height, 200, 25), PCName, 25);

            if (Rede[0].Trainning) { GUI.enabled = false; }
            if (GUI.Button(new Rect(30, 0.2f * Screen.height + 40, 290, 25), "Carregar dados"))
            {
                //if (Validate) Certification();
                RequestPathData = true;
            }

            GUI.enabled = true;

            Functions.DrawDataDetails(DataName, DataInput, DataOutput, DataFileInfo.TargetFile, 30, Mathf.RoundToInt(0.2f * Screen.height + 80), Screen.width - 60, Mathf.RoundToInt(Screen.height - (0.2f * Screen.height + 80) - 30), DataDetails);
            GUI.skin.label.fontSize = FontSize;

            NetDetails.Casos = CasosDeTreinamento;

        }

        if (DefineNet)
        {

            Functions.TextsNetworkDetails(Rede[0], NetDetails, 2 * Screen.width / 3 - 30, 1 * Screen.height / 5, Screen.width / 3, 4 * Screen.height / 5 - 30, DataInput, DataOutput);
            GUI.skin.label.fontSize = FontSize;

            GUI.Label(new Rect(30, 0.2f * Screen.height, 90, 25), "Nome do PC: ");
            PCName = GUI.TextField(new Rect(140, 0.2f * Screen.height, 210, 25), PCName, 25);

            if (Rede[0].Trainning) { GUI.enabled = false; }
            GUI.Label(new Rect(30, 0.2f * Screen.height + 30, 160, 25), "Nome da rede neural: ");
            NetworkName = GUI.TextField(new Rect(170, 0.2f * Screen.height + 30, 180, 25), NetworkName, 25);

            if (Serie)
            {
                GUI.Label(new Rect(30, 0.2f * Screen.height + 60, 450, 30), "Número de valores de dados a cada instante no tempo: " + DataOutput.GetLength(1));
            }
            else
            {
                GUI.Label(new Rect(30, 0.2f * Screen.height + 60, 450, 30), "Número de entradas nos dados: " + DataInput.GetLength(1) + "  Número de saídas dos dados: " + DataOutput.GetLength(1));
            }

            GUI.Label(new Rect(30, 0.2f * Screen.height + 90, 350, 30), "Numero de camadas ocultas");
            LayersNumS = GUI.TextField(new Rect(30, 0.2f * Screen.height + 120, 50, 25), LayersNumS, 25);

            GUI.skin.label.fontSize = FontSize;
            if (GUI.Button(new Rect(100, 0.2f * Screen.height + 120, 100, 25), "Aplicar"))
            {
                int.TryParse(LayersNumS, out LayersNum);
                int InputsNum = NetStructure[0];
                int OutputNum = NetStructure[NetStructure.Length - 1];
                NetStructure = new int[LayersNum + 2];
                NetStructureS = new string[LayersNum + 2];
                for (int i = 0; i < NetStructure.Length; i++)
                {
                    if (i == 0) { NetStructure[0] = InputsNum; }
                    if (i == 1) { NetStructure[1] = Mathf.RoundToInt(1.4f * (float)InputsNum); }
                    if (i == NetStructure.Length - 1) { NetStructure[NetStructure.Length - 1] = OutputNum; }
                    if (i > 1 && i < NetStructure.Length - 1) { NetStructure[i] = Mathf.RoundToInt(((i) * (OutputNum) + (NetStructure.Length - 1 - i) * (NetStructure[1])) / (NetStructure.Length - 2)); }
                    NetStructureS[i] = NetStructure[i].ToString();
                }
            }
            GUI.Label(new Rect(30, 0.2f * Screen.height + 150, 350, 30), "Estrutura da rede");
            for (int i = 0; i < NetStructure.Length; i++)
            {
                NetStructureS[i] = GUI.TextField(new Rect(30 + 40 * i, 0.2f * Screen.height + 180, 30, 23), NetStructureS[i], 25);
                int.TryParse(NetStructureS[i], out NetStructure[i]);
            }
            GUI.Label(new Rect(30, 0.2f * Screen.height + 210, 350, 30), "Entradas");
            GUI.Label(new Rect(30 + 40 * (NetStructure.Length - 1), 0.2f * Screen.height + 210, 150, 30), "Saídas");
            GUI.Label(new Rect(30, 0.2f * Screen.height + 240, 350, 30), "Tipo de função ativação");
            Seno = GUI.Toggle(new Rect(30, 0.2f * Screen.height + 270, 50, 30), Seno, "Seno");
            Cosseno = GUI.Toggle(new Rect(90, 0.2f * Screen.height + 270, 70, 30), Cosseno, "Cosseno");
            Sigmoid = GUI.Toggle(new Rect(170, 0.2f * Screen.height + 270, 70, 30), Sigmoid, "Sigmoid");
            Relu = GUI.Toggle(new Rect(240, 0.2f * Screen.height + 270, 50, 30), Relu, "Relu");
            Swish = GUI.Toggle(new Rect(290, 0.2f * Screen.height + 270, 50, 30), Swish, "Swish");
            Linear = GUI.Toggle(new Rect(30, 0.2f * Screen.height + 300, 50, 30), Linear, "Linear");
            Ramp = GUI.Toggle(new Rect(90, 0.2f * Screen.height + 300, 50, 30), Ramp, "Rampa");
            Exp = GUI.Toggle(new Rect(150, 0.2f * Screen.height + 300, 50, 30), Exp, "Exp");
            Gaussian = GUI.Toggle(new Rect(200, 0.2f * Screen.height + 300, 80, 30), Gaussian, "Gaussian");
            Elu = GUI.Toggle(new Rect(290, 0.2f * Screen.height + 300, 50, 30), Elu, "Elu");

            GUI.Label(new Rect(30, 0.2f * Screen.height + 360, 350, 30), "Alfa");
            AlphaS = GUI.TextField(new Rect(30, 0.2f * Screen.height + 390, 35, 25), AlphaS, 25);
            float.TryParse(AlphaS, out Alpha);

            GUI.Label(new Rect(30, 0.2f * Screen.height + 430, 100, 30), "Reinicialização");
            if (GUI.Button(new Rect(140, 0.2f * Screen.height + 430, 60, 25), "Random"))
            {
                Functions.ReinitializeParameters(Rede[0], 2f, "Random");
                InitializeMaxMin();
                PrepareComputeAndInitialError();
            }
            if (GUI.Button(new Rect(210, 0.2f * Screen.height + 430, 60, 25), "Normal"))
            {
                Functions.ReinitializeParameters(Rede[0], 2f, "Normalization");
                InitializeMaxMin();
                PrepareComputeAndInitialError();
            }

            if (GUI.Button(new Rect(30, 0.2f * Screen.height + 470, 350, 30), "Criar Rede"))
            {
                string TextoFalado = null;
                if (Seno) Functions.CreateNetwork(Rede[0], NetStructure, "Seno", NetworkName);
                if (Cosseno) Functions.CreateNetwork(Rede[0], NetStructure, "Cosseno", NetworkName);
                if (Sigmoid) Functions.CreateNetwork(Rede[0], NetStructure, "Sigmoid", NetworkName);
                if (Relu) Functions.CreateNetwork(Rede[0], NetStructure, "Relu", NetworkName);
                if (Swish) Functions.CreateNetwork(Rede[0], NetStructure, "Swish", NetworkName);
                if (Linear) Functions.CreateNetwork(Rede[0], NetStructure, "Linear", NetworkName);
                if (Ramp) Functions.CreateNetwork(Rede[0], NetStructure, "Ramp", NetworkName);
                if (Exp) Functions.CreateNetwork(Rede[0], NetStructure, "Exp", NetworkName);
                if (Gaussian) Functions.CreateNetwork(Rede[0], NetStructure, "Gaussian", NetworkName);
                if (Elu) Functions.CreateNetwork(Rede[0], NetStructure, "Elu", NetworkName);

                if (Rede[0].TotalFreeParameters * DataOutput.GetLength(0) * DataOutput.GetLength(1) > 600000)
                {
                    TextoFalado += "A rede e os dados tem um tamanho considerável. será necessário um longo tempo de treinamento.";
                    TreinoTempo = 3f * TreinoTempo;
                    TempoTreino = TreinoTempo.ToString();
                }

                InitializeMaxMin();
                PrepareComputeAndInitialError();
                Rede[0].Name = NetworkName;
                Rede[0].alpha = Alpha;
                CasosDeTreinamento = (Rede[0].Structure[0]) * (DataInput.GetLength(0) - Rede[0].Structure[0] / DataInput.GetLength(1) - Rede[0].Structure[Rede[0].Structure.Length - 1] / DataInput.GetLength(1));
                if ((100f * (float)Rede[0].TotalFreeParameters) / ((float)NetDetails.Casos) >= 70f)
                {
                    if (TextoFalado != null) { TextoFalado += " e também, "; }
                    TextoFalado += "A rede tem muitos parâmetros e poucos dados, é aconselhavel diminuir o tamanho da rede ou aumentar a quantidade de casos de treinamento para evitar overfitting";
                }
                Common.Speak(TextoFalado);
            }

            if (GUI.Button(new Rect(30, 0.2f * Screen.height + 500, 350, 30), "Carregar rede"))
            {
                //if (Validate) Certification();
                RequestPathNN = true;
            }
            GUI.enabled = true;

        }

        if (Simulate)
        {
            if (Serie)
            { TrainnedData = Functions.PartOfArrayLine(Functions.ComputeOverDataSerie(Rede[0], DataOutput), OutputLineToGraph); }
            if (!Serie && Rede[0].Structure[0] == DataInput.GetLength(1))
            {
                TrainnedData = Functions.PartOfArrayLine(Functions.ComputeOverData(Rede[0], DataInput), OutputLineToGraph);
            }
            if (Rede[0].Structure[0] != DataInput.GetLength(1) && !InputAndNetworkProblem && !Serie)
            {
                Common.Speak("O número de entradas dos dados e da rede não coincide.");
                InputAndNetworkProblem = true;
            }
            if (Rede[0].Trainning) { GUI.enabled = false; }
            GUI.Label(new Rect(220, 0.6f * Screen.height + 30, 350, 30), "Tempo de treinamento (s)");
            TempoTreino = GUI.TextField(new Rect(220, 0.6f * Screen.height + 55, 150, 23), TempoTreino, 25);
            float.TryParse(TempoTreino, out TreinoTempo);
            GUI.Label(new Rect(220, 0.6f * Screen.height + 80, 350, 30), "Taxa de Aprendizagem");
            TaxaDeAprendizagemS = GUI.TextField(new Rect(220, 0.6f * Screen.height + 105, 150, 23), TaxaDeAprendizagemS, 25);
            float.TryParse(TaxaDeAprendizagemS, out TaxaDeAprendizagem);
            GUI.Label(new Rect(220, 0.6f * Screen.height + 130, 350, 30), "Numero de redes");
            NumeroDeRedesS = GUI.TextField(new Rect(220, 0.6f * Screen.height + 155, 150, 23), NumeroDeRedesS, 25);
            int.TryParse(NumeroDeRedesS, out NumeroDeRedes);

            //GUI.Label(new Rect(220, 0.6f * Screen.height + 180, 350, 30), "Erro admissivel: ");
            //ErroSimuS = GUI.TextField(new Rect(220, 0.6f * Screen.height + 205, 150, 23), ErroSimuS, 25);
            //float.TryParse(ErroSimuS, out ErroSimu);
            GUI.enabled = true;

            GUI.Label(new Rect(30, 0.6f * Screen.height - 30, 450, 25), "Descriçao dos dados: " + DataName);

            GUI.Label(new Rect(0.35f * Screen.width, 0.6f * Screen.height, 90, 25), "Saida gráfica: ");
            OutputLineToGraphS = GUI.TextField(new Rect(0.35f * Screen.width + 100, 0.6f * Screen.height, 50, 23), OutputLineToGraphS, 25);
            if (GUI.Button(new Rect(0.35f * Screen.width + 160, 0.6f * Screen.height, 80, 25), "Aplicar"))
            {
                int NumberOutputs = Rede[0].Structure[Rede[0].Structure.Length - 1];
                if (Serie && NumberOutputs / DataOutput.GetLength(1) > 1)
                {
                    if (DataOutput.GetLength(1) == 1)
                        Common.Speak("Há um problema no gráfico quando tenta-se prever mais um passo no tempo, por favor, utilize apenas " + DataOutput.GetLength(1).ToString() + " saída para a rede");
                    if (DataOutput.GetLength(1) > 1)
                        Common.Speak("Há um problema no gráfico quando tenta-se prever mais um passo no tempo, por favor, utilize apenas " + DataOutput.GetLength(1).ToString() + " saídas para a rede");
                }
                int.TryParse(OutputLineToGraphS, out OutputLineToGraph);
            }
            GUI.Label(new Rect(0.35f * Screen.width, 0.6f * Screen.height + 30, 90, 25), "Filtro: ");
            if (GUI.Button(new Rect(0.35f * Screen.width + 60, 0.6f * Screen.height + 30, 80, 25), "Aplicar"))
            {
                DataOutput = Functions.filter(DataOutput, "Savitzky Golay", 1);
                Common.Speak("Aplicando filtro de ruido");
            }

            SendEmailTrained = GUI.Toggle(new Rect(0.35f * Screen.width, 0.6f * Screen.height + 60, 280, 25), SendEmailTrained, "Enviar Email ao concluir treinamento");
            GUI.Label(new Rect(0.35f * Screen.width, 0.6f * Screen.height + 90, 300, 25), "Emails: ");
            Email1 = GUI.TextField(new Rect(0.35f * Screen.width, 0.6f * Screen.height + 120, 250, 23), Email1, 50);
            Email2 = GUI.TextField(new Rect(0.35f * Screen.width, 0.6f * Screen.height + 150, 250, 23), Email2, 50);

            ///Sobre series
            if (Serie)
            {
                GUI.Label(new Rect(0.35f * Screen.width, 0.6f * Screen.height + 180, 300, 25), "Series: ");
                if (GUI.Button(new Rect(0.35f * Screen.width, 0.6f * Screen.height + 210, 250, 25), "Mudar gráfico"))
                {
                    ShowSeriePlot = !ShowSeriePlot;
                }
                GUI.Label(new Rect(0.35f * Screen.width, 0.6f * Screen.height + 240, 130, 25), "Número de pontos: ");
                PredictedPointsS = GUI.TextField(new Rect(0.35f * Screen.width + 120, 0.6f * Screen.height + 240, 130, 23), PredictedPointsS, 25);
                int.TryParse(PredictedPointsS, out PredictedPoints);
                if (PredictedPoints > 120)
                {
                    PredictedPoints = 120;
                    PredictedPointsS = PredictedPoints.ToString();
                }
            }


            GUI.Label(new Rect(30, 0.6f * Screen.height, 450, 25), "Descriçao da rede: " + Rede[0].Name);
            if (GUI.Button(new Rect(30, 0.6f * Screen.height + 30, 160, 25), "Treinar Rede"))
            {
                if (Validate) Certification();
                Complete = false;
                if (!Serie)
                    StartCoroutine(Functions.TrainNetworkGA(Rede[0], GAStat, DataInput, DataOutput, TaxaDeAprendizagem, TreinoTempo, ErroSimu, NumeroDeRedes));
                //StartCoroutine(Functions.TrainNetworkBP(Rede[0], DataInput, DataOutput, TaxaDeAprendizagem, TreinoTempo, Erro));
                if (Serie)
                    StartCoroutine(Functions.TrainNetworkSerieGA(Rede[0], GAStat, DataOutput, TaxaDeAprendizagem, TreinoTempo, ErroSimu, NumeroDeRedes));
            }
            if (GUI.Button(new Rect(30, 0.6f * Screen.height + 60, 160, 25), "Parar aprendizado"))
            {
                Functions.DestroyCoroutiners();
                Rede[0].Trainning = false;
                Rede[0].Status = "NotTrainedEnough";
            }
            if (GUI.Button(new Rect(30, 0.6f * Screen.height + 90, 160, 25), "Reiniciar Rede"))
            {
                Functions.DestroyCoroutiners();
                Functions.CreateNetwork(Rede[0], Rede[0].Structure, Rede[0].ActivationFunction, Rede[0].Name);
                Rede[0].Trainning = false;
                Rede[0].Status = "Initialized";
            }

            if (GUI.Button(new Rect(30, 0.6f * Screen.height + 120, 160, 25), "Salvar rede"))
            {
                //if (Validate) Certification();
                if (Serie)
                {
                    Functions.SaveNN(Application.dataPath + "/Redes/Backup da rede " + Rede[0].Name + ".csv", Rede[0], Functions.ComputeOverDataSerie(Rede[0], DataInput));
                }
                if (!Serie)
                {
                    Functions.SaveNN(Application.dataPath + "/Redes/Backup da rede " + Rede[0].Name + ".csv", Rede[0], Functions.ComputeOverData(Rede[0], DataInput));
                }

            }
            if (GUI.Button(new Rect(30, 0.6f * Screen.height + 150, 160, 25), "Carregar rede"))
            {
                //if (Validate) Certification();
                RequestPathNN = true;
            }

            Functions.DrawNetworkDetails(Rede[0], 2 * Screen.width / 3 - 30, 6 * Screen.height / 10 - 30, Screen.width / 3, 4 * Screen.height / 10, TreinoTempo);
            GUI.skin.label.fontSize = FontSize;

            X = Functions.PartOfArrayLine(DataInput, 0);

            if (!Serie)
            {
                Y = Functions.PartOfArrayLine(DataOutput, OutputLineToGraph);
                X2 = Functions.PartOfArrayLine(DataInput, 0);
            }
            if (Serie)
            {
                Y = Functions.PartOfArrayLine(DataOutput, OutputLineToGraph);//corrigir posteriormente
                X2 = Functions.RemoveInitialNumbers(X, Rede[0].Structure[0] / DataOutput.GetLength(1));
            }
            //Mostrar gráfico

            if (!ShowSeriePlot && (Serie || (Rede[0].Structure[0] == DataInput.GetLength(1) && !Serie)))
            {
                Rect frameRect = new Rect(30, 8 * Screen.height / 55 + 30, Screen.width - 60, Mathf.RoundToInt(0.35f * Screen.height));
                GUI.BeginGroup(frameRect);
                Functions.Plot("Dados: linha " + OutputLineToGraph.ToString(), "Tempo", "Valor\n" + "(unid)", 30, 8 * Screen.height / 55 + 30, Screen.width - 60, Mathf.RoundToInt(0.35f * Screen.height), "Experimental", "Modelo Neural", X, Y, X2, TrainnedData, Color.white, Color.red, 0.55f * Color.blue + 0.25f * Color.green + 0.2f * Color.white);
                GUI.EndGroup();
                GUI.skin.label.fontSize = FontSize;
            }
            else
            {
                GUI.Label(new Rect(30 + 0.35f * Screen.width, 0.3f * Screen.height, 450, 25), "Número de entradas dos dados e da rede não coincide");
            }
            //Mostrar gráfico série
            if (ShowSeriePlot && Serie)
            {
                Functions.PlotSeriePrediction("Previsão", "Pontos", "Valor\n" + "(unid)", 30, 8 * Screen.height / 55 + 30, Screen.width - 60, Mathf.RoundToInt(0.35f * Screen.height), TimeSerieCounter, TimeSerieAndPrediction, OutputLineToGraph, DataInput.GetLength(0), TimeSerieAndPrediction);
            }
            GUI.skin.label.fontSize = FontSize;

            if (Serie)
            {
                if (GUI.Button(new Rect(30, 0.6f * Screen.height + 180, 160, 25), "Salvar previsão no Log"))
                {
                    Functions.PredictedPoints(Rede[0], DataOutput, PredictedPoints, out TimeSerieAndPrediction);
                    TimeSerieCounter = Functions.CreateCounter(TimeSerieAndPrediction);
                    Functions.AddTextToFile(Application.dataPath + "/Log.txt", " ");
                    Functions.Log("Prediction: " + Functions.Show2DArray(TimeSerieAndPrediction));
                    Functions.AddTextToFile(Application.dataPath + "/Log.txt", " ");
                }
            }
        }

        if (Draw)
        {
            Functions.DrawNetwork(Rede[0], 30, 10 * Screen.height / 55, Screen.width - 60, Screen.height * 5 / 6 - 30, InputTexture, NeuronTexture, blankTexture, DrawDetails);
            GUI.skin.label.fontSize = FontSize;

        }

        if (Compute)
        {
            GUI.Label(new Rect(30, 0.2f * Screen.height, 350, 25), "Resultados: ");
            GUI.Label(new Rect(30, 0.2f * Screen.height + 30, 350, 30), "Entradas");
            for (int i = 0; i < Rede[0].Structure[0]; i++)
            {
                InputToComputeS[i] = GUI.TextField(new Rect(30 + 40 * i, 0.2f * Screen.height + 60, 30, 23), InputToComputeS[i], 25);
                float.TryParse(InputToComputeS[i], out InputToCompute[i]);
            }
            GUI.Label(new Rect(30, 0.2f * Screen.height + 90, 350, 30), "Saidas");
            ResultadoTest = Functions.ComputeNetwork(Rede[0], InputToCompute, false);
            GUI.Label(new Rect(30, 0.2f * Screen.height + 115, Screen.width - 60, 60), Functions.ShowArrayCounter(ResultadoTest));

            if (GUI.Button(new Rect(30, 0.2f * Screen.height + 180, 160, 25), "Abrir pasta do programa"))
            {
                Common.ShowExplorerSelected(Application.dataPath + "/Log.txt");
            }

            if (GUI.Button(new Rect(30, 0.2f * Screen.height + 210, 160, 25), "Abrir Log"))
            {
                Process.Start("notepad.exe", Application.dataPath + "/Log.txt");
            }

            if (GUI.Button(new Rect(30, 0.2f * Screen.height + 240, 160, 25), "Abrir pasta de Backup"))
            {
                Common.ShowExplorerInside(Application.dataPath + "/Backup");
            }

            if (GUI.Button(new Rect(30, 0.2f * Screen.height + 270, 160, 25), "Abrir Screenshots"))
            {
                Common.ShowExplorerInside(Application.dataPath + "/Screenshots");
            }

            GUI.Label(new Rect(210, 0.2f * Screen.height + 180, 280, 25), "Exportação para outras linguagens:");

            if (GUI.Button(new Rect(210, 0.2f * Screen.height + 210, 120, 25), "Copiar código"))
            {
                if (Rede[0].TotalFreeParameters * Rede[0].Structure.Length < 3050)
                {
                    CodeText = Functions.GenerateNetworkCode(Rede[0], CodeLanguage);
                    TextEditor te = new TextEditor();
                    te.text = CodeText;
                    te.SelectAll();
                    te.Copy();
                    if (CodeLanguage == "") CodeLanguage = "genérica";
                    if (CodeLanguage == "MATLAB") CodeLanguage = "MATILABI";
                    Common.Speak("código gerado e copiado na linguagem " + CodeLanguage + " . aperte control vê no destino");
                    if (CodeLanguage == "MATILABI") CodeLanguage = "MATLAB";
                    if (CodeLanguage == "genérica") CodeLanguage = "";
                }
                if (Rede[0].TotalFreeParameters * Rede[0].Structure.Length > 3050)
                {
                    CodeText = null;
                    Common.Speak("O computador atual não tem capacidade de gerar o código");
                }
            }

            GUI.Label(new Rect(350, 0.2f * Screen.height + 210, 80, 25), "Linguagem:");
            if (GUI.Button(new Rect(430, 0.2f * Screen.height + 210, 80, 25), "Genérica"))
            {
                CodeLanguage = "";
            }
            if (GUI.Button(new Rect(520, 0.2f * Screen.height + 210, 80, 25), "MATLAB"))
            {
                CodeLanguage = "MATLAB";
            }
            if (GUI.Button(new Rect(610, 0.2f * Screen.height + 210, 80, 25), "C#"))
            {
                CodeLanguage = "C#";
            }
            if (GUI.Button(new Rect(700, 0.2f * Screen.height + 210, 80, 25), "C++"))
            {
                CodeLanguage = "C++";
            }

            if (CodeText.Length > 16010 || Rede[0].TotalFreeParameters * Rede[0].Structure.Length >= 310)
            {
                GUI.Label(new Rect(210, 0.2f * Screen.height + 240, 280, 25), "Rede muito grande para demonstrar o código");
            }

            if (CodeText.Length <= 16010)
            {
                GUILayout.BeginArea(new Rect(210, 0.2f * Screen.height + 240, Screen.width - 240, 0.8f * Screen.height - 270));
                scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width(Screen.width - 240), GUILayout.Height(0.8f * Screen.height - 280));
                GUILayout.Label(CodeText);
                GUILayout.EndScrollView();
                GUILayout.EndArea();
            }
        }

        if (RequestPathData)
        {
            if (PathToData != string.Empty)
            {
                Functions.LoadData(PathToData, out DataName, out DataInput, out DataOutput);
                DataFileInfo.TargetFile = PathToData;
                DataFileInfo.OutputPath = PathToData;
                if (DataName.Contains("Serie") || DataName.Contains("Série") || DataName.Contains("serie") || DataName.Contains("série") || DataName.Contains("Series") || DataName.Contains("Serie"))
                { Serie = true; }
                if (!DataName.Contains("Serie") && !DataName.Contains("Série") && !DataName.Contains("serie") && !DataName.Contains("série") && !DataName.Contains("Series") && !DataName.Contains("Serie"))
                { Serie = false; }
                PrepareComputeAndInitialError();
                PathToData = string.Empty;
                RequestPathData = false;
                CasosDeTreinamento = (Rede[0].Structure[0]) * (DataInput.GetLength(0) - Rede[0].Structure[0] / DataInput.GetLength(1) - Rede[0].Structure[Rede[0].Structure.Length - 1] / DataInput.GetLength(1));
            }
            else
            {
                PathToData = Common.GetPath((Screen.width - 800) / 2, (Screen.height - 600) / 2, 800, 600, "csv", DataFileInfo, out RequestPathData);
            }
        }

        if (RequestPathNN)
        {
            if (PathToNetwork != string.Empty)
            {
                Functions.LoadNN(PathToNetwork, Rede[0]);
                NNFileInfo.TargetFile = PathToNetwork;
                NNFileInfo.OutputPath = PathToNetwork;
                PrepareComputeAndInitialError();
                PathToNetwork = string.Empty;
                RequestPathNN = false;
                CasosDeTreinamento = (Rede[0].Structure[0]) * (DataInput.GetLength(0) - Rede[0].Structure[0] / DataInput.GetLength(1) - Rede[0].Structure[Rede[0].Structure.Length - 1] / DataInput.GetLength(1));
            }
            else
            {
                PathToNetwork = Common.GetPath((Screen.width - 800) / 2, (Screen.height - 600) / 2, 800, 600, "csv", NNFileInfo, out RequestPathNN);
            }
        }

        if (Closing)
        {
            ShowClosingWindow();
        }

        //Limpar recursos nao utilizados
        Resources.UnloadUnusedAssets();
    }

    void Update()
    {
        if (!Rede[0].Trainning && Rede[0].Status == "Trained" && !Complete && SendEmailTrained && (Email1 != "" || Email2 != ""))
        {
            Functions.DestroyCoroutiners();
            float[,] TrainnedData = new float[1, 1];
            if (Serie)
            {
                TrainnedData = Functions.ComputeOverDataSerie(Rede[0], DataInput);
            }
            if (!Serie)
            {
                TrainnedData = Functions.ComputeOverData(Rede[0], DataInput);
            }
            Functions.SaveNN(Application.dataPath + "/Redes/RedeTreinada.csv", Rede[0], TrainnedData);
            string[] SendTo = { Email1, Email2 };
            string[] Atached = { "/Redes/RedeTreinada.csv" };
            string CorpoEmail = "Nome do computador: " + PCName + "\n\n"
                                + "Rede ajustada: " + Rede[0].Name + " Estrutura: " + Functions.ShowArrayInt(Rede[0].Structure) + "\n\n"
                                + "Erro: " + Rede[0].NetworkError.ToString() + "\n"
                                + "R2: " + Rede[0].RSquare.ToString() + " R2 ajustado: " + Rede[0].RSquareAdju.ToString() + "\n"
                                + "Iteraçoes: " + Rede[0].NumberIterations.ToString() + "\n"
                                + "Tempo de treinamento: " + Rede[0].TrainningTime.ToString() + " seg \n"
                                + "\n\n\n"
                                + "Resultado esperado: " + Functions.ShowArray(ResultadoTest) + "\n"
                                + "\n\n\n"
                                + "Nome do dispositivo: " + SystemInfo.deviceName + "\n"
                                + "IP: " + Common.LocalIpAddress() + "  (" + Common.PlayerCountry() + ")\n"
                                + "Sistema operacional: " + Common.WindowsVersion() + "\n\n";
            Common.SendEmail(SendTo, "Resultado do ajuste neural " + Rede[0].Name + "  -Computador: " + PCName, CorpoEmail, Atached, false);
            Common.Speak("E-mail com o resultado do treinamento enviado");
            Complete = true;
        }

        //A cada 0.5 segundos
        if (Time.time >= Timer05s)
        {
            if (Serie && Simulate)
            {
                Functions.PredictedPoints(Rede[0], DataOutput, PredictedPoints, out TimeSerieAndPrediction);
                TimeSerieCounter = Functions.CreateCounter(TimeSerieAndPrediction);
            }
            if (Compute && Rede[0].TotalFreeParameters * Rede[0].Structure.Length <= 100)
            {
                CodeText = Functions.GenerateNetworkCode(Rede[0], CodeLanguage);
            }
            if (GetData)
            {
                CasosDeTreinamento = (Rede[0].Structure[0]) * (DataInput.GetLength(0) - Rede[0].Structure[0] / DataInput.GetLength(1) - Rede[0].Structure[Rede[0].Structure.Length - 1] / DataInput.GetLength(1));
            }

            Timer05s += 0.5f;
        }


        //A cada 10 segundos
        if (Time.time >= Timer10s)
        {

            FontSize = 12 + Mathf.RoundToInt(Screen.height * Screen.width / 1000000);

            DataFileInfo.allDrives = Directory.GetLogicalDrives();
            NNFileInfo.allDrives = Directory.GetLogicalDrives();

            if (Compute && Rede[0].TotalFreeParameters * Rede[0].Structure.Length > 100 && Rede[0].TotalFreeParameters * Rede[0].Structure.Length < 310)
            {
                CodeText = Functions.GenerateNetworkCode(Rede[0], CodeLanguage);
            }

            Timer10s += 10f;
        }

        //A cada 10 min
        if (Time.time >= Timer10min)
        {
            if (Rede[0].Trainning)
            {
                Common.Speak("Um Backup da rede em treinamento foi salvo");
                SaveNetworkBackup();
            }
            Timer10min += 600f;
        }
    }

    public void SaveNetworkBackup()
    {
        float[,] TrainnedData = new float[1, 1];
        if (!Serie)
        {
            TrainnedData = Functions.ComputeOverData(Rede[0], DataInput);
        }
        if (Serie)
        {
            TrainnedData = Functions.ComputeOverDataSerie(Rede[0], DataInput);
        }
        Functions.SaveNN(Application.dataPath + "/Backup/" + Rede[0].Name + " "
                         + System.DateTime.Now.ToString("hh.mm.ss") + " "
                         + System.DateTime.Now.ToString("dd-MM-yyyy") + ".csv", Rede[0], TrainnedData);
    }

    public void GenerateCode()
    {
        if (Rede[0].TotalFreeParameters * Rede[0].Structure.Length < 310)
        {
            CodeText = Functions.GenerateNetworkCode(Rede[0], CodeLanguage);
        }
    }

    public void InitializeMaxMin()
    {
        if (Serie)
        {
            Rede[0].InputMin = Functions.Min2D(DataOutput);
            Rede[0].InputMax = Functions.Max2D(DataOutput);
            Rede[0].OutputMin = Functions.Min2D(DataOutput);
            Rede[0].OutputMax = Functions.Max2D(DataOutput);
        }
        if (!Serie)
        {
            Rede[0].InputMin = Functions.Min2D(DataInput);
            Rede[0].InputMax = Functions.Max2D(DataInput);
            Rede[0].OutputMin = Functions.Min2D(DataOutput);
            Rede[0].OutputMax = Functions.Max2D(DataOutput);
        }
    }

    public void ShowClosingWindow()
    {
        int PosX = Screen.width / 3;
        int PosY = Screen.height / 3;
        int SizeX = PosX;
        int SizeY = PosY;
        //criar quadro
        GUI.DrawTexture(new Rect(PosX, PosY, SizeX, SizeY), grayTexture);
        Functions.DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX, PosY + SizeY), blankTexture, 1);
        Functions.DrawLineStretched(new Vector2(PosX, PosY), new Vector2(PosX + SizeX, PosY), blankTexture, 1);
        Functions.DrawLineStretched(new Vector2(PosX + SizeX, PosY), new Vector2(PosX + SizeX, PosY + SizeY), blankTexture, 1);
        Functions.DrawLineStretched(new Vector2(PosX, PosY + SizeY), new Vector2(PosX + SizeX, PosY + SizeY), blankTexture, 1);
        int squareSize = 1 + Mathf.RoundToInt(SizeY * Screen.width / 200000);
        GUI.DrawTexture(new Rect(PosX, PosY, squareSize, squareSize), blankTexture);
        GUI.DrawTexture(new Rect(PosX + SizeX - squareSize, PosY, squareSize, squareSize), blankTexture);
        GUI.DrawTexture(new Rect(PosX, PosY + SizeY - squareSize, squareSize, squareSize), blankTexture);
        GUI.DrawTexture(new Rect(PosX + SizeX - squareSize, PosY + SizeY - squareSize, squareSize, squareSize), blankTexture);

        GUI.skin.label.fontSize = 3 * FontSize / 2;
        GUI.skin.button.fontSize = 3 * FontSize / 2;
        GUI.Label(new Rect(PosX + SizeX * 0.29f, PosY + SizeY / 5, FontSize * 22, FontSize * 3), "Deseja fechar o programa?");
        if (GUI.Button(new Rect(PosX + SizeX / 5, PosY + 3 * SizeY / 5, FontSize * 8, FontSize * 3), "Sim"))
        {
            SaveNetworkBackup();
            Application.Quit();
        }
        if (GUI.Button(new Rect(PosX + 3 * SizeX / 5, PosY + 3 * SizeY / 5, FontSize * 8, FontSize * 3), "Não"))
        {
            Closing = false;
        }

        GUI.skin.button.fontSize = FontSize;
        //GUI.skin.label.fontSize = 12;
        //GUI.skin.textField.fontSize = 12;
    }

    public void PrepareComputeAndInitialError()
    {
        InputAndNetworkProblem = false;

        int NumberInputs = Rede[0].Structure[0];
        int NumberOutputs = Rede[0].Structure[Rede[0].Structure.Length - 1];

        if ((NumberInputs != DataInput.GetLength(1) || NumberOutputs != DataOutput.GetLength(1)) && !Serie)
        {
            Common.Speak("O número de entradas dos dados e da rede não coincide.");
            InputAndNetworkProblem = true;
        }
        if ((NumberInputs % DataOutput.GetLength(1) != 0 || NumberOutputs % DataOutput.GetLength(1) != 0) && Serie)
        {
            Common.Speak("O número de entradas dos dados e da rede não coincide.");
            InputAndNetworkProblem = true;
        }

        InputToComputeS = new string[DataInput.GetLength(1)];
        InputToCompute = new float[DataInput.GetLength(1)];
        if (!Serie)
        {
            Functions.CopyFinalNumbers(DataInput, InputToCompute);

            if (!InputAndNetworkProblem) Rede[0].NetworkError = Functions.InitialError(Rede[0], DataInput, DataOutput);
        }
        if (Serie)
        {
            if (!InputAndNetworkProblem)
            {
                float[,] ErrorInput;
                float[,] ErrorOutput;
                Functions.SerieToInputOutput(DataOutput, NumberInputs, NumberOutputs, out ErrorInput, out ErrorOutput);
                Rede[0].NetworkError = Functions.InitialError(Rede[0], ErrorInput, ErrorOutput);
            }

            InputToCompute = Functions.LastInputToPredict(DataOutput, Rede[0].Structure[0]);
        }
        InputToComputeS = Functions.ArrayFloatToString(InputToCompute);

        Rede[0].TrainNetworkError = Rede[0].NetworkError;
    }

    public void Certification()
    {
        if (Common.CheckForInternetConnection())
        {
            ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
            WebClient Client = new WebClient();
            string result = Client.DownloadString("https://drive.google.com/file/d/1Q8TAIx9eCo4jHZPs3uzhnX-83RYx_Ycl/view?usp=sharing");
            if (result.Contains("True465bdu6843vdju645"))
            {
                if (result.Contains("1.1Version") || result.Contains("1.2Version") || result.Contains("1.3Version") || result.Contains("1.4Version") || result.Contains("1.5Version") || result.Contains("1.6Version") || result.Contains("1.7Version") || result.Contains("1.8Version") || result.Contains("1.9Version") || result.Contains("2.0Version"))
                {
                    UpdateVersionMesage();
                    if (Environment.Is64BitOperatingSystem)
                        Process.Start("chrome.exe", "https://drive.google.com/file/d/1YGhexP95jv8BI-7a4WPcSGO3kdMZ4W6Z/view?usp=sharing"); //atualizar 64 bits
                    if (!Environment.Is64BitOperatingSystem)
                        Process.Start("chrome.exe", "https://drive.google.com/file/d/1TgjWUzzh5FWLv_Wh7D0eh64oNZkzfGxR/view?usp=sharing"); //atualizar 32 bits
                    Application.Quit();
                }
            }
            else
            {
                Common.Speak("Software ilegal, falar com Josivan Pedro no número 9 8 5 0 2 5 3 5 9");
                Application.Quit();
            }
        }
        else
        {
            Common.Speak("Internet indisponivel");
            Application.Quit();
        }
    }

    public void UpdateVersionMesage()
    {
        Common.Speak("Esta versão está desatualizada. Você será direcionado para a página de download da versão mais recente");
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

}
