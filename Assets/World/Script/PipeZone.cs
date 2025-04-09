using UnityEngine;

public class PipeZone : MonoBehaviour
{
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
            player.SetIsInPipe(true);
        }
        else if (pipeType == PipeType.Exit)
        {
            player.SetIsInPipe(false);
            player.ApplyBoost(boostSpeed);
        }
    }
}
