using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        Drag_Item card = eventData.pointerDrag.GetComponent<Drag_Item>();
        if (card != null && DealerDeck.Instance.turn == DealerDeck.Turn.Player && !GameManager.Instance.specialCardUsed)
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "PlaceHolder" && !child.GetComponent<PlaceHolder>().hasCard)
                {
                    card.parentToReturn = child.transform;
                    card.transform.position = child.transform.position;
                    child.GetComponent<PlaceHolder>().SetPHCardFlag();
                    break;
                }
            }
            
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}
