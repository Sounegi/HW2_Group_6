using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public float roam_distance;
    public GameObject Patrol, dest_hold, player, bullet;
    public class EnemyInfo
    {
        public NavMeshAgent agent;
        public int hp;
        public bool isPatrolling;
        public Vector3 destination_target;
        float roam_distance, map_x_size, map_z_size;
        public GameObject dest_hold, player, bullet_holder;
        List<GameObject> patrol_points;
        int val = 0;
        float speed, acceleration;
        float patrol_time = 3f, remain_time;
        int lastchosen;
        public int enemy_type;
        public float shoot_interval;
        DetectAttack detect;

        public EnemyInfo(NavMeshAgent a, Vector3 dest, float roam, float x, float z, GameObject template_target, 
            GameObject player_info, List<GameObject> points, int type, DetectAttack detection)
        {
            agent = a;
            destination_target = dest;
            roam_distance = roam;
            isPatrolling = false;
            agent.SetDestination(agent.transform.position);
            bullet_holder = template_target;
            player = player_info;
            enemy_type = type;
            map_x_size = x;
            map_z_size = z;
            speed = agent.speed;
            acceleration = agent.acceleration;
            remain_time = patrol_time;
            patrol_points = points;
            lastchosen = -1;
            detect = detection;
            shoot_interval = -1;
        }
        public bool foundPlayer()
        {
            return detect.playerfound;
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

        int ChooseSpot(int lastchosen)
        {
            int prev = lastchosen;
            while (prev == lastchosen)
            {
                prev = Random.Range(0, patrol_points.Count);
            }
            return prev;
        }
        public void Patrol(bool first)
        {

            float distance = agent.remainingDistance;
            //Debug.Log(distance + "chosen spot:" + patrol_points[choose].gameObject.name);
            if (distance <= 0.03f && !isPatrolling)
            {
                int choose = ChooseSpot(lastchosen);
                agent.SetDestination(patrol_points[choose].transform.position);
                isPatrolling = true;
                lastchosen = choose;
            }

            if (distance > 5f && isPatrolling)
            {
                isPatrolling = false;
            }
        }

        public void Chase()
        {
            agent.SetDestination(player.transform.position);
        }

        public void Attack()
        {
            agent.isStopped = true;
            print("attack called");
            Debug.Log(agent.transform.position);
            GameObject bullet = Instantiate(bullet_holder, agent.transform.position, Quaternion.identity);
            //bullet.name = "kontol";
            Rigidbody bull_rb = bullet.GetComponent<Rigidbody>();
            Vector3 direction = player.transform.position - bullet.transform.position;
            bull_rb.AddForce(direction.normalized * 1000, ForceMode.Force);
            Destroy(bullet, 3);

            //yield return new WaitForSeconds(3);
        }
    }

    public Spawner spawner;
    Dictionary<GameObject, EnemyInfo> enemydict = new Dictionary<GameObject, EnemyInfo>();
    void Start()
    {
        List<GameObject> enemylist = spawner.enemy;
        List<GameObject> patrol_points = new List<GameObject>();
        foreach (Transform point in Patrol.GetComponentsInChildren<Transform>())
        {
            patrol_points.Add(point.gameObject);
        }

        int type;
        foreach (GameObject enemy in enemylist)
        {
            //1 = small fast
            //2 = big and slow
            //3 = ranged
            type = Random.Range(1, 3);
            type = 3;
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            DetectAttack detection = enemy.GetComponent<DetectAttack>();
            enemydict.Add(enemy, new EnemyInfo(agent, Vector3.zero, roam_distance, 200f, 200f, dest_hold, player, patrol_points, type, detection));
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyValuePair<GameObject, EnemyInfo> singleenemy in enemydict)
        {
            bool shoot_player = singleenemy.Value.foundPlayer();
            singleenemy.Value.shoot_interval -= Time.deltaTime;
            //SphereCollider range = singleenemy.Key.GetComponent<SphereCollider>();
            if (shoot_player)
            {
                if (singleenemy.Value.enemy_type == 3 && singleenemy.Value.shoot_interval <= 0)
                {
                    singleenemy.Value.Attack();
                    print("attacking!");
                    singleenemy.Value.shoot_interval = 4;
                }
            }
            else
            {
                singleenemy.Value.Chase();
            }
        }
    }

    
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(1f, 1f, 1f, 0.3f); ;// Color.yellow;
        
        foreach (KeyValuePair<GameObject, EnemyInfo> singleenemy in enemydict)
        {
            Gizmos.DrawSphere(singleenemy.Key.transform.position, 10f);
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        // Check if the collider belongs to one of the controlled objects
        if (other.CompareTag("Player"))
        {
        // Do something while the object stays in the trigger zone
            GameObject getObject = other.gameObject;
            EnemyInfo enemy_data = enemydict[getObject];
            enemy_data.Attack();
        }
    }
}