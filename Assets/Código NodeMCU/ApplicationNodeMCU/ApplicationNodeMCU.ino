#include "SupervisoryESP.h"
#include "Ticker.h"

//Define o PLC
//PLC(String model, String serialnum, String processcode, int clpnum, String empresa)
PLC ThisPLC("JOEL-0001", "0001", "PROC001", 1, "Teste");

//Cria a serial para conexão via rádio usando o HC-12
//SoftwareSerial HC12(10, 11); // HC-12 TX Pin, HC-12 RX Pin
bool RadioCom = false;         //Define que a comunicação é feita por rádio

//Cria a lista de atuadores disponíveis, deve ser coerente com o software
//Actuator(int pin, bool pwm, int number, int intensity [0-1023], bool userelay, int ciclo [0-1023], String sensorname)
//cuidado com os pinos disponiveis são diferentes do arduino
//https://www.fernandok.com/2018/05/nodemcu-esp8266-detalhes-e-pinagem.html
//D0->16,D1->5,D2->4,D3->0,D4->2(LED_BUILTIN),D5->14,D6->12,D7->13,D8->15,A0->17
//PWM pins D0 até D7
Actuator Actuators[] = {Actuator(2,false,0,512,true,10,"Teste1"),//Built in led
                        Actuator(5,true,1,0,true,10,"Teste2"),
                        Actuator(14,true,2,0,false,10,"Teste4")};
int ActuatorSize = 3;   //Tamanho do vetor, calculado posteriormente também

//Cria a lista de sensores disponíveis, deve ser coerente com o software
//Sensor::Sensor(int pin, int number, float value, String sensorname)
Sensor Sensors[] = {Sensor(0,0,20,"Temperatura1"),
                    Sensor(16,1,20,"Temperatura2"),
                    Sensor(4,2,20,"Temperatura3"),
                    Sensor(13,3,20,"Temperatura4")};
int SensorSize = 4;   //Tamanho do vetor, calculado posteriormente também

//Cria os a lista de PIDs disponiveis, deve ser coerente com o software
//PID::PID(float _kP, float _kI, float _kD, float setpoint)
PID PIDs[] = {PID(0.3, 0.004, 0.02, 29),
              PID(0.3, 0.004, 0.02, 29),
              PID(0.3, 0.004, 0.02, 29),
              PID(0.3, 0.004, 0.02, 29)
             };

//tempo do interruptor 50ms = 20 vezes por segundo
void callback();
#define TIMER_INTERVAL_MS       50
Ticker tickerObject(callback, TIMER_INTERVAL_MS);

//Tempo para envio das informações via serial, 400ms = 2,5 vezes por segundo
//9600bps ~= 1000 caracteres por segundo, uma mensagem tem no máximo 25 caracteres
//O que leva a um limite de 1000/25 = 40 mensagens por segundo
//Modificar caso haja mais de 16 sensores e atuadores no total
unsigned long TempTimelastupdate;
unsigned long TempDeltaTimeUpdate = 500;

WifiConnection LocalCom("APT01_2G", "985025359");
WiFiServer server(80);

void setup() {
  Serial.begin(115200);               //Pode alterar para 115200 quando não utilizar rádio
  Serial.setTimeout(30);            //Torna mais rápida e eficiente a comunicação serial, não alterar

  LocalCom.WifiConnect(server);

  Serial.println(ARDUINO_BOARD);
  //Initialize Ticker
  tickerObject.start();

  //Calcula os tamanhos dos vetores para conferência e uso posterior
  ActuatorSize = sizeof(Actuators) / sizeof(Actuators[0]);
  SensorSize = sizeof(Sensors) / sizeof(Sensors[0]);

  //Temporário
  //PIDs[0].Enabled=true;
}

void loop() {
  //faça o ticker ser atualizado
  tickerObject.update();
  //Lê o que recebe da serial e distribui onde for necessário
  //ThisPLC.ReadFromSerial(ThisPLC, Actuators, ActuatorSize, Sensors);
  //Envia o handshake apenas quando não está conectado
  //ThisPLC.SendHandshake();

  //Aqui deve-se incluir todos os calculos de valores dos sensores
  //Sensor[0] foi movido devido a um teste
  Sensors[1].SetValue(27 + 11 * cos(0.00008 * millis()));
  Sensors[2].SetValue(27 + 11 * exp(-0.000005 * millis())*cos(0.00008 * millis()));

  //Define o intervalo de tempo necessário para envio das informação e calculo
  if (ThisPLC.Connected && (unsigned long)(millis() - TempTimelastupdate) > TempDeltaTimeUpdate) {

    //Temporario
    Sensors[0].SetValue((10.0*Sensors[0].Value + 20.0 + (Actuators[0].Intensity/10.0))/11.0);

    //Sensors[0]
    //Sensors[0].SendInfo(ThisPLC.CLPNum);

    //Sensors[1]
    //Sensors[1].SendInfo(ThisPLC.CLPNum);

    //Sensors[2]
    //Sensors[2].SendInfo(ThisPLC.CLPNum);

    //Trabalhar no PID
    //PIDs[0].update(ThisPLC.CLPNum, Actuators, 0, Sensors, 0);
    //Temporário
    //Sensors[0].DebugSensor();
    //Actuators[0].DebugActuator();
    TempTimelastupdate = millis();
  }
  
  LocalCom.WifiRunClient(ThisPLC, server, ActuatorSize, Actuators, SensorSize, Sensors);
}

void callback()
{
  ThisPLC.MenageActuatorOnOff(Actuators, ActuatorSize, (float)TIMER_INTERVAL_MS);
}
