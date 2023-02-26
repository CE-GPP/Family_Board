
using UnityEngine;
using Unity​Engine.UIElements;
using System.Collections.Generic;
public class UpdateElse : MonoBehaviour
{
    // provide reference in editor inspector (from step 2)
    public UIDocument UIDocument;
    private Button uiButton;

    private TextField FatherTextField;
    private TextField MotherTextField;
    private TextField SonTextField;
    private TextField DaughterTextField;
 private void Awake()
    {
        var root = UIDocument.rootVisualElement;
        // get ui elements by name
        FatherTextField = root.Query<TextField>("FatherTextField");
        MotherTextField = root.Query<TextField>("MotherTextField");
        SonTextField = root.Query<TextField>("SonTextField");
        DaughterTextField = root.Query<TextField>("DaughterTextField");
        uiButton = root.Query<Button>("SubmitButton");

 
        // add event handler
        uiButton.clicked += Button_clicked;//subscribe to the function
    }
 
    private void Button_clicked()
    {
        // label.text = textField.text;
        Debug.Log("submited");
    }
}