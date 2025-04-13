using UnityEngine;

public class TasGrandao : MonoBehaviour, IActivable
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    public void Ativate(int player)
    {/*
        if (player == 1)
        {
            Spawn(player1);
        }
        else
        {
            Spawn(player2);
        }*/
    }
    private void Spawn( GameObject player)
    {
       //efeito
    }
}
