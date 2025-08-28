#include "Supervisory.h"

//Funções comuns

String FloatToString(float f, int len)
{
	char result[len + 1];     //result string len positions + \0 at the end
	dtostrf(f, len, 4, result);
	return result;
}

String IntToString4(int n)
{
	char result[4];
	String Temp = String(n);
	if (n <= 9999 && n > 999) { result[0] = Temp[0]; result[1] = Temp[1]; result[2] = Temp[2]; result[3] = Temp[3]; }
	if (n <= 999 && n > 99) { result[0] = '0'; result[1] = Temp[0]; result[2] = Temp[1]; result[3] = Temp[2]; }
	if (n <= 99 && n > 9) { result[0] = '0'; result[1] = '0'; result[2] = Temp[0]; result[3] = Temp[1]; }
	if (n <= 9 && n > 0) { result[0] = '0'; result[1] = '0'; result[2] = '0'; result[3] = Temp[0]; }
	if (n == 0) { result[0] = '0'; result[1] = '0'; result[2] = '0'; result[3] = '0'; }
	return String(result).substring(0, 4);
}

String IntToString3(int n)
{
	char result[3];
	String Temp = String(n);
	if (n <= 999 && n > 99) { result[0] = Temp[0]; result[1] = Temp[1]; result[2] = Temp[2]; }
	if (n <= 99 && n > 9) { result[0] = '0'; result[1] = Temp[0]; result[2] = Temp[1]; }
	if (n <= 9 && n > 0) { result[0] = '0'; result[1] = '0'; result[2] = Temp[0]; }
	if (n == 0) { result[0] = '0'; result[1] = '0'; result[2] = '0'; }
	return String(result).substring(0, 3);
}

String IntToString2(int n)
{
	char result[2];
	String Temp = String(n);
	if (n <= 99 && n > 9) { result[0] = Temp[0]; result[1] = Temp[1]; }
	if (n <= 9 && n > 0) { result[0] = '0'; result[1] = Temp[0]; }
	if (n == 0) { result[0] = '0'; result[1] = '0'; }
	return String(result).substring(0, 2);
}

void PLC::MenageActuatorOnOff(Actuator Actuators[], int ActuatorsSize, float InterruptUpdateFrequency, bool RadioCom, SoftwareSerial &HC12)
{

	Serial.flush();
	//MenageActuatorOnOff(Actuators, InterruptUpdateFrequency);
	//PWM não testado e com problemas de atualização
	for (int i = 0; i < ActuatorsSize; i++) {

		if (!Actuators[i].PWM) {

			Actuators[i].TotalCiclesOn = (Actuators[i].Intensity * Actuators[i].Ciclo) / (255 * InterruptUpdateFrequency);//verificar limite de 255
			Actuators[i].TotalCiclesOff = Actuators[i].Ciclo / InterruptUpdateFrequency - Actuators[i].TotalCiclesOn;

			if (Actuators[i].StatusCicloOnOff) {
				Actuators[i].CounterON += 1;
				if (Actuators[i].CounterON > Actuators[i].TotalCiclesOn) {
					Actuators[i].StatusCicloOnOff = false;
					Actuators[i].CounterON = 0;
					if (Actuators[i].Intensity < 255) {
						Actuators[i].TurnOff();
						if (Connected)Actuators[i].SendInfo(CLPNum, "LOW");
						if (RadioCom && Connected)Actuators[i].SendRadioInfo(CLPNum, "LOW", HC12);
					}
				}
				else {
					Actuators[i].TurnOn();
				}
			}

			if (!Actuators[i].StatusCicloOnOff) {
				Actuators[i].CounterOff += 1;
				if (Actuators[i].CounterOff > Actuators[i].TotalCiclesOff) {//verificar limite de 255
					Actuators[i].StatusCicloOnOff = true;
					Actuators[i].CounterOff = 0;
					if (Actuators[i].Intensity > 0) {
						Actuators[i].TurnOn();
						if (Connected)Actuators[i].SendInfo(CLPNum, "HIGH");
						if (RadioCom && Connected)Actuators[i].SendRadioInfo(CLPNum, "HIGH", HC12);
					}
				}
				else {
					Actuators[i].TurnOff();
				}
			}
		}
		else
		{
			//é PWM
			Actuators[i].TurnOn();
		}
	}
}

