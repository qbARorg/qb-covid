using UnityEngine;

public class ARShooting : MonoBehaviour
{
    #region Attributes

    [Header("Prefabs")]
    [SerializeField] private Animator emptyAnimator;

    private GameObject gelBottleHead;       //Animating head
    private GameObject gelBottle;           //Moving in X axis 
    private Transform shootPosition;        //Shoot particle syst
    private Vector3 localBottlePos;

    [Header("Head animation")]
    private Vector3 offsetBottlePos;
    private enum Animate { up = 1, down = 0 };
    private bool finishDownAnimation = false;

    [Header("Gel")]
    public float maxAmountGel = 100f;
    private float gelAmount;
    private bool doPuff = false;
    private ParticleSystem.ForceOverLifetimeModule gelForceModule;
    private ParticleSystem mainParticleSyst;
    private ParticleSystem splatterParticleSyst;
    private int eliminatedVirus;

    private bool isDragging;

    #endregion

    #region Unity3D

    private void Awake()
    {
        SetVariables();
    }

    public void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //Press
            if (Input.touchCount == 1)
            {
                isDragging = true;
                if (!finishDownAnimation) AnimateBottle(Animate.down);  //Animate bottle
            }
            //Release
            if (Input.touchCount <= 0)
            {
                isDragging = false;
                if (finishDownAnimation) AnimateBottle(Animate.up);     //Animate bottle
            }
            if (isDragging) OnDrag();
        }
        
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor ||
            Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            //Press
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                if (!finishDownAnimation) AnimateBottle(Animate.down);  //Animate bottle
            }
            //Release
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                if (finishDownAnimation) AnimateBottle(Animate.up);     //Animate bottle
            }
            if (isDragging) OnDrag();
        }
    }

    #endregion

    #region Private Methods

    private void SetVariables()
    {
        eliminatedVirus = 0;
        gelBottleHead = GameObject.FindGameObjectWithTag("BottleHead");
        gelBottle = gelBottleHead.transform.parent.gameObject;
        shootPosition = gelBottleHead.transform.GetChild(0).transform;
        localBottlePos = gelBottle.transform.localPosition;

        GameObject[] particleSystems = GameObject.FindGameObjectsWithTag("ParticleSyst");
        mainParticleSyst = particleSystems[0].GetComponent<ParticleSystem>();
        splatterParticleSyst = particleSystems[1].GetComponent<ParticleSystem>();
        
        gelAmount = maxAmountGel;
        gelForceModule = mainParticleSyst.forceOverLifetime;
        offsetBottlePos = new Vector3(0f, 0.02f, 0f);
    }

    private void OnDrag()
    {
        Vector3 touchPos = Vector3.zero;
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount != 1) return;
            //Move bottle
            touchPos = Input.touches[0].position;
            touchPos.z = 0.2f;
        }
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor ||
            Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            //Move bottle
            touchPos = Input.mousePosition;
            touchPos.z = 0.2f;
        }

        touchPos = Camera.main.ScreenToWorldPoint(touchPos);
        gelBottle.transform.position = new Vector3(touchPos.x, gelBottle.transform.position.y, gelBottle.transform.position.z);
        gelBottle.transform.localPosition = new Vector3(gelBottle.transform.localPosition.x, localBottlePos.y, localBottlePos.z);

        //Emit one particle at a time
        gelAmount -= 0.1f;
        if (gelAmount > 0 || true)
        {
            mainParticleSyst.Emit(1);

            //Set height of mainParticleSyst
            float height = Mathf.Clamp(touchPos.y * 3 - shootPosition.position.y, 0f, 1f);
            gelForceModule.yMultiplier = Mathf.Lerp(0.001f, 15f, height);
        }
        else if (!doPuff)
        {
            emptyAnimator.gameObject.GetComponent<Transform>().position = shootPosition.position;
            doPuff = true;
        }
    }

    private void AnimateBottle(Animate direction)
    {
        if (direction == Animate.up)
        {
            gelBottleHead.transform.position += offsetBottlePos;
            finishDownAnimation = false;
            emptyAnimator.gameObject.SetActive(false);
        }
        else if (direction == Animate.down)
        {
            gelBottleHead.transform.position -= offsetBottlePos;
            finishDownAnimation = true;
            if (doPuff)
            {
                emptyAnimator.gameObject.SetActive(true);
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

    public void IncrementEliminatedVirus()
    {
        eliminatedVirus++;
    }

    public int GetEliminatedVirus()
    {
        return eliminatedVirus;
    }

    #endregion
}
