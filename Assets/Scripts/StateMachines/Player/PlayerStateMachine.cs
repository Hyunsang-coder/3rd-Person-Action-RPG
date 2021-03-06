using UnityEngine;
using System;

public class PlayerStateMachine : StateMachine
{
    // Monobehavior 상속 안 했지만 에디터창에 표시하기 위한 어트리뷰트
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public RagDoll Ragdoll { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public float DodgeLength { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeCoolDown { get; private set; }

    [field: SerializeField] public Attack[] Attacks { get; private set; }
    public Transform MainCameraTransform { get; private set; }
    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamge += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void HandleDie()
    {
        SwitchState(new PlayerDeadState(this));
    }

    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }

    public void SetDodgeTime(float time)
    {
        PreviousDodgeTime = time;
    }


    private void OnDisable()
    {
        Health.OnTakeDamge -= HandleTakeDamage;
        Health.OnDie += HandleDie;
    }
}
