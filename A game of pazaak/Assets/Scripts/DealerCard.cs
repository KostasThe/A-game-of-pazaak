using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DealerCard : MonoBehaviour{

    [HideInInspector] public int rank;
    [HideInInspector] public int value;
    public enum CardScoreEffect { plus, minus }
    public CardScoreEffect scoreEffect = CardScoreEffect.plus;
    public Text TextScore;
    public Text ScoreEffectText;

   void Start()
    {
        BoardView.Instance.DrawCard(this.tag, TextScore, value, scoreEffect, ScoreEffectText);
    }
    
}