int hash(String s) {
	int k = 7;
	for (int i = 0; i < s.length(); i++) {
		k += (int)s[i]; //ASCII number
		k *= 3;
		k -= 13;
		k %= 999;
	}
	return abs(k);
}

//Envio sistemático do handshake
void PLC::SendHandshake()
{
	//Um possivel problema seria overflow após 49 dias, mas em teoria tudo está ok, porem não testado
	if (!Connected && (unsigned long)(millis()- Timelastupdate)> DeltaTimeUpdate) {
		Serial.print(Model);
		Serial.print(" ");
		Serial.println(ProcessCode);
		Timelastupdate = millis();
	}
}

//Envio sistemático do handshake
void PLC::SendHandshake(SoftwareSerial _HC12)
{
	//Um possivel problema seria overflow após 49 dias, mas em teoria tudo está ok, porem não testado
	if (!Connected && (unsigned long)(millis() - Timelastupdate) > DeltaTimeUpdate) {
		_HC12.print(Model);
		_HC12.print(" ");
		_HC12.println(ProcessCode);
		Timelastupdate = millis();
	}
}

void DistributeInfo(String Input, PLC& ThisPLC, Actuator Actuators[], int ActuatorsSize, Sensor Sensors[], PID PIDs[]) {
	if (Input.length() > 0)
	{
		//Serial.print("Input: ");
		//Serial.println(Input);
		//Serial.println(" ");
		//Espera por "ConnectionON" da serial para confirmação

		//Atuadores
		if (Input[0] == 'P' && Input[3] == 'A' && Input[15] == 'L' && Input[16] == 'F') {
			ThisPLC.TempHash = hash(Input.substring(0, 11));
			ThisPLC.TempCLPNum = Input.substring(1, 3).toInt();
			if (ThisPLC.TempHash == Input.substring(12, 15).toInt() && ThisPLC.TempCLPNum == ThisPLC.CLPNum) {

				if (Input[6] == 'V') {
					ThisPLC.TempNumber = Input.substring(4, 7).toInt();
					ThisPLC.TempIntensity = Input.substring(7, 11).toInt();  //[0-256]
					Actuators[ThisPLC.TempNumber].Intensity = ThisPLC.TempIntensity;
					if (Actuators[ThisPLC.TempNumber].PWM) Actuators[ThisPLC.TempNumber].TurnOn();
					//Input = "";
					ThisPLC.Connected = true;
				}
				if (Input[6] == 'C') {
					ThisPLC.TempNumber = Input.substring(4, 7).toInt();
					ThisPLC.TempCiclo = Input.substring(7, 11).toInt();  //[0-256 segundos]
					Actuators[ThisPLC.TempNumber].Ciclo = ThisPLC.TempCiclo;
					//Input = "";
					ThisPLC.Connected = true;
				}
			}
		}
		//PIDs
		if (Input[0] == 'P' && Input[3] == 'P' && Input[15] == 'L' && Input[16] == 'F') {
			ThisPLC.TempHash = hash(Input.substring(0, 11));
			ThisPLC.TempCLPNum = Input.substring(1, 3).toInt();
			if (ThisPLC.TempHash == Input.substring(12, 15).toInt() && ThisPLC.TempCLPNum == ThisPLC.CLPNum) {

				if (Input[6] == 'S') {
					ThisPLC.TempNumber = Input.substring(4, 7).toInt();
					ThisPLC.TempIntensity = Input.substring(7, 11).toInt();  //Numero do sensor utilizado no PID
					PIDs[ThisPLC.TempNumber].Sens = ThisPLC.TempIntensity;
					PIDs[ThisPLC.TempNumber].Enabled = true;
					Serial.println("----------Sensor recebido");
					//Input = "";
					ThisPLC.Connected = true;
				}
				if (Input[6] == 'C') {
					ThisPLC.TempNumber = Input.substring(4, 7).toInt();
					ThisPLC.TempIntensity = Input.substring(7, 11).toInt();  //Numero do atuador utilizado no PID
					PIDs[ThisPLC.TempNumber].Actu = ThisPLC.TempIntensity;
					PIDs[ThisPLC.TempNumber].Enabled = true;
					Serial.println("----------Atuador recebido");
					//Input = "";
					ThisPLC.Connected = true;
				}
			}
		}
	}
}


