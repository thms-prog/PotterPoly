using UnityEngine;

public class DiceDoubleTest : MonoBehaviour
{
    public DiceReader dice1;
    public DiceReader dice2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int r1 = dice1.GetTopFace();
            int r2 = dice2.GetTopFace();
            int total = r1 + r2;

            Debug.Log("Dé 1 : " + r1);
            Debug.Log("Dé 2 : " + r2);
            Debug.Log("Total : " + total);
        }
    }
}
