using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowScript : MonoBehaviour {
    private Color originalColor;
    private Color originalColorChild;
    private Transform childToGlow;
    IEnumerator co;

    [HideInInspector] public bool toGlow = false;

	// Use this for initialization
	void Start () {
        co = Glow(1.0f, 0.5f);
        originalColor = this.GetComponent<Image>().color;
        foreach (Transform child in transform)
        {
            originalColorChild = child.GetComponentInChildren<Image>().color;
            childToGlow = child;
        }		
	}
	
	// Update is called once per frame
	void Update () {
        if (toGlow)
            StartCoroutine(co);
        else
        {
            StopCoroutine(co);
            co = Glow(1.0f, 0.5f);
            this.GetComponent<Image>().color = originalColor;
            childToGlow.GetComponent<Image>().color = originalColorChild;
        }
	}

    IEnumerator Glow(float alphaValue, float alphaTime)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / alphaTime)
        {
            var tempColor = this.GetComponent<Image>().color;
            var tempColorChild = childToGlow.GetComponent<Image>().color;
            tempColor.a = Mathf.Lerp(originalColor.a, alphaValue, t);
            tempColorChild.a = Mathf.Lerp(originalColorChild.a, alphaValue - 0.5f, t);
            this.transform.GetComponent<Image>().color = tempColor;
            childToGlow.GetComponent<Image>().color = tempColorChild;
            yield return null;
        }
    }
}