//Receber dados da serial
//Se atualizar , atualizar também ReadFromRadioSerial
void PLC::ReadFromSerial(PLC& ThisPLC, Actuator Actuators[], int ActuatorsSize, Sensor Sensors[], PID PIDs[], int PIDsSize)
{

	while (Serial.available() > 0) {
		//Serial1.flush();
		delay(20);
		/*
		for (int i = 0; i < 18; i++)
		{
			delay(5);
			char c = Serial.read();
			TempSerialInput += c;
		}
		*/
		TempSerialInput = Serial.readString();
		if (TempSerialInput.length() > 0)
		{
			if (TempSerialInput[0] == 'C' && TempSerialInput[1] == 'o' && TempSerialInput[2] == 'n' && TempSerialInput[3] == 'n' && TempSerialInput[10] == 'O' && TempSerialInput[11] == 'N') {
				ThisPLC.Connected = true;
				//Primeiro envio da situação
				for (int i = 0; i < ActuatorsSize; i++) {
					//delay(1);
					Actuators[i].SendInfo(ThisPLC.CLPNum);
					Actuators[i].SendCicleInfo(ThisPLC.CLPNum);
				}
				delay(50);
				for (int i = 0; i < ActuatorsSize; i++) {
					Actuators[i].SendInfo(ThisPLC.CLPNum);
					Actuators[i].SendCicleInfo(ThisPLC.CLPNum);
				}
				delay(50);
				for (int j = 0; j < PIDsSize; j++) {
					if (PIDs[j].Enabled)
					PIDs[j].SendInitialInfo(ThisPLC.CLPNum);
				}
				delay(50);
				for (int j = 0; j < PIDsSize; j++) {
					if (PIDs[j].Enabled)
					PIDs[j].SendInitialInfo(ThisPLC.CLPNum);
				}
				//Serial.print("Conexão confirmada: ");
				//Serial.println(Connected);
			}
			//Fecha a conexão caso receba ConnectionOFF
			if (TempSerialInput[0] == 'C' && TempSerialInput[1] == 'o' && TempSerialInput[2] == 'n' && TempSerialInput[3] == 'n' && TempSerialInput[10] == 'O' && TempSerialInput[11] == 'F' && TempSerialInput[12] == 'F') {
				ThisPLC.Connected = false;
				//Serial.print("Conexão fechada: ");
				//Serial.println(Connected);
			}
		}
		DistributeInfo(TempSerialInput, ThisPLC, Actuators, ActuatorsSize, Sensors, PIDs);
	}
}

