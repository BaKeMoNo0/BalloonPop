using UnityEngine;

public class Poop : MonoBehaviour
{
    public float fallSpeed = 5f;

    private void Update() {
        transform.Translate(Vector3.down * (fallSpeed * Time.deltaTime));
    }
    
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;
        
        player.Explode();
    }
}
