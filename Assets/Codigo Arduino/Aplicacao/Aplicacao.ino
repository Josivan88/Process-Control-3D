//SISTEMA DE PLC UTILIZANDO ARDUINO
#include "Supervisory.h"
#include "TimerOne.h" //necessário para contagem dos ciclos


//Temperatura
#include <OneWire.h>                
#include <DallasTemperature.h>

//Define o PLC
//PLC(String model, String serialnum, String processcode, int clpnum, String empresa)
PLC ThisPLC("JOEL-0001","0001","PROC001",1,"Teste");

//Cria a serial para conexão via rádio usando o HC-12
SoftwareSerial HC12(10, 11); // HC-12 TX Pin, HC-12 RX Pin
bool RadioCom = false;        //Define que a comunicação é feita por rádio

//Cria a lista de atuadores disponíveis, deve ser coerente com o software
//Actuator(int pin, bool pwm, int number, int intensity [0-255], bool userelay, int ciclo [0-255], String sensorname)
//Timer 1 usa os pinos 11 e 12 do mega (Não usar 10 e 13 também), eles não poderão ser usados
//Timer 1 usa os pinos 9 e 10 do uno, eles não poderão ser usados
Actuator Actuators[] = {Actuator(39,false,0,0,true,20,"Teste1"),
                        Actuator(40,false,1,0,true,20,"Teste2"),
                        Actuator(41,false,2,0,true,20,"Teste3"),
                        Actuator(42,false,3,0,true,20,"Teste4"),
                        Actuator(43,false,4,0,true,20,"Teste5"),
                        Actuator(44,false,5,0,true,20,"Teste6"),
                        Actuator(45,false,6,0,true,20,"Teste7"),
                        Actuator(46,false,7,0,true,20,"Teste8")};
int ActuatorSize = 8;   //Tamanho do vetor, calculado posteriormente também

//Cria a lista de sensores disponíveis, deve ser coerente com o software
//Sensor::Sensor(int pin, int number, float value, String sensorname)
Sensor Sensors[] = {Sensor(5,0,20,"Temperatura1"), 
                    Sensor(6,1,20,"Temperatura2")};
int SensorSize = 2;   //Tamanho do vetor, calculado posteriormente também

//Cria os a lista de PIDs disponiveis, deve ser coerente com o software
//PID::PID(int Number, int Sens, int Actu, float _kP, float _kI, float _kD, float setpoint)
PID PIDs[] = {PID(0,0,0,0.5,0.004,0.02,29),
              PID(1,0,0,8.5,0.004,0.02,20),
              PID(2,0,0,0.5,0.004,0.02,29),
              PID(3,0,0,0.5,0.004,0.02,29)};

int PIDsSize = 4;   //Tamanho do vetor, calculado posteriormente também

//tempo do interruptor 50ms = 20 vezes por segundo
float InterruptUpdateFrequency = 0.05; //segundos

//Tempo para envio das informações via serial, 400ms = 2,5 vezes por segundo
//9600bps ~= 1000 caracteres por segundo, uma mensagem tem no máximo 25 caracteres
//O que leva a um limite de 1000/25 = 40 mensagens por segundo
//Modificar caso haja mais de 16 sensores e atuadores no total
unsigned long TempTimelastupdate;
unsigned long TempDeltaTimeUpdate=500;

//Temperatura
OneWire ourWire(8);                //Se establece el pin 2  como bus OneWire
DallasTemperature TempSens(&ourWire); //Se declara una variable u objeto para nuestro sensor

void setup(){
  Serial.begin(115200);               //Alterar para 9600 quando utilizar rádio
  Serial.setTimeout(30);            //Torna mais rápida e eficiente a comunicação serial, não alterar
  
  HC12.begin(9600);                 // Serial port to HC12 para comunicação via rádio
  HC12.setTimeout(30);              //Torna mais rápida e eficiente a comunicação serial, não alterar
  
  Timer1.initialize(InterruptUpdateFrequency*1000000);    // initialize timer1, and set a 1 second period
  Timer1.attachInterrupt(callback);                       // attaches callback() as a timer overflow interrupt

  //Calcula os tamanhos dos vetores para conferência e uso posterior
  ActuatorSize = sizeof(Actuators) / sizeof(Actuators[0]);
  SensorSize = sizeof(Sensors) / sizeof(Sensors[0]);
  PIDsSize = sizeof(PIDs) / sizeof(PIDs[0]);

  //Inicialização dos sensores
  TempSens.begin(); 
  
  //Temporário
  //PIDs[0].Enabled=true;
  //PIDs[1].Enabled=true;
  //ThisPLC.Connected=true;
}
 
