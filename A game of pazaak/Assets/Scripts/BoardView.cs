using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardView : MonoBehaviour {
    public static BoardView Instance;

    public Text playerScoreText;
    public Text enemyScoreText;
    public Canvas dynCanvas;
    public Canvas mainCanvas;

    public GameObject playerTurnCard;
    public GameObject enemyTurnCard;
    public Text playerTurnCardText;
    public Text enemyTurnCardText;

    public GameObject winImage;
    public Text winText;

    public GameObject playerTurnIcon;
    public GameObject enemyTurnIcon;

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

    void Start()
    {
        dynCanvas.GetComponent<GraphicRaycaster>().enabled = false;
    }

    public void UpdateScore(int playerScore, int enemyScore)
    {
        playerScoreText.text = playerScore.ToString();
        enemyScoreText.text = enemyScore.ToString();
    }
    //Show the "turn cards" used to determine who is going first
    public void ShowTurnCards(int playerRandom, int enemyRandom)
    {
        playerTurnCardText.text = playerRandom.ToString();
        enemyTurnCardText.text = enemyRandom.ToString();
        playerTurnCard.SetActive(true);
        enemyTurnCard.SetActive(true);
    }

    public void HideTurnCards()
    {
        playerTurnCard.SetActive(false);
        enemyTurnCard.SetActive(false);
        dynCanvas.GetComponent<GraphicRaycaster>().enabled = true;
    }
    //Setting the UI elementes of the card
    //Draws the card value for the cards
    public void DrawCard(string tag,Text cardText, int value, SpecialCard.CardScoreEffect scoreEffect, Text scoreEffectText)
    {
        cardText.text = value.ToString();
        if(tag == "SpecialCard")
            DrawPlusMinus(scoreEffect, scoreEffectText);
    }
    //Draw the plus or minus sign of the special card
    public void DrawPlusMinus (SpecialCard.CardScoreEffect scoreEffect, Text scoreEffectText)
    {
        if (scoreEffect == DealerCard.CardScoreEffect.plus)
            scoreEffectText.text = "+";
        else
            scoreEffectText.text = "-";
    }

    //Draws the card effect sign for the special cards
    public void DrawSpecialCard(SpecialCard.CardSpecialEffect effect, Text effectText, Button plusMinusButton)
    {
        if (effect == SpecialCard.CardSpecialEffect.flipNumbers)
            effectText.text = "↻";
        else if (effect == SpecialCard.CardSpecialEffect.plusMinus)
        {
            effectText.text = "±";
            plusMinusButton.gameObject.SetActive(true);
        }
        else
            effectText.text = "=";

    }

    public void ShowWinScreen(string winner)
    {
        winImage.SetActive(true);
        winText.text = winner + " WINS!";
        dynCanvas.GetComponent<GraphicRaycaster>().enabled = false;
    }

    public void ShowTieScreen()
    {
        winImage.SetActive(true);
        winText.text = "TIE!";
        dynCanvas.GetComponent<GraphicRaycaster>().enabled = false;
    }
    //Enables and disables the turn icons depending on whose turn it is
    public void ShowTurnIcon()
    {
        if (DealerDeck.Instance.turn == DealerDeck.Turn.Player)
        {
            playerTurnIcon.SetActive(true);
            enemyTurnIcon.SetActive(false);
        }
        else
        {
            playerTurnIcon.SetActive(false);
            enemyTurnIcon.SetActive(true);
        }
    }

}
