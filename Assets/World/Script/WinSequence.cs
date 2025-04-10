using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSequence : MonoBehaviour
{
    public GameObject effect;
    public float offsetX = 1.5f;
    public float offsetY = 0.5f;
    public float waitTime = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (!player) return;

            SpawnEffects();
            StartCoroutine(NextLevel(waitTime));
        }
    }

    private void SpawnEffects()
    {
        Vector3 center = transform.position;

        // Right
        Instantiate(effect, center + new Vector3(offsetX, offsetY, 0), Quaternion.identity);
        Instantiate(effect, center + new Vector3(offsetX * 2, offsetY, 0), Quaternion.identity);

        // Left
        Instantiate(effect, center + new Vector3(-offsetX, offsetY, 0), Quaternion.identity);
        Instantiate(effect, center + new Vector3(-offsetX * 2, offsetY, 0), Quaternion.identity);
    }

    private System.Collections.IEnumerator NextLevel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(1);
    }
}