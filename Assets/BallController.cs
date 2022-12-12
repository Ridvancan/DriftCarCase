using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallController : MonoBehaviour
{
    [SerializeField] public GameObject connectedCar;
    [SerializeField] private float lerpSpeed = 1;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] TrailRenderer wreckingBallTrailRenderer;
    [SerializeField] Transform wreckingBallTransform;
    [SerializeField] private bool isSpinActive;
    Tween boostTween;
    Vector3 BalldefaultPosition;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        BalldefaultPosition = wreckingBallTransform.localPosition;
    }
    private void FixedUpdate()
    {

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, wreckingBallTransform.position);


    }
    public void EnableTrail()
    {
        wreckingBallTrailRenderer.enabled = true;
    }
    IEnumerator DisableTrail()
    {
        yield return new WaitForSeconds(2);
        wreckingBallTrailRenderer.enabled = false;
    }
    void Update()
    {
        transform.position = connectedCar.transform.position;
        if (!isSpinActive)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, connectedCar.transform.rotation, Time.deltaTime * lerpSpeed);
            wreckingBallTransform.localPosition = Vector3.Lerp(wreckingBallTransform.localPosition, BalldefaultPosition, Time.deltaTime * lerpSpeed);
        }

    }
    public void StopSpin()
    {
        isSpinActive = false;
        lineRenderer.enabled = true;
        StartCoroutine(DisableTrail());
    }
    public void SpinBall()
    {
        isSpinActive = true;
        EnableTrail();
        lineRenderer.enabled = false;
        boostTween = transform.DORotate(Vector3.up * 360 * 6, 3, RotateMode.FastBeyond360).SetRelative().OnComplete(() => StopSpin());
    }
}
