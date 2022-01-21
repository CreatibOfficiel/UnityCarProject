using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float forwardInput;
    private float turnSpeed = 45.0f;

    [SerializeField]
    [Range(0, 1)]
    private float inertie;

    [SerializeField]
    private AnimationCurve speedCurve;

    [SerializeField]
    private float speedMax, speedMin, speedCurrent;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(speedChange());
    }

    public void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Acceleration
        if (forwardInput > 0.0f)
            speedCurrent = Mathf.Min(speedCurrent + speedCurve.Evaluate(forwardInput) * 1.25f, speedMax);

        // Deceleration
        if (forwardInput < 0.0f)
            speedCurrent = Mathf.Max(speedCurrent - speedCurve.Evaluate(-forwardInput) * 1.0f, speedMin);

        transform.Translate(Vector3.forward * Time.fixedDeltaTime * speedCurrent);
        transform.Rotate(Vector3.up, turnSpeed * Time.fixedDeltaTime  * horizontalInput);
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
