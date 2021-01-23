using UnityEngine;

public class ARShooting : MonoBehaviour
{
    #region Attributes

    private GameObject gelBottleHead;   //For animating
    private GameObject gelBottle;       
    private Transform shootPosition;

    [Header("Bottle head animation")]
    private Vector3 offsetBottlePos;
    private enum Animate { up = 1, down = 0 };
    private bool finishDownAnimation = false;

    [Header("Bottle movement")]
    private Vector3 localBottlePos;

    [Header("Gel")]
    public Animator emptyAnimation;
    public float maxAmountGel = 100f;
    private float gelAmount;
    private bool doPuff = false;
    public ParticleSystem mainParticleSyst;
    public ParticleSystem splatterParticleSyst;
    private ParticleSystem.ForceOverLifetimeModule gelForceModule;

    private bool isDragging;

    #endregion

    #region Unity3D

    private void Awake()
    {
        gelAmount = maxAmountGel;
        gelForceModule = mainParticleSyst.forceOverLifetime;
        offsetBottlePos = new Vector3(0f, 0.1f, 0f);
        //SetVariables();
    }

    void Update()
    {
        //Press
        if (Input.GetMouseButton(0) || Input.touchCount == 1)
        {
            isDragging = true;
            if (!finishDownAnimation) AnimateBottle(Animate.down);  //Animate bottle
        }
        //Release
        if (Input.GetMouseButtonUp(0)) /*if (Input.touchCount <= 0)*/
        {
            isDragging = false;
            if (finishDownAnimation) AnimateBottle(Animate.up);     //Animate bottle
        }
        if (isDragging) OnDrag();
    }

    #endregion

    #region Private Methods
    
    private void SetVariables()
    {
        gelBottleHead = GameObject.FindGameObjectWithTag("BottleHead");
        gelBottle = gelBottleHead.transform.parent.gameObject;
        shootPosition = gelBottleHead.transform.GetChild(0).transform;
        localBottlePos = gelBottle.transform.localPosition;
    }

    private void OnDrag()
    {
        //Move bottle
        Vector3 touchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f);
        touchPos = Camera.main.ScreenToWorldPoint(touchPos);
        gelBottle.transform.position = new Vector3(touchPos.x, gelBottle.transform.position.y, gelBottle.transform.position.z);
        gelBottle.transform.localPosition = new Vector3(gelBottle.transform.localPosition.x, localBottlePos.y, localBottlePos.z);

        //Emit one particle at a time
        gelAmount -= 0.1f;
        if (gelAmount > 0)
        {
            mainParticleSyst.Emit(1);

            //Set height of mainParticleSyst
            float height = Mathf.Clamp(touchPos.y - shootPosition.position.y, 0f, 1f);
            gelForceModule.yMultiplier = Mathf.Lerp(0.001f, 15f, height);
            //Debug.Log($"multiplier: {gelForceModule.yMultiplier}");
        }
        else
        {
            if (!doPuff)
            {
                emptyAnimation.gameObject.GetComponent<Transform>().position = shootPosition.position;
                doPuff = true;
            }
        }
    }

    private void AnimateBottle(Animate direction)
    {
        if (direction == Animate.up)
        {
            gelBottleHead.transform.position += offsetBottlePos;
            finishDownAnimation = false;
            emptyAnimation.gameObject.SetActive(false);
        }
        else if (direction == Animate.down)
        {
            gelBottleHead.transform.position -= offsetBottlePos;
            finishDownAnimation = true;
            if (doPuff)
            {
                emptyAnimation.gameObject.SetActive(true);
                doPuff = false;
            }
        }
    }

    #endregion

    #region Public Methods

    public float GetGelAmount()
    {
        return gelAmount;
    }

    public float GetGelAmountNormalized()
    {
        return gelAmount / maxAmountGel;
    }

    #endregion
}
