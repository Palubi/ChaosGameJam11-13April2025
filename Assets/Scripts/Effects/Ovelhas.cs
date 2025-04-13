using UnityEngine;

public class Ovelhas : MonoBehaviour, IActivable
{
    [SerializeField] private GameObject ovelhasMainLeft;
    [SerializeField] private GameObject ovelhasMainRight;

   // [SerializeField] private 

    private void Walk()
    {





    }

    private void FixedUpdate()
    {
        Walk();
    }

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
