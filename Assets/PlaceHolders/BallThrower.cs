using System.Collections;
using UnityEngine;

public class BallThrower : MonoBehaviour
{
    //Balll strenght
    [SerializeField] private int ballStrenght = 0;
    [SerializeField] private GameObject ballPrefab;
    private WaitForSeconds waitTime;

    private void Start()
    {
        StartCoroutine(InitialDelay());
        waitTime = new WaitForSeconds(3.5f);
    }
    private IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(ThrowBall());
    }

    private IEnumerator ThrowBall()
    {
        int ballsThrown = 0;
        while (ballsThrown <= 50)
        {
            ballsThrown++;
            GameObject newBall = Instantiate(ballPrefab,gameObject.transform);
            Rigidbody ballRigidbody = newBall.GetComponent<Rigidbody>();
            ballRigidbody.AddForce(new Vector3(0, 0.5f, -1) * ballStrenght, ForceMode.Impulse);

            yield return waitTime;
        }
    }
}