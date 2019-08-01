using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    public PlayerData PlayerData { get => playerData; set => playerData = value; }

    private float rotAmountX;
    private float rotAmountY;
    private Camera playerCamera;

  
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SetCamera();
        PlayerData.Setup();
    }
    public void SetCamera()
    {
        if (playerCamera == null)
        {
            playerCamera = transform.GetChild(0).GetComponent<Camera>();
        }
    }

    public void RotateCamera(Vector2 rotation)
    {
        rotAmountX = rotation.y;
        rotAmountY = rotation.x;
    }

    public void RotateCamera()
    {
        if (playerCamera.enabled)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            float targetRotX = rotAmountX += mouseX * PlayerData.PlayerRules.MouseSensitivity;
            float targetRotY = rotAmountY -= mouseY * PlayerData.PlayerRules.MouseSensitivity;
            if (targetRotY > 90)
            {
                targetRotY = 90;
                rotAmountY = 90;
            }
            else if (targetRotY < -90)
            {
                targetRotY = -90;
                rotAmountY = -90;
            }
            transform.eulerAngles = new Vector3(0, targetRotX, 0);
            if (playerCamera != null)
            {
                playerCamera.transform.eulerAngles = new Vector3(targetRotY, targetRotX, 0);
            }
        }
    }
}