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


        VisualElement updatingVaildation = GetComponent<UIDocument>().rootVisualElement.Q(topic);
        // Keeping refreshing the UI board with new MQTT data
        if (updatingVaildation == null){
            VisualElement data = new VisualElement();
            data.name = topic;
            data.AddToClassList("text-content");

            Label title = new Label(topic);
            title.AddToClassList("title");
            title.name = "Title";
            data.Add(title);

            Label value = new Label(messageStorage[lastIndex].ToString());

            value.name = "Value";
            value.AddToClassList("value");
            data.Add(value);

            baseTemplate.Add(data);
        }else{
                Label value = updatingVaildation.Query<Label>("Value");
                value.text = (messageStorage[lastIndex]).ToString();
            }
    }
}    

    
