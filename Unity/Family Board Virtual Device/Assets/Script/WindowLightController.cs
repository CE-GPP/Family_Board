//Created For testing purposes, not being included in the final version of the app.
using UnityEngine;
using UnityEngine.UI;


public class WindowLightController : MonoBehaviour
{   
    public Material[] WindowLightsOn;
    public Material[] WindowLightsOff;
    public Color colorOn = new Color();
    public Color colorOff = new Color();

    public Button changeDataBtn;
     // Start is called before the first frame update
    void Start()
    {Button btn = changeDataBtn.GetComponent<Button>();
        btn.onClick.AddListener(setLED);
    }

 public void setLED(){
        Debug.Log ("You have clicked the button!");
        for (int i = 0; i<  WindowLightsOn.Length; i++){
                WindowLightsOn[i].color = colorOn;
            }

        for (int i = 0; i< WindowLightsOff.Length; i++){
                WindowLightsOff[i].color = colorOff;
            }
        }   
}
