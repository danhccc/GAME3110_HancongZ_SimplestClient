using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int whoTurn;                 // 0 = X and 1 = O
    public int turnCounter;             // Counts the number of turn play

    public GameObject[] turnIndicators; // Display Whos turn it is
    public Button[] tictactoeSpaces;    // Playable spaces for our game
    public Sprite[] playerIcon;     // 0 = X icon, 1 = y icon

    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup()        // Initializing
    {
        whoTurn = 0;
        turnCounter = 0;
        turnIndicators[0].SetActive(true);
        turnIndicators[1].SetActive(false);

        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
