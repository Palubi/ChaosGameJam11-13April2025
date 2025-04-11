using UnityEngine;

public class UIManager: MonoBehaviour
{
    private int scorePlayer1 = 0;
    private int scorePlayer2 = 0;

    


    //to be called by points manager
    public void PointA()
    {
        scorePlayer1++;

        // alterar os simbolos na ui
    }

    public void PointB()
    {
        scorePlayer2++;

        // alterar os simbolos na ui
    }
}
