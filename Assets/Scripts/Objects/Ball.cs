using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    /// <summary>       VER AQUI
    /// /public int Player { set; get actualPlayer }; //isso nao deve tar bem  escrito xd - mas é pro colider pegar este valor
    /// </summary>

    private int actualPlayer = 0;

    //quando o player lança a bola envia aqui o seu numero

    // public lançar bola (int playernumber)
    // { actual player = player numebr }
    // 


    private void OnTriggerEnter(Collider other)     //para mandar o tipo de collider
    {   
        if (other.gameObject.TryGetComponent<ICollider>(out ICollider collider))
        {
            collider.Colliding();
        }
    }
    private void FieldCollision()
    {
        //if()
    }
}
