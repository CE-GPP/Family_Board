/*
The MIT License (MIT)

Copyright (c) 2018 Giovanni Paolo Vigano'
****
Modified by Valerio Signorelli, UCL Connected Environments 2021
Subscribed to list of topics

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
****
Modified by Valerio Signorelli, UCL Connected Environments 2021
Subscribed to list of topics

*/


using System.Collections.Generic;
using UnityEngine;
using M2MqttUnity;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt.Messages;

//the mqttObj is use to store both message and topic, so we can select the right object from the controller
public class mqttObj{
    private string m_msg;
    public string msg
    {
        get{return m_msg;}
        set{
            if (m_msg == value) return;
            m_msg = value;
        }
    }
    private string m_topic;
    public string topic
    {
        get
        {
            return m_topic;
        }
        set
        {
            if (m_topic == value) return;
            m_topic = value;
        }
    }
}


public class mqttReceiverList : M2MqttUnityClient
{
    [Header("MQTT topics")]
    [Tooltip("Set the topic to subscribe. !!!ATTENTION!!! multi-level wildcard # subscribes to all topics")]
    //public string topicSubscribe = "#"; // topic to subscribe. !!! The multi-level wildcard # is used to subscribe to all the topics. Attention i if #, subscribe to all topics. Attention if MQTT is on data plan
    public List<string> topicSubscribe = new List<string>(); //list of topics to subscribe
    
    [Header("Set the topic to publish Father Status")]


    public string topicPublishFather = ""; // topic to publish
    
    public string messagePublishFH = ""; // message to publish
    public Button changeDataBtnFH;

    public string messagePublishFW = ""; // message to publish
    public Button changeDataBtnFW;


    public string messagePublishFS = ""; // message to publish
    public Button changeDataBtnFS;


    public string messagePublishFO = ""; // message to publish
    public Button changeDataBtnFO;
    public string ElsePublishFather = ""; // topic to publish
[Header("Set the topic to publish Mother Status")]
    public string topicPublishMother = ""; // topic to publish
    public string messagePublishMH = ""; // message to publish
    public Button changeDataBtnMH;


    public string messagePublishMW = ""; // message to publish
    public Button changeDataBtnMW;


    public string messagePublishMS = ""; // message to publish
    public Button changeDataBtnMS;


    public string messagePublishMO = ""; // message to publish
    public Button changeDataBtnMO;
    public string ElsePublishMother = ""; // topic to publish
[Header("Set the topic to publish Son Status")]
    public string topicPublishSon = ""; // topic to publish
    public string messagePublishSH = ""; // message to publish
    public Button changeDataBtnSH;


    public string messagePublishSW = ""; // message to publish
    public Button changeDataBtnSW;


    public string messagePublishSS = ""; // message to publish
    public Button changeDataBtnSS;


    public string messagePublishSO = ""; // message to publish
    public Button changeDataBtnSO;
    public string ElsePublishSon = ""; // topic to publish
[Header("Set the topic to publish Daughter Status")]
    public string topicPublishDaughter = ""; // topic to publish
    public string messagePublishDH = ""; // message to publish
    public Button changeDataBtnDH;


    public string messagePublishDW = ""; // message to publish
    public Button changeDataBtnDW;


    public string messagePublishDS = ""; // message to publish
    public Button changeDataBtnDS;


    public string messagePublishDO = ""; // message to publish
    public Button changeDataBtnDO;
    public string ElsePublishDaughter = ""; // topic to publish
    [Tooltip("Set this to true to perform a testing cycle automatically on startup")]
    public bool autoTest = false;


    public InputField FatherInputField;
    public InputField MotherInputField;
    public InputField SonInputField;
    public InputField DaughterInputField;
    //using C# Property GET/SET and event listener to reduce Update overhead in the controlled objects
    //private string m_msg;
    //private string m_topic;
     mqttObj mqttObject = new mqttObj();

    public event OnMessageArrivedDelegate OnMessageArrived;
    public delegate void OnMessageArrivedDelegate(mqttObj mqttObject);

    //using C# Property GET/SET and event listener to expose the connection status
    private bool m_isConnected;


    public bool isConnected
    {
        get
        {
            return m_isConnected;
        }
        set
        {
            if (m_isConnected == value) return;
            m_isConnected = value;
            if (OnConnectionSucceeded != null)
            {
                OnConnectionSucceeded(isConnected);
            }
        }
    }
    public event OnConnectionSucceededDelegate OnConnectionSucceeded;
    public delegate void OnConnectionSucceededDelegate(bool isConnected);

    // a list to store the messages
    public List<string> eventMessages = new List<string>();
    public List<string> eventTopics = new List<string>();

