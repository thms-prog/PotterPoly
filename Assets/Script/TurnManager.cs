using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour
{
    [Header("Références des dés")]
    public DiceReader dice1;
    public DiceReader dice2;

    [Header("Référence du pion")]
    public PawnMover pawn;

    private bool hasLaunched = false;
    private bool hasMoved = false;

    void Update()
    {
        // 👆 Clic tactile ou souris pour lancer les dés
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            LaunchDice();
        }

        // ✅ Quand les dés sont figés et ont été lancés
        if (hasLaunched && dice1.isReady && dice2.isReady && !hasMoved)
        {
            hasMoved = true;
            StartCoroutine(ShowDiceResult());
        }

        // 🔄 Reset si les dés sont relancés
        if (!dice1.isReady || !dice2.isReady)
        {
            hasMoved = false;
        }
    }

    void LaunchDice()
    {
        hasLaunched = true;

        // Active les dés et les lance
        dice1.gameObject.SetActive(true);
        dice2.gameObject.SetActive(true);

        dice1.rb.isKinematic = false;
        dice2.rb.isKinematic = false;

        dice1.rb.AddForce(Random.onUnitSphere * 5f, ForceMode.Impulse);
        dice2.rb.AddForce(Random.onUnitSphere * 5f, ForceMode.Impulse);
    }

    IEnumerator ShowDiceResult()
    {
        // 📦 Position d’affichage à l’écran
        Vector3 screenPos1 = new Vector3(-2, 2, -5);
        Vector3 screenPos2 = new Vector3(2, 2, -5);

        // 🎥 Animation des dés vers l’écran
        yield return StartCoroutine(dice1.MoveToScreen(screenPos1));
        yield return StartCoroutine(dice2.MoveToScreen(screenPos2));

        // 🔢 Lecture du score
        int total = dice1.lastResult + dice2.lastResult;
        Debug.Log("Résultat : " + total);

        yield return new WaitForSeconds(1f);

        // 🧼 Disparition des dés
        dice1.gameObject.SetActive(false);
        dice2.gameObject.SetActive(false);

        // 🧍‍♂️ Déplacement du pion
        pawn.MoveBy(total);

        // 🔄 Prêt pour le tour suivant
        hasLaunched = false;
    }
}
