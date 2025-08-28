#include "SupervisoryESP.h"

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
	if (n <= 9999 && n > 999) { result[0] = Temp[0]; result[1] = Temp[1]; result[2] = Temp[2]; result[3] = Temp[3];}
	if (n <= 999 && n > 99) { result[0] = '0'; result[1] = Temp[0]; result[2] = Temp[1]; result[3] = Temp[2]; }
	if (n <= 99 && n > 9) { result[0] = '0'; result[1] = '0'; result[2] = Temp[0]; result[3] = Temp[1]; }
	if (n <= 9 && n > 0) { result[0] = '0'; result[1] = '0'; result[2] = '0'; result[3] = Temp[0]; }
	if (n == 0) { result[0] = '0'; result[1] = '0'; result[2] = '0'; result[3] = '0';}
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
	if (n <= 99 && n > 9) { result[0] = Temp[0]; result[1] = Temp[1];}
	if (n <= 9 && n > 0) { result[0] = '0'; result[1] = Temp[0];}
	if (n == 0) { result[0] = '0'; result[1] = '0';}
	return String(result).substring(0, 2);
}
//Gerencia o ligamento e desligamento dos equipamentos // não pode conter Delay()
void PLC::MenageActuatorOnOff(Actuator Actuators[], int ActuatorsSize, float InterruptUpdateFrequency)
{
	//InterruptUpdateFrequency (ms)
	InterruptUpdateFrequency = ((float)InterruptUpdateFrequency) / ((float)1000);
	//Serial.println("Timer tick");
	//Serial.print("Frequency");
	//Serial.println(InterruptUpdateFrequency);
	Serial.flush();
	//MenageActuatorOnOff(Actuators, InterruptUpdateFrequency);
	//PWM não testado e com problemas de atualização
	for (int i = 0; i < ActuatorsSize; i++) {
		if (!Actuators[i].PWM) {

			Actuators[i].TotalCiclesOn = (Actuators[i].Intensity * Actuators[i].Ciclo) / (1023 * InterruptUpdateFrequency);//verificar limite de 1023
			Actuators[i].TotalCiclesOff = Actuators[i].Ciclo / InterruptUpdateFrequency - Actuators[i].TotalCiclesOn;

			//Actuators[i].DebugActuator();

			if (Actuators[i].StatusCicloOnOff) {
				Actuators[i].CounterON += 1;
				if (Actuators[i].CounterON > Actuators[i].TotalCiclesOn) {
					Actuators[i].StatusCicloOnOff = false;
					Actuators[i].CounterON = 0;
					if (Actuators[i].Intensity < 1023) {
						Actuators[i].TurnOff();
						if (Connected)Actuators[i].SendInfo(CLPNum, Actuators[i].Status);
						//if (RadioCom && Connected)Actuators[i].SendRadioInfo(CLPNum, "LOW", HC12);
					}
				}
				else {
					Actuators[i].TurnOn();
				}
			}

			if (!Actuators[i].StatusCicloOnOff) {
				Actuators[i].CounterOff += 1;
				if (Actuators[i].CounterOff > Actuators[i].TotalCiclesOff) {//verificar limite de 1023
					Actuators[i].StatusCicloOnOff = true;
					Actuators[i].CounterOff = 0;
					if (Actuators[i].Intensity > 0) {
						Actuators[i].TurnOn();
						if (Connected)Actuators[i].SendInfo(CLPNum, Actuators[i].Status);
						//if (RadioCom && Connected)Actuators[i].SendRadioInfo(CLPNum, "HIGH", HC12);
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
/*
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
*/

void DistributeInfo(String Input, PLC& ThisPLC, Actuator Actuators[], int ActuatorsSize, Sensor Sensors[]) {
	if (Input.length() > 0)
	{
		//Serial.print("Input: ");
		//Serial.println(Input);
		//Serial.println(" ");
		//Espera por "ConnectionON" da serial para confirmação

		if (Input[0] == 'P' && Input[3] == 'A' && Input[15] == 'L' && Input[16] == 'F') {
			ThisPLC.TempHash = hash(Input.substring(0, 11));
			ThisPLC.TempCLPNum = Input.substring(1, 3).toInt();
			if (ThisPLC.TempHash == Input.substring(12, 15).toInt() && ThisPLC.TempCLPNum == ThisPLC.CLPNum) {

				if (Input[6] == 'V') {
					ThisPLC.TempActuatorNumber = Input.substring(4, 7).toInt();
					ThisPLC.TempIntensity = Input.substring(7, 11).toInt();  //[0-1023]
					Actuators[ThisPLC.TempActuatorNumber].Intensity = ThisPLC.TempIntensity;
					if (Actuators[ThisPLC.TempActuatorNumber].PWM) Actuators[ThisPLC.TempActuatorNumber].TurnOn();
					//Input = "";
					ThisPLC.Connected = true;
				}
				if (Input[6] == 'C') {
					ThisPLC.TempActuatorNumber = Input.substring(4, 7).toInt();
					ThisPLC.TempCiclo = Input.substring(7, 11).toInt();  //[0-1023 segundos]
					Actuators[ThisPLC.TempActuatorNumber].Ciclo = ThisPLC.TempCiclo;
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
		DistributeInfo(TempSerialInput, ThisPLC, Actuators, ActuatorsSize, Sensors);
	}
}
/*
//Receber dados da serial via radio (HC12)
//Se atualizar , atualizar também ReadFromSerial
void PLC::ReadFromRadioSerial(int CLPNum, Actuator Actuators[], int ActuatorsSize, Sensor Sensors[], SoftwareSerial &_HC12)
{

	while (_HC12.available()) {
		//Serial.print("disp");
		//Serial1.flush();
		delay(10);
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
					Actuators[i].SendRadioInfo(CLPNum, _HC12);
				}
			}
			//Fecha a conexão caso receba ConnectionOFF
			if (TempSerialInput[0] == 'C' && TempSerialInput[1] == 'o' && TempSerialInput[2] == 'n' && TempSerialInput[3] == 'n' && TempSerialInput[10] == 'O' && TempSerialInput[11] == 'F' && TempSerialInput[12] == 'F') {
				Connected = false;
			}

			if (TempSerialInput[0] == 'P' && TempSerialInput[4] == 'A' && TempSerialInput[16] == 'L' && TempSerialInput[17] == 'F') {
				TempHash = hash(TempSerialInput.substring(0, 12));
				TempCLPNum = TempSerialInput.substring(1, 4).toInt();
				if (TempHash == TempSerialInput.substring(13, 16).toInt() && TempCLPNum == CLPNum) {

					if (TempSerialInput[8] == 'V') {
						TempActuatorNumber = TempSerialInput.substring(5, 8).toInt();
						TempIntensity = TempSerialInput.substring(9, 13).toInt();  //[0-255] -> [0-100]
						Actuators[TempActuatorNumber].Intensity = TempIntensity;
						if (Actuators[TempActuatorNumber].PWM) Actuators[TempActuatorNumber].TurnOn();
						//TempSerialInput = "";
						Connected = true;
					}
					if (TempSerialInput[8] == 'C') {
						TempActuatorNumber = TempSerialInput.substring(5, 8).toInt();
						TempCiclo = TempSerialInput.substring(9, 13).toInt();  //[0-255 segundos]
						Actuators[TempActuatorNumber].Ciclo = TempCiclo;
						//TempSerialInput = "";
						Connected = true;
					}
				}
			}
		}
	}
}
*/
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

//

//Atuador
Actuator::Actuator(int pin, bool pwm, int number, int intensity, bool userelay, int ciclo, String sensorname)
{
	Pin = pin;
	digitalWrite(pin, HIGH); //Starts on low state
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
	//this is because it is acive low on the ESP8266.
	if (!PWM) {
		Status = "HIGH";
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
	//this is because it is acive low on the ESP8266.
	if (!PWM) {
		Status = "LOW";
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
	ToSend = ToSend + IntToString2(CLPNum);                 //01 -> PLC number 01
	ToSend = ToSend + "A";                                  //S -> from Sensor
	ToSend = ToSend + IntToString2(Number);                 //Sensor Number
	ToSend = ToSend + "I";                                  //Intensidade
	ToSend = ToSend + IntToString4(Intensity);              //[0000-1023]
	ToSend = ToSend + " " + IntToString3(hash(ToSend));     //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}

String Actuator::CreateStatusMesage(int CLPNum, String Status)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString2(CLPNum);                 //01 -> PLC number 01
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
	ToSend = ToSend + IntToString4(Ciclo);					//Cicle time in seconds [0-1023] //this is because it is acive low on the ESP8266.
	ToSend = ToSend + " " + IntToString3(hash(ToSend));     //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}

void Actuator::SendInfo(int CLPNum, String Status)
{
	Serial.flush();
	Serial.println(CreateStatusMesage(CLPNum, Status));
	//delay(30);
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

void Actuator::SendIntensityOverWifi(int CLPNum, WiFiClient& client)
{
	client.println(CreateIntensityMesage(CLPNum));
	delay(5);
	client.flush();
}

void Actuator::SendCicleOverWifi(int CLPNum, WiFiClient& client)
{
	client.println(CreateCicleMesage(CLPNum));
	delay(5);
	client.flush();
}

void Actuator::SendStatusOverWifi(int CLPNum, WiFiClient& client)
{
	//String Status = "LOW";
	//int state = digitalRead(Pin);
	//if (state==1 && UseRelay) Status = "HIGH";
	//if (state == 0 && !UseRelay) Status = "HIGH";
	client.println(CreateStatusMesage(CLPNum, Status));
	delay(5);
	client.flush();
}

/*
void Actuator::SendRadioInfo(int CLPNum, String Status, SoftwareSerial _HC12)
{
	_HC12.flush();
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString(CLPNum);                  //001 -> PLC number 001
	ToSend = ToSend + "A";                                  //A -> from Actuator
	ToSend = ToSend + IntToString(Number);                  //Actuator Number
	ToSend = ToSend + "S";                                  //Enviando Status
	ToSend = ToSend + Status;								//Status HIGH or LOW
	ToSend = ToSend + " " + IntToString(hash(ToSend));      //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	_HC12.println(ToSend);
	delay(30);
	_HC12.flush();
}

void Actuator::SendRadioInfo(int CLPNum, SoftwareSerial _HC12)
{
	_HC12.flush();
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString(CLPNum);                  //001 -> PLC number 001
	ToSend = ToSend + "A";                                  //S -> from Sensor
	ToSend = ToSend + IntToString(Number);                  //Sensor Number
	ToSend = ToSend + "I";                                  //Intensidade
	ToSend = ToSend + IntToString(Intensity);               //[000-255]
	ToSend = ToSend + " " + IntToString(hash(ToSend));      //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	_HC12.println(ToSend);
	delay(30);
	_HC12.flush();
}
*/

void Actuator::DebugActuator() {

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
	Serial.print("Status: ");
	Serial.println(Status);
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

void Sensor::SendWifiInfo(int CLPNum, WiFiClient &client)
{
	client.println(CreateValueMesage(CLPNum));
	delay(40);
	client.flush();
}

String Sensor::CreateValueMesage(int CLPNum)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString2(CLPNum);                 //001 -> PLC number 001
	ToSend = ToSend + "S";                                  //S -> from Sensor
	ToSend = ToSend + IntToString2(Number);                 //Sensor Number
	ToSend = ToSend + "V";                                  //Enviando Valor
	ToSend = ToSend + FloatToString(Value, 10);             //0000000000 -> Value
	ToSend = ToSend + " " + IntToString3(hash(ToSend));     //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	return ToSend;
}

/*
void Sensor::SendRadioInfo(int CLPNum, SoftwareSerial _HC12)
{
	String ToSend = "P";                                    //P -> PLC
	ToSend = ToSend + IntToString(CLPNum);                  //001 -> PLC number 001
	ToSend = ToSend + "S";                                  //S -> from Sensor
	ToSend = ToSend + IntToString(Number);                  //Sensor Number
	ToSend = ToSend + "V";                                  //Enviando Valor
	ToSend = ToSend + FloatToString(Value, 10);             //0000000000 -> Value
	ToSend = ToSend + " " + IntToString(hash(ToSend));      //Checksum
	ToSend = ToSend + "LF";                                 //LF -> Final
	_HC12.println(ToSend);
	delay(20);
	_HC12.flush();
}
*/

void Sensor::DebugSensor() {

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

//Wifi

WifiConnection::WifiConnection(String _ssid, String _password) {
	ssid = _ssid;
	password = _password;
}

void WifiConnection::WifiConnect(WiFiServer &server) {
	// Connect to Wi-Fi network with SSID and password
	Serial.print("Connecting to ");
	Serial.println(ssid);
	WiFi.begin(ssid, password);
	while (WiFi.status() != WL_CONNECTED) {
		delay(500);
		Serial.print(".");
	}
	// Print local IP address and start web server
	Serial.println("");
	Serial.println("WiFi connected.");
	Serial.println("IP address: ");
	Serial.println(WiFi.localIP());
	// print the received signal strength:
	long rssi = WiFi.RSSI();
	Serial.print("signal strength (RSSI):");
	Serial.print(rssi);
	Serial.print(" dBm");
	Serial.print(" Quality: ");
	Serial.print(100+rssi);
	Serial.println("%");

	if (WiFi.status() == WL_CONNECTED) {
		Connected = true;
	}
	server.begin();
}

void WifiConnection::WifiRunClientTeste(WiFiServer &server) {
	WiFiClient client = server.available();   // Listen for incoming clients

	if (client) {                             // If a new client connects,
		Serial.println("New Client.");          // print a message out in the serial port
		String currentLine = "";                // make a String to hold incoming data from the client
		currentTime = millis();
		previousTime = currentTime;
		//client.println("PLC001- JOEL - P001");
		while (client.connected() && currentTime - previousTime <= timeoutTime) { // loop while the client's connected
			delay(1);
			currentTime = millis();
			if (client.available()) {             // if there's bytes to read from the client,
				char c = client.read();             // read a byte, then

				header += c;
				if (c == '\n') {                    // if the byte is a newline character
					Serial.print("Recebido: ");
					Serial.println(c);                    // print it out the serial monitor
				  // if the current line is blank, you got two newline characters in a row.
				  // that's the end of the client HTTP request, so send a response:
					if (currentLine.length() == 0) {
						// HTTP headers always start with a response code (e.g. HTTP/1.1 200 OK)
						// and a content-type so the client knows what's coming, then a blank line:
						client.println("HTTP/1.1 200 OK");
						client.println("Content-type:text/html");
						client.println("Connection: close");
						client.println();

						// Display the HTML web page
						client.println("<!DOCTYPE html><html>");
						client.println("<head><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
						client.println("<p>PLC001- JOEL - P001</p>");
						client.println("<meta http-equiv=\"refresh\" content=\"1\" >"); //atualiza uma vez a cada segundo a página
						client.println("<link rel=\"icon\" href=\"data:,\">");
						// CSS to style the on/off buttons 
						// Feel free to change the background-color and font-size attributes to fit your preferences
						client.println("<style>html { font-family: Helvetica; display: inline-block; margin: 0px auto; text-align: center;}");
						client.println(".button { background-color: #195B6A; border: none; color: white; padding: 16px 40px;");
						client.println("text-decoration: none; font-size: 30px; margin: 2px; cursor: pointer;}");
						client.println(".button2 {background-color: #77878A;}</style></head>");

						// Web Page Heading
						client.println("<body><h1>ESP8266 Web Server</h1>");

						//Mostra o tempo atual na página (troca de informações)
						client.println("<p>Tempo: " + String(currentTime, DEC) + "</p>");

						client.println("</body></html>");

						// The HTTP response ends with another blank line
						client.println();
						// Break out of the while loop
						break;
					}
					else { // if you got a newline, then clear currentLine
						currentLine = "";
					}
				}
				else if (c != '\r') {  // if you got anything else but a carriage return character,
					currentLine += c;      // add it to the end of the currentLine
				}
			}
		}
		// Clear the header variable
		//delay(1000);
		header = "";
		// Close the connection
		//client.stop();
		//Serial.println("Client disconnected.");
		Serial.println("");
	}
}

void WifiConnection::WifiRunClient(PLC& ThisPLC, WiFiServer& server, int ActuatorsSize, Actuator Actuators[], int SensorsSize, Sensor Sensors[]) {

	WiFiClient client = server.available();   // Wait for a new client

	timer = millis(); // timer equals current time
	// When the client sends the first byte, say hello:
	if (client) { //if there is a client
		if (!alreadyConnected) { //and its not already connected
			client.flush();          // clead out the input buffer:
			Serial.println("Client has connected to Arduino");
			client.println("PLC001- JOEL - PROC001");       /////Modificar
			delay(50);
			client.println("Tempo: " + String(millis(), DEC));
			delay(50);
			SendInfoOverWifi(ThisPLC, client, ActuatorsSize, Actuators, SensorsSize, Sensors);
			alreadyConnected = true; // remember the client is now connected
		}
		lastTimer = timer;
	}
	else { // if there is no client
		alreadyConnected = false;
		if (timer - lastTimer > checkInterval) { // if the current time minus the last time is more than the check interval
			Serial.println("No client available");
			lastTimer = timer;
		}
	}

	while (client.available()>0) {  // if a client is available
		delay(1);
		thisChar = client.read();   // Read the bytes incoming from the client:
		header += thisChar;
		if (thisChar == '\n') {

			Serial.print("Recebido: ");
			Serial.println(header);

			if (header.length() > 0)
			{
				if (header[0] == 'C' && header[1] == 'o' && header[2] == 'n' && header[3] == 'n' && header[10] == 'O' && header[11] == 'N') {
					ThisPLC.Connected = true;
					//Primeiro envio da situação
					for (int i = 0; i < ActuatorsSize; i++) {
						//delay(1);
						Actuators[i].SendIntensityOverWifi(ThisPLC.CLPNum, client);
						Actuators[i].SendCicleOverWifi(ThisPLC.CLPNum, client);
					}
					delay(30);
					for (int i = 0; i < ActuatorsSize; i++) {
						Actuators[i].SendIntensityOverWifi(ThisPLC.CLPNum, client);
						Actuators[i].SendCicleOverWifi(ThisPLC.CLPNum, client);
					}
					//Serial.print("Conexão confirmada: ");
					//Serial.println(Connected);
				}
				//Fecha a conexão caso receba ConnectionOFF
				if (header[0] == 'C' && header[1] == 'o' && header[2] == 'n' && header[3] == 'n' && header[10] == 'O' && header[11] == 'F' && header[12] == 'F') {
					ThisPLC.Connected = false;
					//Serial.print("Conexão fechada: ");
					//Serial.println(Connected);
				}
			}

			DistributeInfo(header, ThisPLC, Actuators, ActuatorsSize, Sensors);
		}
	}

	client.flush();
	header = "";
}

void WifiConnection::SendInfoOverWifi(PLC& ThisPLC, WiFiClient& client, int ActuatorsSize, Actuator Actuators[], int SensorsSize, Sensor Sensors[]) {

	for (int i = 0; i < ActuatorsSize; i++) {
		//Actuators[i].SendIntensityOverWifi(ThisPLC.CLPNum, client);
		Actuators[i].SendStatusOverWifi(ThisPLC.CLPNum, client);
	}
	for (int j = 0; j < SensorsSize; j++) {
		Sensors[j].SendWifiInfo(ThisPLC.CLPNum, client);
	}

}
//PID

PID::PID(float _kP, float _kI, float _kD, float setpoint) {
	setPoint = setpoint;
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

	if (_Actuator.Intensity >= 1023)
	{
		I = 0; 	//Anti Reset WindUp
		_Actuator.Intensity = 1023;
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


void PID::update(int CLPNum, Actuator Actuators[], int ActNum, Sensor Sensors[], int SensNum) {
	if (Enabled) {
		addNewSample(Sensors[SensNum]);
		process();
		apply(Actuators[ActNum]);
		Actuators[ActNum].SendInfo(CLPNum);
	}
}