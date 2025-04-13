using UnityEngine;

public class SlowEffect : MonoBehaviour, IActivable
{
    [SerializeField] private GameObject slowLeft1;
    [SerializeField] private GameObject slowRigh2;

    public void Ativate(int player)
    {
        if (player == 1)
        {
            Spawn(slowLeft1);
        }
        else
        {
            Spawn(slowRigh2);
        }
    }
    private void Spawn(GameObject slow)
    {
        slow.SetActive(true);
    }

}
