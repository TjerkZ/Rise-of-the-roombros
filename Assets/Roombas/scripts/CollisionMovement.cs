using UnityEngine;

public class CollisionMovement : MonoBehaviour
{
    [SerializeField] float rotationspeed = 20;
    [SerializeField][Range(0f,360f)] float direction = 0f;
    private Vector3 RotateTo = new Vector3(0, -45, 0);
    private bool Rotating = true;
    private bool TuringRight = true;

    // Start is called before the first frame update
    void Start()
    {
        TuringRight = (Random.value > 0.5f);
        RotateTo = GetNewAngle();
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
            Debug.Log("Hit Object");
        }
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * 2 * Time.deltaTime);
    }

    Vector3 GetNewAngle()
    {
        //switch direction everytime function is called
        TuringRight = !TuringRight;

        float randomRange = Random.Range(direction + 40f, direction + 70f);

        return new Vector3(0, ConvertToAngle(randomRange), 0);
    }

    private float ConvertToAngle(float angle)
    {
        if (TuringRight)
        {
            angle = direction + angle;
        }
        else
        {
            angle = direction - angle;
        }


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
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotationspeed);
        }else if (!(transform.eulerAngles.y > RotateTo.y - 1 && transform.eulerAngles.y < RotateTo.y) && !TuringRight)
        {
            transform.Rotate(rotationspeed * Time.deltaTime * new Vector3(0, -1, 0));
        }else{
            Rotating = false;
        }
    }
}
