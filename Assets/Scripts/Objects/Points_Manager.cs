using UnityEngine;

public class GamePlayManager
{
    private int scorePoints = 0;
    private int ActualPoints = 0;

    private bool isRunning = false;

    [SerializeField] private Collider floorCollider;
    [SerializeField] private Collider bootomCollider;
    [SerializeField] private Collider frontCollider;
    [SerializeField] private Collider leftCollider;
    [SerializeField] private Collider rightCollider;

    private int[] players = new int[2] { 1, 2 };

    public void TouchingGround()
    {
        if (isRunning)
        {
            isRunning = false;
        }
        else
        {
            isRunning = true;
        }
    }

    private void Awake()
    {

    }

    private void Start()
    {

    }

    private void Update()
    {

    }



}
