using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    private int scorePoints = 0;
    private int ActualPoints = 0;

    private bool hasTouchedGround = false;

   
    [SerializeField] private Collider leftInCollider;
    [SerializeField] private Collider rightInCollider;
    [SerializeField] private Collider leftOutCollider;
    [SerializeField] private Collider rightOutCollider;

    [SerializeField] private UIManager uiManager;



    [SerializeField] private GameObject[] players;
  private int[] playersArray = new int[2] { 1, 2 };
    private int actualPlayer = 0;

    [SerializeField] private List<GameObject> effectsPlayer1;
    [SerializeField] private List<GameObject> effectsPlayer2;

    private int effectzSie;

    [SerializeField] private GameObject ball;

    private void Awake()
    {
        Debug.Log("2");

        StartCoroutine(Spawning());
        Debug.Log("3");

    }

    private IEnumerator Spawning()
    {
        while (true)
        {
            SpawnBall1();

            Debug.Log("AAAAAAAAAAAAAAAAAAAA");
            int number = Random.Range(1, 3);
            Debug.Log("BBBBBBBBBBBBBBBBBBBBBBBBB");
            if (number == 1)
            {
                Debuff(effectsPlayer1, 1);
            }
            else if(number == 2) { Debuff(effectsPlayer2, 2); }
            else {
                Debug.Log("nao deu !!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }

                yield return new WaitForSeconds(4f);
            Debug.Log("REPETIUUUUUU");
        }
        

       
    }
    private void Aleatorio()
    {
        int number = Random.Range(0, players.Length);
      //  SpawnBall1(number);
        print(number);
        Debug.Log("4");
    }

    private void SpawnBall1()
    {
        Debug.Log("5");
        Vector3 posicao = new Vector3(0, 1, 0);
        GameObject newBall = Instantiate(ball, posicao, Quaternion.identity);
        Rigidbody newBallRigibody =
            newBall.GetComponent<Rigidbody>();
         // newBallRigibody.useGravity = false;
        Debug.Log("6");

    }


    private void Debuff(List<GameObject> objectList, int chosenPlayer)    //Touching grouund chama este metodo
    {
        if (objectList.Count > 0)
        {
            int index = Random.Range(0, effectzSie);

            
            if (objectList[index].TryGetComponent<IActivable>(out IActivable effect))
            {
                effect.Ativate(chosenPlayer);
            }

            objectList.RemoveAt(index);
        }
        
    }/*

    public void TouchingGround(int player, Collider touchedCollider)
    {

        if (touchedCollider == leftInCollider)
        {

            if (player == 1)
            {
                uiManager.PointBlue();
                Debuff(effectsPlayer2, 2);
            }
            else
            {
                if (player == actualPlayer) // segunda vez a tocar no chao com o playr 2
                {
                    uiManager.PointBlue();
                    Debuff(effectsPlayer2, 2);

                }
                touchedCollider = leftInCollider;
            }
        }
        else if (touchedCollider == rightInCollider)
        {
            if (player == 2)
            {
                uiManager.PointRed();
                Debuff(effectsPlayer1, 1);
            }
            else
            {
                if (player == actualPlayer) // segunda vez a tocar no chao com o playr 2
                {
                    uiManager.PointRed();
                    Debuff(effectsPlayer1, 1);
                }
            }
            touchedCollider = rightInCollider;
        }
        else if (touchedCollider == leftOutCollider)       // quando lançam pra fora ver se troca o nome do colider
        {
            if (player == 1)
            {
                uiManager.PointBlue();
                Debuff(effectsPlayer2, 2);
            }
            else
            {
                uiManager.PointRed();
                Debuff(effectsPlayer1, 1);
            }
            touchedCollider = leftOutCollider;
        }
        else
        {
            touchedCollider = rightOutCollider;
        }





            actualPlayer = player; 
    } 
    */



    private void OnDestroy()
    {
        Debug.LogWarning(gameObject.name + " foi destruído!");
        Debug.Break();
    }

}
