using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supervisory;
using EncryptStringSample;

public class Aplication : MonoBehaviour
{
    [Tooltip("The size of the Sensor display data")]
    [Range(0f, 20f)]
    public int Font = 10;
    public GameObject MainCamera;
    public GameObject CameraTarget;
    public GameObject NetworkObject;

    public ProcessDetails ThisProcess;
    public VirtualAssistant Eva;

    public UIButtonData[] Menu;
    public UIButtonData[] PIDButtons;
    public UIButtonData[] WindowsButtons;
    public UIButtonData[] GeneralButtons;
    public UIbuttonGroupData WindowsButtonsGroup;
    public UISliderData SetFontSize;
    public UISensorGroupData UiSensorsGroupSource;
    public UIAtuadorGroupData UiAtuadoresGroupSource;
    public UIbuttonGroupData ButtonGroup;

    // Use this for initialization
    void Start()
    {
        //Functions.Speak("Obrigado por me trazer de volta chefe");
        Functions.CreateAndInitializeDependencies(ThisProcess);                                     //Cria as dependências como pastas e arquivos, inicialização
        Functions.InitializeSensorsAndAtuators(ThisProcess);                                        //Inicializa o histórico de sensores e atuadores
        StartCoroutine(Functions.ManageProcess(ThisProcess, Eva));                                  //Gerencia as informações sgerais do processo
        StartCoroutine(Functions.ManageBattery(ThisProcess));                                       //Gerencia a bateria, caso use
        StartCoroutine(Functions.ManageInternetConnection(ThisProcess));                            //Gerencia a conexão com a internet
        StartCoroutine(Functions.ManageProcessConnection(ThisProcess));                             //Gerencia conexão com o processo
        StartCoroutine(Functions.ManageRemoteConnection(ThisProcess, NetworkObject));               //Gerencia as conexões remotas com o processo
        StartCoroutine(Functions.ManageLocalWebConnection(ThisProcess, NetworkObject));             //Gerencia a conexão e os dados de uma conexão via IP local (NodeMCU)
        StartCoroutine(Functions.ManageActuatorAndSensorsUpdates(ThisProcess));                     //Atualiza histórico de dados dos sensores e dos atuadores
        StartCoroutine(Functions.ManagePIDs(ThisProcess));                                          //Atualiza as informações referentes aos PIDs
        StartCoroutine(Functions.ManageSentInfo(ThisProcess));                                      //Gerencia o envio de informações ao arduino
        StartCoroutine(Functions.ManageReceptedInfo(ThisProcess));                                  //Melhorar //Gerencia dados recebidos do arduino
        StartCoroutine(Functions.StartVirtualAssistant(Eva));                                       //Inicia a assistente virtual, Eva
        StartCoroutine(Functions.UpdateVirtualAssistant(ThisProcess, Eva));                         //Mantém a assistente virtual funcionando, Eva
        Functions.SetCameraMoviment(MainCamera, CameraTarget);                                      //Define objeto central
        Functions.SetCoreObject(gameObject);                                                        //Define objeto central
        StartCoroutine(Functions.CollectGarbage());                                                 //Resources.UnloadUnusedAssets(); A cada Function.CollectGarbageInterval
        Functions.SetCompactSensors(UiSensorsGroupSource);                                          //Coloca sensores na forma compacta na inicialização
        Functions.SetCompactAtuadores(UiAtuadoresGroupSource);                                      //Coloca atuadores na forma compacta na inicialização
        Functions.InitializePIDs(ThisProcess, PIDButtons);                                          //Inicializa os PIDs
        Font = Functions.IdealUISize(ThisProcess,  UiSensorsGroupSource, UiAtuadoresGroupSource);   //Calcula o tamanho inicial ideal da fonnte e da interface
        Functions.SetFontSize(Mathf.RoundToInt(Font), SetFontSize);                                 //Seta o valor da fonte
    }

    // Update is called once per frame
    void Update()
    {
        Functions.BlockModelInteraction();                                          //Bloqueia a interação com o modelo 3D dependendo da situação
    }
    // Draw GUI in screen space
    void OnGUI()
    {
        
        Functions.MainMenuButtonGroup(ButtonGroup, Menu, UiAtuadoresGroupSource);//Desenha o menu principal

        if (Menu[0].Toggle)                                                      //Se o menu permitir
        {
            Functions.AtuadorsGUI(UiAtuadoresGroupSource, ThisProcess);          //Desenha os atuadores
        }
        if (Menu[1].Toggle)                                                      //Se o menu permitir
        {
            Functions.SensorsGUI(UiSensorsGroupSource, ThisProcess);             //Desenha os sensores
        }
        if (Menu[2].Selected)                                                    //Se o menu permitir
        {
            Functions.PIDDetails(ThisProcess, PIDButtons);                       //Desenhe o editor de PIDs
        }
        if (Menu[3].Selected)                                                    //Se o menu permitir
        {
            Functions.RemoteConections(ThisProcess);                             //Desenhe as informações da conexão remota
        }
        if (Menu[4].Selected)                                                    //Se o menu permitir
        {
            Functions.Email(GeneralButtons, ThisProcess);                        //Gerenciado pelo Functions.ManageProcess
        }
        if (Menu[6].Selected)                                                    //Se o menu permitir
        {
            Functions.DebugCOM(GeneralButtons, ThisProcess);                     //Desenha o Debug da porta COM
        }
        if (Menu[7].Selected)                                                    //Se o menu permitir
        {
            Functions.Configurations(Font,SetFontSize);
        }

        Functions.ObjectInteraction(ThisProcess);                                                   //Aplica alterações visuais aos modelos 3D quando há interação
        Functions.VirtualSensors(ThisProcess);                                                      //Permite o cálculo de sensores virtuais quando necessário, eles ficam marcados por *
        Functions.CalculateFreeScreenSpace(UiSensorsGroupSource, UiAtuadoresGroupSource);           //Calcula o espaço livre na tela
        Functions.MenageFreeSpace(Menu, ThisProcess);                                               //Desenha os gráficos de sensores e atuadores quando habilitados
        Functions.WindowsButtons(WindowsButtonsGroup, WindowsButtons, ThisProcess);                 //Gera os botões de maximizar minimizar e de status da bateria, arduino e internet
        Functions.CompanyLogo(ThisProcess, WindowsButtonsGroup);                                    //Desenha a logo da empre e patrocinadores
        Functions.AssistantGUI(Eva, ThisProcess);                                                   //Interface de interação com EVA
        Functions.VisualizeClick();                                                                 //Permite visualizar o clique, ajuda em telas touch
    }

    void OnApplicationQuit()
    {
        Functions.CloseSerial(ThisProcess);
        //desconectar se estiver conectado por wifi
        //desconectar se estiver conectado via internet
    }

}