      protected override void Start()
    {
        base.Start();
        Button btnFH = changeDataBtnFH.GetComponent<Button>();
        btnFH.onClick.AddListener(PublishFH);
        Button btnFW = changeDataBtnFW.GetComponent<Button>();
        btnFW.onClick.AddListener(PublishFW);
        Button btnFS = changeDataBtnFS.GetComponent<Button>();
        btnFS.onClick.AddListener(PublishFS);
        Button btnFO = changeDataBtnFO.GetComponent<Button>();
        btnFO.onClick.AddListener(PublishFO);

        Button btnMH = changeDataBtnMH.GetComponent<Button>();
        btnMH.onClick.AddListener(PublishMH);
        Button btnMW = changeDataBtnMW.GetComponent<Button>();
        btnMW.onClick.AddListener(PublishMW);
        Button btnMS = changeDataBtnMS.GetComponent<Button>();
        btnMS.onClick.AddListener(PublishMS);
        Button btnMO = changeDataBtnMO.GetComponent<Button>();
        btnMO.onClick.AddListener(PublishMO);

        Button btnSH = changeDataBtnSH.GetComponent<Button>();
        btnSH.onClick.AddListener(PublishSH);
        Button btnSW = changeDataBtnSW.GetComponent<Button>();
        btnSW.onClick.AddListener(PublishSW);
        Button btnSS = changeDataBtnSS.GetComponent<Button>();
        btnSS.onClick.AddListener(PublishSS);
        Button btnSO = changeDataBtnSO.GetComponent<Button>();
        btnSO.onClick.AddListener(PublishSO);

        Button btnDH = changeDataBtnDH.GetComponent<Button>();
        btnDH.onClick.AddListener(PublishDH);
        Button btnDW = changeDataBtnDW.GetComponent<Button>();
        btnDW.onClick.AddListener(PublishDW);
        Button btnDS = changeDataBtnDS.GetComponent<Button>();
        btnDS.onClick.AddListener(PublishDS);
        Button btnDO = changeDataBtnDO.GetComponent<Button>();
        btnDO.onClick.AddListener(PublishDO);


        //Input fields (Legacy) 
       FatherInputField.GetComponent<InputField>().onEndEdit.AddListener(FatherSubmitName);
       MotherInputField.GetComponent<InputField>().onEndEdit.AddListener(MotherSubmitName);
       SonInputField.GetComponent<InputField>().onEndEdit.AddListener(SonSubmitName);
       DaughterInputField.GetComponent<InputField>().onEndEdit.AddListener(DaughterSubmitName);
    }

