/*
 * Family Board - Physical device 
 *
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

//library used in the project, for LCD RFID Reader, Wifi MQTT
#include <SPI.h>
#include <MFRC522.h>
#include <ESP8266WiFi.h>
#include <ESP8266WebServer.h>
#include <ezTime.h>
#include <PubSubClient.h>
#include <FastLED.h>
#include <Wire.h>
#include <LiquidCrystal_I2C.h>

//pin define
#define LED_PIN     16
#define NUM_LEDS    96

//LED strip brightness
int brightness = 50; 

CRGB leds[NUM_LEDS];
 
#define SS_PIN 15
#define RST_PIN 0

MFRC522 rfid(SS_PIN, RST_PIN); 
 
// initialize array to store NUID that has been read
byte nuidPICC[4];
const int numOfCards = 4;//the nuber of cards. this can change as you want
byte cards[numOfCards][4] = {{0xC3, 0x08, 0x30, 0x95},{0x73, 0x25, 0x3F, 0x92},{0x73, 0x25, 0x3F, 0x93},{0x73, 0x25, 0x3F, 0x94}}; // array of UIDs of rfid cards
int n = 0;//n is for the total number of family members
//j is for to detect the card is valid or not
int numCard[numOfCards]; //this array content the details of cards that already detect or not .
String names[numOfCards] = {"Dad","Mom","Daughter","Son"};//each family member
//1-Home,2-Study,3-Work,4-Else;
int fLocations[numOfCards] = {1,1,1,1};//family member locations


// Wifi and MQTT
#include "arduino_secrets.h" 

//sensitive data saved in arduino secret file
const char* ssid     = SECRET_SSID;
const char* password = SECRET_PASS;
const char* mqttuser = SECRET_MQTTUSER;
const char* mqttpass = SECRET_MQTTPASS;

//MQTT and WiFi connection
ESP8266WebServer server(80);
const char* mqtt_server = "mqtt.cetools.org";
WiFiClient espClient;
PubSubClient client(espClient);
long lastMsg = 0;
char msg[50];
int value = 0;

// Date and time
Timezone GB;

//LCD Display
int totalColumns = 16;
int totalRows = 2;
//I2C pins declaration for 1602A with I2C module
LiquidCrystal_I2C lcd(0x27, totalColumns, totalRows);

void setup() { 
  Serial.begin(9600);
  SPI.begin(); // initialize SPI
  rfid.PCD_Init(); // initialize MFRC522 

    // run initialisation functions
  startWifi();
  syncDate();

  FastLED.addLeds<WS2812, LED_PIN, GRB>(leds, NUM_LEDS);
  FastLED.setBrightness(brightness);

  // start MQTT server
  client.setServer(mqtt_server, 1884);
  client.setCallback(callback);

  // set up the LCD's number of columns and rows:
   lcd.init();
   lcd.backlight();  // use to turn on and turn off LCD back light

}
 
void loop() {
  //verify MQTT connection
  if (!client.connected()) {
    reconnect();
  }
  client.loop();
 
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
    sendMQTT();
  }

  
  // set IC card placed in the reading area enters hibernation state, no longer read the card repeatedly
  rfid.PICC_HaltA();
 
  // Stop reading the card module code
  rfid.PCD_StopCrypto1();

}
//function for invalid information
void invalid(){
  Serial.println("Invalid Card.");
  lcd.clear();
  // Print a message to the LCD.
  lcd.setCursor(0, 0);
  lcd.print("Card not defined");
}

//function for Change family member location
void logCardData(String name, long fLocation, int j){
  //change card location
  fLocations[j] = fLocations[j] + 1;
  if (fLocations[j]==5)
  {
    fLocations[j]=1;
  }
  numCard[j] = 1;//put 1 in the numCard array : numCard[j]={1,1} to let the arduino know if the card was detecting

  //display details to the console (serial monitor)
  Serial.print("Family member: ");
  Serial.println(name); //print name of family member
  Serial.print("Location: ");
  Serial.println(fLocation); //print location of member
  Serial.println();
  showLED();
  FastLED.show();

}

//function for change led strip light
void showLED(){
  Serial.println("change light");
  FastLED.clearData();
  //dad location
  switch (fLocations[0]) {
    case 1:  // home
    for (int i = 72; i <= 75; i++) {
    leds[i] = CRGB (0, 0, 255);
    }
      break;
    case 2:  // study
    for (int i = 79; i <= 82; i++) {
    leds[i] = CRGB ( 0, 0, 255);
    }
      break;
    case 3:  // work
    for (int i = 86; i <= 89; i++) {
    leds[i] = CRGB ( 0, 0, 255);
    }  
      break;
    case 4:  // else
    for (int i = 93; i <= 95; i++) {
    leds[i] = CRGB ( 0, 0, 255); 
    }
      break;
  }
 // FastLED.show();
  //mom location
  switch (fLocations[1]) {
    case 1:  // home
    for (int i = 48; i <= 51; i++) {
    leds[i] = CRGB ( 204, 85, 0); 
    }
      break;
    case 2:  // study
    for (int i = 55; i <= 58; i++) {
    leds[i] = CRGB ( 204, 85, 0);
    }
      break;
    case 3:  // work
    for (int i = 62; i <= 65; i++) {
    leds[i] = CRGB (204, 85, 0);
    }  
      break;
    case 4:  // else
    for (int i = 69; i <= 71; i++) {
    leds[i] = CRGB ( 204, 85, 0);
    }
      break;
  }
    FastLED.show();
  //Daugther location
  switch (fLocations[3]) {
    case 1:  // home
    for (int i = 24; i <= 27; i++) {
    leds[i] = CRGB (255,20,147);
    }
      break;
    case 2:  // study
    for (int i = 31; i <= 34; i++) {
    leds[i] = CRGB ( 255,20,147);
    }
      break;
    case 3:  // work
    for (int i = 38; i <= 41; i++) {
    leds[i] = CRGB ( 255,20,147);
    }  
      break;
    case 4:  // else
    for (int i = 45; i <= 47; i++) {
    leds[i] = CRGB ( 255,20,147);
    }
      break;
  }
    FastLED.show();
  //son location
  switch (fLocations[2]) {
    case 1:  // home
    for (int i = 0; i <= 3; i++) {
    leds[i] = CRGB ( 0, 255, 0);
    }
      break;
    case 2:  // study
    for (int i = 7; i <= 10; i++) {
    leds[i] = CRGB ( 0, 255, 0);
    }
      break;
    case 3:  // work
    for (int i = 14; i <= 17; i++) {
    leds[i] = CRGB ( 0, 255, 0);
    }  
      break;
    case 4:  // else
    for (int i = 21; i <= 23; i++) {
    leds[i] = CRGB ( 0, 255, 0);
    }
      break;
  }
    FastLED.show();
}

//Wifi connnection
void startWifi() {
  // We start by connecting to a WiFi network
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);
  WiFi.begin(ssid, password);

  // check to see if connected and wait until you are
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.println("WiFi connected");
  Serial.print("IP address: ");
  Serial.println(WiFi.localIP());
}

void syncDate() {
  // get real date and time
  waitForSync();
  Serial.println("UTC: " + UTC.dateTime());
  GB.setLocation("Europe/London");
  Serial.println("London time: " + GB.dateTime());

}

//send message to MQTT
void sendMQTT() {

  if (!client.connected()) {
    reconnect();
  }
  client.loop();
  int MOML = fLocations[1];
  snprintf (msg, 50, "%.0i", MOML);
  Serial.print("Publish message for mom: ");
  Serial.println(msg);
  client.publish("student/ucfnnie/FamilyBoard/Mom/MomLocation", msg);

 
  snprintf (msg, 50, "%.0i", fLocations[0]);
  Serial.print("Publish message for Dad: ");
  Serial.println(msg);
  client.publish("student/ucfnnie/FamilyBoard/Dad/DadLocation", msg);

  
  snprintf (msg, 50, "%.0i", fLocations[2]);
  Serial.print("Publish message for Daughter: ");
  Serial.println(msg);
  client.publish("student/ucfnnie/FamilyBoard/Daughter/DaughterLocation", msg);

 
  snprintf (msg, 50, "%.0i", fLocations[3]);
  Serial.print("Publish message for Son: ");
  Serial.println(msg);
  client.publish("student/ucfnnie/FamilyBoard/Son/SonLocation", msg);

}

//callback receive function
void callback(char* topic, byte* payload, unsigned int length) {
  Serial.print("Message arrived [");
  Serial.print(topic);
  Serial.print("] ");
  for (int i = 0; i < length; i++) {
    Serial.print((char)payload[i]);
  }
  Serial.println();
  char str[length+1];
  if(strcmp(topic,"student/ucfnnie/FamilyBoard/Dad/DadLocation")==0 ){
    payload[length] = '\0'; // Add a NULL to the end of the char* to make it a string.
    int aNumber = atoi((char *)payload);
    fLocations[0] = aNumber;
  }
   if(strcmp(topic,"student/ucfnnie/FamilyBoard/Mom/MomLocation")==0 ){
    payload[length] = '\0'; // Add a NULL to the end of the char* to make it a string.
    int aNumber = atoi((char *)payload);
    fLocations[1] = aNumber;
  }
   if(strcmp(topic,"student/ucfnnie/FamilyBoard/Daughter/DaughterLocation")==0 ){
    payload[length] = '\0'; // Add a NULL to the end of the char* to make it a string.
    int aNumber = atoi((char *)payload);
    fLocations[2] = aNumber;
  }
   if(strcmp(topic,"student/ucfnnie/FamilyBoard/Son/SonLocation")==0 ){
    payload[length] = '\0'; // Add a NULL to the end of the char* to make it a string.
    int aNumber = atoi((char *)payload);
    fLocations[3] = aNumber;
  }
    if(strcmp(topic,"student/ucfnnie/FamilyBoard/Dad/DadElse")==0 ){
    for (int i = 0; i < length; i++) {
      // Serial.print((char)payload[i]);
      str[i] = (char)payload[i];
      lcd.clear();
      // Print a message to the LCD.
       // (note: line 1 is the second row, since counting begins with 0):
      lcd.setCursor(0, 0);
      lcd.print("Dad:");
    }
  }
   if(strcmp(topic,"student/ucfnnie/FamilyBoard/Daughter/DaughterElse")==0 ){
    for (int i = 0; i < length; i++) {
      // Serial.print((char)payload[i]);
      str[i] = (char)payload[i];
      lcd.clear();
      // Print a message to the LCD.
       // (note: line 1 is the second row, since counting begins with 0):
      lcd.setCursor(0, 0);
      lcd.print("Daughter:");
    }
  }
   if(strcmp(topic,"student/ucfnnie/FamilyBoard/Son/SonElse")==0 ){
    for (int i = 0; i < length; i++) {
      // Serial.print((char)payload[i]);
      str[i] = (char)payload[i];
      lcd.clear();
      // Print a message to the LCD.
       // (note: line 1 is the second row, since counting begins with 0):
      lcd.setCursor(0, 0);
      lcd.print("Son:");
    }
  }
   if(strcmp(topic,"student/ucfnnie/FamilyBoard/Mom/MomElse")==0 ){
    for (int i = 0; i < length; i++) {
      // Serial.print((char)payload[i]);
      str[i] = (char)payload[i];
      lcd.clear();
      // Print a message to the LCD.
       // (note: line 1 is the second row, since counting begins with 0):
      lcd.setCursor(0, 0);
      lcd.print("Mom:");
    }
  }
  showLED();
  FastLED.show();
   lcd.setCursor(0, 1);
  lcd.print(str);
}

//reconnect to MQTT
void reconnect() {
  // Loop until we're reconnected
  while (!client.connected()) {
    Serial.print("Attempting MQTT connection...");
    // Create a random client ID
    String clientId = "ESP8266Client-";
    clientId += String(random(0xffff), HEX);
    
    // Attempt to connect with clientID, username and password
    if (client.connect(clientId.c_str(), mqttuser, mqttpass)) {
      Serial.println("connected");
      // ... and resubscribe
      client.subscribe("student/ucfnnie/FamilyBoard/Dad/DadLocation");
      client.subscribe("student/ucfnnie/FamilyBoard/Dad/DadElse");
      client.subscribe("student/ucfnnie/FamilyBoard/Mom/MomLocation");
      client.subscribe("student/ucfnnie/FamilyBoard/Mom/MomElse");
      client.subscribe("student/ucfnnie/FamilyBoard/Daughter/DaughterLocation");
      client.subscribe("student/ucfnnie/FamilyBoard/Daughter/DaughterElse");
      client.subscribe("student/ucfnnie/FamilyBoard/Son/SonLocation");
      client.subscribe("student/ucfnnie/FamilyBoard/Son/SonElse");
    } else {
      Serial.print("failed, rc=");
      Serial.print(client.state());
      Serial.println(" try again in 5 seconds");
      // Wait 5 seconds before retrying
      delay(5000);
    }
  }
}


