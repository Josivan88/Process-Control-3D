#include "Arduino.h"
#include <SoftwareSerial.h> //necessário para a conexão via rádio

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
	void SendRadioInfo(int, SoftwareSerial);	//Enviar via rádio
	void Debug();							//Envia informações de Debug via serial
};

class Actuator {
public:
	int Pin;
	bool PWM;
	int Number;
	bool PIDEnabled=false;
	bool StatusCicloOnOff;
	int Intensity; //[0-255]
	bool UseRelay;  //Usa Relé? pois assim a saida LOW e HIGH é invertida
	int TotalCiclesOn; //
	int CounterON; //
	int Ciclo=30;     //[0-255]
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
	void SendRadioInfo(int, String, SoftwareSerial);
	void SendRadioInfo(int, SoftwareSerial);
	void SendCicleRadioInfo(int, SoftwareSerial);
	void Debug();
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
	void SendInitialRadioInfo(int, SoftwareSerial);

	void SendInfo(int);
	void SendRadioInfo(int, SoftwareSerial);

	void update(int, Actuator[], Sensor[]);

};

class PLC {
public:
	String Model;								//Protótipo
	String ProcessCode;							//Código do processo
	String SerialNum;
	int CLPNum;									//inicia de 1, 0 é broadcast, alterar quando necessário
	int TempCLPNum;								//inicia de 1, 0 é broadcast, alterar quando necessário
	unsigned long Timelastupdate;				//Tempo para marcação do envio do handshake quando desconectado
	unsigned long DeltaTimeUpdate = 3000;		//Intervalo de envio do handshake quando desconectado
	String TempSerialInput;
	String TempSerialOutput;
	int TempHash;
	int TempNumber;
	int TempIntensity; //[0-255]
	int TempCiclo;     //[0-255]
	bool Connected;
	String Empresa;
	PLC(String, String, String, int, String);
	void SendHandshake();
	void SendHandshake(SoftwareSerial);
	void ReadFromSerial(PLC&, Actuator[], int, Sensor[], PID[], int);
	void ReadFromRadioSerial(PLC&, Actuator[],int, Sensor[], PID[], int, SoftwareSerial&);
	void MenageActuatorOnOff(Actuator[], int, float, bool, SoftwareSerial&);
	void ModelName();
	void DebugPLC();
};