void loop(){
  //Lê o que recebe da serial e distribui onde for necessário
  if (!RadioCom) ThisPLC.ReadFromSerial(ThisPLC,Actuators, ActuatorSize, Sensors, PIDs, PIDsSize);
  if (RadioCom) ThisPLC.ReadFromRadioSerial(ThisPLC,Actuators, ActuatorSize, Sensors, PIDs, PIDsSize, HC12);
  //Envia o handshake apenas quando não está conectado
  if (!RadioCom) ThisPLC.SendHandshake();
  if (RadioCom) ThisPLC.SendHandshake(HC12);

  //Aqui deve-se incluir todos os calculos de valores dos sensores
  //Sensor de turbidez
  int sensorValue = analogRead(A0);// read the input on analog pin 0:
  float turbidity = sensorValue * (5.0 / 1024.0); // Convert the analog reading (which goes from 0 – 1023) to a voltage (0 – 5V):
  turbidity = (5.0-turbidity)*20.0; //Converte de tensão para porcentagem, sendo 5v transparente e 0v totalmente opaco

  //Sensor[0] e Sensor[1] foi movido devido a um teste

  //Sensors[2].SetValue(27+11*exp(-0.000005*millis())*sin(0.00008*millis()));
  //Sensor de temperatura
  TempSens.requestTemperatures();   //Se envía el comando para leer la temperatura
  //Sensors[3].SetValue(TempSens.getTempCByIndex(0)); //Se obtiene la temperatura en ºC

  //Define o intervalo de tempo necessário para envio das informação e calculo
  if (ThisPLC.Connected && (unsigned long)(millis() - TempTimelastupdate) > TempDeltaTimeUpdate) {

      //Temporario
      Sensors[0].SetValue(turbidity); //Sensor de turbidez na A0
      Sensors[1].SetValue((10.0*Sensors[1].Value + (Actuators[1].Intensity/10.0)+5*cos(0.00007*millis()))/11.0);

      //Sensors[0]
      if (!RadioCom)Sensors[0].SendInfo(ThisPLC.CLPNum);
      if (RadioCom)Sensors[0].SendRadioInfo(ThisPLC.CLPNum, HC12);

      //Sensors[1]
      if (!RadioCom && ThisPLC.Connected)Sensors[1].SendInfo(ThisPLC.CLPNum);
      if (RadioCom && ThisPLC.Connected)Sensors[1].SendRadioInfo(ThisPLC.CLPNum, HC12);

      //Sensors[2]
      //if (!RadioCom && ThisPLC.Connected)Sensors[2].SendInfo(ThisPLC.CLPNum);
      //if (RadioCom && ThisPLC.Connected)Sensors[2].SendRadioInfo(ThisPLC.CLPNum, HC12);

      //Sensors[3]
      //if (!RadioCom && ThisPLC.Connected)Sensors[3].SendInfo(ThisPLC.CLPNum);
      //if (RadioCom && ThisPLC.Connected)Sensors[3].SendRadioInfo(ThisPLC.CLPNum, HC12);

      //Trabalhar no PID
      PIDs[0].update(ThisPLC.CLPNum, Actuators, Sensors);
      if (!RadioCom && ThisPLC.Connected)PIDs[0].SendInfo(ThisPLC.CLPNum);
      if (RadioCom && ThisPLC.Connected) PIDs[0].SendRadioInfo(ThisPLC.CLPNum, HC12);

      PIDs[1].update(ThisPLC.CLPNum, Actuators, Sensors);
      if (!RadioCom && ThisPLC.Connected)PIDs[1].SendInfo(ThisPLC.CLPNum);
      if (RadioCom && ThisPLC.Connected) PIDs[1].SendRadioInfo(ThisPLC.CLPNum, HC12);
      //Temporário
      //Sensors[0].DebugSensor();

      TempTimelastupdate = millis();
      //Serial.print("Intensity: ");
      //Serial.println(Actuators[1].Intensity);
  }

}

void callback()
{
    ThisPLC.MenageActuatorOnOff(Actuators, ActuatorSize, InterruptUpdateFrequency, RadioCom, HC12);
}
