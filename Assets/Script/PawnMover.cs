using System.Collections;
using UnityEngine;

public class PawnMover : MonoBehaviour
{
    public Transform[] pathTiles; // Assign tiles in order (Tile_0 to Tile_39)
    public float moveSpeed = 2f;
    private int currentTileIndex = 0;

    public void MovePawn(int stepsToMove)
    {
        StartCoroutine(MoveStepByStep(stepsToMove));
    }

    IEnumerator MoveStepByStep(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            currentTileIndex = (currentTileIndex + 1) % pathTiles.Length;
            Vector3 targetPos = pathTiles[currentTileIndex].position + Vector3.up * 0.5f;

            while (Vector3.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(0.1f); // petite pause entre les cases
        }
    }
}
