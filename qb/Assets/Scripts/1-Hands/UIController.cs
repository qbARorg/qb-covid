using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private HandsSceneManager sceneManager;
    private RectTransform pivotGel;
    private float gelAmount;

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GetComponentInParent<HandsSceneManager>();
        pivotGel = GameObject.FindGameObjectWithTag("Hud").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sceneManager) gelAmount = sceneManager.GetGelAmountNormalized();
        if (gelAmount < 0) return;
        if (pivotGel)
        {
            pivotGel.localScale = new Vector3(gelAmount, pivotGel.localScale.y, pivotGel.localScale.y);
        }
    }
}
