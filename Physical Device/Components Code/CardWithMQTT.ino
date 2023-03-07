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
#include <ESP8266WiFi.h>
#include <ESP8266WebServer.h>
#include <ezTime.h>
#include <PubSubClient.h>
 
#define SS_PIN 15
#define RST_PIN 0

MFRC522 rfid(SS_PIN, RST_PIN); 
 
// initialize array to store NUID that has been read
byte nuidPICC[4];

const int numOfCards = 4;//the nuber of cards. this can change as you want
byte cards[numOfCards][4] = {{0xC3, 0x08, 0x30, 0x95},{0x73, 0x25, 0x3F, 0x92},{0x73, 0x25, 0x3F, 0x93},{0x73, 0x25, 0x3F, 0x94}}; // array of UIDs of rfid cards
int n = 0;//n is for the total number of family members//j is for to detect the card is valid or not
int numCard[numOfCards]; //this array content the details of cards that already detect or not .
String names[numOfCards] = {"MOM","DAD","Daughter","Son"};//each family member
//1-Home,2-Study,3-Work,4-Else;
int fLocations[numOfCards] = {1,1,1,1};//family member locations


// Wifi and MQTT
#include "arduino_secrets.h" 

const char* ssid     = SECRET_SSID;
const char* password = SECRET_PASS;
const char* mqttuser = SECRET_MQTTUSER;
const char* mqttpass = SECRET_MQTTPASS;

ESP8266WebServer server(80);
const char* mqtt_server = "mqtt.cetools.org";
WiFiClient espClient;
PubSubClient client(espClient);
long lastMsg = 0;
char msg[50];
int value = 0;

// Date and time
Timezone GB;


void setup() { 
  Serial.begin(9600);
  SPI.begin(); // initialize SPI
  rfid.PCD_Init(); // initialize MFRC522 

    // run initialisation functions
  startWifi();
  syncDate();

  // start MQTT server
  client.setServer(mqtt_server, 1884);
  client.setCallback(callback);
}
 
void loop() {

     // handler for receiving requests to webserver
  server.handleClient();
 
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
    sendMQTT();
    //send data to display and save
    logCardData(names[j], fLocations[j], j);
  }
  delay(1000);
  
  // set IC card placed in the reading area enters hibernation state, no longer read the card repeatedly
  rfid.PICC_HaltA();
 
  // Stop reading the card module code
  rfid.PCD_StopCrypto1();

  client.loop();

}

void invalid(){
  Serial.println("Invalid Card.");
}

void logCardData(String name, long fLocation, int j){
  fLocations[j] = fLocations[j] + 1;
  if (fLocations[j]==5)
  {
    fLocations[j]=1;
  }
  numCard[j] = 1;//put 1 in the numCard array : numCard[j]={1,1} to let the arduino know if the card was detecting

  //display details to the console (serial monitor)
  Serial.print("Family member: ");
  Serial.println(name); //print name of student
  Serial.print("Location: ");
  Serial.println(fLocation); //print location of member
  Serial.println();

}

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

void sendMQTT() {

  if (!client.connected()) {
    reconnect();
  }
  client.loop();
  int MOML = fLocations[0];
  snprintf (msg, 50, "%.0i", MOML);
  Serial.print("Publish message for mom: ");
  Serial.println(msg);
  client.publish("student/ucfnnie/FamilyBoard/Mom/MomLocation", msg);

 
  snprintf (msg, 50, "%.0i", fLocations[1]);
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

void callback(char* topic, byte* payload, unsigned int length) {
  Serial.print("Message arrived [");
  Serial.print(topic);
  Serial.print("] ");
  for (int i = 0; i < length; i++) {
    Serial.print((char)payload[i]);
  }
  Serial.println();
}

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
      client.subscribe("student/ucfnnie/FamilyBoard/LCD");
    } else {
      Serial.print("failed, rc=");
      Serial.print(client.state());
      Serial.println(" try again in 5 seconds");
      // Wait 5 seconds before retrying
      delay(5000);
    }
  }
}


