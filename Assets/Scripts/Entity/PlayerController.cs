using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    //    [HideInInspector]
    public float currentSpeed;
    public float maxAngularVelocity = 25;
    [Header("Gameplay Config")]
    public float turnTime = .6f;
    public float horizontalThresholdSwipe = .6f;
    public float horizontalDistance;
    public int currentLand = 2;
    public int minLand = 0;
    public int maxLand = 4;
    public AudioSource turnSound;

    private Rigidbody rigid;
    private Vector3 mouseDownPosition;
    private Vector3 mouseUpPosition;
    private bool finishTurn;
    
    private float xDistance;

    public bool allowControl = true;


    void Start()
    {

        finishTurn = true;
        rigid = GetComponent<Rigidbody>();

        rigid.maxAngularVelocity = maxAngularVelocity;
        rigid.ResetCenterOfMass();
    }

    // Update is called once per frame
    void Update()
    {
        if (!allowControl) return;

            if (finishTurn)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mouseDownPosition = Input.mousePosition; //Get mouse down position
                }
                
                if (Input.GetMouseButtonUp(0))
                {

                    mouseUpPosition = Input.mousePosition; //Get mouse position

                    xDistance = (mouseDownPosition.x - mouseUpPosition.x)/Screen.width; //Caculate the distance between them

                    // Side swiping
                    if (Mathf.Abs(xDistance) > horizontalThresholdSwipe && mouseDownPosition.x != 0)
                    {
                        if (xDistance < 0) // Right
                        {
                            StartCoroutine(TurnRight());

                        }
                        else // Left
                        {
                            StartCoroutine(TurnLeft());
                        }
                    }
                }
            }
    }

    IEnumerator TurnRight()
    {
        if (currentLand < maxLand)
        {

            currentLand++;

            if (turnSound != null) turnSound.Play();

            finishTurn = false;

            yield return new WaitForFixedUpdate();

            float startX = Mathf.Round(transform.position.x);
            float endX = startX + horizontalDistance;

            if (endX <= 8)
            {
                float t = 0;
                while (t < turnTime)
                {
                    t += Time.deltaTime;
                    float fraction = t / turnTime;
                    float newX = Mathf.Lerp(startX, endX, fraction);
                    Vector3 newPos = transform.position;
                    newPos.x = newX;
                    transform.position = newPos;
                    yield return null;
                }
            }
            finishTurn = true;
        }
    }

    IEnumerator TurnLeft()
    {
        if (currentLand > minLand)
        {
            currentLand--;

            if (turnSound != null) turnSound.Play();

            finishTurn = false;

            yield return new WaitForFixedUpdate();

            float startX = Mathf.Round(transform.position.x);
            float endX = startX - horizontalDistance;

            if (endX >= -8)
            {
                float t = 0;
                while (t < turnTime)
                {
                    t += Time.deltaTime;
                    float fraction = t / turnTime;
                    float newX = Mathf.Lerp(startX, endX, fraction);
                    Vector3 newPos = transform.position;
                    newPos.x = newX;
                    transform.position = newPos;
                    yield return null;
                }
            }

            finishTurn = true;
        }
    }


    IEnumerator AddForce(Rigidbody rigid, bool isForPlayer, float minForce, float maxForce, Vector3 dirCollision,Rigidbody other)
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForFixedUpdate();
            Vector3 torqueDir = (isForPlayer) ? (-dirCollision * 500f) : (dirCollision * 40f);
            rigid.AddTorque(torqueDir);
        }
        yield return new WaitForEndOfFrame();
        Vector3 angularV = rigid.angularVelocity;
        angularV.x /= Mathf.Abs(angularV.x);
        angularV.y /= Mathf.Abs(angularV.y);
        angularV.z /= Mathf.Abs(angularV.z);

        rigid.angularVelocity = angularV * maxAngularVelocity;
    }

    public void DisableControl()
    {
        allowControl = false;
    }
}
