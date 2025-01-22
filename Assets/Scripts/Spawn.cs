using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Camera cam;
    
    public Transform playerTransform;

    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    GameObject applePrefab;
    [SerializeField]
    GameObject dragonPrefab;
    [SerializeField]
    GameObject enemyContainer;

    [SerializeField]
    float spawnTime = 2.0f;
    [SerializeField]
    float spawnAppleTime = 2.0f;
    [SerializeField]
    float spawnDragonTime = 2.0f;
    float cameraHeight;
    float cameraWidth;
    float leftBoundary;
    float rightBoundary;

    bool spawning = true;
    IEnumerator SpawnRoutine()
    {
        while (spawning)
        {
            Vector3 spawnPosition = new Vector2(cam.transform.position.x + 11.5f, -3.46f);
            GameObject new_enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            new_enemy.transform.parent = enemyContainer.transform; 

            EnemyControls enemyScript = new_enemy.GetComponent<EnemyControls>();
            if (enemyScript != null)
            {
                enemyScript.AssignPlayer(playerTransform);
            }

            yield return new WaitForSeconds(spawnTime); 
        }

    }
    IEnumerator AppleSpawnRoutine()
    {
        while (spawning)
        {
            Vector3 spawnPosition = new Vector2(Random.Range(leftBoundary, rightBoundary), -3.573f);
            GameObject new_apple = Instantiate(applePrefab, spawnPosition, Quaternion.identity);
            new_apple.transform.parent = enemyContainer.transform; 

            Consume consumeScript = new_apple.GetComponent<Consume>();
            if (consumeScript != null)
            {
                consumeScript.AssignPlayer(playerTransform);
            }

            yield return new WaitForSeconds(spawnAppleTime); 
        }

    }
    IEnumerator DragonSpawnRoutine()
    {
        while (spawning)
        {
            Vector3 spawnPosition = new Vector2(Random.Range(leftBoundary, rightBoundary), -3.573f);
            GameObject new_dragon = Instantiate(dragonPrefab, spawnPosition, Quaternion.identity);
            new_dragon.transform.parent = enemyContainer.transform; 

            Consume1 consumeScript = new_dragon.GetComponent<Consume1>();
            if (consumeScript != null)
            {
                consumeScript.AssignPlayer(playerTransform);
            }

            yield return new WaitForSeconds(spawnDragonTime); 
        }

    }
    public void StopSpawning()
    {
        foreach (Transform child in enemyContainer.transform) 
        {
            Destroy(child.gameObject);
        }
        spawning = false;
    }

    
    void Start()
    {
        cameraHeight = cam.orthographicSize * 2;
        cameraWidth = cameraHeight * cam.aspect;

        leftBoundary = cam.transform.position.x - cameraWidth / 2;
        rightBoundary = cam.transform.position.x + cameraWidth / 2;

        StartCoroutine(SpawnRoutine()); 
        StartCoroutine(AppleSpawnRoutine());
        StartCoroutine(DragonSpawnRoutine()); 
    }

    
    void Update()
    {
        leftBoundary = cam.transform.position.x - cameraWidth / 2;
        rightBoundary = cam.transform.position.x + cameraWidth / 2;
    }
}
