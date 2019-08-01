using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerOnFireController : MonoBehaviour
{
    [Tooltip("Image in UI canvas which shows if player is on fire")]
    [SerializeField] private Image image;
    public Image Image { get => image; set => image = value; }

    [SerializeField] private Animator uiFireAnimator;
    public Animator UIFireAnimator { get => uiFireAnimator; set => uiFireAnimator = value; }

    private bool onFire;

    private void Awake()
    {
        // Modify material back to default so the fire won't appear on UI
        // when we start the scene
        Image.material.SetFloat("_Height", 1.9f);
    }

    /// <summary>
    /// Make fire appear on UI by decreasing the height
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartFire()
    {
        yield return new WaitForSeconds(0.01f);
        if (!onFire)
        {
            if (Image.material.GetFloat("_Height") > 1f)
            {
                Image.material.SetFloat("_Height", Image.material.GetFloat("_Height") - 0.01f);
                StartCoroutine(StartFire());
            }
            else
            {
                onFire = true;
                StopCoroutine(StartFire());
            }

        }
    }

    /// <summary>
    /// Call the coroutine and make fire appear on UI
    /// </summary>
    public void SetOnFire()
    {
        StartCoroutine(StartFire());
    }

    /// <summary>
    /// Change fire's height
    /// </summary>
    /// <returns></returns>
    private IEnumerator IStopFire()
    {
        yield return new WaitForSeconds(0.01f);
        if (onFire)
        {
            if (Image.material.GetFloat("_Height") < 1.8f)
            {
                Image.material.SetFloat("_Height", Image.material.GetFloat("_Height") + 0.01f);
                StartCoroutine(IStopFire());
            }
            else
                onFire = false;
        }
    }

    /// <summary>
    /// Call the coroutine to stop the fire
    /// </summary>
    public void StopFire()
    {
        StartCoroutine(IStopFire());
    }
}