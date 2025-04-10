using UnityEngine;

public class PipeZone : MonoBehaviour
{

    private const float MaxBalloonScale = 2.4f;
    private const float EntryPosX = 2.7f;
    
    public enum PipeType
    {
        Enter,
        Exit
    }

    public PipeType pipeType;
    public float boostSpeed = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null)
        {
            player = other.GetComponentInParent<PlayerController>();
        }

        if (player == null) return;

        if (pipeType == PipeType.Enter)
        {
            float playerPosX = player.transform.position.x;
            float playerScaleX = player.transform.localScale.x;
            if (playerScaleX <= MaxBalloonScale && playerPosX >= EntryPosX-0.3 && playerPosX <= EntryPosX+0.3) {
                player.transform.position = new Vector3(EntryPosX, player.transform.position.y, player.transform.position.z);
                player.transform.localScale = new Vector3(MaxBalloonScale, MaxBalloonScale, MaxBalloonScale);
                player.SetIsInPipe(true);
            } else {
                StartCoroutine(CanEnterPipe(1f, player));
            }
        }
        else if (pipeType == PipeType.Exit)
        {
            player.SetIsInPipe(false);
            player.ApplyBoostUp(boostSpeed);
        }
    }
    
    private System.Collections.IEnumerator CanEnterPipe(float delay, PlayerController player)
    {
        yield return new WaitForSeconds(delay);
        player.ApplyBoostDown(boostSpeed);
    }
}
