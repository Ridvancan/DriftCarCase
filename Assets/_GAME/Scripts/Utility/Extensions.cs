using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public static class Extensions
{
    public delegate void Method();
    public static void DO(this MonoBehaviour behaviour, float delay, Method method)
    {
        behaviour.StartCoroutine(DoCoroutine(delay, method));
    }
    private static IEnumerator DoCoroutine(float delay, Method method)
    {
        yield return new WaitForSecondsRealtime(delay * Time.timeScale);
        method.Invoke();
    }


    public static void LocalReset(this Transform obj)
    {
        obj.localPosition = Vector3.zero;
        obj.localRotation = Quaternion.identity;
        obj.localScale = Vector3.one;
    }


    public static Color MixColor(this Color currentColor, Color targetColor, float multiplier)
    {
        float currentMultiplier = 1 - multiplier;
        float targetMultiplier = multiplier;
        Color result = currentColor;
        for (int i = 0; i < 4; i++)
        {
            result[i] = currentColor[i] * currentMultiplier + targetColor[i] * targetMultiplier;
        }
        return result;
    }


    public static Vector3 SetX(this Vector3 vector, float x)
    {
        return new Vector3(x, vector.y, vector.z);
    }
    public static Vector3 SetY(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, y, vector.z);
    }
    public static Vector3 SetZ(this Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, z);
    }

    public static Vector3 AddX(this Vector3 vector, float x)
    {
        return new Vector3(vector.x + x, vector.y, vector.z);
    }
    public static Vector3 AddY(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, vector.y + y, vector.z);
    }
    public static Vector3 AddZ(this Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, vector.z + z);
    }

    public static Vector3 MultiplyX(this Vector3 vector, float x)
    {
        return new Vector3(vector.x * x, vector.y, vector.z);
    }
    public static Vector3 MultiplyY(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, vector.y * y, vector.z);
    }
    public static Vector3 MultiplyZ(this Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, vector.z * z);
    }


    public static void SetStartColor(this ParticleSystem.MainModule mainModule, Color color)
    {
        mainModule.startColor = color;
    }


    public static string ValueText(this float value)
    {
        return value.ToString("#0.00");
    }


    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }


    public static bool IsVisible(this Renderer renderer, Camera camera)
    {
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), renderer.bounds);
    }


    public static Tween DOBlendShapeWeight(this SkinnedMeshRenderer renderer, int index, float endValue, float duration)
    {
        float currentValue = renderer.GetBlendShapeWeight(index);
        return DOTween.To(() => currentValue, x => currentValue = x, endValue, duration).OnUpdate(() =>
        {
            renderer.SetBlendShapeWeight(index, currentValue);
        });
    }


    public static void SetTarget(this CinemachineVirtualCamera camera, Transform target)
    {
        camera.Follow = target;
        camera.LookAt = target;
    }


    public static Quaternion Rotation(this RaycastHit hit, Transform transform)
    {
        return Quaternion.FromToRotation(transform.up, hit.normal);
    }


    public static void Debug(this object obj)
    {
        UnityEngine.Debug.Log(obj);
    }


    public static Vector3[] Trajectory(this Vector3 startPosition, Vector3 endPosition, float midHeight, int pointCount)
    {
        Vector3 midPosition = startPosition + (endPosition - startPosition) / 2f + Vector3.up * midHeight;

        Vector3[] result = new Vector3[pointCount];

        result[0] = startPosition;
        for (int i = 1; i < pointCount - 1; i++)
        {
            float t = i / (float)pointCount;
            Vector3 m1 = Vector3.Lerp(startPosition, midPosition, t);
            Vector3 m2 = Vector3.Lerp(midPosition, endPosition, t);
            Vector3 position = Vector3.Lerp(m1, m2, t);
            result[i] = position;
        }
        result[pointCount - 1] = endPosition;

        return result;
    }
}
