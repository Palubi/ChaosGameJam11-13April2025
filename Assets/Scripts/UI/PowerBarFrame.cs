using UnityEngine;

public class PowerBarFrame : MonoBehaviour
{
    //Camera (Look target)
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
    }
}