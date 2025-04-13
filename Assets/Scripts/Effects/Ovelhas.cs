using UnityEngine;

public class Ovelhas : MonoBehaviour, IActivable
{
    [SerializeField] private GameObject ovelhasMainLeft;
    [SerializeField] private GameObject ovelhasMainRight;

    private void Awake()
    {
      //set active false - mas deixamos predefinido false 
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
