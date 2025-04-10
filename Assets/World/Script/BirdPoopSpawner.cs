using UnityEngine;

public class BirdPoopSpawner : MonoBehaviour
{
    public GameObject poopPrefab;
    public float spawnInterval = 3f;
    private float timer;
    
    private GameObject[] pooledPoops;
    private int currentIndex = 0;

    private void Start()
    {
        pooledPoops = new GameObject[2];
        for (int i = 0; i < pooledPoops.Length; i++)
        {
            pooledPoops[i] = Instantiate(poopPrefab, transform.position, Quaternion.identity);
            pooledPoops[i].SetActive(false);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPoop();
            timer = 0f;
        }
    }
    
    private void SpawnPoop()
    {
        GameObject poop = pooledPoops[currentIndex];
        poop.transform.position = transform.position + Vector3.down * 0.2f;
        poop.SetActive(true);
        
        StartCoroutine(ResetPoop(poop, 4f));

        currentIndex = (currentIndex + 1) % pooledPoops.Length;
    }

    private System.Collections.IEnumerator ResetPoop(GameObject poop, float delay)
    {
        yield return new WaitForSeconds(delay);
        poop.SetActive(false);
    }
}
