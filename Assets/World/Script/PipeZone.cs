using System;
using System.Collections;
using UnityEngine;

public class PipeZone : MonoBehaviour
{

    private const float MaxBalloonScale = 2f;
    private float EntryPosX;
    private bool hasTriggered = false;
    
    public enum PipeType { Enter, Exit }

    public PipeType pipeType;
    public float boostSpeed = 100f;

    private void Start() {
        EntryPosX = transform.position.x + 0.2f;
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;
        
        hasTriggered = true;

        if (pipeType == PipeType.Enter) {
            player.SetIsInPipe(true);

            if (IsPlayerCorrectlyAligned(player)) {
                AlignPlayerToPipe(player);
            } else {
                StartCoroutine(CanEnterPipe(2f, player));
            }
        } else if (pipeType == PipeType.Exit) {
            player.SetIsInPipe(false);
            player.ApplyBoostUp(boostSpeed);
        }
    }
    
    
    private IEnumerator CanEnterPipe(float delay, PlayerController player)
    {
        yield return new WaitForSeconds(delay);

        if (!IsPlayerCorrectlyAligned(player)) {
            player.SetIsInPipe(false);
            player.ApplyBoostDown(boostSpeed);
        } else {
            AlignPlayerToPipe(player);
        }
        hasTriggered = false;
    }

    private bool IsPlayerCorrectlyAligned(PlayerController player) {
        float playerPosX = player.transform.position.x;
        float playerScaleX = player.transform.localScale.x;

        return playerScaleX >= MaxBalloonScale - 0.8f &&
               playerScaleX <= MaxBalloonScale + 0.8f &&
               playerPosX >= EntryPosX - 0.4f &&
               playerPosX <= EntryPosX + 0.4f;
    }

    private void AlignPlayerToPipe(PlayerController player) {
        player.transform.position = new Vector3(EntryPosX, player.transform.position.y, player.transform.position.z);
        player.transform.localScale = new Vector3(MaxBalloonScale, MaxBalloonScale, MaxBalloonScale);
    }
}
