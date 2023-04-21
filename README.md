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

## 3. Physical Model and Assembly
You will need:
1. 3mm plywood
2. All the leftover plywood scraps from laser cutting
3. Access to a laser cutter
4. Access to a 3D printer
5. Access to a printer
6. Paper
7. Portraits of 4 family members
8. Transparent PLA filament
9. Wood PLA filament
10. Wood glue
11. Tape
12. Multipurpose adhesive
13. Fine grain sandpaper
14. 1 piece of black paper
15. A magnetic sheet
16. Scissors
17. Ruler
18. Cable ties

## 3.1 3D printing
Download the files in the [3D printed components folder](https://github.com/CE-GPP/Family_Board/tree/main/Physical%20Device/3D%20printed%20components) in the Physical Device folder. 
Upload both gcode files to the SD card of a 3D printer, and print the familyBoardDoor with 0.2mm wood PLA filament.
Next, print the twoFamilyBoardWindows with 0.2mm transparent PLA filament, and **repeat this 8 times** to print all 16 windows you need. 
Next, print the familyBoardLCDScreenHolder with 0.2mm PLA filament of your choice.

## 3.2 Laser cutting
Download both files in the [Laser cut template folder](https://github.com/CE-GPP/Family_Board/tree/main/Physical%20Device/Laser%20cut%20template) in the Physical Device folder.

### 3.2.1 Laser cutting the family board face
After opening the familyBoardFace file and exporting it to the laser cutter, set the **location labels** and **spaces for the magnetic family portraits**, which are the **first columnn of rectangles from the left** to **engrave** instead of **cut**. All other lines can be set to **cut**. 
After making this change, it can be laser cut onto 3mm plywood.

### 3.2.2 Laser cutting the family board roof
After opening the familyBoardRoof file and exporting it to the laser cutter, ensure that all its lines are set to **cut**. It can then be laser cut onto 3mm plywood.

## 3.3 Assembling the family board
After you have laser cut the family board face, **keep the leftover plywood scraps** as you need them for later.

### 3.3.1 Assembling the family board face
Assemble the exterior faces of the board with wood glue one by one, and tape each of glued edges together for a secure finish. In the process, ensure that all the components are orientated correctly, and check that the slots at the side of the board are close to the board's front instead of the board's back. 

Next, glue the two walls without finger joints to the top and bottom of the board's interior. Ensure that it is glued as tightly as possible, as this is going to form the rail for the sliding face.

Glue the top of the rails to the back of the top and bottom faces of the board, and glue the grip rail to the sliding face.

Very carefully slot the sliding face of the board into the rail created for it, with the raised edge of the grip rail facing outwards, and check whether it is able to slide in smoothly or not.If not, then remove the debris inside the rail or sand down the wood edges for smoother motion.
![IMG_9236](https://user-images.githubusercontent.com/114293506/233712919-f4899dff-0ecb-4158-a5e2-1cfdece6bda9.jpg)

Tape or glue the piece of black paper to the back of sliding face, in the area that will be behind the windows when the board is closed. This will prevent excessive light reflection between each window from the LED strip.
![IMG_0312](https://user-images.githubusercontent.com/114293506/233712529-02c8c3db-1583-4b9a-9b13-c869629c866e.jpg)

Glue the support beams into the sides of the board, with the side with the hole closer to the end of the beam on the left side (the side of the LCD screen) of the board.

Glue the stair pieces to each other, then glue it underneath the hole for the door in the front of the board, so that the hole for the door is positioned above the middle of the stairs, about 10mm away from each end.

After following these steps, you should have a completely assembled the laser cut components of your family board face. Leave the board to dry for at least 2 hours.
 
### 3.3.2 Assembling the family board roof
Assemble each face of the roof with wood glue one by one, and tape each of the glued edges together for a secure finish. It should look like this:
![IMG_9081](https://user-images.githubusercontent.com/114293506/233711041-24fe282c-a922-4abd-a01e-78f014f4a064.jpg)

Leave the roof to dry for at least 2 hours.

Then you can glue the longer base of the roof to the top of the family board, leaving about 35mm of overhang on each side.
![IMG_9240](https://user-images.githubusercontent.com/114293506/233711367-732b795f-a1bd-4eec-b6f9-e4bddf709757.jpg)

## 3.4 Adding 3D printed and electronic components

### 3.4.1 Adding the 3D printed components
Slot each of the 3D printed windows into the 4 rows of rectangular holes on the board. In this process, you will have to slightly sand down the edges of each window and the holes in the board to make them fit snugly without adhesive. 

Next, glue the 3D printed door to the front of the board on top of the stairs with multipurpose adhesive, covering the hole.

The board should now look like this:
![IMG_9250](https://user-images.githubusercontent.com/114293506/233713613-98227790-1969-4ce1-835d-060476fb2140.jpg)

Slot the LCD screen into the 3D printed LCD screen holder, and position it on top of the top inner support beam so the LCD screen is aligned with and visible from the hole cut for it in the front of the board. To do so, you may need to place a scrap piece of plywood underneath the LCD screen holder to prop it up so the screen can be fully visible through the hole. 
![IMG_0195](https://user-images.githubusercontent.com/114293506/233714038-39348428-86ab-4202-9a81-a45c04b6c713.jpg)

Then take out the LCD screen to wire it, and glue the LCD screen holder into the suitable position with multipurpose adhesive.

### 3.4.2 Adding electronic components
Wire the electronic components of the device. 

Thread the LED strip through each of the support beams and position them on underside of each one. To ensure the correct positioning of each LED pixel in accordance with our code, each of the 4 LED strips should always be placed from **right(side close to the family portraits magnets)** to **left (side close to the LCD screen)**. The spacing should be accurate if you position **the first 4 LED pixels** on each separate LED strip **behind the window the far right (side close to the family portait magnets)** in each row. Afterwards, peel the sticker off the LED strip and stick them in the correct position, and further secure it with cable ties.
![IMG_0242](https://user-images.githubusercontent.com/114293506/233714179-599b7ce3-8706-4113-867c-6f73f73ef3ec.jpg)


Slot the wired LCD screen into the now-secured LCD screen holder. 
![IMG_0194](https://user-images.githubusercontent.com/114293506/233714485-01e1139b-0328-4493-9324-76821798365c.jpg)

Position the RFID reader horizontally in the space behind the 3D printed door. 
Place 2 leftover plywood scraps perpendicular to each other and glue them to the bottom of the board near the corner of the RFID reader to secure it in place behind the door.  
![Picture1](https://user-images.githubusercontent.com/114293506/233715045-75a9f6e7-92da-4a02-9a26-ec0adcfcddc2.jpg)

### 3.4.3 Adding the family portrait magnets
To create the family portrait magnets, print the portraits of four of your family members on paper around 36mm tall and 26mm wide and cut them out.
Cut 8 rectangles of that same length and width (36mm x 26mm) out of your magnetic sheet. Glue 4 of them in the engraved rectangles on the face of the family board with multipurpose adhesive to make the magnetic surface to attach the family portrait magnets to. 
Glue your printed family portraits to the remaining 4 magnetic sheets, and stick them onto the 4 magnetic surfaces of the board.

After following these steps, the physical family board should be ready for use!
![IMG_0222](https://user-images.githubusercontent.com/114293506/233715134-3a3fc21c-d638-4e4c-a005-d49dd1e8126f.jpg)
![IMG_0245](https://user-images.githubusercontent.com/114293506/233715334-bee3d7e8-a981-435f-820b-b71191e7b1d6.jpg)

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
