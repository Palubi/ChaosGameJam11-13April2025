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


    private List<GameObject> movedGround;

    private float initialZPosition = 0;
    private bool groundIsActive = false;


    public void SelectGrid(List<GameObject> ground)
    {
        int cubes = 9;

        movedGround.Clear();

        for (int i = 0; i <= cubes; i++)
        {
            int j = Random.Range(0, ground.Count + 1);
            movedGround.Add(ground[j]);
            ground.RemoveAt(j);
            ground[i].GetComponent<Rigidbody>().useGravity = true;
        }

        //random select do array enviado

        // suffle - escolher os primeiros 9 e adicionr a lista

    }
}
