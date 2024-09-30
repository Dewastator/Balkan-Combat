using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(InputHandler))]
public class Player : MonoBehaviour
{
    private PlayerStateMachine stateMachine;
    
    public Rigidbody rb { get; set; }

    public InputHandler inputHandler { get; private set; }
    public GroundedCheck groundedCheck { get; private set; }

    public AnimationCurve jumpAnimationCurve;
    public AnimationCurve jumpDistanceAnimationCurve;
    public AnimationCurve fallAnimationCurve;

    [SerializeField]
    private Animator playerAnimator;


    [SerializeField]
    private Transform enemyCheckForward;
    [SerializeField]
    private Transform enemyCheckBack;
    public LayerMask enemyMask;

    public float movementSpeed;
    public float animMovementSpeed;

    [field: Header("JumpSettings")]
    [field: SerializeField]
    public float jumpSpeed { get; private set; }

    [field: SerializeField]
    public float jumpDistance { get; set; }

    public float startingJumpDistance;

    [field: SerializeField]
    public float jumpHeight { get; private set; }


    [field: Header("AttackPositionPoints")]
    [SerializeField]
    private Transform rightPuncHitPoint;
    [SerializeField]
    private Transform leftPunchHitPoint;
    [SerializeField]
    private Transform rightKickPointHitPoint;
    [SerializeField]
    private Transform leftKickPointHitPoint;

    [SerializeField]
    private UnityEvent<float> OnHit;

    private bool isEnemyRight;
    private bool isEnemyLeft;

    public int currentHitTimes { get; set; }

    public bool isHitted { get; set; }

    public bool isDead { get; set; }
    public bool isWon { get; set; }
    public bool isRotated { get; set; }
    public bool isplayer2Rotated { get; set; }
    public PlayerInputActions playerInput { get; private set; }

    [SerializeField]
    private UnityEvent OnCelebrationStart;

    [field: Header("FallSettings")]

    [field: SerializeField]
    public float fallSpeed { get; private set; }


    public Vector3 enemyPosToRotateTo { get; private set; }

    [SerializeField]
    private float timeToWaitForCelebration = 2f;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        groundedCheck = GetComponent<GroundedCheck>();
        SetupStates();
        animMovementSpeed = movementSpeed;
        startingJumpDistance = jumpDistance;
    }

    private void SetupStates()
    {
        stateMachine.Initialize(this, playerAnimator, rb);
    }

    private void Update()
    {
        EnemyCheck();

        stateMachine.CurrentPlayerState.OnUpdate();
    }

    private void EnemyCheck()
    {

        var isEnemyInFront = Physics.OverlapBox(enemyCheckForward.position, new Vector3(0.1f, 1.5f, 0.1f), Quaternion.identity, enemyMask).Length > 0;


        if (isEnemyInFront)
        {
            if (Helper.FacingRight(transform))
            {
                //movementSpeed = inputHandler.isMovingForward ? 0f : animMovementSpeed;
            }
            else
            {
                //movementSpeed = !inputHandler.isMovingForward ? 0f : animMovementSpeed;
            }
            //jumpDistance = 1.1f;
        }
        else
        {
            //movementSpeed = animMovementSpeed;
        }
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentPlayerState.OnFixedUpdate();
    }

    public void Move(float x)
    {
        //transform.position += new Vector3(x, 0f, 0f) * movementSpeed * Time.deltaTime;
        //rb.MovePosition(new Vector3(x,0f,0f) * movementSpeed * Time.deltaTime);

        rb.velocity = new Vector3(x,0f,0f) * movementSpeed;
    }

    public void ChangeAnimation(string animation, float crossFade, int layer = 0, float time = 0f, float duration = 0f)
    {
        playerAnimator.CrossFadeInFixedTime(animation, crossFade, layer, 0, time);
    }

    public void TakeDamage(string attackType)
    {
        var hitPointPosition = Vector3.zero;
        
        switch (attackType)
        {
            case "RightPunch":
                hitPointPosition = rightPuncHitPoint.position;
                break;
            case "LeftPunch":
                hitPointPosition = leftPunchHitPoint.position;
                break;
            case "RightKick":
                hitPointPosition = rightKickPointHitPoint.position;
                break;
            case "LeftKick":
                hitPointPosition = leftKickPointHitPoint.position;
                break;

        }

        Collider[] colliders = Physics.OverlapSphere(hitPointPosition, 0.05f, enemyMask);

        
        foreach(Collider collider in colliders)
        {
            if (collider != null)
            {
                if(collider.gameObject.GetComponent<Player>() != null)
                {
                    collider.gameObject.GetComponent<Player>().GetHit();
                }
            }
        }
    }

    private void GetHit()
    {
        if(isHitted) return;

        isHitted = true;
        float damage = 0;
        if (inputHandler.isBlocking)
        {
            damage = 2;
        }
        else
        {
            damage = 5;
        }
        OnHit.Invoke(damage);
        currentHitTimes += 1;
    }

    public void Die()
    {
        isDead = true;
    }

    public void Win()
    {
        Invoke(nameof(StartWinAnimation), timeToWaitForCelebration);
    }

    public void Rotate(Vector3 posToRotateTo)
    {
        isRotated = true;
        enemyPosToRotateTo = posToRotateTo;
    }
    public void StartWinAnimation()
    {
        OnCelebrationStart.Invoke();
        isWon = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(enemyCheckForward.position, enemyCheckForward.forward * 0.1f);
        //Draw a cube at the maximum distance
        Gizmos.DrawWireCube(enemyCheckForward.position + enemyCheckForward.forward * 0.1f, new Vector3(0.3f, 1.5f, 0.3f));
    }

}


public enum PlayerAnimation
{
    Idle,
    Walking,
    Jumping_Up,
    Forward_Flip,
    Backflip,
    Right_Kick,
    Right_Punch,
    Left_Kick,
    Left_Punch,
    Head_Hit,
    Death,
    Celebration,
    Air_Hit,
    Standing_To_Crouch,
    Crouch_Idle,
    Crouch_To_Standring,
    Block,
    Block_Hit
}
