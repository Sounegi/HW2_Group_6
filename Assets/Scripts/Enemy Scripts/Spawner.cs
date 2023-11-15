using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Terrain map;
    public GameObject floor, enemy_template, _plane;
    public int enemy_count;
    float map_x_size, map_z_size;
    public List<GameObject> enemy = new List<GameObject>();

    void Awake()
    {
        for (int i = 0; i < enemy_count; i++)
        {
            if (map != null)
                GenerateRandomEnemies();
            else
                GenerateRandomAtPlane();
        }
    }

    void GenerateRandomEnemies()
    {
        bool created = false;
        Vector3 randomSpawnPosition = Vector3.zero;
        RaycastHit hit = new RaycastHit();
        Vector3 terrainSize = map.terrainData.size;
        float max_x = terrainSize.x;
        float max_z = terrainSize.z;
        while (!created)
        {
            randomSpawnPosition = new Vector3(Random.Range(5f, terrainSize.x - 5f), 7f, Random.Range(5f, terrainSize.z - 5f));
            if (Physics.Raycast(randomSpawnPosition, -Vector3.up, out hit))
            {
                float offsetDistance = hit.distance;
                Debug.DrawLine(randomSpawnPosition, hit.point, Color.cyan);
                //Debug.Log("Raycast hit at " + hit.point.ToString());
                //Debug.Log("Generated Object at: " + randomSpawnPosition.ToString());
                created = true;
            }
        }
        randomSpawnPosition = new Vector3(randomSpawnPosition.x, hit.point.y, randomSpawnPosition.z);
        GameObject musuh = Instantiate(enemy_template, randomSpawnPosition, Quaternion.identity);
        enemy.Add(musuh);
    }

    void GenerateRandomAtPlane()
    {
        bool created = false;
        Vector3 randomSpawnPosition = Vector3.zero;

        Vector3 center = floor.transform.position + Vector3.up;
        Mesh planeMesh = _plane.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;
        float boundsX = _plane.transform.localScale.x * bounds.size.x;
        float boundsY = _plane.transform.localScale.y * bounds.size.y;
        float boundsZ = _plane.transform.localScale.z * bounds.size.z;
        float range = 10f;

        NavMeshHit hit = new NavMeshHit();
        while (!created)
        {
            float x_pos = Random.Range(-boundsX / 2, boundsX / 2);
            float z_pos = Random.Range(-boundsZ / 2, boundsZ / 2);
            randomSpawnPosition = new Vector3(x_pos, floor.transform.position.y + 10f, z_pos);
            randomSpawnPosition = randomSpawnPosition + Random.insideUnitSphere * range;
            Debug.Log(randomSpawnPosition);
            if (NavMesh.SamplePosition(randomSpawnPosition, out hit, 1.0f, NavMesh.AllAreas))
            {
                Debug.Log("Success");
                float offsetDistance = hit.distance;
                Debug.Log("Generated Object at: " + randomSpawnPosition.ToString());
                created = true;
            }
        }
        randomSpawnPosition = new Vector3(randomSpawnPosition.x, hit.position.y + 1f, randomSpawnPosition.z);
        GameObject musuh = Instantiate(enemy_template, randomSpawnPosition, Quaternion.identity);
        //GameObject test = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), randomSpawnPosition, Quaternion.identity);
        //test.name = "ngengto";
        //enemy_template.transform.localScale *= 0.3f;
        //enemy_template.transform.localScale *= 0.3f;
        enemy.Add(musuh);
    }

    // Update is called once per frame

    void Update()
    {
        Destroy(this);
    }

}