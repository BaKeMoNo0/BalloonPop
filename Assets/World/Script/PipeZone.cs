using UnityEngine;

public class PipeZone : MonoBehaviour
{

    private const float MaxBalloonScale = 2f;
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
        if (player == null) return;

        if (pipeType == PipeType.Enter)
        {
            float playerPosX = player.transform.position.x;
            float playerScaleX = player.transform.localScale.x;
            player.SetIsInPipe(true);
            if (playerScaleX is >= MaxBalloonScale-0.8f and <= MaxBalloonScale+0.8f && playerPosX >= EntryPosX-0.4 && playerPosX <= EntryPosX+0.4) {
                player.transform.position = new Vector3(EntryPosX, player.transform.position.y, player.transform.position.z);
                player.transform.localScale = new Vector3(MaxBalloonScale, MaxBalloonScale, MaxBalloonScale);
            } else {
                StartCoroutine(CanEnterPipe(2f, player));
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
        player.SetIsInPipe(false);
        player.ApplyBoostDown(boostSpeed);
    }
}
