using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag_Item : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public Transform parentToReturn = null;
    private GameObject toGlowObj;
    private Vector3 originalScale;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentToReturn = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var screenPoint =Input.mousePosition;
        screenPoint.z = 10.0f;
        this.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
        //Determine which item begins glowing
        if (this.tag == "SpecialCard" && DealerDeck.Instance.turn == DealerDeck.Turn.Player && !GameManager.Instance.specialCardUsed)
        {
            toGlowObj = GameObject.Find("PlayerBorderGlow");
            toGlowObj.GetComponent<GlowScript>().toGlow = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(parentToReturn);
        if (parentToReturn.name == "PlayerHandPanel")                //if the card has be dragged to an invalid space and parent is gonna be Player's hand panel enable raycasts)
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        else                                                       // else if card has been dragged to valid space and it's the players turn --> end the turn.
        {
            if (DealerDeck.Instance.turn == DealerDeck.Turn.Player && !GameManager.Instance.specialCardUsed)
                GameManager.Instance.UsedSpecialCard(this.gameObject);
        }
        if(toGlowObj!=null)
            toGlowObj.GetComponent<GlowScript>().toGlow = false;        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        originalScale = this.transform.localScale;
        this.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = originalScale;
    }
}


