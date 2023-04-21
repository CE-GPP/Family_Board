# by Team 4 for CASA0021

- Leo Liu : ucfniup@ucl.ac.uk


- Sophia Chong : sophia.chong.19@ucl.ac.uk
- Xiaoya Nie : ucfnnie@ucl.ac.uk

 We build the family board project. If you have any question, please contact us.


# Family Board

Project introduction:

Family board is a device that can allow family members upload and share their location with family members. It has a physical device and its digital twin on android unity app. All the location information is self- reported. It donot have the privacy issue (like personal location tracking). This device has a warm, home-like appearance.  

 - <img width="800" alt="image" src="https://github.com/CE-GPP/Family_Board/blob/main/Physical Device/product.jpg">

To use the physical device, user just need to tap their unique PFID tag on the "door", the window light on the device will change.
User can also change location in app. When they are at other location, they can input message in the pannel. The physical device will show the message on the LCD screen.

 - <img width="800" alt="image" src="https://github.com/CE-GPP/Family_Board/blob/main/Physical Device/a2a16a8f39e635f15a1c85eb3049ed2.jpg">

1. **


2. **

3. **

# Getting Started

## Physical Device:

### 1. Hardware required:

The hardware required for the project are:

1). Adafruit Feather HUZZAH ESP8266 

2). MRCF522 RFID module

3). WS2812b led strip: to make the led strip fit with the family board, it need to be cut in to 4 pieces, and solder with cable to rebuild the circuit connection.

4). LCD (I2C)

The function of the hardware can be found below:

 - <img width="400" alt="image" src="https://github.com/CE-GPP/Family_Board/blob/main/Physical Device/Components Code/08172beb176893227e6eea35bed5293.png">
 
### 2. Hardware code:

The code for the hardware is in the Physical device folder: CardLCD.ino.
The platform used for the project is Arduino IDE. Please download the code and upload it to the microcontroller.
The circuit connection for the RFID module can also be found in the code.

##3. Physical Model and Assembly
You will need:
1. 3mm plywood
2. Access to a laser cutter
3. 3D printer
4. wood glue
5. tape
6. multipurpose adhesive
7. transparent PLA filament
8. wood PLA filament
9. fine grain sandpaper
10. 1 piece of black paper
11. magnetic sheet
12. scissors
13. ruler

# 3.1 3D printing
Download the files in the [3D printed components folder](https://github.com/CE-GPP/Family_Board/tree/main/Physical%20Device/3D%20printed%20components) in the Physical Device folder. 
Upload both gcode files to the SD card of a 3D printer, and print the familyBoardDoor with 0.2mm wood PLA filament.
Next, print the twoFamilyBoardWindows with 0.2mm transparent PLA filament, and repeat this 8 times to print all 16 windows you need. 
Next, print the familyBoardLCDScreenHolder with 0.2mm PLA filament of your choice.

# 3.2 Laser cutting
Download both files in the [Laser cut template folder](https://github.com/CE-GPP/Family_Board/tree/main/Physical%20Device/Laser%20cut%20template) in the Physical Device folder.

3.2.1 Laser cutting the family board face
After opening the familyBoardFace file and exporting it to the laser cutter, set the **location labels** and **spaces for the magnetic family portraits**, which are the **first columnn of rectangles from the left** to **engrave** instead of **cut**. All other lines can be set to **cut**. 
After making this change, it can be laser cut onto 3mm plywood.

#3.2.2 Laser cutting the family board roof
After opening the familyBoardRoof file and exporting it to the laser cutter, ensure that all its lines are set to **cut**. It can then be laser cut onto 3mm plywood.

# 3.3 Assembling the family board
A labelled version of the components of the family board face are shown below:

-Assemble the exterior faces of the board with wood glue one by one, and tape each of glued edges together for a secure finish. In the process, ensure that all the components are orientated correctly, and check that the slots at the side of the board are close to the board's front instead of the board's back. 
-Next, glue the two walls without finger joins to the top and bottom of the board's interior. Ensure that it glued as tightly as possible, as this is going to form the rail for the lid.
-Glue the top of the rails to the back of the top and bottom faces of the board, and glue the grip rail to the sliding face.
-Glue the support beams into the sides of the board, with the side with the least amount of distance between the hole and the end of the support beam on the side of the board with the hole for the LCD screen.
-Glue the stair pieces to each other, then glue it underneath the hole for the door in the front of the board.


## Virtual Device:

### 1. Download The Virtual Device Android APP from Unity

1. Head over to the [Family Board Virtual Device folder](https://github.com/CE-GPP/Family_Board/tree/main/Unity/Family%20Board%20Virtual%20Device) and open the folder as a new project in Unity. Before you begin, make sure that your project is set up in Unity 2021.3.13f1. The Mobile application for Family Board only supports the Android platform.

2. In Unity, open the Prefab folder and double-click the "Final model" file to open it. In the Hierarchy window on the left, click the UI layer under "Final model" once. Then, enter your MQTT user name and password under "Mqtt Receiver List (Script)" in the Inspector window on the right. Feel free to change the MQTT broker address and use your own MQTT server instead of the CE MQTT server. Save your project to prepare for building the app in the next step.

3. From the top menu bar, select File and go to Build Settings. Select Android from the right platform lists and click Build and Run. Then, save the project in the desired location and make sure the Android mobile device is connected to Unity through a USB cable. Do not disconnect the mobile device until the Unity app automatically starts on the device.


### 2. The Virtual Device Android APP

The goal of the Android app developed using Unity is to allow users to check or manipulate their family boards remotely through their mobile devices. The app contains a digital twin of the physical device and a user interface panel.

The virtual device has the same functions and appearance as the physical device, and the model was imported into Unity from Fusion 360 directly. Both physical and virtual devices are connected through MQTT. However, the LCD is not responsive on the virtual device because the UI panel beside it can display more information in a clear and organized way. The virtual device is interactable since users can change their status by clicking on the windows. We kept the buttons transparent for better user experiences. 


- <img width="400" alt="image" src="https://github.com/CE-GPP/Family_Board/blob/main/Unity/workflow.png">
- AR application workflow.

- <img width="400" alt="image" src="https://github.com/CE-GPP/Family_Board/blob/main/Unity/demo.png">
- The final version of the Virtual device and the Android app.

# Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.microsoft.com.

When you submit a pull request, a CLA-bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., label, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
