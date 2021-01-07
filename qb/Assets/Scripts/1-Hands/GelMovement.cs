using UnityEngine;

public class GelMovement : MonoBehaviour
{
    #region Attributes
    private ARShooting _ARShooting;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _ARShooting = GameObject.FindGameObjectWithTag("GameController").GetComponent<ARShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
