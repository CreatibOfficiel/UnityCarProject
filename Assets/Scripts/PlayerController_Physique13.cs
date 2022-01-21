using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController_Physique13 : MonoBehaviour
{
    private float horizontalInput;
    private float forwardInput;
    private float turnSpeed = 45.0f;

    [SerializeField]
    [Range(0, 1)]
    private float inertie;

    [SerializeField]
    private AnimationCurve speedCurve;
    private Rigidbody RB;

    public float speedMax, speedMin, speedCurrent;

    [SerializeField]
    private bool mooveOnlyIfIsGrounded, mooveXZ = true;

    [SerializeField]
    private DataScore maxDistanceData;

    private bool IsGrounded, nitro = false;

    private static PlayerController_Physique13 instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("double player controller");
            return;
        }
        else instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(speedChange());
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
    }

    private void OnTriggerEnter(Collider collider)
    {
        IsGrounded = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        IsGrounded = false;
    }

    public void FixedUpdate()
    {
        // Acceleration
        if (forwardInput >= 0.0f)
        {
            speedCurrent = Mathf.Min(speedCurrent + speedCurve.Evaluate(forwardInput) * 1.25f, speedMax);
        }

        // Deceleration
        if (forwardInput < 0.0f)
        {
            speedCurrent = Mathf.Max(speedCurrent - speedCurve.Evaluate(-forwardInput) * 1.0f, speedMin);
        }

        if (transform.position.y < -2)
        {
            death();
        }
            

        // Physique de déplacement RigidBody

        Vector3 deltaPos = RB.transform.forward * Time.fixedDeltaTime * speedCurrent;
        if (mooveXZ)
        {
            RB.MovePosition(new Vector3(RB.position.x + deltaPos.x, transform.position.y, RB.position.z + deltaPos.z));
            Vector3 t = new Vector3(0, horizontalInput * turnSpeed * Time.fixedDeltaTime, 0);
            Quaternion deltaRotation = Quaternion.Euler(t);
            Quaternion temp = RB.rotation * deltaRotation;
            RB.MoveRotation(temp);
        } else if (IsGrounded && mooveOnlyIfIsGrounded)
        {
            RB.MovePosition(new Vector3(RB.position.x + deltaPos.x, transform.position.y, RB.position.z + deltaPos.z));
            Vector3 t = new Vector3(0, horizontalInput * turnSpeed * Time.fixedDeltaTime, 0);
            Quaternion deltaRotation = Quaternion.Euler(t);
            Quaternion temp = RB.rotation * deltaRotation;
            RB.MoveRotation(temp);
        }
        else
        {
            Quaternion Rotation = Quaternion.Euler(Vector3.up * turnSpeed * horizontalInput * Time.fixedDeltaTime);
            RB.MoveRotation(RB.rotation * Rotation);
            RB.MovePosition(RB.position + transform.forward * Time.fixedDeltaTime * speedCurrent);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer.ToString() == "8" && !nitro)
        {
            speedMax += 10;
            speedCurrent += 50;
            nitro = true;
            StartCoroutine(endNitro());
        }

        if (collision.gameObject.CompareTag("Obstacles"))
        {
            if(speedCurrent - 10 < 0) {
                speedCurrent = 0;
            }
            else
            {
                speedCurrent -= 10;
            }
        }
    }

    public void death()
    {
        if(GameManager.GetInstance().gamemode == GameManager.GameMode.STANDART)
        {
            if (maxDistanceData.maxDistanceStandart < transform.position.z)
            {
                maxDistanceData.maxDistanceStandart = (int)transform.position.z;
            }
                
        }

        if(GameManager.GetInstance().gamemode == GameManager.GameMode.CHRONOMODE)
        {
            if (maxDistanceData.maxDistanceChrono < transform.position.z)
                maxDistanceData.maxDistanceChrono = (int)transform.position.z;
        }

        SceneController.GetInstance().loadScene(4);
    }

    IEnumerator endNitro()
    {
        yield return new WaitForSeconds(2f);
        speedMax -= 10;
        nitro = false;
    }

    IEnumerator speedChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            if (speedCurrent > speedMin)
            {
                speedCurrent *= inertie;
            }
            else speedCurrent = speedMin;
        }
    }

    public static PlayerController_Physique13 getInstance()
    {
        return instance;
    }

    public float getCurrentSpeed()
    {
        return speedCurrent;
    }
}
