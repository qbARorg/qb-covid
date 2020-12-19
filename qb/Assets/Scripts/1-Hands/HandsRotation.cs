using UnityEngine;

public class HandsRotation : MonoBehaviour
{
    private Transform points;
    private Vector3 startPoint;
    private Vector3 endPoint;
    [SerializeField] private float speed;
    private Transform bottleHead;
    private Vector3 difference;

    [SerializeField] [Range(0f, 20f)] public float zAxisDiagonal = 5f;
    [SerializeField] [Range(0f, 30f)] public float xAxisDiagonal = 10f;
    public int alpha = 0;
 

    // Start is called before the first frame update
    void Start()
    {
        points = GameObject.FindGameObjectWithTag("Points").transform;
        startPoint = points.GetChild(0).transform.position;
        endPoint = points.GetChild(1).transform.position;
        //this.transform.position = startPoint;
        bottleHead = GameObject.FindGameObjectWithTag("BottleHead").transform;
    }

    // Update is called once per frame
    void Update()
    {
        difference = bottleHead.parent.parent.transform.position - this.transform.position;
        //transform.RotateAround(bottleHead.position, Vector3.up, speed * Time.deltaTime);
        //Vector3.MoveTowards(this.transform.position, endPoint, speed * Time.deltaTime);
        alpha += 1;

        //this.transform.position = bottleHead.position + new Vector3(xAxisDiagonal * Mathf.Cos(alpha * .005f), 0, zAxisDiagonal * Mathf.Sin(alpha * .005f)) * Time.deltaTime * speed;
        transform.position = new Vector3(bottleHead.position.x + (xAxisDiagonal * Mathf.Sin(Mathf.Deg2Rad * alpha)), bottleHead.position.y, bottleHead.position.z + (zAxisDiagonal * Mathf.Cos(Mathf.Deg2Rad * alpha)));
        
        this.transform.LookAt(startPoint);
    }
}
