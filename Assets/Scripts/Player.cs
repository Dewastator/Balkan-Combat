using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InputHandler))]
public class Player : MonoBehaviour
{
    private PlayerStateMachine stateMachine;
    #region States
    private IdleState idleState;
    private MoveState moveState;
    private JumpState jumpState;
    private AttackState attackState;
    private HitState hitState;
    private DeathState deathState;
    private CelebrationState celebrationState;
    #endregion
    public Rigidbody rb { get; set; }

    public InputHandler inputHandler { get; private set; }
    public GroundedCheck groundedCheck { get; private set; }

    public AnimationCurve jumpAnimationCurve;
    public AnimationCurve jumpDistanceAnimationCurve;

    public float startingYPos;
    public float startingXPos;

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private List<AnimationClip> animationClips = new List<AnimationClip>();

    [SerializeField]
    private Transform enemyCheckForward;
    [SerializeField]
    private Transform enemyCheckBack;
    public LayerMask enemyMask;
    public bool isInAir => inputHandler.IsInAir();

    public float movementSpeed;
    public float animMovementSpeed;

    [field: Header("JumpSettings")]
    [field: SerializeField]
    public float jumpSpeed { get; private set; }

    [field: SerializeField]
    public float jumpDistance { get; private set; }

    [field: SerializeField]
    public float jumpHeight { get; private set; }


    [field: Header("AttackPoints")]
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

    private bool isDead { get; set; }
    private bool isWon { get; set; }
    public PlayerInputActions playerInput { get; private set; }

    [SerializeField]
    private UnityEvent OnCelebrationStart;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        groundedCheck = GetComponent<GroundedCheck>();
        startingYPos = transform.position.y;
        SetupStates();
        animMovementSpeed = movementSpeed;
    }

    private void SetupStates()
    {
        idleState = new IdleState(this, stateMachine, playerAnimator, rb);
        moveState = new MoveState(this, stateMachine, playerAnimator, rb);
        jumpState = new JumpState(this, stateMachine, playerAnimator, rb);
        attackState = new AttackState(this, stateMachine, playerAnimator, rb);
        hitState = new HitState(this, stateMachine, playerAnimator, rb);
        deathState = new DeathState(this, stateMachine, playerAnimator, rb);
        celebrationState = new CelebrationState(this, stateMachine, playerAnimator, rb);
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        EnemyCheck();

        if (inputHandler.moveInput.x != 0 && groundedCheck.isGrounded && !isInAir && !inputHandler.isAttacking && !isHitted && !isDead && !isWon)
        {
            stateMachine.ChangeState(moveState);
        }
        else if (groundedCheck.isGrounded && isInAir && !inputHandler.isAttacking && !isHitted && !isDead && !isWon)
        {
            stateMachine.ChangeState(jumpState);
        }
        else if (!isInAir && groundedCheck.isGrounded && !inputHandler.isAttacking && !isHitted && !isDead && !isWon)
        {
            stateMachine.ChangeState(idleState);
        }
        else if (inputHandler.isAttacking && groundedCheck.isGrounded && !isHitted && !isDead && !isWon)
        {
            stateMachine.ChangeState(attackState);
        }
        else if (isHitted && !isDead && !isWon)
        {
            stateMachine.ChangeState(hitState);
        }
        else if (isDead)
        {
            stateMachine.ChangeState(deathState);
        }
        else if (isWon)
        {
            stateMachine.ChangeState(celebrationState);
        }

        stateMachine.CurrentPlayerState.OnUpdate();
    }

    private void EnemyCheck()
    {
        if (!Helper.FacingRight(transform))
        {
            isEnemyRight = Physics.Raycast(enemyCheckBack.position, enemyCheckBack.TransformDirection(Vector3.forward), 0.1f, enemyMask);
            isEnemyLeft = Physics.Raycast(enemyCheckForward.position, enemyCheckForward.TransformDirection(Vector3.forward), 0.1f, enemyMask);
        }
        else
        {
            isEnemyRight = Physics.Raycast(enemyCheckForward.position, enemyCheckForward.TransformDirection(Vector3.forward), 0.1f, enemyMask);
            isEnemyLeft = Physics.Raycast(enemyCheckBack.position, enemyCheckBack.TransformDirection(Vector3.forward), 0.1f, enemyMask);
        }

        if (isEnemyRight)
        {
            movementSpeed = inputHandler.isMovingRight ? 0f : animMovementSpeed;
        }
        else if (isEnemyLeft)
        {
            movementSpeed = inputHandler.isMovingLeft ? 0f : animMovementSpeed;
        }
        else
        {
            movementSpeed = animMovementSpeed;
        }
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentPlayerState.OnFixedUpdate();
    }

    public void Move(float x)
    {
        transform.position += new Vector3(x, 0f, 0f) * movementSpeed * Time.deltaTime;
    }

    public void ChangeAnimation(PlayerAnimation animation, float crossFade, int layer = 0, float time = 0f, float duration = 0f)
    {
        playerAnimator.CrossFadeInFixedTime(animationClips[(int)animation].name, crossFade, layer, 0, time);
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
        isHitted = true;
        OnHit.Invoke(5);
        currentHitTimes += 1;
    }

    public void Die()
    {
        isDead = true;
    }

    public void Win()
    {
        Invoke("StartWinAnimation", animationClips[(int)PlayerAnimation.Death].length);
    }

    public void StartWinAnimation()
    {
        OnCelebrationStart.Invoke();
        isWon = true;
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(enemyCheckForward.position, enemyCheckForward.TransformDirection(Vector3.forward), Color.red);
        Debug.DrawRay(enemyCheckBack.position, enemyCheckBack.TransformDirection(Vector3.forward), Color.red);
    }

}


public enum PlayerAnimation
{
    Idle,
    Move,
    JumpUp,
    Frontflip,
    Backflip,
    RightLegKick,
    RightPunch,
    LeftLegKick,
    LeftPunch,
    Hit,
    Death,
    Celebration
}
