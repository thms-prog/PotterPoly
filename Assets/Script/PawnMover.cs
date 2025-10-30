using System.Collections;
using UnityEngine;

public class PawnMover : MonoBehaviour
{
    [Header("Assign all tiles in order")]
    public Transform[] tiles; // Tile_0 to Tile_39 (ou plus)

    [Header("Movement settings")]
    public float moveSpeed = 5f;
    public float delayBetweenSteps = 0.2f;
    public float hoverHeight = 0.5f;

    private int currentIndex = 0;
    private bool isMoving = false;

    /// <summary>
    /// Déplace le pion d’un nombre de cases donné
    /// </summary>
    public void MoveBy(int steps)
    {
        if (tiles == null || tiles.Length == 0)
        {
            Debug.LogWarning("❌ Aucun tile assigné dans PawnMover !");
            return;
        }

        if (steps <= 0)
        {
            Debug.LogWarning("⚠️ MoveBy ignoré : steps <= 0");
            return;
        }

        if (isMoving)
        {
            Debug.LogWarning("⛔ MoveBy ignoré : pion déjà en déplacement");
            return;
        }

        Debug.Log($"🔁 Déplacement du pion de {steps} case(s) depuis tile_{currentIndex}");
        StartCoroutine(MoveStepByStep(steps));
    }

    private IEnumerator MoveStepByStep(int steps)
    {
        isMoving = true;

        for (int i = 0; i < steps; i++)
        {
            currentIndex = (currentIndex + 1) % tiles.Length;

            if (tiles[currentIndex] == null)
            {
                Debug.LogError($"❌ tile_{currentIndex} est null !");
                yield break;
            }

            Vector3 targetPos = tiles[currentIndex].position + Vector3.up * hoverHeight;

            // Déplacement fluide
            while (Vector3.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = targetPos;
            Debug.Log($"➡️ Étape {i + 1}/{steps} → tile_{currentIndex}");
            yield return new WaitForSeconds(delayBetweenSteps);
        }

        Debug.Log($"✅ Pion arrivé sur : {tiles[currentIndex].name}");
        isMoving = false;
    }

    /// <summary>
    /// Replace le pion sur la première case
    /// </summary>
    public void ResetPosition()
    {
        if (tiles == null || tiles.Length == 0)
        {
            Debug.LogWarning("❌ Aucun tile assigné pour ResetPosition !");
            return;
        }

        currentIndex = 0;
        transform.position = tiles[0].position + Vector3.up * hoverHeight;
        Debug.Log("🔄 Pion réinitialisé sur tile_0");
    }

    /// <summary>
    /// Retourne l’index actuel du pion
    /// </summary>
    public int GetCurrentIndex()
    {
        return currentIndex;
    }

    /// <summary>
    /// Indique si le pion est en mouvement
    /// </summary>
    public bool IsMoving()
    {
        return isMoving;
    }
}
