void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600); 
}

void loop() {
  // put your main code here, to run repeatedly:
  
    Serial.print("Arduino");
    Serial.println(millis());
    delay(1000);
    Serial.println((int)'A');
    Serial.println((int)'b');
    Serial.println((int)'0');
    Serial.println((int)'1');
}
