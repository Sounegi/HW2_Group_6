using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Terrain map;
    public GameObject enemy_template;
    public int enemy_count;
    public float roam_distance;
    float map_x_size, map_z_size;
    Dictionary<GameObject, EnemyInfo> enemy = new Dictionary<GameObject, EnemyInfo>();

    public class EnemyInfo
    {
        public NavMeshAgent agent;
        public bool isPatrolling;
        public bool playerFound;
        public Vector3 destination_target;
        float roam_distance, map_x_size, map_z_size;
        public GameObject temp;

        public EnemyInfo(NavMeshAgent a, Vector3 dest, float roam, GameObject template, float x, float z)
        {
            agent = a;
            destination_target = dest;
            roam_distance = roam;
            isPatrolling = false;
            playerFound = false;
            temp = template;
            agent.SetDestination(agent.transform.position);
            map_x_size = x;
            map_z_size = z;
        }
        private Vector3 FixBoundary(Vector3 new_dest)
        {
            if (new_dest.x > map_x_size)
                new_dest.x = map_x_size - 5f;
            if (new_dest.z > map_z_size)
                new_dest.z = map_z_size - 5f;
            if (new_dest.x <= 0f)
                new_dest.x = 5f;
            if (new_dest.z <= 0f)
                new_dest.z = 5f;
            return new_dest;
        }
        public void Patrol(bool first)
        {
            Vector3 new_dest = agent.transform.position + Random.insideUnitSphere * roam_distance;
            new_dest = FixBoundary(new_dest);
            new_dest.y = agent.transform.position.y+5f;
            float distance = agent.remainingDistance;
            Debug.Log("remaining distance is: " + distance);
            if (distance <= 3f && !isPatrolling || first)
            {
                agent.SetDestination(new_dest);
                isPatrolling = true; // Set the flag to true so it won't patrol again until reset
                //GameObject newEnemy = Instantiate(temp, new_dest, Quaternion.identity);
                //newEnemy.transform.localScale *= 10f;// = new Vector3(10f, 10f, 10f);
            }

            // Add a reset condition for when you want to patrol again
            if (distance > 3f && isPatrolling)
            {
                isPatrolling = false;
            }
        }
    }

    void Start()
    {
        for (int i = 0; i < enemy_count; i++)
        {
            GenerateRandomEnemies();
        }
        foreach (KeyValuePair<GameObject, EnemyInfo> enemyhandle in enemy)
        {
            enemyhandle.Value.Patrol(true);
        }
    }

    void GenerateRandomEnemies()
    {
        bool created = false;
        Vector3 randomSpawnPosition = Vector3.zero;
        RaycastHit hit = new RaycastHit();
        Vector3 terrainSize = map.terrainData.size;
        float max_x = terrainSize.x;
        float max_z = terrainSize.y;
        while (!created)
        {
            randomSpawnPosition = new Vector3(Random.Range(5f, terrainSize.x-5f), 7f, Random.Range(5f, terrainSize.z-5f));
            if (Physics.Raycast(randomSpawnPosition, -Vector3.up, out hit))
            {
                float offsetDistance = hit.distance;
                Debug.DrawLine(randomSpawnPosition, hit.point, Color.cyan);
                Debug.Log("Raycast hit at " + hit.point.ToString());
                Debug.Log("Generated Object at: " + randomSpawnPosition.ToString());
                created = true;
            }
        }
        randomSpawnPosition = new Vector3(randomSpawnPosition.x, hit.point.y, randomSpawnPosition.z);
        GameObject musuh = Instantiate(enemy_template, randomSpawnPosition, Quaternion.identity);
        musuh.transform.localScale *= 5f;
        NavMeshAgent agent = musuh.GetComponent<NavMeshAgent>();
        EnemyInfo newenemy_info = new EnemyInfo(agent, Vector3.zero, roam_distance, enemy_template, max_x, max_z);

        enemy.Add(musuh, newenemy_info);
    }

    // Update is called once per frame

    void Update()
    {
        foreach (KeyValuePair<GameObject, EnemyInfo> enemyhandle in enemy)
        {
            if(!enemyhandle.Value.playerFound)
                enemyhandle.Value.Patrol(false);
        }
    }

}
