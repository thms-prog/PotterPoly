using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    public float rollForce = 500f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // désactivé au départ
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // clic souris ou tap tactile
        {
            rb.useGravity = true; // activé au clic
            RollDice();
        }
    }

    void RollDice()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Vector3 randomDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        rb.AddTorque(randomDirection * rollForce, ForceMode.Impulse);
        
    }
}