//Receber dados da serial via radio (HC12)
//Se atualizar , atualizar também ReadFromSerial
void PLC::ReadFromRadioSerial(PLC& ThisPLC, Actuator Actuators[], int ActuatorsSize, Sensor Sensors[], PID PIDs[], int PIDsSize, SoftwareSerial &_HC12)
{

	while (_HC12.available()) {
		//Serial.print("disp");
		//Serial1.flush();
		delay(20);
		TempSerialInput = _HC12.readString();
		if (TempSerialInput.length() > 0)
		{
			//Serial.print("TempSerialInput: ");
			//Serial.println(TempSerialInput);
			//Serial.println(" ");
			//Espera por ConnectionON da serial para confirmação
			if (TempSerialInput[0] == 'C' && TempSerialInput[1] == 'o' && TempSerialInput[2] == 'n' && TempSerialInput[3] == 'n' && TempSerialInput[10] == 'O' && TempSerialInput[11] == 'N') {
				Connected = true;
				for (int i = 0; i < ActuatorsSize; i++) {
					Actuators[i].SendRadioInfo(ThisPLC.CLPNum, _HC12);
					Actuators[i].SendCicleRadioInfo(ThisPLC.CLPNum, _HC12);
				}
				delay(30);
				for (int i = 0; i < ActuatorsSize; i++) {
					Actuators[i].SendRadioInfo(ThisPLC.CLPNum, _HC12);
					Actuators[i].SendCicleRadioInfo(ThisPLC.CLPNum, _HC12);
				}
				delay(50);
				for (int j = 0; j < PIDsSize; j++) {
					if (PIDs[j].Enabled)
					PIDs[j].SendInitialInfo(ThisPLC.CLPNum);
				}
				delay(50);
				for (int j = 0; j < PIDsSize; j++) {
					if (PIDs[j].Enabled)
					PIDs[j].SendInitialInfo(ThisPLC.CLPNum);
				}
			}
			//Fecha a conexão caso receba ConnectionOFF
			if (TempSerialInput[0] == 'C' && TempSerialInput[1] == 'o' && TempSerialInput[2] == 'n' && TempSerialInput[3] == 'n' && TempSerialInput[10] == 'O' && TempSerialInput[11] == 'F' && TempSerialInput[12] == 'F') {
				Connected = false;
			}
			DistributeInfo(TempSerialInput, ThisPLC, Actuators, ActuatorsSize, Sensors, PIDs);
		}
	}
}

/*
void Ramp(int i) {
	int DelayValue = 20;
	int dif = EquipIntensity - PinsIntensity[i];
	if (dif > 0) {
		for (int j = PinsIntensity[i]; j <= EquipIntensity; j++) {
			analogWrite(Pins[i], j);
			PinsIntensity[i] = j;
			//Serial.println(j);
			delay(DelayValue);
		}
	}

	if (dif < 0) {
		for (int j = PinsIntensity[i]; j >= EquipIntensity; j--) {
			analogWrite(Pins[i], j);
			PinsIntensity[i] = j;
			///Serial.println(j);
			delay(DelayValue);
		}
	}
}
*/

//Atuador
Actuator::Actuator(int pin, bool pwm, int number, int intensity, bool userelay, int ciclo, String sensorname)
{
	Pin = pin;
	pinMode(pin, OUTPUT);  //initializes digital pin as an output
	PWM = pwm;
	Number = number;
	Intensity = intensity;
	UseRelay = userelay;
	Ciclo = ciclo;
	Name = sensorname;
}

void Actuator::SetIntensity(int intensity)
{
	this->Intensity = intensity;
}

void Actuator::TurnOn()
{
	if (!PWM) {
		if (UseRelay)
			digitalWrite(Pin, LOW);
		if (!UseRelay)
			digitalWrite(Pin, HIGH);
	}
	else 
	{
		analogWrite(Pin, Intensity);
	}
}

void Actuator::TurnOff()
{
	if (!PWM) {
		if (UseRelay)
			digitalWrite(Pin, HIGH);
		if (!UseRelay)
			digitalWrite(Pin, LOW);
	}
	else
	{
		Intensity = 0;
		analogWrite(Pin, Intensity);
	}
}

String Actuator::CreateIntensityMesage(int CLPNum)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString2(CLPNum);                 //001 -> PLC number 001
	ToSend = ToSend + "A";                                  //S -> from Sensor
	ToSend = ToSend + IntToString2(Number);                 //Sensor Number
	ToSend = ToSend + "I";                                  //Intensidade
	ToSend = ToSend + IntToString4(Intensity);              //[0000-255]
	ToSend = ToSend + " " + IntToString3(hash(ToSend));     //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}

