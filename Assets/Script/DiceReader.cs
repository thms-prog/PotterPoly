using UnityEngine;
using System.Collections;

public class DiceReader : MonoBehaviour
{
    [Header("Face detection")]
    public Transform[] facePoints; // Assign Face_1 to Face_6 in Inspector
    public Rigidbody rb;
    public float velocityThreshold = 0.05f;
    public float checkDelay = 0.5f;
   
    [Header("State tracking")]
    public bool hasBeenLaunched = false;
    public bool isReady = false;
    public int lastResult = -1;

    private float timer = 0f;
    private bool resultLocked = false;

    void Update()
{
    if (!hasBeenLaunched || resultLocked)
        return;

    if (!isReady && rb.linearVelocity.magnitude < velocityThreshold)
    {
        timer += Time.deltaTime;
        if (timer >= checkDelay)
        {
            lastResult = GetTopFace();
            isReady = true;
            resultLocked = true; // ✅ verrouille le résultat
            Debug.Log($"🎲 Résultat du dé {gameObject.name} : {lastResult}");
        }
    }
    else
    {
        timer = 0f;
        isReady = false;
    }
}

    

    /// <summary>
    /// Détecte la face supérieure du dé
    /// </summary>
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

        return topIndex + 1; // Face_1 = index 0 → chiffre 1
    }

    /// <summary>
    /// Déplacement visuel vers une position écran
    /// </summary>
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

    /// <summary>
    /// Vérifie si le dé est figé
    /// </summary>
    public bool IsStopped()
    {
        return rb.linearVelocity.magnitude < velocityThreshold;
    }

    /// <summary>
    /// Réinitialise l’état du dé pour un nouveau tour
    /// </summary>
    public void ResetDice()
    {
        hasBeenLaunched = false;
        isReady = false;
        lastResult = -1;
        timer = 0f;

    }
    public void LockResult()
   {
        resultLocked = true;
   }

}
