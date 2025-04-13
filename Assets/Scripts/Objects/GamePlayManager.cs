using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    private int scorePoints = 0;
    private int ActualPoints = 0;

    private bool hasTouchedGround = false;

    [SerializeField] private Collider Collider; // n ha
    [SerializeField] private Collider leftInCollider;
    [SerializeField] private Collider rightInCollider;
    [SerializeField] private Collider leftOutCollider;
    [SerializeField] private Collider rightOutCollider;

  

    private int[] players = new int[2] { 1, 2 };
    private int actualPlayer = 0;

    [SerializeField] private List<GameObject> effectsPlayer1;
    [SerializeField] private List<GameObject> effectsPlayer2;

    private int effectzSie;

    [SerializeField] private GameObject Ball;

    private void Aleatorio()
    {
        int number = Random.Range(0, 2);
        SpawnBall1(number);
          
    }

    private void SpawnBall1(int player)
    {

    }


    private void Debuff(List<GameObject> objectList, int chosenPlayer)    //Touching grouund chama este metodo
    {
        int index = Random.Range(0, effectzSie);

        objectList[index].TryGetComponent<IActivable>(out IActivable effect);
        effect.Ativate(chosenPlayer);
        objectList.RemoveAt(index);
    }
    
    public void TouchingGround( int player, Collider touchedCollider)
    {

        if (touchedCollider == leftInCollider)
        {

            if (player == 1)
            {
                // campo proprio perde
            }
            else
            {
                if (player == actualPlayer) // segunda vez a tocar no chao com o playr 2
                {
                    // ponto pro 2
                }
                
            }
            touchedCollider = leftInCollider;
        }
        else if (touchedCollider == rightInCollider)
        {
            if (player == 2)
            {
                // campo proprio perde
            }
            else
            {
                if (player == actualPlayer) // segunda vez a tocar no chao com o playr 2
                {
                    // ponto pro 1
                }
            }
            touchedCollider = rightInCollider;
        }
        else if (touchedCollider == leftOutCollider)       // quando lançam pra fora ver se troca o nome do colider
        {
            if (player == 1)
            {
                // ponto pro player 2
            }
            else
            {
                //  ponto pro player 1
            }
            touchedCollider = leftOutCollider;
        }
        else
        {
            touchedCollider = rightOutCollider;
        }

        if (actualPlayer != players[player]) // signific que ja tocou uma vez no chao
        {
            //other ganha um ponto
        }

        actualPlayer = player;

        // se plyer for igua ao campo do proprio player





     
        
        
        
        
        
        
        ///
        /// 
        /// if (isRunning)  //ja tinha tocado 1 vez
        /// {
       /// isRunning = false;
    }
        ///else          // primeiro toque no chão
       /// {isRunning = true; actualPlayer = players[player]; }
        
    

    private void Awake()
    {

    }

    private void Start()
    {

    }

    private void Update()
    {

    }



}
