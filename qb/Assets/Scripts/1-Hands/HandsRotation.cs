using UnityEngine;

public class HandsRotation : MonoBehaviour
{
    #region Attributes
    private Vector3 startPoint;
    private Transform bottleHead;

    [SerializeField] [Range(0f, 20f)] public float zAxisDiagonal = 5f;
    [SerializeField] [Range(0f, 30f)] public float xAxisDiagonal = 10f;
    private int alpha = 0;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        startPoint = GameObject.FindGameObjectWithTag("Points").transform.GetChild(0).transform.position;
        bottleHead = GameObject.FindGameObjectWithTag("BottleHead").transform;
    }

    // Update is called once per frame
    void Update()
    {
        alpha += 1; //Update angle

        //Positions of elliptical orbit
        float xAxis = bottleHead.position.x + (xAxisDiagonal * Mathf.Sin(Mathf.Deg2Rad * alpha));
        float zAxis = bottleHead.position.z + (zAxisDiagonal * Mathf.Cos(Mathf.Deg2Rad * alpha));

        transform.position = new Vector3(xAxis, bottleHead.transform.parent.transform.position.y, zAxis);
        
        this.transform.LookAt(startPoint);
    }

    #region Trash
    //[SerializeField] private float speed;
    //private Vector3 difference;
    //this.transform.position = startPoint;
    //private Transform points;
    //points = GameObject.FindGameObjectWithTag("Points").transform;

    //private Vector3 endPoint;
    //endPoint = points.GetChild(1).transform.position;
    //difference = bottleHead.parent.parent.transform.position - this.transform.position;
    //transform.RotateAround(bottleHead.position, Vector3.up, speed * Time.deltaTime);
    //Vector3.MoveTowards(this.transform.position, endPoint, speed * Time.deltaTime);

    #endregion
}
