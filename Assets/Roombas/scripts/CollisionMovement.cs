using UnityEngine;

public class CollisionMovement : MonoBehaviour
{
    [SerializeField] float RotationSpeed = 20;
    [SerializeField] float MoveSpeed = 2;
    [SerializeField] float chaseRange = 10f;
    [SerializeField][Range(0f,359f)] float direction = 0f;
    [SerializeField][Range(0f, 45f)] float MinAngle = 40f;
    [SerializeField][Range(46f, 90f)] float MaxAngle = 70f;

    private Vector3 RotateTo;
    private bool Rotating = true;
    private bool TuringRight = true;
    private float distanceToTarget = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        TuringRight = (Random.value > 0.5f);
        RotateTo = GetNewAngle();
        //set object to move direction
        transform.Rotate(new Vector3(0, direction, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Rotating)
        {
            RotateToAngle();
        }
        else
        {
            MoveForward();
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "RoombaBorder")
        {
            Debug.Log("rotating");
            RotateTo = GetNewAngle();
            Rotating = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
    }

    Vector3 GetNewAngle()
    {
        //switch direction everytime function is called
        TuringRight = !TuringRight;

        float randomRange = Random.Range(MinAngle, MaxAngle);
        Debug.Log(randomRange);
        Debug.Log(ConvertToAngle(randomRange));

        return new Vector3(0, ConvertToAngle(randomRange), 0);
    }

    private float ConvertToAngle(float angle)
    {
        //calulate angle based on direction
        if (TuringRight)
        {
            angle = direction + angle;
        }
        else
        {
            angle = direction - angle;
        }

        //convert the angel over 360 and under 0 to stay in 360 range
        if (angle >= 360)
        {
            angle -= 360;
        }
        else if (angle <= 0)
        {
            angle += 360;
        }

        return angle;
    }

    void RotateToAngle()
    {
        //Debug.Log("rotation:" + transform.eulerAngles.y + " turnto: " + RotateTo.y);
        //Debug.Log(transform.eulerAngles.y);

        if (!(transform.eulerAngles.y > RotateTo.y && transform.eulerAngles.y < RotateTo.y + 1f) && TuringRight)
        {
            transform.Rotate(RotationSpeed * Time.deltaTime * new Vector3(0, 1, 0));
        }else if (!(transform.eulerAngles.y > RotateTo.y - 1 && transform.eulerAngles.y < RotateTo.y) && !TuringRight)
        {
            transform.Rotate(RotationSpeed * Time.deltaTime * new Vector3(0, -1, 0));
        }else{
            Rotating = false;
        }
    }
}
