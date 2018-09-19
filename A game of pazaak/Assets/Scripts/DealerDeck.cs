using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealerDeck : MonoBehaviour {
    public static DealerDeck Instance;

    public GameObject cardpref;
    public GameObject playerBoard;
    public GameObject enemyBoard;
    private const int numberOfCards = 40;

    public enum Turn { Player, Enemy, }

    public Turn turn;
    public List<int> cardsInDeck = new List<int>(40);
    public int orderNr = 0;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Debug.Log("Multiple instances of DealerDeck");
            Destroy(this);
            return;
        }
        if(Instance == null)
            Instance = this;
    }
    void Start()
    {
        InitDeck();    
    }

    //Initialize the deck
    private void InitDeck()
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            cardsInDeck.Add((i % 10) + 1);
        }
        ShuffleDeck();
    }

    // Checks the children of the playing board and if tagged with placeholder + are free, places a card there
    //TODO : implement it wihout the use of "children"
    public void DealCard()
    {
        if (turn == Turn.Player)
        {
            foreach (Transform child in playerBoard.GetComponent<Transform>())
            {
                if (child.tag == "PlaceHolder" && !child.GetComponent<PlaceHolder>().hasCard)
                {
                    InitCard(child);
                    break;
                }
            }
        }
        else
        {
            foreach (Transform child in enemyBoard.GetComponent<Transform>())
            {
                if (child.tag == "PlaceHolder" && !child.GetComponent<PlaceHolder>().hasCard)
                {
                    InitCard(child);
                    break;
                }
            }
        }
    }
    //Instantiate a Card;
    public void InitCard(Transform child)
    {
        GameObject cardClone;
        cardClone = Instantiate(cardpref);
        orderNr++;
        cardClone.GetComponent<DealerCard>().value = cardsInDeck[orderNr];
        GameManager.Instance.UpdateScore(cardClone);
        cardClone.transform.SetParent(child.transform, false);
        cardClone.transform.position = child.transform.position;
        child.GetComponent<PlaceHolder>().SetPHCardFlag();
        //TODO : WIN CONDITION FOR FULL BOARD
    }

    public void NextTurn()
    {
        if (this.turn == Turn.Enemy)
        {
            this.turn = Turn.Player;
        }
        else
        {
            this.turn = Turn.Enemy;
        }
        Debug.Log("changed turn to " + this.turn);
        BoardView.Instance.ShowTurnIcon();
    }
    //shuffle the deck
    private void ShuffleDeck()
    {
        for (int i =0; i < cardsInDeck.Count; i++)
        {
            int temp = cardsInDeck[i];
            int randomIndex = Random.Range(i, cardsInDeck.Count);
            cardsInDeck[i] = cardsInDeck[randomIndex];
            cardsInDeck[randomIndex] = temp;
        }
    }

   
}
