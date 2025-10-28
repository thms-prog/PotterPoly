using UnityEngine;

public class DiceReader : MonoBehaviour
{
    public Transform[] facePoints; // Assign Face_1 to Face_6 in Inspector
    public TextMesh debugText; // ← à assigner dans l’Inspector

    public int GetTopFace()
    {
        float maxDot = -1f;
        int topFace = -1;

        for (int i = 0; i < facePoints.Length; i++)
        {
            float dot = Vector3.Dot(facePoints[i].up, Vector3.up);
            if (dot > maxDot)
            {
                maxDot = dot;
                topFace = i + 1; // Face_1 = index 0 → result = 1
            }
        }

        Debug.Log("Face visible sur " + gameObject.name + " : " + topFace);
        return topFace;
        

        
    }
}