String Actuator::CreateStatusMesage(int CLPNum, String Status)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString2(CLPNum);                 //001 -> PLC number 001
	ToSend = ToSend + "A";                                  //A -> from Actuator
	ToSend = ToSend + IntToString2(Number);                 //Actuator Number
	ToSend = ToSend + "S";                                  //Enviando Status
	ToSend = ToSend + Status;								//Status HIGH or LOW //this is because it is acive low on the ESP8266.
	ToSend = ToSend + " " + IntToString3(hash(ToSend));     //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}

String Actuator::CreateCicleMesage(int CLPNum)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString2(CLPNum);                 //01 -> PLC number 01
	ToSend = ToSend + "A";                                  //A -> from Actuator
	ToSend = ToSend + IntToString2(Number);                 //Actuator Number
	ToSend = ToSend + "C";                                  //Ciclo
	ToSend = ToSend + IntToString4(Ciclo);					//Cicle time in seconds [0-255] //this is because it is acive low on the ESP8266.
	ToSend = ToSend + " " + IntToString3(hash(ToSend));     //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}

void Actuator::SendInfo(int CLPNum, String Status)
{
	Serial.flush();
	Serial.println(CreateStatusMesage(CLPNum, Status));
	delay(30);
	Serial.flush();
}

void Actuator::SendInfo(int CLPNum)
{
	Serial.flush();
	Serial.println(CreateIntensityMesage(CLPNum));
	delay(30);
	Serial.flush();
}

void Actuator::SendCicleInfo(int CLPNum)
{
	Serial.flush();
	Serial.println(CreateCicleMesage(CLPNum));
	delay(30);
	Serial.flush();
}

void Actuator::SendRadioInfo(int CLPNum, String Status, SoftwareSerial _HC12)
{
	_HC12.flush();
	_HC12.println(CreateStatusMesage(CLPNum, Status));
	delay(10);
	_HC12.flush();
}

void Actuator::SendRadioInfo(int CLPNum, SoftwareSerial _HC12)
{
	_HC12.flush();
	_HC12.println(CreateIntensityMesage(CLPNum));
	delay(30);
	_HC12.flush();
}

void Actuator::SendCicleRadioInfo(int CLPNum, SoftwareSerial _HC12)
{
	_HC12.flush();
	_HC12.println(CreateCicleMesage(CLPNum));
	delay(30);
	_HC12.flush();
}

void Actuator::Debug() {

	Serial.print("Name: ");
	Serial.println(Name);
	Serial.print("Pin: ");
	Serial.println(Pin);
	Serial.print("PWM: ");
	Serial.println(PWM);
	Serial.print("Number: ");
	Serial.println(Number);
	Serial.print("PIDEnabled: ");
	Serial.println(PIDEnabled);
	Serial.print("StatusCicloOnOff: ");
	Serial.println(StatusCicloOnOff);
	Serial.print("Intensity: ");
	Serial.println(Intensity);
	Serial.print("UseRelay: ");
	Serial.println(UseRelay);
	Serial.print("Ciclo: ");
	Serial.println(Ciclo);
	Serial.print("TotalCiclesOn: ");
	Serial.println(TotalCiclesOn);
	Serial.print("TotalCiclesOff: ");
	Serial.println(TotalCiclesOff);

}

//Sensor
Sensor::Sensor(int pin, int number, float value, String sensorname)
{
	Pin = pin;
	pinMode(pin, INPUT); //initializes digital pin as an input
	Number = number;
	Value = value;
	Name = sensorname;
}

void Sensor::SetValue(float value)
{
	this->Value = value;
}

void Sensor::SendInfo(int CLPNum)
{
	Serial.println(CreateValueMesage(CLPNum));
	delay(20);
	Serial.flush();
}


