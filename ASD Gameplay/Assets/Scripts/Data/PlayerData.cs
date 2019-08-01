using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField] private PlayerRules playerRules;
    public PlayerRules PlayerRules { get => playerRules; set => playerRules = value; }

    [SerializeField] private float speedMultiplier;
    [SerializeField] private int currentHealth;
    [SerializeField] private int currentStamina;
    [SerializeField] private float currentSpeed;

    public bool damageTaken { get; private set; }
    public bool sprinting { get; private set; }
    public bool exhausted { get; private set; }

    const int DEFAULT_FIELD_OF_VIEW = 60;
    const int FOV_CHANGE_MULTIPLIER = 20;

    public float SpeedMultiplier
    {
        get { return speedMultiplier; }
        set
        {
            float oldSpeedMultiplier = speedMultiplier;
            speedMultiplier = Mathf.Clamp(value, 0, 100);
            CurrentSpeed *= speedMultiplier / oldSpeedMultiplier;
        }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, PlayerRules.MaxHealth); }
    }

    public int CurrentStamina
    {
        get { return currentStamina; }
        private set { currentStamina = Mathf.Clamp(value, 0, PlayerRules.MaxStamina); }
    }

    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set
        {
            currentSpeed = Mathf.Clamp(value, 0, PlayerRules.SprintingSpeed * SpeedMultiplier);
            sprinting = value >= PlayerRules.SprintingSpeed / speedMultiplier;
        }
    }

    public void Setup()
    {
        SpeedMultiplier = 1; //this also gets done in PlayerRules
        CurrentHealth = PlayerRules.MaxHealth;
        CurrentStamina = PlayerRules.MaxStamina;
        CurrentSpeed = PlayerRules.MovementSpeed * SpeedMultiplier;
    }

    public void AddStamina(int amount)
    {
        CurrentStamina += amount;
        exhausted = CurrentStamina < PlayerRules.StaminaResprintAmount;
    }

    public void useStamina(int amount)
    {
        AddStamina(-amount);
    }

    public void AddHealth(int amount)
    {
        CurrentHealth += amount;
        damageTaken = amount < 0;
    }

    public void TakeDamage(int damage)
    {
        AddHealth(-damage);
    }
}