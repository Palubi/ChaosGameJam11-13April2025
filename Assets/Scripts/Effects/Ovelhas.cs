using UnityEngine;
using UnityEngine.AI;

public class Ovelhas : MonoBehaviour, IActivable
{
    [SerializeField] private GameObject ovelhasMainLeft;
    [SerializeField] private GameObject ovelhasMainRight;

    public void Ativate(int player)
    {
        if (player == 1)
        {
            Spawn(ovelhasMainLeft);
        }
        else
        {
            Spawn(ovelhasMainRight);
        }

    }
     
    private void Spawn(GameObject ovelhasMain)
    {
        ovelhasMain.SetActive(true);
    }
}
