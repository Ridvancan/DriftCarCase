using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState { idle, atack, free, dead }
public class AIPlayer : MonoBehaviour, ICrashable
{
    private Rigidbody carRigidbody;
    [SerializeField] private float acceleration;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float MAX_SPEED = 20f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] Transform targetTransform;
    [SerializeField] Transform centerOfArenaTransform;
    [SerializeField] Transform nearEnemyTransform;
    Quaternion targetRotation;
    bool atacking;
    bool crashable;
    [SerializeField] PlayerState currentState;
    [SerializeField] Animator driverAnimator;
    [SerializeField] Image driverEmoji;
    [Header("WreckingBall")]
    [SerializeField] private BallController wreckingBallController;

    private void Start()
    {
        crashable = true;
        carRigidbody = GetComponent<Rigidbody>();
        SelectEnemy();

    }
    void StateChange(PlayerState aIState)
    {
        currentState = aIState;
    }
    IEnumerator AfterAttackPosition()
    {
        yield return new WaitForSeconds(2);
        StateChange(PlayerState.idle);
        StartCoroutine(GetNewEnemyPosition());
    }
    IEnumerator GetNewEnemyPosition()
    {
        yield return new WaitForSeconds(5);
        SelectEnemy();
    }
    void CheckState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.idle:
                targetRotation = Quaternion.LookRotation(nearEnemyTransform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                speed = Mathf.Clamp(Mathf.Lerp(speed, speed / 2, Time.deltaTime), 5, 15f);
                break;
            case PlayerState.atack:
                targetRotation = Quaternion.LookRotation(centerOfArenaTransform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed * 2);
                speed = Mathf.Clamp(Mathf.Lerp(speed, speed * 2, Time.deltaTime), 0, 15f);
                break;
            case PlayerState.free:

                break;
            case PlayerState.dead:

                break;
            default:
                break;
        }
    }
    void Update()
    {
        if (Vector3.Distance(targetTransform.position, transform.position) < 6f && currentState != PlayerState.atack)//so close to rival player
        {
            StateChange(PlayerState.atack);
            StartCoroutine(AfterAttackPosition());
        }
        CheckState(currentState);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }


    void SelectEnemy()
    {
        nearEnemyTransform = ManagerHub.Get<PlayersManager>().NearestEnemyTransform(transform);
        targetRotation = Quaternion.LookRotation(nearEnemyTransform.position - transform.position);
        currentState = PlayerState.idle;
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

