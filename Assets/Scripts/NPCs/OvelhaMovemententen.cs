using UnityEngine;

public class OvelhaMovement : MonoBehaviour, ISlowable
{
    [SerializeField] private float initialSpeed;
    private float speed;
    
    public void NotSlow()
    {
        speed = initialSpeed; 
    }

    public void Slow(float amount)
    {
        speed = initialSpeed * amount;
    }
}
