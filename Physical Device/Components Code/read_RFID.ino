#include <SPI.h>
#include <MFRC522.h>
 
#define SS_PIN 10
#define RST_PIN 9

MFRC522 rfid(SS_PIN, RST_PIN); 
 
// initialize array to store NUID that has been read
byte nuidPICC[4];
 
void setup() { 
  Serial.begin(9600);
  SPI.begin(); // initialize SPI
  rfid.PCD_Init(); // initialize MFRC522 
}
 
void loop() {
 
  // find card
  if ( ! rfid.PICC_IsNewCardPresent())
    return;
 
  // verify if NUID is readable
  if ( ! rfid.PICC_ReadCardSerial())
    return;
 
  MFRC522::PICC_Type piccType = rfid.PICC_GetType(rfid.uid.sak);
 
  // Check whether the card type is MIFARE
  if (piccType != MFRC522::PICC_TYPE_MIFARE_MINI &&  
    piccType != MFRC522::PICC_TYPE_MIFARE_1K &&
    piccType != MFRC522::PICC_TYPE_MIFARE_4K) {
    Serial.println("Type not support");
    return;
  }
  
  // Save the NUID to the nuidPICC array
  for (byte i = 0; i < 4; i++) {
    nuidPICC[i] = rfid.uid.uidByte[i];
  }   
  Serial.print("hex UID:");
  printHex(rfid.uid.uidByte, rfid.uid.size);
  Serial.println();
  
  Serial.print("deci UID:");
  printDec(rfid.uid.uidByte, rfid.uid.size);
  Serial.println();
  
  // set IC card placed in the reading area enters hibernation state, no longer read the card repeatedly
  rfid.PICC_HaltA();
 
  // Stop reading the card module code
  rfid.PCD_StopCrypto1();
}
 
void printHex(byte *buffer, byte bufferSize) {
  for (byte i = 0; i < bufferSize; i++) {
    Serial.print(buffer[i] < 0x10 ? " 0" : "");
    Serial.print(buffer[i], HEX);
  }
}
 
void printDec(byte *buffer, byte bufferSize) {
  for (byte i = 0; i < bufferSize; i++) {
    Serial.print(buffer[i] < 0x10 ? " 0" : "");
    Serial.print(buffer[i], DEC);
  }
}