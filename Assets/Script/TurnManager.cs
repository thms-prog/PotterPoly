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
    private bool isProcessing = false;

    void Update()
    {
        // 🎲 Déclenchement du lancer
        if (!hasLaunched && (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)))
        {
            LaunchDice();
        }
        


        // 📦 Attente que les dés soient figés
        if (hasLaunched && !hasMoved && !isProcessing &&
            dice1.rb.linearVelocity.magnitude < 0.05f &&
            dice2.rb.linearVelocity.magnitude < 0.05f)
        {
            hasMoved = true;
            isProcessing = true;
            StartCoroutine(ShowDiceResult());
        }
    }

    void LaunchDice()
{
    hasLaunched = true;
    hasMoved = false;
    isProcessing = false;

    // ✅ Activation des dés
    dice1.gameObject.SetActive(true);
    dice2.gameObject.SetActive(true);

    // ✅ Position de départ
    dice1.transform.position = new Vector3(0, 2, 0);
    dice2.transform.position = new Vector3(1, 2, 0);

    // ✅ Activation de la gravité
    dice1.rb.useGravity = true;
    dice2.rb.useGravity = true;

    // ✅ Lancement physique
    dice1.rb.isKinematic = false;
    dice2.rb.isKinematic = false;

    dice1.rb.linearVelocity = Vector3.zero;
    dice2.rb.linearVelocity = Vector3.zero;

    dice1.rb.AddForce(Random.onUnitSphere * 6f, ForceMode.Impulse);
    dice2.rb.AddForce(Random.onUnitSphere * 6f, ForceMode.Impulse);

    dice1.rb.AddTorque(Random.onUnitSphere * 6f, ForceMode.Impulse);
    dice2.rb.AddTorque(Random.onUnitSphere * 6f, ForceMode.Impulse);

    dice1.hasBeenLaunched = true;
    dice2.hasBeenLaunched = true;

    dice1.isReady = false;
    dice2.isReady = false;

    Debug.Log("🎲 Dés lancés !");
}

    IEnumerator ShowDiceResult()
    {
        // ⏳ Attente que les dés soient prêts
        yield return new WaitUntil(() => dice1.isReady && dice2.isReady);

        // ✅ Verrouille le résultat
        dice1.LockResult();
        dice2.LockResult();

        // ✅ Stop physique
        dice1.rb.useGravity = false;
        dice2.rb.useGravity = false;
        dice1.rb.linearVelocity = Vector3.zero;
        dice2.rb.linearVelocity = Vector3.zero;
        dice1.rb.angularVelocity = Vector3.zero;
        dice2.rb.angularVelocity = Vector3.zero;

        // ✅ Remontée en hauteur sans changer de rotation
        Vector3 resultPos1 = new Vector3(0.5f, 8f, 0f);
        Vector3 resultPos2 = new Vector3(-0.5f, 8f, 0f);
        dice1.transform.position = resultPos1;
        dice2.transform.position = resultPos2;

        // 📊 Lecture du résultat
        int total = dice1.lastResult + dice2.lastResult;
        Debug.Log($"📊 D1: {dice1.lastResult}, D2: {dice2.lastResult}, Total: {total}");

        // 🕒 Attente pour lecture visuelle
        yield return new WaitForSeconds(3f);

        // 🧼 Désactivation des dés
        dice1.gameObject.SetActive(false);
        dice2.gameObject.SetActive(false);

        // 🧍‍♂️ Déplacement du pion
        pawn.MoveBy(total);

        // 🔄 Réinitialisation
        hasLaunched = false;
        hasMoved = false;
        isProcessing = false;

        dice1.hasBeenLaunched = false;
        dice2.hasBeenLaunched = false;
        dice1.isReady = false;
        dice2.isReady = false;
        
    }
}
