using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform playerPosition;
    [SerializeField] GameObject redEnemyPrefab;
    [SerializeField] GameObject blueEnemyPrefab;
    [SerializeField] GameObject yellowEnemyPrefab;
    [SerializeField] GameObject orangeEnemyPrefab;
    [SerializeField] GameObject greenEnemyPrefab;
    public int redSpawnCount;
    public Transform redSpawnPoint;
    public int blueSpawnCount;
    public Transform blueSpawnPoint;
    public int yellowSpawnCount;
    public Transform yellowSpawnPoint;
    public Transform orangeSpawnPoint;
    public Transform greenSpawnPoint;
    public float spawnRange = 20f;
    bool canSpawnBlue = true;

    private void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies(InputType color)
    {
        /*switch (color)
        {
            case InputType.Red:
                Instantiate(redEnemyPrefab, redSpawnPoint.position, Quaternion.identity);
                break;
            case InputType.Blue:
                Instantiate(blueEnemyPrefab, blueSpawnPoint.position, Quaternion.identity);
                canSpawnBlue = !canSpawnBlue;
                break;
            case InputType.Yellow:
                Instantiate(yellowEnemyPrefab, yellowSpawnPoint.position, Quaternion.identity);
                break;
            case InputType.Orange:
                Instantiate(orangeEnemyPrefab, orangeSpawnPoint.position, Quaternion.identity);
                break;
            case InputType.Green:
                Instantiate(greenEnemyPrefab, greenSpawnPoint.position, Quaternion.identity);
                break;

        }
        */
    }

    public Vector2 GetRandomPointInCircle()
    {
        Vector2 spawnPoint = (Random.insideUnitCircle.normalized * spawnRange) + (Vector2)playerPosition.position;
        return spawnPoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(playerPosition.position, spawnRange);
    }
}
