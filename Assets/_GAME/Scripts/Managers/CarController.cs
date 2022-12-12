using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum GameState { idle, inGame, fail, success }
public class CarController : BaseManager, ICrashable
{
    [Header("Inputs&Controls")]
    public GameState currentGameState;
    private Vector3 currentmousePos;
    private Vector3 prevmousePos;
    private Vector3 deltamousePos;
    private Vector2 rotationMovement;
    private Vector3 target;
    private Rigidbody carRigidbody;
    [SerializeField] private float acceleration;
    [SerializeField] private float speed = 0f;
    [SerializeField] private float MAX_SPEED = 20f;
    [SerializeField] private float rotationSpeed = 0f;
    bool crashable;
    [Header("WreckingBall")]
    [SerializeField] private BallController wreckingBallController;
    [Header("Driver")]
    [SerializeField] Animator driverAnimator;
    [SerializeField] Image driverEmoji;
    private void Start()
    {
        crashable = true;
        carRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (currentGameState != GameState.inGame) return;
        //Inputs
        prevmousePos = currentmousePos;
        currentmousePos = Input.mousePosition;
        deltamousePos = currentmousePos - prevmousePos;
        target = new Vector3(deltamousePos.x, 0);
        //InputsEnd
        RaycastHit raycastHit;

        if (Input.GetMouseButton(0))
        {
            if (speed < MAX_SPEED) speed += acceleration;
            transform.eulerAngles += new Vector3(0, deltamousePos.x, 0);

        }
        else
        {
            if (speed > 0) speed -= acceleration;
        }
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
    public void SetBoostModeOn()
    {
        wreckingBallController.SpinBall();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BoosterBox>())
        {
            other.gameObject.SetActive(false);
            SetBoostModeOn();
        }
    }
    public void ReactWin()
    {
        ShowEmoji(EmojiType.good);
        CheerAnimationDriver();
    }
    public void ReactTakeDamage()
    {
        UIHub.Instance.ShowEmoji(EmojiType.bad, driverEmoji);
    }
    public void ShowEmoji(EmojiType emojiType)
    {
        UIHub.Instance.ShowEmoji(emojiType, driverEmoji);
    }
    public void CheerAnimationDriver()
    {
        //TODO
    }
    IEnumerator CrashProtector()
    {
        yield return new WaitForSeconds(2);
        crashable = true;
    }
    public void OnCrashed(float crashPower, Transform crashTransform)
    {
        if (crashable)
        {
            crashable = false;
            carRigidbody.AddForceAtPosition(Vector3.one * crashPower, crashTransform.position);
            ReactTakeDamage();
            StartCoroutine(CrashProtector());
        }

    }
}
