using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/Player Rules")]
public class PlayerRules : ScriptableObject
{
    [SerializeField] private int maxHealth = 100;                       // The max health of the player.
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    [SerializeField] private int healthRecoverAmount = 2;               // The amount of health the player recovers every healthRecoverRate.
    public int HealthRecoverAmount { get => healthRecoverAmount; set => healthRecoverAmount = value; }

    [SerializeField] private int healthRecoverRate = 1;                 // The rate in which the player recovers health.
    public int HealthRecoverRate { get => healthRecoverRate; set => healthRecoverRate = value; }

    [SerializeField] private int timeUntillRecoverHealth = 5;           // The time thas has to pass with no damage taken before recovering health.
    public int TimeUntillRecoverHealth { get => timeUntillRecoverHealth; set => timeUntillRecoverHealth = value; }

    [SerializeField] private int maxStamina = 100;                      // The max stamina of the player.
    public int MaxStamina { get => maxStamina; set => maxStamina = value; }

    [SerializeField] private int staminaRecoverAmount = 4;              // The amount of stamina the player recovers every staminaRecoverRate.
    public int StaminaRecoverAmount { get => staminaRecoverAmount; set => staminaRecoverAmount = value; }

    [SerializeField] private int staminaRecoverRate = 1;                // The rate in which the player recovers stamina.
    public int StaminaRecoverRate { get => staminaRecoverRate; set => staminaRecoverRate = value; }

    [SerializeField] private int staminaUsageAmount = 10;               // The amount of stamina the player uses when sprinting every staminaUsageRate.
    public int StaminaUsageAmount { get => staminaUsageAmount; set => staminaUsageAmount = value; }

    [SerializeField] private int staminaUsageRate = 1;                  // The rate in which the player uses stamina.
    public int StaminaUsageRate { get => staminaUsageRate; set => staminaUsageRate = value; }

    [SerializeField] private int staminaResprintAmount = 20;            // The minimum amount of stamina that is required to be able to sprint again.
    public int StaminaResprintAmount { get => staminaResprintAmount; set => staminaResprintAmount = value; }

    [SerializeField] private int staminaPerInjection = 25;              // The amount of stamina that gets recovered after using an injection
    public int StaminaPerInjection { get => staminaPerInjection; set => staminaPerInjection = value; }

    [SerializeField] private float movementSpeed = 5.0f;                // The walking movement speed of the player.
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

    [SerializeField] private float sprintingSpeed = 10.0f;              // The running movement speed of the player.
    public float SprintingSpeed { get => sprintingSpeed; set => sprintingSpeed = value; }

    [SerializeField] private float jumpHeight = 4.0f;                   // How high a player will jump.
    public float JumpHeight { get => jumpHeight; set => jumpHeight = value; }

    [SerializeField] private float gravity = 9.81f;                     // The amount of gravity influencing the player.
    public float Gravity { get => gravity; set => gravity = value; }

    [SerializeField] private float interactionRange = 5.0f;             // The distance from which you can interact with an object.
    public float InteractionRange { get => interactionRange; set => interactionRange = value; }

    [SerializeField] private float mouseSensitivity = 5.0f;             // The mouse sensitivity.
    public float MouseSensitivity { get => mouseSensitivity; set => mouseSensitivity = value; }

    [SerializeField] private float speedBuff = 1.3f;                    // The speed multiplier when stamina injection has been used.
    public float SpeedBuff { get => speedBuff; set => speedBuff = value; }

    [SerializeField] private float speedBuffTime = 5.0f;                // The time the speedbuff applies on the player.
    public float SpeedBuffTime { get => speedBuffTime; set => speedBuffTime = value; }

    [SerializeField] private float slowBuff = 0.5f;                     // The slowdown multiplier when the speed buff is timed out.
    public float SlowBuff { get => slowBuff; set => slowBuff = value; }

    [SerializeField] private float slowBuffTime = 5.0f;                 // The time the slowbuff applies on the player.
    public float SlowBuffTime { get => slowBuffTime; set => slowBuffTime = value; }

    [SerializeField] private AudioClip hammer;
    public AudioClip Hammer { get => hammer; set => hammer = value; }

    [SerializeField] private AudioClip wrench;
    public AudioClip Wrench { get => wrench; set => wrench = value; }

    [SerializeField] private float sprintAccelarationTimeInSeconds = 0.5f;
    public float SprintAccelarationTimeInSeconds { get => sprintAccelarationTimeInSeconds; set => sprintAccelarationTimeInSeconds = value; }

    [SerializeField] private float sprintDeaccelarationTimeInSeconds = 0.2f;
    public float SprintDeaccelarationTimeInSeconds { get => sprintDeaccelarationTimeInSeconds; set => sprintDeaccelarationTimeInSeconds = value; }

    [Range(0, 5)] [SerializeField] float fieldOfViewDifference = 1.5f;
    public float FieldOfViewDifference { get => fieldOfViewDifference; set => fieldOfViewDifference = value; }
    [SerializeField] float throwSpeed = 7;
    public float ThrowSpeed { get => throwSpeed; set => throwSpeed = value; }
}