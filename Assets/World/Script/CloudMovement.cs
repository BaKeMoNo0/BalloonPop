using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float slowDuration = 5f;
    public float slowFactor = 0.2f;
    public bool isElectrical = false;
    public int direction = 1;
    
    void Update()
    {
        transform.Translate(Vector3.right * (direction * moveSpeed * Time.deltaTime));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cloud") || other.CompareTag("Building") || other.CompareTag("Pipe") || other.CompareTag("lb_bird")) direction *= -1;
        
        if (other.CompareTag("Player")) {
            PlayerController player = other.GetComponent<PlayerController>();
            if (!isElectrical && player != null) {
                StartCoroutine(TemporarilySlowPlayer(player));
            } else {
              player.Explode();  
            }
        }
    }
    
    private System.Collections.IEnumerator TemporarilySlowPlayer(PlayerController player)
    {
        float originalSideForce = player.GetSideForce();
        float originalLiftForce = player.GetLiftForce();

        player.SetSideForce(originalSideForce * slowFactor);
        player.SetLiftForce(originalLiftForce * slowFactor);

        yield return new WaitForSeconds(slowDuration);

        player.SetSideForce(originalSideForce);
        player.SetLiftForce(originalLiftForce);
    }
}
