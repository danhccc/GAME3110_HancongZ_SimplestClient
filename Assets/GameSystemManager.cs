using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{


    GameObject inputFieldUserName, inputFieldPassword, buttonSubmit, toggleLogin, toggleCreate;


    GameObject networkClient;

    // Start is called before the first frame update
    void Start()
    {
        // Get all the GameObject in the scene
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        // Loop through all objects
        foreach (GameObject go in allObjects)
        {
            if (go.name == "InputFieldUserName")
                inputFieldUserName = go;
            else if (go.name == "InputFieldPassword")
                inputFieldPassword = go;
            else if (go.name == "ButtonSubmit")
                buttonSubmit = go;
            else if (go.name == "ToggleLogin")
                toggleLogin = go;
            else if (go.name == "ToggleCreate")
                toggleCreate = go;
            else if (go.name == "NetworkedClient")
                networkClient = go;

        }

        buttonSubmit.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        toggleCreate.GetComponent<Toggle>().onValueChanged.AddListener(ToggleCreateValueChanged);
        toggleLogin.GetComponent<Toggle>().onValueChanged.AddListener(ToggleLoginValueChanged);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubmitButtonPressed()
    {
        string name =     inputFieldUserName.GetComponent<InputField>().text;
        string password = inputFieldPassword.GetComponent<InputField>().text;

        if (toggleLogin.GetComponent<Toggle>().isOn)
        {
            networkClient.GetComponent<NetworkedClient>().SendMessageToHost
                (ClientToServerSignifiers.Login + "," + name + "," + password);
        }
        else
        {
            networkClient.GetComponent<NetworkedClient>().SendMessageToHost
                (ClientToServerSignifiers.CreateAccount + "," + name + "," + password);
        }
    }

    public void ToggleCreateValueChanged(bool newValue)
    {
        toggleLogin.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }

    public void ToggleLoginValueChanged(bool newValue)
    {
        toggleCreate.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }


    public static class ClientToServerSignifiers
    {
        public const int Login = 1;
        public const int CreateAccount = 2;
    }

    public static class ServerToClientSignifiers
    {
        public const int LoginResponse = 1;
    }

    public static class LoginResponses
    {
        public const int Success = 1;
        public const int FailureNameInUse = 2;
        public const int FailureNameNotFound = 3;
        public const int FailureIncorrectPassword = 4;
    }
}
