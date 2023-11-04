using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> itemPrefabs;
    [SerializeField] List<float> spawnTime;
    [SerializeField] Vector3 lowerCoordinate;
    [SerializeField] Vector3 upperCoordinate;


    private void OnEnable() {
        StartCoroutine(SpawnRandomItem());
    }

    private void OnDisable() {
        StopCoroutine(SpawnRandomItem());
    }

    private IEnumerator SpawnRandomItem() {
        while(true) {
            int itemIndex = Random.Range(0, itemPrefabs.Count - 1);
            Vector3 position = new Vector3(Random.Range(lowerCoordinate.x, upperCoordinate.x),
                                           Random.Range(lowerCoordinate.y, upperCoordinate.y),
                                           Random.Range(lowerCoordinate.z, upperCoordinate.z));
            yield return new WaitForSeconds(spawnTime[itemIndex]);
            Instantiate(itemPrefabs[itemIndex], position, Quaternion.identity);
        }
    }
}
