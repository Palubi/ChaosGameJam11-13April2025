using UnityEngine;

public class Dog : MonoBehaviour, IActivable
{
    [SerializeField] private GameObject dogleft;
    [SerializeField] private GameObject dogright;

    public void Ativate(int player)
    {
        if (player == 1)
        {
            Spawn(dogleft);
        }
        else
        {
            Spawn(dogright);
        }
        
    }

    private void Spawn(GameObject dog)
    {
        dog.SetActive(true);
    }
}
