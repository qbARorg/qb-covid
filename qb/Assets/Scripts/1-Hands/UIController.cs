using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Attributes

    private ARShooting _ARShooting;
    private GameObject gelVisualizer;
    private RectTransform pivotGel;
    private float gelAmount;

    #endregion

    #region Unity3D

    void Start()
    {
        _ARShooting = GetComponentInParent<ARShooting>();
        gelVisualizer = GameObject.FindGameObjectWithTag("Hud").transform.GetChild(0).gameObject;
        Debug.Log($"gel visualizer: {gelVisualizer.name}");
        if (gelVisualizer) pivotGel = gelVisualizer.GetComponent<RectTransform>();
    }

    void Update()
    {
        if(_ARShooting) gelAmount = _ARShooting.GetGelAmountNormalized();
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
