using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICrashable
{
    void OnCrashed(float crashPower, Transform crashTransform);

}
