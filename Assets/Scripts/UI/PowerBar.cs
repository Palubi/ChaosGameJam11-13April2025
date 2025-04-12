using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    //Camera (Look target)
    private Camera mainCamera;

    //Sprite image
    private Image powerBarImage = null;

    private void Awake()
    {
        mainCamera = Camera.main;
        powerBarImage = GetComponent<Image>();
}

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
    }

    public void UpdatePower(float percentage)
    {
        powerBarImage.fillAmount = percentage / 100;
    }
}