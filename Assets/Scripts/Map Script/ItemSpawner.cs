using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Ranges")]
    [SerializeField] Vector3 lowerCoordinate;
    [SerializeField] Vector3 upperCoordinate;

    public GameObject potion;

    private void OnEnable()
    {
        StartCoroutine(SpawnRandomItem());
    }

    private void OnDisable()
    {
        StopCoroutine(SpawnRandomItem());
    }

    private IEnumerator SpawnRandomItem()
    {
        
        while(true)
        {
            yield return new WaitForSeconds(5.0f);

            Vector3 position = new Vector3(Random.Range(lowerCoordinate.x, upperCoordinate.x),
                                           Random.Range(lowerCoordinate.y, upperCoordinate.y),
                                           Random.Range(lowerCoordinate.z, upperCoordinate.z));

            Instantiate(potion, position, Quaternion.identity);
        }
    }

}
