using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerRules playerRules;
    public PlayerRules PlayerRules { get => playerRules; set => playerRules = value; }

    [SerializeField] private PlayerData playerData;
    public PlayerData PlayerData { get => playerData; set => playerData = value; }

    [SerializeField] private Transform playerHand;
    public Transform PlayerHand { get => playerHand; set => playerHand = value; }

    private Vector3 input;

    private Vector3 moveDirection;
    private CharacterController controller;
    private Coroutine timeUntilRecoverHealth;
    private Coroutine fireTimer;
    private Coroutine walking;

    private float movingRight;
    private float movingUp;
    private float timeInAir;
    private bool running;

    private const float FOV_CHANGE_MULTIPLIER = 0.05f;

    private void OnEnable()
    {
        StopCoroutine(boost());
        PlayerData.SpeedMultiplier = 1;
        PlayerData.CurrentHealth = PlayerRules.MaxHealth;
        moveDirection = Vector3.zero;
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");
    }

    public void Move()
    {
        float oldY = moveDirection.y;
        if (controller.isGrounded)
        {
            timeInAir = 1;

            if (!IsMoving() && walking != null)
            {
                StopCoroutine(walking);
                walking = null;
            } 

            if (Input.GetButtonDown("Jump"))
            {
                oldY = PlayerRules.JumpHeight;
            }
            moveDirection = transform.TransformDirection(input).normalized * PlayerData.CurrentSpeed * PlayerData.SpeedMultiplier;
            movingRight = 0;
            movingUp = 0;
            
        }
        else
        {
            //timeInAir makes the player slow down a little bit the longer it is in air (after 2 seconds it would stand still)
            timeInAir = Mathf.Clamp(timeInAir - (Time.deltaTime / 2), 0, 1);
            if (input.x != 0) movingRight = input.x;
            if (input.z != 0) movingUp = input.z;
            moveDirection = transform.TransformDirection(new Vector3(movingRight, 0.0f, movingUp)).normalized * PlayerData.CurrentSpeed * PlayerData.SpeedMultiplier * timeInAir;
        }
        // Apply gravity
        moveDirection.y = oldY - PlayerRules.Gravity * Time.deltaTime;
        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }

  

    public void TakeDamage(int damage)
    {
        PlayerData.TakeDamage(damage);
        if (timeUntilRecoverHealth != null)
        {
            StopCoroutine(timeUntilRecoverHealth);
        }
        timeUntilRecoverHealth = StartCoroutine(TimeUntilRecoverHealth());
    }

    private IEnumerator RecoverHealth()
    {
        while (true)
        {
            PlayerData.AddHealth(PlayerRules.HealthRecoverAmount);
            yield return new WaitForSeconds(PlayerRules.HealthRecoverRate);
        }
    }

    private IEnumerator TimeUntilRecoverHealth()
    {
        yield return new WaitForSeconds(PlayerRules.TimeUntillRecoverHealth);
        if (timeUntilRecoverHealth != null)
        {
            StopCoroutine(timeUntilRecoverHealth);
            timeUntilRecoverHealth = null;
        }
        timeUntilRecoverHealth = StartCoroutine(RecoverHealth());
    }

    public IEnumerator Walk()
    {
        float speedIncreasement = -((playerRules.MovementSpeed * playerData.SpeedMultiplier) - playerData.CurrentSpeed) / 10;
        while (playerData.CurrentSpeed > playerRules.MovementSpeed * playerData.SpeedMultiplier)
        {
            playerData.CurrentSpeed -= speedIncreasement;
            yield return new WaitForSeconds(playerRules.SprintDeaccelarationTimeInSeconds / 10);
        } running = false;
    }

    public IEnumerator Sprint()
    {
        float speedIncreasement = ((playerRules.SprintingSpeed * playerData.SpeedMultiplier) - playerData.CurrentSpeed) / 10;
        
        while (playerData.CurrentSpeed < playerRules.SprintingSpeed * playerData.SpeedMultiplier)
        {
            playerData.CurrentSpeed += speedIncreasement;
            yield return new WaitForSeconds(playerRules.SprintAccelarationTimeInSeconds / 10);
        }
        running = true;
    }

    public IEnumerator UseStamina()
    {
        while (true)
        {
            if (!PlayerData.sprinting)
                yield break;
            PlayerData.AddStamina(-PlayerRules.StaminaUsageAmount);
            yield return new WaitForSeconds(PlayerRules.StaminaUsageRate);
        }
    }

    public IEnumerator RecoverStamina()
    {
        while (true)
        {
            if (PlayerData.sprinting)
                yield break;
            PlayerData.AddStamina(PlayerRules.StaminaRecoverAmount);
            yield return new WaitForSeconds(PlayerRules.StaminaRecoverRate);
        }
    }

    /// <summary>
    /// Boosts the player by increasing its movement speed and decreasing it after a few seconds.
    /// At the end the speed will go back to normal.
    /// </summary>
    public IEnumerator boost()
    {
        float oldSpeedMultiplier = PlayerData.SpeedMultiplier;
        float desiredSpeedMultiplier = oldSpeedMultiplier * PlayerRules.SpeedBuff;

        while (desiredSpeedMultiplier > PlayerData.SpeedMultiplier)
            yield return StartCoroutine(IncreaseSpeedMultiplier(FOV_CHANGE_MULTIPLIER));
        PlayerData.SpeedMultiplier = desiredSpeedMultiplier;

        yield return new WaitForSeconds(PlayerRules.SpeedBuffTime);

        desiredSpeedMultiplier = oldSpeedMultiplier + (oldSpeedMultiplier * PlayerRules.SlowBuff) - oldSpeedMultiplier;

        while (desiredSpeedMultiplier < PlayerData.SpeedMultiplier)
            yield return StartCoroutine(IncreaseSpeedMultiplier(-FOV_CHANGE_MULTIPLIER));
        PlayerData.SpeedMultiplier = desiredSpeedMultiplier;

        yield return new WaitForSeconds(PlayerRules.SlowBuffTime);

        while (oldSpeedMultiplier > PlayerData.SpeedMultiplier)
            yield return StartCoroutine(IncreaseSpeedMultiplier(FOV_CHANGE_MULTIPLIER));
        PlayerData.SpeedMultiplier = oldSpeedMultiplier;
    }

    IEnumerator IncreaseSpeedMultiplier(float amount)
    {
        PlayerData.SpeedMultiplier += amount;
        yield return new WaitForSeconds(0.01f);
    }

    /// <summary>
    /// Check if player is moving
    /// </summary>
    public bool IsMoving()
    {
        if (input != Vector3.zero)
            return true;
        else return false;
    }
}