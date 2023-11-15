using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner_ver2 : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] GameObject floor;
    [SerializeField] GameObject plane;

    public List<GameObject> enemyTypes;

    public int enemyCount;

    void Awake()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GenerateRandomAtPlane();
        }
    }

    void GenerateRandomAtPlane()
    {
        bool created = false;

        Vector3 randomSpawnPosition = Vector3.zero;

        Vector3 center = floor.transform.position + Vector3.up;

        Mesh planeMesh = plane.GetComponent<MeshFilter>().mesh;

        Bounds bounds = planeMesh.bounds;

        float boundsX = plane.transform.localScale.x * bounds.size.x;
        float boundsY = plane.transform.localScale.y * bounds.size.y;
        float boundsZ = plane.transform.localScale.z * bounds.size.z;

        float range = 10f;

        NavMeshHit hit = new NavMeshHit();

        while (!created)
        {
            float x_pos = Random.Range(-boundsX / 2, boundsX / 2);
            float z_pos = Random.Range(-boundsZ / 2, boundsZ / 2);

            randomSpawnPosition = new Vector3(x_pos, floor.transform.position.y + 10f, z_pos);
            randomSpawnPosition = randomSpawnPosition + Random.insideUnitSphere * range;

            // Debug.Log(randomSpawnPosition);

            if (NavMesh.SamplePosition(randomSpawnPosition, out hit, 1.0f, NavMesh.AllAreas))
            {
                // Debug.Log("Success");
                float offsetDistance = hit.distance;
                // Debug.Log("Generated Object at: " + randomSpawnPosition.ToString());
                created = true;
            }
        }
        randomSpawnPosition = new Vector3(randomSpawnPosition.x, hit.position.y + 1f, randomSpawnPosition.z);

        int selectedIndex = Random.Range(1, enemyTypes.Count);
        Instantiate(enemyTypes[selectedIndex], randomSpawnPosition, Quaternion.identity);
    }

}