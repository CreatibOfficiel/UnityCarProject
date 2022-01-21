using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public int speedMax;
    public bool kinematicVelocity = false;
    public Light light;
    private bool inside = false, check = true;

    public void Update()
    {
        speedMax = StarsManager.GetInstance().getAllowedSpeed();
        if (inside && check)
        {
            StartCoroutine(ReleaseValues());
            float actualSpeed = PlayerController_Physique13.getInstance().getCurrentSpeed();
            if (actualSpeed > speedMax)
            {
                inside = false;
                flasher(actualSpeed);
            }
        }         
    }

    private void OnTriggerEnter(Collider other)
    {
        inside = true;
        StarsManager.GetInstance().chooseAllowedSpeed();
    }

    private void flasher(float speedValue)
    {
        StartCoroutine(flash());
        StarsManager.GetInstance().removePoint(speedValue);
    }

    IEnumerator ReleaseValues()
    {
        check = false;
        yield return new WaitForSeconds(2.0f);
        check = true;
    }

    private IEnumerator flash()
    {
        yield return null;
        light.enabled = true;
        yield return new WaitForSeconds(0.15f);
        light.enabled = false;
    }
}
