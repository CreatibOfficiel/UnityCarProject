using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Physique23 : MonoBehaviour
{

    private float horizontalInput;
    private float forwardInput;
    private float turnSpeed = 45.0f;
    private Rigidbody RB;

    // Speed parameters
    [Header("Speed parameters")]
    [SerializeField]
    private AnimationCurve speedCurve;
    [Range(0, 1)]
    private float inertie;
    [SerializeField]
    GameObject virtualGround;
    [SerializeField]
    private float speedMax, speedMin, speedCurrent;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(speedChange());
        RB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Acceleration
        if (forwardInput > 0.0f)
            speedCurrent = Mathf.Min(speedCurrent + speedCurve.Evaluate(forwardInput) * 1.25f, speedMax);

        // Deceleration
        if (forwardInput < 0.0f)
            speedCurrent = Mathf.Max(speedCurrent - speedCurve.Evaluate(-forwardInput) * 1.0f, speedMin);

        RB.velocity += RB.transform.forward * speedCurrent * Time.fixedDeltaTime;
        RB.angularVelocity = new Vector3(0, horizontalInput * turnSpeed * Time.fixedDeltaTime, 0);
        virtualGround.transform.position = new Vector3(RB.position.x, virtualGround.transform.position.y, RB.position.z);
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
}
