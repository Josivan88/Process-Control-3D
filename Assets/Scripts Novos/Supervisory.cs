using System;
using System.IO;
using System.Text;
using System.Collections;
using System.IO.Ports; //Conexão com as portas
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;
using Reusable;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using UnityEditor;

namespace Supervisory
{

    [System.Serializable]
    public class ProcessDetails
    {
        [Header("Process information")]
        public string CompanyName;                                  //Nome da empresa
        public Texture2D CompanyLogo;                               //Logo da empresa formato 4x1
        [HideInInspector]
        public Rect CompanyLogoRect;                                //Moldura da logo
        public string Name;                                         //Nome
        public string Code = "PROC001";                             //Código do processo
        public string Description;                                  //Descrição
        public enum myEnum                                          //Que tipo de dispositivo?
        {
            Arduino,
            ESP8266
        };
        public myEnum PLCType = myEnum.Arduino;                     // this public var should appear as a drop down
        public bool Connected = false;                              //Verifica se o processo e stá conectado de qualquer forma
        public LocalData LocalData;                                 //Dados locais de clima, ip, etc...
        public ProcessConnection CableConnection;                   //Dados da conexão via cabo
        public PhotonRemoteConnection RemoteProcessConnection;      //Dados da conexão remota (photon Engine)
        public ProcessWifiConnection LocalWifiProcessConnection;    //Conexão local via wifi ao processo
        public InternetConnection WebConnection;                    //Verifica a conexão com a internet
        public Battery DeviceBattery;                               //Bateria do dispositivo
        public Sensor[] Sensors;                                    //Sensores
        public Atuador[] Atuators;                                  //Atuadores
        public PID[] PIDs;                                          //PIDs do processo
        public Emails Emails;                                       //Dados de envio de e-mails
        public float Time;                                          //Tempo decorrido desde o inicio (s)
        public string TimeHour;                                     //Hora atual
        public string TimeDate;                                     //Data atual
        public float TotalPowerConsumption;                         //Estimativa da potência total consumida atualmente

    }

    [System.Serializable]
    public class Emails
    {
        public string[] EmailsList;                          //Emails para receber informações
        public bool SendEmail = false;                       //Habilita envio de email
        public float SendInterval = 24f;                     //Intervalo de tempo em HORAS para envio de email periodicos
        public string SendIntervalString = "24";             //Intervalo de tempo em String em HORAS para envio de email periodicos
        public float SendMarkInterval = 3600f;               //Marcador para envio de email em SEGUNDOS
        public int SendedEmails = 0;                       //Total de emails enviados
    }
    [System.Serializable]
    public class LocalData
    {
        public string LocationFrom;                           //Site para pegar a localização
        public string LocationCountry;                        //Pais
        public string LocationState;                          //Estado
        public string LocationCity;                           //Cidade
        public string LocationLatitude;                       //Latitude
        public string LocationLongitude;                      //Longitude
        public string LocationIPAdress;                       //IP
        public string WeatherFrom;                            //Site para pegar dados de clima
        public string TempActual;                             //Temperatura atual
        public float PercentageHumidity;                      //Humidade
        public float WindSpeedKmh;                            //Velocidade do vento
        public float TempMorning;                             //Temperatura pela manhã
        public string MorningWeather;                         //céu pela manhã
        public float TempAfternoon;                           //Temperatura pela tarde
        public string AfternoonWeather;                       //céu pela tarde
        public float TempEvening;                             //Temperatura pela noite
        public string EveningWeather;                         //céu pela noite
        public float TempOvernight;                           //Temperatura pela madrugada
        public string OvernightWeather;                       //céu pela madrugada
    }

    [System.Serializable]
    public class VirtualAssistant
    {
        [Header("Enhanced Virtual Assistant")]
        public string Name="Eva";                               //Nome
        public float DangerLevel=1f;                            //Nível de perigo do processo: 0 Perigo, 0,5 moderado, 1 tudo OK
        public bool FirstPresentation = false;                  //Já se apresentou pela primeira vez?
        public Rect ClickRect = new Rect(0, 0, 100, 100);       //Área de clique
        public bool MouseOver = false;                          //Mouse está sobre a área?
        public bool MouseEnter = false;                         //Mouse entrou na área?
        public bool MouseClick = false;                         //Mouse clicou na área?
        public GameObject AssistantMainObject;                  //Objeto 3D que representa a assistente
        public string SpeakText;                                //texto para fala
        public float InitialTimeOfCheck = 60f;                  //tempo inicial de checagem do processo, duplicado a cada ocorrência
        public bool FirstCheckDone = false;                     //Primeira checagem concluida?
        public float UpdateTimeInterval = 5.1f;                 //Intervalo de atualização
        public float UpdateTimeIntervalMark = 5.1f;             //marcador do intervalo de atualização
        public Process SpeechRecognitionProcess;                //Processo externo de reconhecimento de voz
        public bool SpeechRecognitionProcessActive;             //Processo externo de reconhecimento de voz ativo?
    }

    [System.Serializable]
    public class Sensor
    {
        public string Name;                                         //Nome
        public string Number;                                       //número de referencia do sensor
        public bool AutoNumber = true;                              //número automático de referencia do sensor?
        public string Description;                                  //Descrição
        public string SensorClass;                                  //Classe: termometro, barometro...
        public string Unit;                                         //Unidade
        public string SpokeUnit;                                    //Unidade falada
        public float ActualValue;                                   //Valor atual no sensor
        public bool Virtual = false;                                //Soft sensor?
        public GameObject[] Objects;                                //Conjunto de objetos 3D que representa o sensor
        public bool PIDControled = false;                           //Controlado por PID?
        public int PIDNumber;                                       //Qual PID?
        public float SetPoint;                                      //Set point definido pelo PID
        public bool AlertPlayed = false;                            //O alerta já foi dado?
        public float MinSecureValue;                                //Valor mínimo seguro do sensor
        public bool MinSecValueAlert=true;                          //usar alerta ao ultrapassar limite?
        public bool MinSecValueCritical;                            //usar alerta contínuo ao ultrapassar limite?
        public float MaxSecureValue;                                //Valor máximo seguro do sensor
        public bool MaxSecValueAlert=true;                          //usar alerta ao ultrapassar limite?
        public bool MaxSecValueCritical;                            //usar alerta continuo ao ultrapassar limite?
        public float SecurityMargin = 15f;                          //Valor em porcentagem de proximidade dos limites, para definir um estado de alerta amarelo para o sensor
        public float SensorStatus = 1f;                             //Status do sensor: 0-ruim, 0,5-tem problemas, 1 - OK
        public bool Focus = false;                                  //Sensor precisa atualizar graficos e texturas?
        public int HistoryLineInFocus = 0;                          //Linha em foco para atualização de dados gráficos
        public HistoricData[,] History = new HistoricData[4,200];   //Histórico de valores. modificações, taxas ... a cada 10min, 1hora, 10horas, 10dias
        public float[] NormalizedValueTexture = new float[200];     //Valores da linha em foco normalizados [min,max]->[0,1]
        public float[] ModificationTexture = new float[200];        //boleanos de modificação da linha em foco [v,f]->[0,1]
        public Texture2D[] HistoryTextures = new Texture2D[2];      //Texturas para representar visualmente as modificações [0], e os valores [1]
        public float TimeLastUpdateMark;
        public float TimeSinceLastUpdate;                           //Tempo desde a ultima atualização
        public bool FirstReceive = true;                            //Primeira vez que um dado é recebido?
        public bool ReqNetUpdate = false;                           //Peça para atualizar computadores conectados

        public string StartDate;                                    //data e hora da primeira mediçao do sensor I
        public float MeanValue;                                     //media desde o inicio de operação do sensor I
        public string DateMeanValue;                                //data e hora da ultima mediçao da media
        public float MinValue;                                      //valor minimo do sensor I desde o inicio
        public string DateMinValue;                                 //data e hora do momento de minimo
        public float MaxValue;                                      //valor maximo do sensor I desde o inicio
        public string DateMaxValue;                                 //data e hora do momento de maximo
        public float Rate;                                          //Taxa atual de variação
        public UIButtonData HistoricChart;                          //Botão para habilitar gráfico e dados do histórico
        public UIButtonData Limits;                                 //Botão para o editor de limites de um sensor
        public UIButtonData ResetButton;                            //Botão para resetar o sensor
        public UIToggleData MinAlert;
        public UIToggleData MinCritical;
        public UIToggleData MaxAlert;
        public UIToggleData MaxCritical;
        public UIComboBoxData ComboBoxHistory;                      //ComboBox de escolha do intervalo de tempo do gráfico
        public ChartData Chart;                                     //Armazena dados do gráfico
        public UISensorData SensorUI;                               //Informações da interface

    }

    [System.Serializable]
    public class HistoricData
    {
        public bool Modification;     //Histórico de modificações
        public int ModifiedAtuator;   //Atuador modificado?
        public float OldValue;
        public float NewValue;
        public float Time;
        public float Values;          //Histórico de valores
        public float Rates;           //Histórico de taxas
        public string Dates;          //Histórico de Datas
    }

    [System.Serializable]
    public class UISensorData
    {
        public string Name;
        public string Text;
        public Texture2D Icon;
        [Range(1, 100)]
        public int TransitionSpeed = 10;
        public int PosX;
        public int TargetPosX;
        public int PosY;
        public int TargetPosY;

        public int SizeX;
        public int TargetSizeX;
        public int SizeY;
        public int TargetSizeY;

        public Vector2 HUDPointer;
        public Vector2 ObjectPos2D;
        public bool DrawPointer;

        public bool MouseOver = false;
        public bool MouseEnter = false;
        public bool MouseClick = false;
        public bool Toggle = false;
        public bool Selected = false;
        public bool Simplified = false;
        public bool Short = false;
        public bool PlaySound = true;
        public float SoundIntensity = 0.4f;

        public bool FontChanged = false;
        public int LocalFontSize = 12;
        public bool Changed = true;
        public Rect[] AllRectInfo = new Rect[20];

        [HideInInspector]
        public float ClickTime = 0f;
        [HideInInspector]
        public float WaitingTime = 0.1f;

        public int BarSubdivisions = 4;

        public UIButtonData ExpandButton;

    }

    [System.Serializable]
    public class Atuador
    {
        public string Name;                                         //Nome
        public string Number;                                       //número de referencia do sensor
        public bool AutoNumber = true;                              //número automático de referencia do sensor?
        public string Description;                                  //Descrição
        public string AtuadorClass;                                 //Classe: bombeamento, aquecimento...
        public string Unit = "%";                                   //Unidade
        public string SpokeUnit = "Porcento";                       //Unidade falada
        public float ActualValue;                                   //Valor atual no sensor
        public bool ActualStatus;                                   //Atualmente On ou Off?
        public bool FirstReceive = true;                            //É a primeira vez que recebe informações?
        public bool ReqNetUpdate = false;                           //Peça para atualizar computadores conectados
        public GameObject[] Objects;                                //Conjunto de objetos 3D que representa o atuador
        public bool PIDControled = false;                           //Controlado por PID?
        public int PIDNumber;                                       //Qual PID?
        public float AtuadorStatus = 1f;                            //Status do atuador: 0-ruim, 0,5-tem problemas, 1 - OK
        public bool PWM;                                            //Atuador é do tipo PWM?
        public bool OnOff;                                          //Atuador é do tipo On/Off?
        public bool UsesRelay;                                      //É um atuador ligado a um Relé? pois assim os niveis LOW e HIGH são invertidos, excessão ESP8266
        public float OnOffCicle = 30f;                              //Tempo do ciclo On/Off ----- Melhorar ciclo só vai até "hardwareMaxBits" segundos
        public float MaxVoltage;                                    //TEnsão máxima de operação (V)
        public float MaxCurrent;                                    //Corrente máxima de operação (A)
        public float PowerConsumption = 0f;                         //Potencia atual de consumo P=UI (W)
        [HideInInspector]
        public string OnOffCicleString = "30";                      //Tempo do ciclo On/Off String
        public float MinSecureValue = 0f;                           //Valor mínimo seguro do sensor
        public float MaxSecureValue = 100f;                         //Valor máximo seguro do sensor
        public float SecurityMargin = 15f;                          //Valor em porcentagem de proximidade dos limites, para definir um estado de alerta amarelo para o sensor
        public bool Focus;                                          //Sensor precisa atualizar graficos e texturas?
        public int HistoryLineInFocus = 0;                          //Linha em foco para atualização de dados gráficos
        public HistoricData[,] History = new HistoricData[4, 200];  //Histórico de valores. modificações, taxas ... a cada 10min, 1hora, 10horas, 10dias
        public float[] NormalizedValueTexture = new float[200];     //Valores da linha em foco normalizados [min,max]->[0,1]
        public float[] ModificationTexture = new float[200];        //boleanos de modificação da linha em foco [v,f]->[0,1]
        public Texture2D[] HistoryTextures = new Texture2D[2];      //Texturas para representar visualmente as modificações [0], e os valores [1]
        public float TimeLastUpdateMark;
        public float TimeSinceLastUpdate;                           //Tempo desde a ultima atualização
        public bool ValueToSend = false;                            //Tem um valor novo para enviar
        public bool CicleToSend = false;                            //Tem um valor de ciclo novo para enviar

        public string StartDate;                                    //data e hora da primeira mediçao do sensor I
        public float MeanValue;                                     //media desde o inicio de operação do sensor I
        public string DateMeanValue;                                //data e hora da ultima mediçao da media
        public float MinValue;                                      //valor minimo do sensor I desde o inicio
        public string DateMinValue;                                 //data e hora do momento de minimo
        public float MaxValue;                                      //valor maximo do sensor I desde o inicio
        public string DateMaxValue;                                 //data e hora do momento de maximo
        public float Rate;                                          //Taxa atual de variação
        public UIButtonData HistoricChart;                          //Botão para habilitar gráfico e dados do histórico
        public UIButtonData Limits;                                 //Botão para o editor de limites de um atuador ---- ?
        public UIComboBoxData ComboBoxHistory;                      //ComboBox de escolha do intervalo de tempo do gráfico
        public ChartData Chart;                                     //Armazena dados do gráfico
        public UIAtuadorData AtuadorUI;                             //Informações da interface

    }

    [System.Serializable]
    public class UIAtuadorData
    {
        public string Name;
        public string Text;
        public Texture2D Icon;
        [Range(1, 100)]
        public int TransitionSpeed = 10;
        public int PosX;
        public int TargetPosX;
        public int PosY;
        public int TargetPosY;

        public int SizeX;
        public int TargetSizeX;
        public int SizeY;
        public int TargetSizeY;

        public Vector2 HUDPointer;
        public Vector2 ObjectPos2D;
        public bool DrawPointer;

        public bool MouseOver = false;
        public bool MouseEnter = false;
        public bool MouseClick = false;
        public bool Toggle = false;
        public bool Selected = false;
        public bool Simplified = false;
        public bool Short = false;
        public bool PlaySound = true;
        public float SoundIntensity = 0.4f;

        public int LocalFontSize = 12;
        public bool FontChanged = false;
        public bool Changed = true;
        public Rect[] AllRectInfo = new Rect[20];

        [HideInInspector]
        public float ClickTime = 0f;
        [HideInInspector]
        public float WaitingTime = 0.1f;

        public int BarSubdivisions = 4;
        public UISliderData IntensitySlider;
        public UIButtonData ExpandButton;

    }

    [System.Serializable]
    public class PID
    {
        public bool Enabled = false;                    //PID habilitado?
        public bool Selected = false;
        public bool ReqNetUpdate = false;               //Atualizar os dispositivos conectados pela internet?
        public bool InfoToSendToPLC = false;            //Deseja enviar informações para o PLC?

        public int ActuatorNum = 0;                     //Atuador para este PID
        public int SensorNum = 0;                       //Sensor para este PID

        public float setPoint;                          //Valor especificado para o sensor manter

        public float error;                             
        public float sample;
        public float lastSample;
        public float KP=0.02f, KI=0.0005f, KD=0.002f;
        public float P, I, D;
        public float pid;

        public UIComboBoxData SensorUsedInCombobox;
        public UIComboBoxData ActuatorUsedInCombobox;
        public UIButtonData ApplyButton;

    }

    [System.Serializable]
    public class FPSStat
    {
        public float FPS;
        public float FrameTime;
    }

    [System.Serializable]
    public class UIButtonData
    {
        public string Name;
        public string Text;
        public Texture2D Icon;
        public Texture2D Icon2;
        public Texture2D Icon3;
        public Texture2D Icon4;
        [Range(1, 100)]
        public int TransitionSpeed = 10;
        public int PosX;
        public int TargetPosX;
        public int PosY;
        public int TargetPosY;

        public int SizeX;
        public int TargetSizeX;
        public int SizeY;
        public int TargetSizeY;

        public bool MouseOver = false;
        public bool MouseEnter = false;
        public bool MouseClick = false;
        public bool Selectable = true;
        public bool Selected = false;
        public bool Toggle = false;

        public bool BarDown = true;
        public bool BarRight = false;

        public bool PlaySound = false;
        public float SoundIntensity = 0.4f;
        public bool HardMove = false;

        [HideInInspector]
        public float ClickTime = 0f;
        [HideInInspector]
        public float WaitingTime = 0.1f;

        public int BarSubdivisions = 4;
    }

    [System.Serializable]
    public class UIToggleData
    {
        public string Name;
        public string Text;

        public Rect DrawRect;

        public bool MouseOver = false;
        public bool MouseEnter = false;
        public bool MouseClick = false;
        public bool Toggle = false;

        public bool PlaySound = false;
        public float SoundIntensity = 0.4f;

        [HideInInspector]
        public float ClickTime = 0f;
        [HideInInspector]
        public float WaitingTime = 0.1f;
    }

    [System.Serializable]
    public class UIAtuadorGroupData
    {
        public string Name;
        public string Text;
        [Range(1, 100)]
        public int TransitionSpeed = 1;
        public float PosX = 0.68f;
        public float PosY = 0.1f;

        public float SizeX = 0.25f;
        public float SizeY = 0.7f;

        public bool MouseOver = false;
        public bool MouseEnter = false;
        public bool MouseClick = false;
        public bool Selected = false;
        public bool Simplified = false;
        public bool Short = false;

        public bool PlaySound = false;
        public float SoundIntensity = 0.4f;
        public bool HardMove = false;

        [HideInInspector]
        public float ClickTime = 0f;
        [HideInInspector]
        public float WaitingTime = 0.1f;

        public UIButtonData ExpandButton;

        public int BarSubdivisions = 4;
    }

    [System.Serializable]
    public class UIbuttonGroupData
    {
        public string Name;
        public string Text;
        [Range(1, 100)]
        public int TransitionSpeed = 10;
        public float PosX = 0.0f;
        public float PosY = 0.1f;

        public float SizeX = 0.05f;
        public float MaxSizeX = 0.1f;
        public float MinSizeX = 0.03f;
        public float SizeY = 0.9f;

        public bool MouseOver = false;
        public bool MouseEnter = false;
        public bool MouseOut = false;

    }

    [System.Serializable]
    public class UIComboBoxData
    {
        public string Name;
        public string[] Texts;
        public int SelectedItem = 0;
        public Texture2D[] Icons;
        public Texture2D Arrow;
        public Rect Rect;

        public bool MouseOver = false;
        public bool MouseEnter = false;
        public bool MouseClick = false;
        public bool Selected = false;

        public bool PlaySound = false;
        public float SoundIntensity = 0.4f;

        [HideInInspector]
        public float ClickTime = 0f;
        [HideInInspector]
        public float WaitingTime = 0.1f;
    }

    [System.Serializable]
    public class UISliderData
    {
        public string Name;
        public string Text;
        public Texture2D DotIcon;
        public Texture2D DotIconDisabled;
        public Texture2D DotClickIcon;
        public Texture2D LineTexture;
        public bool Enabled = true;
        public int LineWidth = 2;
        public Vector2 MousePosition;
        public int InitialX;
        public int FinalX;
        public int SliderSize;
        public float Min = 0f;
        public float Max = 1f;
        public float Value = 0.5f;
        public float OldValue = 0.5f;
        public float Fraction = 0.5f;
        public bool Changed = false;
        [Range(1, 100)]
        public int TransitionSpeed = 10;
        public int PosX;
        public int TargetPosX;
        public int PosY;
        public int TargetPosY;

        public int SizeX;
        public int TargetSizeX;
        public int SizeY;
        public int TargetSizeY;

        public bool MouseOver = false;
        public bool MouseOverDotIcon = false;
        public bool MouseEnter = false;
        public bool MouseClick = false;
        public bool Selected = false;

        public bool PlaySound = false;
        public float SoundIntensity = 0.4f;
        public bool HardMove = false;

        [HideInInspector]
        public float ClickTime = 0f;
        [HideInInspector]
        public float WaitingTime = 0.1f;

        public int BarSubdivisions = 4;
    }

    [System.Serializable]
    public class UISensorGroupData
    {
        public string Name;
        public string Text;
        [Range(1, 100)]
        public int TransitionSpeed = 1;
        public float PosX = 0.68f;
        public float PosY = 0.1f;

        public float SizeX = 0.25f;
        public float SizeY = 0.7f;

        public bool MouseOver = false;
        public bool MouseEnter = false;
        public bool MouseClick = false;
        public bool Selected = false;
        public bool Simplified = false;
        public bool Short = false;

        public bool PlaySound = false;
        public float SoundIntensity = 0.4f;
        public bool HardMove = false;

        [HideInInspector]
        public float ClickTime = 0f;
        [HideInInspector]
        public float WaitingTime = 0.1f;

        public UIButtonData ExpandButton;

        public int BarSubdivisions = 4;
    }

    [System.Serializable]
    public class ChartData
    {
        public string Name;
        [Range(1, 100)]
        public float TransitionSpeed = 10f;
        public bool rigid = true;
        public Rect ChartRect;
        public Rect TargetChartRect;
        public string Title;
        [HideInInspector]
        public Rect TitleRect;
        public string XLabel;
        [HideInInspector]
        public Rect XLabelRect;
        public string YLabel;
        public Vector2 pivotPoint = new Vector2(0f, 0f);
        [HideInInspector]
        public Rect YLabelRect;
        public float MinValueY;
        public float MaxValueY;
        public float MinValueX;
        public float MaxValueX;
        public float percStart;
        public float percFinish;
        public float barWidth1;
        public float barHeight;
        public Vector2 Start = new Vector2(0f, 0f);
        public Vector2 Finish = new Vector2(0f, 0f);
        public float LeftOverFraction = 0.06f;
        public Rect[] AllRectInfo = new Rect[40];
        public int[] LinesThickness = new int[1];
        public float[] Tangents = new float[200];

    }

    [System.Serializable]
    public class ProcessConnection
    {
        [Header("Cable or radio connection")]
        [Tooltip("Direct cable or radio conection to arduino")]
        public int PLCNumber = 0;
        public bool TryingToConnect = false;
        [Tooltip("Utilize 9600 apenas quando usar Radio, fora isso utilize 115200")]
        public int BaudRate = 115200;             //Utilize 9600 apenas quando usar Radio, fora isso utilize 115200
        public bool CableConectionInterrupted = false;
        public bool Connected = false;
        public float InitialTimeOfConnection = 0;
        public float ConnectionTime = 0f;
        public float TimeLastUpdate = 0;
        public float TimeSinceLastUpdate = 0;
        public SerialPort ConnectionPort;       //Porta do Arduino
        public float UpdateCOMList;
        public string[] Ports;                  //Todas as portas
        public bool[] TryingToConnectTo;
        public bool[] BusyPorts;
        public int ConnectIndex;
        public int Fails;
        public long CheckSumFails;
        public int Restarts;
        public int Reconnections;
        public bool UnstableConnection;
        public bool Restarting;
        public string Recepted = "";
        public string COMLastOutput = "";
        public string COMOutput = "";
        public string COMPortName = "";
    }

    [System.Serializable]
    public class PhotonRemoteConnection
    {
        [Header("Photon Remote process connection")]
        public int PLCNumber = 0;
        public bool TryingToConnectRPC = true;
        public bool ConnectedRPC = false;
        public bool RPCHost = false;
        public bool RPCClient = false;
        public NetworkCommunication NetCommunication;
        public NetworkLauncher NetLauncher;
        public string Status;
        public int Ping;

        public float InitialTimeOfConnection = 0;
        public float ConnectionTime = 0f;
        public float TimeLastUpdate = 0;
        public float TimeSinceLastUpdate = 0;


        public int Fails;
        public long CheckSumFails;
        public int Restarts;
        public int Reconnections;
        public bool UnstableConnection;
        public bool Restarting;
    }

    [System.Serializable]
    public class ProcessWifiConnection
    {

        [Header("Local process wifi network")]
        public bool TryingToConnectLocalProcessServer = true;
        public bool ConnectedLocalProcessServer = false;
        public bool localProcessWifiInterrupted = false;
        public string InitialLocalServerIPToSearch = "192.168.1.9";
        public string SearchingOnLocalServerIP = "";
        public string LocalServerIP = "";
        public int[] WebServerIPDivided = new int[4];
        public TcpClient mySocket;
        public NetworkStream theStream;
        public StreamWriter theWriter;
        public StreamReader theReader;
        public Int32 Port = 80;
        public string Recepted;
        public string SocketRead;
        public string LastSocketRead;
        public float InitialTimeOfConnection = 0;
        public float ConnectionTime = 0f;
        public float TimeLastUpdate = 0;
        public float TimeSinceLastUpdate = 0;


        public int Fails;
        public long CheckSumFails;
        public int Restarts;
        public int Reconnections;
        public bool UnstableConnection;
        public bool Restarting;
    }

    [System.Serializable]
    public class InternetConnection
    {
        [Tooltip("Indica que há conexão com a internet")]
        public bool Connected = false;                  //Tem internet?
        public float InitialTimeOfConnection = 0;       //Primeiro instante que a conexão foi confirmada
        public float ConnectionTime = 0f;               //Tempo de conexão
        public float TimeLastUpdate = 0;                //Momento da última conexão
        public int Reconnections;                       //Quantas reconexões foram feitas?
        public bool UnstableConnection;                 //Caso tenha havido muitas avisar que a internet está instável
    }

    [System.Serializable]
    public class Battery
    {
        public bool ChargerConnected = false;           //Carregador conectado?
        public float BatteryLevel = 0f;                 //Nível atual da bateria
        public float TimeLastUpdate = 0f;               //Tempo da última atualização
        public float LowValue = 0.25f;                  //Valor de bateria baixa
        public float CriticalValue = 0.10f;             //Valor de bateria crítica
    }

    public class Functions
    {
        public static int HardwareMaxbits = 255;        //Bits do dispositivo controlador (Arduino - 256, ESP8266 - 1024)

        public static GameObject CoreObject = null;     //Objeto que contém o script central
        public static GameObject MainCamera;            //Câmera principal
        public static GameObject CameraTarget;          //Alvo da câmera

        public static string LogFile = Application.dataPath + "/Log.txt";   //Endereço do Log
        public static string LogString = null;                              //Variavel que guarda informações do Log para uso na interface
        public static bool BlockWriteInfo = false;                          //Bloqueia a escrita de informações em arquivos de texto, aplicado quando se manipula os mesmos

        public static string Config = Application.dataPath + "/Config.ini";
        public static AP_INIFile ConfigIni = new AP_INIFile(Config);

        public static string EchoServerUrl = "https://www.google.com/generate_204";
        public static float CheckInterval = 0.6f;
        public static int MaxChecksBeforeRetry = 26;
        public static int TimeoutSeconds = 5;

        public static float CollectGarbageInterval = 11f;                   //Intervalo de tempo para verificar se há lixo acumulado na memória

        public static Rect rectDraw = new Rect(0, 0, 0, 0);                                 //Rect para desenhar temporário
        public static Rect rectDraw2 = new Rect(0, 0, 0, 0);                                //Rect para desenhar temporário
        public static Vector2 OffsetForRect = new Vector2(0,0);
        public static Vector2 mousePosition = new Vector2(0, 0);                            //Posição do mouse a cada instante (Corrigida para coordenadas da interface)
        public static Rect FreeRectSpace = new Rect(0, 0, 0, 0);                            //Rect da posição atual que define o espaço livre entre sensores e atuadores
        public static Rect FreeRectSpaceTarget = new Rect(0, 0, 0, 0);                      //Rect Alvo que define o espaço livre entre sensores e atuadores
        public static bool FreeSpaceInUse = false;                                          //Algo utiliza o espaço livre?
        public static bool SelectedObject = false;                                          //Selecionou em algum objeto interativo?
        public static float ClickEvolution = 0f;                                            //Auxilia a definir a evolução do desenho do click
        public static Ray ray;                                                              //Verificação de colisão com objetos a partir do mouse
        public static RaycastHit hit;                                                       //Verificação de colisão com objetos a partir do mouse
        public static float TimeSinceLastUpdateFail = 30f;                                  //Tempo sem receber informações do sensor para alerta amarelo
        public static float TimeSinceLastUpdateCritical = 80f;                              //Tempo sem receber informações do sensor para alerta vermelho
        public static bool Speaking = true;                                                 //Habilita ou desabilita a sintese de voz
        public static float SensorStatus = 1f;                                              //1 OK, 0.5 problemas, 0 Não OK
        public static float AtuadorStatus = 1f;                                             //1 OK, 0.5 problemas, 0 Não OK
        public static float SaveResultsOnFile = 10f;                                        //Intervalo de tempo para salvar na pasta results
        public static string Results = Application.dataPath + "/Resultados/Resultados.txt"; //local para salvar os resultados
        public static string LogSensors = Application.dataPath + "/Resultados/Sensores.txt"; //local para salvar os resultados só dos sensores
        public static string LogAtuators = Application.dataPath + "/Resultados/Atuadores.txt"; //local para salvar os resultados só dos atuadores

        public static bool ScrollDebugString = true;                //Texto da janela de Debug
        public static bool ScrollLogString = true;                  //Texto da janela de LOG
        public static string COMString = "";                        //
        public static string COMToSend = "";                        //Texto para envio
        public static Vector2 COMDebugScrollPosition;               //Posição do scroll do debug (melhorar)
        public static Vector2 LogScrollPosition;                    //Posição do scroll do LOG (melhorar)
        public static int DebugCOMFontSize = 12;                    //Tamanho da fonte na tela de Debug

        public static Vector2 ChatScrollPosition;                   //Scroll da janela de chat (melhorar)

        //Recursos embutidos, texturas e sons
        public static Texture2D ScreenshotIcon = Resources.Load<Texture2D>("Textures/ScreenshotIcon");
        public static Texture2D ApplyIcon = Resources.Load<Texture2D>("Textures/ApplyIcon");
        public static Texture2D MinimumArrowIcon = Resources.Load<Texture2D>("Textures/Minimum");
        public static Texture2D MaximumArrowIcon = Resources.Load<Texture2D>("Textures/Maximum");
        public static Texture2D MeanIcon = Resources.Load<Texture2D>("Textures/Mean");
        public static Texture2D RateUpIcon = Resources.Load<Texture2D>("Textures/Up");
        public static Texture2D RateDownIcon = Resources.Load<Texture2D>("Textures/Down");
        public static Texture2D DownArrowIcon = Resources.Load<Texture2D>("Textures/DownArrow");
        public static Texture2D StableIcon = Resources.Load<Texture2D>("Textures/Stable");
        public static Texture2D ExpandIcon = Resources.Load<Texture2D>("Textures/Expand");
        public static Texture2D DutyCicle = Resources.Load<Texture2D>("Textures/CicleDuty");
        public static Texture2D GoodSphere = Resources.Load<Texture2D>("Textures/GoodSphere");
        public static Texture2D BadSphere = Resources.Load<Texture2D>("Textures/BadSphere");
        public static Texture2D SimpleSphere35 = Resources.Load<Texture2D>("Textures/Sphere35");
        public static Texture2D SimpleSphere = Resources.Load<Texture2D>("Textures/Sphere");
        public static Texture2D HardwareConnectionOn = Resources.Load<Texture2D>("Textures/HardwareConnectionOn");
        public static Texture2D HardwareConnectionOff = Resources.Load<Texture2D>("Textures/HardwareConnectionOff");
        public static Texture2D HardwareConnectionDisabled = Resources.Load<Texture2D>("Textures/HardwareConnectionDisabled");
        public static Texture2D InternetOn = Resources.Load<Texture2D>("Textures/InternetOn");
        public static Texture2D InternetOff = Resources.Load<Texture2D>("Textures/InternetOff");
        public static Texture2D BatteryFull = Resources.Load<Texture2D>("Textures/BatteryFull");
        public static Texture2D BatteryLow = Resources.Load<Texture2D>("Textures/BatteryLow");
        public static Texture2D WebComOn = Resources.Load<Texture2D>("Textures/WebComON");
        public static Texture2D WebComOff = Resources.Load<Texture2D>("Textures/WebComOFF");
        public static Texture2D WebComDisabled = Resources.Load<Texture2D>("Textures/WebComDisabled");
        public static Texture2D WebCom = Resources.Load<Texture2D>("Textures/WebCom");
        public static Texture2D BatteryCritical = Resources.Load<Texture2D>("Textures/BatteryCritical");
        public static Texture2D VertGradWhiteToGray = Resources.Load<Texture2D>("Textures/VertGradWhiteToGray");
        public static Texture2D Debug = Resources.Load<Texture2D>("Textures/Debug");
        public static Texture2D Reset = Resources.Load<Texture2D>("Textures/Reset");

        public static AudioClip clip1 = (AudioClip)Resources.Load("Sounds/button001");
        public static AudioClip clip2 = (AudioClip)Resources.Load("Sounds/OnEnter");
        public static AudioClip clip3 = (AudioClip)Resources.Load("Sounds/Alarm15");
        public static AudioClip clip4 = (AudioClip)Resources.Load("Sounds/Alarm60");

        //Cores para interface
        public static Texture2D BlackTexture = CreateTexture(Color.black);
        public static Texture2D BlackTextureAlpha20 = CreateTexture(new Color(0f, 0f, 0f, 0.2f));
        public static Texture2D Gray10TextureAlpha60 = CreateTexture(new Color(0.1f, 0.1f, 0.1f, 0.6f));
        public static Texture2D Gray20TextureAlpha90 = CreateTexture(new Color(0.2f, 0.2f, 0.2f, 0.9f));
        public static Texture2D GrayTextureAlpha50 = CreateTexture(new Color(0.5f, 0.5f, 0.5f, 0.5f));
        public static Texture2D Gray12Texture = CreateTexture(new Color(0.12f, 0.12f, 0.12f, 1f));
        public static Texture2D Gray16Texture = CreateTexture(new Color(0.16f, 0.16f, 0.16f, 1f));
        public static Texture2D Gray20Texture = CreateTexture(new Color(0.2f, 0.2f, 0.2f, 1f));
        public static Texture2D Gray25Texture = CreateTexture(new Color(0.25f, 0.25f, 0.25f, 1f));
        public static Texture2D Gray50Texture = CreateTexture(Color.gray);
        public static Texture2D Gray75Texture = CreateTexture(new Color(0.75f, 0.75f, 0.75f, 1f));
        public static Texture2D WhiteTexture = CreateTexture(Color.white);
        public static Texture2D WhiteTextureAlpha50 = CreateTexture(new Color(1f, 1f, 1f, 0.5f));
        public static Texture2D GreenTexture = CreateTexture(new Color(0f, 1f, 0f, 1f));
        public static Texture2D RedTexture = CreateTexture(new Color(1f, 0f, 0f, 1f));
        public static Texture2D YellowTexture = CreateTexture(new Color(1f, 0.92f, 0.016f, 1f));
        public static Texture2D GreenTextureAlpha40 = CreateTexture(new Color(0f, 1f, 0f, 0.4f));
        public static Texture2D RedTextureAlpha50 = CreateTexture(new Color(1f, 0f, 0f, 0.4f));
        public static Texture2D YellowTextureAlpha45 = CreateTexture(new Color(1f, 0.92f, 0.016f, 0.4f));

        //Ui global data
        public static int FontSize = 10;
        public static Color PrimaryColor = Color.white;
        public static Color SecondaryColor = Color.white;
        public static int Padding = 10;

        //Inicialiar um sensor-----------------não funciona mais
        public static Sensor InitializeSensor(Sensor PointedSensor, string Name, string Description, string SensorClass, string Unit, string SpokeUnit)
        {
            PointedSensor.Name = Name;                        //Nome
            PointedSensor.Description = Description;          //Descrição
            PointedSensor.SensorClass = SensorClass;          //Classe: termometro, barometro...
            PointedSensor.Unit = Unit;                        //Unidade
            PointedSensor.SpokeUnit = SpokeUnit;              //Unidade falada
            for (int i = 0; i < PointedSensor.History.GetLength(0); i++)
            {
                for (int j = 0; j < PointedSensor.History.GetLength(1); j++)
                {
                    PointedSensor.History[i, j].Modification = false;     //Histórico de modificações
                    PointedSensor.History[i, j].Values = 0f;              //Histórico de valores
                    PointedSensor.History[i, j].Rates = 0f;                //Histórico de taxas
                    PointedSensor.History[i, j].Dates = "";                //Histórico de datas
                }
            }
            return PointedSensor;
        }

        //Inicialiar um sensor
        public static void InitializeSensorsAndAtuators(ProcessDetails ThisProcess)
        {
            for (int k = 0; k < ThisProcess.Sensors.GetLength(0); k++)
            {
                //Inicializa os sensores
                ThisProcess.Sensors[k].SensorUI.PosX = Screen.width / 2; //Faz com que saia do centro logo que o programa abre
                ThisProcess.Sensors[k].SensorUI.PosY = Screen.height / 2;
                ThisProcess.Sensors[k].MeanValue = (ThisProcess.Sensors[k].MaxSecureValue + ThisProcess.Sensors[k].MinSecureValue)/2f;
                ThisProcess.Sensors[k].MinValue = (ThisProcess.Sensors[k].MaxSecureValue + ThisProcess.Sensors[k].MinSecureValue) / 2f;
                ThisProcess.Sensors[k].MaxValue = (ThisProcess.Sensors[k].MaxSecureValue + ThisProcess.Sensors[k].MinSecureValue) / 2f;
                ThisProcess.Sensors[k].History = new HistoricData[4, 200];
                ThisProcess.Sensors[k].SensorStatus = 1f;
                ThisProcess.Sensors[k].SensorUI.AllRectInfo = new Rect[20];
                ThisProcess.Sensors[k].NormalizedValueTexture = new float[200];
                ThisProcess.Sensors[k].ModificationTexture = new float[200];
                ThisProcess.Sensors[k].HistoryTextures = new Texture2D[2];
                ThisProcess.Sensors[k].FirstReceive = true;
                for (int i = 0; i < ThisProcess.Sensors[k].History.GetLength(0); i++)
                {
                    for (int j = 0; j < ThisProcess.Sensors[k].History.GetLength(1); j++)
                    {
                        ThisProcess.Sensors[k].History[i, j] = new HistoricData();
                    }
                }
                //inicializa os gráficos
                ThisProcess.Sensors[k].Chart.Name = "Gráfico do sensor " + ThisProcess.Sensors[k].Name;
                ThisProcess.Sensors[k].Chart.Title = "Gráfico do " + ThisProcess.Sensors[k].Name + " " + k.ToString("D2");
                ThisProcess.Sensors[k].Chart.XLabel = "Tempo (s)"; // melhorar
                ThisProcess.Sensors[k].Chart.YLabel = ThisProcess.Sensors[k].SensorClass + " " + ThisProcess.Sensors[k].Unit;
                ThisProcess.Sensors[k].Chart.MinValueY = ThisProcess.Sensors[k].MinSecureValue;
                ThisProcess.Sensors[k].Chart.MaxValueY = ThisProcess.Sensors[k].MaxSecureValue;
                ThisProcess.Sensors[k].Chart.AllRectInfo = new Rect[20];
                ThisProcess.Sensors[k].Chart.TransitionSpeed = 10f;
                ThisProcess.Sensors[k].Chart.rigid = true;
                ThisProcess.Sensors[k].Chart.LeftOverFraction = 0.06f;
                ThisProcess.Sensors[k].ComboBoxHistory.Texts = new string[]{ "10 minutos", "1 hora", "10 horas", "10 dias" };
                ThisProcess.Sensors[k].ComboBoxHistory.Arrow = DownArrowIcon;
                //ThisProcess.Sensors[k].HistoricChart.Icon = Reset;
                ThisProcess.Sensors[k].HistoricChart.Name = "Mostrar dados do histórico";
                ThisProcess.Sensors[k].HistoricChart.Text = "Histórico";
                ThisProcess.Sensors[k].HistoricChart.HardMove = true;
                ThisProcess.Sensors[k].HistoricChart.Toggle = true;
                //ThisProcess.Sensors[k].Limits.Icon = Reset;
                ThisProcess.Sensors[k].Limits.Name = "Editar os limites de segurança";
                ThisProcess.Sensors[k].Limits.Text = "Limites";
                ThisProcess.Sensors[k].Limits.HardMove = true;
                ThisProcess.Sensors[k].Limits.Toggle = false;
                ThisProcess.Sensors[k].ResetButton.Icon = Reset;
                ThisProcess.Sensors[k].ResetButton.Name = "Resetar histórico e valores";
                ThisProcess.Sensors[k].ResetButton.Text = "Resetar";
                ThisProcess.Sensors[k].ResetButton.HardMove = true;
                ThisProcess.Sensors[k].SensorUI.ExpandButton.BarDown = true;
                ThisProcess.Sensors[k].SensorUI.PlaySound = true;
                ThisProcess.Sensors[k].SensorUI.SoundIntensity = 1f;
            }
            for (int k = 0; k < ThisProcess.Atuators.GetLength(0); k++)
            {
                ThisProcess.Atuators[k].AtuadorUI.PosX = Screen.width / 2; //Faz com que saia do centro logo que o programa abre
                ThisProcess.Atuators[k].AtuadorUI.PosY = Screen.height / 2;
                ThisProcess.Atuators[k].History = new HistoricData[4, 200];
                ThisProcess.Atuators[k].NormalizedValueTexture = new float[200];
                ThisProcess.Atuators[k].ModificationTexture = new float[200];
                ThisProcess.Atuators[k].AtuadorUI.AllRectInfo = new Rect[20];
                ThisProcess.Atuators[k].AtuadorStatus = 1f;
                ThisProcess.Atuators[k].SecurityMargin = 0f;


                //first receive = true;

                for (int i = 0; i < ThisProcess.Atuators[k].History.GetLength(0); i++)
                {
                    for (int j = 0; j < ThisProcess.Atuators[k].History.GetLength(1); j++)
                    {
                        ThisProcess.Atuators[k].History[i, j] = new HistoricData();
                    }
                }
                //inicializa os gráficos
                ThisProcess.Atuators[k].Chart.Name = "Gráfico do sensor " + ThisProcess.Atuators[k].Name;
                ThisProcess.Atuators[k].Chart.Title = "Gráfico do " + ThisProcess.Atuators[k].Name + " " + k.ToString("D2");
                ThisProcess.Atuators[k].Chart.XLabel = "Tempo (s)"; // melhorar
                ThisProcess.Atuators[k].Chart.YLabel = ThisProcess.Atuators[k].AtuadorClass + " " + ThisProcess.Atuators[k].Unit;
                ThisProcess.Atuators[k].Chart.MinValueY = ThisProcess.Atuators[k].MinSecureValue;
                ThisProcess.Atuators[k].Chart.MaxValueY = ThisProcess.Atuators[k].MaxSecureValue;
                ThisProcess.Atuators[k].Chart.AllRectInfo = new Rect[20];
                ThisProcess.Atuators[k].Chart.TransitionSpeed = 10f;
                ThisProcess.Atuators[k].Chart.rigid = true;
                ThisProcess.Atuators[k].Chart.LeftOverFraction = 0.06f;
                //ThisProcess.Atuators[k].HistoricChart.Icon = Reset;
                ThisProcess.Atuators[k].HistoricChart.Name = "Mostrar dados do histórico";
                ThisProcess.Atuators[k].HistoricChart.Text = "Histórico";
                ThisProcess.Atuators[k].HistoricChart.HardMove = true;
                //ThisProcess.Atuators[k].Limits.Icon = Reset;
                ThisProcess.Atuators[k].Limits.Name = "Editar os limites de segurança";
                ThisProcess.Atuators[k].Limits.Text = "Limites";
                ThisProcess.Atuators[k].Limits.HardMove = true;
                ThisProcess.Atuators[k].HistoryTextures = new Texture2D[2];
                ThisProcess.Atuators[k].HistoricChart.Name = "Mostrar dados do histórico";
                ThisProcess.Atuators[k].HistoricChart.Text = "Histórico";
                ThisProcess.Atuators[k].HistoricChart.HardMove = true;
                ThisProcess.Atuators[k].HistoricChart.Toggle = true;
                ThisProcess.Atuators[k].ComboBoxHistory.Texts = new string[] { "10 minutos", "1 hora", "10 horas", "10 dias" };
                ThisProcess.Atuators[k].ComboBoxHistory.Arrow = DownArrowIcon;
                ThisProcess.Atuators[k].AtuadorUI.ExpandButton.BarDown = true;
                ThisProcess.Atuators[k].AtuadorUI.PlaySound = true;
                ThisProcess.Atuators[k].AtuadorUI.SoundIntensity = 1f;
                //reset button
                //ThisProcess.Atuators[k].ResetButton.Icon = Reset;
                //ThisProcess.Atuators[k].ResetButton.Name = "Resetar histórico e valores";
                //ThisProcess.Atuators[k].ResetButton.Text = "Resetar";
                //ThisProcess.Atuators[k].ResetButton.HardMove = true;
            }

        }

        //Define quantos bits tem o hardware (Arduino ou NodeMCU)
        public static void SetMaxBits(int MaxBits)
        {
            HardwareMaxbits = MaxBits;
        }

        //Define o objeto central e a fonte de audio
        public static void SetCoreObject(GameObject Target)
        {
            CoreObject = Target;
        }

        //Define camera central e alvo para movimento
        public static void SetCameraMoviment(GameObject MCamera, GameObject CameraT)
        {
            MainCamera = MCamera;
            CameraTarget = CameraT;
        }

        //Gerencia quando o usuario pode mover a camera  ------------ expandir para interações com os objetos 3D, verificar eficiência
        public static void BlockModelInteraction()
        {
            if (FreeSpaceInUse || !MouseOverRect(FreeRectSpace))
            {
                MainCamera.GetComponent<MouseOrbit>().Blocked = true;
                CameraTarget.GetComponent<Pan>().Blocked = true;
            }
            else
            {
                MainCamera.GetComponent<MouseOrbit>().Blocked = false;
                CameraTarget.GetComponent<Pan>().Blocked = false;
            }
        }

        //Define o tamanho padrão da fonte
        public static void SetFontSize(int Input, UISliderData SetFontSize)
        {
            FontSize = Input;
            //Escreve no Config.ini o tamanho da fonte se ela ainda não tiver sido definida
            if (ConfigIni.ReadString("UI", "FontSize") != "")
            {
                FontSize = ConfigIni.ReadInt("UI", "FontSize");
                SetFontSize.Value = FontSize;
                SetFontSize.Fraction = (SetFontSize.Value - SetFontSize.Min) / (SetFontSize.Max - SetFontSize.Min);
            }
            if (ConfigIni.ReadString("UI", "FontSize") == "")
            {
                ConfigIni.WriteInt("UI", "FontSize", Input);
                SetFontSize.Value = FontSize;
                SetFontSize.Fraction = (SetFontSize.Value - SetFontSize.Min) / (SetFontSize.Max - SetFontSize.Min);
            }
        }
        //Desenha o clique do mouse para facil visualização
        public static void VisualizeClick()
        {
            mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            if (Input.GetMouseButtonDown(0))
            {
                ClickEvolution = 0.05f;
            }
            if (ClickEvolution > 1f)
            {
                ClickEvolution = 0f;
            }
            if (ClickEvolution > 0f)
            {
                ClickEvolution += 0.04f;
                rectDraw = new Rect(mousePosition.x - 4f * FontSize * ClickEvolution / 2, mousePosition.y - 4f * FontSize * ClickEvolution / 2, 4f * FontSize * ClickEvolution, 4f * FontSize * ClickEvolution);
                GUI.DrawTexture(rectDraw, SimpleSphere35);
            }

        }

        //Calcula o tamanho inicial ideal da fonte
        public static int IdealUISize(ProcessDetails ThisProcess, UISensorGroupData UiSensorsGroupSource, UIAtuadorGroupData UiAtuadoresGroupSource)
        {
            int Size = 0;
            int MaxVert = 8; // numero máximo de sensore/atuadores na vertical
            int VertNumMax = ThisProcess.Sensors.Length;
            if (ThisProcess.Sensors.Length >= ThisProcess.Atuators.Length)//Quem tem mais itens: sensores ou atuadores?
            {
                VertNumMax = ThisProcess.Sensors.Length;
            }
            else
            {
                VertNumMax = ThisProcess.Atuators.Length;
            }
            if (ThisProcess.Sensors.Length <= MaxVert && ThisProcess.Atuators.Length <= MaxVert)
            {
                Size = Mathf.RoundToInt(0.0104f * (float)Screen.height + 2.654f);
            }
            if (ThisProcess.Sensors.Length > MaxVert || ThisProcess.Atuators.Length > MaxVert)
            {
                if (ThisProcess.Sensors.Length >= ThisProcess.Atuators.Length)
                    Size = Mathf.RoundToInt(0.01f * (float)Screen.height + 2.5f) - Mathf.RoundToInt(1.1f * (ThisProcess.Sensors.Length - MaxVert));
                if (ThisProcess.Sensors.Length < ThisProcess.Atuators.Length)
                    Size = Mathf.RoundToInt(0.01f * (float)Screen.height + 2.5f) - Mathf.RoundToInt(1.1f * (ThisProcess.Atuators.Length - MaxVert));
            }
            if (VertNumMax > MaxVert)//Caso ultrapasse o limite use a forma reduzida
            {
                Size = Mathf.RoundToInt(0.0125f * (float)Screen.height + 2.5f) - Mathf.RoundToInt(0.6f * (VertNumMax - MaxVert));
                if (ThisProcess.Sensors.Length > MaxVert) UiSensorsGroupSource.Short = true;
                if (ThisProcess.Atuators.Length > MaxVert) UiAtuadoresGroupSource.Short = true;
            }

            //algumas modificações importantes
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Cursor.visible = true;
            Padding = 6 * FontSize / 10 + Screen.width/500;
            return Size;  //tamanho ideal da fonte
        }

        //Gerencia o espaço livre na tela, 
        public static void CalculateFreeScreenSpace(UISensorGroupData UiSensorsGroupSource, UIAtuadorGroupData UiAtuadoresGroupSource)
        {
            FreeRectSpaceTarget.x = (UiAtuadoresGroupSource.PosX + UiAtuadoresGroupSource.SizeX + 0.04f) * Screen.width;
            FreeRectSpaceTarget.y = UiAtuadoresGroupSource.PosY * Screen.height;
            FreeRectSpaceTarget.width = (1f - UiAtuadoresGroupSource.PosX - UiAtuadoresGroupSource.SizeX - (1f - UiSensorsGroupSource.PosX) - 0.06f) * Screen.width;
            FreeRectSpaceTarget.height = (0.98f - UiAtuadoresGroupSource.PosY) * Screen.height;

            if (FreeRectSpace.x != FreeRectSpaceTarget.x ||
                FreeRectSpace.y != FreeRectSpaceTarget.y ||
                FreeRectSpace.width != FreeRectSpaceTarget.width ||
                FreeRectSpace.height != FreeRectSpaceTarget.height)
            {
                if (UiSensorsGroupSource.TransitionSpeed != -100)
                {
                    //UnityEngine.Debug.Log("TS: "+ UiSensorsGroupSource.TransitionSpeed);
                    FreeRectSpace.x = (100 * FreeRectSpace.x + UiSensorsGroupSource.TransitionSpeed * FreeRectSpaceTarget.x) / (UiSensorsGroupSource.TransitionSpeed + 100);
                    if (FreeRectSpace.x - FreeRectSpaceTarget.x > 0) FreeRectSpace.x = FreeRectSpace.x - 1;
                    if (FreeRectSpace.x - FreeRectSpaceTarget.x < 0) FreeRectSpace.x = FreeRectSpace.x + 1;

                    FreeRectSpace.y = (100 * FreeRectSpace.y + UiSensorsGroupSource.TransitionSpeed * FreeRectSpaceTarget.y) / (UiSensorsGroupSource.TransitionSpeed + 100);
                    if (FreeRectSpace.y - FreeRectSpaceTarget.y > 0) FreeRectSpace.y = FreeRectSpace.y - 1;
                    if (FreeRectSpace.y - FreeRectSpaceTarget.y < 0) FreeRectSpace.y = FreeRectSpace.y + 1;

                    FreeRectSpace.width = (100 * FreeRectSpace.width + UiSensorsGroupSource.TransitionSpeed * FreeRectSpaceTarget.width) / (UiSensorsGroupSource.TransitionSpeed + 100);
                    if (FreeRectSpace.width - FreeRectSpaceTarget.width > 0) FreeRectSpace.width = FreeRectSpace.width - 1;
                    if (FreeRectSpace.width - FreeRectSpaceTarget.width < 0) FreeRectSpace.width = FreeRectSpace.width + 1;

                    FreeRectSpace.height = (100 * FreeRectSpace.height + UiSensorsGroupSource.TransitionSpeed * FreeRectSpaceTarget.height) / (UiSensorsGroupSource.TransitionSpeed + 100);
                    if (FreeRectSpace.height - FreeRectSpaceTarget.height > 0) FreeRectSpace.height = FreeRectSpace.height - 1;
                    if (FreeRectSpace.height - FreeRectSpaceTarget.height < 0) FreeRectSpace.height = FreeRectSpace.height + 1;
                }
            }
        }

        //Gerencia as interações com os modelos
        public static void ObjectInteraction(ProcessDetails ThisProcess)
        {
            if (MouseOverRect(FreeRectSpace) && !FreeSpaceInUse)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    //Sensores
                    for (int i = 0; i < ThisProcess.Sensors.Length; i++)
                    {
                        for (int j = 0; j < ThisProcess.Sensors[i].Objects.Length; j++)
                        {
                            if (hit.collider.gameObject == ThisProcess.Sensors[i].Objects[j])
                            {
                                SelectedObject = true;
                                //Para o sensor selecionado, realçe
                                for (int k = 0; k < ThisProcess.Sensors[i].Objects.Length; k++)
                                {
                                    if (ThisProcess.Sensors[i].Objects[k].GetComponent<Renderer>().material.HasProperty("_Selected"))
                                    {
                                        ThisProcess.Sensors[i].Objects[k].GetComponent<Renderer>().material.SetFloat("_DangerLevel", ThisProcess.Sensors[i].SensorStatus);
                                        ThisProcess.Sensors[i].Objects[k].GetComponent<Renderer>().material.SetFloat("_Selected", 1.0f);
                                        ThisProcess.Sensors[i].SensorUI.DrawPointer = true;
                                    }
                                }
                                //para os outros desabilite o realçe
                                for (int l = 0; l < ThisProcess.Sensors.Length; l++)//sensores
                                {
                                    for (int k = 0; k < ThisProcess.Sensors[l].Objects.Length; k++)
                                    {
                                        if (ThisProcess.Sensors[l].Objects[k].GetComponent<Renderer>().material.HasProperty("_Selected") && l!=i)
                                        {
                                            ThisProcess.Sensors[l].Objects[k].GetComponent<Renderer>().material.SetFloat("_DangerLevel", ThisProcess.Sensors[l].SensorStatus);
                                            ThisProcess.Sensors[l].Objects[k].GetComponent<Renderer>().material.SetFloat("_Selected", 0.0f);
                                            ThisProcess.Sensors[l].SensorUI.DrawPointer = false;
                                        }
                                    }
                                }
                                for (int l = 0; l < ThisProcess.Atuators.Length; l++)//atuadores
                                {
                                    for (int k = 0; k < ThisProcess.Atuators[l].Objects.Length; k++)
                                    {
                                        if (ThisProcess.Atuators[l].Objects[k].GetComponent<Renderer>().material.HasProperty("_Selected"))
                                        {
                                            ThisProcess.Atuators[l].Objects[k].GetComponent<Renderer>().material.SetFloat("_DangerLevel", ThisProcess.Atuators[l].AtuadorStatus);
                                            ThisProcess.Atuators[l].Objects[k].GetComponent<Renderer>().material.SetFloat("_Selected", 0.0f);
                                            ThisProcess.Atuators[l].AtuadorUI.DrawPointer = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //Atuadores
                    for (int i = 0; i < ThisProcess.Atuators.Length; i++)
                    {
                        for (int j = 0; j < ThisProcess.Atuators[i].Objects.Length; j++)
                        {
                            if (hit.collider.gameObject == ThisProcess.Atuators[i].Objects[j] && ThisProcess.Atuators[i].Objects[j].GetComponent<Renderer>().material.HasProperty("_Selected"))
                            {
                                SelectedObject = true;
                                //Para o sensor selecionado, realçe
                                for (int k = 0; k < ThisProcess.Atuators[i].Objects.Length; k++)
                                {
                                    if (ThisProcess.Atuators[i].Objects[k].GetComponent<Renderer>().material.HasProperty("_Selected"))
                                    {
                                        ThisProcess.Atuators[i].Objects[k].GetComponent<Renderer>().material.SetFloat("_DangerLevel", ThisProcess.Atuators[i].AtuadorStatus);
                                        ThisProcess.Atuators[i].Objects[k].GetComponent<Renderer>().material.SetFloat("_Selected", 1.0f);
                                        ThisProcess.Atuators[i].AtuadorUI.DrawPointer = true;
                                    }
                                }
                                //para os outros desabilite o realçe
                                for (int l = 0; l < ThisProcess.Atuators.Length; l++)//atuadores
                                {
                                    for (int k = 0; k < ThisProcess.Atuators[l].Objects.Length; k++)
                                    {
                                        if (ThisProcess.Atuators[l].Objects[k].GetComponent<Renderer>().material.HasProperty("_Selected") && l != i)
                                        {
                                            ThisProcess.Atuators[l].Objects[k].GetComponent<Renderer>().material.SetFloat("_DangerLevel", ThisProcess.Atuators[l].AtuadorStatus);
                                            ThisProcess.Atuators[l].Objects[k].GetComponent<Renderer>().material.SetFloat("_Selected", 0.0f);
                                            ThisProcess.Atuators[l].AtuadorUI.DrawPointer = false;
                                        }
                                    }
                                }
                                for (int l = 0; l < ThisProcess.Sensors.Length; l++)//sensores
                                {
                                    for (int k = 0; k < ThisProcess.Sensors[l].Objects.Length; k++)
                                    {
                                        if (ThisProcess.Sensors[l].Objects[k].GetComponent<Renderer>().material.HasProperty("_Selected"))
                                        {
                                            ThisProcess.Sensors[l].Objects[k].GetComponent<Renderer>().material.SetFloat("_DangerLevel", ThisProcess.Sensors[l].SensorStatus);
                                            ThisProcess.Sensors[l].Objects[k].GetComponent<Renderer>().material.SetFloat("_Selected", 0.0f);
                                            ThisProcess.Sensors[l].SensorUI.DrawPointer = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                SelectedObject = false;
                //Melhorar
                DisableAllObjectSelection(ThisProcess);
            }
            if (Input.GetMouseButtonDown(0) && MouseOverRect(FreeRectSpace) && SelectedObject)
            {
                SelectedObject = false;
                //Melhorar
                DisableAllObjectSelection(ThisProcess);
            }
        }

        public static void DisableAllObjectSelection(ProcessDetails ThisProcess)
        {
            //Sensores
            for (int i = 0; i < ThisProcess.Sensors.Length; i++)
            {
                for (int j = 0; j < ThisProcess.Sensors[i].Objects.Length; j++)
                {
                    if (ThisProcess.Sensors[i].Objects[j] != null)
                    {
                        if (ThisProcess.Sensors[i].Objects[j].GetComponent<Renderer>().material.HasProperty("_Selected"))
                        {
                            ThisProcess.Sensors[i].Objects[j].GetComponent<Renderer>().material.SetFloat("_Selected", 0.0f);
                            ThisProcess.Sensors[i].SensorUI.DrawPointer = false;
                        }
                    }
                }
            }
            //Atuadores
            for (int i = 0; i < ThisProcess.Atuators.Length; i++)
            {
                for (int j = 0; j < ThisProcess.Atuators[i].Objects.Length; j++)
                {
                    if (ThisProcess.Atuators[i].Objects[j] != null)
                    {
                        if (ThisProcess.Atuators[i].Objects[j].GetComponent<Renderer>().material.HasProperty("_Selected"))
                        {
                            ThisProcess.Atuators[i].Objects[j].GetComponent<Renderer>().material.SetFloat("_Selected", 0.0f);
                            ThisProcess.Atuators[i].AtuadorUI.DrawPointer = false;
                        }
                    }
                }
            }
        }

        //Desenha os detalhes de um sensor
        public static bool Button(UIButtonData UiSource)
        {
            int fontsize = FontSize + Mathf.RoundToInt(UiSource.SizeY / 25); ;
            GUI.skin.label.fontSize = fontsize;
            UiSource.MouseClick = false;

            if (!UiSource.HardMove) ButtonUiUpdate(UiSource);
            mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            if (mousePosition.x >= (UiSource.PosX) && mousePosition.x <= (UiSource.PosX + UiSource.SizeX) && mousePosition.y >= (UiSource.PosY) && mousePosition.y <= (UiSource.PosY + UiSource.SizeY))
            {
                if (UiSource.MouseOver == false) UiSource.MouseEnter = true;
                UiSource.MouseOver = true;
            }
            else { UiSource.MouseOver = false; }

            if (UiSource.MouseEnter)
            {
                UiSource.MouseEnter = false;
            }

            if (Input.GetMouseButtonDown(0) && UiSource.MouseOver && Time.time - UiSource.ClickTime > UiSource.WaitingTime)
            {
                //UiSource.Toggle = !UiSource.Toggle;
                if (UiSource.PlaySound) PlaySound(CoreObject, 0, UiSource.SoundIntensity);
                UiSource.ClickTime = Time.time;
                UiSource.MouseClick = true;
                UiSource.Selected = !UiSource.Selected;
                UiSource.Toggle = !UiSource.Toggle;
            }

            rectDraw = new Rect(UiSource.PosX, UiSource.PosY, UiSource.SizeX, UiSource.SizeY);
            if (!UiSource.MouseOver)
                GUI.DrawTexture(rectDraw, BlackTextureAlpha20); //quadro traseiro
            if (UiSource.MouseOver)
                GUI.DrawTexture(rectDraw, Gray10TextureAlpha60); //quadro traseiro se selecionado
            if (Input.GetMouseButton(0) && UiSource.MouseOver)
                GUI.DrawTexture(rectDraw, Gray50Texture); //quadro traseiro se clicado
            if (UiSource.BarDown)
            {
                rectDraw = new Rect(UiSource.PosX, UiSource.PosY + (UiSource.SizeY - fontsize / 4), UiSource.SizeX, fontsize / 4);
                if (!UiSource.MouseOver)
                    GUI.DrawTexture(rectDraw, GrayTextureAlpha50); //linha inferior
                if (UiSource.MouseOver || UiSource.Toggle)
                    GUI.DrawTexture(rectDraw, WhiteTexture); //linha inferior
            }
            if (UiSource.BarRight)
            {
                rectDraw = new Rect(UiSource.PosX + UiSource.SizeX, UiSource.PosY, fontsize / 4, UiSource.SizeY);
                if (!UiSource.MouseOver)
                    GUI.DrawTexture(rectDraw, GrayTextureAlpha50); //linha direita
                if (UiSource.MouseOver || UiSource.Toggle)
                    GUI.DrawTexture(rectDraw, WhiteTexture); //linha direita
            }

            var centeredStyle = GUI.skin.GetStyle("Label");
            if (UiSource.Icon == null)
            {
                centeredStyle.alignment = TextAnchor.MiddleCenter;
                GUI.Label(new Rect(UiSource.PosX, UiSource.PosY, UiSource.SizeX, UiSource.SizeY), UiSource.Text); //Texto do botão
            }
            if (UiSource.Text == null || UiSource.Text == string.Empty)
            {
                rectDraw = new Rect(UiSource.PosX + (UiSource.SizeX / 2 - (UiSource.SizeY - UiSource.SizeY / 4) / 2), UiSource.PosY + UiSource.SizeY / 8, UiSource.SizeY - UiSource.SizeY / 4, UiSource.SizeY - UiSource.SizeY / 4);
                GUI.DrawTexture(rectDraw, UiSource.Icon); //Icone
            }

            centeredStyle.alignment = TextAnchor.UpperLeft;

            if (UiSource.Icon != null && UiSource.Text != string.Empty)//Icone e texto
            {
                if (UiSource.Text.Length > (UiSource.SizeX - UiSource.SizeY - fontsize / 2) / (3 * fontsize / 5) + 1 && 2 * fontsize < UiSource.SizeY)
                {
                    GUI.Label(new Rect(UiSource.SizeY + UiSource.PosX + 4 * fontsize / 5, UiSource.PosY + UiSource.SizeY / 2 - 12 * fontsize / 10, UiSource.SizeX - UiSource.SizeY - fontsize, 3 * fontsize), UiSource.Text); //Texto do botão
                }
                else
                {
                    GUI.Label(new Rect(UiSource.SizeY + UiSource.PosX + 4 * fontsize / 5, UiSource.PosY + UiSource.SizeY / 2 - 8 * fontsize / 10, UiSource.SizeX - UiSource.SizeY - fontsize, 18 * fontsize / 10), UiSource.Text); //Texto do botão
                }

                rectDraw = new Rect(UiSource.PosX + UiSource.SizeY / 6, UiSource.PosY + UiSource.SizeY / 6, UiSource.SizeY - UiSource.SizeY / 3, UiSource.SizeY - UiSource.SizeY / 3);
                GUI.DrawTexture(rectDraw, UiSource.Icon);
            }

            return UiSource.MouseClick;
        }

        public static void ButtonGroup(UIbuttonGroupData UiGroupSource, UIButtonData[] ButtonList)
        {
            mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            if (mousePosition.x >= (UiGroupSource.PosX * Screen.width) && mousePosition.x <= (UiGroupSource.PosX + UiGroupSource.SizeX) * Screen.width && mousePosition.y >= (UiGroupSource.PosY * Screen.height) && mousePosition.y <= (UiGroupSource.PosY + UiGroupSource.SizeY) * Screen.height)
            {
                if (UiGroupSource.MouseOver == false) UiGroupSource.MouseEnter = true;
                UiGroupSource.MouseOver = true;
                UiGroupSource.SizeX = UiGroupSource.MaxSizeX;
            }
            else
            {
                UiGroupSource.MouseOver = false;
                UiGroupSource.SizeX = UiGroupSource.MinSizeX;
            }

            rectDraw = new Rect(UiGroupSource.PosX * Screen.width, UiGroupSource.PosY * Screen.height, UiGroupSource.SizeX * Screen.width, UiGroupSource.SizeY * Screen.height);
            GUI.DrawTexture(rectDraw, BlackTextureAlpha20); //quadro traseiro

            if (ButtonList.Length > 0)
            {
                //Desenho dos Botões
                for (int i = 0; i < ButtonList.Length; i++)
                {
                    ButtonList[i].TransitionSpeed = UiGroupSource.TransitionSpeed;
                    ButtonList[i].TargetPosX = Mathf.RoundToInt(UiGroupSource.PosX * Screen.width);
                    ButtonList[i].TargetPosY = Mathf.RoundToInt(UiGroupSource.PosY * Screen.height + i * (UiGroupSource.MinSizeX * Screen.width + Padding / 4));
                    ButtonList[i].TargetSizeX = Mathf.RoundToInt(UiGroupSource.SizeX * Screen.width);
                    ButtonList[i].TargetSizeY = Mathf.RoundToInt(UiGroupSource.MinSizeX * Screen.width);
                    if (Button(ButtonList[i])) { UnityEngine.Debug.Log("OK"); }

                }
            }
            else
            {
                GUI.Label(new Rect(UiGroupSource.PosX * Screen.width, UiGroupSource.PosY * Screen.height, 100, 20), "Sem botões");
            }
        }

        public static void MainMenuButtonGroup(UIbuttonGroupData UiGroupSource, UIButtonData[] ButtonList, UIAtuadorGroupData AtuadoresMenu)
        {
            mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            if (mousePosition.x >= (UiGroupSource.PosX * Screen.width) && mousePosition.x <= (UiGroupSource.PosX + UiGroupSource.SizeX) * Screen.width && mousePosition.y >= (UiGroupSource.PosY * Screen.height) && mousePosition.y <= (UiGroupSource.PosY + UiGroupSource.SizeY) * Screen.height)
            {
                if (UiGroupSource.MouseOver == false) UiGroupSource.MouseEnter = true;
                UiGroupSource.MouseOver = true;
                UiGroupSource.SizeX = UiGroupSource.MaxSizeX;
            }
            else
            {
                if (UiGroupSource.MouseOver == true) UiGroupSource.MouseOut = true;
                UiGroupSource.MouseOver = false;
                UiGroupSource.SizeX = UiGroupSource.MinSizeX;
            }
            if (UiGroupSource.MouseEnter)
            {
                MoveAtuadores(AtuadoresMenu, UiGroupSource.MaxSizeX - UiGroupSource.MinSizeX);
            }
            if (UiGroupSource.MouseOut)
            {
                MoveAtuadores(AtuadoresMenu, -(UiGroupSource.MaxSizeX - UiGroupSource.MinSizeX));
            }

            UiGroupSource.MouseEnter = false;
            UiGroupSource.MouseOut = false;

            rectDraw = new Rect(UiGroupSource.PosX * Screen.width, UiGroupSource.PosY * Screen.height, UiGroupSource.SizeX * Screen.width, UiGroupSource.SizeY * Screen.height);
            GUI.DrawTexture(rectDraw, BlackTextureAlpha20); //quadro traseiro

            if (ButtonList.Length > 0)
            {
                //Desenho dos Botões
                for (int i = 0; i < ButtonList.Length; i++)
                {
                    ButtonList[i].TransitionSpeed = UiGroupSource.TransitionSpeed;
                    ButtonList[i].TargetPosX = Mathf.RoundToInt(UiGroupSource.PosX * Screen.width);
                    ButtonList[i].TargetPosY = Mathf.RoundToInt(UiGroupSource.PosY * Screen.height + i * (UiGroupSource.MinSizeX * Screen.width + Padding / 4));
                    ButtonList[i].TargetSizeX = Mathf.RoundToInt(UiGroupSource.SizeX * Screen.width);
                    ButtonList[i].TargetSizeY = Mathf.RoundToInt(UiGroupSource.MinSizeX * Screen.width);

                    if (Button(ButtonList[i])) { 
                        //UnityEngine.Debug.Log("OK"); 
                    }

                }
            }
            else
            {
                GUI.Label(new Rect(UiGroupSource.PosX * Screen.width, UiGroupSource.PosY * Screen.height, 100, 20), "Sem botões");
            }
        }
        //Desenha a logo da empresa
        public static void CompanyLogo(ProcessDetails ThisProcess, UIbuttonGroupData UiGroupSource)
        {
            if (Time.time > 3f)
            {
                ThisProcess.CompanyLogoRect = new Rect(Screen.width - Padding - Screen.height / 6, UiGroupSource.PosY * Screen.height + UiGroupSource.SizeY * Screen.height / 5 + Padding, Screen.height / 6, Screen.height / 24);
                GUI.DrawTexture(ThisProcess.CompanyLogoRect, Gray12Texture);
                GUI.DrawTexture(ThisProcess.CompanyLogoRect, ThisProcess.CompanyLogo); //Padrao
            }
        }

        //Botões da janela (Windows)
        public static void WindowsButtons(UIbuttonGroupData UiGroupSource, UIButtonData[] ButtonList, ProcessDetails ThisProcess)
        {
            mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            if (mousePosition.x >= (UiGroupSource.PosX * Screen.width) && mousePosition.x <= (UiGroupSource.PosX + UiGroupSource.SizeX) * Screen.width && mousePosition.y >= (UiGroupSource.PosY * Screen.height) && mousePosition.y <= (UiGroupSource.PosY + UiGroupSource.SizeY) * Screen.height)
            {
                if (UiGroupSource.MouseOver == false) UiGroupSource.MouseEnter = true;
                UiGroupSource.MouseOver = true;
            }
            else
            {
                if (UiGroupSource.MouseOver == true) UiGroupSource.MouseOut = true;
                UiGroupSource.MouseOver = false;
            }

            UiGroupSource.MouseEnter = false;
            UiGroupSource.MouseOut = false;

            //Conexão via cabo, rede local ou radio
            if (ThisProcess.CableConnection.Connected || ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer) ButtonList[1].Icon = HardwareConnectionOn; //Conexão do processo On
            if (!(ThisProcess.CableConnection.Connected || ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer) && ThisProcess.RemoteProcessConnection.ConnectedRPC) ButtonList[1].Icon = HardwareConnectionDisabled; //Conexão do processo desabilitada pois existe uma conexão online
            if (!(ThisProcess.CableConnection.Connected || ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer) && !ThisProcess.RemoteProcessConnection.ConnectedRPC) ButtonList[1].Icon = HardwareConnectionOff; //Conexão do processo Off
            //Conexão via rede
            if (!ThisProcess.RemoteProcessConnection.ConnectedRPC) ButtonList[2].Icon = WebComDisabled; //Conexão do processo desabilitada
            if (ThisProcess.RemoteProcessConnection.ConnectedRPC) ButtonList[2].Icon = WebComOn; //Conexão do processo desabilitada pois existe uma conexão online
            if (!ThisProcess.CableConnection.Connected && !ThisProcess.RemoteProcessConnection.ConnectedRPC) ButtonList[2].Icon = WebComOff; //Conexão do processo Off

            if (ThisProcess.WebConnection.Connected) ButtonList[3].Icon = InternetOn; //Conexão da internet On
            if (!ThisProcess.WebConnection.Connected) ButtonList[3].Icon = InternetOff; //Conexão da internet Off

            if (ThisProcess.DeviceBattery.BatteryLevel > ThisProcess.DeviceBattery.LowValue || ThisProcess.DeviceBattery.BatteryLevel == -1f) ButtonList[0].Icon = BatteryFull; //Bateria Ok
            if (ThisProcess.DeviceBattery.BatteryLevel > ThisProcess.DeviceBattery.CriticalValue && ThisProcess.DeviceBattery.BatteryLevel <= ThisProcess.DeviceBattery.LowValue) ButtonList[0].Icon = BatteryLow; //Bateria esgotando
            if (ThisProcess.DeviceBattery.BatteryLevel <= ThisProcess.DeviceBattery.CriticalValue && ThisProcess.DeviceBattery.BatteryLevel > -0.1f) ButtonList[0].Icon = BatteryCritical; //Bateria critica

            //rectDraw = new Rect(UiGroupSource.PosX * Screen.width + (UiGroupSource.SizeX * Screen.width) / 5 + Padding / 4, UiGroupSource.PosY * Screen.height, UiGroupSource.SizeY * Screen.height / 5, UiGroupSource.SizeY * Screen.height / 5);
            //GUI.DrawTexture(rectDraw, InternetOn); //Conexão de internet

            if (ButtonList.Length > 0)
            {
                //Desenho dos Botões
                for (int i = 0; i < ButtonList.Length; i++)
                {
                    ButtonList[i].TransitionSpeed = UiGroupSource.TransitionSpeed;
                    ButtonList[i].TargetPosX = Mathf.RoundToInt(UiGroupSource.PosX * Screen.width + (i) * ((UiGroupSource.SizeX * Screen.width) / 5 + Padding / 4));
                    ButtonList[i].TargetPosY = Mathf.RoundToInt(UiGroupSource.PosY * Screen.height);
                    ButtonList[i].TargetSizeX = Mathf.RoundToInt(UiGroupSource.SizeX * Screen.width / 5);
                    ButtonList[i].TargetSizeY = Mathf.RoundToInt(UiGroupSource.SizeY * Screen.height / 5);
                    if (Button(ButtonList[i])) { UnityEngine.Debug.Log("Um dos botões de janela foi apertado"); }
                }
            }
            if (ButtonList[0].MouseClick)
            {
                if (ThisProcess.DeviceBattery.BatteryLevel == -1f) Speak("O Computador não depende de bateria"); //Não depende de bateria
                if (ThisProcess.DeviceBattery.BatteryLevel > ThisProcess.DeviceBattery.LowValue)
                {
                    if (ThisProcess.DeviceBattery.ChargerConnected)
                        Speak("A bateria está a " + (ThisProcess.DeviceBattery.BatteryLevel * 100).ToString() + " porcento"); //Bateria Ok
                    if (!ThisProcess.DeviceBattery.ChargerConnected)
                        Speak("A bateria está a " + (ThisProcess.DeviceBattery.BatteryLevel * 100).ToString() + " porcento, e o carregador está desconectado"); //Bateria Ok mas descarregando
                }
                if (ThisProcess.DeviceBattery.BatteryLevel > ThisProcess.DeviceBattery.CriticalValue && ThisProcess.DeviceBattery.BatteryLevel <= ThisProcess.DeviceBattery.LowValue)
                {
                    if (ThisProcess.DeviceBattery.ChargerConnected)
                        Speak("A bateria está a " + (ThisProcess.DeviceBattery.BatteryLevel * 100).ToString() + " porcento."); //Bateria esgotando
                    if (!ThisProcess.DeviceBattery.ChargerConnected)
                        Speak("A bateria está a " + (ThisProcess.DeviceBattery.BatteryLevel * 100).ToString() + " porcento. por favor. conecte o carregador"); //Bateria esgotando
                }
                if (ThisProcess.DeviceBattery.BatteryLevel <= ThisProcess.DeviceBattery.CriticalValue && ThisProcess.DeviceBattery.BatteryLevel > -0.1f)
                {
                    if (ThisProcess.DeviceBattery.ChargerConnected)
                        Speak("A bateria está a " + (ThisProcess.DeviceBattery.BatteryLevel * 100).ToString() + " porcento. e está em um nível crítico"); //Bateria critica
                    if (!ThisProcess.DeviceBattery.ChargerConnected)
                        Speak("A bateria está a " + (ThisProcess.DeviceBattery.BatteryLevel * 100).ToString() + " porcento. por favor. conecte urgentemente o carregador"); //Bateria critica e descarregando
                }
            }
            if (ButtonList[1].MouseClick)
            {
                if (ThisProcess.CableConnection.Connected) Speak("O processo está conectado diretamente via cabo a este computador"); //Conexão do processo On via cabo
                if (!ThisProcess.Connected) Speak("Este dispositivo não está conectado ao processo. Estou verificando a conexão"); //Conexão do processo Off
                if (!ThisProcess.CableConnection.Connected && ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer) Speak("Este dispositivo está conectado via rede local ao processo, não por meio de um cabo"); //Conexão via cabo do processo Off, mas conectado pela rede local
                if (!ThisProcess.CableConnection.Connected && ThisProcess.RemoteProcessConnection.ConnectedRPC) Speak("Este dispositivo não está conectado diretamente ao processo, ele está conectado via internet"); //Conexão via cabo do processo Off, mas conectado pela internet
            }
            if (ButtonList[2].MouseClick)
            {
                if (ThisProcess.RemoteProcessConnection.ConnectedRPC && ThisProcess.RemoteProcessConnection.RPCClient) Speak("Este dispositivo está conectado a um computador remoto, o qual está conectado ao processo"); //Conexão remota
                if (ThisProcess.RemoteProcessConnection.ConnectedRPC && ThisProcess.RemoteProcessConnection.RPCHost) Speak("Este computador é o servidor dos dados para outros dispositivos via internet"); //Conexão remota
                if (!ThisProcess.RemoteProcessConnection.ConnectedRPC) Speak("Este computador não está participando de uma conexão remota"); //Conexão Off
            }
            if (ButtonList[3].MouseClick)
            {
                if (ThisProcess.WebConnection.Connected) Speak("Este computador está conectado a internet"); //Conexão da internet On
                if (!ThisProcess.WebConnection.Connected) Speak("Este computador não está conectado a internet. por favor, verifique a conexão"); //Conexão da internet Off
            }
            if (ButtonList[4].MouseClick)
            {
                Common.Minimize();
            }
            if (ButtonList[5].MouseClick)
            {
                Common.FullScreen(!ButtonList[3].Toggle);
            }
            if (ButtonList[6].MouseClick)
            {
                //Adicionar rotinas de segurança, como desligando todos os equipamentos
                //Fazer isso para o fechamento da janela também
                CloseSerial(ThisProcess);
                Application.Quit();
            }
        }


        //Desenha os detalhes de um sensor
        public static bool FigureButton(UIButtonData UiSource)
        {
            int fontsize = FontSize + Mathf.RoundToInt(UiSource.SizeY * UiSource.SizeX / 3000); ;
            GUI.skin.label.fontSize = fontsize;
            UiSource.MouseClick = false;

            if (!UiSource.HardMove) ButtonUiUpdate(UiSource);
            mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            if (mousePosition.x >= (UiSource.PosX) && mousePosition.x <= (UiSource.PosX + UiSource.SizeX) && mousePosition.y >= (UiSource.PosY) && mousePosition.y <= (UiSource.PosY + UiSource.SizeY))
            {
                if (UiSource.MouseOver == false) UiSource.MouseEnter = true;
                UiSource.MouseOver = true;
            }
            else { UiSource.MouseOver = false; }

            if (UiSource.MouseEnter)
            {
                UiSource.MouseEnter = false;
            }

            if (Input.GetMouseButtonDown(0) && UiSource.MouseOver && Time.time - UiSource.ClickTime > UiSource.WaitingTime)
            {
                if (UiSource.PlaySound) PlaySound(CoreObject, 0, UiSource.SoundIntensity);
                UiSource.ClickTime = Time.time;
                UiSource.MouseClick = true;
                UiSource.Selected = !UiSource.Selected;
            }

            rectDraw = new Rect(UiSource.PosX, UiSource.PosY, UiSource.SizeX, UiSource.SizeY);
            if (!UiSource.Selected)
            {
                if (!UiSource.MouseOver)
                    GUI.DrawTexture(rectDraw, UiSource.Icon); //Padrao
                if (UiSource.MouseOver)
                    GUI.DrawTexture(rectDraw, UiSource.Icon2); //Padrao
            }
            if (UiSource.Selected)
            {
                if (!UiSource.MouseOver)
                    GUI.DrawTexture(rectDraw, UiSource.Icon3); //Padrao
                if (UiSource.MouseOver)
                    GUI.DrawTexture(rectDraw, UiSource.Icon4); //Padrao
            }

            return UiSource.MouseClick;
        }

        //Desenha um checkbox
        public static bool Toggle(UIToggleData UiSource, bool Variable)
        {
            //int fontsize = Mathf.RoundToInt(8f*UiSource.DrawRect.height/10f);
            GUI.skin.label.fontSize = FontSize;
            UiSource.MouseClick = false;
            bool result = Variable;

            if (UiSource.MouseEnter)
            {
                UiSource.MouseEnter = false;
            }

            if (MouseOverRect(UiSource.DrawRect))
            {
                if (UiSource.MouseOver == false) UiSource.MouseEnter = true;
                UiSource.MouseOver = true;
                GUI.DrawTexture(UiSource.DrawRect, Gray50Texture);
                if (Input.GetMouseButtonDown(0) && Time.time - UiSource.ClickTime > UiSource.WaitingTime)
                {
                    if (UiSource.PlaySound) PlaySound(CoreObject, 0, UiSource.SoundIntensity);
                    UiSource.ClickTime = Time.time;
                    UiSource.MouseClick = true;
                    result = !Variable;
                }
            }
            else
            {
                GUI.DrawTexture(UiSource.DrawRect, Gray25Texture);
            }

            //var centeredStyle = GUI.skin.GetStyle("Label");
            //centeredStyle.alignment = TextAnchor.UpperLeft;

            if (UiSource.Text.Length > (UiSource.DrawRect.width - UiSource.DrawRect.height - FontSize / 2) / (3 * FontSize / 5) + 1 && 2 * FontSize < UiSource.DrawRect.height)
            {
                GUI.Label(new Rect(UiSource.DrawRect.height + UiSource.DrawRect.x + 4 * FontSize / 5, UiSource.DrawRect.y + UiSource.DrawRect.height / 2 - 12 * FontSize / 10, UiSource.DrawRect.width - UiSource.DrawRect.height - FontSize, 3 * FontSize), UiSource.Text); //Texto
            }
            else
            {
                GUI.Label(new Rect(UiSource.DrawRect.height + UiSource.DrawRect.x + 4 * FontSize / 5, UiSource.DrawRect.y + UiSource.DrawRect.height / 2 - 9*FontSize/10, UiSource.DrawRect.width - UiSource.DrawRect.height - FontSize, 2 * FontSize), UiSource.Text); //Texto
            }

            rectDraw = new Rect(UiSource.DrawRect.x + UiSource.DrawRect.height / 6, UiSource.DrawRect.y + UiSource.DrawRect.height / 6, UiSource.DrawRect.height - UiSource.DrawRect.height / 3, UiSource.DrawRect.height - UiSource.DrawRect.height / 3);
            if(result) 
            GUI.DrawTexture(rectDraw, WhiteTexture);
            else
            GUI.DrawTexture(rectDraw, BlackTexture);

            return result;
        }

        //Desenha um ComboBox
        public static int ComboBox(UIComboBoxData UiSource)
        {
            int fontsize = FontSize + Mathf.RoundToInt(UiSource.Rect.width * UiSource.Rect.height / 3000); ;
            GUI.skin.label.fontSize = fontsize;
            UiSource.MouseClick = false;

            //Corrige itens selecionados fora da faixa
            if (UiSource.SelectedItem < 0) UiSource.SelectedItem = 0;
            if (UiSource.SelectedItem >= UiSource.Texts.Length) UiSource.SelectedItem = UiSource.Texts.Length - 1;

            mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            if (MouseOverRect(UiSource.Rect))
            {
                if (UiSource.MouseOver == false) UiSource.MouseEnter = true;
                UiSource.MouseOver = true;
            }
            else { UiSource.MouseOver = false; }

            if (UiSource.MouseEnter)
            {
                UiSource.MouseEnter = false;
            }

            if (Input.GetMouseButtonDown(0) && UiSource.MouseOver && Time.time - UiSource.ClickTime > UiSource.WaitingTime)
            {
                //UiSource.Toggle = !UiSource.Toggle;
                if (UiSource.PlaySound) PlaySound(CoreObject, 0, UiSource.SoundIntensity);
                UiSource.ClickTime = Time.time;
                UiSource.MouseClick = true;
                UiSource.Selected = !UiSource.Selected;
            }

            if (!UiSource.MouseOver)
                GUI.DrawTexture(UiSource.Rect, Gray20Texture); //quadro traseiro
            if (UiSource.MouseOver)
                GUI.DrawTexture(UiSource.Rect, Gray12Texture); //quadro traseiro se selecionado
            if (Input.GetMouseButton(0) && UiSource.MouseOver)
                GUI.DrawTexture(UiSource.Rect, Gray50Texture); //quadro traseiro se clicado

            rectDraw = new Rect(UiSource.Rect.x + UiSource.Rect.width - UiSource.Rect.height, UiSource.Rect.y, UiSource.Rect.height, UiSource.Rect.height);
            GUI.DrawTexture(rectDraw, UiSource.Arrow); //Seta de opções

            var centeredStyle = GUI.skin.GetStyle("Label");
            if (UiSource.Icons == null || UiSource.Icons.Length == 0)
            {
                centeredStyle.alignment = TextAnchor.MiddleLeft;
                rectDraw = new Rect(UiSource.Rect.x + Padding, UiSource.Rect.y, UiSource.Rect.width, UiSource.Rect.height);
                GUI.Label(rectDraw, UiSource.Texts[UiSource.SelectedItem]); //Texto do botão
            }

            //Desenha as opções
            if (UiSource.Selected) {
                rectDraw = new Rect(UiSource.Rect.x, UiSource.Rect.y + UiSource.Rect.height, UiSource.Rect.width, UiSource.Rect.height * UiSource.Texts.Length);
                GUI.DrawTexture(rectDraw, Gray20Texture);

                for (int i = 0; i < UiSource.Texts.Length; i++)
                {
                    rectDraw = new Rect(UiSource.Rect.x, UiSource.Rect.y + UiSource.Rect.height * (i + 1), UiSource.Rect.width, UiSource.Rect.height);

                    if (MouseOverRect(rectDraw))
                    {
                        //UiSource.MouseOver = true;
                        if (Input.GetMouseButtonDown(0))
                        {
                            GUI.DrawTexture(rectDraw, Gray50Texture);
                            UiSource.MouseClick = true;
                            UiSource.Selected = false;
                            UiSource.SelectedItem = i;
                        }
                        else
                        {
                            GUI.DrawTexture(rectDraw, Gray12Texture);
                        }
                    }
                    rectDraw = new Rect(UiSource.Rect.x + Padding, UiSource.Rect.y + UiSource.Rect.height * (i + 1), UiSource.Rect.width, UiSource.Rect.height);
                    GUI.Label(rectDraw, UiSource.Texts[i]);
                }
            }
            //deselecionar clicando fora
            if (Input.GetMouseButtonDown(0) && !UiSource.MouseOver && UiSource.Selected)
            {
                UiSource.Selected = false;
            }

            if (UiSource.Texts == null)
            {

            }

            centeredStyle.alignment = TextAnchor.UpperLeft;

            if (UiSource.Icons != null)//Icone e texto
            {

            }

            return UiSource.SelectedItem;
        }

        //Desenha os detalhes de um sensor
        public static float Slider(UISliderData UiSource)
        {
            int fontsize = FontSize + Mathf.RoundToInt(UiSource.SizeY * UiSource.SizeX / 5000); ;
            GUI.skin.label.fontSize = fontsize;

            //if (!UiSource.HardMove) ButtonUiUpdate(UiSource);
            mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            UiSource.MousePosition = mousePosition;
            if (mousePosition.x >= (UiSource.PosX) && mousePosition.x <= (UiSource.PosX + UiSource.SizeX) && mousePosition.y >= (UiSource.PosY) && mousePosition.y <= (UiSource.PosY + UiSource.SizeY))
            {
                if (UiSource.MouseOver == false) UiSource.MouseEnter = true;
                UiSource.MouseOver = true;
            }
            else { UiSource.MouseOver = false; }

            if (UiSource.MouseEnter)
            {
                UiSource.MouseEnter = false;
            }

            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperLeft;

            if (UiSource.MouseOver && Time.time - UiSource.ClickTime > UiSource.WaitingTime)
            {
                if (UiSource.PlaySound) PlaySound(CoreObject, 0, UiSource.SoundIntensity);
                UiSource.Selected = !UiSource.Selected;
            }

            rectDraw = new Rect(UiSource.PosX, UiSource.PosY, UiSource.SizeX, UiSource.SizeY);
            if (!UiSource.MouseOver)
            {
                GUI.DrawTexture(rectDraw, BlackTextureAlpha20); //Padrao
                rectDraw = new Rect(UiSource.PosX, UiSource.PosY, fontsize / 4, UiSource.SizeY);
                GUI.DrawTexture(rectDraw, WhiteTextureAlpha50);
            }
            if (UiSource.MouseOver)
            {
                GUI.DrawTexture(rectDraw, Gray10TextureAlpha60); //Padrao
                rectDraw = new Rect(UiSource.PosX, UiSource.PosY, fontsize / 4, UiSource.SizeY);
                GUI.DrawTexture(rectDraw, WhiteTexture);
            }
            GUI.skin.label.fontSize = 8 * fontsize / 10;
            GUI.Label(new Rect(UiSource.PosX + fontsize, UiSource.PosY + UiSource.SizeY / 2 - 3 * fontsize / 4, UiSource.SizeX / 3, fontsize * 2), UiSource.Text);
            GUI.skin.label.fontSize = 2 * fontsize / 3;
            GUI.Label(new Rect(UiSource.PosX + UiSource.SizeX / 3 + fontsize, UiSource.PosY + UiSource.SizeY / 8, UiSource.SizeX / 6, fontsize), UiSource.Min.ToString("G3"));
            centeredStyle.alignment = TextAnchor.UpperRight;
            GUI.Label(new Rect(UiSource.PosX + UiSource.SizeX - fontsize - UiSource.SizeX / 6, UiSource.PosY + UiSource.SizeY / 8, UiSource.SizeX / 6, fontsize), UiSource.Max.ToString("G3"));
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(UiSource.PosX + 2 * UiSource.SizeX / 3 - UiSource.SizeX / 12, UiSource.PosY + UiSource.SizeY / 8, UiSource.SizeX / 6, fontsize), UiSource.Value.ToString("G3"));
            centeredStyle.alignment = TextAnchor.UpperLeft;

            rectDraw = new Rect(UiSource.PosX + UiSource.SizeX / 3 + fontsize, UiSource.PosY + 2 * UiSource.SizeY / 3 - fontsize / 10, 2 * UiSource.SizeX / 3 - 2 * fontsize, fontsize / 5);
            if (UiSource.LineTexture == null)
            {
                if (UiSource.Enabled)
                    GUI.DrawTexture(rectDraw, WhiteTexture); //branco
                else
                    GUI.DrawTexture(rectDraw, Gray50Texture); //branco
            }
            if (UiSource.LineTexture != null)
            {
                if (UiSource.Enabled)
                    GUI.DrawTexture(rectDraw, UiSource.LineTexture); //textura
                else
                    GUI.DrawTexture(rectDraw, Gray50Texture); //branco
            }

            UiSource.InitialX = UiSource.PosX + UiSource.SizeX / 3 + fontsize;
            UiSource.FinalX = UiSource.PosX + UiSource.SizeX / 3 + fontsize + 2 * UiSource.SizeX / 3 - 2 * fontsize;
            UiSource.SliderSize = 2 * UiSource.SizeX / 3 - 2 * fontsize;

            if ((mousePosition.x >= UiSource.InitialX
                && mousePosition.x <= UiSource.InitialX + UiSource.SliderSize
                && mousePosition.y >= (UiSource.PosY + 2 * UiSource.SizeY / 3 - fontsize / 2)
                && mousePosition.y <= (UiSource.PosY + 2 * UiSource.SizeY / 3 + fontsize / 2))
                || UiSource.Selected && UiSource.Enabled)
            {
                UiSource.MouseOverDotIcon = true;
                if (Input.GetMouseButton(0))
                {
                    UiSource.Selected = true;
                    UiSource.Fraction = (mousePosition.x - UiSource.InitialX) / (UiSource.SliderSize);
                    UiSource.Value = UiSource.Min + UiSource.Fraction * (UiSource.Max - UiSource.Min);
                }
            }
            if (Input.GetMouseButtonUp(0) && UiSource.Enabled)
            {
                if (UiSource.Selected) UiSource.Changed = true;
                UiSource.MouseOverDotIcon = false;
                UiSource.Selected = false;
            }


            UiSource.Fraction = (UiSource.Value - UiSource.Min) / (UiSource.Max - UiSource.Min);
            if (UiSource.Fraction < 0) { UiSource.Fraction = 0f; UiSource.Value = UiSource.Min; }
            if (UiSource.Fraction > 1) { UiSource.Fraction = 1f; UiSource.Value = UiSource.Max; }
            rectDraw = new Rect(UiSource.PosX + UiSource.SizeX / 3 + Mathf.RoundToInt(UiSource.Fraction * (2 * UiSource.SizeX / 3 - 3 * fontsize)) + fontsize, UiSource.PosY + 2 * UiSource.SizeY / 3 - fontsize / 2, fontsize, fontsize);
            if (UiSource.DotIcon != null)
            {
                if (UiSource.Enabled)
                    GUI.DrawTexture(rectDraw, UiSource.DotIcon); //Imagem
                else
                    GUI.DrawTexture(rectDraw, UiSource.DotIconDisabled); //Imagem
            }
            if (UiSource.DotIcon == null)
            {
                if (UiSource.Enabled)
                    GUI.DrawTexture(rectDraw, WhiteTexture); //branco
                else
                    GUI.DrawTexture(rectDraw, Gray50Texture); //branco
            }

            return UiSource.Value;
        }

        //Desenha um slider simplificado
        public static float SimpleSlider(UISliderData UiSource)
        {
            int fontsize = FontSize;
            GUI.skin.label.fontSize = fontsize;

            //if (!UiSource.HardMove) ButtonUiUpdate(UiSource);
            mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            UiSource.MousePosition = mousePosition;
            if (mousePosition.x >= (UiSource.PosX) && mousePosition.x <= (UiSource.PosX + UiSource.SizeX) && mousePosition.y >= (UiSource.PosY - UiSource.SizeY) && mousePosition.y <= (UiSource.PosY))
            {
                if (UiSource.MouseOver == false) UiSource.MouseEnter = true;
                UiSource.MouseOver = true;
            }
            else { UiSource.MouseOver = false; }

            if (UiSource.MouseEnter)
            {
                UiSource.MouseEnter = false;
            }

            if (UiSource.MouseOver && Time.time - UiSource.ClickTime > UiSource.WaitingTime)
            {
                if (UiSource.PlaySound) PlaySound(CoreObject, 0, UiSource.SoundIntensity);
                UiSource.Selected = !UiSource.Selected;
            }

            rectDraw = new Rect(UiSource.PosX, UiSource.PosY, UiSource.SizeX, fontsize / 5);
            if (UiSource.LineTexture == null)
            {
                if (UiSource.Enabled)
                    GUI.DrawTexture(rectDraw, WhiteTexture); //branco
                else
                    GUI.DrawTexture(rectDraw, Gray50Texture); //branco
            }
            if (UiSource.LineTexture != null)
            {
                if (UiSource.Enabled)
                    GUI.DrawTexture(rectDraw, UiSource.LineTexture); //textura
                else
                    GUI.DrawTexture(rectDraw, Gray50Texture); //branco
            }

            UiSource.InitialX = UiSource.PosX - fontsize / 2;
            UiSource.FinalX = UiSource.PosX + UiSource.SizeX - fontsize / 2;
            UiSource.SliderSize = UiSource.FinalX - UiSource.InitialX;

            if (((mousePosition.x >= UiSource.InitialX
                && mousePosition.x <= UiSource.FinalX
                && mousePosition.y >= (UiSource.PosY - fontsize / 2)
                && mousePosition.y <= (UiSource.PosY + fontsize / 2))
                || UiSource.Selected) && UiSource.Enabled)
            {
                UiSource.MouseOverDotIcon = true;
                if (Input.GetMouseButton(0))
                {
                    UiSource.Selected = true;
                    UiSource.Fraction = (mousePosition.x - UiSource.InitialX) / (UiSource.SliderSize);
                    UiSource.Value = UiSource.Min + UiSource.Fraction * (UiSource.Max - UiSource.Min);
                }
            }
            if (Input.GetMouseButtonUp(0) && UiSource.Enabled)
            {
                if (UiSource.Selected) UiSource.Changed = true;
                UiSource.MouseOverDotIcon = false;
                UiSource.Selected = false;
            }


            UiSource.Fraction = (UiSource.Value - UiSource.Min) / (UiSource.Max - UiSource.Min);
            if (UiSource.Fraction < 0) { UiSource.Fraction = 0f; UiSource.Value = UiSource.Min; }
            if (UiSource.Fraction > 1) { UiSource.Fraction = 1f; UiSource.Value = UiSource.Max; }
            rectDraw = new Rect(UiSource.PosX - fontsize / 2 + Mathf.RoundToInt(UiSource.Fraction * UiSource.SizeX), UiSource.PosY - fontsize / 2, fontsize, fontsize);
            if (UiSource.DotIcon != null)
            {
                if (UiSource.Enabled)
                    GUI.DrawTexture(rectDraw, UiSource.DotIcon); //Imagem
                else
                    GUI.DrawTexture(rectDraw, UiSource.DotIconDisabled); //Imagem
            }
            if (UiSource.DotIcon == null)
            {
                if (UiSource.Enabled)
                    GUI.DrawTexture(rectDraw, WhiteTexture); //branco
                else
                    GUI.DrawTexture(rectDraw, Gray50Texture); //branco
            }

            return UiSource.Value;
        }

        //Desenha os detalhes de um sensor
        public static void SensorDetails(Sensor PointedSensor, ProcessDetails ThisProcess)
        {
            PointedSensor.SensorUI.FontChanged = false;
            if (PointedSensor.SensorUI.LocalFontSize != FontSize) PointedSensor.SensorUI.FontChanged = true;
            PointedSensor.SensorUI.LocalFontSize = FontSize;// + Mathf.RoundToInt(PointedSensor.SensorUI.SizeY * PointedSensor.SensorUI.SizeX / 3000); ;
            GUI.skin.label.fontSize = PointedSensor.SensorUI.LocalFontSize;

            PointedSensor.SensorUI.Changed = UiDetectChanges(PointedSensor.SensorUI);
            //Modificar caso as dimensões sejam alteradas
            if (PointedSensor.SensorUI.Short && PointedSensor.SensorUI.AllRectInfo[0].height != PointedSensor.SensorUI.LocalFontSize * 3 + 2 * Padding)
            {
                PointedSensor.SensorUI.Changed = true;
            }
            if (!PointedSensor.SensorUI.Short && PointedSensor.SensorUI.AllRectInfo[0].height != PointedSensor.SensorUI.LocalFontSize * 5 + 2 * Padding)
            {
                PointedSensor.SensorUI.Changed = true;
            }

            if (PointedSensor.SensorUI.Changed || PointedSensor.SensorUI.FontChanged)
            {
                UiUpdate(PointedSensor.SensorUI);

                if (!PointedSensor.SensorUI.Simplified && !PointedSensor.SensorUI.Short) PointedSensor.SensorUI.AllRectInfo[0] = new Rect(PointedSensor.SensorUI.PosX - Padding, PointedSensor.SensorUI.PosY - Padding, PointedSensor.SensorUI.SizeX + 2 * Padding, PointedSensor.SensorUI.LocalFontSize * 5 + 2 * Padding);
                if (PointedSensor.SensorUI.Simplified && !PointedSensor.SensorUI.Short) PointedSensor.SensorUI.AllRectInfo[0] = new Rect(PointedSensor.SensorUI.PosX - Padding, PointedSensor.SensorUI.PosY - Padding, 3 * PointedSensor.SensorUI.SizeX / 10 + 2 * Padding, PointedSensor.SensorUI.LocalFontSize * 5 + 2 * Padding);
                if (!PointedSensor.SensorUI.Simplified && PointedSensor.SensorUI.Short) PointedSensor.SensorUI.AllRectInfo[0] = new Rect(PointedSensor.SensorUI.PosX - Padding, PointedSensor.SensorUI.PosY - Padding, PointedSensor.SensorUI.LocalFontSize * 3 + 2 * Padding, PointedSensor.SensorUI.LocalFontSize * 3 + 2 * Padding);
                if (PointedSensor.SensorUI.Simplified && PointedSensor.SensorUI.Short) PointedSensor.SensorUI.AllRectInfo[0] = new Rect(PointedSensor.SensorUI.PosX - Padding, PointedSensor.SensorUI.PosY - Padding, PointedSensor.SensorUI.LocalFontSize * 3 + 2 * Padding, PointedSensor.SensorUI.LocalFontSize * 3 + 2 * Padding);

                if (!PointedSensor.SensorUI.Simplified) PointedSensor.SensorUI.AllRectInfo[1] = new Rect(PointedSensor.SensorUI.PosX - Padding, PointedSensor.SensorUI.PosY - Padding, PointedSensor.SensorUI.SizeX + 2 * Padding, 1);
                if (PointedSensor.SensorUI.Simplified) PointedSensor.SensorUI.AllRectInfo[1] = new Rect(PointedSensor.SensorUI.PosX - Padding, PointedSensor.SensorUI.PosY - Padding, 3 * PointedSensor.SensorUI.SizeX / 10 + 2 * Padding, 1);

                if (!PointedSensor.SensorUI.Short) PointedSensor.SensorUI.AllRectInfo[2] = new Rect(PointedSensor.SensorUI.PosX - Padding, PointedSensor.SensorUI.PosY - Padding, PointedSensor.SensorUI.LocalFontSize / 4, PointedSensor.SensorUI.LocalFontSize * 5 + 2 * Padding);
                if (PointedSensor.SensorUI.Short) PointedSensor.SensorUI.AllRectInfo[2] = new Rect(PointedSensor.SensorUI.PosX - Padding, PointedSensor.SensorUI.PosY - Padding, PointedSensor.SensorUI.LocalFontSize / 4, PointedSensor.SensorUI.LocalFontSize * 3 + 2 * Padding);

                PointedSensor.SensorUI.AllRectInfo[3] = new Rect(PointedSensor.SensorUI.PosX + Padding / 2, PointedSensor.SensorUI.PosY, PointedSensor.SensorUI.SizeX, PointedSensor.SensorUI.LocalFontSize * 2);
                PointedSensor.SensorUI.AllRectInfo[15] = new Rect(PointedSensor.SensorUI.PosX + Padding / 2 , PointedSensor.SensorUI.PosY + 3 * PointedSensor.SensorUI.LocalFontSize / 2, PointedSensor.SensorUI.SizeX, PointedSensor.SensorUI.LocalFontSize * 2.5f);
                PointedSensor.SensorUI.AllRectInfo[4] = new Rect(PointedSensor.SensorUI.PosX + Padding / 2, PointedSensor.SensorUI.PosY + 3 * PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.SizeX, PointedSensor.SensorUI.LocalFontSize * 4.2f);
                PointedSensor.SensorUI.AllRectInfo[5] = new Rect(PointedSensor.SensorUI.PosX + PointedSensor.SensorUI.SizeX / 3, PointedSensor.SensorUI.PosY + 3 * PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.LocalFontSize);
                PointedSensor.SensorUI.AllRectInfo[6] = new Rect(PointedSensor.SensorUI.PosX + PointedSensor.SensorUI.SizeX / 3 + 2 * PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.PosY + 3 * PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.SizeX / 3, 2 * PointedSensor.SensorUI.LocalFontSize);
                PointedSensor.SensorUI.AllRectInfo[7] = new Rect(PointedSensor.SensorUI.PosX + 2 * PointedSensor.SensorUI.SizeX / 3, PointedSensor.SensorUI.PosY + 3 * PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.LocalFontSize);
                PointedSensor.SensorUI.AllRectInfo[8] = new Rect(PointedSensor.SensorUI.PosX + 2 * PointedSensor.SensorUI.SizeX / 3 + 2 * PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.PosY + 3 * PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.SizeX / 3, 2 * PointedSensor.SensorUI.LocalFontSize);
                PointedSensor.SensorUI.AllRectInfo[9] = new Rect(PointedSensor.SensorUI.PosX + PointedSensor.SensorUI.SizeX / 3, PointedSensor.SensorUI.PosY + 4 * PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.LocalFontSize);
                PointedSensor.SensorUI.AllRectInfo[10] = new Rect(PointedSensor.SensorUI.PosX + PointedSensor.SensorUI.SizeX / 3 + 2 * PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.PosY + 4 * PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.SizeX / 3, 2 * PointedSensor.SensorUI.LocalFontSize);
                PointedSensor.SensorUI.AllRectInfo[11] = new Rect(PointedSensor.SensorUI.PosX + 2 * PointedSensor.SensorUI.SizeX / 3, PointedSensor.SensorUI.PosY + 4 * PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.LocalFontSize);
                PointedSensor.SensorUI.AllRectInfo[12] = new Rect(PointedSensor.SensorUI.PosX + 2 * PointedSensor.SensorUI.SizeX / 3 + 2 * PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.PosY + 4 * PointedSensor.SensorUI.LocalFontSize, PointedSensor.SensorUI.SizeX / 3, 2 * PointedSensor.SensorUI.LocalFontSize);
                PointedSensor.SensorUI.AllRectInfo[13] = new Rect(PointedSensor.SensorUI.PosX + PointedSensor.SensorUI.SizeX / 3, PointedSensor.SensorUI.PosY, PointedSensor.SensorUI.LocalFontSize * 7, 2 * PointedSensor.SensorUI.LocalFontSize);
                PointedSensor.SensorUI.AllRectInfo[14] = new Rect(PointedSensor.SensorUI.PosX + PointedSensor.SensorUI.SizeX - PointedSensor.SensorUI.LocalFontSize * 7, PointedSensor.SensorUI.PosY, PointedSensor.SensorUI.LocalFontSize * 7, 2 * PointedSensor.SensorUI.LocalFontSize);
                PointedSensor.SensorUI.AllRectInfo[16] = PointedSensor.SensorUI.AllRectInfo[0];
                if (!PointedSensor.SensorUI.Simplified) PointedSensor.SensorUI.AllRectInfo[16].width /= 3;
                //UnityEngine.Debug.Log("UIUpdate");

                PointedSensor.SensorUI.ExpandButton.PosX = PointedSensor.SensorUI.PosX + PointedSensor.SensorUI.SizeX - 2 * PointedSensor.SensorUI.LocalFontSize;
                PointedSensor.SensorUI.ExpandButton.PosY = PointedSensor.SensorUI.PosY + 3 * PointedSensor.SensorUI.LocalFontSize; ;
                PointedSensor.SensorUI.ExpandButton.SizeX = 2 * PointedSensor.SensorUI.LocalFontSize;
                PointedSensor.SensorUI.ExpandButton.SizeY = 2 * PointedSensor.SensorUI.LocalFontSize;
                PointedSensor.SensorUI.ExpandButton.Icon = ExpandIcon;
                PointedSensor.SensorUI.ExpandButton.HardMove = true;

            }

            if (MouseOverRect(PointedSensor.SensorUI.AllRectInfo[0]))
            {
                if (PointedSensor.SensorUI.MouseOver == false) PointedSensor.SensorUI.MouseEnter = true;
                PointedSensor.SensorUI.MouseOver = true;
            }
            else { PointedSensor.SensorUI.MouseOver = false; }
            //relatório quando clicado
            if (MouseOverRect(PointedSensor.SensorUI.AllRectInfo[0]) && Input.GetMouseButtonUp(0) && !PointedSensor.SensorUI.Changed && Time.time - PointedSensor.SensorUI.ExpandButton.ClickTime > PointedSensor.SensorUI.ExpandButton.WaitingTime)
            {
                bool State = PointedSensor.SensorUI.ExpandButton.Toggle;

                if (!State) Speak(ReportSensor(PointedSensor));

                PointedSensor.SensorUI.ExpandButton.ClickTime = Time.time;
                PointedSensor.SensorUI.ExpandButton.MouseClick = true;
                PointedSensor.Focus = !State;
                PointedSensor.SensorUI.ExpandButton.Selected = !State;
                PointedSensor.SensorUI.ExpandButton.Toggle = !State;
                //aqui
            }

            if (PointedSensor.SensorUI.MouseEnter)
            {
                if (PointedSensor.SensorUI.PlaySound) PlaySound(CoreObject, 1, PointedSensor.SensorUI.SoundIntensity);
                PointedSensor.SensorUI.MouseEnter = false;
            }

            float Perc = 0f;
            if (PointedSensor.MaxSecureValue != PointedSensor.MinSecureValue) { Perc = (PointedSensor.ActualValue - PointedSensor.MinSecureValue) / (PointedSensor.MaxSecureValue - PointedSensor.MinSecureValue); }
            if (Perc < 0f || float.IsNaN(Perc)) { Perc = 0f; }
            if (Perc > 1f) { Perc = 1f; }
            int BarSize = Mathf.RoundToInt(Perc * (2f * (float)PointedSensor.SensorUI.SizeX / 3f));
            //UnityEngine.Debug.Log(Perc);

            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperLeft;

            //
            if (!PointedSensor.SensorUI.MouseOver)
                GUI.DrawTexture(PointedSensor.SensorUI.AllRectInfo[0], Gray20TextureAlpha90); //quadro traseiro
            if (PointedSensor.SensorUI.MouseOver)
                GUI.DrawTexture(PointedSensor.SensorUI.AllRectInfo[0], Gray16Texture); //quadro traseiro
            //
            

            //Indicador de Status
            if (PointedSensor.SensorStatus == 1f && ThisProcess.Connected)
            {
                if (PointedSensor.SensorUI.MouseOver) GUI.DrawTexture(PointedSensor.SensorUI.AllRectInfo[2], GreenTexture);
                else GUI.DrawTexture(PointedSensor.SensorUI.AllRectInfo[2], GreenTextureAlpha40);
            }
            if (PointedSensor.SensorStatus == 0.5f || !ThisProcess.Connected)
            {
                if (PointedSensor.SensorUI.MouseOver) GUI.DrawTexture(PointedSensor.SensorUI.AllRectInfo[2], YellowTexture);
                else GUI.DrawTexture(PointedSensor.SensorUI.AllRectInfo[2], YellowTextureAlpha45);
            }
            if (PointedSensor.SensorStatus == 0f && ThisProcess.Connected)
            {
                if (PointedSensor.SensorUI.MouseOver) GUI.DrawTexture(PointedSensor.SensorUI.AllRectInfo[2], RedTexture);
                else GUI.DrawTexture(PointedSensor.SensorUI.AllRectInfo[2], RedTexture);
            }

            //
            if (!PointedSensor.SensorUI.Short) //Nome do sensor
            {
                GUI.skin.label.fontSize = 12 * PointedSensor.SensorUI.LocalFontSize / 10;
                if (!PointedSensor.Virtual) { GUI.Label(PointedSensor.SensorUI.AllRectInfo[3], PointedSensor.Name + " " + PointedSensor.Number); }
                else { GUI.Label(PointedSensor.SensorUI.AllRectInfo[3], PointedSensor.Name + " " + PointedSensor.Number + "*"); }
                GUI.skin.label.fontSize = 9 * PointedSensor.SensorUI.LocalFontSize / 10;
                GUI.Label(PointedSensor.SensorUI.AllRectInfo[15], PointedSensor.Description);
                GUI.skin.label.fontSize = 2 * PointedSensor.SensorUI.LocalFontSize - PointedSensor.SensorUI.SizeY / 100;
                if (ThisProcess.Connected && !PointedSensor.FirstReceive)
                {
                    GUI.Label(PointedSensor.SensorUI.AllRectInfo[4], PointedSensor.ActualValue.ToString("G4") + PointedSensor.Unit);
                }
                else
                {
                    GUI.Label(PointedSensor.SensorUI.AllRectInfo[4], "-- " + PointedSensor.Unit);
                }
            }
            else
            {
                GUI.skin.label.fontSize = 8 * PointedSensor.SensorUI.LocalFontSize / 10 + 2;
                if (!PointedSensor.Virtual) { GUI.Label(PointedSensor.SensorUI.AllRectInfo[3], PointedSensor.Name[0] + " " + PointedSensor.Number); }
                else { GUI.Label(PointedSensor.SensorUI.AllRectInfo[3], PointedSensor.Name[0] + " " + PointedSensor.Number + "*"); }
                GUI.skin.label.fontSize = 8 * PointedSensor.SensorUI.LocalFontSize / 10 - PointedSensor.SensorUI.SizeY / 100 + 2;
                if (ThisProcess.Connected && !PointedSensor.FirstReceive)
                {
                    GUI.Label(PointedSensor.SensorUI.AllRectInfo[15], PointedSensor.ActualValue.ToString("G4") + PointedSensor.Unit);
                }
                else
                {
                    GUI.Label(PointedSensor.SensorUI.AllRectInfo[15], "-- " + PointedSensor.Unit);
                }
            }

            if (!PointedSensor.SensorUI.Short) //Linha divisória
            {
                DrawLineStretched(new Vector2(PointedSensor.SensorUI.PosX + 3 * PointedSensor.SensorUI.SizeX / 10, PointedSensor.SensorUI.PosY + PointedSensor.SensorUI.LocalFontSize / 3), new Vector2(PointedSensor.SensorUI.PosX + 3 * PointedSensor.SensorUI.SizeX / 10, PointedSensor.SensorUI.PosY + 5 * PointedSensor.SensorUI.LocalFontSize), Gray50Texture, PointedSensor.SensorUI.LocalFontSize / 10); //divisor
            }
            else
            {
                //DrawLineStretched(new Vector2(PointedSensor.SensorUI.PosX + 3 * PointedSensor.SensorUI.SizeX / 10, PointedSensor.SensorUI.PosY + PointedSensor.SensorUI.LocalFontSize / 3), new Vector2(PointedSensor.SensorUI.PosX + 3 * PointedSensor.SensorUI.SizeX / 10, PointedSensor.SensorUI.PosY + 3 * PointedSensor.SensorUI.LocalFontSize), Gray50Texture, PointedSensor.SensorUI.LocalFontSize / 10); //divisor
            }

            if (!PointedSensor.SensorUI.Simplified)
            {
                if (!PointedSensor.SensorUI.Short)
                {
                    GUI.DrawTexture(PointedSensor.SensorUI.AllRectInfo[1], GrayTextureAlpha50); //linha superior

                    DrawLineStretched(new Vector2(PointedSensor.SensorUI.PosX + PointedSensor.SensorUI.SizeX / 3, PointedSensor.SensorUI.PosY + 2 * PointedSensor.SensorUI.LocalFontSize), new Vector2(PointedSensor.SensorUI.PosX + PointedSensor.SensorUI.SizeX, PointedSensor.SensorUI.PosY + 2 * PointedSensor.SensorUI.LocalFontSize), GrayTextureAlpha50, 2 * PointedSensor.SensorUI.LocalFontSize / 3);
                    if (BarSize > 0)
                    {
                        DrawLineStretched(new Vector2(PointedSensor.SensorUI.PosX + PointedSensor.SensorUI.SizeX / 3, PointedSensor.SensorUI.PosY + 2 * PointedSensor.SensorUI.LocalFontSize), new Vector2(PointedSensor.SensorUI.PosX + PointedSensor.SensorUI.SizeX / 3 + BarSize, PointedSensor.SensorUI.PosY + 2 * PointedSensor.SensorUI.LocalFontSize), WhiteTexture, 2 * PointedSensor.SensorUI.LocalFontSize / 3);
                    }

                    //Subdivisões da barra de valor
                    if (PointedSensor.SensorUI.BarSubdivisions >= 1)
                    {
                        for (int i = 0; i <= PointedSensor.SensorUI.BarSubdivisions; i++)
                        {
                            rectDraw = new Rect(PointedSensor.SensorUI.PosX + PointedSensor.SensorUI.SizeX / 3 + i * (2 * PointedSensor.SensorUI.SizeX / 3 - PointedSensor.SensorUI.LocalFontSize / 5) / PointedSensor.SensorUI.BarSubdivisions, PointedSensor.SensorUI.PosY + 5 * PointedSensor.SensorUI.LocalFontSize / 3 - PointedSensor.SensorUI.LocalFontSize / 5, PointedSensor.SensorUI.LocalFontSize / 5, PointedSensor.SensorUI.LocalFontSize / 5);
                            GUI.DrawTexture(rectDraw, WhiteTextureAlpha50);
                        }
                    }

                    GUI.skin.label.fontSize = 8 * PointedSensor.SensorUI.LocalFontSize / 10;
                    //
                    GUI.DrawTexture(PointedSensor.SensorUI.AllRectInfo[5], MinimumArrowIcon);
                    if (PointedSensor.MinValue < PointedSensor.MinSecureValue || PointedSensor.MinValue > PointedSensor.MaxSecureValue) { GUI.color = Color.yellow; }
                    //
                    GUI.Label(PointedSensor.SensorUI.AllRectInfo[6], PointedSensor.MinValue.ToString("G5") + PointedSensor.Unit);
                    GUI.color = Color.white;

                    //
                    GUI.DrawTexture(PointedSensor.SensorUI.AllRectInfo[7], MaximumArrowIcon);
                    if (PointedSensor.MaxValue < PointedSensor.MinSecureValue || PointedSensor.MaxValue > PointedSensor.MaxSecureValue) { GUI.color = Color.yellow; }
                    //
                    GUI.Label(PointedSensor.SensorUI.AllRectInfo[8], PointedSensor.MaxValue.ToString("G5") + PointedSensor.Unit);
                    GUI.color = Color.white;

                    //
                    GUI.DrawTexture(PointedSensor.SensorUI.AllRectInfo[9], MeanIcon);
                    if (PointedSensor.MeanValue < PointedSensor.MinSecureValue || PointedSensor.MeanValue > PointedSensor.MaxSecureValue) { GUI.color = Color.red; }
                    //
                    GUI.Label(PointedSensor.SensorUI.AllRectInfo[10], PointedSensor.MeanValue.ToString("G5") + PointedSensor.Unit);
                    GUI.color = Color.white;

                    //
                    if (PointedSensor.Rate > 0)
                        GUI.DrawTexture(PointedSensor.SensorUI.AllRectInfo[11], RateUpIcon);
                    if (PointedSensor.Rate < 0)
                        GUI.DrawTexture(PointedSensor.SensorUI.AllRectInfo[11], RateDownIcon);
                    if (PointedSensor.Rate == 0)
                        GUI.DrawTexture(PointedSensor.SensorUI.AllRectInfo[11], StableIcon);
                    //
                    GUI.Label(PointedSensor.SensorUI.AllRectInfo[12], PointedSensor.Rate.ToString("G5") + PointedSensor.Unit + "/s");

                    if (Button(PointedSensor.SensorUI.ExpandButton)) { UnityEngine.Debug.Log("Sensor expandido"); }

                    GUI.skin.label.fontSize = 9 * PointedSensor.SensorUI.LocalFontSize / 10;
                    centeredStyle.alignment = TextAnchor.UpperLeft;
                    //
                    GUI.Label(PointedSensor.SensorUI.AllRectInfo[13], PointedSensor.MinSecureValue.ToString());
                    centeredStyle.alignment = TextAnchor.UpperRight;
                    //
                    GUI.Label(PointedSensor.SensorUI.AllRectInfo[14], PointedSensor.MaxSecureValue.ToString());
                    centeredStyle.alignment = TextAnchor.UpperLeft;
                }

            }

            //Desenhar linha apontadora
            if ((PointedSensor.SensorUI.MouseOver || PointedSensor.SensorUI.DrawPointer) && !FreeSpaceInUse)
            {
                if (!PointedSensor.SensorUI.Short)//Ponto no HUD
                {
                    PointedSensor.SensorUI.HUDPointer = new Vector2(PointedSensor.SensorUI.PosX - Padding, PointedSensor.SensorUI.PosY + 2 * FontSize);
                }
                else
                {
                    PointedSensor.SensorUI.HUDPointer = new Vector2(PointedSensor.SensorUI.PosX - Padding, PointedSensor.SensorUI.PosY + FontSize);
                }
                if (PointedSensor.Objects.Length > 0 && PointedSensor.Objects[0] != null)//Ponto 3D
                {
                    PointedSensor.SensorUI.ObjectPos2D = new Vector2(Camera.main.WorldToScreenPoint(PointedSensor.Objects[0].transform.position).x, Screen.height - Camera.main.WorldToScreenPoint(PointedSensor.Objects[0].transform.position).y);
                }
                else
                {
                    PointedSensor.SensorUI.ObjectPos2D = new Vector2(Camera.main.WorldToScreenPoint(new Vector3(0f, 0f, 0f)).x, Screen.height - Camera.main.WorldToScreenPoint(new Vector3(0f, 0f, 0f)).y);
                }
                DrawLineStretched(PointedSensor.SensorUI.HUDPointer, PointedSensor.SensorUI.ObjectPos2D, Gray50Texture, Padding / 2);
            }

            GUI.skin.label.fontSize = PointedSensor.SensorUI.LocalFontSize;
        }

        public static void SetCompactSensors(UISensorGroupData UiGroupSource)
        {
            UiGroupSource.ExpandButton.Selected = true;
            UiGroupSource.PosX += 2 * UiGroupSource.SizeX / 3;
            UiGroupSource.Simplified = true;
        }
        public static void SetExpandedSensors(UISensorGroupData UiGroupSource)
        {
            UiGroupSource.ExpandButton.Selected = false;
            UiGroupSource.PosX -= 2 * UiGroupSource.SizeX / 3;
            UiGroupSource.Simplified = false;
        }

        public static void SensorsGUI(UISensorGroupData UiGroupSource, ProcessDetails ThisProcess)
        {
            if (ThisProcess.Sensors.Length > 0)
            {

                if (!UiGroupSource.Short)
                    rectDraw = new Rect(ThisProcess.Sensors[0].SensorUI.PosX - 0.08f * UiGroupSource.SizeX * Screen.width, UiGroupSource.PosY * Screen.height, 1, (ThisProcess.Sensors.Length / 7 - 1) * (FontSize * 6 + 2 * Padding) / 2 + FreeRectSpace.height / 30);
                if (UiGroupSource.Short)
                    rectDraw = new Rect(ThisProcess.Sensors[0].SensorUI.PosX - 0.08f * UiGroupSource.SizeX * Screen.width, UiGroupSource.PosY * Screen.height, 1, (ThisProcess.Sensors.Length/7 - 1) * (FontSize * 3 + 2 * Padding) / 2 + FreeRectSpace.height / 30);
                GUI.DrawTexture(rectDraw, GrayTextureAlpha50);

                if (!UiGroupSource.Short)
                    rectDraw = new Rect(ThisProcess.Sensors[0].SensorUI.PosX - 0.08f * UiGroupSource.SizeX * Screen.width, UiGroupSource.PosY * Screen.height + (ThisProcess.Sensors.Length / 7 - 1) * (FontSize * 6 + 2 * Padding) / 2 + 3.5f * FontSize + FreeRectSpace.height / 30, 1, (ThisProcess.Sensors.Length/7 - 1) * (FontSize * 6 + 2 * Padding) / 2 + FreeRectSpace.height / 30);
                if (UiGroupSource.Short)
                    rectDraw = new Rect(ThisProcess.Sensors[0].SensorUI.PosX - 0.08f * UiGroupSource.SizeX * Screen.width, UiGroupSource.PosY * Screen.height + (ThisProcess.Sensors.Length / 7 - 1) * (FontSize * 3 + 2 * Padding) / 2 + 3.5f * FontSize + FreeRectSpace.height / 30, 1, (ThisProcess.Sensors.Length/7 - 1) * (FontSize * 3 + 2 * Padding) / 2 + FreeRectSpace.height / 30);
                GUI.DrawTexture(rectDraw, GrayTextureAlpha50);

                UiGroupSource.ExpandButton.PosX = Mathf.RoundToInt(ThisProcess.Sensors[0].SensorUI.PosX - 0.08f * UiGroupSource.SizeX * Screen.width - FontSize * 1.25f);
                if (!UiGroupSource.Short)
                    UiGroupSource.ExpandButton.PosY = Mathf.RoundToInt((UiGroupSource.PosY * Screen.height) + (ThisProcess.Sensors.Length/7 - 1) * (FontSize * 6 + 2 * Padding) / 2 + FontSize * 0.5f + FreeRectSpace.height / 30);
                if (UiGroupSource.Short)
                    UiGroupSource.ExpandButton.PosY = Mathf.RoundToInt((UiGroupSource.PosY * Screen.height) + (ThisProcess.Sensors.Length/7 - 1) * (FontSize * 3 + 2 * Padding) / 2 + FontSize * 0.5f + FreeRectSpace.height / 30);
                UiGroupSource.ExpandButton.SizeX = Mathf.RoundToInt(FontSize * 2.5f);
                UiGroupSource.ExpandButton.SizeY = Mathf.RoundToInt(FontSize * 2.5f);

                if (FigureButton(UiGroupSource.ExpandButton))
                {
                    if (!UiGroupSource.ExpandButton.Selected)
                    {
                        SetExpandedSensors(UiGroupSource);
                    }
                    if (UiGroupSource.ExpandButton.Selected)
                    {
                        SetCompactSensors(UiGroupSource);
                    }
                }

                rectDraw = ThisProcess.Sensors[0].SensorUI.AllRectInfo[0];
                rectDraw.y = Mathf.RoundToInt(UiGroupSource.PosY * Screen.height);
                rectDraw.height = FreeRectSpace.height / 30;
                if (!UiGroupSource.Simplified)
                {
                    if (UiGroupSource.Short)
                    {
                        rectDraw.width = ThisProcess.Sensors[0].SensorUI.AllRectInfo[0].width * 7.7f;
                    }
                    else
                    {
                        rectDraw.width = ThisProcess.Sensors[0].SensorUI.AllRectInfo[0].width;
                    }
                }
                if (UiGroupSource.Simplified)
                {
                    rectDraw.width = ThisProcess.Sensors[0].SensorUI.AllRectInfo[0].width * 2.7f;
                }
                //rectDraw = new Rect(Mathf.RoundToInt(UiGroupSource.PosX * Screen.width + 0.08f * UiGroupSource.SizeX * Screen.width), Mathf.RoundToInt(UiGroupSource.PosY * Screen.height), Mathf.RoundToInt(0.92f * UiGroupSource.SizeX * Screen.width), FreeRectSpace.height / 30)
                GUI.DrawTexture(rectDraw, Gray20TextureAlpha90);

                GUI.skin.label.fontSize = GUI.skin.label.fontSize + Padding / 2;
                GUI.Label(new Rect(rectDraw.x + Padding, rectDraw.y + Padding / 4, rectDraw.width, rectDraw.height), "Sensores");
                GUI.skin.label.fontSize = GUI.skin.label.fontSize - Padding / 2;

                rectDraw.y = Mathf.RoundToInt(UiGroupSource.PosY * Screen.height) + FreeRectSpace.height / 30;
                rectDraw.height = FontSize / 5;
                GUI.DrawTexture(rectDraw, WhiteTexture);

                //Desenho dos Sensores
                for (int i = 0; i < ThisProcess.Sensors.Length; i++)
                {
                    if (ThisProcess.Sensors[i].AutoNumber)
                        ThisProcess.Sensors[i].Number = i.ToString("D2");
                    ThisProcess.Sensors[i].SensorUI.Short = UiGroupSource.Short;
                    ThisProcess.Sensors[i].SensorUI.TransitionSpeed = UiGroupSource.TransitionSpeed;
                    ThisProcess.Sensors[i].SensorUI.Simplified = UiGroupSource.Simplified;

                    if (!UiGroupSource.Short)
                    {
                        ThisProcess.Sensors[i].SensorUI.TargetPosY = Mathf.RoundToInt((UiGroupSource.PosY + 1f / 20f) * Screen.height + i * (FontSize * 6 + 3 * Padding / 2));
                        ThisProcess.Sensors[i].SensorUI.TargetPosX = Mathf.RoundToInt(UiGroupSource.PosX * Screen.width + 0.08f * UiGroupSource.SizeX * Screen.width);
                    }
                    if (UiGroupSource.Short)
                    {
                        ThisProcess.Sensors[i].SensorUI.TargetPosY = Mathf.RoundToInt((UiGroupSource.PosY + 1f / 20f) * Screen.height + (float)(Mathf.Floor(i/7)) * (FontSize * 3 + 5 * Padding / 2));
                        ThisProcess.Sensors[i].SensorUI.TargetPosX = Mathf.RoundToInt(UiGroupSource.PosX * Screen.width + 0.08f * UiGroupSource.SizeX * Screen.width + (float)(i % 7) * (FontSize * 3 + 3 * Padding));
                    }
                    ThisProcess.Sensors[i].SensorUI.TargetSizeX = Mathf.RoundToInt(0.92f * UiGroupSource.SizeX * Screen.width);
                    ThisProcess.Sensors[i].SensorUI.TargetSizeY = 0;
                    SensorDetails(ThisProcess.Sensors[i], ThisProcess);
                }
            }
            else
            {
                GUI.Label(new Rect(UiGroupSource.PosX * Screen.width, UiGroupSource.PosY * Screen.height, 100, 20), "Sem sensores");
            }
        }

        //Identifica mudanças
        public static bool UiDetectChanges(UISensorData UiSource)
        {
            bool result = false;
            if (UiSource.PosX != UiSource.TargetPosX ||
                UiSource.PosY != UiSource.TargetPosY ||
                UiSource.SizeX != UiSource.TargetSizeX ||
                UiSource.SizeY != UiSource.TargetSizeY)
            {
                result = true;
            }
            return result;

        }


        //Atualiza os dados de uma UI para animações suaves
        public static void UiUpdate(UISensorData UiSource)
        {

            if (UiSource.TransitionSpeed != -100)
            {
                UiSource.PosX = (100 * UiSource.PosX + UiSource.TransitionSpeed * UiSource.TargetPosX) / (UiSource.TransitionSpeed + 100);
                if (UiSource.PosX - UiSource.TargetPosX > 0) UiSource.PosX = UiSource.PosX - 1;
                if (UiSource.PosX - UiSource.TargetPosX < 0) UiSource.PosX = UiSource.PosX + 1;

                UiSource.PosY = (100 * UiSource.PosY + UiSource.TransitionSpeed * UiSource.TargetPosY) / (UiSource.TransitionSpeed + 100); ;
                if (UiSource.PosY - UiSource.TargetPosY > 0) UiSource.PosY = UiSource.PosY - 1;
                if (UiSource.PosY - UiSource.TargetPosY < 0) UiSource.PosY = UiSource.PosY + 1;

                UiSource.SizeX = (100 * UiSource.SizeX + UiSource.TransitionSpeed * UiSource.TargetSizeX) / (UiSource.TransitionSpeed + 100); ;
                if (UiSource.SizeX - UiSource.TargetSizeX > 0) UiSource.SizeX = UiSource.SizeX - 1;
                if (UiSource.SizeX - UiSource.TargetSizeX < 0) UiSource.SizeX = UiSource.SizeX + 1;

                UiSource.SizeY = (100 * UiSource.SizeY + UiSource.TransitionSpeed * UiSource.TargetSizeY) / (UiSource.TransitionSpeed + 100); ;
                if (UiSource.SizeY - UiSource.TargetSizeY > 0) UiSource.SizeY = UiSource.SizeY - 1;
                if (UiSource.SizeY - UiSource.TargetSizeY < 0) UiSource.SizeY = UiSource.SizeY + 1;
            }

        }

        //Atualiza os dados de um botão para animações suaves
        public static void ButtonUiUpdate(UIButtonData UiSource)
        {

            if (UiSource.TransitionSpeed != -100)
            {
                UiSource.PosX = (100 * UiSource.PosX + UiSource.TransitionSpeed * UiSource.TargetPosX) / (UiSource.TransitionSpeed + 100);
                if (UiSource.PosX - UiSource.TargetPosX > 0) UiSource.PosX = UiSource.PosX - 1;
                if (UiSource.PosX - UiSource.TargetPosX < 0) UiSource.PosX = UiSource.PosX + 1;

                UiSource.PosY = (100 * UiSource.PosY + UiSource.TransitionSpeed * UiSource.TargetPosY) / (UiSource.TransitionSpeed + 100); ;
                if (UiSource.PosY - UiSource.TargetPosY > 0) UiSource.PosY = UiSource.PosY - 1;
                if (UiSource.PosY - UiSource.TargetPosY < 0) UiSource.PosY = UiSource.PosY + 1;

                UiSource.SizeX = (100 * UiSource.SizeX + UiSource.TransitionSpeed * UiSource.TargetSizeX) / (UiSource.TransitionSpeed + 100); ;
                if (UiSource.SizeX - UiSource.TargetSizeX > 0) UiSource.SizeX = UiSource.SizeX - 1;
                if (UiSource.SizeX - UiSource.TargetSizeX < 0) UiSource.SizeX = UiSource.SizeX + 1;

                UiSource.SizeY = (100 * UiSource.SizeY + UiSource.TransitionSpeed * UiSource.TargetSizeY) / (UiSource.TransitionSpeed + 100); ;
                if (UiSource.SizeY - UiSource.TargetSizeY > 0) UiSource.SizeY = UiSource.SizeY - 1;
                if (UiSource.SizeY - UiSource.TargetSizeY < 0) UiSource.SizeY = UiSource.SizeY + 1;
            }

        }

        //Desenha os detalhes de um sensor
        public static void AtuadorDetails(Atuador PointedAtuador, ProcessDetails ThisProcess)
        {
            PointedAtuador.AtuadorUI.FontChanged = false;
            if (PointedAtuador.AtuadorUI.LocalFontSize != FontSize) PointedAtuador.AtuadorUI.FontChanged = true;
            PointedAtuador.AtuadorUI.LocalFontSize = FontSize;// + Mathf.RoundToInt(PointedAtuador.AtuadorUI.SizeY * PointedAtuador.AtuadorUI.SizeX / 3000); ;
            GUI.skin.label.fontSize = PointedAtuador.AtuadorUI.LocalFontSize;

            //Verifica alterações
            PointedAtuador.AtuadorUI.Changed = UiDetectChangesAtuador(PointedAtuador.AtuadorUI);
            //Modificar caso as dimensões sejam alteradas
            if (PointedAtuador.AtuadorUI.Short && PointedAtuador.AtuadorUI.AllRectInfo[0].height != PointedAtuador.AtuadorUI.LocalFontSize * 3 + 2 * Padding)
            {
                PointedAtuador.AtuadorUI.Changed = true;
            }
            if (!PointedAtuador.AtuadorUI.Short && PointedAtuador.AtuadorUI.AllRectInfo[0].height != PointedAtuador.AtuadorUI.LocalFontSize * 5 + 2 * Padding)
            {
                PointedAtuador.AtuadorUI.Changed = true;
            }

            if (PointedAtuador.AtuadorUI.Changed || PointedAtuador.AtuadorUI.FontChanged)
            {

                UiUpdateAtuador(PointedAtuador.AtuadorUI);

                if (!PointedAtuador.AtuadorUI.Simplified && !PointedAtuador.AtuadorUI.Short) PointedAtuador.AtuadorUI.AllRectInfo[0] = new Rect(PointedAtuador.AtuadorUI.PosX - Padding, PointedAtuador.AtuadorUI.PosY - Padding, PointedAtuador.AtuadorUI.SizeX + 2 * Padding, PointedAtuador.AtuadorUI.LocalFontSize * 5 + 2 * Padding);
                if (PointedAtuador.AtuadorUI.Simplified && !PointedAtuador.AtuadorUI.Short) PointedAtuador.AtuadorUI.AllRectInfo[0] = new Rect(PointedAtuador.AtuadorUI.PosX + 7 * PointedAtuador.AtuadorUI.SizeX / 10 - Padding, PointedAtuador.AtuadorUI.PosY - Padding, 3 * PointedAtuador.AtuadorUI.SizeX / 10 + 2 * Padding, PointedAtuador.AtuadorUI.LocalFontSize * 5 + 2 * Padding);
                if (!PointedAtuador.AtuadorUI.Simplified && PointedAtuador.AtuadorUI.Short) PointedAtuador.AtuadorUI.AllRectInfo[0] = new Rect(PointedAtuador.AtuadorUI.PosX + 7 * PointedAtuador.AtuadorUI.SizeX / 10 - Padding, PointedAtuador.AtuadorUI.PosY - Padding, PointedAtuador.AtuadorUI.LocalFontSize * 3 + 2 * Padding, PointedAtuador.AtuadorUI.LocalFontSize * 3 + 2 * Padding);
                if (PointedAtuador.AtuadorUI.Simplified && PointedAtuador.AtuadorUI.Short) PointedAtuador.AtuadorUI.AllRectInfo[0] = new Rect(PointedAtuador.AtuadorUI.PosX + 7 * PointedAtuador.AtuadorUI.SizeX / 10 - Padding, PointedAtuador.AtuadorUI.PosY - Padding, PointedAtuador.AtuadorUI.LocalFontSize * 3 + 2 * Padding, PointedAtuador.AtuadorUI.LocalFontSize * 3 + 2 * Padding);

                if (!PointedAtuador.AtuadorUI.Simplified) PointedAtuador.AtuadorUI.AllRectInfo[1] = new Rect(PointedAtuador.AtuadorUI.PosX - Padding, PointedAtuador.AtuadorUI.PosY - Padding, PointedAtuador.AtuadorUI.SizeX + 2 * Padding, 1);
                if (PointedAtuador.AtuadorUI.Simplified) PointedAtuador.AtuadorUI.AllRectInfo[1] = new Rect(PointedAtuador.AtuadorUI.PosX + 7 * PointedAtuador.AtuadorUI.SizeX / 10 - Padding, PointedAtuador.AtuadorUI.PosY - Padding, 3 * PointedAtuador.AtuadorUI.SizeX / 10 + 2 * Padding, 1);

                if (!PointedAtuador.AtuadorUI.Short) PointedAtuador.AtuadorUI.AllRectInfo[2] = new Rect(PointedAtuador.AtuadorUI.PosX + PointedAtuador.AtuadorUI.SizeX + Padding, PointedAtuador.AtuadorUI.PosY - Padding, PointedAtuador.AtuadorUI.LocalFontSize / 4, PointedAtuador.AtuadorUI.LocalFontSize * 5 + 2 * Padding);
                if (PointedAtuador.AtuadorUI.Short) PointedAtuador.AtuadorUI.AllRectInfo[2] = new Rect(PointedAtuador.AtuadorUI.AllRectInfo[0].position.x + PointedAtuador.AtuadorUI.LocalFontSize * 3 + 2 * Padding, PointedAtuador.AtuadorUI.PosY - Padding, PointedAtuador.AtuadorUI.LocalFontSize / 4, PointedAtuador.AtuadorUI.LocalFontSize * 3 + 2 * Padding);

                PointedAtuador.AtuadorUI.AllRectInfo[3] = new Rect(PointedAtuador.AtuadorUI.PosX + 7 * PointedAtuador.AtuadorUI.SizeX / 10 + 12 * Padding / 10, PointedAtuador.AtuadorUI.PosY, PointedAtuador.AtuadorUI.SizeX, PointedAtuador.AtuadorUI.LocalFontSize * 2);
                PointedAtuador.AtuadorUI.AllRectInfo[4] = new Rect(PointedAtuador.AtuadorUI.PosX + 7 * PointedAtuador.AtuadorUI.SizeX / 10 + 12 * Padding / 10, PointedAtuador.AtuadorUI.PosY + 3 * PointedAtuador.AtuadorUI.LocalFontSize / 2, PointedAtuador.AtuadorUI.SizeX, PointedAtuador.AtuadorUI.LocalFontSize * 2.5f);
                PointedAtuador.AtuadorUI.AllRectInfo[5] = new Rect(PointedAtuador.AtuadorUI.PosX + 7 * PointedAtuador.AtuadorUI.SizeX / 10 + 12 * Padding / 10, PointedAtuador.AtuadorUI.PosY + 3 * PointedAtuador.AtuadorUI.LocalFontSize, PointedAtuador.AtuadorUI.SizeX, PointedAtuador.AtuadorUI.LocalFontSize * 4.2f);
                PointedAtuador.AtuadorUI.AllRectInfo[6] = new Rect(PointedAtuador.AtuadorUI.PosX, PointedAtuador.AtuadorUI.PosY, PointedAtuador.AtuadorUI.LocalFontSize * 7, 2 * PointedAtuador.AtuadorUI.LocalFontSize);
                PointedAtuador.AtuadorUI.AllRectInfo[7] = new Rect(PointedAtuador.AtuadorUI.PosX + 2 * PointedAtuador.AtuadorUI.SizeX / 3 - PointedAtuador.AtuadorUI.LocalFontSize * 7, PointedAtuador.AtuadorUI.PosY, PointedAtuador.AtuadorUI.LocalFontSize * 7, 2 * PointedAtuador.AtuadorUI.LocalFontSize);
                PointedAtuador.AtuadorUI.AllRectInfo[8] = new Rect(PointedAtuador.AtuadorUI.PosX + FontSize / 2, PointedAtuador.AtuadorUI.PosY + 2 * PointedAtuador.AtuadorUI.LocalFontSize, 2 * PointedAtuador.AtuadorUI.SizeX / 3 - FontSize / 5 - FontSize, 2 * PointedAtuador.AtuadorUI.LocalFontSize);
                PointedAtuador.AtuadorUI.AllRectInfo[9] = new Rect(PointedAtuador.AtuadorUI.PosX + 3 * PointedAtuador.AtuadorUI.LocalFontSize / 2, PointedAtuador.AtuadorUI.PosY + 3 * PointedAtuador.AtuadorUI.LocalFontSize, PointedAtuador.AtuadorUI.SizeX, 2.3f * PointedAtuador.AtuadorUI.LocalFontSize);
                PointedAtuador.AtuadorUI.AllRectInfo[10] = new Rect(PointedAtuador.AtuadorUI.PosX + 8f * PointedAtuador.AtuadorUI.LocalFontSize, PointedAtuador.AtuadorUI.PosY + 3 * PointedAtuador.AtuadorUI.LocalFontSize, 5 * PointedAtuador.AtuadorUI.LocalFontSize, 2.3f * PointedAtuador.AtuadorUI.LocalFontSize);
                PointedAtuador.AtuadorUI.AllRectInfo[14] = new Rect(PointedAtuador.AtuadorUI.PosX + 12f * PointedAtuador.AtuadorUI.LocalFontSize, PointedAtuador.AtuadorUI.PosY + 3.3f * PointedAtuador.AtuadorUI.LocalFontSize, 8 * PointedAtuador.AtuadorUI.LocalFontSize / 10, 8 * PointedAtuador.AtuadorUI.LocalFontSize / 10);
                PointedAtuador.AtuadorUI.AllRectInfo[15] = new Rect(PointedAtuador.AtuadorUI.PosX + 13f * PointedAtuador.AtuadorUI.LocalFontSize, PointedAtuador.AtuadorUI.PosY + 3.3f * PointedAtuador.AtuadorUI.LocalFontSize, 8 * PointedAtuador.AtuadorUI.LocalFontSize / 10, 8 * PointedAtuador.AtuadorUI.LocalFontSize / 10);

                PointedAtuador.AtuadorUI.AllRectInfo[11] = new Rect(PointedAtuador.AtuadorUI.PosX, PointedAtuador.AtuadorUI.PosY + 3.1f * PointedAtuador.AtuadorUI.LocalFontSize, PointedAtuador.AtuadorUI.LocalFontSize, PointedAtuador.AtuadorUI.LocalFontSize);
                PointedAtuador.AtuadorUI.AllRectInfo[12] = new Rect(PointedAtuador.AtuadorUI.PosX, PointedAtuador.AtuadorUI.PosY + 4.2f * PointedAtuador.AtuadorUI.LocalFontSize, PointedAtuador.AtuadorUI.LocalFontSize, PointedAtuador.AtuadorUI.LocalFontSize);
                PointedAtuador.AtuadorUI.AllRectInfo[13] = new Rect(PointedAtuador.AtuadorUI.PosX + 3 * PointedAtuador.AtuadorUI.LocalFontSize / 2, PointedAtuador.AtuadorUI.PosY + 4 * PointedAtuador.AtuadorUI.LocalFontSize, 2 * PointedAtuador.AtuadorUI.SizeX / 3, 2 * PointedAtuador.AtuadorUI.LocalFontSize);


                //UnityEngine.Debug.Log("UIUpdate");

                PointedAtuador.AtuadorUI.ExpandButton.PosX = PointedAtuador.AtuadorUI.PosX + 2 * PointedAtuador.AtuadorUI.SizeX / 3 - 2 * PointedAtuador.AtuadorUI.LocalFontSize;
                PointedAtuador.AtuadorUI.ExpandButton.PosY = PointedAtuador.AtuadorUI.PosY + 3 * PointedAtuador.AtuadorUI.LocalFontSize; ;
                PointedAtuador.AtuadorUI.ExpandButton.SizeX = 2 * PointedAtuador.AtuadorUI.LocalFontSize;
                PointedAtuador.AtuadorUI.ExpandButton.SizeY = 2 * PointedAtuador.AtuadorUI.LocalFontSize;
                PointedAtuador.AtuadorUI.ExpandButton.Icon = ExpandIcon;
                PointedAtuador.AtuadorUI.ExpandButton.HardMove = true;

                PointedAtuador.AtuadorUI.IntensitySlider.PosX = Mathf.RoundToInt(PointedAtuador.AtuadorUI.AllRectInfo[8].x);
                PointedAtuador.AtuadorUI.IntensitySlider.PosY = Mathf.RoundToInt(PointedAtuador.AtuadorUI.AllRectInfo[8].y);
                PointedAtuador.AtuadorUI.IntensitySlider.SizeX = Mathf.RoundToInt(PointedAtuador.AtuadorUI.AllRectInfo[8].width);
                PointedAtuador.AtuadorUI.IntensitySlider.SizeY = Mathf.RoundToInt(PointedAtuador.AtuadorUI.AllRectInfo[8].height);
                PointedAtuador.AtuadorUI.IntensitySlider.HardMove = true;
            }

            if (MouseOverRect(PointedAtuador.AtuadorUI.AllRectInfo[0]))
            {
                if (PointedAtuador.AtuadorUI.MouseOver == false) PointedAtuador.AtuadorUI.MouseEnter = true;
                PointedAtuador.AtuadorUI.MouseOver = true;
            }
            else { PointedAtuador.AtuadorUI.MouseOver = false; }

            //relatório quando clicado
            if (MouseOverRect(PointedAtuador.AtuadorUI.AllRectInfo[0]) && Input.GetMouseButtonUp(0) && !PointedAtuador.AtuadorUI.Changed && Time.time - PointedAtuador.AtuadorUI.ExpandButton.ClickTime > PointedAtuador.AtuadorUI.ExpandButton.WaitingTime)
            {
                bool State = PointedAtuador.AtuadorUI.ExpandButton.Toggle;

                //if (!State) Speak(ReportAtuador(PointedAtuador));

                PointedAtuador.AtuadorUI.ExpandButton.ClickTime = Time.time;
                PointedAtuador.AtuadorUI.ExpandButton.MouseClick = true;
                PointedAtuador.Focus = !State;
                PointedAtuador.AtuadorUI.ExpandButton.Selected = !State;
                PointedAtuador.AtuadorUI.ExpandButton.Toggle = !State;
                //aqui
            }

            if (PointedAtuador.AtuadorUI.MouseEnter)
            {
                if (PointedAtuador.AtuadorUI.PlaySound) PlaySound(CoreObject, 1, PointedAtuador.AtuadorUI.SoundIntensity);
                PointedAtuador.AtuadorUI.MouseEnter = false;
            }

            float Perc = 0f;
            if (PointedAtuador.MaxSecureValue != PointedAtuador.MinSecureValue) { Perc = (PointedAtuador.ActualValue - PointedAtuador.MinSecureValue) / (PointedAtuador.MaxSecureValue - PointedAtuador.MinSecureValue); }
            if (Perc < 0f || float.IsNaN(Perc)) { Perc = 0f; }
            if (Perc > 1f) { Perc = 1f; }
            int BarSize = Mathf.RoundToInt(Perc * (2f * (float)PointedAtuador.AtuadorUI.SizeX / 3f));
            //UnityEngine.Debug.Log(Perc);

            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperLeft;

            //
            if (!PointedAtuador.AtuadorUI.MouseOver)
                GUI.DrawTexture(PointedAtuador.AtuadorUI.AllRectInfo[0], Gray20TextureAlpha90); //quadro traseiro
            if (PointedAtuador.AtuadorUI.MouseOver)
                GUI.DrawTexture(PointedAtuador.AtuadorUI.AllRectInfo[0], Gray16Texture); //quadro traseiro


            //Indicador de Status
            if (PointedAtuador.AtuadorStatus == 1f  && ThisProcess.Connected)
            {
                if (PointedAtuador.AtuadorUI.MouseOver) GUI.DrawTexture(PointedAtuador.AtuadorUI.AllRectInfo[2], GreenTexture);
                else GUI.DrawTexture(PointedAtuador.AtuadorUI.AllRectInfo[2], GreenTextureAlpha40);
            }
            if (PointedAtuador.AtuadorStatus == 0.5f || !ThisProcess.Connected)
            {
                if (PointedAtuador.AtuadorUI.MouseOver) GUI.DrawTexture(PointedAtuador.AtuadorUI.AllRectInfo[2], YellowTexture);
                else GUI.DrawTexture(PointedAtuador.AtuadorUI.AllRectInfo[2], YellowTextureAlpha45);
            }
            if (PointedAtuador.AtuadorStatus == 0f && ThisProcess.Connected)
            {
                if (PointedAtuador.AtuadorUI.MouseOver) GUI.DrawTexture(PointedAtuador.AtuadorUI.AllRectInfo[2], RedTexture);
                else GUI.DrawTexture(PointedAtuador.AtuadorUI.AllRectInfo[2], RedTexture);

            }

            if (!PointedAtuador.AtuadorUI.Short)
            {
                //
                GUI.skin.label.fontSize = 12 * PointedAtuador.AtuadorUI.LocalFontSize / 10;
                GUI.Label(PointedAtuador.AtuadorUI.AllRectInfo[3], PointedAtuador.Name + " " + PointedAtuador.Number); //Nome do atuador 
                //                                                      //
                GUI.skin.label.fontSize = 9 * PointedAtuador.AtuadorUI.LocalFontSize / 10;
                GUI.Label(PointedAtuador.AtuadorUI.AllRectInfo[4], PointedAtuador.Description); //descrição do atuador
                GUI.skin.label.fontSize = 2 * PointedAtuador.AtuadorUI.LocalFontSize - PointedAtuador.AtuadorUI.SizeY / 100;
                //
                if (ThisProcess.Connected)
                {
                    GUI.Label(PointedAtuador.AtuadorUI.AllRectInfo[5], PointedAtuador.ActualValue.ToString("G4") + PointedAtuador.Unit);
                }
                else { 
                    GUI.Label(PointedAtuador.AtuadorUI.AllRectInfo[5], "-- " + PointedAtuador.Unit);
                    PointedAtuador.AtuadorUI.IntensitySlider.Enabled = false;
                }
            }
            else
            {
                GUI.skin.label.fontSize = 8 * PointedAtuador.AtuadorUI.LocalFontSize / 10 + 2;
                //
                GUI.Label(PointedAtuador.AtuadorUI.AllRectInfo[3], PointedAtuador.Name[0] + " " + PointedAtuador.Number); //Nome do atuador
                GUI.skin.label.fontSize = 8 * PointedAtuador.AtuadorUI.LocalFontSize / 10 - PointedAtuador.AtuadorUI.SizeY / 100 + 2;
                //
                if (ThisProcess.Connected)
                {
                    GUI.Label(PointedAtuador.AtuadorUI.AllRectInfo[4], PointedAtuador.ActualValue.ToString("G4") + PointedAtuador.Unit); //Valor do atuador e unidade
                }
                else
                {
                    GUI.Label(PointedAtuador.AtuadorUI.AllRectInfo[4], "-- " + PointedAtuador.Unit);
                    PointedAtuador.AtuadorUI.IntensitySlider.Enabled = false;
                }
            }

            if (!PointedAtuador.AtuadorUI.Short)
            {
                DrawLineStretched(new Vector2(PointedAtuador.AtuadorUI.PosX + 7 * PointedAtuador.AtuadorUI.SizeX / 10, PointedAtuador.AtuadorUI.PosY + PointedAtuador.AtuadorUI.LocalFontSize / 3), new Vector2(PointedAtuador.AtuadorUI.PosX + 7 * PointedAtuador.AtuadorUI.SizeX / 10, PointedAtuador.AtuadorUI.PosY + 5 * PointedAtuador.AtuadorUI.LocalFontSize), Gray50Texture, PointedAtuador.AtuadorUI.LocalFontSize / 10); //divisor
            }
            else
            {
                //DrawLineStretched(new Vector2(PointedAtuador.AtuadorUI.PosX + 7 * PointedAtuador.AtuadorUI.SizeX / 10, PointedAtuador.AtuadorUI.PosY + PointedAtuador.AtuadorUI.LocalFontSize / 3), new Vector2(PointedAtuador.AtuadorUI.PosX + 7 * PointedAtuador.AtuadorUI.SizeX / 10, PointedAtuador.AtuadorUI.PosY + 3 * PointedAtuador.AtuadorUI.LocalFontSize), Gray50Texture, PointedAtuador.AtuadorUI.LocalFontSize / 10); //divisor
            }

            if (!PointedAtuador.AtuadorUI.Simplified)
            {
                if (!PointedAtuador.AtuadorUI.Short)
                {
                    //
                    //Slider que define o valor
                    PointedAtuador.ActualValue = SimpleSlider(PointedAtuador.AtuadorUI.IntensitySlider);

                    //Subdivisões da barra de valor
                    if (PointedAtuador.AtuadorUI.BarSubdivisions >= 1)
                    {
                        for (int i = 0; i <= PointedAtuador.AtuadorUI.BarSubdivisions; i++)
                        {
                            rectDraw = new Rect(PointedAtuador.AtuadorUI.PosX + FontSize / 2 + i * (2 * PointedAtuador.AtuadorUI.SizeX / 3 - FontSize / 5 - FontSize) / PointedAtuador.AtuadorUI.BarSubdivisions, PointedAtuador.AtuadorUI.PosY + 4 * PointedAtuador.AtuadorUI.LocalFontSize / 3, PointedAtuador.AtuadorUI.LocalFontSize / 5, PointedAtuador.AtuadorUI.LocalFontSize / 5);
                            GUI.DrawTexture(rectDraw, WhiteTextureAlpha50);
                        }
                    }

                    GUI.skin.label.fontSize = 8 * PointedAtuador.AtuadorUI.LocalFontSize / 10;

                    centeredStyle.alignment = TextAnchor.UpperLeft;
                    //
                    GUI.Label(PointedAtuador.AtuadorUI.AllRectInfo[6], PointedAtuador.MinSecureValue.ToString());
                    centeredStyle.alignment = TextAnchor.UpperRight;
                    //
                    GUI.Label(PointedAtuador.AtuadorUI.AllRectInfo[7], PointedAtuador.MaxSecureValue.ToString());
                    centeredStyle.alignment = TextAnchor.UpperLeft;

                    GUI.skin.label.fontSize = 9 * PointedAtuador.AtuadorUI.LocalFontSize / 10;
                    //GUI.skin.textField.fontSize = PointedAtuador.AtuadorUI.LocalFontSize;

                    //
                    GUI.DrawTexture(PointedAtuador.AtuadorUI.AllRectInfo[1], GrayTextureAlpha50); //linha superior

                    if (Button(PointedAtuador.AtuadorUI.ExpandButton)) { UnityEngine.Debug.Log("OK"); }
                    //
                    if (PointedAtuador.PWM)
                    {
                        GUI.DrawTexture(PointedAtuador.AtuadorUI.AllRectInfo[11], DutyCicle);
                        GUI.Label(PointedAtuador.AtuadorUI.AllRectInfo[9], "Ciclo PWM");
                    }

                    if (PointedAtuador.OnOff)
                    {
                        GUI.DrawTexture(PointedAtuador.AtuadorUI.AllRectInfo[11], DutyCicle);
                        GUI.Label(PointedAtuador.AtuadorUI.AllRectInfo[9], "Ciclo On/Off");

                        if (PointedAtuador.ActualStatus)
                        {
                            GUI.DrawTexture(PointedAtuador.AtuadorUI.AllRectInfo[12], SimpleSphere);
                            GUI.Label(PointedAtuador.AtuadorUI.AllRectInfo[13], "Atualmente ligado");
                        }
                        if (!PointedAtuador.ActualStatus)
                        {
                            GUI.DrawTexture(PointedAtuador.AtuadorUI.AllRectInfo[12], SimpleSphere35);
                            GUI.Label(PointedAtuador.AtuadorUI.AllRectInfo[13], "Atualmente desligado");
                        }

                        GUI.Label(PointedAtuador.AtuadorUI.AllRectInfo[10], PointedAtuador.OnOffCicle.ToString() + "seg");
                        GUI.DrawTexture(PointedAtuador.AtuadorUI.AllRectInfo[14], RateUpIcon);
                        if (ClickOnRect(PointedAtuador.AtuadorUI.AllRectInfo[14])) { PointedAtuador.OnOffCicle += 5f; PointedAtuador.CicleToSend = true; }
                        GUI.DrawTexture(PointedAtuador.AtuadorUI.AllRectInfo[15], RateDownIcon);
                        if (ClickOnRect(PointedAtuador.AtuadorUI.AllRectInfo[15])) {PointedAtuador.OnOffCicle -= 5f; PointedAtuador.CicleToSend = true;}
                    //PointedAtuador.OnOffCicle = float.Parse(GUI.TextField(PointedAtuador.AtuadorUI.AllRectInfo[10], PointedAtuador.OnOffCicle.ToString(), PointedAtuador.AtuadorUI.LocalFontSize));
                    if (PointedAtuador.OnOffCicle < 5f) PointedAtuador.OnOffCicle = 5f;
                    if (PointedAtuador.OnOffCicle > 250f) PointedAtuador.OnOffCicle = 250f;
                    }
                }
            }

            //Desenhar linha apontadora
            if ((PointedAtuador.AtuadorUI.MouseOver || PointedAtuador.AtuadorUI.DrawPointer) && !FreeSpaceInUse)
            {
                if (!PointedAtuador.AtuadorUI.Short)
                {
                    PointedAtuador.AtuadorUI.HUDPointer = new Vector2(PointedAtuador.AtuadorUI.PosX + PointedAtuador.AtuadorUI.SizeX + Padding + PointedAtuador.AtuadorUI.LocalFontSize / 4, PointedAtuador.AtuadorUI.PosY + 2 * FontSize);
                }
                else
                {
                    PointedAtuador.AtuadorUI.HUDPointer = new Vector2(PointedAtuador.AtuadorUI.AllRectInfo[0].position.x + PointedAtuador.AtuadorUI.LocalFontSize * 3 + 2*Padding + PointedAtuador.AtuadorUI.LocalFontSize / 4, PointedAtuador.AtuadorUI.PosY + FontSize);
                }

                if (PointedAtuador.Objects.Length > 0 && PointedAtuador.Objects[0] != null)
                {
                    PointedAtuador.AtuadorUI.ObjectPos2D = new Vector2(Camera.main.WorldToScreenPoint(PointedAtuador.Objects[0].transform.position).x, Screen.height - Camera.main.WorldToScreenPoint(PointedAtuador.Objects[0].transform.position).y);
                }
                else
                {
                    PointedAtuador.AtuadorUI.ObjectPos2D = new Vector2(Camera.main.WorldToScreenPoint(new Vector3(0f, 0f, 0f)).x, Screen.height - Camera.main.WorldToScreenPoint(new Vector3(0f, 0f, 0f)).y);
                }
                DrawLineStretched(PointedAtuador.AtuadorUI.HUDPointer, PointedAtuador.AtuadorUI.ObjectPos2D, Gray50Texture, Padding / 2);
            }
            //UnityEngine.Debug.Log("Pad: "+(Padding / 2).ToString());

            GUI.skin.label.fontSize = PointedAtuador.AtuadorUI.LocalFontSize;
        }
        //Retorna se um click foi efetuado sobre um rect
        public static bool ClickOnRect(Rect RectTest)
        {
            bool Result = false;
            if (MouseOverRect(RectTest))
            {
                if (Input.GetMouseButtonDown(0))
                    Result = true;
            }
            return Result;
        }
        //Retorna se o mouse está sobre um rect
        public static bool MouseOverRect(Rect RectTest)
        {
            bool Result = false;
            mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            if (mousePosition.x >= RectTest.x && mousePosition.x <= (RectTest.x + RectTest.width) && mousePosition.y >= (RectTest.y) && mousePosition.y <= (RectTest.y + RectTest.height))
            {
                Result = true;
            }
            return Result;
        }

        public static void AtuadorsGUI(UIAtuadorGroupData UiGroupSource, ProcessDetails ThisProcess)
        {

            if (ThisProcess.Atuators.Length > 0)
            {
                if (!UiGroupSource.Short)
                    rectDraw = new Rect(ThisProcess.Atuators[0].AtuadorUI.PosX + UiGroupSource.SizeX * Screen.width, UiGroupSource.PosY * Screen.height, 1, (ThisProcess.Atuators.Length/2 - 1) * (FontSize * 6 + 2 * Padding) / 2 + FreeRectSpace.height / 30);
                if (UiGroupSource.Short)
                    rectDraw = new Rect(ThisProcess.Atuators[0].AtuadorUI.PosX + UiGroupSource.SizeX * Screen.width, UiGroupSource.PosY * Screen.height, 1, (ThisProcess.Atuators.Length/2 - 1) * (FontSize * 3 + 2 * Padding) / 2 + FreeRectSpace.height / 30);

                GUI.DrawTexture(rectDraw, GrayTextureAlpha50);

                if (!UiGroupSource.Short)
                    rectDraw = new Rect(ThisProcess.Atuators[0].AtuadorUI.PosX + UiGroupSource.SizeX * Screen.width, UiGroupSource.PosY * Screen.height + (ThisProcess.Atuators.Length/2 - 1) * (FontSize * 6 + 2 * Padding) / 2 + 3.5f * FontSize + FreeRectSpace.height / 30, 1, (ThisProcess.Atuators.Length / 2 - 1) * (FontSize * 6 + 2 * Padding) / 2 + FreeRectSpace.height / 30);
                if (UiGroupSource.Short)
                    rectDraw = new Rect(ThisProcess.Atuators[0].AtuadorUI.PosX + UiGroupSource.SizeX * Screen.width, UiGroupSource.PosY * Screen.height + (ThisProcess.Atuators.Length/2 - 1) * (FontSize * 3 + 2 * Padding) / 2 + 3.5f * FontSize + FreeRectSpace.height / 30, 1, (ThisProcess.Atuators.Length / 2 - 1) * (FontSize * 3 + 2 * Padding) / 2 + FreeRectSpace.height / 30);
                GUI.DrawTexture(rectDraw, GrayTextureAlpha50);

                UiGroupSource.ExpandButton.PosX = Mathf.RoundToInt(ThisProcess.Atuators[0].AtuadorUI.PosX + UiGroupSource.SizeX * Screen.width - FontSize * 1.25f);
                if (!UiGroupSource.Short)
                    UiGroupSource.ExpandButton.PosY = Mathf.RoundToInt((UiGroupSource.PosY * Screen.height) + (ThisProcess.Atuators.Length/2 - 1) * (FontSize * 6 + 2 * Padding) / 2 + FontSize * 0.5f + FreeRectSpace.height / 30);
                if (UiGroupSource.Short)
                    UiGroupSource.ExpandButton.PosY = Mathf.RoundToInt((UiGroupSource.PosY * Screen.height) + (ThisProcess.Atuators.Length/2 - 1) * (FontSize * 3 + 2 * Padding) / 2 + FontSize * 0.5f + FreeRectSpace.height / 30);
                UiGroupSource.ExpandButton.SizeX = Mathf.RoundToInt(FontSize * 2.5f);
                UiGroupSource.ExpandButton.SizeY = Mathf.RoundToInt(FontSize * 2.5f);

                if (FigureButton(UiGroupSource.ExpandButton))
                {
                    if (!UiGroupSource.ExpandButton.Selected)
                    {
                        SetExpandedAtuadores(UiGroupSource);
                    }
                    if (UiGroupSource.ExpandButton.Selected)
                    {
                        SetCompactAtuadores(UiGroupSource);
                    }
                }

                rectDraw = ThisProcess.Atuators[0].AtuadorUI.AllRectInfo[0];
                rectDraw.y = Mathf.RoundToInt(UiGroupSource.PosY * Screen.height);
                rectDraw.height = FreeRectSpace.height / 30;
                if (!UiGroupSource.Simplified)
                {
                    if (UiGroupSource.Short)
                    {
                        rectDraw.width = ThisProcess.Atuators[0].AtuadorUI.AllRectInfo[0].width * 7.7f;
                        rectDraw.x = rectDraw.x - ThisProcess.Atuators[0].AtuadorUI.AllRectInfo[0].width * 5f;
                    }
                    else
                    {
                        rectDraw.width = ThisProcess.Atuators[0].AtuadorUI.AllRectInfo[0].width;
                    }
                }
                if (UiGroupSource.Simplified)
                {
                    if (UiGroupSource.Short)
                    {
                        rectDraw.width = ThisProcess.Atuators[0].AtuadorUI.AllRectInfo[0].width * 2.7f;
                    }
                    else
                    {
                        rectDraw.width = ThisProcess.Atuators[0].AtuadorUI.AllRectInfo[0].width;
                    }
                }
                GUI.DrawTexture(rectDraw, Gray20TextureAlpha90);

                GUI.skin.label.fontSize = GUI.skin.label.fontSize + Padding / 2;
                GUI.Label(new Rect(rectDraw.x + Padding, rectDraw.y + Padding / 4, rectDraw.width, rectDraw.height), "Atuadores");
                GUI.skin.label.fontSize = GUI.skin.label.fontSize - Padding / 2;

                rectDraw.y = Mathf.RoundToInt(UiGroupSource.PosY * Screen.height) + FreeRectSpace.height / 30;
                rectDraw.height = FontSize / 5;
                GUI.DrawTexture(rectDraw, WhiteTexture);

                //Desenho dos atuadores
                for (int i = 0; i < ThisProcess.Atuators.Length; i++)
                {
                    if (ThisProcess.Atuators[i].AutoNumber)
                        ThisProcess.Atuators[i].Number = i.ToString("D2");
                    if (ThisProcess.Connected && !ThisProcess.Atuators[i].PIDControled) ThisProcess.Atuators[i].AtuadorUI.IntensitySlider.Enabled = !ThisProcess.Atuators[i].AtuadorUI.Changed; //Desabilita o slider quando está se movendo
                    if (ThisProcess.Atuators[i].PIDControled) ThisProcess.Atuators[i].AtuadorUI.IntensitySlider.Enabled = false; //Desabilita o slider quando o atuador é controlado pelo PID
                    ThisProcess.Atuators[i].AtuadorUI.Short = UiGroupSource.Short;
                    ThisProcess.Atuators[i].AtuadorUI.TransitionSpeed = UiGroupSource.TransitionSpeed;
                    ThisProcess.Atuators[i].AtuadorUI.Simplified = UiGroupSource.Simplified;
                   
                    if (!UiGroupSource.Short)
                    {
                        ThisProcess.Atuators[i].AtuadorUI.TargetPosY = Mathf.RoundToInt((UiGroupSource.PosY + 1f / 20f) * Screen.height + i * (FontSize * 6 + 3 * Padding / 2));
                        ThisProcess.Atuators[i].AtuadorUI.TargetPosX = Mathf.RoundToInt(UiGroupSource.PosX * Screen.width + 0.08f * UiGroupSource.SizeX * Screen.width);
                    }
                    if (UiGroupSource.Short)
                    {
                        ThisProcess.Atuators[i].AtuadorUI.TargetPosY = Mathf.RoundToInt((UiGroupSource.PosY + 1f / 20f) * Screen.height + (float)(Mathf.Floor(i / 2)) * (FontSize * 3 + 5 * Padding / 2));
                        ThisProcess.Atuators[i].AtuadorUI.TargetPosX = Mathf.RoundToInt(UiGroupSource.PosX * Screen.width + 0.08f * UiGroupSource.SizeX * Screen.width + (float)(i % 2) * (FontSize * 3 + 3 * Padding));
                    }
                    ThisProcess.Atuators[i].AtuadorUI.TargetSizeX = Mathf.RoundToInt(0.92f * UiGroupSource.SizeX * Screen.width);
                    ThisProcess.Atuators[i].AtuadorUI.TargetSizeY = 0;
                    AtuadorDetails(ThisProcess.Atuators[i], ThisProcess);
                }
            }
            else
            {
                GUI.Label(new Rect(UiGroupSource.PosX * Screen.width, UiGroupSource.PosY * Screen.height, 100, 20), "Sem atuadores");
            }
        }

        public static void MoveAtuadores(UIAtuadorGroupData UiGroupSource, float OffsetValue)
        {
            UiGroupSource.PosX += OffsetValue;
        }
        public static void SetCompactAtuadores(UIAtuadorGroupData UiGroupSource)
        {
            UiGroupSource.ExpandButton.Selected = true;
            UiGroupSource.PosX -= 2 * UiGroupSource.SizeX / 3;
            UiGroupSource.Simplified = true;
        }
        public static void SetExpandedAtuadores(UIAtuadorGroupData UiGroupSource)
        {
            UiGroupSource.ExpandButton.Selected = false;
            UiGroupSource.PosX += 2 * UiGroupSource.SizeX / 3;
            UiGroupSource.Simplified = false;
        }

        //Identifica mudanças
        public static bool UiDetectChangesAtuador(UIAtuadorData UiSource)
        {
            bool result = false;
            if (UiSource.PosX != UiSource.TargetPosX ||
                UiSource.PosY != UiSource.TargetPosY ||
                UiSource.SizeX != UiSource.TargetSizeX ||
                UiSource.SizeY != UiSource.TargetSizeY)
            {
                result = true;
            }
            return result;

        }

        //Identifica mudanças em um grafico
        public static bool UiDetectChanges(ChartData Chart)
        {
            bool result = false;
            if (Chart.ChartRect.x != Chart.TargetChartRect.x ||
                Chart.ChartRect.y != Chart.TargetChartRect.y ||
                Chart.ChartRect.width != Chart.TargetChartRect.width ||
                Chart.ChartRect.height != Chart.TargetChartRect.height)
            {
                result = true;
            }
            return result;

        }

        //Atualiza os dados de uma UI para animações suaves
        public static void UiUpdate(ChartData Chart, bool rigid)
        {

            if (Chart.TransitionSpeed != -100 && !rigid)
            {
                Chart.ChartRect.x = (100 * Chart.ChartRect.x + Chart.TransitionSpeed * Chart.TargetChartRect.x) / (Chart.TransitionSpeed + 100);
                if (Chart.ChartRect.x - Chart.TargetChartRect.x > 0) Chart.ChartRect.x = Chart.ChartRect.x - 1;
                if (Chart.ChartRect.x - Chart.TargetChartRect.x < 0) Chart.ChartRect.x = Chart.ChartRect.x + 1;

                Chart.ChartRect.y = (100 * Chart.ChartRect.y + Chart.TransitionSpeed * Chart.TargetChartRect.y) / (Chart.TransitionSpeed + 100);
                if (Chart.ChartRect.y - Chart.TargetChartRect.y > 0) Chart.ChartRect.y = Chart.ChartRect.y - 1;
                if (Chart.ChartRect.y - Chart.TargetChartRect.y < 0) Chart.ChartRect.y = Chart.ChartRect.y + 1;

                Chart.ChartRect.width = (100 * Chart.ChartRect.width + Chart.TransitionSpeed * Chart.TargetChartRect.width) / (Chart.TransitionSpeed + 100);
                if (Chart.ChartRect.width - Chart.TargetChartRect.width > 0) Chart.ChartRect.width = Chart.ChartRect.width - 1;
                if (Chart.ChartRect.width - Chart.TargetChartRect.width < 0) Chart.ChartRect.width = Chart.ChartRect.width + 1;

                Chart.ChartRect.height = (100 * Chart.ChartRect.height + Chart.TransitionSpeed * Chart.TargetChartRect.height) / (Chart.TransitionSpeed + 100);
                if (Chart.ChartRect.height - Chart.TargetChartRect.height > 0) Chart.ChartRect.height = Chart.ChartRect.height - 1;
                if (Chart.ChartRect.height - Chart.TargetChartRect.height < 0) Chart.ChartRect.height = Chart.ChartRect.height + 1;
            }
            else
            {
                Chart.ChartRect.x = Chart.TargetChartRect.x;
                Chart.ChartRect.y = Chart.TargetChartRect.y;
                Chart.ChartRect.width = Chart.TargetChartRect.width;
                Chart.ChartRect.height = Chart.TargetChartRect.height;
            }

        }

        //Atualiza os dados de uma UI para animações suaves
        public static void UiUpdateAtuador(UIAtuadorData UiSource)
        {

            if (UiSource.TransitionSpeed != -100)
            {
                UiSource.PosX = (100 * UiSource.PosX + UiSource.TransitionSpeed * UiSource.TargetPosX) / (UiSource.TransitionSpeed + 100);
                if (UiSource.PosX - UiSource.TargetPosX > 0) UiSource.PosX = UiSource.PosX - 1;
                if (UiSource.PosX - UiSource.TargetPosX < 0) UiSource.PosX = UiSource.PosX + 1;

                UiSource.PosY = (100 * UiSource.PosY + UiSource.TransitionSpeed * UiSource.TargetPosY) / (UiSource.TransitionSpeed + 100); ;
                if (UiSource.PosY - UiSource.TargetPosY > 0) UiSource.PosY = UiSource.PosY - 1;
                if (UiSource.PosY - UiSource.TargetPosY < 0) UiSource.PosY = UiSource.PosY + 1;

                UiSource.SizeX = (100 * UiSource.SizeX + UiSource.TransitionSpeed * UiSource.TargetSizeX) / (UiSource.TransitionSpeed + 100); ;
                if (UiSource.SizeX - UiSource.TargetSizeX > 0) UiSource.SizeX = UiSource.SizeX - 1;
                if (UiSource.SizeX - UiSource.TargetSizeX < 0) UiSource.SizeX = UiSource.SizeX + 1;

                UiSource.SizeY = (100 * UiSource.SizeY + UiSource.TransitionSpeed * UiSource.TargetSizeY) / (UiSource.TransitionSpeed + 100); ;
                if (UiSource.SizeY - UiSource.TargetSizeY > 0) UiSource.SizeY = UiSource.SizeY - 1;
                if (UiSource.SizeY - UiSource.TargetSizeY < 0) UiSource.SizeY = UiSource.SizeY + 1;
            }

        }

        //Verifica o Status do sensor
        public static float Status(Atuador PointedAtuador)
        {
            //Detalhar e tornar mais geral, usar uma classe ou subclasse "Security" para manter essas informações públicas
            float output = 1f; //1 OK, 0 não OK
            float RiskMinimum = PointedAtuador.MinSecureValue + (PointedAtuador.SecurityMargin / 100f) * (PointedAtuador.MaxSecureValue - PointedAtuador.MinSecureValue);
            float RiskMaximum = PointedAtuador.MaxSecureValue - (PointedAtuador.SecurityMargin / 100f) * (PointedAtuador.MaxSecureValue - PointedAtuador.MinSecureValue);
            //if (PointedAtuador.MinValue <= PointedAtuador.MinSecureValue) { output = 0.5f; }
            //if (PointedAtuador.MaxValue >= PointedAtuador.MaxSecureValue) { output = 0.5f; }
            if (PointedAtuador.ActualValue < RiskMinimum || PointedAtuador.ActualValue > RiskMaximum) { output = 0.5f; }
            return output;
        }
        //Desenha os graficos dos sensores e atuadores, apenas o selecionado
        public static void MenageFreeSpace(UIButtonData[] ButtonList, ProcessDetails ThisProcess)
        {
            //Menu
            for (int i = 0; i < ButtonList.Length; i++)
            {
                if (ButtonList[i].MouseClick)
                {
                    for (int j = 0; j < ButtonList.Length; j++) //desabilita todos os outros
                    {
                        if (j != i && ButtonList[j].Selectable)
                        {
                            ButtonList[j].Selected = false;
                            ButtonList[j].Toggle = false;
                        }
                    }
                    for (int j = 0; j < ThisProcess.Sensors.Length; j++) //desabilita os sensores
                    {
                        ThisProcess.Sensors[j].Focus = false;
                        ThisProcess.Sensors[j].SensorUI.ExpandButton.Toggle = false;
                    }
                    for (int j = 0; j < ThisProcess.Atuators.Length; j++) //assim como desabilita os atuadores
                    {
                        ThisProcess.Atuators[j].Focus = false;
                        ThisProcess.Atuators[j].AtuadorUI.ExpandButton.Toggle = false;
                    }
                    if (i != 0 && i != 1) //Atuadores e sensores de fora
                    {
                        FreeSpaceInUse = ButtonList[i].Toggle; //Define o uso do espaço central
                    }
                    else
                    {
                        FreeSpaceInUse = false;
                    }
                }
            }
            //Sensores
            for (int i = 0; i < ThisProcess.Sensors.Length; i++)
            {
                if (ThisProcess.Sensors[i].SensorUI.ExpandButton.MouseClick) //verifica se algum sensor foi clicado
                {
                    UnityEngine.Debug.Log("MouseCLick" + i.ToString());
                    for (int j = 0; j < ThisProcess.Sensors.Length; j++) //desabilita todos os outros
                    {
                        if (j != i)
                        {
                            ThisProcess.Sensors[j].Focus = false;
                            ThisProcess.Sensors[j].SensorUI.ExpandButton.Toggle = false;
                            ThisProcess.Sensors[j].SensorUI.ExpandButton.MouseClick = false;
                        }
                    }
                    for (int j = 0; j < ButtonList.Length; j++) //desabilita o que vier do menu
                    {
                        if (ButtonList[j].Selectable)
                        {
                            ButtonList[j].Selected = false;
                            ButtonList[j].Toggle = false;
                        }
                    }
                    for (int j = 0; j < ThisProcess.Atuators.Length; j++) //assim como desabilita os atuadores
                    {
                        ThisProcess.Atuators[j].Focus = false;
                        ThisProcess.Atuators[j].AtuadorUI.ExpandButton.Toggle = false;
                        ThisProcess.Atuators[j].AtuadorUI.ExpandButton.MouseClick = false;
                    }
                    FreeSpaceInUse = ThisProcess.Sensors[i].SensorUI.ExpandButton.Toggle;  //diz que o espaço livre esta em uso
                    ThisProcess.Sensors[i].HistoryLineInFocus = 0; //seta a linha padrão de 10 minutos
                    ThisProcess.Sensors[i].Focus = ThisProcess.Sensors[i].SensorUI.ExpandButton.Toggle; //atrela o foco ao toggle do botão
                    UpdateCharts(ThisProcess.Sensors, ThisProcess.Atuators);
                    ThisProcess.Sensors[i].SensorUI.ExpandButton.MouseClick = false;
                    UnityEngine.Debug.Log("MouseCLick to false");
                }
                //Os graficos e as imagens devem ser atualizados também se houver alteração no combobox de escolha da linha do tempo (10 min, 1 hora, ...)
                if (ThisProcess.Sensors[i].ComboBoxHistory.MouseClick)
                {
                    UpdateCharts(ThisProcess.Sensors, ThisProcess.Atuators);
                }
                if (ThisProcess.Sensors[i].Focus) //se estiver em foco
                {
                    SensorHistoryDetails(ThisProcess.Sensors[i]);  //desenhe o gráfico
                }

            }
            //Atuadores
            for (int i = 0; i < ThisProcess.Atuators.Length; i++)
            {
                if (ThisProcess.Atuators[i].AtuadorUI.ExpandButton.MouseClick) //verifica se algum atuador foi clicado
                {
                    for (int j = 0; j < ThisProcess.Atuators.Length; j++)  //desabilita todos os outros
                    {
                        if (j != i)
                        {
                            ThisProcess.Atuators[j].Focus = false;
                            ThisProcess.Atuators[j].AtuadorUI.ExpandButton.Toggle = false;
                        }
                    }
                    for (int j = 0; j < ButtonList.Length; j++) //desabilita o que vier do menu
                    {
                        if (ButtonList[j].Selectable)
                        {
                            ButtonList[j].Selected = false;
                            ButtonList[j].Toggle = false;
                        }
                    }
                    for (int j = 0; j < ThisProcess.Sensors.Length; j++)  //assim como desabilita os sensores
                    {
                        ThisProcess.Sensors[j].Focus = false;
                        ThisProcess.Sensors[j].SensorUI.ExpandButton.Toggle = false;
                        ThisProcess.Sensors[j].SensorUI.ExpandButton.MouseClick = false;
                    }
                    FreeSpaceInUse = ThisProcess.Atuators[i].AtuadorUI.ExpandButton.Toggle;  //diz que o espaço livre esta em uso
                    ThisProcess.Atuators[i].HistoryLineInFocus = 0;  //seta a linha padrão de 10 minutos
                    ThisProcess.Atuators[i].Focus = ThisProcess.Atuators[i].AtuadorUI.ExpandButton.Toggle;  //atrela o foco ao toggle do botão
                    UpdateCharts(ThisProcess.Sensors, ThisProcess.Atuators);
                    ThisProcess.Atuators[i].AtuadorUI.ExpandButton.MouseClick = false;
                }
                //Os graficos e as imagens devem ser atualizados também se houver alteração no combobox de escolha da linha do tempo (10 min, 1 hora, ...)
                if (ThisProcess.Atuators[i].ComboBoxHistory.MouseClick)
                {
                    UpdateCharts(ThisProcess.Sensors, ThisProcess.Atuators);
                }
                if (ThisProcess.Atuators[i].Focus)
                {
                    ActuatorHistoryDetails(ThisProcess.Atuators[i]);    //desenhe o gráfico
                }

            }
        }

        //Esboça gráfico
        public static void PlotChart(ChartData Chart, float[] XValues, float[] YValues)
        {
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.MiddleCenter;

            if (UiDetectChanges(Chart))
            {
                UiUpdate(Chart, Chart.rigid);
                Chart.TitleRect = new Rect(Chart.ChartRect.x, Chart.ChartRect.y + Chart.ChartRect.height * Chart.LeftOverFraction / 2 - FontSize, Chart.ChartRect.width, 2 * FontSize);
                Chart.XLabelRect = new Rect(Chart.ChartRect.x, Chart.ChartRect.y + Chart.ChartRect.height - Chart.ChartRect.height * Chart.LeftOverFraction / 2 - FontSize, Chart.ChartRect.width, 2 * FontSize);
                Chart.YLabelRect = new Rect(Chart.ChartRect.x + Chart.ChartRect.width * Chart.LeftOverFraction / 2 - Chart.ChartRect.height / 2 - FontSize, Chart.ChartRect.y + Chart.ChartRect.height / 2 - FontSize, Chart.ChartRect.height, 2 * FontSize);
                Chart.pivotPoint = new Vector2(Chart.ChartRect.x + Chart.ChartRect.width * Chart.LeftOverFraction / 2 - FontSize, Chart.ChartRect.y + Chart.ChartRect.height / 2 - FontSize);
                Chart.AllRectInfo[4] = new Rect(Chart.ChartRect.x + Chart.ChartRect.width * Chart.LeftOverFraction + FontSize, Chart.ChartRect.y + Chart.ChartRect.height * Chart.LeftOverFraction, Chart.ChartRect.width - 2 * Chart.ChartRect.width * Chart.LeftOverFraction, Padding / 6);
                Chart.AllRectInfo[5] = new Rect(Chart.ChartRect.x + Chart.ChartRect.width * Chart.LeftOverFraction + FontSize, Chart.ChartRect.y + Chart.ChartRect.height - Chart.ChartRect.height * Chart.LeftOverFraction - FontSize, Chart.ChartRect.width - 2 * Chart.ChartRect.width * Chart.LeftOverFraction, Padding / 6);
                Chart.AllRectInfo[6] = new Rect(Chart.ChartRect.x + Chart.ChartRect.width * Chart.LeftOverFraction + FontSize, Chart.ChartRect.y + Chart.ChartRect.height * Chart.LeftOverFraction, Padding / 6, Chart.ChartRect.height - 2 * Chart.ChartRect.height * Chart.LeftOverFraction - FontSize);
                Chart.AllRectInfo[7] = new Rect(Chart.ChartRect.x + Chart.ChartRect.width - Chart.ChartRect.width * Chart.LeftOverFraction + FontSize, Chart.ChartRect.y + Chart.ChartRect.height * Chart.LeftOverFraction, Padding / 6, Chart.ChartRect.height - 2 * Chart.ChartRect.height * Chart.LeftOverFraction - FontSize);
                Chart.AllRectInfo[8] = new Rect(Chart.ChartRect.x + Chart.ChartRect.width * Chart.LeftOverFraction + FontSize, Chart.ChartRect.y + Chart.ChartRect.height * Chart.LeftOverFraction + (Chart.AllRectInfo[6].height) / 2, Chart.ChartRect.width - 2 * Chart.ChartRect.width * Chart.LeftOverFraction, Padding / 4);
                Chart.AllRectInfo[9] = new Rect(Chart.ChartRect.x + Chart.ChartRect.width * Chart.LeftOverFraction + (Chart.AllRectInfo[4].width) /2 + FontSize, Chart.ChartRect.y + Chart.ChartRect.height * Chart.LeftOverFraction, Padding / 4, Chart.ChartRect.height - 2 * Chart.ChartRect.height * Chart.LeftOverFraction - FontSize);


                Chart.AllRectInfo[10] = new Rect(Chart.ChartRect.x + Chart.ChartRect.width * Chart.LeftOverFraction - 2 * FontSize, Chart.ChartRect.y + Chart.ChartRect.height * Chart.LeftOverFraction - FontSize, 4 * FontSize, 2 * FontSize);
                Chart.AllRectInfo[11] = new Rect(Chart.ChartRect.x + Chart.ChartRect.width * Chart.LeftOverFraction - 2 * FontSize, Chart.ChartRect.y + Chart.ChartRect.height - Chart.ChartRect.height * Chart.LeftOverFraction - 2 * FontSize, 4 * FontSize, 2 * FontSize);
                Chart.AllRectInfo[12] = new Rect(Chart.ChartRect.x + Chart.ChartRect.width * Chart.LeftOverFraction - FontSize, Chart.ChartRect.y + Chart.ChartRect.height - Chart.ChartRect.height * Chart.LeftOverFraction - FontSize, 4 * FontSize, 2 * FontSize);
                Chart.AllRectInfo[13] = new Rect(Chart.ChartRect.x + Chart.ChartRect.width - Chart.ChartRect.width * Chart.LeftOverFraction - FontSize, Chart.ChartRect.y + Chart.ChartRect.height - Chart.ChartRect.height * Chart.LeftOverFraction - FontSize, 4 * FontSize, 2 * FontSize);

                Chart.barWidth1 = Chart.AllRectInfo[4].width / XValues.Length;
                Chart.barHeight = Chart.AllRectInfo[6].height;
            }
            GUI.DrawTexture(Chart.ChartRect, WhiteTexture);
            GUI.color = Color.black;
            GUI.Label(Chart.TitleRect, Chart.Title);

            GUIUtility.RotateAroundPivot(-90f, Chart.pivotPoint);
            GUI.Label(Chart.YLabelRect, Chart.YLabel);
            GUI.matrix = Matrix4x4.identity;

            GUI.Label(Chart.XLabelRect, Chart.XLabel);
            //linhas
            GUI.DrawTexture(Chart.AllRectInfo[4], BlackTexture); //superior
            GUI.DrawTexture(Chart.AllRectInfo[5], BlackTexture); //inferior
            GUI.DrawTexture(Chart.AllRectInfo[6], BlackTexture); //lateral esquerda
            GUI.DrawTexture(Chart.AllRectInfo[7], BlackTexture); //lateral direita
            GUI.DrawTexture(Chart.AllRectInfo[8], BlackTextureAlpha20); //horizontal central
            GUI.DrawTexture(Chart.AllRectInfo[9], BlackTextureAlpha20); //Vertical central
            //Valores dos eixos
            GUI.Label(Chart.AllRectInfo[10], Chart.MaxValueY.ToString("G3"));
            GUI.Label(Chart.AllRectInfo[11], Chart.MinValueY.ToString("G3"));
            GUI.Label(Chart.AllRectInfo[12], Chart.MinValueX.ToString());
            GUI.Label(Chart.AllRectInfo[13], Chart.MaxValueX.ToString());

            GUI.color = Color.red;
            for (int i = 0; i < YValues.Length - 1; i++)
            {
                if (Chart.MaxValueY != Chart.MinValueY)
                {
                    Chart.percStart = ((YValues[i] - Chart.MinValueY) / (Chart.MaxValueY - Chart.MinValueY));
                    Chart.percFinish = ((YValues[i + 1] - Chart.MinValueY) / (Chart.MaxValueY - Chart.MinValueY));
                }
                GUI.color = Color.red;
                Chart.Start = new Vector2(Chart.AllRectInfo[4].x + Chart.barWidth1 * i, Chart.AllRectInfo[6].y + Chart.AllRectInfo[6].height - (int)(((float)Chart.barHeight) * Chart.percStart));
                Chart.Finish = new Vector2(Chart.AllRectInfo[4].x + Chart.barWidth1 * (i + 1), Chart.AllRectInfo[6].y + Chart.AllRectInfo[6].height - (int)(((float)Chart.barHeight) * Chart.percFinish));
                if (Chart.Start.x != Chart.Finish.x || Chart.Start.y != Chart.Finish.y)
                    DrawLineStretched(Chart.Start, Chart.Finish, RedTexture, Padding/3);
            }
            GUI.color = Color.white;

            centeredStyle.alignment = TextAnchor.UpperLeft;
        }

        //Atualiza um gráfico, deve ser aplicado apenas na atualização dos dados
        public static void UpdateChart(ChartData Chart, float[] XValues, float[] YValues)
        {
            Chart.MinValueX = Min(XValues);
            Chart.MinValueY = Min(YValues);
            Chart.MaxValueX = Max(XValues);
            Chart.MaxValueY = Max(YValues);
            Chart.barWidth1 = Chart.AllRectInfo[4].width / XValues.Length;
            Chart.barHeight = Chart.AllRectInfo[6].height;
        }

        //Maximum value of array
        public static float Max(float[] Entrada)
        {
            float maximo = float.NegativeInfinity;
            for (int i = 0; i < Entrada.Length; i++)
            {
                if (Entrada[i] > maximo) maximo = Entrada[i];
            }
            return maximo;
        }

        //Minimum value of array
        public static float Min(float[] Entrada)
        {
            float minimo = float.PositiveInfinity;
            for (int i = 0; i < Entrada.Length; i++)
            {
                if (Entrada[i] < minimo) minimo = Entrada[i];
            }
            return minimo;
        }

        //Coleta o lixo
        public static IEnumerator CollectGarbage()
        {
            while (true)
            {
                Resources.UnloadUnusedAssets();
                yield return new WaitForSeconds(CollectGarbageInterval);
            }
        }

        //Executa um som a partir do CoreObject
        public static void PlaySound(GameObject Source, int Number, float Intensity)
        {
            if (Number == 0) Source.GetComponent<AudioSource>().PlayOneShot(clip1, Intensity);
            if (Number == 1) Source.GetComponent<AudioSource>().PlayOneShot(clip2, Intensity);
            if (Number == 2) Source.GetComponent<AudioSource>().PlayOneShot(clip3, Intensity); //Alarme 15 segundos
            if (Number == 3) Source.GetComponent<AudioSource>().PlayOneShot(clip4, Intensity); //Alarme 60 segundos
        }

        //Gerencia o status da bateria
        public static IEnumerator ManageBattery(ProcessDetails ThisProcess)
        {
            while (true)
            {
                if (SystemInfo.batteryStatus == BatteryStatus.Charging)
                {
                    ThisProcess.DeviceBattery.ChargerConnected = true;
                }
                else
                {
                    ThisProcess.DeviceBattery.ChargerConnected = false;
                }
                ThisProcess.DeviceBattery.BatteryLevel = SystemInfo.batteryLevel;
                ThisProcess.DeviceBattery.TimeLastUpdate = Time.time;
                yield return new WaitForSeconds(13f);
            }
        }

        // Verifica a conexão com a internet de forma contínua
        // Função principal que gerencia a conexão com a internet
        public static IEnumerator ManageInternetConnection(ProcessDetails ThisProcess)
        {
            bool connected = false;

            // Primeira verificação
            yield return CheckInternetConnection(result => connected = result);

            if (connected)
            {
                ThisProcess.WebConnection.Connected = true;
                ThisProcess.WebConnection.InitialTimeOfConnection = Time.time;
            }

            while (true)
            {
                for (int i = 0; i < MaxChecksBeforeRetry; i++)
                {
                    if (connected)
                    {
                        ThisProcess.WebConnection.ConnectionTime = Time.time - ThisProcess.WebConnection.InitialTimeOfConnection;
                    }
                    yield return new WaitForSeconds(CheckInterval);
                }

                if (Application.internetReachability != NetworkReachability.NotReachable)
                {
                    yield return CheckInternetConnection(result => connected = result);
                }
                else
                {
                    connected = false;
                }

                // Atualiza os dados da conexão
                if (!ThisProcess.WebConnection.Connected && connected)
                {
                    ThisProcess.WebConnection.Reconnections++;
                    ThisProcess.WebConnection.InitialTimeOfConnection = Time.time;
                }

                ThisProcess.WebConnection.Connected = connected;

                if (connected)
                {
                    ThisProcess.WebConnection.ConnectionTime = Time.time - ThisProcess.WebConnection.InitialTimeOfConnection;
                }

                ThisProcess.WebConnection.TimeLastUpdate = Time.time;

                if (ThisProcess.WebConnection.Reconnections > 2)
                {
                    ThisProcess.WebConnection.UnstableConnection = true;
                }
            }
        }

        // Verifica a conexão com o servidor e retorna via callback
        private static IEnumerator CheckInternetConnection(System.Action<bool> resultCallback)
        {
            using (var request = UnityWebRequest.Head(EchoServerUrl))
            {
                request.timeout = TimeoutSeconds;
                yield return request.SendWebRequest();

                #if UNITY_2020_2_OR_NEWER
                bool success = request.result == UnityWebRequest.Result.Success && request.responseCode == 200;
                #else
                bool success = !request.isNetworkError && !request.isHttpError && request.responseCode == 200;
                #endif
                resultCallback(success);
            }
        }

        //Atualiza as informações gerais do proccesso
        public static IEnumerator ManageProcess(ProcessDetails ThisProcess,VirtualAssistant Assistant)
        {
            float Count1min = 1f;
            float Count15min = 30f;

            if ((int)ThisProcess.PLCType == 0) HardwareMaxbits = 255;   //Arduino
            if ((int)ThisProcess.PLCType == 1) HardwareMaxbits = 1023;  //ESP8266

            //pegar localização da internet
            using (UnityWebRequest webRequest = UnityWebRequest.Get("https://www.ip-adress.com/what-is-my-ip-address"))
            {

                yield return webRequest.SendWebRequest();   // Request and wait for the desired page.

                if (webRequest.isNetworkError || webRequest.isHttpError) { LogInfo("Erro ao adquirir dados de localização: " + webRequest.error); } //Em caso de erro
                else
                {
                    ThisProcess.LocalData.LocationFrom = "https://www.ip-adress.com/what-is-my-ip-address";
                    GetInfoFromIPAdress(ThisProcess, webRequest.downloadHandler.text);
                }
            }

            while (true)
            {
                ThisProcess.Time = Time.time;
                ThisProcess.TimeHour = System.DateTime.Now.ToString("HH.mm.ss");
                ThisProcess.TotalPowerConsumption = PowerCalculation(ThisProcess);
                //verifica se o processo esta conectado de alguma forma, atualizar caso seja adicionada outra forma de conexão
                if (ThisProcess.CableConnection.Connected || (ThisProcess.RemoteProcessConnection.ConnectedRPC && ThisProcess.RemoteProcessConnection.RPCClient) || ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer)
                {
                    ThisProcess.Connected = true;
                }
                else
                {
                    ThisProcess.Connected = false;
                }

                //Emails
                if (ThisProcess.Emails.SendEmail)
                {
                    SendEmail(ThisProcess, Assistant);
                    ThisProcess.Emails.SendEmail = false;
                }
                if (Time.time >= ThisProcess.Emails.SendMarkInterval)
                {
                    ThisProcess.Emails.SendEmail = true;
                    ThisProcess.Emails.SendMarkInterval += ThisProcess.Emails.SendInterval * 3600f;
                }

                //A cada 1 minuto
                if (ThisProcess.Time > Count1min)
                {
                    //Dados de localização
                    if (ThisProcess.LocalData.LocationCity == "" && ThisProcess.LocalData.LocationState == "" && ThisProcess.WebConnection.Connected)
                    {
                        //pegar localização da internet
                        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://www.ip-adress.com/what-is-my-ip-address"))
                        {
                            yield return webRequest.SendWebRequest();   // Request and wait for the desired page.

                            if (webRequest.isNetworkError || webRequest.isHttpError) { LogInfo("Erro ao adquirir dados de localização: " + webRequest.error); } //Em caso de erro
                            else
                            {
                                ThisProcess.LocalData.LocationFrom = "https://www.ip-adress.com/what-is-my-ip-address";
                                GetInfoFromIPAdress(ThisProcess, webRequest.downloadHandler.text);
                            }
                        }
                    }
                    //Dados de clima
                    if (ThisProcess.LocalData.LocationLatitude != "" && ThisProcess.LocalData.TempMorning == 0f && ThisProcess.LocalData.TempEvening == 0f)
                    {
                        string latitude = (float.Parse(ThisProcess.LocalData.LocationLatitude.Replace(".", ","))).ToString("F2").Replace(",", ".");
                        string longitude = (float.Parse(ThisProcess.LocalData.LocationLongitude.Replace(".", ","))).ToString("F2").Replace(",", ".");
                        //pegar localização da internet
                        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://weather.com/weather/today/l/" + latitude + "," + longitude + "?par=google&temp=c"))
                        {
                            yield return webRequest.SendWebRequest();   // Request and wait for the desired page.

                            if (webRequest.isNetworkError || webRequest.isHttpError) { LogInfo("Erro ao adquirir dados de clima: " + webRequest.error); } //Em caso de erro
                            else
                            {
                                ThisProcess.LocalData.WeatherFrom = webRequest.url;
                                GetInfoFromWeatherAdress(ThisProcess, webRequest.downloadHandler.text);
                            }
                        }
                        ActualTemp(ThisProcess);
                    }

                    ThisProcess.TimeDate = System.DateTime.Now.ToString("dd-MM-yyyy");
                    Count1min += 60;
                }
                if (ThisProcess.Time > Count15min)
                {
                    //pegar temperatura local da internet
                    //UnityEngine.Debug.LogWarning("Tentando obter dados de clima");
                    if (ThisProcess.WebConnection.Connected)
                    {
                        string latitude = (float.Parse(ThisProcess.LocalData.LocationLatitude.Replace(".", ","))).ToString("F2").Replace(",", ".");
                        string longitude = (float.Parse(ThisProcess.LocalData.LocationLongitude.Replace(".", ","))).ToString("F2").Replace(",", ".");
                        //pegar localização da internet
                        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://weather.com/weather/today/l/" + latitude + "," + longitude + "?par=google&temp=c"))
                        {
                            yield return webRequest.SendWebRequest();   // Request and wait for the desired page.

                            if (webRequest.isNetworkError || webRequest.isHttpError) { LogInfo("Erro ao adquirir dados de clima: " + webRequest.error); } //Em caso de erro
                            else
                            {
                                ThisProcess.LocalData.WeatherFrom = webRequest.url;
                                GetInfoFromWeatherAdress(ThisProcess, webRequest.downloadHandler.text);
                            }
                        }
                    }
                    ActualTemp(ThisProcess);
                    Count15min += 899.5f;
                }
                yield return new WaitForSeconds(0.5f);
            }
        }

        public static void GetInfoFromIPAdress(ProcessDetails ThisProcess, string text)
        {
            string[] lines = text.Replace("\r", "").Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                    if (lines[i].Contains("Country</th><td>"))  //Pais
                    {
                        //UnityEngine.Debug.LogWarning(lines[i]);
                        ThisProcess.LocalData.LocationCountry = FindUsingDelimiters(lines[i], "Country</th><td>", "</td></tr>");
                        //if (ThisProcess.LocalData.LocationCountry == "Brazil") ThisProcess.LocalData.LocationCountry = "Brasil";
                    }
                    if (lines[i].Contains("City</th><td>"))     //Cidade
                    {
                        ThisProcess.LocalData.LocationCity = FindUsingDelimiters(lines[i], "City</th><td>", "</td></tr>");
                    }
                    if (lines[i].Contains("State</th><td>"))     //Estado
                    {
                        ThisProcess.LocalData.LocationState = FindUsingDelimiters(lines[i], "State</th><td>", "</td></tr>");
                    }
                    if (lines[i].Contains("What Is My IP</a>:<br/>"))     //IP
                    {
                        ThisProcess.LocalData.LocationIPAdress = FindUsingDelimiters(lines[i], "What Is My IP</a>:<br/>", "</");
                    }
                    if (lines[i].Contains("Latitude</th><td>"))     //Latitude
                    {
                        ThisProcess.LocalData.LocationLatitude = FindUsingDelimiters(lines[i], "(", ")");
                    }
                    if (lines[i].Contains("Longitude</th><td>"))     //Longitude
                    {
                        ThisProcess.LocalData.LocationLongitude = FindUsingDelimiters(lines[i], "(", ")");
                    }
            }
            if (ThisProcess.LocalData.LocationCountry == "" && ThisProcess.LocalData.LocationIPAdress == "")
            {
                LogInfo("Houve alteração na página de indentificação de dados locais");
            }
            if (ThisProcess.LocalData.LocationCountry == "") ThisProcess.LocalData.LocationCountry = "------";
            if (ThisProcess.LocalData.LocationIPAdress != "" && ThisProcess.LocalData.LocationCity != "" && ThisProcess.LocalData.LocationState != "") LogInfo("Indentificação de dados locais obtida"); ;
        }

        public static void ActualTemp(ProcessDetails ThisProcess)
        {
            if (int.Parse(ThisProcess.TimeHour.Substring(0, 2)) >= 0 && int.Parse(ThisProcess.TimeHour.Substring(0, 2)) < 6)
                ThisProcess.LocalData.TempActual = ThisProcess.LocalData.TempOvernight.ToString();
            if (int.Parse(ThisProcess.TimeHour.Substring(0, 2)) >= 6 && int.Parse(ThisProcess.TimeHour.Substring(0, 2)) < 12)
                ThisProcess.LocalData.TempActual = ThisProcess.LocalData.TempMorning.ToString();
            if (int.Parse(ThisProcess.TimeHour.Substring(0, 2)) >= 12 && int.Parse(ThisProcess.TimeHour.Substring(0, 2)) < 18)
                ThisProcess.LocalData.TempActual = ThisProcess.LocalData.TempAfternoon.ToString();
            if (int.Parse(ThisProcess.TimeHour.Substring(0, 2)) >= 18)
                ThisProcess.LocalData.TempActual = ThisProcess.LocalData.TempEvening.ToString();
        }

        public static string FindUsingDelimiters(string Input, string InitDelimiter, string FinDelimiter)
        {
            string result = "";
            string Temp;
            int ix = Input.IndexOf(InitDelimiter);
            //UnityEngine.Debug.LogWarning("IX " + ix.ToString());
            if (ix != -1)
            {
                try
                {
                    Temp = Input.Substring(ix + InitDelimiter.Length, Input.Length-(ix + InitDelimiter.Length));
                    //UnityEngine.Debug.LogWarning(Temp);
                    Temp = Temp.Substring(0, Temp.IndexOf(FinDelimiter));
                    //UnityEngine.Debug.LogWarning(Temp);
                    result = Temp;
                }
                catch{LogInfo("Problemas na conversão. Provavelmente houve alteração na página de indentificação de dados");}
            }
            else{LogInfo("Delimitador inicial não encontrado");}
            return result;
        }

        public static void GetInfoFromWeatherAdress(ProcessDetails ThisProcess, string text)
        {
            string[] lines = text.Replace("\r", "").Split('\n');
            
            for (int i = 0; i < lines.Length; i++)
            {
                //Humidade (%)
                if (lines[i].Contains("<title>Humidity</title>"))
                {
                    GatherWeatherData(lines[i], ThisProcess, "Humidity");
                }

                //Vento (Km/h)
                if (lines[i].Contains("<title>Wind</title>"))
                {
                    GatherWeatherData(lines[i], ThisProcess, "Wind");
                }

                //Manhã
                if (lines[i].Contains("Morning</span>"))
                {
                    GatherWeatherData(lines[i], ThisProcess, "Morning");
                }

                //Tarde
                if (lines[i].Contains("Afternoon</span>"))
                {
                    GatherWeatherData(lines[i], ThisProcess, "Afternoon");
                }

                //Noite
                if (lines[i].Contains("Evening</span>"))
                {
                    GatherWeatherData(lines[i], ThisProcess, "Evening");
                }

                //Madrugada
                if (lines[i].Contains("Overnight</span>"))
                {
                    GatherWeatherData(lines[i], ThisProcess, "Overnight");
                }

            }
            if (ThisProcess.LocalData.TempMorning != 0f && ThisProcess.LocalData.TempEvening != 0f && ThisProcess.LocalData.TempAfternoon != 0f)
            {
                LogInfo("Indentificação de dados de clima obtida");
            }
            else
            {
                LogInfo("Houve alterações na página de indentificação de dados de clima");
            }
        }

        //conseguir dados a partir de linhas do site
        public static void GatherWeatherData(string line, ProcessDetails ThisProcess, string type)
        {
            string toBeSearched;
            string Temp;
            int ix;
            if (line.Contains("<span data-testid=\"TemperatureValue\">"))
            {
                toBeSearched = "<span data-testid=\"TemperatureValue\">";
                ix = line.IndexOf(toBeSearched);

                if (ix != -1)
                {
                    try
                    {
                        Temp = line.Substring(ix + toBeSearched.Length, 5);
                        Temp = Temp.Substring(0, Temp.IndexOf("°"));
                        //UnityEngine.Debug.LogWarning(Temp);
                        if (type == "Morning") ThisProcess.LocalData.TempMorning = float.Parse(Temp);
                        if (type == "Afternoon") ThisProcess.LocalData.TempAfternoon = float.Parse(Temp);
                        if (type == "Evening") ThisProcess.LocalData.TempEvening = float.Parse(Temp);
                        if (type == "Overnight") ThisProcess.LocalData.TempOvernight = float.Parse(Temp);
                    }
                    catch
                    {
                        LogInfo("Problemas na conversão de valores de temperatura de clima. Provavelmente houve alteração na página de indentificação de dados");
                    }
                }
                else
                {
                    LogInfo("Problemas em encontrar temperatura de clima.");
                }
            }
            if (line.Contains("0\"><title>"))
            {
                toBeSearched = "0\"><title>";
                ix = line.IndexOf(toBeSearched);

                if (ix != -1)
                {
                    if (type == "Morning")
                    {
                        ThisProcess.LocalData.MorningWeather = line.Substring(ix + toBeSearched.Length, 60);
                        ThisProcess.LocalData.MorningWeather = ThisProcess.LocalData.MorningWeather.Substring(0, ThisProcess.LocalData.MorningWeather.IndexOf("</title>"));
                    }
                    if (type == "Afternoon")
                    {
                        ThisProcess.LocalData.AfternoonWeather = line.Substring(ix + toBeSearched.Length, 60);
                        ThisProcess.LocalData.AfternoonWeather = ThisProcess.LocalData.AfternoonWeather.Substring(0, ThisProcess.LocalData.AfternoonWeather.IndexOf("</title>"));
                    }
                    if (type == "Evening")
                    {
                        ThisProcess.LocalData.EveningWeather = line.Substring(ix + toBeSearched.Length, 60);
                        ThisProcess.LocalData.EveningWeather = ThisProcess.LocalData.EveningWeather.Substring(0, ThisProcess.LocalData.EveningWeather.IndexOf("</title>"));
                    }
                    if (type == "Overnight")
                    {
                        ThisProcess.LocalData.OvernightWeather = line.Substring(ix + toBeSearched.Length, 60);
                        ThisProcess.LocalData.OvernightWeather = ThisProcess.LocalData.OvernightWeather.Substring(0, ThisProcess.LocalData.OvernightWeather.IndexOf("</title>"));
                    }
                }
                else
                {
                    LogInfo("Problemas em encontrar condições de clima.");
                }
                //Exemplos:
                //Sunny, Partly Cloudy, Mostly Cloudy, Mostly Cloudy Night, Scattered Thunderstorms Night
            }

            if (type == "Humidity")
            {
                if (line.Contains("testid=\"PercentageValue\">"))
                {
                    toBeSearched = "testid=\"PercentageValue\">";
                    ix = line.IndexOf(toBeSearched);

                    if (ix != -1)
                    {
                        try
                        {
                            Temp = line.Substring(ix + toBeSearched.Length, 5);
                            Temp = Temp.Substring(0, Temp.IndexOf("%"));
                            ThisProcess.LocalData.PercentageHumidity = float.Parse(Temp);
                        }
                        catch
                        {
                            LogInfo("Problemas na conversão de valores de humidade. Provavelmente houve alteração na página de indentificação de dados");
                        }
                    }
                    else
                    {
                        LogInfo("Problemas em encontrar humidade.");
                    }
                }
            }
            if (type == "Wind")
            {
                if (line.Contains(" km/h"))
                {
                    toBeSearched = " km/h";
                    ix = line.IndexOf(toBeSearched);

                    if (ix != -1)
                    {
                        try
                        {
                            Temp = line.Substring(ix - 7, 7);
                            toBeSearched = "g>";
                            ix = Temp.IndexOf(toBeSearched);
                            Temp = Temp.Substring(ix + toBeSearched.Length, Temp.Length - toBeSearched.Length - ix);
                            ThisProcess.LocalData.WindSpeedKmh = float.Parse(Temp);
                        }
                        catch
                        {
                            LogInfo("Problemas na conversão de valores de velocidade do vento. Provavelmente houve alteração na página de indentificação de dados");
                        }
                    }
                    else
                    {
                        LogInfo("Problemas em encontrar velocidade do vento.");
                    }
                }
            }
            //Ainda é possivel conseguir: Pressão (mB), Visibilidade (Km), Ponto de condensação da água (°C), índice UV ([0-10] e Extreme)
        }

        //Conecta ao arduino
        public static IEnumerator ManageRemoteConnection(ProcessDetails ThisProcess, GameObject NetworkObject)
        {
            ThisProcess.RemoteProcessConnection.NetCommunication = NetworkObject.GetComponent<NetworkCommunication>();
            ThisProcess.RemoteProcessConnection.NetLauncher = NetworkObject.GetComponent<NetworkLauncher>();
            float UpdateRPCTime = Time.time;
            float UpdateRPCTimeInterval = 1.13f;

            while (true)
            {
                //conexão via RPC
                if (Time.time >= UpdateRPCTime)
                {
                    GenerateRemoteConectionStatus(ThisProcess.RemoteProcessConnection);
                    ThisProcess.RemoteProcessConnection.Ping = ThisProcess.RemoteProcessConnection.NetCommunication.Ping;
                    if (ThisProcess.RemoteProcessConnection.ConnectedRPC != (ThisProcess.RemoteProcessConnection.NetLauncher.client || ThisProcess.RemoteProcessConnection.NetLauncher.host))
                    {
                        ThisProcess.RemoteProcessConnection.ConnectedRPC = (ThisProcess.RemoteProcessConnection.NetLauncher.client || ThisProcess.RemoteProcessConnection.NetLauncher.host);
                        ThisProcess.RemoteProcessConnection.InitialTimeOfConnection = Time.time;
                        ThisProcess.RemoteProcessConnection.RPCHost = ThisProcess.RemoteProcessConnection.NetLauncher.host;
                        ThisProcess.RemoteProcessConnection.RPCClient = ThisProcess.RemoteProcessConnection.NetLauncher.client;
                    }

                    ThisProcess.RemoteProcessConnection.ConnectionTime = Time.time - ThisProcess.RemoteProcessConnection.InitialTimeOfConnection;
                    ThisProcess.RemoteProcessConnection.TimeSinceLastUpdate = Time.time - ThisProcess.RemoteProcessConnection.TimeLastUpdate;
                    ThisProcess.RemoteProcessConnection.TryingToConnectRPC = !ThisProcess.RemoteProcessConnection.NetLauncher.client;
                    UpdateRPCTime += UpdateRPCTimeInterval;
                }
                yield return new WaitForSeconds(0.9f);
            }
        }

        //Conecta ao arduino
        public static IEnumerator ManageLocalWebConnection(ProcessDetails ThisProcess, GameObject NetworkObject)
        {
            string URLContent;
            string TempStringIP;
            int IPRange = 80;
            //Caso não haja a mínima ideia de qual é o IP, procure próximo do IP deste dispositivo
            if (ThisProcess.LocalWifiProcessConnection.InitialLocalServerIPToSearch == "")
            {
                ThisProcess.LocalWifiProcessConnection.InitialLocalServerIPToSearch = LocalIPAddress();
            }
            
            while (true)
            {
                //Conexão via Wifi
                if (!ThisProcess.LocalWifiProcessConnection.localProcessWifiInterrupted)
                {
                    //Procura nos IPs locais em função do primeiro IP dado (ThisProcess.LocalWifiProcessConnection.WebServerIP)
                    while (!ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer)
                    {
                        ThisProcess.LocalWifiProcessConnection.TryingToConnectLocalProcessServer = true;
                        ThisProcess.LocalWifiProcessConnection.WebServerIPDivided = Array.ConvertAll(ThisProcess.LocalWifiProcessConnection.InitialLocalServerIPToSearch.Split('.'), int.Parse);
                        for (int i = 0; i <= 5; i++)//indice 2
                        {
                            for (int j = 0; j <= IPRange; j++)//indice 3
                            {
                                for (int k = 1; k >= -1; k -= 2)//para mais ou para menos no indice 2
                                {
                                    for (int l = 1; l >= -1; l -= 2)//para mais ou para menos no indice 3
                                    {
                                        if (ThisProcess.LocalWifiProcessConnection.WebServerIPDivided[3] + j * l >= 0 && ThisProcess.LocalWifiProcessConnection.WebServerIPDivided[2] + i * k >= 0 && !ThisProcess.LocalWifiProcessConnection.localProcessWifiInterrupted)
                                        {
                                            TempStringIP = ThisProcess.LocalWifiProcessConnection.WebServerIPDivided[0].ToString() + "." +
                                                ThisProcess.LocalWifiProcessConnection.WebServerIPDivided[1].ToString() + "." +
                                                (ThisProcess.LocalWifiProcessConnection.WebServerIPDivided[2] + i * k).ToString() + "." +
                                                (ThisProcess.LocalWifiProcessConnection.WebServerIPDivided[3] + j * l).ToString();
                                            ThisProcess.LocalWifiProcessConnection.SearchingOnLocalServerIP = TempStringIP;
                                            //UnityEngine.Debug.LogWarning("TempIP: " + TempStringIP);

                                            UnityEngine.Ping p = new UnityEngine.Ping(TempStringIP); //faz um ping para o IP alvo
                                            yield return new WaitForSeconds(1.2f);
                                            if (p.isDone) //verifica se foi feito com sucesso, isso indica que o IP está sendo usado
                                            {
                                                //UnityEngine.Debug.LogWarning(p.time.ToString());

                                                using (UnityWebRequest webRequest = UnityWebRequest.Get(TempStringIP))
                                                {
                                                    // Request and wait for the desired page.
                                                    yield return webRequest.SendWebRequest();

                                                    if (webRequest.isNetworkError || webRequest.isHttpError)
                                                    {
                                                        //não encontrado aqui
                                                    }
                                                    else
                                                    {
                                                        URLContent = webRequest.downloadHandler.text;
                                                        string[] lines = URLContent.Replace("\r", "").Split('\n');
                                                        //if (lines[2].Contains("JOEL") && lines[2].Contains(ThisProcess.Code))  //Verificação da validade da página
                                                        if ((lines[0].Contains("JOEL") && lines[0].Contains(ThisProcess.Code)) || lines[2].Contains("LF"))
                                                        {
                                                            //LogInfo("Processo na rede local encontrado no IP: " + TempStringIP);
                                                            ThisProcess.LocalWifiProcessConnection.LocalServerIP = TempStringIP;
                                                            setupSocket(ThisProcess.LocalWifiProcessConnection);
                                                            yield return new WaitForSeconds(1.5f);
                                                            if (SocketConnected(ThisProcess.LocalWifiProcessConnection.mySocket.Client))
                                                            {
                                                                LogInfo("Processo conectado localmente pelo IP: " + TempStringIP);
                                                                ThisProcess.LocalWifiProcessConnection.InitialTimeOfConnection = Time.time;
                                                                ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer = true;
                                                                ThisProcess.LocalWifiProcessConnection.TryingToConnectLocalProcessServer = false;
                                                                if (!ThisProcess.CableConnection.Connected) //Interromper a tentativa de conexão por cabo
                                                                {
                                                                    ThisProcess.CableConnection.CableConectionInterrupted = true;
                                                                    ThisProcess.CableConnection.TryingToConnect = false;
                                                                }
                                                                else
                                                                {
                                                                    Speak("O dispositivo esta tentando se conectar de duas forma simultaneas ao processo");
                                                                    LogInfo("O dispositivo esta tentando se conectar de duas forma simultaneas ao processo");
                                                                }
                                                                yield return new WaitForSeconds(1.5f);
                                                                writeSocket(ThisProcess.LocalWifiProcessConnection, "ConnectionON"); //Handshake
                                                                break;
                                                            }
                                                        }       
                                                    }
                                                }
                                            }
                                            p.DestroyPing();
                                        }
                                    }
                                    if (ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer || ThisProcess.LocalWifiProcessConnection.localProcessWifiInterrupted) break;
                                }
                                if (ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer || ThisProcess.LocalWifiProcessConnection.localProcessWifiInterrupted) break;
                            }
                            if (ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer || ThisProcess.LocalWifiProcessConnection.localProcessWifiInterrupted) break;
                        }
                        if (ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer || ThisProcess.LocalWifiProcessConnection.localProcessWifiInterrupted) break;
                        yield return new WaitForSeconds(0.004f);
                    }

                    ThisProcess.LocalWifiProcessConnection.SearchingOnLocalServerIP = ThisProcess.LocalWifiProcessConnection.InitialLocalServerIPToSearch;

                    //Se comunica quando o dispositivo for encontado na rede local
                    if (ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer && ThisProcess.LocalWifiProcessConnection.LocalServerIP!="")
                    {
                        maintainConnection(ThisProcess.LocalWifiProcessConnection);
                        //writeSocket(ThisProcess.LocalWifiProcessConnection, "ConnectionON"); //Handshake
                        //writeSocket(ThisProcess.LocalWifiProcessConnection, "Testando " + Time.time.ToString());  
                        yield return new WaitForSeconds(0.004f);

                        ThisProcess.LocalWifiProcessConnection.Recepted = readSocket(ThisProcess.LocalWifiProcessConnection);
                        if (ThisProcess.LocalWifiProcessConnection.SocketRead != ThisProcess.LocalWifiProcessConnection.Recepted)
                        {
                            COMString += "\n<-- " + ThisProcess.LocalWifiProcessConnection.SocketRead; //Melhorar
                            if (ScrollDebugString) COMDebugScrollPosition += new Vector2(0, 25000);    //melhorar
                            ThisProcess.LocalWifiProcessConnection.LastSocketRead = ThisProcess.LocalWifiProcessConnection.SocketRead;
                            ThisProcess.LocalWifiProcessConnection.SocketRead = ThisProcess.LocalWifiProcessConnection.Recepted;
                        }

                            /*
                            using (UnityWebRequest webRequest = UnityWebRequest.Get(ProcWifiConnection.LocalServerIP))
                            {
                                // Request and wait for the desired page.
                                yield return webRequest.SendWebRequest();

                                if (webRequest.isNetworkError || webRequest.isHttpError)
                                {
                                    LogInfo("O processo conectado diretamente a rede local foi desconetado");
                                    LogInfo("Verifique a conexão e resete o controlador");
                                    LogInfo(webRequest.error);
                                    ProcWifiConnection.ConnectedLocalProcessServer = false;
                                    ProcWifiConnection.TryingToConnectLocalProcessServer = true;
                                }
                                else
                                {

                                    URLContent = webRequest.downloadHandler.text;
                                    //Consiga as informações na rede local
                                }
                            }
                            */

                            ThisProcess.LocalWifiProcessConnection.ConnectionTime = Time.time - ThisProcess.LocalWifiProcessConnection.InitialTimeOfConnection;
                        ThisProcess.LocalWifiProcessConnection.TimeSinceLastUpdate = Time.time - ThisProcess.LocalWifiProcessConnection.TimeLastUpdate;
                    }
                }
                yield return new WaitForSeconds(0.004f);
            }
        }

        public static string LocalIPAddress()
        {
            string result = "192.168.1.10";
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (!ip.IsDnsEligible)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            if (ip.Address.ToString().Contains("192"))
                            {
                                result = ip.Address.ToString();
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static void setupSocket(ProcessWifiConnection RemoteConnection)
        {   
            // Socket setup here
            try
            {
                RemoteConnection.mySocket = new TcpClient(RemoteConnection.LocalServerIP, RemoteConnection.Port);
                RemoteConnection.theStream = RemoteConnection.mySocket.GetStream();
                RemoteConnection.theWriter = new StreamWriter(RemoteConnection.theStream);
                RemoteConnection.theReader = new StreamReader(RemoteConnection.theStream);
                //writeSocket(RemoteConnection, "ConnectionON"); //Handshake
            }
            catch (Exception e)
            {
                LogInfo("Erro de socket: " + e);                // catch any exceptions
            }
        }

        public static void writeSocket(ProcessWifiConnection RemoteConnection, string theLine)
        {   // function to write data out
            if (!RemoteConnection.ConnectedLocalProcessServer && !RemoteConnection.mySocket.Connected)
                return;
            try
            {
                if (RemoteConnection.mySocket.Connected)
                {
                    RemoteConnection.theWriter.WriteLine(theLine);
                    RemoteConnection.theWriter.Flush();
                }
            }
            catch { }
        }

        public static string readSocket(ProcessWifiConnection RemoteConnection)
        {   // function to read data in
            string result = "";

            if (!RemoteConnection.theStream.DataAvailable)
            {
                result = "";
            }
            else
            {
                while (RemoteConnection.theStream.DataAvailable)
                {
                    result += RemoteConnection.theReader.ReadLine();
                    RemoteConnection.TimeLastUpdate = Time.time;
                    //UnityEngine.Debug.LogWarning(result);
                }
            }
            return result;
        }

        public static void closeSocket(ProcessWifiConnection RemoteConnection)
        {                            // function to close the socket
            if (!RemoteConnection.ConnectedLocalProcessServer)
                return;
            RemoteConnection.theWriter.Close();
            RemoteConnection.theReader.Close();
            RemoteConnection.mySocket.Close();
            RemoteConnection.ConnectedLocalProcessServer = false;
            RemoteConnection.LocalServerIP = "";
            //RemoteConnection.TryingToConnectLocalProcessServer = true;
        }

        public static void maintainConnection(ProcessWifiConnection RemoteConnection)
        {   // function to maintain the connection (not sure why! but Im sure it will become a solution to a problem at somestage)
            if (!SocketConnected(RemoteConnection.mySocket.Client))
            {
                setupSocket(RemoteConnection);
            }
        }

        public static bool SocketConnected(Socket s)
        {
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);
            if (part1 && part2)
                return false;
            else
                return true;
        }

        //gera o status da conexão remota como um string
        public static void GenerateRemoteConectionStatus(PhotonRemoteConnection ProcessRemoteConnection)
        {
            ProcessRemoteConnection.Status = "Status: ";
            if (ProcessRemoteConnection.NetLauncher.InternetConnected)
            {
                ProcessRemoteConnection.Status += "Conectado a internet";
                if (ProcessRemoteConnection.NetLauncher.MasterServerConnected)
                {
                    ProcessRemoteConnection.Status += ", Conectado ao servidor mestre";
                    if (ProcessRemoteConnection.NetLauncher.host)
                    {
                        ProcessRemoteConnection.Status += ", e é o servidor de dados";
                    }
                    if (ProcessRemoteConnection.NetLauncher.client)
                    {
                        ProcessRemoteConnection.Status += ", e esta conectado a outro computador.";
                    }
                    if (!ProcessRemoteConnection.NetLauncher.client && !ProcessRemoteConnection.NetLauncher.host)
                    {
                        ProcessRemoteConnection.Status += ", não está conectado a um processo remoto, nem é um servidor.";
                    }
                }
                else
                {
                    ProcessRemoteConnection.Status += ", Desconectado ao servidor mestre.";
                }
            }
            else
            {
                ProcessRemoteConnection.Status += "Desconectado da internet.";
            }
        }

         //Conecta ao arduino
        public static IEnumerator ManageProcessConnection(ProcessDetails ThisProcess)
        {
            string KeyWordHandshake = "JOEL";        //Palavra chave para reconhecer o dispositivo não conectado
            string KeyWordHandshake2 = "LF";         //Segunda palavra chave para reconhecer o dispositivo ja conectado
            //ThisProcess.Code também é uma palavra chave para reconhecer o dispositivo
            ThisProcess.CableConnection.Ports = SerialPort.GetPortNames();
            if (ThisProcess.CableConnection.Ports.Length >= 1) ThisProcess.CableConnection.ConnectIndex = ThisProcess.CableConnection.Ports.Length - 1;
            ThisProcess.CableConnection.TryingToConnectTo = new bool[25];
            ThisProcess.CableConnection.BusyPorts = new bool[25];
            while (true)
            {
                if (!ThisProcess.CableConnection.CableConectionInterrupted) //Interrompe a tentativa de conexão pela porta serial, pois já se conectou de outra forma
                {
                    //Atualizar lista de portas COM
                    if (Time.time >= ThisProcess.CableConnection.UpdateCOMList)
                    {
                        ThisProcess.CableConnection.Ports = SerialPort.GetPortNames();
                        if (ThisProcess.CableConnection.Ports.Length == 1) ThisProcess.CableConnection.ConnectIndex = 0;
                        if (ThisProcess.CableConnection.ConnectIndex >= ThisProcess.CableConnection.Ports.Length || ThisProcess.CableConnection.ConnectIndex < 0) ThisProcess.CableConnection.ConnectIndex = ThisProcess.CableConnection.Ports.Length - 1;
                        if (ThisProcess.CableConnection.Ports[ThisProcess.CableConnection.ConnectIndex] != ThisProcess.CableConnection.COMPortName)
                        {
                            for (int i = 0; i < ThisProcess.CableConnection.Ports.Length; i++)
                            {
                                if (ThisProcess.CableConnection.Ports[i] == ThisProcess.CableConnection.COMPortName)
                                {
                                    ThisProcess.CableConnection.ConnectIndex = i;
                                }
                            }
                        }
                        if (ThisProcess.CableConnection.Fails > 1 && ThisProcess.CableConnection.Reconnections > 1) ThisProcess.CableConnection.UnstableConnection = true;
                        ThisProcess.CableConnection.UpdateCOMList += 3f;
                    }
                    //Tentando se conectar
                    if (!ThisProcess.CableConnection.TryingToConnect && !ThisProcess.CableConnection.Connected && ThisProcess.CableConnection.Ports.Length > 0 && !ThisProcess.CableConnection.BusyPorts[ThisProcess.CableConnection.ConnectIndex])
                    {
                        ThisProcess.CableConnection.TryingToConnect = ConnectToProcess(ThisProcess.CableConnection, ThisProcess.CableConnection.ConnectIndex);
                        yield return new WaitForSeconds(0.1f);
                        ThisProcess.CableConnection.BusyPorts[ThisProcess.CableConnection.ConnectIndex] = !ThisProcess.CableConnection.TryingToConnect;
                        if (ThisProcess.CableConnection.ConnectionPort.IsOpen)
                        {
                            ThisProcess.CableConnection.ConnectionPort.WriteLine("ConnectionOFF"); //handshake
                            ThisProcess.CableConnection.COMPortName = ThisProcess.CableConnection.ConnectionPort.PortName;
                            ThisProcess.CableConnection.TryingToConnectTo[ThisProcess.CableConnection.ConnectIndex] = true;
                            ThisProcess.CableConnection.InitialTimeOfConnection = Time.time;
                        }
                    }
                    //Adquirindo resultado da porta COM
                    ThisProcess.CableConnection.TimeSinceLastUpdate = Time.time - ThisProcess.CableConnection.TimeLastUpdate;
                    ThisProcess.CableConnection.ConnectionTime = Time.time - ThisProcess.CableConnection.InitialTimeOfConnection;
                    if (ThisProcess.CableConnection.Ports.Length > 0)
                    {
                        if (ThisProcess.CableConnection.ConnectionPort.IsOpen)
                        {
                            try
                            {
                                ThisProcess.CableConnection.Recepted = ThisProcess.CableConnection.ConnectionPort.ReadLine();
                                if (ThisProcess.CableConnection.COMOutput != ThisProcess.CableConnection.Recepted)
                                {
                                    COMString += "\n<-- " + ThisProcess.CableConnection.Recepted;           //melhorar
                                    if (ScrollDebugString) COMDebugScrollPosition += new Vector2(0, 25000);    //melhorar
                                    ThisProcess.CableConnection.COMLastOutput = ThisProcess.CableConnection.COMOutput;
                                    ThisProcess.CableConnection.COMOutput = ThisProcess.CableConnection.Recepted;

                                    ThisProcess.CableConnection.TimeLastUpdate = Time.time;
                                    ThisProcess.CableConnection.TimeSinceLastUpdate = 0f;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    //Verificando se o dispositivo é o correto
                    if (!ThisProcess.CableConnection.Connected && (ThisProcess.CableConnection.COMOutput.Contains(KeyWordHandshake) || ThisProcess.CableConnection.COMOutput.Contains(KeyWordHandshake2) || ThisProcess.CableConnection.COMOutput.Contains(ThisProcess.Code)))
                    {
                        ThisProcess.CableConnection.ConnectionPort.WriteLine("ConnectionON"); //handshake
                        ThisProcess.CableConnection.TryingToConnect = false;
                        if (ThisProcess.CableConnection.Restarting) ThisProcess.CableConnection.Reconnections += 1;
                        ThisProcess.CableConnection.Restarting = false;
                        ThisProcess.CableConnection.Connected = true;
                        ThisProcess.CableConnection.COMPortName = ThisProcess.CableConnection.ConnectionPort.PortName;
                        ThisProcess.CableConnection.TryingToConnectTo[ThisProcess.CableConnection.ConnectIndex] = false;
                        ThisProcess.CableConnection.BusyPorts[ThisProcess.CableConnection.ConnectIndex] = false;
                        LogInfo("Processo verificado na porta " + ThisProcess.CableConnection.COMPortName);
                        yield return new WaitForSeconds(0.5f);
                        ThisProcess.CableConnection.ConnectionPort.WriteLine("ConnectionON"); //handshake verified
                        if (!ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer)
                        {
                            ThisProcess.LocalWifiProcessConnection.localProcessWifiInterrupted = true;
                            ThisProcess.LocalWifiProcessConnection.TryingToConnectLocalProcessServer = false;
                        }
                        else
                        {
                            Speak("O dispositivo esta tentando se conectar de duas forma simultaneas ao processo");
                            LogInfo("O dispositivo esta tentando se conectar de duas forma simultaneas ao processo");
                        }
                    }
                    //Tentando conexão com outra porta se a ultima porta da lista não é a correta
                    if (ThisProcess.CableConnection.ConnectionTime > 20 && !ThisProcess.CableConnection.Connected && ThisProcess.CableConnection.Ports.Length > 1)
                    {
                        ThisProcess.CableConnection.ConnectionPort.Close();
                        yield return new WaitForSeconds(1.25f);
                        ThisProcess.CableConnection.Ports = SerialPort.GetPortNames();
                        ThisProcess.CableConnection.TryingToConnectTo[ThisProcess.CableConnection.ConnectIndex] = false;
                        ThisProcess.CableConnection.ConnectIndex -= 1;
                        if (ThisProcess.CableConnection.ConnectIndex < 0) { ThisProcess.CableConnection.ConnectIndex = ThisProcess.CableConnection.Ports.Length - 1; }
                        ThisProcess.CableConnection.TryingToConnectTo[ThisProcess.CableConnection.ConnectIndex] = true;
                        if (!ThisProcess.CableConnection.BusyPorts[ThisProcess.CableConnection.ConnectIndex])
                        {
                            ThisProcess.CableConnection.TryingToConnect = ConnectToProcess(ThisProcess.CableConnection, ThisProcess.CableConnection.ConnectIndex);
                            yield return new WaitForSeconds(0.05f);
                            ThisProcess.CableConnection.BusyPorts[ThisProcess.CableConnection.ConnectIndex] = !ThisProcess.CableConnection.TryingToConnect;
                        }
                        if (ThisProcess.CableConnection.BusyPorts[ThisProcess.CableConnection.ConnectIndex] && UnityEngine.Random.Range(0.0f, 1.0f) > 0.7f)
                        {
                            ThisProcess.CableConnection.TryingToConnect = ConnectToProcess(ThisProcess.CableConnection, ThisProcess.CableConnection.ConnectIndex);
                            yield return new WaitForSeconds(0.05f);
                            ThisProcess.CableConnection.BusyPorts[ThisProcess.CableConnection.ConnectIndex] = !ThisProcess.CableConnection.TryingToConnect;
                        }
                        yield return new WaitForSeconds(0.1f);
                        if (ThisProcess.CableConnection.ConnectionPort.IsOpen)
                        {
                            ThisProcess.CableConnection.ConnectionPort.WriteLine("ConnectionOFF"); //handshake
                            ThisProcess.CableConnection.COMPortName = ThisProcess.CableConnection.ConnectionPort.PortName;
                            ThisProcess.CableConnection.InitialTimeOfConnection = Time.time;
                            ThisProcess.CableConnection.BusyPorts[ThisProcess.CableConnection.ConnectIndex] = false;
                        }
                    }

                    //Se o arduino for resetado
                    if (ThisProcess.CableConnection.Connected && (ThisProcess.CableConnection.COMOutput.Contains(KeyWordHandshake) || ThisProcess.CableConnection.COMOutput.Contains(ThisProcess.Code)))
                    {
                        ThisProcess.CableConnection.Restarts += 1;
                        ThisProcess.CableConnection.ConnectionPort.WriteLine("ConnectionON"); //handshake
                        LogInfo("Conexão com o dispositivo de controle resetada");
                        yield return new WaitForSeconds(0.5f);
                    }

                    //Tentando re-conexão
                    if (ThisProcess.CableConnection.TimeSinceLastUpdate > 10 && ThisProcess.CableConnection.Connected)
                    {
                        ThisProcess.CableConnection.TryingToConnectTo[ThisProcess.CableConnection.ConnectIndex] = false;
                        ThisProcess.CableConnection.TimeSinceLastUpdate = 0f;
                        ThisProcess.CableConnection.TimeLastUpdate = Time.time;
                        ThisProcess.CableConnection.ConnectionPort.Close();
                        yield return new WaitForSeconds(1.25f);
                        ThisProcess.CableConnection.Ports = SerialPort.GetPortNames();
                        if (ThisProcess.CableConnection.Ports.Length >= 1) ThisProcess.CableConnection.ConnectIndex = ThisProcess.CableConnection.Ports.Length - 1;
                        ThisProcess.CableConnection.COMOutput = "";
                        ThisProcess.CableConnection.Restarting = true;
                        ThisProcess.CableConnection.Fails += 1;
                        ThisProcess.CableConnection.TryingToConnect = false;
                        ThisProcess.CableConnection.Connected = false;
                    }
                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        //Conexão
        public static bool ConnectToProcess(ProcessConnection Connection, int i)
        {
            bool Sucess = false;
            // Get a list of serial port names.
            Connection.Ports = SerialPort.GetPortNames();

            //Iniciando a conexao com o Arduino
            if (Connection.Ports.Length > 0 && i < Connection.Ports.Length && i >= 0)
            {
                if (COMPortNumber(Connection.Ports[i]) < 10)
                {
                    Connection.ConnectionPort = new SerialPort(Connection.Ports[i], Connection.BaudRate);
                    Connection.ConnectionPort.DtrEnable = false;
                }
                if (COMPortNumber(Connection.Ports[i]) >= 10)
                {
                    Connection.ConnectionPort = new SerialPort("\\\\.\\" + Connection.Ports[i], Connection.BaudRate);
                    Connection.ConnectionPort.DtrEnable = false;
                }
                Sucess = OpenConnection(Connection.ConnectionPort);
                if (!Sucess)
                {
                    LogInfo("Problemas na tentativa de conexão à porta " + Connection.ConnectionPort.PortName);
                }
            }

            if (Connection.Ports.Length == 0)
            {
                LogInfo("Nao ha portas seriais disponiveis, o sistema esta desconectado");
                Sucess = false;
            }

            if (i >= Connection.Ports.Length && i < 0)
            {
                LogInfo("O software tentou acessar um indice de porta inexistente: Número de portas - " + Connection.Ports.Length.ToString() + " Índice - " + i.ToString());
                Sucess = false;
            }

            return Sucess;
        }


        public static bool OpenConnection(SerialPort Port)
        {
            bool result = false;
            if (Port != null)
            {
                try
                {
                    if (Port.IsOpen)
                    {
                        Port.Close();
                        LogInfo("Fechando Porta " + Port.PortName + " pois ja esta aberta");
                    }
                    if (!Port.IsOpen)
                    {
                        Port.Open();
                        Port.ReadTimeout = 20;  //melhorar
                        //Port.WriteTimeout = 15;
                        if (Port.IsOpen)
                        {
                            //LogInfo("Porta " + Port.PortName + " Conectada com sucesso");
                            result = true;
                        }
                    }
                }
                catch
                {
                    LogInfo("Porta " + Port.PortName + " Ocupada");
                    result = false;
                }
            }
            else
            {
                LogInfo("Tentativa de conexão a porta " + Port.PortName + " que é nula");
                result = false;
            }
            return result;
        }

        public static int COMPortNumber(string COMPortName)
        {
            int num = COMPortName.IndexOf("M");
            string s = string.Empty;
            s = COMPortName.Substring(num + 1, COMPortName.Length - 1 - num);
            return int.Parse(s);
        }

        //Fechar comunicação serial de forma adequada
        public static void CloseSerial(ProcessDetails ThisProcess)
        {
            if (ThisProcess.CableConnection.ConnectionPort.IsOpen)
            {
                ThisProcess.CableConnection.ConnectionPort.WriteLine("ConnectionOFF"); //handshake
                System.Threading.Thread.Sleep(200);
                ThisProcess.CableConnection.ConnectionPort.Close();
                ThisProcess.CableConnection.Connected = false;
            }
        }

        //Inicia a assistente virtual
        public static IEnumerator StartVirtualAssistant(VirtualAssistant Assistant)
        {
            yield return new WaitForSeconds(0.2f);
            string Eva = Application.dataPath + "/Eva/Eva.exe";
            int EvasProcs = System.Diagnostics.Process.GetProcessesByName("Eva").Length;
            Assistant.ClickRect.height = Screen.height / 11;
            Assistant.ClickRect.width = Screen.width / 20;
            yield return new WaitForSeconds(0.2f);
            if (File.Exists(Eva) && EvasProcs == 0)
            {
                UnityEngine.Debug.Log("Eva existe");
                Assistant.SpeechRecognitionProcess = new Process();
                Assistant.SpeechRecognitionProcess.StartInfo.FileName = Eva;
                Assistant.SpeechRecognitionProcess.StartInfo.UseShellExecute = false;
                //Proc.StartInfo.Arguments = "-silent";
                Assistant.SpeechRecognitionProcess.StartInfo.CreateNoWindow = true;
                Assistant.SpeechRecognitionProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //evita de mostrar a janela de prompt
                Assistant.SpeechRecognitionProcess.Start();
                Assistant.SpeechRecognitionProcessActive = true;
            }
            if (!Assistant.SpeechRecognitionProcessActive && EvasProcs == 0)
            {
                //GetComponent<Renderer>().material.SetFloat("_EvaOut", 1f);
                LogInfo("O reconhecimento de voz está indisponível!");
            }
            yield return new WaitForSeconds(0.1f);
        }

        //Parte visual de interação com a assistente
        public static void AssistantGUI(VirtualAssistant Assistant, ProcessDetails ThisProcess)
        {
            //Desenho de EVA
            if (MouseOverRect(Assistant.ClickRect))
            {
                if (!Assistant.MouseOver) { 
                    Assistant.MouseEnter = true;
                }

                Assistant.MouseOver = true;

                if (Input.GetMouseButtonDown(0))
                {
                    Assistant.MouseClick = true;
                }
            }
            else
            {
                Assistant.MouseOver = false;
            }
            //Informações locais
            rectDraw = new Rect(Assistant.ClickRect.width + Padding, Screen.height / 65, Screen.width/10, Screen.height / 15);
            GUI.DrawTexture(rectDraw, Gray16Texture);
            rectDraw.width = Padding/3;
            GUI.DrawTexture(rectDraw, WhiteTexture);
            rectDraw.x = rectDraw.x + Screen.width / 10 - Padding/3;
            GUI.DrawTexture(rectDraw, WhiteTexture);
            GUI.skin.label.fontSize = GUI.skin.label.fontSize + Padding / 4;
            rectDraw = new Rect(Assistant.ClickRect.width + 2 * Padding, Screen.height / 61, Screen.width / 10 - 2 * Padding, Screen.height / 30);
            GUI.Label(rectDraw, ThisProcess.Name);
            GUI.skin.label.fontSize = GUI.skin.label.fontSize - Padding / 2;
            rectDraw = new Rect(Assistant.ClickRect.width + 2 * Padding, Screen.height / 61 + Screen.height / 40, Screen.width / 10 - 2 * Padding, Screen.height / 30);
            GUI.Label(rectDraw, ThisProcess.TimeHour);
            rectDraw = new Rect(Assistant.ClickRect.width + 2 * Padding, Screen.height / 61 + Screen.height / 24, Screen.width / 10 - 2 * Padding, Screen.height / 30);
            if (ThisProcess.LocalData.LocationCity != "" && ThisProcess.LocalData.TempActual != "")
            {
                GUI.Label(rectDraw, ThisProcess.LocalData.LocationCity + " " + ThisProcess.LocalData.TempActual + "°C");
            }

            GUI.skin.label.fontSize = GUI.skin.label.fontSize + Padding / 4;
        }

        //Mantém a assistente virtual em funcionamento
        public static IEnumerator UpdateVirtualAssistant(ProcessDetails ThisProcess,VirtualAssistant Assistant)
        {
            while (true)
            {
                Assistant.DangerLevel = 1;
                if (Time.time >= Assistant.UpdateTimeIntervalMark-0.05f)
                {

                    for (int i = 0; i < ThisProcess.Sensors.Length; i++)
                    {
                        if (ThisProcess.Sensors[i].SensorStatus < Assistant.DangerLevel)
                        {
                            Assistant.DangerLevel = ThisProcess.Sensors[i].SensorStatus;
                        }
                    }
                    for (int i = 0; i < ThisProcess.Atuators.Length; i++)
                    {
                        if (ThisProcess.Atuators[i].AtuadorStatus < Assistant.DangerLevel)
                        {
                            Assistant.DangerLevel = ThisProcess.Atuators[i].AtuadorStatus;
                        }
                    }

                    if (!ThisProcess.CableConnection.Connected && Assistant.DangerLevel == 1) Assistant.DangerLevel = 0.5f; //Conexão do processo Off
                    if (!ThisProcess.WebConnection.Connected && Assistant.DangerLevel == 1) Assistant.DangerLevel = 0.5f; //Conexão da internet Off
                    if (ThisProcess.DeviceBattery.BatteryLevel > ThisProcess.DeviceBattery.CriticalValue && ThisProcess.DeviceBattery.BatteryLevel <= ThisProcess.DeviceBattery.LowValue && Assistant.DangerLevel == 1) Assistant.DangerLevel = 0.5f; //Bateria esgotando
                    if (ThisProcess.DeviceBattery.BatteryLevel <= ThisProcess.DeviceBattery.CriticalValue && ThisProcess.DeviceBattery.BatteryLevel > -0.1f && Assistant.DangerLevel >= 0.5f) Assistant.DangerLevel = 0f; //Bateria critica

                    Assistant.UpdateTimeIntervalMark += Assistant.UpdateTimeInterval;
                    
                    if (Assistant.DangerLevel == 1f)
                        Assistant.AssistantMainObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(0f, 0.5f, 1f, 1f));
                    if (Assistant.DangerLevel > 0f && Assistant.DangerLevel < 1f)
                        Assistant.AssistantMainObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                    if (Assistant.DangerLevel == 0f)
                        Assistant.AssistantMainObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

                    Assistant.ClickRect.height = Screen.height / 11;
                    Assistant.ClickRect.width = Screen.width / 21;

                    if (Time.time >= Assistant.InitialTimeOfCheck)//Checagem inicial
                    {
                        LogInfo("Relatório informado");
                        if (Assistant.FirstCheckDone && Assistant.DangerLevel != 1f) { Speak("Estou checando o processo, e " + GeneralReport(Assistant, ThisProcess)); }
                        if (!Assistant.FirstCheckDone) { 
                            Speak("Fiz a checagem inicial, e " + GeneralReport(Assistant,ThisProcess));
                            Assistant.FirstCheckDone = true;
                        }
                        Assistant.InitialTimeOfCheck += Assistant.InitialTimeOfCheck;
                    }
                }

                if (Assistant.MouseEnter)
                {
                    //Detecta quando o mouse entrou na area sobre o icone de Eva
                    Assistant.MouseEnter = false;
                    if (!Assistant.FirstPresentation)
                    {
                        Speak("Olá, eu me chamo Eva, sou sua assistente de supervisão, e estou a disposição, se desejar um relatório geral clique aqui.");
                        Assistant.FirstPresentation = true;
                    }
                    else
                    {
                        Speak("Se desejar um relatório geral clique aqui.");
                    }
                }

                if (Assistant.MouseClick)
                {
                    //Detecta quando o mouse clicou na area sobre o icone de Eva
                    Assistant.MouseClick = false;
                    LogInfo("Pedido de relatório");
                    Speak(GeneralReport(Assistant,ThisProcess));
                }

                yield return new WaitForSeconds(0.3f);
                Assistant.ClickRect.height = Screen.height / 11;
                Assistant.ClickRect.width = Screen.width / 21;
                yield return new WaitForSeconds(0.3f);
            }
        }

        //Relatório geral
        public static string GeneralReport(VirtualAssistant Assistant, ProcessDetails ThisProcess)
        {
            string TempText = "";
            int AtuatorsProblems = 0;
            int AtuatorsProblemsCritical = 0;
            int SensorsProblems = 0;
            int SensorsProblemsCritical = 0;

            //Verifica a estabilidade da conexão via cabo
            if (ThisProcess.CableConnection.Restarts > 7 || ThisProcess.CableConnection.Reconnections > 7)
            {
                if (ThisProcess.CableConnection.Restarts > 7 && ThisProcess.CableConnection.Reconnections < 7)
                {
                    TempText += " O processo se re iniciou " + ThisProcess.CableConnection.Restarts.ToString() + " vezes, a conexão via cabo está instável. ";
                }
                if (ThisProcess.CableConnection.Restarts < 7 && ThisProcess.CableConnection.Reconnections > 7)
                {
                    TempText += " O processo se re conectou " + ThisProcess.CableConnection.Reconnections.ToString() + " vezes, a conexão via cabo está instável. ";
                }
                if (ThisProcess.CableConnection.Restarts > 7 && ThisProcess.CableConnection.Reconnections > 7)
                {
                    TempText += " O processo se re conectou " + ThisProcess.CableConnection.Reconnections.ToString() + " vezes, e re iniciou" + ThisProcess.CableConnection.Restarts.ToString() + " vezes, a conexão via cabo está instável. ";
                }
            }

            if (ThisProcess.Connected)
            {
                for (int i = 0; i < ThisProcess.Atuators.Length; i++)//Para cada atuador
                {
                    if (ThisProcess.Atuators[i].AtuadorStatus < 1)
                    {
                        if (ThisProcess.Atuators[i].AtuadorStatus < 0.5) { AtuatorsProblemsCritical += 1; }
                        AtuatorsProblems += 1;
                    }
                }

                if (AtuatorsProblems == 1)
                {
                    if (TempText != "") { TempText += " E também "; }
                    TempText += " Um dos atuadores apresenta problema. ";
                    if (AtuatorsProblemsCritical == 1)
                    {
                        TempText += " E é um problema crítico, clique no atuador em vermelho para mais detalhes. ";
                    }
                }
                if (AtuatorsProblems > 1)
                {
                    if (TempText != "") { TempText += " E também "; }
                    TempText += AtuatorsProblems.ToString() + " atuadores apresentam problemas. ";
                    if (AtuatorsProblemsCritical == 1)
                    {
                        TempText += " Um deles tem um problema crítico, clique no atuador em vermelho para mais detalhes. ";
                    }
                    if (AtuatorsProblemsCritical > 1)
                    {
                        TempText += AtuatorsProblemsCritical.ToString() + " deles tem problemas críticos, clique nos atuadores em vermelho para mais detalhes. ";
                    }
                }

                for (int i = 0; i < ThisProcess.Sensors.Length; i++)//Para cada sensor
                {
                    if (ThisProcess.Sensors[i].SensorStatus < 1)
                    {
                        if (ThisProcess.Sensors[i].SensorStatus < 0.5) { SensorsProblemsCritical += 1; }
                        SensorsProblems += 1;
                    }
                }
                if (SensorsProblems == 1)
                {
                    if (TempText != "") { TempText += " Além disso "; }
                    TempText += " Um dos sensores apresenta problema. ";
                    if (SensorsProblemsCritical == 1)
                    {
                        TempText += " Este é um problema crítico, clique no sensor em vermelho para mais detalhes. ";
                    }
                }
                if (SensorsProblems > 1)
                {
                    if (TempText != "") { TempText += " Além disso "; }
                    TempText += SensorsProblems.ToString() + " sensores apresentam problemas. ";
                    if (SensorsProblemsCritical == 1)
                    {
                        TempText += " Um deles apresenta um problema crítico, clique no sensor em vermelho para mais detalhes. ";
                    }
                    if (SensorsProblemsCritical > 1)
                    {
                        TempText += SensorsProblemsCritical.ToString() + " deles tem problemas críticos, clique nos sensores em vermelho para mais detalhes. ";
                    }
                }
            }
            if (!ThisProcess.Connected)
            {
                if (TempText != "") { TempText += " e também "; }
                TempText += " O processo ainda não está conectado com este dispositivo. nem via cabo, nem rede local, nem via internet. verifique a conexão ";  //Conexão do processo Off
            }
            if (!ThisProcess.WebConnection.Connected)
            {
                if (TempText != "") { TempText += " e "; }
                TempText += " não estou conectada com a internet ";  //Conexão da internet Off
            }
            if (!ThisProcess.DeviceBattery.ChargerConnected)
            {
                if (ThisProcess.DeviceBattery.BatteryLevel > ThisProcess.DeviceBattery.CriticalValue && ThisProcess.DeviceBattery.BatteryLevel <= ThisProcess.DeviceBattery.LowValue) TempText += " a bateria está se esgotando, conecte-se a um carregador. ";  //Bateria esgotando
                if (ThisProcess.DeviceBattery.BatteryLevel <= ThisProcess.DeviceBattery.CriticalValue && ThisProcess.DeviceBattery.BatteryLevel > -0.1f) TempText += " a bateria atingiu um nível crítico, conecte-se urgentemente a um carregador. "; //Bateria critica
            }
            //Caso não haja problemas
            if (TempText == "" && AtuatorsProblems == 0 && AtuatorsProblemsCritical == 0 && SensorsProblems == 0 && SensorsProblemsCritical == 0)
            {
                TempText = " nenhum problema foi observado, o sistema está funcionando normalmente ";
            }

            //UnityEngine.Debug.Log(TempText); //debug
            return TempText;
        }

        //Relatório de um sensor
        public static string ReportSensor(Sensor PointedSensor)
        {
            string TempText = "";
            TempText = "O " + PointedSensor.Name + " " + PointedSensor.Number + " do " + PointedSensor.Description;
            if (PointedSensor.SensorStatus == 1 && !PointedSensor.Virtual && !PointedSensor.FirstReceive) { TempText += " mede no momento uma " + PointedSensor.SensorClass + " de " + PointedSensor.ActualValue.ToString("G4") + " " + PointedSensor.SpokeUnit + ". E Está funcionando normalmente "; }
            if (PointedSensor.SensorStatus == 1 && PointedSensor.Virtual && !PointedSensor.FirstReceive) { TempText += " mede no momento uma " + PointedSensor.SensorClass + " de " + PointedSensor.ActualValue.ToString("G4") + " " + PointedSensor.SpokeUnit + ". E Está funcionando normalmente. Lembrando que este é um sensor virtual."; }
            if (PointedSensor.SensorStatus < 1 && !PointedSensor.Virtual) { TempText += " Apresenta problemas... "; }
            if (PointedSensor.SensorStatus < 1 && PointedSensor.Virtual) { TempText += ". É um sensor virtual, e Apresenta problemas... "; }
            if (PointedSensor.FirstReceive) { TempText += " Nenhum dado foi recebido dêste sensor até o momento. "; }
            float RiskMinimum = PointedSensor.MinSecureValue + (PointedSensor.SecurityMargin / 100f) * (PointedSensor.MaxSecureValue - PointedSensor.MinSecureValue);
            float RiskMaximum = PointedSensor.MaxSecureValue - (PointedSensor.SecurityMargin / 100f) * (PointedSensor.MaxSecureValue - PointedSensor.MinSecureValue);
            if (PointedSensor.MinValue <= PointedSensor.MinSecureValue) { TempText += "A " + PointedSensor.SensorClass + " atingiu um valor inferior ao permitido a algum tempo atrás..."; }
            if (PointedSensor.MaxValue >= PointedSensor.MaxSecureValue) { TempText += "A " + PointedSensor.SensorClass + " atingiu um valor superior ao permitido a algum tempo atrás..."; }
            if (PointedSensor.ActualValue <= RiskMinimum || PointedSensor.ActualValue >= RiskMaximum) { TempText += "A " + PointedSensor.SensorClass + " está próxima do limite permitido..."; }
            if (PointedSensor.TimeSinceLastUpdate >= TimeSinceLastUpdateFail && PointedSensor.TimeSinceLastUpdate < TimeSinceLastUpdateCritical) { TempText += "O sensor não está respondendo, por favor, verifique as conexões elétricas..."; }
            if (PointedSensor.TimeSinceLastUpdate >= TimeSinceLastUpdateCritical) { TempText += "O sensor não responde a um longo tempo, por favor, verifique as conexões elétricas..."; }
            if (PointedSensor.MinValue >= PointedSensor.MaxSecureValue) { TempText += "A " + PointedSensor.SensorClass + " mínima está acima do valor máximo permitido, o sistema está fora de controle... sugiro desligar e reiniciar o processo. "; }
            if (PointedSensor.MaxValue <= PointedSensor.MinSecureValue) { TempText += "A " + PointedSensor.SensorClass + " máxima está abaixo do valor mínimo permitido, o sistema está fora de controle... sugiro desligar e reiniciar o processo. "; }
            if (PointedSensor.ActualValue <= PointedSensor.MinSecureValue || PointedSensor.ActualValue >= PointedSensor.MaxSecureValue) { TempText += "O sensor está fora dos limites permitidos, por favor, verifique a situação..."; }
            if (PointedSensor.MeanValue <= PointedSensor.MinSecureValue || PointedSensor.MeanValue >= PointedSensor.MaxSecureValue) { TempText += "O sensor está a um longo tempo fora dos limites permitidos, por favor, verifique a situação..."; }
            return TempText;
        }

        //Gerencia atualizações dos PIDs
        public static IEnumerator ManagePIDs(ProcessDetails ThisProcess)
        {
            while (true)
            {
                //Enviar as informações para setar um PID
                for (int i = 0; i < ThisProcess.PIDs.Length; i++)
                {
                    if (ThisProcess.PIDs[i].ApplyButton.Selected)
                    {
                        //ThisProcess.PIDs[i].InfoToSendToPLC = true;
                        ThisProcess.PIDs[i].ApplyButton.Selected = false;
                        //ThisProcess.PIDs[i].ApplyButton.Toggle = false;
                    }
                }
                yield return new WaitForSeconds(0.51f);
            }
        }

        //Gerencia atualizações de sensores e atuadores
        public static IEnumerator ManageActuatorAndSensorsUpdates(ProcessDetails ThisProcess)
        {
            float TimeSaveResults = Time.time;
            float Time10min = Time.time;
            float Time1hour = Time.time;
            float Time10hours = Time.time;
            float Time10days = Time.time;
            float Delay = 0.1f;
            float FinalValue = 0f;
            float AntFinalValue = 0f;

            while (true)
            {

                //salvar resultados, salvar em mais lugares depois
                if (Time.time >= TimeSaveResults - Delay / 2f)
                {
                    LogResult("SaveInfo", ThisProcess);
                    TimeSaveResults += SaveResultsOnFile;
                }

                //atualiza valores de historico, máximo, mínimo e tempos desde o recebimento
                for (int i = 0; i < ThisProcess.Atuators.Length; i++)//Tempo desde o ultimo recebimento
                {
                    UpdateActuatorsInfo(ThisProcess.Atuators[i]);
                }
                //Gera o alerta em caso de ultrapassar os limites
                for (int i = 0; i < ThisProcess.Sensors.Length; i++)//Tempo desde o ultimo recebimento
                {
                    if (!ThisProcess.Sensors[i].AlertPlayed)
                    {
                        if (ThisProcess.Sensors[i].MaxSecValueAlert)
                        {
                            if (ThisProcess.Sensors[i].ActualValue >= ThisProcess.Sensors[i].MaxSecureValue)
                            {
                                if (ThisProcess.Sensors[i].MaxSecValueCritical)
                                {
                                    PlaySound(CoreObject, 3, 2f);
                                    ThisProcess.Sensors[i].AlertPlayed = true;
                                    Speak("O sensor " + ThisProcess.Sensors[i].Number.ToString() + " atingiu o estado crítico, providências precisam ser tomadas");
                                }
                                else
                                {
                                    PlaySound(CoreObject, 2, 1f);
                                    ThisProcess.Sensors[i].AlertPlayed = true;
                                    Speak("O sensor " + ThisProcess.Sensors[i].Number.ToString() + " ultrapassou o limite permitido");
                                }
                            }
                        }
                        if (ThisProcess.Sensors[i].MinSecValueAlert)
                        {
                            if (ThisProcess.Sensors[i].ActualValue <= ThisProcess.Sensors[i].MinSecureValue)
                            {
                                if (ThisProcess.Sensors[i].MinSecValueCritical)
                                {
                                    PlaySound(CoreObject, 3, 2f);
                                    ThisProcess.Sensors[i].AlertPlayed = true;
                                    Speak("O sensor " + ThisProcess.Sensors[i].Number.ToString() + " atingiu o estado crítico, providências precisam ser tomadas");
                                }
                                else
                                {
                                    PlaySound(CoreObject, 2, 1f);
                                    ThisProcess.Sensors[i].AlertPlayed = true;
                                    Speak("O sensor " + ThisProcess.Sensors[i].Number.ToString() + " está abaixo do limite permitido");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (ThisProcess.Sensors[i].ActualValue > ThisProcess.Sensors[i].MinSecureValue && ThisProcess.Sensors[i].ActualValue < ThisProcess.Sensors[i].MaxSecureValue)
                        {
                            ThisProcess.Sensors[i].AlertPlayed = false;
                        }
                    }
                }

                //atualiza valores de historico e tempos desde o recebimento
                for (int i = 0; i < ThisProcess.Sensors.Length; i++)//Tempo desde o ultimo recebimento
                {

                    ThisProcess.Sensors[i].TimeSinceLastUpdate = Time.time - ThisProcess.Sensors[i].TimeLastUpdateMark;
                    //Atualiza status dos sensores
                    ThisProcess.Sensors[i].SensorStatus = Status(ThisProcess.Sensors[i]);  //Quando o sensor recebe uma nova informação
                }

                if (Time.time >= Time10days - Delay)        //200 pontos dentro dos 10 dias
                {
                    UpdateHistory(ThisProcess.Atuators, 3);
                    yield return new WaitForSeconds(Delay / 8f);
                    UpdateHistory(ThisProcess.Sensors, 3);
                    Time10days += 432f;
                }
                yield return new WaitForSeconds(Delay / 8f);

                if (Time.time >= Time10hours - Delay)       //200 pontos dentro das 10 horas
                {
                    UpdateHistory(ThisProcess.Atuators, 2);
                    yield return new WaitForSeconds(Delay / 8f);
                    UpdateHistory(ThisProcess.Sensors, 2);
                    Time10hours += 180f;
                }
                yield return new WaitForSeconds(Delay / 8f);

                if (Time.time >= Time1hour - Delay / 2f)        //200 pontos dentro de 1 hora
                {
                    UpdateHistory(ThisProcess.Atuators, 1);
                    yield return new WaitForSeconds(Delay / 8f);
                    UpdateHistory(ThisProcess.Sensors, 1);
                    Time1hour += 18f;
                }
                yield return new WaitForSeconds(Delay / 8f);

                if (Time.time >= Time10min - Delay / 2f)         //200 pontos dentro dos 10 minutos
                {
                    UpdateHistory(ThisProcess.Sensors, 0);
                    yield return new WaitForSeconds(Delay / 8f);
                    UpdateHistory(ThisProcess.Atuators, 0);
                    UpdateCharts(ThisProcess.Sensors, ThisProcess.Atuators);
                    for (int i = 0; i < ThisProcess.Sensors.Length; i++)//Atualiza as taxas
                    {
                        FinalValue = ThisProcess.Sensors[i].History[0, ThisProcess.Sensors[i].History.GetLength(1) - 1].Values;
                        AntFinalValue = ThisProcess.Sensors[i].History[0, ThisProcess.Sensors[i].History.GetLength(1) - 2].Values;

                        ThisProcess.Sensors[i].Rate = (FinalValue - AntFinalValue) / 3f; //melhorar
                    }

                    Time10min += 3f;//melhorar e alterar a linha de cima
                }
                yield return new WaitForSeconds(Delay);
            }

        }

        //Envia dados e comandos para o arduino
        public static IEnumerator ManageSentInfo(ProcessDetails ThisProcess)
        {
            //Formato de mensagem para o atuador:
            //P01A02V0128 876LF
            //P - PLC, 01 - Numero do PLC/Arduino, A - Atuador, 02 - Numero do atuador, começa em 0
            //V - Valor do atuador, 0128 - Valor do atuador [0-HardwareMaxBits], 879 - CheckSum/Hash do inicio até o fim do valor, LF - Fim da mensagem

            //Formato de mensagem para definir o ciclo de um atuador:
            //P01A02C0050 176LF
            //P - PLC, 01 - Numero do PLC/Arduino, A - Atuador, 02 - Numero do atuador, começa em 0
            //C - definição do Ciclo, 0050 - Valor do ciclo [0-HardwareMaxBits], 179 - CheckSum/Hash do inicio até o fim do valor, LF - Fim da mensagem

            string Message = "";
            string COMOutput = ThisProcess.CableConnection.COMOutput; //Saida da porta COM proveniente da conexão
            float TimeToSend = Time.time;

            while (true)
            {
                for (int i = 0; i < ThisProcess.Atuators.Length; i++) //Para todos os atuadores
                {
                    if (ThisProcess.CableConnection.Connected || ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer)//Só gerencia se houver conexão
                    {
                        if (ThisProcess.Atuators[i].AtuadorUI.IntensitySlider.Changed)//verifica se o valor foi alterado em habilita o envio do comando para o arduino
                        {
                            ThisProcess.Atuators[i].ValueToSend = true;
                            ThisProcess.Atuators[i].CicleToSend = true;
                            ThisProcess.Atuators[i].AtuadorUI.IntensitySlider.Changed = false;
                            ThisProcess.Atuators[i].TimeLastUpdateMark = Time.time;
                            ModificationDone(ThisProcess.Sensors, ThisProcess.Atuators, i);                    //Atualiza a matriz de modificação
                        }
                        if (ThisProcess.Atuators[i].ValueToSend)//Se estiver habilitado para enviar
                        {
                            Message = "P" + ThisProcess.CableConnection.PLCNumber.ToString("D2") + "A" + i.ToString("D2") + "V" + RemapToMaxBits(ThisProcess.Atuators[i].ActualValue).ToString("D4");
                            Message = Message + " " + hash(Message).ToString("D3") + "LF";
                            if (ThisProcess.CableConnection.Connected) ThisProcess.CableConnection.ConnectionPort.WriteLine(Message);
                            if (ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer) writeSocket(ThisProcess.LocalWifiProcessConnection, Message+"\n");
                            COMString += "\n--> " + Message;           //melhorar
                            if (ScrollDebugString) COMDebugScrollPosition += new Vector2(0, 25000);    //melhorar
                            //UnityEngine.Debug.Log(Message); //debug
                            ThisProcess.Atuators[i].ValueToSend = false;
                            ThisProcess.Atuators[i].ReqNetUpdate = true;  //enviar via internet
                            yield return new WaitForSeconds(0.5f);
                            if (ThisProcess.CableConnection.Connected) ThisProcess.CableConnection.ConnectionPort.WriteLine(Message);
                            if (ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer) writeSocket(ThisProcess.LocalWifiProcessConnection, Message + "\n");
                            yield return new WaitForSeconds(0.5f);
                        }
                        if (ThisProcess.Atuators[i].CicleToSend)
                        {
                            Message = "P" + ThisProcess.CableConnection.PLCNumber.ToString("D2") + "A" + i.ToString("D2") + "C" + ((int)ThisProcess.Atuators[i].OnOffCicle).ToString("D4");
                            Message = Message + " " + hash(Message).ToString("D3") + "LF";
                            if (ThisProcess.CableConnection.Connected) ThisProcess.CableConnection.ConnectionPort.WriteLine(Message);
                            if (ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer) writeSocket(ThisProcess.LocalWifiProcessConnection, Message + "\n");
                            //UnityEngine.Debug.Log(Message); //debug
                            COMString += "\n--> " + Message;           //melhorar
                            if (ScrollDebugString) COMDebugScrollPosition += new Vector2(0, 25000);    //melhorar
                            ThisProcess.Atuators[i].CicleToSend = false;
                            ThisProcess.Atuators[i].ReqNetUpdate = true; //enviar via internet
                            yield return new WaitForSeconds(0.5f);
                            if (ThisProcess.CableConnection.Connected) ThisProcess.CableConnection.ConnectionPort.WriteLine(Message);
                            if (ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer) writeSocket(ThisProcess.LocalWifiProcessConnection, Message + "\n");
                            yield return new WaitForSeconds(0.5f);
                        }
                    }
                    else
                    {
                        if (ThisProcess.Atuators[i].AtuadorUI.IntensitySlider.Changed)
                        {
                            ThisProcess.Atuators[i].AtuadorUI.IntensitySlider.Changed = false;
                            //TODO o que fazer quando não há nenhum tipo de conexão?
                        }
                    }
                }
                //Enviar as informações para setar um PID
                for (int i = 0; i < ThisProcess.PIDs.Length; i++)
                {
                    if (ThisProcess.PIDs[i].InfoToSendToPLC) //Se a flag foi setada
                    {   //Qual sensor o PID irá utilizar
                        SendMessage("P", i, "S", ThisProcess.PIDs[i].SensorNum, ThisProcess);
                        yield return new WaitForSeconds(0.5f);
                        SendMessage("P", i, "S", ThisProcess.PIDs[i].SensorNum, ThisProcess);
                        yield return new WaitForSeconds(1.0f);
                        //Qual atuador o PID irá utilizar
                        SendMessage("P", i, "C", ThisProcess.PIDs[i].ActuatorNum, ThisProcess);
                        yield return new WaitForSeconds(0.5f);
                        SendMessage("P", i, "C", ThisProcess.PIDs[i].ActuatorNum, ThisProcess);
                        yield return new WaitForSeconds(1.0f);

                        ThisProcess.PIDs[i].InfoToSendToPLC = false; //Desabilita a flag
                        ThisProcess.PIDs[i].ReqNetUpdate = true;  //enviar via internet
                    }
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        //Construir uma mensagem de envio e enviar
        public static void SendMessage(string Letter1, int Number1, string Letter2, int Number2, ProcessDetails ThisProcess)
        {
            string Message = "";
            if (ThisProcess.CableConnection.Connected || ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer)//Só envia se houver conexão
            {
                Message = "P" + ThisProcess.CableConnection.PLCNumber.ToString("D2") + Letter1 + Number1.ToString("D2") + Letter2 + Number2.ToString("D4");
                Message = Message + " " + hash(Message).ToString("D3") + "LF";
                if (ThisProcess.CableConnection.Connected) ThisProcess.CableConnection.ConnectionPort.WriteLine(Message);
                if (ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer) writeSocket(ThisProcess.LocalWifiProcessConnection, Message + "\n");
                COMString += "\n--> " + Message;           //melhorar
                if (ScrollDebugString) COMDebugScrollPosition += new Vector2(0, 25000);    //melhorar
            }
        }

        //Converte um valor de [0-100] -> [0-HarwareMaxBits]
        public static int RemapToMaxBits(float Input)
        {
            return Mathf.RoundToInt(Input * ((float)HardwareMaxbits) / 100f);
        }

        //Distribui e atualiza dados recebidos do arduino
        public static IEnumerator ManageReceptedInfo(ProcessDetails ThisProcess)
        {
            //Formato de mensagem de sensor:
            //P01S00V -933.3313 876LF
            //P - PLC, 01 - Numero do PLC/Arduino, S - Sensor, 00 - Numero do sensor, começa em 0
            //V - Valor do Sensor, -933.3313 - Valor do sensor, 879 - CheckSum/Hash do inicio até o fim do valor (espaço em branco), LF - Fim da mensagem

            //Formato de mensagem de status do atuador:
            //P01A02SLOW 938LF
            //P - PLC, 01 - Numero do PLC/Arduino, A - Atuador, 02 - Numero do atuador, começa em 0
            //S - Código do status do atuador, LOW - Status do atuador, 938 - CheckSum/Hash do inicio até o espaço em branco, LF - Fim da mensagem

            //Formato de mensagem de Intensidade do atuador:
            //P01A00I0462 542LF
            //P - PLC, 01 - Numero do PLC/Arduino, A - Atuador, 00 - Numero do atuador, começa em 0
            //I - Código de intensidade do atuador, 0462 - Intensidade do atuador [0-HarwareMaxBits], 542 - CheckSum/Hash do inicio até o espaço em branco, LF - Fim da mensagem

            //Formato de mensagem do termo P de um PID:
            //P01P00P -0.531333 876LF
            //P - PLC, 01 - Numero do PLC/Arduino, P - PID, 00 - Numero do PID, começa em 0
            //P - Código do termo proporcional, -0.531333 - Valor de P, 876 - CheckSum/Hash do inicio até o espaço em branco, LF - Fim da mensagem


            bool ValueConversionFail = false;
            string EndMessage = "";                     //Fim da mensagem
            string COMOutput = ThisProcess.CableConnection.COMOutput;    //Saida da porta COM proveniente da conexão
            int HardwareNumber = 0;                     //Numero do hardware
            int SensorNumber = 0;                       //Numero do sensor
            int ActuatorNumber = 0;                     //Numero do atuador
            int PIDNumber = 0;                          //Número do PID
            string Message = "";                        //mensagem
            float Value = 0f;                           //Valor do sensor
            float Intensity = 0f;                       //Intensidade de um atuador  [0-255] (Arduino) [0-1023] (Esp8266)
            float Cicle = 0f;                           //Valor do ciclo de um atuador [0-255] (Arduino) [0-1023] (Esp8266)
            int CheckSum = 0;                           //Soma de checagem, verifica a integridade da mensagem

            while (true)
            {
                if (ThisProcess.CableConnection.Connected || ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer)//Só gerencia se houver conexão
                {
                    if (ThisProcess.CableConnection.Connected) COMOutput = ThisProcess.CableConnection.COMOutput; //Resultado da porta COM
                    if (ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer) COMOutput = ThisProcess.LocalWifiProcessConnection.SocketRead; //Resultado da porta COM
                    if (COMOutput.Length > 12) //Verifica se está recebendo uma mensagem de um sensor
                    {
                        EndMessage = COMOutput.Substring(COMOutput.Length - 2, 2);
                        if (COMOutput[0] == 'P' && COMOutput[3] == 'S' && COMOutput.IndexOf(" ") > 5 && EndMessage == "LF") //Garantias de que é uma mensagem típica de um sensor
                        {
                            //Divide a mensagem em seus respectivos trechos
                            try { HardwareNumber = int.Parse(COMOutput.Substring(1, 2)); }
                            catch { ValueConversionFail = true; }
                            ThisProcess.CableConnection.PLCNumber = HardwareNumber; //Melhorar não é uma boa ideia esperar dessa forma
                            try { SensorNumber = int.Parse(COMOutput.Substring(4, 2)); }
                            catch { ValueConversionFail = true; }
                            try { Value = float.Parse(COMOutput.Substring(7, COMOutput.Length - 13), CultureInfo.InvariantCulture); }  //Já houve erros aqui...usar try
                            catch { ValueConversionFail = true; }
                            try{CheckSum = int.Parse(COMOutput.Substring(COMOutput.Length - 5, 3));}
                            catch {ThisProcess.CableConnection.CheckSumFails += 1; }
                            if (hash(COMOutput.Substring(0, COMOutput.Length - 6)) == CheckSum && !ValueConversionFail) //Checa integridade
                            {
                                if (COMOutput[6] == 'V') //Valor
                                {
                                    if (SensorNumber < ThisProcess.Sensors.Length)
                                    {
                                        ThisProcess.Sensors[SensorNumber].TimeLastUpdateMark = Time.time;

                                        if (ThisProcess.Sensors[SensorNumber].TimeSinceLastUpdate != 0f)
                                        {
                                            if (ThisProcess.RemoteProcessConnection.RPCHost) ThisProcess.Sensors[SensorNumber].ReqNetUpdate = true;
                                            ThisProcess.Sensors[SensorNumber].SensorStatus = Status(ThisProcess.Sensors[SensorNumber]);
                                            ReceiveSensorValue(ThisProcess.Sensors, SensorNumber, Value);           //Distribui a informação
                                        }
                                    }
                                    else
                                    {
                                        LogInfo("O controlador está enviando informações de um sensor não cadastrado no software, Índice: " + ActuatorNumber.ToString());
                                    }
                                }

                                //Recebimento de outras Funções
                                //...
                            }
                            else
                            {
                                ThisProcess.CableConnection.CheckSumFails += 1;
                            }
                            ValueConversionFail = false;
                        }
                    }

                //Mensagem de um atuador
                    if (COMOutput.Length > 12) //Verifica se a mensagem tem o tamanho certo
                    {
                        EndMessage = COMOutput.Substring(COMOutput.Length - 2, 2);
                        if (COMOutput[0] == 'P' && COMOutput[3] == 'A' && EndMessage == "LF") //Garantias de que é uma mensagem típica de um atuador
                        {
                            //Mensagem de status do atuador. Divide a mensagem em seus respectivos trechos
                            if (COMOutput[6] == 'S')
                            {
                                try { HardwareNumber = int.Parse(COMOutput.Substring(1, 2)); }
                                catch { ValueConversionFail = true; }
                                ThisProcess.CableConnection.PLCNumber = HardwareNumber; //Melhorar não é uma boa ideia esperar dessa forma
                                try { ActuatorNumber = int.Parse(COMOutput.Substring(4, 2)); }
                                catch { ValueConversionFail = true; }
                                Message = COMOutput.Substring(7, COMOutput.Length - 13);
                                try { CheckSum = int.Parse(COMOutput.Substring(COMOutput.Length - 5, 3)); }
                                catch { ThisProcess.CableConnection.CheckSumFails += 1; }
                                if (hash(COMOutput.Substring(0, COMOutput.Length - 6)) == CheckSum && !ValueConversionFail) //Checa integridade
                                {
                                    if (ActuatorNumber < ThisProcess.Atuators.Length)
                                    {
                                        if (ThisProcess.RemoteProcessConnection.RPCHost) ThisProcess.Atuators[ActuatorNumber].ReqNetUpdate = true;
                                        if (Message == "LOW")
                                            ThisProcess.Atuators[ActuatorNumber].ActualStatus = false;
                                        if (Message == "HIGH")
                                            ThisProcess.Atuators[ActuatorNumber].ActualStatus = true;
                                        ThisProcess.Atuators[ActuatorNumber].TimeLastUpdateMark = Time.time;
                                        //UnityEngine.Debug.Log("Hash OK ");
                                    }
                                    else
                                    {
                                        LogInfo("O controlador está enviando informações de um atuador não cadastrado no software, Índice: " + ActuatorNumber.ToString());
                                    }
                                }
                                else
                                {
                                    ThisProcess.CableConnection.CheckSumFails += 1;
                                }
                            }

                            //Mensagem de Intensidade do atuador. Divide a mensagem em seus respectivos trechos
                            //if (COMOutput[6] == 'I' && !ThisProcess.LocalWifiProcessConnection.ConnectedLocalProcessServer)
                            if (COMOutput[6] == 'I')
                            {
                                try { HardwareNumber = int.Parse(COMOutput.Substring(1, 2)); }
                                catch { ValueConversionFail = true; }
                                ThisProcess.CableConnection.PLCNumber = HardwareNumber; //Melhorar não é uma boa ideia esperar dessa forma
                                try { ActuatorNumber = int.Parse(COMOutput.Substring(4, 2)); }
                                catch { ValueConversionFail = true; }
                                try { Intensity = int.Parse(COMOutput.Substring(7, COMOutput.Length - 13)); }
                                catch { ValueConversionFail = true; }
                                try { CheckSum = int.Parse(COMOutput.Substring(COMOutput.Length - 5, 3)); }
                                catch { ThisProcess.CableConnection.CheckSumFails += 1; }
                                if (hash(COMOutput.Substring(0, COMOutput.Length - 6)) == CheckSum && !ValueConversionFail) //Checa integridade
                                {
                                    if (ActuatorNumber < ThisProcess.Atuators.Length)
                                    {
                                        if (ThisProcess.RemoteProcessConnection.RPCHost) ThisProcess.Atuators[ActuatorNumber].ReqNetUpdate = true;
                                        ThisProcess.Atuators[ActuatorNumber].ActualValue = 100f * ((float)Intensity) / ((float)HardwareMaxbits);
                                        ThisProcess.Atuators[ActuatorNumber].AtuadorUI.IntensitySlider.Value = ThisProcess.Atuators[ActuatorNumber].ActualValue;
                                        ThisProcess.Atuators[ActuatorNumber].TimeLastUpdateMark = Time.time;
                                        ThisProcess.Atuators[ActuatorNumber].FirstReceive = false;
                                    }
                                    else
                                    {
                                        LogInfo("O controlador está enviando informações de um atuador não cadastrado no software, Índice: " + ActuatorNumber.ToString());
                                    }
                                    //UnityEngine.Debug.Log("Hash OK ");
                                }
                                else
                                {
                                    ThisProcess.CableConnection.CheckSumFails += 1;
                                }
                            }

                            //Mensagem de tempo de ciclo do atuador. Divide a mensagem em seus respectivos trechos
                            if (COMOutput[6] == 'C')
                            {
                                try { HardwareNumber = int.Parse(COMOutput.Substring(1, 2)); }
                                catch { ValueConversionFail = true; }
                                ThisProcess.CableConnection.PLCNumber = HardwareNumber; //Melhorar não é uma boa ideia esperar dessa forma
                                try { ActuatorNumber = int.Parse(COMOutput.Substring(4, 2)); }
                                catch { ValueConversionFail = true; }
                                try { Cicle = int.Parse(COMOutput.Substring(7, COMOutput.Length - 13)); }
                                catch { ValueConversionFail = true; }
                                try { CheckSum = int.Parse(COMOutput.Substring(COMOutput.Length - 5, 3)); }
                                catch { ThisProcess.CableConnection.CheckSumFails += 1; }
                                if (hash(COMOutput.Substring(0, COMOutput.Length - 6)) == CheckSum && !ValueConversionFail) //Checa integridade
                                {
                                    if (ActuatorNumber < ThisProcess.Atuators.Length)
                                    {
                                        if (ThisProcess.RemoteProcessConnection.RPCHost) ThisProcess.Atuators[ActuatorNumber].ReqNetUpdate = true;
                                        ThisProcess.Atuators[ActuatorNumber].OnOffCicle = (float)Cicle;
                                        ThisProcess.Atuators[ActuatorNumber].OnOffCicleString = Cicle.ToString();
                                        //UnityEngine.Debug.Log("Hash OK ");
                                    }
                                    else
                                    {
                                        LogInfo("O controlador está enviando informações de um atuador não cadastrado no software, Índice: " + ActuatorNumber.ToString());
                                    }
                                }
                                else
                                {
                                    ThisProcess.CableConnection.CheckSumFails += 1;
                                }
                            }

                            ValueConversionFail = false;
                        }
                    }
                    //verifica se é uma mensagem de um PID
                    if (COMOutput.Length > 12) //Verifica se está recebendo uma mensagem de um PID
                    {
                        EndMessage = COMOutput.Substring(COMOutput.Length - 2, 2);
                        if (COMOutput[0] == 'P' && COMOutput[3] == 'P' && COMOutput.IndexOf(" ") > 5 && EndMessage == "LF") //Garantias de que é uma mensagem típica de um sensor
                        {
                            //Divide a mensagem em seus respectivos trechos
                            try { HardwareNumber = int.Parse(COMOutput.Substring(1, 2)); }
                            catch { ValueConversionFail = true; }
                            ThisProcess.CableConnection.PLCNumber = HardwareNumber; //Melhorar não é uma boa ideia esperar dessa forma
                            try { PIDNumber = int.Parse(COMOutput.Substring(4, 2)); }
                            catch { ValueConversionFail = true; }
                            try { Value = float.Parse(COMOutput.Substring(7, COMOutput.Length - 13), CultureInfo.InvariantCulture); }
                            catch { ValueConversionFail = true; }
                            try { CheckSum = int.Parse(COMOutput.Substring(COMOutput.Length - 5, 3)); }
                            catch { ThisProcess.CableConnection.CheckSumFails += 1; }
                            if (hash(COMOutput.Substring(0, COMOutput.Length - 6)) == CheckSum && !ValueConversionFail) //Checa integridade
                            {
                                if (PIDNumber < ThisProcess.PIDs.Length)
                                {
                                    ThisProcess.PIDs[PIDNumber].Enabled = true;
                                    if (ThisProcess.RemoteProcessConnection.RPCHost) ThisProcess.PIDs[PIDNumber].ReqNetUpdate = true;
                                    //iniciais
                                    if (COMOutput[6] == 'S') //Sensor fonte
                                    {
                                        ThisProcess.PIDs[PIDNumber].SensorNum = (int)Value;
                                        ThisProcess.PIDs[PIDNumber].ActuatorUsedInCombobox.SelectedItem = (int)Value + 1;
                                        ThisProcess.Sensors[PIDNumber].PIDControled = true;
                                        ThisProcess.Sensors[PIDNumber].PIDNumber = PIDNumber;
                                    }
                                    if (COMOutput[6] == 'C') //Atuador de destino
                                    {
                                        ThisProcess.PIDs[PIDNumber].ActuatorNum = (int)Value;
                                        ThisProcess.PIDs[PIDNumber].SensorUsedInCombobox.SelectedItem = (int)Value + 1;
                                        ThisProcess.Atuators[PIDNumber].PIDControled = true;
                                        ThisProcess.Atuators[PIDNumber].PIDNumber = PIDNumber;
                                    }
                                    if (COMOutput[6] == 'A') //SetPoint
                                    {
                                        ThisProcess.PIDs[PIDNumber].setPoint = Value;
                                    }
                                    if (COMOutput[6] == 'Q') //kP
                                    {
                                        ThisProcess.PIDs[PIDNumber].KP = Value;
                                    }
                                    if (COMOutput[6] == 'J') //kI
                                    {
                                        ThisProcess.PIDs[PIDNumber].KI = Value;
                                    }
                                    if (COMOutput[6] == 'E') //kD
                                    {
                                        ThisProcess.PIDs[PIDNumber].KD = Value;
                                    }
                                    //Atualização
                                    if (COMOutput[6] == 'P') //Termo proporcional
                                    {
                                        ThisProcess.PIDs[PIDNumber].P = Value;
                                    }
                                    if (COMOutput[6] == 'I') //Termo Integral
                                    {
                                        ThisProcess.PIDs[PIDNumber].I = Value;
                                    }
                                    if (COMOutput[6] == 'D') //Termo proporcional
                                    {
                                        ThisProcess.PIDs[PIDNumber].D = Value;
                                    }
                                    if (COMOutput[6] == 'T') //Termo PID
                                    {
                                        ThisProcess.PIDs[PIDNumber].pid = Value;
                                    }

                                    //Recebimento de outras Funções
                                    //...
                                }
                                else
                                {
                                    ThisProcess.CableConnection.CheckSumFails += 1;
                                }
                                ValueConversionFail = false;
                            }
                        }
                    }
                }
                yield return new WaitForSeconds(0.004f);
            }
        }
        //Verifica a integridade de uma mensagem, por uma checagem de soma [0-999]
        public static int hash(string s)
        {
            int k = 7;

            for (int i = 0; i < s.Length; i++)
            {
                k += (int)s[i]; //ASCII number
                k *= 3;
                k -= 13;
                k %= 999;
            }
            return Mathf.Abs(k);
        }

        //permite o calculo de valores inferidos para sensores virtuais, soft sensors
        public static void VirtualSensors(ProcessDetails ThisProcess)
        {
            //ThisProcess guarda varias informações do ambiente proveniente da internet, temperatura, humidade, velocidade do vento, pode ser útil ...
            //Alguns cálculos termodinâmicos estão inclusos, como cálculo de densidade da água líquida dada uma pressão e temperatura...
            //Exemplo, remover em produção

            if (ThisProcess.Sensors.Length >= 4) {
                //Sensor 4
                float value = (ThisProcess.Sensors[0].ActualValue + Time.time) / 900f;
                ThisProcess.Sensors[4].Virtual = true;
                ThisProcess.Sensors[4].TimeLastUpdateMark = Time.time;
                ThisProcess.Sensors[4].TimeSinceLastUpdate = 0f;
                //ThisProcess.Sensors[4].ReqNetUpdate = true;
                if (ThisProcess.Connected)
                {
                    ReceiveSensorValue(ThisProcess.Sensors, 4, value);
                }
            }
        }

        //Cálculos termodinâmicos
        //Densidade
        //https://www.engineeringtoolbox.com/fluid-density-temperature-pressure-d_309.html
        public static float Density(string Substance, float Temperature, float Pressure)
        {
            //Temperature (°C)
            //Pressure (Pa)
            float Result = 0f; //kg/m3
            float T0 = 0.0f;  //°C
            float P0 = 100000f;//Pa
            if (Substance == "Water" || Substance == "water" || Substance == "Água" || Substance == "água")
            {
                if ((Temperature >= 0f && Temperature <= 100f) && (Pressure < 500*100000))//limites de aplicabilidade
                {
                    float B = 0f;      //Volumetric Temperature Coefficient (m3/m3°C)
                    B = MediumBetaOverRange(Substance, 0f, Temperature); //valor médio na faixa de temperatura utilizada
                    //UnityEngine.Debug.Log("B: " + B.ToString());
                    float E = 2150000000f;  //Bulk modulus (Pa)
                    float Ro0 = 999.8f;     //kg/m3 a 0°C
                    Result = DensityCalculation(Ro0, B, T0, Temperature, Pressure, P0, E);
                }
                else
                {
                    LogString += "-- Valores de cálculo de densidade da água fora da faixa de aplicabilidade dos modelos " + "\r\n";
                }
            }
            return Result;
        }

        public static float DensityCalculation(float Ro0, float B, float T0, float Temperature, float Pressure, float P0, float E)
        {
            //UnityEngine.Debug.Log("Termo de pressão: " + (1f - (Pressure - P0) / E).ToString());
            return (Ro0 / (1f + B * (Temperature - T0)))/ (1f - (Pressure - P0) / E); //kg/m3
        }
        public static float Beta(string Substance, float T)
        {
            float B = 0f; //k-1
            if (Substance == "Water" || Substance == "water" || Substance == "Água" || Substance == "água")
            {
                float p1 = 0.000005024f;
                float p2 = -0.0013f;
                float p3 = 0.1568f;
                float p4 = -0.6355f;
                B = p1 * T * T * T + p2 * T * T + p3 * T + p4;
            }
            return B*0.0001f;
        }
        public static float MediumBetaOverRange(string Substance, float Ti, float Tf)
        {
            float NumPoints = 20f;
            float Increment = (Tf - Ti) / NumPoints;
            float Sum = 0f;
            for(float T = Ti; T<=Tf; T+=Increment)
            {
                Sum += Beta(Substance, T);
                //UnityEngine.Debug.Log("T: " + T.ToString());
                //UnityEngine.Debug.Log("B: " + Beta(Substance, T).ToString());
            }
            return Sum/NumPoints;
        }
        //Recebe valores e os direciona para os locais corretos
        public static void UpdateActuatorsInfo(Atuador Actuator)
        {
            //Média dos últimos 10 minutos
            float SumTemp = 0f;
            int NumberOfValues = Actuator.History.GetLength(1);
            for (int j = 0; j < NumberOfValues; j++)
            {
                SumTemp += Actuator.History[0, j].Values;
            }
            Actuator.MeanValue = SumTemp / (float)Actuator.History.GetLength(1);
            Actuator.DateMeanValue = System.DateTime.Now.ToString("HH.mm.ss") + " " + System.DateTime.Now.ToString("dd-MM-yyyy");

            //Valor mínimo
            if (Actuator.ActualValue < Actuator.MinValue)
            {
                Actuator.MinValue = Actuator.ActualValue;
                Actuator.DateMinValue = System.DateTime.Now.ToString("HH.mm.ss") + " " + System.DateTime.Now.ToString("dd-MM-yyyy");
            }

            //valor máximo
            if (Actuator.ActualValue > Actuator.MaxValue)
            {
                Actuator.MaxValue = Actuator.ActualValue;
                Actuator.DateMaxValue = System.DateTime.Now.ToString("HH.mm.ss") + " " + System.DateTime.Now.ToString("dd-MM-yyyy");
            }

            Actuator.TimeSinceLastUpdate = Time.time - Actuator.TimeLastUpdateMark;
            //Atualiza status dos atuadores
            Actuator.AtuadorStatus = Status(Actuator);
            //Atualiza a potência utilizada
            Actuator.PowerConsumption = (Actuator.ActualValue / 100f) * Actuator.MaxVoltage * Actuator.MaxCurrent;

        }

        //Recebe valores e os direciona para os locais corretos
        public static void ReceiveSensorValue(Sensor[] Sensors, int SensorNumber, float Value)
        {
            //Seta o valor recebido na Classe sensor
            Sensors[SensorNumber].ActualValue = Value;
            //Primeiro Recebimento
            if (Sensors[SensorNumber].FirstReceive)
            {
                //Todo o histórico é iniciado com o valor inicial
                for (int i = 0; i < Sensors[SensorNumber].History.GetLength(0); i++)
                {
                    for (int j = 0; j < Sensors[SensorNumber].History.GetLength(1); j++)
                    {
                        Sensors[SensorNumber].History[i, j].Values = (Value + (Sensors[SensorNumber].MaxSecureValue + Sensors[SensorNumber].MinSecureValue) / 2f) / 2f; //Procura um valor médio entre o recebido e a faixa de trabalho
                    }
                }
                Sensors[SensorNumber].StartDate = System.DateTime.Now.ToString("HH.mm.ss") + " " + System.DateTime.Now.ToString("dd-MM-yyyy");
                Sensors[SensorNumber].MeanValue = Value;
                Sensors[SensorNumber].DateMeanValue = Sensors[SensorNumber].StartDate;
                Sensors[SensorNumber].MinValue = Value;
                Sensors[SensorNumber].DateMinValue = Sensors[SensorNumber].StartDate;
                Sensors[SensorNumber].MaxValue = Value;
                Sensors[SensorNumber].DateMaxValue = Sensors[SensorNumber].StartDate;

                Sensors[SensorNumber].FirstReceive = false;
            }
            else
            {
                //Outros recebimentos comuns
                //Média dos últimos 10 minutos
                float SumTemp = 0f;
                int NumberOfValues = Sensors[SensorNumber].History.GetLength(1);
                for (int j = 0; j < NumberOfValues; j++)
                {
                    SumTemp += Sensors[SensorNumber].History[0, j].Values;
                }
                Sensors[SensorNumber].MeanValue = SumTemp / (float)Sensors[SensorNumber].History.GetLength(1);
                Sensors[SensorNumber].DateMeanValue = System.DateTime.Now.ToString("HH.mm.ss") + " " + System.DateTime.Now.ToString("dd-MM-yyyy");
                //Valor mínimo
                if (Value < Sensors[SensorNumber].MinValue)
                {
                    Sensors[SensorNumber].MinValue = Value;
                    Sensors[SensorNumber].DateMinValue = System.DateTime.Now.ToString("HH.mm.ss") + " " + System.DateTime.Now.ToString("dd-MM-yyyy");
                }
                //valor máximo
                if (Value > Sensors[SensorNumber].MaxValue)
                {
                    Sensors[SensorNumber].MaxValue = Value;
                    Sensors[SensorNumber].DateMaxValue = System.DateTime.Now.ToString("HH.mm.ss") + " " + System.DateTime.Now.ToString("dd-MM-yyyy");
                }
            }
        }

        //adiciona um novo valor ao vetor descartando o mais antigo (melhorar)
        public static void UpdateHistory(Sensor[] Sensors, int Line)
        {

            for (int i = 0; i < Sensors.Length; i++)
            {
                for (int j = 0; j < Sensors[i].History.GetLength(1) - 1; j++)
                {
                    Sensors[i].History[Line, j].Modification = Sensors[i].History[Line, j + 1].Modification;
                    Sensors[i].History[Line, j].ModifiedAtuator = Sensors[i].History[Line, j + 1].ModifiedAtuator;
                    Sensors[i].History[Line, j].OldValue = Sensors[i].History[Line, j + 1].OldValue;
                    Sensors[i].History[Line, j].NewValue = Sensors[i].History[Line, j + 1].NewValue;
                    Sensors[i].History[Line, j].Time = Sensors[i].History[Line, j + 1].Time;
                    Sensors[i].History[Line, j].Values = Sensors[i].History[Line, j + 1].Values;
                    Sensors[i].History[Line, j].Rates = Sensors[i].History[Line, j + 1].Rates;
                    Sensors[i].History[Line, j].Dates = Sensors[i].History[Line, j + 1].Dates;
                }

                Sensors[i].History[Line, Sensors[i].History.GetLength(1) - 1].Modification = false;
                Sensors[i].History[Line, Sensors[i].History.GetLength(1) - 1].ModifiedAtuator = 0;
                Sensors[i].History[Line, Sensors[i].History.GetLength(1) - 1].OldValue = 0f;
                Sensors[i].History[Line, Sensors[i].History.GetLength(1) - 1].NewValue = 0f;
                Sensors[i].History[Line, Sensors[i].History.GetLength(1) - 1].Time = 0f;
                Sensors[i].History[Line, Sensors[i].History.GetLength(1) - 1].Values = Sensors[i].ActualValue;
                Sensors[i].History[Line, Sensors[i].History.GetLength(1) - 1].Rates = Sensors[i].Rate;
                Sensors[i].History[Line, Sensors[i].History.GetLength(1) - 1].Dates = DateTime.Now.ToString("HH.mm.ss");
            }
        }
        //adiciona um novo valor ao vetor descartando o mais antigo ------------- não testado (melhorar)
        public static void UpdateHistory(Atuador[] Atuators, int Line)
        {
            for (int i = 0; i < Atuators.Length; i++)
            {
                for (int j = 0; j < Atuators[i].History.GetLength(1) - 1; j++)
                {
                    Atuators[i].History[Line, j].Modification = Atuators[i].History[Line, j + 1].Modification;
                    Atuators[i].History[Line, j].ModifiedAtuator = Atuators[i].History[Line, j + 1].ModifiedAtuator;
                    Atuators[i].History[Line, j].OldValue = Atuators[i].History[Line, j + 1].OldValue;
                    Atuators[i].History[Line, j].NewValue = Atuators[i].History[Line, j + 1].NewValue;
                    Atuators[i].History[Line, j].Time = Atuators[i].History[Line, j + 1].Time;
                    Atuators[i].History[Line, j].Values = Atuators[i].History[Line, j + 1].Values;
                    Atuators[i].History[Line, j].Rates = Atuators[i].History[Line, j + 1].Rates;
                    Atuators[i].History[Line, j].Dates = Atuators[i].History[Line, j + 1].Dates;
                }

                Atuators[i].History[Line, Atuators[i].History.GetLength(1) - 1].Modification = false;
                Atuators[i].History[Line, Atuators[i].History.GetLength(1) - 1].ModifiedAtuator = 0;
                Atuators[i].History[Line, Atuators[i].History.GetLength(1) - 1].OldValue = 0f;
                Atuators[i].History[Line, Atuators[i].History.GetLength(1) - 1].NewValue = 0f;
                Atuators[i].History[Line, Atuators[i].History.GetLength(1) - 1].Time = 0f;
                Atuators[i].History[Line, Atuators[i].History.GetLength(1) - 1].Values = Atuators[i].ActualValue;
                Atuators[i].History[Line, Atuators[i].History.GetLength(1) - 1].Rates = Atuators[i].Rate;
                Atuators[i].History[Line, Atuators[i].History.GetLength(1) - 1].Dates = DateTime.Now.ToString("HH.mm.ss");
            }
        }

        //Atualiza o conjunto de gráficos necessário
        public static void UpdateCharts(Sensor[] Sensors, Atuador[] Atuadors)
        {
            for (int i = 0; i < Sensors.Length; i++) //fazer para os outros tempos também
            {
                if (Sensors[i].Focus)
                {
                    for (int k = 0; k < Sensors[i].NormalizedValueTexture.Length; k++)
                    {
                        Sensors[i].ModificationTexture[k] = Convert.ToSingle(Sensors[i].History[Sensors[i].HistoryLineInFocus, k].Modification);
                        Sensors[i].NormalizedValueTexture[k] = (Sensors[i].History[Sensors[i].HistoryLineInFocus, k].Values - Sensors[i].MinSecureValue) / (Sensors[i].MaxSecureValue - Sensors[i].MinSecureValue);
                        //if (Sensors[i].NormalizedValueTexture[k] > 1f) Sensors[i].NormalizedValueTexture[k] = 1f;
                        //if (Sensors[i].NormalizedValueTexture[k] < 0f) Sensors[i].NormalizedValueTexture[k] = 0f;
                    }
                    Sensors[i].HistoryTextures[0] = CreateTextureFromArray(Sensors[i].ModificationTexture);
                    Sensors[i].HistoryTextures[1] = CreateTextureFromArray(Sensors[i].NormalizedValueTexture, Sensors[i].SecurityMargin);

                    UpdateChart(Sensors[i].Chart, CreateCounter(Sensors[i].History.GetLength(1), Sensors[i].HistoryLineInFocus), HistoricValuesLine(Sensors[i].History, Sensors[i].HistoryLineInFocus));//melhorar
                }
            }
            for (int i = 0; i < Atuadors.Length; i++) //fazer para os outros tempos também
            {
                if (Atuadors[i].Focus)
                {
                    //apenas para a linha em foco
                    for (int k = 0; k < Atuadors[i].NormalizedValueTexture.Length; k++)
                    {
                        Atuadors[i].ModificationTexture[k] = Convert.ToSingle(Atuadors[i].History[Atuadors[i].HistoryLineInFocus, k].Modification);
                        Atuadors[i].NormalizedValueTexture[k] = (Atuadors[i].History[Atuadors[i].HistoryLineInFocus, k].Values - Atuadors[i].MinSecureValue) / (Atuadors[i].MaxSecureValue - Atuadors[i].MinSecureValue);
                        if (Atuadors[i].NormalizedValueTexture[k] > 1f) Atuadors[i].NormalizedValueTexture[k] = 1f;
                        if (Atuadors[i].NormalizedValueTexture[k] < 0f) Atuadors[i].NormalizedValueTexture[k] = 0f;
                    }
                    Atuadors[i].HistoryTextures[0] = CreateTextureFromArray(Atuadors[i].ModificationTexture);
                    Atuadors[i].HistoryTextures[1] = CreateTextureFromArray(Atuadors[i].NormalizedValueTexture, Atuadors[i].SecurityMargin);

                    UpdateChart(Atuadors[i].Chart, CreateCounter(Atuadors[i].History.GetLength(1), Sensors[i].HistoryLineInFocus), HistoricValuesLine(Atuadors[i].History, Atuadors[i].HistoryLineInFocus));//melhorar
                }
            }
        }

        //Armazena a informação de que uma modificação foi efetuada em cada sensor e atuador
        public static void ModificationDone(Sensor[] Sensors, Atuador[] Atuators, int AtuatorNumber)
        {
            for (int i = 0; i < Sensors.Length; i++)
            {
                for (int j = 0; j < Sensors[i].History.GetLength(0) - 1; j++)
                {
                    Sensors[i].History[j, Sensors[i].History.GetLength(1) - 1].Modification = true;
                    Sensors[i].History[j, Sensors[i].History.GetLength(1) - 1].ModifiedAtuator = AtuatorNumber;
                    Sensors[i].History[j, Sensors[i].History.GetLength(1) - 1].OldValue = Atuators[AtuatorNumber].History[j, Atuators[AtuatorNumber].History.GetLength(1) - 2].Values;
                    Sensors[i].History[j, Sensors[i].History.GetLength(1) - 1].NewValue = Atuators[AtuatorNumber].ActualValue;
                    Sensors[i].History[j, Sensors[i].History.GetLength(1) - 1].Time = Time.time;
                }
            }
            for (int i = 0; i < Atuators.Length; i++)
            {
                for (int j = 0; j < Atuators[i].History.GetLength(0) - 1; j++)
                {
                    Atuators[i].History[j, Atuators[i].History.GetLength(1) - 1].Modification = true;
                    Atuators[i].History[j, Atuators[i].History.GetLength(1) - 1].ModifiedAtuator = AtuatorNumber;
                    Atuators[i].History[j, Atuators[i].History.GetLength(1) - 1].OldValue = Atuators[AtuatorNumber].History[j, Atuators[AtuatorNumber].History.GetLength(1) - 2].Values;
                    Atuators[i].History[j, Atuators[i].History.GetLength(1) - 1].NewValue = Atuators[AtuatorNumber].ActualValue;
                    Atuators[i].History[j, Atuators[i].History.GetLength(1) - 1].Time = Time.time;
                }
            }
        }

        //Definir e visualizar detalhes dos PIDs
        public static void PIDDetails(ProcessDetails ThisProcess, UIButtonData[] PIDButtons)
        {
            GUI.DrawTexture(FreeRectSpace, Gray20TextureAlpha90);

            for (int i = 0; i < ThisProcess.PIDs.Length; i++)//
            {
                PIDButtons[i].PosX = Mathf.RoundToInt(FreeRectSpace.x + Padding + i * (FreeRectSpace.width - 2 * Padding) / (ThisProcess.PIDs.Length));
                PIDButtons[i].PosY = Mathf.RoundToInt(FreeRectSpace.y + 2 * FreeRectSpace.height / 30);
                PIDButtons[i].SizeX = Mathf.RoundToInt((FreeRectSpace.width - 2 * Padding) / (ThisProcess.PIDs.Length));
                PIDButtons[i].SizeY = Mathf.RoundToInt(FreeRectSpace.height / 20);

                if (Button(PIDButtons[i]))
                {
                    ThisProcess.PIDs[i].Selected = true;
                    for (int j = 0; j < ThisProcess.PIDs.Length; j++)//
                    {
                        if (i != j)
                        {
                            PIDButtons[j].Toggle = false;
                            ThisProcess.PIDs[j].Selected = false;
                        }
                    }
                }

                if (ThisProcess.PIDs[i].Selected || PIDButtons[i].Toggle)
                {
                    rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + 12 * FreeRectSpace.height / 100 + Padding, FreeRectSpace.width - 2 * Padding, FreeRectSpace.height / 10);
                    GUI.DrawTexture(rectDraw, BlackTextureAlpha20);
                    rectDraw.x = rectDraw.x + 2 * Padding;
                    rectDraw.y = rectDraw.y + FreeRectSpace.height / 160;
                    rectDraw.width = FreeRectSpace.width / 6;
                    GUI.skin.label.fontSize = 14 * FontSize / 10;  // modificou o tamanho da fonte do label
                    GUI.Label(rectDraw, "PID " + i.ToString());
                    //PIDs[i].Enabled = GUI.Toggle(rectDraw, PIDs[i].Enabled, "  PID "+i.ToString());

                    ThisProcess.PIDs[i].ApplyButton.PosX = Mathf.RoundToInt(FreeRectSpace.x + Padding);
                    ThisProcess.PIDs[i].ApplyButton.PosY = Mathf.RoundToInt(FreeRectSpace.y + 28 * FreeRectSpace.height / 30);
                    ThisProcess.PIDs[i].ApplyButton.SizeX = Mathf.RoundToInt(FreeRectSpace.width - 2 * Padding);
                    ThisProcess.PIDs[i].ApplyButton.SizeY = Mathf.RoundToInt(FreeRectSpace.height / 20);
                    if (Button(ThisProcess.PIDs[i].ApplyButton))
                    {
                        //Enviar dados ao processo
                        ThisProcess.PIDs[i].ReqNetUpdate = true;
                        ThisProcess.Sensors[ThisProcess.PIDs[i].SensorUsedInCombobox.SelectedItem - 1].PIDControled = true;
                        ThisProcess.Atuators[ThisProcess.PIDs[i].ActuatorUsedInCombobox.SelectedItem - 1].PIDControled = true;
                        ThisProcess.PIDs[i].ActuatorNum = ThisProcess.PIDs[i].ActuatorUsedInCombobox.SelectedItem - 1;
                        ThisProcess.PIDs[i].SensorNum = ThisProcess.PIDs[i].SensorUsedInCombobox.SelectedItem - 1;
                        ThisProcess.PIDs[i].Enabled = true;
                        ThisProcess.PIDs[i].InfoToSendToPLC = true;
                    }
                    rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + 12 * FreeRectSpace.height / 100 + Padding, Padding / 2, FreeRectSpace.height / 10);
                    if (!ThisProcess.PIDs[i].Enabled)
                    {
                        GUI.DrawTexture(rectDraw, Gray75Texture);
                        rectDraw = new Rect(FreeRectSpace.x + 3 * Padding, FreeRectSpace.y + 15 * FreeRectSpace.height / 100 + 2 * Padding, FreeRectSpace.width / 3, FreeRectSpace.height / 8);
                        GUI.Label(rectDraw, "Dasabilitado");
                    }
                    else
                    {
                        GUI.DrawTexture(rectDraw, GreenTexture);
                        rectDraw = new Rect(FreeRectSpace.x + 3 * Padding, FreeRectSpace.y + 15 * FreeRectSpace.height / 100 + 2 * Padding, FreeRectSpace.width / 3, FreeRectSpace.height / 8);
                        GUI.Label(rectDraw, "Habilitado");
                    }

                    rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + FreeRectSpace.height / 2 + Padding, FreeRectSpace.width / 8 + 10 * FontSize, FreeRectSpace.height / 30);
                    GUI.skin.textField.fontSize = 14 * FontSize / 10;  // modificou o tamanho da fonte do textfield
                    GUI.Label(rectDraw, "kP:");
                    rectDraw.y = rectDraw.y + FreeRectSpace.height / 25;
                    ThisProcess.PIDs[i].KP = float.Parse(GUI.TextField(rectDraw, ThisProcess.PIDs[i].KP.ToString(), 25));
                    rectDraw.y = rectDraw.y + FreeRectSpace.height / 25;
                    GUI.Label(rectDraw, "kI:");
                    rectDraw.y = rectDraw.y + FreeRectSpace.height / 25;
                    ThisProcess.PIDs[i].KI = float.Parse(GUI.TextField(rectDraw, ThisProcess.PIDs[i].KI.ToString(), 25));
                    rectDraw.y = rectDraw.y + FreeRectSpace.height / 25;
                    GUI.Label(rectDraw, "kD:");
                    rectDraw.y = rectDraw.y + FreeRectSpace.height / 25;
                    ThisProcess.PIDs[i].KD = float.Parse(GUI.TextField(rectDraw, ThisProcess.PIDs[i].KD.ToString(), 25));

                    rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + 45 * FreeRectSpace.height / 100, FreeRectSpace.width - 2 * Padding, FreeRectSpace.height / 30);
                    if (ThisProcess.PIDs[i].SensorUsedInCombobox.SelectedItem != 0)
                    {
                        //rectDraw.y += FreeRectSpace.height / 15;
                        GUI.Label(rectDraw, "Setpoint:                              " + ThisProcess.Sensors[ThisProcess.PIDs[i].SensorUsedInCombobox.SelectedItem - 1].Unit);
                        rectDraw.x += 6 * FontSize;
                        rectDraw.width = 6 * FontSize;
                        ThisProcess.PIDs[i].setPoint = float.Parse(GUI.TextField(rectDraw, ThisProcess.PIDs[i].setPoint.ToString(), 25));
                    }

                    GUI.skin.textField.fontSize = FontSize;  // modificou o tamanho da fonte do textfield

                    rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + FreeRectSpace.height / 3 + Padding, FreeRectSpace.width / 8 + 10 * FontSize, FreeRectSpace.height / 30);
                    GUI.Label(rectDraw, "Sensor:");
                    rectDraw.y = rectDraw.y + FreeRectSpace.height / 40;
                    ThisProcess.PIDs[i].SensorUsedInCombobox.Rect = rectDraw;
                    ComboBox(ThisProcess.PIDs[i].SensorUsedInCombobox);
                    //if (ThisProcess.PIDs[i].SensorUsedInCombobox.Selected) DisableAllSensorsAbove = true;

                    rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + FreeRectSpace.height / 4 + Padding, FreeRectSpace.width / 8 + 10 * FontSize, FreeRectSpace.height / 30);
                    GUI.Label(rectDraw, "Atuador:");
                    rectDraw.y = rectDraw.y + FreeRectSpace.height / 40;
                    ThisProcess.PIDs[i].ActuatorUsedInCombobox.Rect = rectDraw;
                    ComboBox(ThisProcess.PIDs[i].ActuatorUsedInCombobox);
                    //if (ThisProcess.PIDs[i].ActuatorUsedInCombobox.Selected) DisableAllActuatorsAbove = true;

                    rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + 8 * FreeRectSpace.height / 10 + Padding, FreeRectSpace.width - 2 * Padding, FreeRectSpace.height / 30);
                    GUI.skin.label.fontSize = 14 * FontSize / 10;  // modificou o tamanho da fonte do label
                    GUI.Label(rectDraw, "P: " + ThisProcess.PIDs[i].P.ToString("G3") + "   I: " + ThisProcess.PIDs[i].I.ToString("G3") + "   D: " + ThisProcess.PIDs[i].D.ToString("G3") + "   PID: " + ThisProcess.PIDs[i].pid.ToString("G3"));

                    rectDraw = new Rect(FreeRectSpace.x, FreeRectSpace.y, FreeRectSpace.width, FreeRectSpace.height / 30);
                    GUI.DrawTexture(rectDraw, Gray20TextureAlpha90);
                    //GUI.skin.label.fontSize = GUI.skin.label.fontSize + Padding / 2;
                    GUI.Label(new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + Padding / 4, FreeRectSpace.width, FreeRectSpace.height / 30), "PIDs ");
                    //GUI.skin.label.fontSize = GUI.skin.label.fontSize - Padding / 2;

                    rectDraw.y = Mathf.RoundToInt(FreeRectSpace.y + FreeRectSpace.height / 30);
                    rectDraw.height = FontSize / 5;
                    GUI.DrawTexture(rectDraw, WhiteTexture);
                }
            }

        }
        //Preenche os combobox dos PIDs
        public static void InitializePIDs(ProcessDetails ThisProcess, UIButtonData[] PIDButtons)
        {
            /*
            PIDButtons = new UIButtonData[ThisProcess.PIDs.Length];
            for (int j = 0; j < ThisProcess.PIDs.Length; j++)
            {
                PIDButtons[j] = new UIButtonData();
            }
            */
            for (int i = 0; i < ThisProcess.PIDs.Length; i++)
            {
                //UI
                if (i == 0) PIDButtons[i].Toggle = true;
                PIDButtons[i].Name = "PID " + i.ToString();
                PIDButtons[i].Text = "PID " + i.ToString();
                PIDButtons[i].BarDown = true;

                //Valores sugeridos iniciais dos PIDs
                ThisProcess.PIDs[i].KP = 0.3f;
                ThisProcess.PIDs[i].KI = 0.004f;
                ThisProcess.PIDs[i].KD = 0.02f;
                //Botão de aplicar
                ThisProcess.PIDs[i].ApplyButton.Name = "Apply";
                ThisProcess.PIDs[i].ApplyButton.Text = "Aplicar";
                ThisProcess.PIDs[i].ApplyButton.Icon = ApplyIcon;
                ThisProcess.PIDs[i].ApplyButton.BarDown = true;
                //Sensores
                ThisProcess.PIDs[i].SensorUsedInCombobox.Texts = new string[ThisProcess.Sensors.Length+1];
                for (int j = 0; j < ThisProcess.Sensors.Length; j++)
                {
                    if (j == 0) ThisProcess.PIDs[i].SensorUsedInCombobox.Texts[j] = "--";

                    if (ThisProcess.Sensors[j].AutoNumber)
                    {
                         ThisProcess.PIDs[i].SensorUsedInCombobox.Texts[j+1] = ThisProcess.Sensors[j].Name + " " + j.ToString();
                    }
                    else
                    {
                        ThisProcess.PIDs[i].SensorUsedInCombobox.Texts[j+1] = ThisProcess.Sensors[j].Name + " " + ThisProcess.Sensors[j].Number;
                    }
                    ThisProcess.PIDs[i].SensorUsedInCombobox.Arrow = DownArrowIcon;
                }
                //Atuadores
                ThisProcess.PIDs[i].ActuatorUsedInCombobox.Texts = new string[ThisProcess.Atuators.Length+1];
                for (int j = 0; j < ThisProcess.Atuators.Length; j++)
                {
                    if (j == 0) ThisProcess.PIDs[i].ActuatorUsedInCombobox.Texts[j] = "--";

                    if (ThisProcess.Atuators[j].AutoNumber)
                    {
                        ThisProcess.PIDs[i].ActuatorUsedInCombobox.Texts[j+1] = ThisProcess.Atuators[j].Name + " " + j.ToString();
                    }
                    else
                    {
                        ThisProcess.PIDs[i].ActuatorUsedInCombobox.Texts[j+1] = ThisProcess.Atuators[j].Name + " " + ThisProcess.Atuators[j].Number;
                    }
                    ThisProcess.PIDs[i].ActuatorUsedInCombobox.Arrow = DownArrowIcon;
                }
            }
        }

        //Vizualizar dados ao longo do tempo para os sensores  ------------------------ Melhorar
        public static void SensorHistoryDetails(Sensor PointedSensor) //Sensor,Intervalo de tempo (10min, 1 hora, ...), 0 modification, 1 valores...
        {
            GUI.DrawTexture(FreeRectSpace, Gray20TextureAlpha90);
            //Gráfico e dados históricos
            if (PointedSensor.HistoricChart.Toggle) {
                //Gráfico
                PointedSensor.Chart.TargetChartRect.x = FreeRectSpace.x + Padding;
                PointedSensor.Chart.TargetChartRect.y = FreeRectSpace.y + 5 * FreeRectSpace.height / 30 + Padding;
                PointedSensor.Chart.TargetChartRect.width = FreeRectSpace.width - 2 * Padding;
                PointedSensor.Chart.TargetChartRect.height = 4 * FreeRectSpace.height / 10 - 4 * Padding;
                //Parâmetros de entrada precisam ser aramzenados a cada atualização
                PlotChart(PointedSensor.Chart, CreateCounter(PointedSensor.History.GetLength(1), PointedSensor.HistoryLineInFocus), HistoricValuesLine(PointedSensor.History, PointedSensor.HistoryLineInFocus)); //melhorar

                rectDraw = new Rect(PointedSensor.Chart.AllRectInfo[4].x, FreeRectSpace.y + FreeRectSpace.height / 2 + 3 * 2 * FontSize, PointedSensor.Chart.AllRectInfo[4].width, 3 * FontSize / 2);
                GUI.Label(new Rect(FreeRectSpace.x + Padding, rectDraw.y - 2 * FontSize, rectDraw.width, rectDraw.height), "Modificações");
                GUI.DrawTexture(rectDraw, PointedSensor.HistoryTextures[0]);

                rectDraw = new Rect(PointedSensor.Chart.AllRectInfo[4].x, FreeRectSpace.y + FreeRectSpace.height / 2 + 5 * 2 * FontSize, PointedSensor.Chart.AllRectInfo[4].width, 3 * FontSize / 2);
                GUI.Label(new Rect(FreeRectSpace.x + Padding, rectDraw.y - 2 * FontSize, rectDraw.width, rectDraw.height), "Escala de valores");
                GUI.DrawTexture(rectDraw, PointedSensor.HistoryTextures[1]);

                rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + FreeRectSpace.height / 2 + 7 * 2 * FontSize, FreeRectSpace.width - 2f * Padding, 2 * FontSize);
                GUI.Label(new Rect(rectDraw.x, rectDraw.y - 2 * FontSize, rectDraw.width, rectDraw.height), "Detalhes do sensor");

                //Valor atual
                GUI.DrawTexture(new Rect(rectDraw.x, rectDraw.y + rectDraw.height / 8, rectDraw.height / 2, rectDraw.height / 2), StableIcon);
                if (PointedSensor.ActualValue < PointedSensor.MinSecureValue || PointedSensor.ActualValue > PointedSensor.MaxSecureValue) { GUI.color = Color.yellow; }
                GUI.Label(new Rect(rectDraw.x + rectDraw.height, rectDraw.y, rectDraw.width, rectDraw.height), "Valor atual: ");
                if (!PointedSensor.FirstReceive)
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y, rectDraw.width, rectDraw.height), PointedSensor.ActualValue.ToString("G5") + PointedSensor.Unit);
                    if (FreeRectSpace.width > Screen.width / 4) GUI.Label(new Rect(rectDraw.x + rectDraw.height + 16 * FontSize + Padding, rectDraw.y, rectDraw.width, rectDraw.height), "Atual");
                }
                else
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y, rectDraw.width, rectDraw.height), "--" + PointedSensor.Unit);
                }
                GUI.color = Color.white;

                //Minimo
                GUI.DrawTexture(new Rect(rectDraw.x, rectDraw.y + rectDraw.height / 8 + 2 * FontSize, rectDraw.height / 2, rectDraw.height / 2), MinimumArrowIcon);
                if (PointedSensor.MinValue < PointedSensor.MinSecureValue || PointedSensor.MinValue > PointedSensor.MaxSecureValue) { GUI.color = Color.yellow; }
                GUI.Label(new Rect(rectDraw.x + rectDraw.height, rectDraw.y + 2 * FontSize, rectDraw.width, rectDraw.height), "Valor mínimo: ");
                if (!PointedSensor.FirstReceive)
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y + 2 * FontSize, rectDraw.width, rectDraw.height), PointedSensor.MinValue.ToString("G5") + PointedSensor.Unit);
                    if (FreeRectSpace.width > Screen.width / 4) GUI.Label(new Rect(rectDraw.x + rectDraw.height + 16 * FontSize + Padding, rectDraw.y + 2 * FontSize, rectDraw.width, rectDraw.height), "Data: " + PointedSensor.DateMinValue);
                }
                else
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y + 2 * FontSize, rectDraw.width, rectDraw.height), "--" + PointedSensor.Unit);
                }

                GUI.color = Color.white;

                //Máximo
                GUI.DrawTexture(new Rect(rectDraw.x, rectDraw.y + rectDraw.height / 8 + 2 * 2 * FontSize, rectDraw.height / 2, rectDraw.height / 2), MaximumArrowIcon);
                if (PointedSensor.MaxValue < PointedSensor.MinSecureValue || PointedSensor.MaxValue > PointedSensor.MaxSecureValue) { GUI.color = Color.yellow; }
                GUI.Label(new Rect(rectDraw.x + rectDraw.height, rectDraw.y + 2 * 2 * FontSize, rectDraw.width, rectDraw.height), "Valor máximo: ");
                if (!PointedSensor.FirstReceive)
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y + 2 * 2 * FontSize, rectDraw.width, rectDraw.height), PointedSensor.MaxValue.ToString("G5") + PointedSensor.Unit);
                    if (FreeRectSpace.width > Screen.width / 4) GUI.Label(new Rect(rectDraw.x + rectDraw.height + 16 * FontSize + Padding, rectDraw.y + 2 * 2 * FontSize, rectDraw.width, rectDraw.height), "Data: " + PointedSensor.DateMaxValue);
                }
                else
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y + 2 * 2 * FontSize, rectDraw.width, rectDraw.height), "--" + PointedSensor.Unit);
                }
                GUI.color = Color.white;

                //Média
                GUI.DrawTexture(new Rect(rectDraw.x, rectDraw.y + rectDraw.height / 8 + 3 * 2 * FontSize, rectDraw.height / 2, rectDraw.height / 2), MeanIcon);
                if (PointedSensor.MeanValue < PointedSensor.MinSecureValue || PointedSensor.MeanValue > PointedSensor.MaxSecureValue) { GUI.color = Color.red; }
                GUI.Label(new Rect(rectDraw.x + rectDraw.height, rectDraw.y + 3 * 2 * FontSize, rectDraw.width, rectDraw.height), "Valor médio: ");
                if (!PointedSensor.FirstReceive)
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y + 3 * 2 * FontSize, rectDraw.width, rectDraw.height), PointedSensor.MeanValue.ToString("G5") + PointedSensor.Unit);
                    if (FreeRectSpace.width > Screen.width / 4) GUI.Label(new Rect(rectDraw.x + rectDraw.height + 16 * FontSize + Padding, rectDraw.y + 3 * 2 * FontSize, rectDraw.width, rectDraw.height), "Últimos 10 minutos");
                }
                else
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y + 3 * 2 * FontSize, rectDraw.width, rectDraw.height), "--" + PointedSensor.Unit);
                }
                GUI.color = Color.white;

                rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + FreeRectSpace.height / 2 + 11 * 2 * FontSize, FreeRectSpace.width - 2f * Padding, 2 * FontSize);

                //
                if (PointedSensor.Rate > 0)
                    GUI.DrawTexture(new Rect(rectDraw.x, rectDraw.y + rectDraw.height / 8, rectDraw.height / 2, rectDraw.height / 2), RateUpIcon);
                if (PointedSensor.Rate < 0)
                    GUI.DrawTexture(new Rect(rectDraw.x, rectDraw.y + rectDraw.height / 8, rectDraw.height / 2, rectDraw.height / 2), RateDownIcon);
                if (PointedSensor.Rate == 0)
                    GUI.DrawTexture(new Rect(rectDraw.x, rectDraw.y + rectDraw.height / 8, rectDraw.height / 2, rectDraw.height / 2), StableIcon);
                //
                GUI.Label(new Rect(rectDraw.x + rectDraw.height, rectDraw.y, rectDraw.width, rectDraw.height), "Taxa de variação: ");
                if (!PointedSensor.FirstReceive)
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y, rectDraw.width, rectDraw.height), PointedSensor.Rate.ToString("G5") + PointedSensor.Unit + "/s");
                    if (FreeRectSpace.width > Screen.width / 4) GUI.Label(new Rect(rectDraw.x + rectDraw.height + 16 * FontSize + Padding, rectDraw.y, rectDraw.width, rectDraw.height), "Atual");
                }
                else
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y, rectDraw.width, rectDraw.height), "--" + PointedSensor.Unit);
                }

                //Combobox de tempo
                PointedSensor.ComboBoxHistory.Rect.x = FreeRectSpace.x + Padding;
                PointedSensor.ComboBoxHistory.Rect.y = FreeRectSpace.y + FreeRectSpace.height / 10 + Padding;
                PointedSensor.ComboBoxHistory.Rect.width = FreeRectSpace.width / 4;
                PointedSensor.ComboBoxHistory.Rect.height = FreeRectSpace.height / 30;
                PointedSensor.HistoryLineInFocus = ComboBox(PointedSensor.ComboBoxHistory);//Seta a linha do histórico de tempo desejada (0,1,2 ou 3 correspondente a 10min, 1 hora, ...)
                if (PointedSensor.HistoryLineInFocus == 0) PointedSensor.Chart.XLabel = "Tempo (s)";
                if (PointedSensor.HistoryLineInFocus == 1) PointedSensor.Chart.XLabel = "Tempo (min)";
                if (PointedSensor.HistoryLineInFocus == 2 || PointedSensor.HistoryLineInFocus == 3) PointedSensor.Chart.XLabel = "Tempo (h)";

                //Botão de resetar
                PointedSensor.ResetButton.PosX = Mathf.RoundToInt(FreeRectSpace.x + FreeRectSpace.width / 4 + Padding);
                PointedSensor.ResetButton.PosY = Mathf.RoundToInt(FreeRectSpace.y + FreeRectSpace.height / 10 + Padding);
                PointedSensor.ResetButton.SizeX = Mathf.RoundToInt(FreeRectSpace.width / 4);
                PointedSensor.ResetButton.SizeY = Mathf.RoundToInt(FreeRectSpace.height / 30);
                if (Button(PointedSensor.ResetButton)) { PointedSensor.FirstReceive = true; }
            }

            //Editor de limites e alertas
            if (PointedSensor.Limits.Toggle)
            {
                GUI.skin.textField.fontSize = FontSize/2 + Screen.height/100;
                GUI.skin.toggle.fontSize = FontSize/2 + Screen.height / 100;
                //Mínimo
                rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + FreeRectSpace.height / 10 + Padding, FreeRectSpace.width/2, 2 * FontSize-Padding/4);
                GUI.Label(rectDraw, "Mínimo valor seguro (" + PointedSensor.Unit + ")");
                rectDraw.width = FreeRectSpace.width / 3 - 2f * Padding;
                rectDraw.y += FreeRectSpace.height / 30;
                PointedSensor.MinSecureValue = float.Parse(GUI.TextField(rectDraw, PointedSensor.MinSecureValue.ToString(), 250));
                rectDraw.x = FreeRectSpace.x + Padding + FreeRectSpace.width / 3;
                rectDraw.width = 8 * FontSize;
                PointedSensor.MinAlert.DrawRect = rectDraw;
                PointedSensor.MinSecValueAlert = Toggle(PointedSensor.MinAlert, PointedSensor.MinSecValueAlert);
                PointedSensor.MinCritical.DrawRect = PointedSensor.MinAlert.DrawRect;
                PointedSensor.MinCritical.DrawRect.x += 9 * FontSize;
                PointedSensor.MinSecValueCritical = Toggle(PointedSensor.MinCritical, PointedSensor.MinSecValueCritical);
                //Máximo
                rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + 5* FreeRectSpace.height / 30 + Padding, FreeRectSpace.width / 2, 2 * FontSize-Padding/4);
                GUI.Label(rectDraw, "Máximo valor seguro (" + PointedSensor.Unit + ")");
                rectDraw.width = FreeRectSpace.width / 3 - 2f * Padding;
                rectDraw.y += FreeRectSpace.height / 30;
                PointedSensor.MaxSecureValue = float.Parse(GUI.TextField(rectDraw, PointedSensor.MaxSecureValue.ToString(), 250));
                rectDraw.x = FreeRectSpace.x + Padding + FreeRectSpace.width / 3;
                rectDraw.width = 8 * FontSize;
                PointedSensor.MaxAlert.DrawRect = rectDraw;
                PointedSensor.MaxSecValueAlert = Toggle(PointedSensor.MaxAlert, PointedSensor.MaxSecValueAlert);
                PointedSensor.MaxCritical.DrawRect = PointedSensor.MaxAlert.DrawRect;
                PointedSensor.MaxCritical.DrawRect.x += 9 * FontSize;
                PointedSensor.MaxSecValueCritical = Toggle(PointedSensor.MaxCritical, PointedSensor.MaxSecValueCritical);
                //Margem de segurança
                rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + 7 * FreeRectSpace.height / 30 + Padding, FreeRectSpace.width / 2, 2 * FontSize - Padding/4);
                GUI.Label(rectDraw, "Margem de segurança (%)");
                rectDraw.width = FreeRectSpace.width / 3 - 2f * Padding;
                rectDraw.y += FreeRectSpace.height / 30;
                PointedSensor.SecurityMargin = float.Parse(GUI.TextField(rectDraw, PointedSensor.SecurityMargin.ToString(), 250));
                //Status
                rectDraw.y += FreeRectSpace.height / 30;
                GUI.Label(rectDraw, "Status: " + PointedSensor.SensorStatus.ToString());

            }

            //Botão de mostrar o gráfico
            PointedSensor.HistoricChart.PosX = Mathf.RoundToInt(FreeRectSpace.x + Padding);
            PointedSensor.HistoricChart.PosY = Mathf.RoundToInt(FreeRectSpace.y + 3*FreeRectSpace.height / 60 + Padding);
            PointedSensor.HistoricChart.SizeX = Mathf.RoundToInt(FreeRectSpace.width / 4);
            PointedSensor.HistoricChart.SizeY = Mathf.RoundToInt(FreeRectSpace.height / 30);
            if (Button(PointedSensor.HistoricChart)) { PointedSensor.Limits.Toggle = false; }

            //Botão de mostrar o editor de limites
            PointedSensor.Limits.PosX = Mathf.RoundToInt(FreeRectSpace.x + FreeRectSpace.width / 4 + Padding);
            PointedSensor.Limits.PosY = Mathf.RoundToInt(FreeRectSpace.y + 3 * FreeRectSpace.height / 60 + Padding);
            PointedSensor.Limits.SizeX = Mathf.RoundToInt(FreeRectSpace.width / 4);
            PointedSensor.Limits.SizeY = Mathf.RoundToInt(FreeRectSpace.height / 30);
            if (Button(PointedSensor.Limits)) { PointedSensor.HistoricChart.Toggle = false; }

            rectDraw = new Rect(FreeRectSpace.x, FreeRectSpace.y, FreeRectSpace.width, FreeRectSpace.height / 30);
            GUI.DrawTexture(rectDraw, Gray20TextureAlpha90);
            GUI.skin.label.fontSize = GUI.skin.label.fontSize + Padding/2;
            GUI.Label(new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + Padding / 4, FreeRectSpace.width, FreeRectSpace.height / 30), "Sensor: " + PointedSensor.Name + " " + PointedSensor.Number);
            GUI.skin.label.fontSize = GUI.skin.label.fontSize - Padding/2;

            rectDraw.y = Mathf.RoundToInt(FreeRectSpace.y + FreeRectSpace.height / 30);
            rectDraw.height = Mathf.RoundToInt(FontSize / 5);
            GUI.DrawTexture(rectDraw, WhiteTexture);

            //importante!//melhorar
            //ThisProcess.Sensors[i].HistoryLineInFocus apenas para a linha em foco, desabilitar quando mudar de grafico

        }

        //Vizualizar dados ao longo do tempo para os atuadores  ------------------------ Melhorar
        public static void ActuatorHistoryDetails(Atuador PointedActuator) //Sensor,Intervalo de tempo (10min, 1 hora, ...), 0 modification, 1 valores...
        {
            GUI.DrawTexture(FreeRectSpace, Gray20TextureAlpha90);
            //Gráfico e dados históricos
            if (PointedActuator.HistoricChart.Toggle)
            {
                //Gráfico
                PointedActuator.Chart.TargetChartRect.x = FreeRectSpace.x + Padding;
                PointedActuator.Chart.TargetChartRect.y = FreeRectSpace.y + 5 * FreeRectSpace.height / 30 + Padding;
                PointedActuator.Chart.TargetChartRect.width = FreeRectSpace.width - 2 * Padding;
                PointedActuator.Chart.TargetChartRect.height = 4 * FreeRectSpace.height / 10 - 4 * Padding;
                //Parâmetros de entrada precisam ser aramzenados a cada atualização
                PlotChart(PointedActuator.Chart, CreateCounter(PointedActuator.History.GetLength(1), PointedActuator.HistoryLineInFocus), HistoricValuesLine(PointedActuator.History, PointedActuator.HistoryLineInFocus)); //melhorar

                rectDraw = new Rect(PointedActuator.Chart.AllRectInfo[4].x, FreeRectSpace.y + FreeRectSpace.height / 2 + 3 * 2 * FontSize, PointedActuator.Chart.AllRectInfo[4].width, 3 * FontSize / 2);
                GUI.Label(new Rect(FreeRectSpace.x + Padding, rectDraw.y - 2 * FontSize, rectDraw.width, rectDraw.height), "Modificações");
                GUI.DrawTexture(rectDraw, PointedActuator.HistoryTextures[0]);

                rectDraw = new Rect(PointedActuator.Chart.AllRectInfo[4].x, FreeRectSpace.y + FreeRectSpace.height / 2 + 5 * 2 * FontSize, PointedActuator.Chart.AllRectInfo[4].width, 3 * FontSize / 2);
                GUI.Label(new Rect(FreeRectSpace.x + Padding, rectDraw.y - 2 * FontSize, rectDraw.width, rectDraw.height), "Escala de valores");
                GUI.DrawTexture(rectDraw, PointedActuator.HistoryTextures[1]);

                rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + FreeRectSpace.height / 2 + 7 * 2 * FontSize, FreeRectSpace.width - 2f * Padding, 2 * FontSize);

                GUI.Label(new Rect(rectDraw.x, rectDraw.y - 2 * FontSize, rectDraw.width, rectDraw.height), "Intensidade");
                //
                //Slider que define o valor
                rectDraw2 = new Rect(rectDraw.x + Padding, rectDraw.y, rectDraw.width - 2f * Padding, 2 * PointedActuator.AtuadorUI.LocalFontSize);
                PointedActuator.AtuadorUI.IntensitySlider.PosX = Mathf.RoundToInt(rectDraw2.x);
                PointedActuator.AtuadorUI.IntensitySlider.PosY = Mathf.RoundToInt(rectDraw2.y);
                PointedActuator.AtuadorUI.IntensitySlider.SizeX = Mathf.RoundToInt(rectDraw2.width);
                PointedActuator.AtuadorUI.IntensitySlider.SizeY = Mathf.RoundToInt(rectDraw2.height);
                PointedActuator.AtuadorUI.IntensitySlider.HardMove = true;
                PointedActuator.ActualValue = SimpleSlider(PointedActuator.AtuadorUI.IntensitySlider);

                rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + FreeRectSpace.height / 2 + 7 * 2 * FontSize, FreeRectSpace.width - 2f * Padding, 2 * FontSize);
                //Subdivisões da barra de valor
                if (PointedActuator.AtuadorUI.BarSubdivisions >= 1)
                {
                    for (int i = 0; i <= PointedActuator.AtuadorUI.BarSubdivisions; i++)
                    {
                        rectDraw2 = new Rect(rectDraw.x + FontSize / 2 + i * (rectDraw.width - 2f * Padding) / PointedActuator.AtuadorUI.BarSubdivisions, rectDraw.y+ 2 * PointedActuator.AtuadorUI.LocalFontSize / 3, PointedActuator.AtuadorUI.LocalFontSize / 5, PointedActuator.AtuadorUI.LocalFontSize / 5);
                        GUI.DrawTexture(rectDraw2, WhiteTextureAlpha50);
                    }
                }

                GUI.skin.label.fontSize = 8 * PointedActuator.AtuadorUI.LocalFontSize / 10;

                var centeredStyle = GUI.skin.GetStyle("Label");

                centeredStyle.alignment = TextAnchor.UpperLeft;
                //
                rectDraw2 = new Rect(rectDraw.x, rectDraw.y, PointedActuator.AtuadorUI.LocalFontSize * 7, 2 * PointedActuator.AtuadorUI.LocalFontSize);
                GUI.Label(rectDraw2, PointedActuator.MinSecureValue.ToString());
                centeredStyle.alignment = TextAnchor.UpperRight;
                //
                rectDraw2 = new Rect(rectDraw.x + rectDraw.width - PointedActuator.AtuadorUI.LocalFontSize * 7, rectDraw.y, PointedActuator.AtuadorUI.LocalFontSize * 7, 2 * PointedActuator.AtuadorUI.LocalFontSize);
                GUI.Label(rectDraw2, PointedActuator.MaxSecureValue.ToString());
                centeredStyle.alignment = TextAnchor.UpperLeft;

                GUI.skin.label.fontSize = 9 * PointedActuator.AtuadorUI.LocalFontSize / 10;
                //GUI.skin.textField.fontSize = PointedActuator.AtuadorUI.LocalFontSize;


                if (PointedActuator.PWM)
                {
                    rectDraw2 = new Rect(rectDraw.x, rectDraw.y + 2.1f * PointedActuator.AtuadorUI.LocalFontSize, PointedActuator.AtuadorUI.LocalFontSize, PointedActuator.AtuadorUI.LocalFontSize);
                    GUI.DrawTexture(rectDraw2, DutyCicle);
                    rectDraw2 = new Rect(rectDraw.x + 3 * PointedActuator.AtuadorUI.LocalFontSize / 2, rectDraw.y + 2f * PointedActuator.AtuadorUI.LocalFontSize, rectDraw.width, 2.3f * PointedActuator.AtuadorUI.LocalFontSize);
                    GUI.Label(rectDraw2, "Ciclo PWM");
                }

                if (PointedActuator.OnOff)
                {
                    rectDraw2 = new Rect(rectDraw.x, rectDraw.y + 2.1f * PointedActuator.AtuadorUI.LocalFontSize, PointedActuator.AtuadorUI.LocalFontSize, PointedActuator.AtuadorUI.LocalFontSize);
                    GUI.DrawTexture(rectDraw2, DutyCicle);
                    rectDraw2 = new Rect(rectDraw.x + 3 * PointedActuator.AtuadorUI.LocalFontSize / 2, rectDraw.y + 2f * PointedActuator.AtuadorUI.LocalFontSize, rectDraw.width, 2.3f * PointedActuator.AtuadorUI.LocalFontSize);
                    GUI.Label(rectDraw2, "Ciclo On/Off");

                    if (PointedActuator.ActualStatus)
                    {
                        rectDraw2 = new Rect(rectDraw.x + 8 * PointedActuator.AtuadorUI.LocalFontSize, rectDraw.y + 2f * PointedActuator.AtuadorUI.LocalFontSize, PointedActuator.AtuadorUI.LocalFontSize, PointedActuator.AtuadorUI.LocalFontSize);
                        GUI.DrawTexture(rectDraw2, SimpleSphere);
                        rectDraw2 = new Rect(rectDraw.x + 10 * PointedActuator.AtuadorUI.LocalFontSize, rectDraw.y + 2f * PointedActuator.AtuadorUI.LocalFontSize, rectDraw.width / 3, 2 * PointedActuator.AtuadorUI.LocalFontSize);
                        GUI.Label(rectDraw2, "Atualmente ligado");
                    }
                    if (!PointedActuator.ActualStatus)
                    {
                        rectDraw2 = new Rect(rectDraw.x +  8 * PointedActuator.AtuadorUI.LocalFontSize, rectDraw.y + 2f * PointedActuator.AtuadorUI.LocalFontSize, PointedActuator.AtuadorUI.LocalFontSize, PointedActuator.AtuadorUI.LocalFontSize);
                        GUI.DrawTexture(rectDraw2, SimpleSphere35);
                        rectDraw2 = new Rect(rectDraw.x + 10 * PointedActuator.AtuadorUI.LocalFontSize, rectDraw.y + 2f * PointedActuator.AtuadorUI.LocalFontSize, rectDraw.width / 4, 2 * PointedActuator.AtuadorUI.LocalFontSize);
                        GUI.Label(rectDraw2, "Atualmente desligado");
                    }
                    rectDraw2 = new Rect(rectDraw.x + 21 * PointedActuator.AtuadorUI.LocalFontSize, rectDraw.y + 2f * PointedActuator.AtuadorUI.LocalFontSize, rectDraw.width / 4, 2.3f * PointedActuator.AtuadorUI.LocalFontSize);
                    GUI.Label(rectDraw2, "Ciclo de " + PointedActuator.OnOffCicle.ToString() + "seg");
                    rectDraw2 = new Rect(rectDraw.x + 28 * PointedActuator.AtuadorUI.LocalFontSize, rectDraw.y + 2.4f * PointedActuator.AtuadorUI.LocalFontSize, 8 * PointedActuator.AtuadorUI.LocalFontSize / 10, 8 * PointedActuator.AtuadorUI.LocalFontSize / 10);
                    GUI.DrawTexture(rectDraw2, RateUpIcon);
                    if (ClickOnRect(rectDraw2)) { PointedActuator.OnOffCicle += 5f; PointedActuator.CicleToSend = true; }
                    rectDraw2 = new Rect(rectDraw.x + 30 * PointedActuator.AtuadorUI.LocalFontSize, rectDraw.y + 2.4f * PointedActuator.AtuadorUI.LocalFontSize, 8 * PointedActuator.AtuadorUI.LocalFontSize / 10, 8 * PointedActuator.AtuadorUI.LocalFontSize / 10);
                    GUI.DrawTexture(rectDraw2, RateDownIcon);
                    if (ClickOnRect(rectDraw2)) { PointedActuator.OnOffCicle -= 5f; PointedActuator.CicleToSend = true; }
                    //PointedActuator.OnOffCicle = float.Parse(GUI.TextField(PointedAtuador.AtuadorUI.AllRectInfo[10], PointedAtuador.OnOffCicle.ToString(), PointedAtuador.AtuadorUI.LocalFontSize));
                    if (PointedActuator.OnOffCicle < 5f) PointedActuator.OnOffCicle = 5f;
                    if (PointedActuator.OnOffCicle > 250f) PointedActuator.OnOffCicle = 250f;
                }

                rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + FreeRectSpace.height / 2 + 10 * 2 * FontSize, FreeRectSpace.width - 2f * Padding, 2 * FontSize);
                GUI.Label(new Rect(rectDraw.x, rectDraw.y - 2 * FontSize, rectDraw.width, rectDraw.height), "Detalhes do atuador");

                //Valor atual
                GUI.DrawTexture(new Rect(rectDraw.x, rectDraw.y + rectDraw.height / 8, rectDraw.height / 2, rectDraw.height / 2), StableIcon);
                if (PointedActuator.ActualValue < PointedActuator.MinSecureValue || PointedActuator.ActualValue > PointedActuator.MaxSecureValue) { GUI.color = Color.yellow; }
                GUI.Label(new Rect(rectDraw.x + rectDraw.height, rectDraw.y, rectDraw.width, rectDraw.height), "Valor atual: ");
                if (!PointedActuator.FirstReceive)
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y, rectDraw.width, rectDraw.height), PointedActuator.ActualValue.ToString("G5") + PointedActuator.Unit);
                    if (FreeRectSpace.width > Screen.width / 4) GUI.Label(new Rect(rectDraw.x + rectDraw.height + 16 * FontSize + Padding, rectDraw.y, rectDraw.width, rectDraw.height), "Atual");
                }
                else
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y, rectDraw.width, rectDraw.height), "--" + PointedActuator.Unit);
                }
                GUI.color = Color.white;

                //Minimo
                GUI.DrawTexture(new Rect(rectDraw.x, rectDraw.y + rectDraw.height / 8 + 2 * FontSize, rectDraw.height / 2, rectDraw.height / 2), MinimumArrowIcon);
                if (PointedActuator.MinValue < PointedActuator.MinSecureValue || PointedActuator.MinValue > PointedActuator.MaxSecureValue) { GUI.color = Color.yellow; }
                GUI.Label(new Rect(rectDraw.x + rectDraw.height, rectDraw.y + 2 * FontSize, rectDraw.width, rectDraw.height), "Valor mínimo: ");
                if (!PointedActuator.FirstReceive)
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y + 2 * FontSize, rectDraw.width, rectDraw.height), PointedActuator.MinValue.ToString("G5") + PointedActuator.Unit);
                    if (FreeRectSpace.width > Screen.width / 4) GUI.Label(new Rect(rectDraw.x + rectDraw.height + 16 * FontSize + Padding, rectDraw.y + 2 * FontSize, rectDraw.width, rectDraw.height), "Data: " + PointedActuator.DateMinValue);
                }
                else
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y + 2 * FontSize, rectDraw.width, rectDraw.height), "--" + PointedActuator.Unit);
                }

                GUI.color = Color.white;

                //Máximo
                GUI.DrawTexture(new Rect(rectDraw.x, rectDraw.y + rectDraw.height / 8 + 2 * 2 * FontSize, rectDraw.height / 2, rectDraw.height / 2), MaximumArrowIcon);
                if (PointedActuator.MaxValue < PointedActuator.MinSecureValue || PointedActuator.MaxValue > PointedActuator.MaxSecureValue) { GUI.color = Color.yellow; }
                GUI.Label(new Rect(rectDraw.x + rectDraw.height, rectDraw.y + 2 * 2 * FontSize, rectDraw.width, rectDraw.height), "Valor máximo: ");
                if (!PointedActuator.FirstReceive)
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y + 2 * 2 * FontSize, rectDraw.width, rectDraw.height), PointedActuator.MaxValue.ToString("G5") + PointedActuator.Unit);
                    if (FreeRectSpace.width > Screen.width / 4) GUI.Label(new Rect(rectDraw.x + rectDraw.height + 16 * FontSize + Padding, rectDraw.y + 2 * 2 * FontSize, rectDraw.width, rectDraw.height), "Data: " + PointedActuator.DateMaxValue);
                }
                else
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y + 2 * 2 * FontSize, rectDraw.width, rectDraw.height), "--" + PointedActuator.Unit);
                }
                GUI.color = Color.white;

                //Média
                GUI.DrawTexture(new Rect(rectDraw.x, rectDraw.y + rectDraw.height / 8 + 3 * 2 * FontSize, rectDraw.height / 2, rectDraw.height / 2), MeanIcon);
                if (PointedActuator.MeanValue < PointedActuator.MinSecureValue || PointedActuator.MeanValue > PointedActuator.MaxSecureValue) { GUI.color = Color.red; }
                GUI.Label(new Rect(rectDraw.x + rectDraw.height, rectDraw.y + 3 * 2 * FontSize, rectDraw.width, rectDraw.height), "Valor médio: ");
                if (!PointedActuator.FirstReceive)
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y + 3 * 2 * FontSize, rectDraw.width, rectDraw.height), PointedActuator.MeanValue.ToString("G5") + PointedActuator.Unit);
                    if (FreeRectSpace.width > Screen.width / 4) GUI.Label(new Rect(rectDraw.x + rectDraw.height + 16 * FontSize + Padding, rectDraw.y + 3 * 2 * FontSize, rectDraw.width, rectDraw.height), "Últimos 10 minutos");
                }
                else
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y + 3 * 2 * FontSize, rectDraw.width, rectDraw.height), "--" + PointedActuator.Unit);
                }
                GUI.color = Color.white;

                rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + FreeRectSpace.height / 2 + 14 * 2 * FontSize, FreeRectSpace.width - 2f * Padding, 2 * FontSize);

                //
                if (PointedActuator.Rate > 0)
                    GUI.DrawTexture(new Rect(rectDraw.x, rectDraw.y + rectDraw.height / 8, rectDraw.height / 2, rectDraw.height / 2), RateUpIcon);
                if (PointedActuator.Rate < 0)
                    GUI.DrawTexture(new Rect(rectDraw.x, rectDraw.y + rectDraw.height / 8, rectDraw.height / 2, rectDraw.height / 2), RateDownIcon);
                if (PointedActuator.Rate == 0)
                    GUI.DrawTexture(new Rect(rectDraw.x, rectDraw.y + rectDraw.height / 8, rectDraw.height / 2, rectDraw.height / 2), StableIcon);
                //
                GUI.Label(new Rect(rectDraw.x + rectDraw.height, rectDraw.y, rectDraw.width, rectDraw.height), "Taxa de variação: ");
                if (!PointedActuator.FirstReceive)
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y, rectDraw.width, rectDraw.height), PointedActuator.Rate.ToString("G5") + PointedActuator.Unit + "/s");
                    if (FreeRectSpace.width > Screen.width / 4) GUI.Label(new Rect(rectDraw.x + rectDraw.height + 16 * FontSize + Padding, rectDraw.y, rectDraw.width, rectDraw.height), "Atual");
                }
                else
                {
                    GUI.Label(new Rect(rectDraw.x + rectDraw.height + 8 * FontSize + Padding, rectDraw.y, rectDraw.width, rectDraw.height), "--" + PointedActuator.Unit);
                }

                //Combobox de tempo
                PointedActuator.ComboBoxHistory.Rect.x = FreeRectSpace.x + Padding;
                PointedActuator.ComboBoxHistory.Rect.y = FreeRectSpace.y + FreeRectSpace.height / 10 + Padding;
                PointedActuator.ComboBoxHistory.Rect.width = FreeRectSpace.width / 4;
                PointedActuator.ComboBoxHistory.Rect.height = FreeRectSpace.height / 30;
                PointedActuator.HistoryLineInFocus = ComboBox(PointedActuator.ComboBoxHistory);//Seta a linha do histórico de tempo desejada (0,1,2 ou 3 correspondente a 10min, 1 hora, ...)
                if (PointedActuator.HistoryLineInFocus == 0) PointedActuator.Chart.XLabel = "Tempo (s)";
                if (PointedActuator.HistoryLineInFocus == 1) PointedActuator.Chart.XLabel = "Tempo (min)";
                if (PointedActuator.HistoryLineInFocus == 2 || PointedActuator.HistoryLineInFocus == 3) PointedActuator.Chart.XLabel = "Tempo (h)";

                //Botão de resetar
            }


            //Botão de mostrar o gráfico
            PointedActuator.HistoricChart.PosX = Mathf.RoundToInt(FreeRectSpace.x + Padding);
            PointedActuator.HistoricChart.PosY = Mathf.RoundToInt(FreeRectSpace.y + 3 * FreeRectSpace.height / 60 + Padding);
            PointedActuator.HistoricChart.SizeX = Mathf.RoundToInt(FreeRectSpace.width / 4);
            PointedActuator.HistoricChart.SizeY = Mathf.RoundToInt(FreeRectSpace.height / 30);
            if (Button(PointedActuator.HistoricChart)) { PointedActuator.Limits.Toggle = false; }

            //Botão de mostrar o editor de limites
            PointedActuator.Limits.PosX = Mathf.RoundToInt(FreeRectSpace.x + FreeRectSpace.width / 4 + Padding);
            PointedActuator.Limits.PosY = Mathf.RoundToInt(FreeRectSpace.y + 3 * FreeRectSpace.height / 60 + Padding);
            PointedActuator.Limits.SizeX = Mathf.RoundToInt(FreeRectSpace.width / 4);
            PointedActuator.Limits.SizeY = Mathf.RoundToInt(FreeRectSpace.height / 30);
            if (Button(PointedActuator.Limits)) { PointedActuator.HistoricChart.Toggle = false; }

            rectDraw = new Rect(FreeRectSpace.x, FreeRectSpace.y, FreeRectSpace.width, FreeRectSpace.height / 30);
            GUI.DrawTexture(rectDraw, Gray20TextureAlpha90);
            GUI.skin.label.fontSize = GUI.skin.label.fontSize + Padding / 2;
            GUI.Label(new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + Padding / 4, FreeRectSpace.width, FreeRectSpace.height / 30), "Atuador: " + PointedActuator.Name + " " + PointedActuator.Number);
            GUI.skin.label.fontSize = GUI.skin.label.fontSize - Padding / 2;

            rectDraw.y = Mathf.RoundToInt(FreeRectSpace.y + FreeRectSpace.height / 30);
            rectDraw.height = Mathf.RoundToInt(FontSize / 5);
            GUI.DrawTexture(rectDraw, WhiteTexture);

            //importante!//melhorar
            //ThisProcess.Sensors[i].HistoryLineInFocus apenas para a linha em foco, desabilitar quando mudar de grafico

        }

        //Debug: Organiza os valores do histórico de um sensor em uma linha
        public static string SensorHistory(Sensor[] Sensors, int SensNum, int line, string separator)
        {
            string result = "";
            for (int i = 0; i < Sensors[SensNum].History.GetLength(1); i++)
            {
                result += Sensors[SensNum].History[line, i].Values.ToString() + separator;
            }
            return result;
        }

        //Organiza os valores dos sensores em uma linha
        public static string SensorsValues(Sensor[] Sensors, string separator)
        {
            string result = "";
            if (Sensors.Length > 0)
            {
                for (int i = 0; i < Sensors.Length; i++)
                {
                    result += Sensors[i].ActualValue.ToString() + separator;
                }
            }
            return result;
        }

        //Organiza os nomes dos sensores em uma linha
        public static string SensorsNames(Sensor[] Sensors, string separator)
        {
            string result = "";
            if (Sensors.Length > 0)
            {
                for (int i = 0; i < Sensors.Length; i++)
                {
                    result += Sensors[i].Name + separator;
                }
            }
            return result;
        }

        //Organiza os valores dos atuadores em uma linha
        public static string AtuatorsValues(Atuador[] Atuators, string separator)
        {
            string result = "";
            if (Atuators.Length > 0)
            {
                for (int i = 0; i < Atuators.Length; i++)
                {
                    result += Atuators[i].ActualValue.ToString() + separator;
                }
            }
            return result;
        }

        //Organiza os nomes dos atuadores em uma linha
        public static string AtuatorsNames(Atuador[] Atuators, string separator)
        {
            string result = "";
            if (Atuators.Length > 0)
            {
                for (int i = 0; i < Atuators.Length; i++)
                {
                    result += Atuators[i].Name + separator;
                }
            }
            return result;
        }
        //Separa uma linha de um vetor bidimensional
        public static float[] PartOfArrayLine(float[,] InputArray, int Line)
        {
            float[] array = new float[InputArray.GetLength(0)];
            for (int i = 0; i < InputArray.GetLength(0); i++)
            {
                array[i] = InputArray[i, Line];
            }
            return array;
        }

        //Separa uma linha de valores a partir um vetor de historico
        public static float[] HistoricValuesLine(HistoricData[,] History, int Line)
        {
            float[] array = new float[History.GetLength(1)];
            for (int i = 0; i < History.GetLength(1); i++)
            {
                array[i] = History[Line, i].Values;
            }
            return array;
        }

        //Criar um vetor com os indices
        public static float[] CreateCounter(int Number, int History)
        {
            float[] Result = new float[Number];
            for (int i = 0; i < Number; i++)
            {
                if (History == 0)//segundos
                {
                    Result[i] = Mathf.Round(10 * (-600f + 3f * (i + 1))) / 10f;
                }
                if (History == 1)//minutos
                {
                    Result[i] = Mathf.Round(10 * (-60f + 0.3f * (i + 1))) / 10f;
                }
                if (History == 2)//horas
                {
                    Result[i] = Mathf.Round(10 * (-10f + 0.05f * (i + 1))) / 10f;
                }
                if (History == 3)//horas
                {
                    Result[i] = Mathf.Round(10 * (-240f + 1.2f * (i + 1))) / 10f;
                }

            }
            return Result;
        }

        //Calcula a potencia total utilizada pelo sistema, baseado nas informações dadas pelo usuário
        public static float PowerCalculation(ProcessDetails ThisProcess)
        {
            float Power = 0f;
            float ArduinoPower = 0f;
            if (ThisProcess.CableConnection.Connected) ArduinoPower = 0.4f;

            if (ThisProcess.Atuators.Length > 0)
            {
                for (int i = 0; i < ThisProcess.Atuators.Length; i++)
                {
                    Power += ThisProcess.Atuators[i].ActualValue * ThisProcess.Atuators[i].MaxVoltage * ThisProcess.Atuators[i].MaxCurrent / 100f;
                }
            }
            //incluir sensores e qualquer consumo extra aqui -------
            return 1.05f * Power + ArduinoPower; //fator de potência 0,95
        }

        //Registro de LOG
        public static void LogInfo(string Info)
        {
            UnityEngine.Debug.Log(Info);
            LogString += "-- " + System.DateTime.Now.ToString("HH.mm.ss") + " " + System.DateTime.Now.ToString("dd-MM-yyyy") + " " + Info + "\r\n";
            if (ScrollLogString) LogScrollPosition += new Vector2(0, 25000);
            if (!BlockWriteInfo) AddTextToLogFile(Info);
        }
        //Registro de LOG
        public static void LogResult(string Info, ProcessDetails ThisProcess)
        {
            if (!BlockWriteInfo)
            {
                if (Info == "Start")
                {
                    AddTextToFile(Results, "\r\n" + System.DateTime.Now.ToString("HH.mm.ss") + "   " + System.DateTime.Now.ToString("dd-MM-yyyy") + "--------------------------------------------------------- Programa Aberto Resultados Gerais -------------------------------------------------");
                    AddTextToFile(Results, "\r\n" + System.DateTime.Now.ToString("HH.mm.ss") + "   " + System.DateTime.Now.ToString("dd-MM-yyyy") + "   " + SensorsNames(ThisProcess.Sensors, "   ") + "   " + AtuatorsNames(ThisProcess.Atuators, "   "));

                    AddTextToFile(LogSensors, "\r\n" + System.DateTime.Now.ToString("HH.mm.ss") + "   " + System.DateTime.Now.ToString("dd-MM-yyyy") + "--------------------------------------------------------- Programa Aberto Resultados dos Sensores -------------------------------------------------");
                    AddTextToFile(LogSensors, "\r\n" + System.DateTime.Now.ToString("HH.mm.ss") + "   " + System.DateTime.Now.ToString("dd-MM-yyyy") + "   " + SensorsNames(ThisProcess.Sensors, "   "));

                    AddTextToFile(LogAtuators, "\r\n" + System.DateTime.Now.ToString("HH.mm.ss") + "   " + System.DateTime.Now.ToString("dd-MM-yyyy") + "--------------------------------------------------------- Programa Aberto Resultados dos Atuadores -------------------------------------------------");
                    AddTextToFile(LogAtuators, "\r\n" + System.DateTime.Now.ToString("HH.mm.ss") + "   " + System.DateTime.Now.ToString("dd-MM-yyyy") + "   " + AtuatorsNames(ThisProcess.Atuators, "   "));
                }
                if (Info == "SaveInfo")
                {
                    AddTextToFile(Results, "\r\n" + System.DateTime.Now.ToString("HH.mm.ss") + "   " + System.DateTime.Now.ToString("dd-MM-yyyy") + "   " + SensorsValues(ThisProcess.Sensors, "   ") + "   " + AtuatorsValues(ThisProcess.Atuators, "   "));
                    AddTextToFile(LogSensors, "\r\n" + System.DateTime.Now.ToString("HH.mm.ss") + "   " + System.DateTime.Now.ToString("dd-MM-yyyy") + "   " + SensorsValues(ThisProcess.Sensors, "   "));
                    AddTextToFile(LogAtuators, "\r\n" + System.DateTime.Now.ToString("HH.mm.ss") + "   " + System.DateTime.Now.ToString("dd-MM-yyyy") + "   " + AtuatorsValues(ThisProcess.Atuators, "   "));
                }
            }
        }

        //Desenha linhas em qualquer direção a partir de dois pontos
        public static void DrawLineStretched(Vector2 lineStart, Vector2 lineEnd, Texture2D texture, int thickness)
        {
            Vector2 lineVector = lineEnd - lineStart;
            float angle = Mathf.Rad2Deg * Mathf.Atan(lineVector.y / lineVector.x);
            if (lineVector.x < 0) { angle += 180; }
            if (thickness < 1) { thickness = 1; }

            int thicknessOffset = (int)Mathf.Ceil(thickness / 2);

            //UnityEngine.Debug.Log("thickness: " + (thickness).ToString());

            GUIUtility.RotateAroundPivot(angle,
                                         lineStart);
            GUI.DrawTexture(new Rect(lineStart.x,
                                     lineStart.y - thicknessOffset,
                                     lineVector.magnitude,
                                     thickness), texture);
            GUIUtility.RotateAroundPivot(-angle, lineStart);
        }

        //Cria uma textura a partir de uma cor
        public static Texture2D CreateTexture(Color TextColor)
        {
            //UnityEngine.Debug.Log("Textura criada");
            Texture2D OutTexture = new Texture2D(1, 1);
            OutTexture.SetPixel(0, 0, TextColor);
            OutTexture.Apply();
            return OutTexture;
        }

        //Cria uma textura a partir de um vetor de float
        public static Texture2D CreateTextureFromArray(float[] Array)
        {
            //UnityEngine.Debug.Log("Textura criada");
            Texture2D OutTexture = new Texture2D(Array.Length, 1);
            for (int i = 0; i < Array.Length; i++)
            {
                OutTexture.SetPixel(i, 0, new Color(Array[i], Array[i], Array[i], 1f));
            }
            //OutTexture.anisoLevel = 9;
            OutTexture.wrapMode = TextureWrapMode.Clamp;
            OutTexture.filterMode = FilterMode.Point;
            OutTexture.Apply();

            return OutTexture;
        }

        //Cria uma textura a partir de um vetor de float
        public static Texture2D CreateTextureFromArray(float[] Array, float Securitymargin)
        {
            //UnityEngine.Debug.Log("Textura criada");
            Texture2D OutTexture = new Texture2D(Array.Length, 1);
            for (int i = 0; i < Array.Length; i++)
            {
                if (Array[i] >= 1f || Array[i] <= 0f)
                {
                    OutTexture.SetPixel(i, 0, new Color(1f, 0.1f, 0.1f, 1f));
                }
                else if (Array[i] >= 1f - Securitymargin / 100f || Array[i] <= Securitymargin / 100f)
                {
                    OutTexture.SetPixel(i, 0, new Color(1f, 0.92f, 0.016f, 1f));
                }
                else
                {
                    OutTexture.SetPixel(i, 0, new Color(Array[i], Array[i], Array[i], 1f));
                }
            }
            //OutTexture.anisoLevel = 9;
            OutTexture.wrapMode = TextureWrapMode.Clamp;
            OutTexture.filterMode = FilterMode.Point;
            OutTexture.Apply();

            return OutTexture;
        }

        //Criar as dependencias
        //Criar Pastas
        public static void CreateAndInitializeDependencies(ProcessDetails ThisProcess)
        {
            if (!Directory.Exists(Application.dataPath + "/Screenshots"))
            {
                System.IO.Directory.CreateDirectory(Application.dataPath + "/Screenshots");
            }
            //Speak
            if (Directory.Exists(Application.dataPath + "/TempSpeak"))
            {
                DeleteFolderContent(Application.dataPath + "/TempSpeak");  //Limpa tudo que houver
            }
            //Fala
            if (!Directory.Exists(Application.dataPath + "/TempSpeak"))
            {
                System.IO.Directory.CreateDirectory(Application.dataPath + "/TempSpeak");
            }
            //Resultados
            if (!Directory.Exists(Application.dataPath + "/Resultados"))
            {
                System.IO.Directory.CreateDirectory(Application.dataPath + "/Resultados");
            }

            //Criar arquivos de armazenamento de dados
            if (!File.Exists(Results)) { File.Create(Results).Close(); }
            if (!File.Exists(LogSensors)) { File.Create(LogSensors).Close(); }
            if (!File.Exists(LogAtuators)) { File.Create(LogAtuators).Close(); }
            //Criar arquivo de Log
            if (!File.Exists(LogFile)) { File.Create(LogFile).Close(); }
            //Cria o arquivo de configurações
            if (!File.Exists(Config)) { 
                File.Create(Config).Close();
                InitializeConfigIni(Config);
            }
            //Carrega dados salvos no Config.ini
            if (ThisProcess.Emails.EmailsList[1] == "") ThisProcess.Emails.EmailsList[1] = ConfigIni.ReadString("Emails", "ExtraEmai1");
            if (ThisProcess.Emails.EmailsList[2] == "") ThisProcess.Emails.EmailsList[2] = ConfigIni.ReadString("Emails", "ExtraEmai2");
            if (ThisProcess.Emails.EmailsList[3] == "") ThisProcess.Emails.EmailsList[3] = ConfigIni.ReadString("Emails", "ExtraEmai3");

            LogResult("Start", ThisProcess);
            LogInfo("Programa iniciado");

        }

        //Inicializa o arquivo config.ini
        public static void InitializeConfigIni(string adress)
        {
            AddTextToFile(adress, "[Emails]");
            AddTextToFile(adress, "ExtraEmai1=");
            AddTextToFile(adress, "ExtraEmai2=");
            AddTextToFile(adress, "ExtraEmai3=");
            AddTextToFile(adress, "[UI]");
            AddTextToFile(adress, "FontSize=");
        }

        //Arquivos de texto
        public static void AddTextToFile(string adress, string value)
        {
            if (!File.Exists(adress))
            {
                File.Create(adress).Close();
            }
            if (!BlockWriteInfo)
            {
                StreamWriter sw = new StreamWriter(adress, true, Encoding.Unicode);
                string NextLine = value + "\r\n";
                sw.Write(NextLine);
                sw.Dispose();
                sw.Close();
            }
        }

        //Arquivos de texto
        public static void AddTextToLogFile(string value)
        {
            if (!File.Exists(LogFile))
            {
                File.Create(LogFile).Close();
            }
            if (!BlockWriteInfo)
            {
                StreamWriter sw = new StreamWriter(LogFile, true, Encoding.UTF8);
                string NextLine = "\r\n" + System.DateTime.Now.ToString("HH.mm.ss") + " " + System.DateTime.Now.ToString("dd-MM-yyyy") + " " + value;
                sw.Write(NextLine);
                sw.Dispose();
                sw.Close();
            }
        }
        //Desabilita o feedback por voz
        public static void Mute()
        {
            Speaking = false;
        }

        //Habilita o feedback por voz
        public static void UnMute()
        {
            Speaking = true;
        }

        //Fala 
        public static void Speak(string text)
        {
            if (Speaking)
            {
                if (!Directory.Exists(Application.dataPath + "/TempSpeak"))
                {
                    Directory.CreateDirectory(Application.dataPath + "/TempSpeak");
                }
                string str = UnityEngine.Random.Range(0, 999999999).ToString("D9");
                string Write = Application.dataPath + "/TempSpeak/Temp" + str + ".vbs";
                AddTextToFile(Write, "\r\nSet voice = CreateObject(\"SAPI.Spvoice\")");
                AddTextToFile(Write, "\r\nvoice.Rate = 1.2");
                AddTextToFile(Write, "\r\nvoice.Volume = 100");
                AddTextToFile(Write, "\r\nvoice.Speak\"" + text + "\"");
                AddTextToFile(Write, "Set obj = CreateObject(\"Scripting.FileSystemObject\")");
                AddTextToFile(Write, "obj.DeleteFile(\"" + Write + "\")");
                new Process
                {
                    StartInfo =
                        {
                            FileName = "cscript",
                            Arguments = "/B /Nologo \"" + Write + "\"",
                            WindowStyle = ProcessWindowStyle.Hidden
                        }
                }.Start();
            }
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

        //Configurações
        public static void RemoteConections(ProcessDetails ThisProcess)
        {
            GUI.skin.toggle.fontSize = FontSize;

            GUI.DrawTexture(FreeRectSpace, Gray20TextureAlpha90);

            rectDraw = new Rect(FreeRectSpace.x, FreeRectSpace.y, FreeRectSpace.width, FreeRectSpace.height / 30);
            GUI.DrawTexture(rectDraw, Gray20TextureAlpha90);
            GUI.skin.label.fontSize = GUI.skin.label.fontSize + Padding / 2;
            GUI.Label(new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + Padding / 4, FreeRectSpace.width, FreeRectSpace.height / 30), "Conexões remotas");

            GUI.Label(new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + Padding + FreeRectSpace.height / 30, FreeRectSpace.width - 2*Padding, FreeRectSpace.height / 13), ThisProcess.RemoteProcessConnection.Status);
            GUI.Label(new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + Padding + 3*FreeRectSpace.height / 30, FreeRectSpace.width - 2 * Padding, FreeRectSpace.height / 13), "Ping: " + ThisProcess.RemoteProcessConnection.Ping.ToString()+" ms");

            //chat
            rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + Padding + 5 * FreeRectSpace.height / 30, FreeRectSpace.width - 2*Padding, 10*FreeRectSpace.height / 30);
            GUILayout.BeginArea(rectDraw);
            ChatScrollPosition = GUILayout.BeginScrollView(ChatScrollPosition, GUILayout.Width(rectDraw.width), GUILayout.Height(rectDraw.height - 10));

            GUILayout.Label("Chat:\n" + ThisProcess.RemoteProcessConnection.NetCommunication.ReceivedString, GUILayout.Width(rectDraw.width - 4 * Padding), GUILayout.Height(1850 * (ThisProcess.RemoteProcessConnection.NetCommunication.ReceivedString.Split('\n').Length + 2) * FontSize / 1000));

            GUILayout.EndScrollView();
            GUILayout.EndArea();

            GUI.skin.textField.fontSize = FontSize;
            GUI.skin.button.fontSize = FontSize;
            ThisProcess.RemoteProcessConnection.NetCommunication.SendString = GUI.TextField(new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + Padding + 16*FreeRectSpace.height / 30, FreeRectSpace.width - 2 * Padding, FreeRectSpace.height / 30), ThisProcess.RemoteProcessConnection.NetCommunication.SendString, 250);
            if (GUI.Button(new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + Padding + 17 * FreeRectSpace.height / 30, FreeRectSpace.width - 2 * Padding, FreeRectSpace.height / 30), "Enviar"))
            {
                ThisProcess.RemoteProcessConnection.NetCommunication.SendChat = true;
                ChatScrollPosition += new Vector2(0, 2500);
            }

            GUI.skin.label.fontSize = GUI.skin.label.fontSize - Padding / 2;
            //
            rectDraw.y = Mathf.RoundToInt(FreeRectSpace.y + FreeRectSpace.height / 30);
            rectDraw.height = FontSize / 5;
            GUI.DrawTexture(rectDraw, WhiteTexture);

            GUI.skin.label.fontSize = FontSize;

        }

        //Emails
        public static void Email(UIButtonData[] GeneralButtons, ProcessDetails ThisProcess)
        {
            GUI.skin.toggle.fontSize = FontSize;


            GUI.DrawTexture(FreeRectSpace, Gray20TextureAlpha90);

            rectDraw = new Rect(FreeRectSpace.x, FreeRectSpace.y, FreeRectSpace.width, FreeRectSpace.height / 30);
            GUI.DrawTexture(rectDraw, Gray20TextureAlpha90);
            GUI.skin.label.fontSize = GUI.skin.label.fontSize + Padding / 2;
            GUI.Label(new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + Padding / 4, FreeRectSpace.width, FreeRectSpace.height / 30), "Email");
            GUI.skin.label.fontSize = GUI.skin.label.fontSize - Padding / 2;

            rectDraw.y = Mathf.RoundToInt(FreeRectSpace.y + FreeRectSpace.height / 30);
            rectDraw.height = FontSize / 5;
            GUI.DrawTexture(rectDraw, WhiteTexture);

            GUI.skin.label.fontSize = GUI.skin.label.fontSize + Padding / 3;

            //Emails para envio
            rectDraw = new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + FreeRectSpace.height / 30 + Padding, 4*FreeRectSpace.width/5, 2 * FontSize - Padding / 4);
            GUI.Label(rectDraw, "E-mails cadastrados");
            rectDraw.y += FreeRectSpace.height / 30;
            GUI.skin.textField.fontSize = FontSize;  // modificar o tamanho da fonte do textfield
            //O zero é reservado para EVA
            ThisProcess.Emails.EmailsList[1] = GUI.TextField(rectDraw, ThisProcess.Emails.EmailsList[1]);
            rectDraw.y += FreeRectSpace.height / 25;
            ThisProcess.Emails.EmailsList[2] = GUI.TextField(rectDraw, ThisProcess.Emails.EmailsList[2]);
            rectDraw.y += FreeRectSpace.height / 25;
            ThisProcess.Emails.EmailsList[3] = GUI.TextField(rectDraw, ThisProcess.Emails.EmailsList[3]);

            //Emails enviados em intervalos definidos
            rectDraw.y += 3 * FreeRectSpace.height / 30;
            GUI.Label(rectDraw, "Intervalo de envio");
            rectDraw.y += FreeRectSpace.height / 30;
            GUI.Label(rectDraw, "Enviar email a cada                  hora(s)");
            rectDraw.x += 12 * FontSize;
            rectDraw.width = 3 * FontSize;
            ThisProcess.Emails.SendIntervalString = GUI.TextField(rectDraw, ThisProcess.Emails.SendIntervalString);
            //float.TryParse(GUI.TextField(rectDraw, (ThisProcess.Emails.SendInterval).ToString()), NumberStyles.Float, CultureInfo.InvariantCulture, out ThisProcess.Emails.SendInterval);
            rectDraw.x -= 12 * FontSize;
            rectDraw.y += FreeRectSpace.height / 30;
            rectDraw.width = 4 * FreeRectSpace.width / 5;
            GUI.Label(rectDraw, "Tempo para o próximo envio: " + Common.IntuitiveTime(ThisProcess.Emails.SendMarkInterval - Time.time));
            rectDraw.y += FreeRectSpace.height / 30;
            GUI.Label(rectDraw, "Quantidade de e-mails enviados: " + ThisProcess.Emails.SendedEmails.ToString());

            GeneralButtons[4].PosX = Mathf.RoundToInt(FreeRectSpace.x + Padding);
            GeneralButtons[4].PosY = Mathf.RoundToInt(FreeRectSpace.y + 13 * FreeRectSpace.height / 30 + FreeRectSpace.height / 60);
            GeneralButtons[4].SizeX = Mathf.RoundToInt(4 * FreeRectSpace.width / 5);
            GeneralButtons[4].SizeY = Mathf.RoundToInt(FreeRectSpace.height / 25);
            //Botão de cadastro de emails
            if (Button(GeneralButtons[4]))
            {
                string TempText = "";
                float.TryParse(ThisProcess.Emails.SendIntervalString, NumberStyles.Float, CultureInfo.InvariantCulture, out ThisProcess.Emails.SendInterval);
                ThisProcess.Emails.SendMarkInterval = Time.time + ThisProcess.Emails.SendInterval * 3600f;
                if (ThisProcess.Emails.EmailsList[1]!="")
                {
                    if (!ThisProcess.Emails.EmailsList[1].Contains("@") && !ThisProcess.Emails.EmailsList[1].Contains("."))
                    {
                        TempText += "O primeiro e-mail digitado parece ser inválido, verifique se ele foi digitado corretamente. ";
                    }
                }
                if (ThisProcess.Emails.EmailsList[2] != "")
                {
                    if (!ThisProcess.Emails.EmailsList[2].Contains("@") && !ThisProcess.Emails.EmailsList[2].Contains("."))
                    {
                        TempText += "O segundo e-mail digitado parece ser inválido, verifique se ele foi digitado corretamente. ";
                    }
                }
                if (ThisProcess.Emails.EmailsList[3] != "")
                {
                    if (!ThisProcess.Emails.EmailsList[3].Contains("@") && !ThisProcess.Emails.EmailsList[3].Contains("."))
                    {
                        TempText += "O terceiro e-mail digitado parece ser inválido, verifique se ele foi digitado corretamente. ";
                    }
                }
                Speak(TempText);
                //Salva os email preenchidos no Config.ini, para posterior aproveitamento
                ConfigIni.WriteString("Emails", "ExtraEmai1", ThisProcess.Emails.EmailsList[1]);
                ConfigIni.WriteString("Emails", "ExtraEmai2", ThisProcess.Emails.EmailsList[2]);
                ConfigIni.WriteString("Emails", "ExtraEmai3", ThisProcess.Emails.EmailsList[3]);
            }
            GUI.skin.label.fontSize = FontSize;


            GeneralButtons[3].PosX = Mathf.RoundToInt(FreeRectSpace.x + Padding);
            GeneralButtons[3].PosY = Mathf.RoundToInt(FreeRectSpace.y + 15 * FreeRectSpace.height / 30);
            GeneralButtons[3].SizeX = Mathf.RoundToInt(4 * FreeRectSpace.width / 5);
            GeneralButtons[3].SizeY = Mathf.RoundToInt(FreeRectSpace.height / 25);
            //Botão de envio de emails
            if (Button(GeneralButtons[3])) {
                ThisProcess.Emails.SendEmail = true;
            }

            GUI.skin.label.fontSize = GUI.skin.label.fontSize;
        }

        //Debug COM  //melhorar........................................

        public static void DebugCOM(UIButtonData[] GeneralButtons, ProcessDetails ThisProcess)
        {
            GUI.skin.toggle.fontSize = FontSize;

            GUI.DrawTexture(FreeRectSpace, Gray20TextureAlpha90);

            rectDraw = new Rect(FreeRectSpace.x, FreeRectSpace.y, FreeRectSpace.width, FreeRectSpace.height / 30);
            GUI.DrawTexture(rectDraw, Gray20TextureAlpha90);
            GUI.skin.label.fontSize = GUI.skin.label.fontSize + Padding / 2;
            GUI.Label(new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + Padding / 4, FreeRectSpace.width, FreeRectSpace.height / 30), "Comunicação serial");
            GUI.skin.label.fontSize = GUI.skin.label.fontSize - Padding / 2;

            rectDraw.y = Mathf.RoundToInt(FreeRectSpace.y + FreeRectSpace.height / 30);
            rectDraw.height = FontSize / 5;
            GUI.DrawTexture(rectDraw, WhiteTexture);

            GUI.skin.label.fontSize = FontSize;

            rectDraw.x = rectDraw.x + Padding;
            rectDraw.y = Mathf.RoundToInt(rectDraw.y + FreeRectSpace.height / 60);
            rectDraw.height = FontSize*2;
            GUI.Label(rectDraw, "Enviar para a serial");
            rectDraw.y = Mathf.RoundToInt(rectDraw.y + FreeRectSpace.height / 30);
            rectDraw.width = FreeRectSpace.width / 3;
            COMToSend = GUI.TextField(rectDraw, COMToSend);
            GeneralButtons[1].SizeX = Mathf.RoundToInt(rectDraw.width);
            GeneralButtons[1].SizeY = Mathf.RoundToInt(rectDraw.height);
            GeneralButtons[1].PosX = Mathf.RoundToInt(rectDraw.x + rectDraw.width + Padding);
            GeneralButtons[1].PosY = Mathf.RoundToInt(rectDraw.y);
            //Botão de adicionar hash
            if (Button(GeneralButtons[1])) { COMToSend = COMToSend+" "+hash(COMToSend).ToString("D3") + "LF"; }

            GeneralButtons[2].SizeX = GeneralButtons[1].SizeX - 4 * Padding;
            GeneralButtons[2].SizeY = GeneralButtons[1].SizeY;
            GeneralButtons[2].PosX = Mathf.RoundToInt(rectDraw.x + rectDraw.width + Padding);
            GeneralButtons[2].PosY = GeneralButtons[1].PosY;

            GUI.skin.label.fontSize = FontSize;

            rectDraw.x = FreeRectSpace.x + Padding;
            rectDraw.y = Mathf.RoundToInt(FreeRectSpace.y + 4*FreeRectSpace.height / 30);
            rectDraw.width = FreeRectSpace.width / 2 - 2 * Padding;
            rectDraw.height = FreeRectSpace.height - 6 * FreeRectSpace.height / 30;
            //Resultados da porta COM
            GUILayout.BeginArea(rectDraw);
            COMDebugScrollPosition = GUILayout.BeginScrollView(COMDebugScrollPosition, GUILayout.Width(rectDraw.width), GUILayout.Height(rectDraw.height - 10));

                int numLines = COMString.Split('\n').Length;
                if (numLines > 2000) COMString = ""; //Limpa o string da serial
                GUILayout.Label("Dados do processo:\n" + COMString, GUILayout.Width(rectDraw.width-4*Padding), GUILayout.Height(1185*(numLines+2)* FontSize/1000));

            GUILayout.EndScrollView();
            GUILayout.EndArea();

            rectDraw.x = FreeRectSpace.x + FreeRectSpace.width/2 + Padding;
            //Resultados no Log
            GUILayout.BeginArea(rectDraw);
            LogScrollPosition = GUILayout.BeginScrollView(LogScrollPosition, GUILayout.Width(rectDraw.width), GUILayout.Height(rectDraw.height - 10));

                numLines = LogString.Split('\n').Length;
                if (numLines > 4000) LogString = ""; //Limpa o string do LOG
                GUILayout.Label("Resultado do Log:\n" + LogString, GUILayout.Width(rectDraw.width-4*Padding), GUILayout.Height(1850 * (numLines + 2) * FontSize / 1000));

            GUILayout.EndScrollView();
            GUILayout.EndArea();

            rectDraw.x = FreeRectSpace.x + Padding;
            rectDraw.y = Mathf.RoundToInt(FreeRectSpace.y + FreeRectSpace.height - 2 * FreeRectSpace.height / 30);
            rectDraw.width = 8*FontSize;
            rectDraw.height = 3 * FontSize / 2;

            ScrollDebugString = GUI.Toggle(rectDraw, ScrollDebugString, "Rolar texto");
            GeneralButtons[0].SizeX = Mathf.RoundToInt(rectDraw.width);
            GeneralButtons[0].SizeY = Mathf.RoundToInt(rectDraw.height);
            GeneralButtons[0].PosX = Mathf.RoundToInt(rectDraw.x + rectDraw.width + Padding);
            GeneralButtons[0].PosY = Mathf.RoundToInt(rectDraw.y);

            if (Button(GeneralButtons[0])) { COMString = ""; }

            rectDraw.x = FreeRectSpace.x + FreeRectSpace.width/2 + Padding;
            rectDraw.width = 8 * FontSize;
            rectDraw.height = 3*FontSize/2;
            ScrollLogString = GUI.Toggle(rectDraw, ScrollLogString, "Rolar texto");

            //Botão de enviar
            if (Button(GeneralButtons[2]))
            {
                if (COMToSend.Length > 7)
                {
                    if (ThisProcess.CableConnection.Connected)
                    {
                         ThisProcess.CableConnection.ConnectionPort.Write(COMToSend);
                         COMString += "\n--> " + COMToSend;
                    }
                    else
                    {
                         Speak("Porta serial desconectada");
                    }
                }
                if (COMToSend.Length == 0)
                {
                    Speak("Menssagem vazia");
                }
                if (COMToSend.Length > 0 && COMToSend.Length <= 7)
                {
                    Speak("Menssagem incompleta");
                }
            }
        }
        //Envia um email de LOG
        public static void SendEmail(ProcessDetails ThisProcess, VirtualAssistant Assistant)
        {
            string ExceptionText = null;
            string[] Atached = { Application.dataPath + "/Resultados/Resultados.txt", Application.dataPath + "/log.txt" };
            BlockWriteInfo = true;
            string[] AtachedCopy = CopyAtachedFiles(Atached);
            BlockWriteInfo = false;
            string CorpoEmail = "<table style=\"background - color:#009BFF;width:100%;\">"
                                + "<tr style=\"background - color:#009BFF;color:#000000;\">"
                                + "<th>RELATÓRIO</th>"
                                + "</table>"
                                + "<br>"
                                + "<style>p { margin:0 } table, td, th {border: 1px solid black;} table {border-collapse: collapse;}</style>"
                                + "<p>Empresa: " + ThisProcess.CompanyName + "</p>"
                                + "<p>Nome do processo: " + ThisProcess.Name + "</p>"
                                + "<p>Código do processo: " + ThisProcess.Code + "</p>"
                                + "<p>Data: " + System.DateTime.Now.ToString("dd/MM/yyyy") + " " + System.DateTime.Now.ToString("HH:mm") + "</p>"
                                + "<p>Temperatura local: " + ThisProcess.LocalData.TempActual + "</p>"
                                + "<br>"
                                + "<p>" + GeneralReport(Assistant, ThisProcess) + "</p>"
                                + "<br>"
                                + "<p>Valores atuais: </p>"
                                + "<br>"
                                + SensorsAndAtuatorsListValues(ThisProcess) + "</p>"
                                + "<br>"
                                + "<p>Nome do dispositivo: " + SystemInfo.deviceName + "</p>"
                                + "<p>Local: " + ThisProcess.LocalData.LocationCountry + " - " + ThisProcess.LocalData.LocationState + " - " + ThisProcess.LocalData.LocationCity + "</p>"
                                + "<p>Lat: " + ThisProcess.LocalData.LocationLatitude + " Long: " + ThisProcess.LocalData.LocationLongitude + "</p>"
                                + "<p>IP Externo: " + ThisProcess.LocalData.LocationIPAdress + " IP Local: " + LocalIPAddress() + "</p>"
                                + "<p>Sistema operacional: " + Common.WindowsVersion() + "</p>"
                                + "<br>"
                                + "<p>Atenciosamente, Eva </p>"
                                + "<p>Assistente virtual de supervisão </p>";
            if (ThisProcess.Emails.EmailsList.Length > 0)
            {
                if (ThisProcess.WebConnection.Connected)
                {
                    BlockWriteInfo = true;
                    try
                    {
                        //if (File.Exists("C:\\html.html")) File.Delete("C:\\html.html");
                        //AddTextToFile("C:\\html.html", CorpoEmail);
                        Common.SendEmail(ThisProcess.Emails.EmailsList, "Relatório: " + ThisProcess.CompanyName + " - " + ThisProcess.Name + " - Computador: " + SystemInfo.deviceName, CorpoEmail, AtachedCopy, true);
                    }
                    catch (Exception e)
                    {
                        ExceptionText =  e.ToString();                // catch any exceptions
                    }
                    BlockWriteInfo = false;
                    if (ExceptionText == null)
                    {
                        Speak("Email enviado");
                        LogInfo("E-mail com as informações do processo enviado");
                        ThisProcess.Emails.SendedEmails += 1;
                    }
                    else
                    {
                        Speak("Ocorreu um erro durante o envio do e-mail");
                        LogInfo("E-mail não enviado: " + ExceptionText);
                    }
                }
                else
                {
                    Speak("Email não enviado, sem conexão com a internet");
                    LogInfo("Email não enviado, sem conexão com a internet");
                }
            }
            else
            {
                Speak("Não há e-mails cadastrados");
                LogInfo("Não há e-mails cadastrados");
            }
        }

        public static string SensorsAndAtuatorsListValues(ProcessDetails ThisProcess)
        {
            string Results = "<p><b>Sensores:</b></p>";
            Results += "<table style=\"width: 100 % \">";
            Results += "<tr>";
            Results += "<th>NOME</th>";
            Results += "<th>VALOR</th>";
            Results += "<th>INTERVALO</th>";
            Results += "<th>MÍNIMO</th>";
            Results += "<th>DATA</th>";
            Results += "<th>MÁXIMO</th>";
            Results += "<th>DATA</th>";
            Results += "</tr>";
            for (int i = 0; i < ThisProcess.Sensors.Length; i++)
            {
                Results += "<tr>";
                Results += "<td>" + ThisProcess.Sensors[i].Name + " " + ThisProcess.Sensors[i].Number + "</td>";
                if (!ThisProcess.Sensors[i].FirstReceive) 
                {
                    if (ThisProcess.Sensors[i].ActualValue <= ThisProcess.Sensors[i].MinSecureValue || ThisProcess.Sensors[i].ActualValue >= ThisProcess.Sensors[i].MaxSecureValue)
                        Results += "<td style=\"color: red; text-align:center; \">" + ThisProcess.Sensors[i].ActualValue.ToString("G4") + ThisProcess.Sensors[i].Unit + "</td>";
                    else
                        Results += "<td style=\"text-align:center; \">" + ThisProcess.Sensors[i].ActualValue.ToString("G4") + ThisProcess.Sensors[i].Unit + "</td>";
                    Results += "<td style=\"text-align:center; \">" + " [" + ThisProcess.Sensors[i].MinSecureValue.ToString("G4") + " ; " + ThisProcess.Sensors[i].MaxSecureValue.ToString() + "]" + "</td>";
                    if (ThisProcess.Sensors[i].MinValue <= ThisProcess.Sensors[i].MinSecureValue || ThisProcess.Sensors[i].MinValue >= ThisProcess.Sensors[i].MaxSecureValue) 
                        Results += "<td style=\"color: red; text-align:center; \">" + ThisProcess.Sensors[i].MinValue.ToString("G4") + ThisProcess.Sensors[i].Unit + "</td>";
                    else
                        Results += "<td style=\"text-align:center; \">" + ThisProcess.Sensors[i].MinValue.ToString("G4") + ThisProcess.Sensors[i].Unit + "</td>";
                    Results += "<td style=\"text-align:center; \">" + ThisProcess.Sensors[i].DateMinValue + "</td>";
                    if (ThisProcess.Sensors[i].MaxValue <= ThisProcess.Sensors[i].MinSecureValue || ThisProcess.Sensors[i].MaxValue >= ThisProcess.Sensors[i].MaxSecureValue)
                        Results += "<td style=\"color: red; text-align:center; \">" + ThisProcess.Sensors[i].MaxValue.ToString("G4") + ThisProcess.Sensors[i].Unit + "</td>";
                    else
                        Results += "<td style=\"text-align:center; \">" + ThisProcess.Sensors[i].MaxValue.ToString("G4") + ThisProcess.Sensors[i].Unit + "</td>";
                    Results += "<td style=\"text-align:center; \">" + ThisProcess.Sensors[i].DateMaxValue + "</td>";
                }
                if (ThisProcess.Sensors[i].FirstReceive)
                {
                    Results += "<td style=\"color: red; text-align:center; \">" + "--" + ThisProcess.Sensors[i].Unit + "</td>";
                    Results += "<td style=\"text-align:center; \">" + " [" + ThisProcess.Sensors[i].MinSecureValue.ToString() + " ; " + ThisProcess.Sensors[i].MaxSecureValue.ToString() + "]" + "</td>";
                    Results += "<td style=\"color: red; text-align:center; \">" + "--" + ThisProcess.Sensors[i].Unit + "</td>";
                    Results += "<td style=\"color: red; text-align:center; \">" + "--" + "</td>";
                    Results += "<td style=\"color: red; text-align:center; \">" + "--" + ThisProcess.Sensors[i].Unit + "</td>";
                    Results += "<td style=\"color: red; text-align:center; \">" + "--" + "</td>";
                }
                Results += "</tr>";
            }

            Results += "</table>";

            Results += "<br><p><b>Atuadores:</b></p>";
            Results += "<table style=\"width: 100 % \">";
            Results += "<tr>";
            Results += "<th>NOME</th>";
            Results += "<th>VALOR</th>";
            Results += "<th>INTERVALO</th>";
            Results += "<th>MÍNIMO</th>";
            Results += "<th>DATA</th>";
            Results += "<th>MÁXIMO</th>";
            Results += "<th>DATA</th>";
            Results += "</tr>";


            for (int i = 0; i < ThisProcess.Atuators.Length; i++)
            {
                Results += "<tr>";
                Results += "<td>" + ThisProcess.Atuators[i].Name + " " + ThisProcess.Atuators[i].Number + "</td>";
                if (!ThisProcess.Atuators[i].FirstReceive)
                {
                    Results += "<td style=\"text-align:center; \">" + ThisProcess.Atuators[i].ActualValue.ToString("G4") + ThisProcess.Atuators[i].Unit + "</td>";
                    Results += "<td style=\"text-align:center; \">" + " [" + ThisProcess.Atuators[i].MinSecureValue.ToString() + " ; " + ThisProcess.Atuators[i].MaxSecureValue.ToString() + "]" + "</td>";
                    Results += "<td style=\"text-align:center; \">" + ThisProcess.Atuators[i].MinValue.ToString("G4") + ThisProcess.Atuators[i].Unit + "</td>";
                    Results += "<td style=\"text-align:center; \">" + ThisProcess.Atuators[i].DateMinValue + "</td>";
                    Results += "<td style=\"text-align:center; \">" + ThisProcess.Atuators[i].MaxValue.ToString("G4") + ThisProcess.Atuators[i].Unit + "</td>";
                    Results += "<td style=\"text-align:center; \">" + ThisProcess.Atuators[i].DateMaxValue + "</td>";
                }
                if (ThisProcess.Atuators[i].FirstReceive)
                {
                    Results += "<td style=\"color: red; text-align:center; \">" + "--" + ThisProcess.Atuators[i].Unit + "</td>";
                    Results += "<td style=\"text-align:center; \">" + " [" + ThisProcess.Atuators[i].MinSecureValue.ToString() + " ; " + ThisProcess.Atuators[i].MaxSecureValue.ToString() + "]" + "</td>";
                    Results += "<td style=\"color: red; text-align:center; \">" + "--" + ThisProcess.Atuators[i].Unit + "</td>";
                    Results += "<td style=\"color: red; text-align:center; \">" + "--" + "</td>";
                    Results += "<td style=\"color: red; text-align:center; \">" + "--" + ThisProcess.Atuators[i].Unit + "</td>";
                    Results += "<td style=\"color: red; text-align:center; \">" + "--" + "</td>";
                }
                Results += "</tr>";
            }
            Results += "</table>";

            return Results;
        }

        public static string[] CopyAtachedFiles(string[] FilesToCopy)
        {
            string[] CopiedFiles = new string[FilesToCopy.Length];
            string filePath;
            string fileName;
            string fileExtension;
            string FullCopyName;
            for (int i = 0; i < FilesToCopy.Length; i++)
            {
                filePath = Path.GetDirectoryName(FilesToCopy[i]);
                fileName = Path.GetFileNameWithoutExtension(FilesToCopy[i]);
                fileExtension = Path.GetExtension(FilesToCopy[i]);
                FullCopyName = filePath + "/" + fileName + "_Copy" + fileExtension;
                CopiedFiles[i] = FullCopyName;
                if (File.Exists(FullCopyName)) { File.Delete(FullCopyName); }
                File.Copy(FilesToCopy[i], FullCopyName, true);
            }
            return CopiedFiles;
        }

        //Configurações
        public static void Configurations(int ConfigFont, UISliderData FontSlider)
        {
            GUI.skin.toggle.fontSize = FontSize;

            GUI.DrawTexture(FreeRectSpace, Gray20TextureAlpha90);

            rectDraw = new Rect(FreeRectSpace.x, FreeRectSpace.y, FreeRectSpace.width, FreeRectSpace.height / 30);
            GUI.DrawTexture(rectDraw, Gray20TextureAlpha90);
            GUI.skin.label.fontSize = GUI.skin.label.fontSize + Padding / 2;
            GUI.Label(new Rect(FreeRectSpace.x + Padding, FreeRectSpace.y + Padding / 4, FreeRectSpace.width, FreeRectSpace.height / 30), "Configurações");
            GUI.skin.label.fontSize = GUI.skin.label.fontSize - Padding / 2;

            rectDraw.y = Mathf.RoundToInt(FreeRectSpace.y + FreeRectSpace.height / 30);
            rectDraw.height = FontSize / 5;
            GUI.DrawTexture(rectDraw, WhiteTexture);

            GUI.skin.label.fontSize = FontSize;

            FontSlider.PosX = Mathf.RoundToInt(FreeRectSpace.x + Padding);
            FontSlider.PosY = Mathf.RoundToInt(FreeRectSpace.y + 2 * FreeRectSpace.height / 30);
            FontSlider.SizeX = Mathf.RoundToInt(FreeRectSpace.width-2*Padding);
            FontSlider.SizeY = Mathf.RoundToInt(FreeRectSpace.height/20);
            ConfigFont = Mathf.RoundToInt(Slider(FontSlider));
            if (FontSlider.Changed || FontSlider.MouseOverDotIcon) { 
                ConfigIni.WriteInt("UI", "FontSize", ConfigFont);
                FontSize = ConfigFont;
                FontSlider.Changed = false;
            }
        }

        //Verifica o Status do sensor
        public static float Status(Sensor PointedSensor)
        {
            //Detalhar e tornar mais geral, usar uma classe ou subclasse "Security" para manter essas informações públicas
            float output = 1f; //1 OK, 0 não OK
            float RiskMinimum = PointedSensor.MinSecureValue + (PointedSensor.SecurityMargin / 100f) * (PointedSensor.MaxSecureValue - PointedSensor.MinSecureValue);
            float RiskMaximum = PointedSensor.MaxSecureValue - (PointedSensor.SecurityMargin / 100f) * (PointedSensor.MaxSecureValue - PointedSensor.MinSecureValue);
            if (PointedSensor.FirstReceive) { output = 0.5f; }
            if (PointedSensor.MinValue <= PointedSensor.MinSecureValue) { output = 0.5f; }
            if (PointedSensor.MaxValue >= PointedSensor.MaxSecureValue) { output = 0.5f; }
            if (PointedSensor.ActualValue <= RiskMinimum || PointedSensor.ActualValue >= RiskMaximum) { output = 0.5f; }
            if (PointedSensor.TimeSinceLastUpdate >= TimeSinceLastUpdateFail) { output = 0.5f; }
            if (PointedSensor.TimeSinceLastUpdate >= TimeSinceLastUpdateCritical) { output = 0f; }
            if (PointedSensor.MinValue >= PointedSensor.MaxSecureValue) { output = 0f; }
            if (PointedSensor.MaxValue <= PointedSensor.MinSecureValue) { output = 0f; }
            if (PointedSensor.ActualValue <= PointedSensor.MinSecureValue || PointedSensor.ActualValue >= PointedSensor.MaxSecureValue) { output = 0f; }
            if (PointedSensor.MeanValue <= PointedSensor.MinSecureValue || PointedSensor.MeanValue >= PointedSensor.MaxSecureValue) { output = 0f; }
            return output;
        }

        //Regressao linear de dados
        public void LinearRegression(float[] X, float[] Y, out float a, out float b, out float Rsquare)
        {
            float Xmedio = Media1D(X);
            float Ymedio = Media1D(Y);
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

        //Coeficient de determinaçao
        public static void R2(int Param, float[] Real, float[] Teoric, out float CoefDet, out float CoefDetAdju)
        {
            float Media = Media1D(Real);
            float SSres = 0f;
            float SStot = 0f;
            float TotSamp = (float)Real.GetLength(0);
            for (int i = 0; i < Teoric.GetLength(0); i++)
            {
                SSres += Mathf.Pow(Real[i] - Teoric[i], 2);
            }
            for (int i = 0; i < Teoric.GetLength(0); i++)
            {
                SStot += Mathf.Pow(Real[i] - Media, 2);
            }
            CoefDet = 1f - SSres / SStot;
            CoefDetAdju = 1f - (1f - CoefDet) * ((TotSamp - 1f) / (TotSamp - (float)Param - 1));
        }

        public static float Media1D(float[] Entrada)
        {
            float CumResult = 0;
            for (int i = 0; i < Entrada.Length; i++)
            {
                CumResult += Entrada[i];
            }
            CumResult = CumResult / Entrada.Length;
            return CumResult;
        }

    }

}
