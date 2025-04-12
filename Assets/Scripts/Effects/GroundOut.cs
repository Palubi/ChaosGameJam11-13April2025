using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundOut : MonoBehaviour
{
    //tremer e dps cair - aniamtor

    //guaradr posicao inciatl
    //NamespaceOption é afetado pela grvaidade


    //guardar a localizacao do z do transform

    [SerializeField] private GameObject[] leftGround;
    [SerializeField] private GameObject[] rightGround;

    private List<GameObject> movedGround;

    private float initialZPosition = 0;
    private bool groundIsActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        initialZPosition = leftGround[0].transform.position.z;
    }

    public void VerifyGround()
    {
        if (!groundIsActive)
        {
            groundIsActive = true;
        }
        else
        {
            groundIsActive = false;
            ReturnGrid();
        }
    }

    public void ActivateGrid(int player)
    {

        VerifyGround();

        if (player == 1)
        {
            DisableGround(leftGround);
        }
        else
        {
            DisableGround(rightGround);
        }
    }
    

    private void DisableGround(GameObject[] ground) 
    {
        

        if(movedGround.Count == 0)
        {
            SelectGrid(ground);
        }
        else
        {
            // if nao tivevr limpa ent ai da delay senao da logo e limpa logo antes
        }
    }
    private void SelectGrid(GameObject[] ground)
    {
        //random select do array enviado
        int arrayLength = ground.Length / 2;
        int cicle = Random.Range(0, arrayLength);

        for (int i = 0; i < arrayLength + 1; i++)
        {
            ground[i].GetComponent<Rigidbody>().useGravity = true;

            movedGround.Add(ground[i]);
        }
    }

    public void ReturnGrid()
    {
        for (int i = 0; i <= movedGround.Count + 1; i++)
        {
            //mover p posicao original8

            movedGround[i].transform.position = new Vector3(movedGround[i].transform.position.x, movedGround[i].transform.position.y, initialZPosition);

            movedGround[i].GetComponent<Rigidbody>().useGravity = false;

            movedGround.Remove(movedGround[i]);
        }
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
