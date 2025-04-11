using UnityEngine;

public class CloudMovement : MonoBehaviour {
    public float moveSpeed = 1f;
    public bool isElectrical = false;
    public int direction = 1;
    
    void Update() {
        transform.Translate(Vector3.right * (direction * moveSpeed * Time.deltaTime));
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Cloud") || other.CompareTag("Building") || other.CompareTag("Pipe") || other.CompareTag("lb_bird")) direction *= -1;
        
        if (other.CompareTag("Player")) {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player == null) return;
            
            if (isElectrical) {
                player.Explode();  
            }
        }
    }
}
