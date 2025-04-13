using UnityEngine;

public class Cabelazzz : MonoBehaviour, IActivable
{
    [SerializeField] private GameObject cabeloLeft1;
    [SerializeField] private GameObject cabeloRight2;

    public void Ativate(int player)
    {
        if (player == 1)
        {
            Spawn(cabeloLeft1);
        }
        else
        {
            Spawn(cabeloRight2);
        }
    }
    private void Spawn(GameObject cabelo)
    {
        cabelo.SetActive(true);
    }
}
