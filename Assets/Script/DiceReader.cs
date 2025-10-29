using UnityEngine;
using System.Collections;

public class DiceReader : MonoBehaviour
{
    public Transform[] facePoints; // Assign Face_1 to Face_6 in Inspector
    public Rigidbody rb;
    public bool isReady = false;
    public int lastResult = -1;

    public float velocityThreshold = 0.05f;
    public float checkDelay = 0.5f;

    private float timer = 0f;

    

    void Update()
    {
        if (rb.linearVelocity.magnitude < velocityThreshold)
        {
            timer += Time.deltaTime;
            if (timer >= checkDelay && !isReady)
            {
                lastResult = GetTopFace();
                isReady = true;
            }
        }
        else
        {
            timer = 0f;
            isReady = false;
        }
    }
    public int GetTopFace()
    {
        float maxDot = -1f;
        int topIndex = -1;

        for (int i = 0; i < facePoints.Length; i++)
        {
            float dot = Vector3.Dot(facePoints[i].up, Vector3.up);
            if (dot > maxDot)
            {
                maxDot = dot;
                topIndex = i;
            }
        }

        int result = topIndex + 1; // Face_1 = index 0 → chiffre 1
        Debug.Log("Résultat du dé " + gameObject.name + " : " + result);
        return result;
    }
    public IEnumerator MoveToScreen(Vector3 screenPos)
{
    float duration = 0.5f;
    Vector3 start = transform.position;
    float t = 0f;

    while (t < duration)
    {
        transform.position = Vector3.Lerp(start, screenPos, t / duration);
        t += Time.deltaTime;
        yield return null;
    }

    transform.position = screenPos;
}

}
