using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialCard : DealerCard {
    public enum CardSpecialEffect { plusMinus, winEqual, flipNumbers}
    public CardSpecialEffect specialEffect;
    public Text valueText;
    public Text effectText;
    public Text scoreEffectText;
    public Button plusMinusButton;
   
	// Use this for initialization
	void Start ()
    {
        specialEffect = (CardSpecialEffect)Random.Range(0, System.Enum.GetValues(typeof(CardSpecialEffect)).Length);
        scoreEffect = (CardScoreEffect)Random.Range(0, System.Enum.GetValues(typeof(CardScoreEffect)).Length);
        value = Random.Range(1, 5);
        BoardView.Instance.DrawCard(this.tag, valueText, value, scoreEffect, scoreEffectText);
        BoardView.Instance.DrawSpecialCard(specialEffect, effectText, plusMinusButton);
	}

    public void flipScoreEffect()
    {
        if (this.scoreEffect == CardScoreEffect.minus)
            this.scoreEffect = CardScoreEffect.plus;
        else
            this.scoreEffect = CardScoreEffect.minus;
        BoardView.Instance.DrawPlusMinus(scoreEffect, scoreEffectText);
    }
	
}