void Sensor::SendRadioInfo(int CLPNum, SoftwareSerial _HC12)
{
	_HC12.println(CreateValueMesage(CLPNum));
	delay(20);
	_HC12.flush();
}

String Sensor::CreateValueMesage(int CLPNum)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString2(CLPNum);                  //001 -> PLC number 001
	ToSend = ToSend + "S";                                  //S -> from Sensor
	ToSend = ToSend + IntToString2(Number);                  //Sensor Number
	ToSend = ToSend + "V";                                  //Enviando Valor
	ToSend = ToSend + FloatToString(Value, 10);             //0000000000 -> Value
	ToSend = ToSend + " " + IntToString3(hash(ToSend));      //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}

void Sensor::Debug() {

	Serial.print("Name");
	Serial.println(Name);
	Serial.print("Pin");
	Serial.println(Pin);
	Serial.print("Number");
	Serial.println(Number);
	Serial.print("Value");
	Serial.println(Value);

}

//PLC
PLC::PLC(String model, String serialnum, String processcode, int clpnum, String empresa)
{
	Model = model;              //Protótipo
	SerialNum = serialnum;
	ProcessCode = processcode;
	CLPNum = clpnum;            //inicia de 1, 0 é broadcast, alterar quando necessário
	Empresa = empresa;
}

void PLC::ModelName() {
	Serial.println("");
	Serial.println(Model);
	Serial.println("");
	delay(50);
}

void PLC::DebugPLC() {
	Serial.print("Model: ");
	Serial.println(Model);
	Serial.print("SerialNum: ");
	Serial.println(SerialNum);
	Serial.print("CLPNum: ");
	Serial.println(CLPNum);
	Serial.print("Timelastupdate: ");
	Serial.println(Timelastupdate);
	Serial.print("DeltaTimeUpdate: ");
	Serial.println(DeltaTimeUpdate);
	Serial.print("Connected: ");
	Serial.println(Connected);
	Serial.print("Empresa: ");
	Serial.println(Empresa);
}


//PID

PID::PID(int Num, int _Sens, int _Actu, float _kP, float _kI, float _kD, float setpoint) {
	Number = Num;
	setPoint = setpoint;
	Actu = _Actu;
	Sens = _Sens;
	kP = _kP;
	kI = _kI;
	kD = _kD;
}

void PID::addNewSample(Sensor &InputSensor) {
	sample = InputSensor.Value;
}

void PID::setSetPoint(float _setPoint) {
	setPoint = _setPoint;
}

void PID::apply(Actuator &_Actuator) {

	_Actuator.Intensity = (int)round((float)_Actuator.Intensity+pid);
	//Debug
	//Serial.print("Intensidade: ");
	//Serial.println(_Actuator.Intensity);

	if (_Actuator.Intensity >= 255)
	{
		I = 0; 	//Anti Reset WindUp
		_Actuator.Intensity = 255;
	}
	if (_Actuator.Intensity <= 0)
	{
		I = 0; 	//Anti Reset WindUp
		_Actuator.Intensity = 0;
	}

}

void PID::process() {
	// Implementação PID
	error = setPoint - sample;
	float deltaTime = (millis() - lastProcess) / 1000.0;
	lastProcess = millis();

	//Anti Reset WindUp
	if (P * (error) <= 0) { I = 0; }
	//o ponto de ajuste de velocidade não deve ser equilibrado por uma integral igual acima do ponto de ajuste de velocidade. 
	//Neste último caso, é desejável zerar o integrador toda vez que o erro for zero ou quando o erro mudar de sinal. 
	//Um método conveniente e robusto para determinar quando o erro muda de sinal ou é igual a zero é multiplicar o erro atual pelo erro anterior. 
	//(O erro anterior estaria disponível se um termo derivado também estivesse sendo usado.) Se o produto for zero ou negativo, o integrador deve ser zerado.

	//Resolver Bumpless Transfer -> Aplicar rampa no setpoint, para que não haja modificações bruscas no erro
	//Resolver Split flow control

	//P
	P = error * kP;

	//I
	I = I + (error * kI) * deltaTime;

	//D
	D = (lastSample - sample) * kD / deltaTime;
	lastSample = sample;

	// Soma tudo
	pid = P + I + D;

}