     private void FatherSubmitName(string arg0)
     {
         Debug.Log(arg0);
        client.Publish(topicPublishFather, System.Text.Encoding.UTF8.GetBytes(messagePublishFO), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        client.Publish(ElsePublishFather, System.Text.Encoding.UTF8.GetBytes(arg0), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);

     }

    private void MotherSubmitName(string arg0)
     {
         Debug.Log(arg0);
        client.Publish(topicPublishMother, System.Text.Encoding.UTF8.GetBytes(messagePublishMO), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        client.Publish(ElsePublishMother, System.Text.Encoding.UTF8.GetBytes(arg0), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        
     }

    private void SonSubmitName(string arg0)
     {
         Debug.Log(arg0);
        client.Publish(topicPublishSon, System.Text.Encoding.UTF8.GetBytes(messagePublishSO), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        client.Publish(ElsePublishSon, System.Text.Encoding.UTF8.GetBytes(arg0), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        
     }

    private void DaughterSubmitName(string arg0)
     {
         Debug.Log(arg0);
        client.Publish(topicPublishDaughter, System.Text.Encoding.UTF8.GetBytes(messagePublishDO), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        client.Publish(ElsePublishDaughter, System.Text.Encoding.UTF8.GetBytes(arg0), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        
     }
    //Father
    public void PublishFH()
    {
        client.Publish(topicPublishFather, System.Text.Encoding.UTF8.GetBytes(messagePublishFH), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }
    public void PublishFW()
    {
        client.Publish(topicPublishFather, System.Text.Encoding.UTF8.GetBytes(messagePublishFW), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }
        public void PublishFS()
    {
        client.Publish(topicPublishFather, System.Text.Encoding.UTF8.GetBytes(messagePublishFS), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }
    public void PublishFO()
    {
        client.Publish(topicPublishFather, System.Text.Encoding.UTF8.GetBytes(messagePublishFO), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        client.Publish(ElsePublishFather, System.Text.Encoding.UTF8.GetBytes(FatherInputField.GetComponent<InputField>().text), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }

    //Mother
        public void PublishMH()
    {
        client.Publish(topicPublishMother, System.Text.Encoding.UTF8.GetBytes(messagePublishMH), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }
    public void PublishMW()
    {
        client.Publish(topicPublishMother, System.Text.Encoding.UTF8.GetBytes(messagePublishMW), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }
        public void PublishMS()
    {
        client.Publish(topicPublishMother, System.Text.Encoding.UTF8.GetBytes(messagePublishMS), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }
    public void PublishMO()
    {
        client.Publish(topicPublishMother, System.Text.Encoding.UTF8.GetBytes(messagePublishMO), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        client.Publish(ElsePublishMother, System.Text.Encoding.UTF8.GetBytes(MotherInputField.GetComponent<InputField>().text), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }

    //Son
        public void PublishSH()
    {
        client.Publish(topicPublishSon, System.Text.Encoding.UTF8.GetBytes(messagePublishSH), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }
    public void PublishSW()
    {
        client.Publish(topicPublishSon, System.Text.Encoding.UTF8.GetBytes(messagePublishSW), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }
        public void PublishSS()
    {
        client.Publish(topicPublishSon, System.Text.Encoding.UTF8.GetBytes(messagePublishSS), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }
    public void PublishSO()
    {
        client.Publish(topicPublishSon, System.Text.Encoding.UTF8.GetBytes(messagePublishSO), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        client.Publish(ElsePublishSon, System.Text.Encoding.UTF8.GetBytes(SonInputField.GetComponent<InputField>().text), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }

    //Daughter
        public void PublishDH()
    {
        client.Publish(topicPublishDaughter, System.Text.Encoding.UTF8.GetBytes(messagePublishDH), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }
    public void PublishDW()
    {
        client.Publish(topicPublishDaughter, System.Text.Encoding.UTF8.GetBytes(messagePublishDW), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }
        public void PublishDS()
    {
        client.Publish(topicPublishDaughter, System.Text.Encoding.UTF8.GetBytes(messagePublishDS), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }
    public void PublishDO()
    {
        client.Publish(topicPublishDaughter, System.Text.Encoding.UTF8.GetBytes(messagePublishDO), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        client.Publish(ElsePublishDaughter, System.Text.Encoding.UTF8.GetBytes(DaughterInputField.GetComponent<InputField>().text), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }

    public void SetEncrypted(bool isEncrypted)
    {
        this.isEncrypted = isEncrypted;
    }

    protected override void OnConnecting()
    {
        base.OnConnecting();
    }

    protected override void OnConnected()
    {
        base.OnConnected();
        isConnected = true;

        if (autoTest)
        {
            PublishFH();
            PublishFW();
            PublishFS();
            PublishFO();


            PublishMH();
            PublishMW();
            PublishMS();
            PublishMO();

            PublishSH();
            PublishSW();
            PublishSS();
            PublishSO();

            PublishDH();
            PublishDW();
            PublishDS();
            PublishDO();
        }
    }

    protected override void OnConnectionFailed(string errorMessage)
    {
        Debug.Log("CONNECTION FAILED! " + errorMessage);
    }

    protected override void OnDisconnected()
    {
        Debug.Log("Disconnected.");
        isConnected = false;
    }

    protected override void OnConnectionLost()
    {
        Debug.Log("CONNECTION LOST!");
    }

    protected override void SubscribeTopics()
    {
        foreach (string item in topicSubscribe) //subscribe to all the topics of the Public List topicSubscribe, not most efficient way (e.g. JSON object works better), but it might be useful in certain circumstances 
        {
         client.Subscribe(new string[] { item }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });   
        }
        
    }

    protected override void UnsubscribeTopics()
    {
        foreach (string item in topicSubscribe)
        {
            client.Unsubscribe(new string[] { item });
        }
    }

  

    protected override void DecodeMessage(string topicReceived, byte[] message)
    {
        //The message is decoded and stored into the mqttObj (defined at the lines 40-63)
        
        mqttObject.msg = System.Text.Encoding.UTF8.GetString(message);
        mqttObject.topic=topicReceived;

        // Debug.Log("Received: " + mqttObject.msg + "from topic: " + mqttObject.topic);

        StoreMessage(mqttObject.msg, mqttObject.topic);
        
        if(OnMessageArrived !=null){
        OnMessageArrived(mqttObject);
        }
        
        
    }

    private void StoreMessage(string eventMsg, string eventTopic)
    {
        // Color = System.Drawing.Color;
        // if (eventMessages.Count > 50)
        // {
        //     eventMessages.Clear();
        //     eventTopics.Clear();
        // }
        //Father Status LEDs
        Renderer rendererFH = GameObject.FindWithTag("LEDFH").GetComponent<Renderer>();
        Renderer rendererFW = GameObject.FindWithTag("LEDFW").GetComponent<Renderer>();
        Renderer rendererFS = GameObject.FindWithTag("LEDFS").GetComponent<Renderer>();
        Renderer rendererFO = GameObject.FindWithTag("LEDFO").GetComponent<Renderer>();

        //Mother Status LEDs
        Renderer rendererMH = GameObject.FindWithTag("LEDMH").GetComponent<Renderer>();
        Renderer rendererMW = GameObject.FindWithTag("LEDMW").GetComponent<Renderer>();
        Renderer rendererMS = GameObject.FindWithTag("LEDMS").GetComponent<Renderer>();
        Renderer rendererMO = GameObject.FindWithTag("LEDMO").GetComponent<Renderer>();

        //Son Status LEDs
        Renderer rendererSH = GameObject.FindWithTag("LEDSH").GetComponent<Renderer>();
        Renderer rendererSW = GameObject.FindWithTag("LEDSW").GetComponent<Renderer>();
        Renderer rendererSS = GameObject.FindWithTag("LEDSS").GetComponent<Renderer>();
        Renderer rendererSO = GameObject.FindWithTag("LEDSO").GetComponent<Renderer>();

        //Daughter Status LEDs
        Renderer rendererDH = GameObject.FindWithTag("LEDDH").GetComponent<Renderer>();
        Renderer rendererDW = GameObject.FindWithTag("LEDDW").GetComponent<Renderer>();
        Renderer rendererDS = GameObject.FindWithTag("LEDDS").GetComponent<Renderer>();
        Renderer rendererDO = GameObject.FindWithTag("LEDDO").GetComponent<Renderer>();
        eventMessages.Add(eventMsg);
        eventTopics.Add(eventTopic);

// Changing Father's LEDs based on the status data fetched from MQTT
        if(eventTopic == "student/ucfnnie/FamilyBoard/Dad/DadLocation"){
            Debug.Log ("found topic");
            if(eventMsg == "1"){
                Debug.Log ("Home");
                rendererFH.materials[0].SetColor("_Color", new Color(1f, 0.62f,0.27f));
                rendererFW.materials[0].SetColor("_Color", Color.grey);
                rendererFS.materials[0].SetColor("_Color", Color.grey);
                rendererFO.materials[0].SetColor("_Color", Color.grey);
            }else if(eventMsg == "2"){
                Debug.Log ("Work");
                rendererFH.materials[0].SetColor("_Color", Color.grey);
                rendererFW.materials[0].SetColor("_Color", new Color(1f, 0.62f,0.27f));
                rendererFS.materials[0].SetColor("_Color", Color.grey);
                rendererFO.materials[0].SetColor("_Color", Color.grey);
            }else if(eventMsg == "3"){
                Debug.Log ("Study");
                rendererFH.materials[0].SetColor("_Color", Color.grey);  
                rendererFW.materials[0].SetColor("_Color", Color.grey);
                rendererFS.materials[0].SetColor("_Color", new Color(1f, 0.62f,0.27f));
                rendererFO.materials[0].SetColor("_Color", Color.grey);
            }else {
                Debug.Log ("Other");
                rendererFH.materials[0].SetColor("_Color", Color.grey);
                rendererFW.materials[0].SetColor("_Color", Color.grey);
                rendererFS.materials[0].SetColor("_Color", Color.grey);
                rendererFO.materials[0].SetColor("_Color", new Color(1f, 0.62f,0.27f));
            }
        }

// Changing Mother's LEDs based on the status data fetched from MQTT
       else if(eventTopic == "student/ucfnnie/FamilyBoard/Mom/MomLocation"){
            Debug.Log ("found topic");
            if(eventMsg == "1"){
                Debug.Log ("Home");
                rendererMH.materials[0].SetColor("_Color", new Color(1f, 0.62f,0.27f));
                rendererMW.materials[0].SetColor("_Color", Color.grey);
                rendererMS.materials[0].SetColor("_Color", Color.grey);
                rendererMO.materials[0].SetColor("_Color", Color.grey);
            }else if(eventMsg == "2"){
                Debug.Log ("Work");
                rendererMH.materials[0].SetColor("_Color", Color.grey);
                rendererMW.materials[0].SetColor("_Color", new Color(1f, 0.62f,0.27f));
                rendererMS.materials[0].SetColor("_Color", Color.grey);
                rendererMO.materials[0].SetColor("_Color", Color.grey);
            }else if(eventMsg == "3"){
                Debug.Log ("Study");
                rendererMH.materials[0].SetColor("_Color", Color.grey);  
                rendererMW.materials[0].SetColor("_Color", Color.grey);
                rendererMS.materials[0].SetColor("_Color", new Color(1f, 0.62f,0.27f));
                rendererMO.materials[0].SetColor("_Color", Color.grey);
            }else {
                Debug.Log ("Other");
                rendererMH.materials[0].SetColor("_Color", Color.grey);
                rendererMW.materials[0].SetColor("_Color", Color.grey);
                rendererMS.materials[0].SetColor("_Color", Color.grey);
                rendererMO.materials[0].SetColor("_Color", new Color(1f, 0.62f,0.27f));
            }
        }      

// Changing Son's LEDs based on the status data fetched from MQTT
      else if(eventTopic == "student/ucfnnie/FamilyBoard/Son/SonLocation"){
            Debug.Log ("found topic");
            if(eventMsg == "1"){
                Debug.Log ("Home");
                rendererSH.materials[0].SetColor("_Color", new Color(1f, 0.62f,0.27f));
                rendererSW.materials[0].SetColor("_Color", Color.grey);
                rendererSS.materials[0].SetColor("_Color", Color.grey);
                rendererSO.materials[0].SetColor("_Color", Color.grey);
            }else if(eventMsg == "2"){
                Debug.Log ("Work");
                rendererSH.materials[0].SetColor("_Color", Color.grey);
                rendererSW.materials[0].SetColor("_Color", new Color(1f, 0.62f,0.27f));
                rendererSS.materials[0].SetColor("_Color", Color.grey);
                rendererSO.materials[0].SetColor("_Color", Color.grey);
            }else if(eventMsg == "3"){
                Debug.Log ("Study");
                rendererSH.materials[0].SetColor("_Color", Color.grey);  
                rendererSW.materials[0].SetColor("_Color", Color.grey);
                rendererSS.materials[0].SetColor("_Color", new Color(1f, 0.62f,0.27f));
                rendererSO.materials[0].SetColor("_Color", Color.grey);
            }else {
                Debug.Log ("Other");
                rendererSH.materials[0].SetColor("_Color", Color.grey);
                rendererSW.materials[0].SetColor("_Color", Color.grey);
                rendererSS.materials[0].SetColor("_Color", Color.grey);
                rendererSO.materials[0].SetColor("_Color", new Color(1f, 0.62f,0.27f));
            }
        }  

// Changing Daughter's LEDs based on the status data fetched from MQTT
       else if(eventTopic == "student/ucfnnie/FamilyBoard/Daughter/DaughterLocation"){
            Debug.Log ("found topic");
            if(eventMsg == "1"){
                Debug.Log ("Home");
                rendererDH.materials[0].SetColor("_Color", new Color(1f, 0.62f,0.27f));
                rendererDW.materials[0].SetColor("_Color", Color.grey);
                rendererDS.materials[0].SetColor("_Color", Color.grey);
                rendererDO.materials[0].SetColor("_Color", Color.grey);
            }else if(eventMsg == "2"){
                Debug.Log ("Work");
                rendererDH.materials[0].SetColor("_Color", Color.grey);
                rendererDW.materials[0].SetColor("_Color", new Color(1f, 0.62f,0.27f));
                rendererDS.materials[0].SetColor("_Color", Color.grey);
                rendererDO.materials[0].SetColor("_Color", Color.grey);
            }else if(eventMsg == "3"){
                Debug.Log ("Study");
                rendererDH.materials[0].SetColor("_Color", Color.grey);  
                rendererDW.materials[0].SetColor("_Color", Color.grey);
                rendererDS.materials[0].SetColor("_Color", new Color(1f, 0.62f,0.27f));
                rendererDO.materials[0].SetColor("_Color", Color.grey);
            }else {
                Debug.Log ("Other");
                rendererDH.materials[0].SetColor("_Color", Color.grey);
                rendererDW.materials[0].SetColor("_Color", Color.grey);
                rendererDS.materials[0].SetColor("_Color", Color.grey);
                rendererDO.materials[0].SetColor("_Color", new Color(1f, 0.62f,0.27f));
            }
        }  
        appendData appendData = GetComponent<appendData>();
        appendData.updatePanel(eventTopics, eventMessages);
    }

    protected override void Update()
    {
        base.Update(); // call ProcessMqttEvents()

    }

    private void OnDestroy()
    {
        Disconnect();
    }

    private void OnValidate()
    {
        if (autoTest)
        {
            autoConnect = true;
        }
    }


}