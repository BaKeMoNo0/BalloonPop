using UnityEngine;

public class SpeedModifier : MonoBehaviour
{
    public enum ModifierType { SpeedUp, SlowDown }
    public ModifierType modifierType = ModifierType.SpeedUp;
    public float multiplier = 1.5f;
    public float duration = 3f;
    public bool isDestroyable = false;
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null) {
                if (modifierType == ModifierType.SpeedUp)
                    player.ApplySpeedUp(multiplier, duration);
                else
                    player.ApplySlowDown(multiplier, duration);
                
                if(isDestroyable) Destroy(gameObject);
            }
        }
    }
}
