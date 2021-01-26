using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Attributes

    [SerializeField] private ARShooting _ARShooting;
    private Transform parent;
    private GameObject gelVisualizer;
    private Text counter;
    private RectTransform pivotGel;
    private float gelAmount;

    #endregion

    #region Unity3D

    void Start()
    {
        parent = GameObject.FindGameObjectWithTag("Hud").transform;
        for(int i = 0; i<parent.childCount; i++)
        {
            if (parent.GetChild(i).name.StartsWith("GelVisualizer"))
            {
                gelVisualizer = parent.GetChild(i).gameObject;
            }
            else if (parent.GetChild(i).name.StartsWith("Counter"))
            {
                counter = parent.GetChild(i).GetComponentInChildren<Text>();
            }
        }
        if (gelVisualizer) pivotGel = gelVisualizer.transform.GetChild(1).GetComponent<RectTransform>();
        gelVisualizer.SetActive(true);
    }

    void Update()
    {
        if (_ARShooting)
        {
            gelAmount = _ARShooting.GetGelAmountNormalized();
            counter.text = _ARShooting.GetEliminatedVirus().ToString();
        }
        if (gelAmount < 0) return;
        if (pivotGel)
        {
            pivotGel.localScale = new Vector3(gelAmount, pivotGel.localScale.y, pivotGel.localScale.y);
        }
    }

    #endregion

    #region Public Methods

    

    #endregion
}
