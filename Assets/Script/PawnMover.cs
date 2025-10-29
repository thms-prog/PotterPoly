using System.Collections;
using UnityEngine;

public class PawnMover : MonoBehaviour
{
    [Header("Assign all tiles in order")]
    public Transform[] tiles; // Tile_1 to Tile_40 (or plus)

    [Header("Movement settings")]
    public float moveSpeed = 5f;           // Speed of movement
    public float delayBetweenSteps = 0.2f; // Pause between each tile
    public float hoverHeight = 0.5f;       // Height above the tile

    private int currentIndex = 0;
    private bool isMoving = false;

    /// <summary>
    /// Déplace le pion d’un nombre de cases donné
    /// </summary>
    public void MoveBy(int steps)
    {
        if (!isMoving && tiles.Length > 0)
        {
            StartCoroutine(MoveStepByStep(steps));
        }
    }

    private IEnumerator MoveStepByStep(int steps)
    {
        isMoving = true;

        for (int i = 0; i < steps; i++)
        {
            currentIndex = (currentIndex + 1) % tiles.Length;
            Vector3 targetPos = tiles[currentIndex].position + Vector3.up * hoverHeight;

            // Déplacement fluide vers la case
            while (Vector3.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = targetPos;
            yield return new WaitForSeconds(delayBetweenSteps);
        }

        Debug.Log("Pion arrivé sur : " + tiles[currentIndex].name);
        isMoving = false;
    }
}
