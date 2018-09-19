using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    DealerDeck deck;
    private bool playerStand = false;
    private bool enemyStand = false;

    public bool specialCardUsed = false;
    public int playerScore;
    public int enemyScore;
    public bool winFlag = false;

    // Use this for initialization
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Multiple instances of DealerDeck");
            Destroy(this);
            return;
        }
        if (Instance == null)
            Instance = this;
    }
    void Start () {
        deck = DealerDeck.Instance;
        StartCoroutine(DetermineFirstPhase());
	}

    // Coroutine that draws two random cards and determines who plays first. 
    IEnumerator DetermineFirstPhase()
    { 
        yield return new WaitForSeconds(1);
        int playerRandom = Random.Range(1, 11);
        int enemyRandom = Random.Range(1, 11);
        BoardView.Instance.ShowTurnCards(playerRandom, enemyRandom); 
        yield return new WaitForSeconds(2);
        BoardView.Instance.HideTurnCards();
        if (playerRandom >= enemyRandom)
            deck.turn = DealerDeck.Turn.Player;
        else
            deck.turn = DealerDeck.Turn.Enemy;
        deck.DealCard();
        BoardView.Instance.ShowTurnIcon();
    }
   
    public void OnEndTurnClicked()
    {
        EndTurn();
    }
    
    public void OnStandClicked()
    {
        if (deck.turn == DealerDeck.Turn.Player)
            playerStand = true;
        else
            enemyStand = true;
        CheckStandFlags();
        if (!winFlag)
        {
            deck.NextTurn();
            EndTurn();
        }
    }
    //TODO IMPLEMENT SPECIAL CARDS
    public void UsedSpecialCard(GameObject specialCard)
    {
        specialCardUsed = true;
        UpdateScore(specialCard);
    }

    public void EndTurn()
    {
        Debug.Log("Turn is" + deck.turn + "player stand" + playerStand + enemyStand);
        if (!playerStand && !enemyStand)
        {
            Debug.Log("entered going to next turn");
            deck.NextTurn();
        }
        deck.DealCard();
        specialCardUsed = false;

    }
    //WIN CONDITION 1: updates the scores and shows the win screen if someone gets eliminated by surpassing 20
    public void UpdateScore(GameObject card)
    {
        if (deck.turn == DealerDeck.Turn.Player)
        {
            if(card.GetComponent<DealerCard>().scoreEffect == DealerCard.CardScoreEffect.plus)
                playerScore += card.GetComponent<DealerCard>().value;
            else
                playerScore -= card.GetComponent<DealerCard>().value;
            if (playerScore > 20)
            {
                BoardView.Instance.ShowWinScreen("ENEMY");
                winFlag = true;
            }
        }
        else
        {
            if (card.GetComponent<DealerCard>().scoreEffect == DealerCard.CardScoreEffect.plus)
                enemyScore += card.GetComponent<DealerCard>().value;
            else
                enemyScore += card.GetComponent<DealerCard>().value;
            if (enemyScore > 20)
            {
                BoardView.Instance.ShowWinScreen("PLAYER");
                winFlag = true;
            }
            
        }
        BoardView.Instance.UpdateScore(playerScore, enemyScore);
    }
    //WIN CONDITION 2: if both players have used the stand option checks for the winner (one with higher score)
    public void CheckStandFlags()
    {
        if(playerStand && enemyStand)
        {
            if (playerScore > enemyScore)
                BoardView.Instance.ShowWinScreen("PLAYER");
            else if (enemyScore > playerScore)
                BoardView.Instance.ShowWinScreen("ENEMY");
            else
                BoardView.Instance.ShowTieScreen();
            winFlag = true;
        }
    }
}

