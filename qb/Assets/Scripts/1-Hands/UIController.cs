using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Attributes

    private ARShooting _ARShooting;
    private RectTransform pivotGel;
    private float gelAmount;

    #endregion

    #region Unity3D

    // Start is called before the first frame update
    void Start()
    {
        _ARShooting = GetComponentInParent<ARShooting>();
        pivotGel = GameObject.FindGameObjectWithTag("Hud").transform.Find("GelVisualizer").GetComponent<RectTransform>();
    }

    // Update is called once per frame
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
}
