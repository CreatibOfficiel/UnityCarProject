using UnityEngine;

public class Move : MonoBehaviour
{
    // Road prefab (useful to get the maximum range where the crate is able to go)
    [SerializeField]
    private GameObject roadPatern;
    [SerializeField]
    [Tooltip("Generate random value if set to 0")]
    [Range(0.0f, 10.0f)]
    private float speed = 0.0f;

    private float roadWidth;
    private bool randomSpeed = true;
    private Vector3 target;

#if UNITY_EDITOR
    void OnValidate()
    {
        // Clamp the speed to very low value if negative
        if (speed < 0.0f)
            speed = 0.1f;
        // If user choose a speed of 0, switch to random speed mod
        if (speed == 0.0f)
            randomSpeed = true;
        else
            randomSpeed = false;
    }
#endif

    void Start()
    {
        // Get the road size
        roadWidth = roadPatern.GetComponent<Renderer>().bounds.size.x / 2;

        // First position of the target
        target = new Vector3(Random.Range(-roadWidth + 2.0f, +roadWidth - 2.0f), 0, transform.position.z);
        // First speed
        if (randomSpeed)
            speed = Random.Range(1.0f, 10.0f);
    }

    void FixedUpdate()
    {
        // Move toward the target by speed * Time.deltaTime
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);

        // Whene the crate reach the target position
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            // Pick up a new x position to go
            target.x = Random.Range(-roadWidth + 2.0f, +roadWidth - 2.0f);

            // Pick up a new random speed
            if (randomSpeed)
                speed = Random.Range(1.0f, 10.0f);
        }
    }
}
