using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class appendData : MonoBehaviour{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void updatePanel(List<string> topicStorage, List<string> messageStorage){
        VisualElement baseTemplate = GetComponent<UIDocument>().rootVisualElement.Q("Content");


        int lastIndex = messageStorage.Count - 1;
        string[] fullTopicAddress = topicStorage[lastIndex].Split("/");
        string topic = fullTopicAddress[fullTopicAddress.Length - 1];
        if (topic =="DadElse"){
            topic = "DadLocation";
        }
        else if (topic =="MomElse"){
            topic = "MomLocation";
        }
        else if (topic =="SonElse"){
            topic = "SonLocation";
        }
        else if (topic =="DaughterElse"){
            topic = "DaughterLocation";
        }


        VisualElement updatingVaildation = GetComponent<UIDocument>().rootVisualElement.Q(topic);


        // Keeping refreshing the UI board with new MQTT data
        if (updatingVaildation == null){
            VisualElement data = new VisualElement();

            data.name = topic;
            

            Debug.Log (data.name);
            data.AddToClassList("text-content");

            Label title = new Label(topic);
            title.AddToClassList("title");
            title.name = "Title";
            data.Add(title);


            if (messageStorage[lastIndex].ToString() == "1"){
                Label value = new Label("Home");
                value.name = "Value";
                value.AddToClassList("value");
                data.Add(value);

                baseTemplate.Add(data);
                 Debug.Log (value);
            }
            else if (messageStorage[lastIndex].ToString() == "2"){
                Label value = new Label("Work");
                value.name = "Value";
                value.AddToClassList("value");
                data.Add(value);

                baseTemplate.Add(data);
                 Debug.Log (value);
            }
            else if (messageStorage[lastIndex].ToString() == "3"){
                Label value = new Label("Study");
                value.name = "Value";
                value.AddToClassList("value");
                data.Add(value);

                baseTemplate.Add(data);
                 Debug.Log (value);
            }
            
            else{
                Label value = new Label(messageStorage[lastIndex].ToString());
                value.name = "Value";
                value.AddToClassList("value");
                data.Add(value);

                baseTemplate.Add(data);
                 Debug.Log (value);
            }

        }else{
                Label value = updatingVaildation.Query<Label>("Value");
                if (messageStorage[lastIndex].ToString() == "1"){
                value.text = "Home";
                }
                else if (messageStorage[lastIndex].ToString() == "2"){
                value.text = "Work";
                }
                else if (messageStorage[lastIndex].ToString() == "3"){
                value.text = "Study";
                }
                else{
                value.text = messageStorage[lastIndex].ToString();
                }
            }
    }
}    

    