String PID::CreateSensMesage(int CLPNum)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString2(CLPNum);                 //01 -> PLC number 01
	ToSend = ToSend + "P";                                  //P -> from PID
	ToSend = ToSend + IntToString2(Number);                 //Pid Number
	ToSend = ToSend + "S";                                  //Source Sensor
	ToSend = ToSend + IntToString4(Sens);					//Sensor number
	ToSend = ToSend + " " + IntToString3(hash(ToSend));     //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}

String PID::CreateActuMesage(int CLPNum)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString2(CLPNum);                 //01 -> PLC number 01
	ToSend = ToSend + "P";                                  //P -> from PID
	ToSend = ToSend + IntToString2(Number);                 //Pid Number
	ToSend = ToSend + "C";                                  //Source aCtuator
	ToSend = ToSend + IntToString4(Actu);					//Actuator number
	ToSend = ToSend + " " + IntToString3(hash(ToSend));     //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}

String PID::CreateSetPMesage(int CLPNum)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString2(CLPNum);                 //001 -> PLC number 001
	ToSend = ToSend + "P";                                  //P -> from PID
	ToSend = ToSend + IntToString2(Number);                 //PID Number
	ToSend = ToSend + "A";                                  //SetPoint
	ToSend = ToSend + FloatToString(setPoint, 10);			//0000000000 -> Value
	ToSend = ToSend + " " + IntToString3(hash(ToSend));     //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}

String PID::CreatekPMesage(int CLPNum)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString2(CLPNum);                 //001 -> PLC number 001
	ToSend = ToSend + "P";                                  //P -> from PID
	ToSend = ToSend + IntToString2(Number);                 //PID Number
	ToSend = ToSend + "Q";                                  //Proportional constant term
	ToSend = ToSend + FloatToString(kP, 10);				//0000000000 -> Value
	ToSend = ToSend + " " + IntToString3(hash(ToSend));     //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}

String PID::CreatekIMesage(int CLPNum)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString2(CLPNum);                 //001 -> PLC number 001
	ToSend = ToSend + "P";                                  //P -> from PID
	ToSend = ToSend + IntToString2(Number);                 //PID Number
	ToSend = ToSend + "J";                                  //Integral constant term
	ToSend = ToSend + FloatToString(kI, 10);				//0000000000 -> Value
	ToSend = ToSend + " " + IntToString3(hash(ToSend));     //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}

String PID::CreatekDMesage(int CLPNum)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString2(CLPNum);                 //001 -> PLC number 001
	ToSend = ToSend + "P";                                  //P -> from PID
	ToSend = ToSend + IntToString2(Number);                 //PID Number
	ToSend = ToSend + "E";                                  //Derivative constant term
	ToSend = ToSend + FloatToString(kD, 10);				//0000000000 -> Value
	ToSend = ToSend + " " + IntToString3(hash(ToSend));     //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}


String PID::CreatePMesage(int CLPNum)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString2(CLPNum);                 //001 -> PLC number 001
	ToSend = ToSend + "P";                                  //P -> from PID
	ToSend = ToSend + IntToString2(Number);                 //PID Number
	ToSend = ToSend + "P";                                  //Proportional term
	ToSend = ToSend + FloatToString(P, 10);					//0000000000 -> Value
	ToSend = ToSend + " " + IntToString3(hash(ToSend));     //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}

