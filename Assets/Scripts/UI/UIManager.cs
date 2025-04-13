using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UIManager: MonoBehaviour
{
    private int scorePlayer1 = 0;
    private int scorePlayer2 = 0;
   private int victoryScore=4;

    [SerializeField] private List<GameObject> redPoints;    //1
    [SerializeField] private List<GameObject> bluePoints;   //2
    [SerializeField] private GameObject winPanel;           //game object UI

    private void UIUpdate(List<GameObject> list, int score)
    {
        int value = score - 1;
        GameObject circle = list[value];
        circle.SetActive(true);
        VerifyScore();
    }

    private void VerifyScore()
    {
        if(scorePlayer1 == victoryScore || scorePlayer2 == victoryScore)
        {
            winPanel.SetActive(true);
            //pause gamme
        }
    }

    //to be called by points manager
    public void PointRed()      //1
    {
        scorePlayer1++;
        UIUpdate(redPoints, scorePlayer1);

        // alterar os simbolos na ui
    }

    public void PointBlue()
    {
        scorePlayer2++;
        UIUpdate(bluePoints, scorePlayer2);
        // alterar os simbolos na ui
    }
}
