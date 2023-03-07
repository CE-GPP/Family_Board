/*

 * Uses MIFARE RFID card using RFID-RC522 reader
 * Uses MFRC522 - Library
 * -----------------------------------------------------------------------------------------
 *             MFRC522      Arduino      
 *             Reader/PCD   Feather Huzzah 
 * Signal      Pin          Pin          
 * -----------------------------------------------------------------------------------------
 * RST/Reset   RST          0           
 * SPI SS      SDA(SS)      15            
 * SPI MOSI    MOSI         13
 * SPI MISO    MISO         12
 * SPI SCK     SCK          14
*/

#include <SPI.h>
#include <MFRC522.h>
 
#define SS_PIN 15
#define RST_PIN 0

MFRC522 rfid(SS_PIN, RST_PIN); 
 
// initialize array to store NUID that has been read
byte nuidPICC[4];

const int numOfCards = 2;//the nuber of cards. this can change as you want
byte cards[numOfCards][4] = {{0xC3, 0x08, 0x30, 0x95},{0x73, 0x25, 0x3F, 0x92}}; // array of UIDs of rfid cards
int n = 0;//n is for the total number of family members//j is for to detect the card is valid or not
int numCard[numOfCards]; //this array content the details of cards that already detect or not .
String names[numOfCards] = {"MOM","DAD"};//each family member
int fLocations[numOfCards] = {0,0};//family member locations


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
  int j = -1;
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

  for (int i = 0; i < numOfCards; i++) {
    if (nuidPICC[0] == cards[i][0] && nuidPICC[1] == cards[i][1] && nuidPICC[2] == cards[i][2] && nuidPICC[3] == cards[i][3]) {
      j = i;
    }
  }
  if(j == -1) {//check the card validity
    invalid();
  }
  else {
    //send data to display and save
    logCardData(names[j], fLocations[j], j);
  }
  delay(1000);
  
  // set IC card placed in the reading area enters hibernation state, no longer read the card repeatedly
  rfid.PICC_HaltA();
 
  // Stop reading the card module code
  rfid.PCD_StopCrypto1();

}

void invalid(){
  Serial.println("Invalid Card.");
}

void logCardData(String name, long fLocation, int j){
  fLocations[j] = fLocations[j] + 1;
  if (fLocations[j]==4)
  {
    fLocations[j]=0;
  }
  numCard[j] = 1;//put 1 in the numCard array : numCard[j]={1,1} to let the arduino know if the card was detecting

  //display details to the console (serial monitor)
  Serial.print(fLocation); //print location of member
  Serial.println();
  Serial.print(name); //print name of student
  Serial.println();

}


