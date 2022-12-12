using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBall : MonoBehaviour
{
    [SerializeField] BallController ballController;
    [SerializeField] float crashPower;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent(typeof(ICrashable)) && other.gameObject != ballController.connectedCar)
        {
            other.GetComponent<ICrashable>().OnCrashed(crashPower, transform);
        }
    }

}
