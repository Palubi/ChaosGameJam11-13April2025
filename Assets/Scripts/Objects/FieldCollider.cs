using UnityEngine;


public class FieldCollider : MonoBehaviour, ICollider
{
    [SerializeField] private GamePlayManager myGameManager;
    private Collider myCollider;


    private void Awake()
    {
        myCollider = gameObject.GetComponent<Collider>();
    }
    public void Colliding(int player)
    {
        myGameManager.TouchingGround(player, myCollider);
    }

  

}
