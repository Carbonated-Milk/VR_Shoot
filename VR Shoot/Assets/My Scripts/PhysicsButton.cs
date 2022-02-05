using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    public float resistance;
    public GameObject buttonHolder;
    private Rigidbody rb;

    public UnityEvent onPress;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.localPosition.y < 0)
        {
            rb.AddForce(Vector3.up * Time.deltaTime * resistance);

            if(transform.localPosition.y < buttonHolder.transform.localPosition.y/2)
            {
                Pressed();
            }

            if (transform.localPosition.y == buttonHolder.transform.localPosition.y)
            {
                
                if (rb.velocity.y < 0)
                {
                    rb.velocity = Vector3.zero;
                }

            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        transform.localPosition = Vector3.up * Mathf.Clamp(transform.localPosition.y, buttonHolder.transform.localPosition.y, 0f);
    }

    private void Pressed()
    {
        if (!GameManager.gameStart)
        {
            GameManager.StartGame();
        }
        //onPress.Invoke();
    }
    /*public static bool gameStart;
    public GameObject buttonHolder;
    [SerializeField] private float threshold = .2f;
    [SerializeField] private float deadZone = .025f;

    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;

    public UnityEvent onRelease;
    void Start()
    {
        _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }
        if(_isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
        /*if(transform.position.y < buttonHolder.transform.position.y && gameStart == false)
        {
            //Debug.Log("nice");
            gameStart = true;
            FindObjectOfType<AudioManager>().Play("Theme");
            //Pressed();
        }
    }

    private float GetValue()
    {
        var value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;
        if(Mathf.Abs(value) < deadZone)
        {
            value = 0;
        }
        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        _isPressed = true;
        Debug.Log("pressed");
    }

    private void Released()
    {
        _isPressed = false;
        //onPress.Invoke();
        Debug.Log("released");
    }*/
}