String PID::CreateIMesage(int CLPNum)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString2(CLPNum);                 //001 -> PLC number 001
	ToSend = ToSend + "P";                                  //P -> from PID
	ToSend = ToSend + IntToString2(Number);                 //PID Number
	ToSend = ToSend + "I";                                  //Integral term
	ToSend = ToSend + FloatToString(I, 10);					//0000000000 -> Value
	ToSend = ToSend + " " + IntToString3(hash(ToSend));     //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}

String PID::CreateDMesage(int CLPNum)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString2(CLPNum);                 //001 -> PLC number 001
	ToSend = ToSend + "P";                                  //P -> from PID
	ToSend = ToSend + IntToString2(Number);                 //PID Number
	ToSend = ToSend + "D";                                  //derivative term
	ToSend = ToSend + FloatToString(D, 10);					//0000000000 -> Value
	ToSend = ToSend + " " + IntToString3(hash(ToSend));     //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}

String PID::CreatePIDMesage(int CLPNum)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString2(CLPNum);                 //001 -> PLC number 001
	ToSend = ToSend + "P";                                  //P -> from PID
	ToSend = ToSend + IntToString2(Number);                 //PID Number
	ToSend = ToSend + "T";                                  //PID sum term
	ToSend = ToSend + FloatToString(pid, 10);				//0000000000 -> Value
	ToSend = ToSend + " " + IntToString3(hash(ToSend));     //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}

void PID::SendInitialInfo(int CLPNum)
{
	Serial.flush();
	Serial.println(CreateSensMesage(CLPNum));
	delay(30);
	Serial.flush();
	Serial.println(CreateActuMesage(CLPNum));
	delay(30);
	Serial.flush();
	Serial.println(CreateSetPMesage(CLPNum));
	delay(30);
	Serial.flush();
	Serial.println(CreatekPMesage(CLPNum));
	delay(30);
	Serial.flush();
	Serial.println(CreatekIMesage(CLPNum));
	delay(30);
	Serial.flush();
	Serial.println(CreatekDMesage(CLPNum));
	delay(30);
	Serial.flush();
}

void PID::SendInitialRadioInfo(int CLPNum, SoftwareSerial _HC12)
{
	_HC12.flush();
	_HC12.println(CreateSensMesage(CLPNum));
	delay(30);
	_HC12.flush();
	_HC12.println(CreateActuMesage(CLPNum));
	delay(30);
	_HC12.flush();
	_HC12.println(CreateSetPMesage(CLPNum));
	delay(30);
	_HC12.flush();
	_HC12.println(CreatekPMesage(CLPNum));
	delay(30);
	_HC12.flush();
	_HC12.println(CreatekIMesage(CLPNum));
	delay(30);
	_HC12.flush();
	_HC12.println(CreatekDMesage(CLPNum));
	delay(30);
	_HC12.flush();
}

void PID::SendInfo(int CLPNum)
{
	Serial.flush();
	Serial.println(CreatePMesage(CLPNum));
	delay(30);
	Serial.flush();
	Serial.println(CreateIMesage(CLPNum));
	delay(30);
	Serial.flush();
	Serial.println(CreateDMesage(CLPNum));
	delay(30);
	Serial.flush();
	Serial.println(CreatePIDMesage(CLPNum));
	delay(30);
	Serial.flush();
}

void PID::SendRadioInfo(int CLPNum, SoftwareSerial _HC12)
{
	_HC12.flush();
	_HC12.println(CreatePMesage(CLPNum));
	delay(30);
	_HC12.flush();
	_HC12.println(CreateIMesage(CLPNum));
	delay(30);
	_HC12.flush();
	_HC12.println(CreateDMesage(CLPNum));
	delay(30);
	_HC12.flush();
	_HC12.println(CreatePIDMesage(CLPNum));
	delay(30);
	_HC12.flush();
}

void PID::update(int CLPNum, Actuator Actuators[], Sensor Sensors[]) {
	if (Enabled) {
		addNewSample(Sensors[Sens]);
		process();
		apply(Actuators[Actu]);
		Actuators[Actu].SendInfo(CLPNum);
	}
}