using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Physique33 : MonoBehaviour
{
    private float horizontalInput;
    private float forwardInput;
    private float turnSpeed = 45.0f;
    private Rigidbody RB;

    [SerializeField]
    private AnimationCurve speedCurve;
    [Range(0, 1)]
    private float inertie;
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

        RB.AddRelativeForce(Vector3.forward * forwardInput, ForceMode.VelocityChange);

        RB.AddTorque(0, turnSpeed * horizontalInput, 0, ForceMode.VelocityChange);
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
