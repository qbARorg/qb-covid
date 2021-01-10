using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public ARShooting _ARShooting;
    public RectTransform pivotGel;
    public Text handsText;

    private float gelAmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gelAmount = _ARShooting.GetGelAmountNormalized();
        if (gelAmount < 0) return;
        pivotGel.localScale = new Vector3(gelAmount, pivotGel.localScale.y, pivotGel.localScale.y);
    }
}
