using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{


    GameObject inputFieldUserName, inputFieldPassword, buttonSubmit, toggleLogin, toggleCreate;

    GameObject infoStuff1, infoStuff2;

    GameObject networkClient;

    GameObject findGameSessionButton, placeHolderGameButton;

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
            else if (go.name == "FindGameSessionButton")
                findGameSessionButton = go;
            else if (go.name == "PlaceHolderGameButton")
                placeHolderGameButton = go;
            else if (go.name == "InfoText1")
                infoStuff1 = go;
            else if (go.name == "InfoText2")
                infoStuff2 = go;

        }

        buttonSubmit.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        toggleCreate.GetComponent<Toggle>().onValueChanged.AddListener(ToggleCreateValueChanged);
        toggleLogin.GetComponent<Toggle>().onValueChanged.AddListener(ToggleLoginValueChanged);

        findGameSessionButton.GetComponent<Button>().onClick.AddListener(FindGameSessionButtonPressed);
        placeHolderGameButton.GetComponent<Button>().onClick.AddListener(PlaceHolderGameButtonPressed);

        ChangeGameState(GameStates.Login);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //    ChangeGameState(GameStates.Login);
        //if (Input.GetKeyDown(KeyCode.S))
        //    ChangeGameState(GameStates.MainMenu);
        //if (Input.GetKeyDown(KeyCode.D))
        //    ChangeGameState(GameStates.WaitingForMatch);
        //if (Input.GetKeyDown(KeyCode.F))
        //    ChangeGameState(GameStates.PlayerTicTacToe);
    }

    private void SubmitButtonPressed()
    {
        string n =     inputFieldUserName.GetComponent<InputField>().text;
        string p = inputFieldPassword.GetComponent<InputField>().text;

        if (toggleLogin.GetComponent<Toggle>().isOn)
        {
            networkClient.GetComponent<NetworkedClient>().SendMessageToHost
                (ClientToServerSignifiers.Login + "," + n + "," + p);
        }
        else
        {
            networkClient.GetComponent<NetworkedClient>().SendMessageToHost
                (ClientToServerSignifiers.CreateAccount + "," + n + "," + p);
        }
    }

    private void ToggleCreateValueChanged(bool newValue)
    {
        toggleLogin.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }

    private void ToggleLoginValueChanged(bool newValue)
    {
        toggleCreate.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }


    private void FindGameSessionButtonPressed()
    {
        networkClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.AddToGameSessioQueue + "");
        ChangeGameState(GameStates.WaitingForMatch);
    }
    private void PlaceHolderGameButtonPressed()
    {
        networkClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.TicTacToePlay + "");

    }


    public void ChangeGameState(int newState)
    {
        // Disable all first
        inputFieldUserName.SetActive(false);
        inputFieldPassword.SetActive(false);
        buttonSubmit.SetActive(false);
        toggleLogin.SetActive(false);
        toggleCreate.SetActive(false);
        findGameSessionButton.SetActive(false);
        placeHolderGameButton.SetActive(false);
        infoStuff1.SetActive(false);
        infoStuff2.SetActive(false);

        // Then load select state
        if (newState == GameStates.Login)
        {
            inputFieldUserName.SetActive(true);
            inputFieldPassword.SetActive(true);
            buttonSubmit.SetActive(true);
            toggleLogin.SetActive(true);
            toggleCreate.SetActive(true);
            infoStuff1.SetActive(true);
            infoStuff2.SetActive(true);
        }
        else if (newState == GameStates.MainMenu)
        {
            findGameSessionButton.SetActive(true);
        }
        else if (newState == GameStates.WaitingForMatch)
        {

        }
        else if (newState == GameStates.PlayerTicTacToe)
        {
            placeHolderGameButton.SetActive(true);
        }



    }




}
    static public class GameStates
    {
        public const int Login = 1;
        public const int MainMenu = 2;
        public const int WaitingForMatch = 3;
        public const int PlayerTicTacToe = 4;
    }
