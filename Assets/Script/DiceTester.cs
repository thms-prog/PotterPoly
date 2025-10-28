using UnityEngine;

public class DiceTester : MonoBehaviour
{
    public DiceReader diceReader;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int result = diceReader.GetTopFace();
            Debug.Log("Résultat du dé : " + result);
        }
    }
}
