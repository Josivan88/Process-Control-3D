#include "Arduino.h"
#include "ESP8266WiFi.h"
//#include <SoftwareSerial.h> //necessário para a conexão via rádio

class Sensor {
public:
	int Pin;									//Pino do sensor
	int Number;									//Número do sensor no vetor
	bool PIDEnabled=false;						//Utiliza PID?
	float Value;								//Valor Atual
	String Name;								//Nome
	Sensor(int, int, float, String);			//Construtor
	void SetValue(float);						//Seta o valor
	String CreateValueMesage(int);
	void SendInfo(int);							//Enviar via serial
	void SendWifiInfo(int, WiFiClient&);		//Enviar via Wifi
	//void SendRadioInfo(int, SoftwareSerial);	//Enviar via rádio
	void DebugSensor();							//Envia informações de Debug via serial
};

class Actuator {
public:
	int Pin;
	bool PWM;
	int Number;
	bool PIDEnabled=false;
	bool StatusCicloOnOff;
	String Status = "LOW";
	int Intensity; //[0-1023]
	bool UseRelay;  //Usa Relé? pois assim a saida LOW e HIGH é invertida
	int TotalCiclesOn; //
	int CounterON; //
	int Ciclo=30;     //[0-1023]
	int TotalCiclesOff; //
	int CounterOff;     //
	String Name;
	Actuator(int, bool, int, int,bool, int, String);
	void SetIntensity(int);
	void TurnOn();
	void TurnOff();
	String CreateIntensityMesage(int);
	String CreateCicleMesage(int);
	String CreateStatusMesage(int, String);
	void SendInfo(int, String);
	void SendInfo(int);
	void SendCicleInfo(int);
	void SendIntensityOverWifi(int, WiFiClient&);
	void SendCicleOverWifi(int, WiFiClient&);
	void SendStatusOverWifi(int, WiFiClient&);
	//void SendRadioInfo(int, String, SoftwareSerial);
	//void SendRadioInfo(int, SoftwareSerial);
	void DebugActuator();
};

class PLC {
public:
	String Model;								//Protótipo
	String ProcessCode;							//Código do processo
	String SerialNum;
	int CLPNum;									//inicia de 1, 0 é broadcast, alterar quando necessário
	int TempCLPNum;								//inicia de 1, 0 é broadcast, alterar quando necessário
	unsigned long Timelastupdate;				//Tempo para marcação do envio do handshake quando desconectado
	unsigned long DeltaTimeUpdate = 500;		//Intervalo de envio do handshake quando desconectado
	String TempSerialInput;
	String TempSerialOutput;
	int TempHash;
	int TempActuatorNumber;
	int TempIntensity; //[0-1023]
	int TempCiclo;     //[0-1023]
	bool Connected;
	String Empresa;
	PLC(String, String, String, int, String);
	void SendHandshake();
	//void SendHandshake(SoftwareSerial);
	void ReadFromSerial(PLC&, Actuator[],int, Sensor[]);
	//void ReadFromRadioSerial(int, Actuator[],int, Sensor[], SoftwareSerial&);
	void MenageActuatorOnOff(Actuator[], int, float);
	void ModelName();
	void DebugPLC();
};

class WifiConnection {
public:

	bool Connected = false;

	String ssid = "APT01_2G";
	String password = "985025359";

	// Variable to store the HTTP request
	String header;

	// Current time
	unsigned long currentTime = millis();
	// Previous time
	unsigned long previousTime = 0;
	// Define timeout time in milliseconds (example: 2000ms = 2s)
	const long timeoutTime = 500;

	//Teste
	int keyIndex = 0;            // your network key Index number (needed only for WEP)

	int status = WL_IDLE_STATUS;

	int timer = 0;
	int lastTimer = 0;
	int checkInterval = 1000;

	boolean alreadyConnected = false; // whether or not the client was connected previously
	char thisChar = 0;

	//Teste

	WifiConnection(String, String);
	void WifiConnect(WiFiServer&);
	void WifiRunClient(PLC&, WiFiServer&, int, Actuator[], int, Sensor[]);
	void SendInfoOverWifi(PLC&, WiFiClient&, int, Actuator[], int, Sensor[]);

	void WifiRunClientTeste(WiFiServer&);
};

class PID {
public:

	bool Enabled = false;
	int Number;

	int Actu;
	int Sens;

	float error;
	float sample;
	float lastSample;
	float kP, kI, kD;
	float P, I, D;
	float pid;

	float setPoint;
	long lastProcess;

	PID(int, int, int, float, float, float, float);

	void addNewSample(Sensor&);

	void setSetPoint(float);

	void apply(Actuator&);

	void process();

	String CreateSensMesage(int);
	String CreateActuMesage(int);
	String CreateSetPMesage(int);
	String CreatekPMesage(int);
	String CreatekIMesage(int);
	String CreatekDMesage(int);

	String CreatePMesage(int);
	String CreateIMesage(int);
	String CreateDMesage(int);
	String CreatePIDMesage(int);

	void SendInitialInfo(int);

	void SendInfo(int);

	void update(int, Actuator[], Sensor[]);

};

