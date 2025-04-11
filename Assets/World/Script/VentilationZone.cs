using UnityEngine;

public class VentZone : MonoBehaviour {
    public bool isRightSide = false;
    private float windForce = 20f;
    
    private void OnTriggerStay(Collider other) {
        if (!other.CompareTag("Player")) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;
        
        Vector3 pushDirection = isRightSide ? Vector3.left : Vector3.right;
        player.ApplyExternalForce(pushDirection, windForce * Time.deltaTime); 
    }
}
