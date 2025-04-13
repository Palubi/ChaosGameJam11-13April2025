using UnityEngine;
using UnityEngine.VFX;

public class FunnyBallz : MonoBehaviour, IActivable
{
    [SerializeField] private GameObject extraBall;

    public void Ativate(int player)
    {
        Spawn();
    }

    private void Spawn()
    {
       //extraBall.SetActive(true);
    }

 
}
