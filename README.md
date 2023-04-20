# by Team 4 for CASA0021
- Leo Liu
- Sophia Chong
- Xiaoya Nie



# Family Board

is your continuous integration, delivery and learning solution for 


1. **


2. **

3. **

# Getting Started

## 1. Download The Virtual Device Android APP from Unity

1. Head over to the [Family Board Virtual Device folder](https://github.com/CE-GPP/Family_Board/tree/main/Unity/Family%20Board%20Virtual%20Device) and open the folder as a new project in Unity. Before you begin, make sure that your project is set up in Unity 2021.3.13f1. The Mobile application for Family Board only supports the Android platform.

2. In Unity, open the Prefab folder and double-click the "Final model" file to open it. In the Hierarchy window on the left, click the UI layer under "Final model" once. Then, enter your MQTT user name and password under "Mqtt Receiver List (Script)" in the Inspector window on the right. Feel free to change the MQTT broker address and use your own MQTT server instead of the CE MQTT server. Save your project to prepare for building the app in the next step.

3. From the top menu bar, select File and go to Build Settings. Select Android from the right platform lists and click Build and Run. Then, save the project in the desired location and make sure the Android mobile device is connected to Unity through a USB cable. Do not disconnect the mobile device until the Unity app automatically starts on the device.


## 2. The Virtual Device Android APP

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
