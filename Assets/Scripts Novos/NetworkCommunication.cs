using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Supervisory;

public class NetworkCommunication : MonoBehaviourPun, IPunObservable
{
    public bool SendChat = false;
    public string SendString;
    public string ReceivedString="nada ainda";
    public int Ping;
    public PhotonView photonView;
    public GameObject CentralObject;
    public Aplication Central;
    public NetworkLauncher Netlauncher;
    public bool AskFirstReceiveFromMaster = false;
    public bool FirstReceiveFromMaster = true;
    //public float[] ActuatorsMarkers;
    public float UpdateTime = 2f;
    public float UpdateInterval = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

        Central = CentralObject.GetComponent<Aplication>();
        Netlauncher = gameObject.GetComponent<NetworkLauncher>();
        /*
        ActuatorsMarkers = new float[Central.ThisProcess.Atuators.Length];
        for (int i = 0; i < Central.ThisProcess.Atuators.Length; i++)
        {
            ActuatorsMarkers[i] = Central.ThisProcess.Atuators[i].ActualValue;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= UpdateTime)
        {
            if (Netlauncher.host)
            {
                for (int i = 0; i < Central.ThisProcess.Sensors.Length; i++)
                {
                    if (Central.ThisProcess.Sensors[i].TimeSinceLastUpdate < 10f && !Central.ThisProcess.Sensors[i].FirstReceive)//Só envie caso o valor seja atual, ou seja, tenha menos de 10 segundos e tenha recebido de forma legitima do processo
                    {
                        photonView.RPC("SendSensorValue", RpcTarget.Others, Central.ThisProcess.Sensors[i].ActualValue, i);
                        Central.ThisProcess.Sensors[i].ReqNetUpdate = false;
                    }
                }
                //Envio inicial de informações do mestre, pedir aqui tudo necessário para uma sincronização
                if (AskFirstReceiveFromMaster)
                {
                    for (int i = 0; i < Central.ThisProcess.Atuators.Length; i++) //Para todos os atuadores
                    {
                        photonView.RPC("SendActuatorValue", RpcTarget.Others, Central.ThisProcess.Atuators[i].ActualValue, Central.ThisProcess.Atuators[i].OnOffCicle, Central.ThisProcess.Atuators[i].ActualStatus, i);
                        Central.ThisProcess.Atuators[i].ReqNetUpdate = false;
                        //ActuatorsMarkers[i] = Central.ThisProcess.Atuators[i].ActualValue;
                        AskFirstReceiveFromMaster = false;
                    }
                }
            }
            else
            {
                if (AskFirstReceiveFromMaster && Netlauncher.client)
                {
                    photonView.RPC("Sinchronize", RpcTarget.MasterClient);
                    AskFirstReceiveFromMaster = false;
                }
            }

            Ping = PhotonNetwork.GetPing();

            if (Netlauncher.host || Netlauncher.client)
            {
                for (int i = 0; i < Central.ThisProcess.Atuators.Length; i++) //Para todos os atuadores
                {
                    if (Central.ThisProcess.Atuators[i].ReqNetUpdate)
                    {
                        photonView.RPC("SendActuatorValue", RpcTarget.Others, Central.ThisProcess.Atuators[i].ActualValue, Central.ThisProcess.Atuators[i].OnOffCicle, Central.ThisProcess.Atuators[i].ActualStatus, i);
                        Central.ThisProcess.Atuators[i].ReqNetUpdate = false;
                    }
                }

                for (int i = 0; i < Central.ThisProcess.PIDs.Length; i++) //Para todos os PIDs
                {
                    if (Central.ThisProcess.PIDs[i].ReqNetUpdate)
                    {
                        photonView.RPC("SendPIDInfo", RpcTarget.Others,
                            Central.ThisProcess.PIDs[i].Enabled,
                            Central.ThisProcess.PIDs[i].SensorNum,
                            Central.ThisProcess.PIDs[i].ActuatorNum,
                            Central.ThisProcess.PIDs[i].setPoint,
                            Central.ThisProcess.PIDs[i].KP,
                            Central.ThisProcess.PIDs[i].KI,
                            Central.ThisProcess.PIDs[i].KD,
                            Central.ThisProcess.PIDs[i].P,
                            Central.ThisProcess.PIDs[i].I,
                            Central.ThisProcess.PIDs[i].D,
                            Central.ThisProcess.PIDs[i].pid,
                            i);
                        Central.ThisProcess.PIDs[i].ReqNetUpdate = false;
                    }
                }
            }
            if (SendChat)
            {
                photonView.RPC("SendData", RpcTarget.All, SendString);
                SendChat = false;
            }

            UpdateTime += UpdateInterval;
        }
    }

    void OnGUI()
    {

    }

    [PunRPC]
    public void Sinchronize() //Pedido de sincronização do master
    {
        AskFirstReceiveFromMaster = true;
    }

    [PunRPC]
    public void SendData(string DataString) //Envia texto -> Chat?
    {
        ReceivedString += "\n" + DataString;
    }

    [PunRPC]
    public void SendSensorValue(float value, int SensorNumber)
    {
        Central.ThisProcess.RemoteProcessConnection.TimeLastUpdate = Time.time;
        Central.ThisProcess.Sensors[SensorNumber].TimeLastUpdateMark = Time.time;
        Central.ThisProcess.Sensors[SensorNumber].TimeSinceLastUpdate = 0f;
        Functions.ReceiveSensorValue(Central.ThisProcess.Sensors, SensorNumber, value);
    }

    [PunRPC]
    public void SendActuatorValue(float value, float Cicle, bool Status, int ActuatorNumber)
    {
        Central.ThisProcess.RemoteProcessConnection.TimeLastUpdate = Time.time;
        Central.ThisProcess.Atuators[ActuatorNumber].ActualValue = value;
        Central.ThisProcess.Atuators[ActuatorNumber].OnOffCicle = Cicle;
        Central.ThisProcess.Atuators[ActuatorNumber].ActualStatus = Status;
        Central.ThisProcess.Atuators[ActuatorNumber].AtuadorUI.IntensitySlider.Value = Central.ThisProcess.Atuators[ActuatorNumber].ActualValue;
        Central.ThisProcess.Atuators[ActuatorNumber].TimeLastUpdateMark = Time.time;
        Central.ThisProcess.Atuators[ActuatorNumber].AtuadorUI.IntensitySlider.Changed = true;
        if (ActuatorNumber == Central.ThisProcess.Atuators.Length - 1) { FirstReceiveFromMaster = false; } //Se atualizou todos até o último
    }

    [PunRPC]
    public void SendPIDInfo(bool Status, int Sensor, int Actuator, float SetPoint, float kP, float kI, float kD, float P, float I, float D, float PID, int PIDNumber)
    {
        Central.ThisProcess.RemoteProcessConnection.TimeLastUpdate = Time.time;
        Central.ThisProcess.PIDs[PIDNumber].Enabled = Status;
        Central.ThisProcess.PIDs[PIDNumber].SensorNum = Sensor;
        Central.ThisProcess.PIDs[PIDNumber].SensorUsedInCombobox.SelectedItem = Sensor + 1;
        Central.ThisProcess.PIDs[PIDNumber].ActuatorNum = Actuator;
        Central.ThisProcess.PIDs[PIDNumber].ActuatorUsedInCombobox.SelectedItem = Actuator + 1;
        Central.ThisProcess.PIDs[PIDNumber].setPoint = SetPoint;
        Central.ThisProcess.PIDs[PIDNumber].KP = kP;
        Central.ThisProcess.PIDs[PIDNumber].KI = kI;
        Central.ThisProcess.PIDs[PIDNumber].KD = kD;
        Central.ThisProcess.PIDs[PIDNumber].P = P;
        Central.ThisProcess.PIDs[PIDNumber].I = I;
        Central.ThisProcess.PIDs[PIDNumber].D = D;
        Central.ThisProcess.PIDs[PIDNumber].pid = PID;

    }

    public void OnPhotonSerializeView(PhotonStream Stream, PhotonMessageInfo info)
    {
        if (Stream.IsReading)
        {
            //ReceivedString = Stream.ReceiveNext();
        }
    }
}
