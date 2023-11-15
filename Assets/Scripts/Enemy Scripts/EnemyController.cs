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
        //patrol stuff
        public bool isPatrolling;
        public Vector3 destination_target;
        float roam_distance, map_x_size, map_z_size;
        float speed, acceleration;
        float patrol_time = 3f, remain_time;
        int lastchosen;
        List<GameObject> patrol_points;
        // patrol stuff end

        //enemy basic information
        public NavMeshAgent agent;
        public int hp;
        public GameObject dest_hold, player, bullet_holder;
        public int enemy_type;
        public float shoot_interval;
        DetectAttack detect;
        EnemyHealthManager manage;
        //end

        public EnemyInfo(NavMeshAgent a, Vector3 dest, float roam, float x, float z, GameObject template_target, 
            GameObject player_info, int type, DetectAttack detection, EnemyHealthManager info)
        {
            agent = a;
            //patrol stuff
            destination_target = dest;
            roam_distance = roam;
            isPatrolling = false;
            map_x_size = x;
            map_z_size = z;
            speed = agent.speed;
            acceleration = agent.acceleration;
            remain_time = patrol_time;
            lastchosen = -1;
            //patrol_points = points;
            // patrol stuff end

            //basic info
            
            agent.SetDestination(agent.transform.position);
            bullet_holder = template_target;
            player = player_info;
            enemy_type = type;
            shoot_interval = -1;
            detect = detection;
            hp = SetHP(type);
            manage = info;

            manage.onHit += damaged;
        }

        private int SetHP(int enemy_type)
        {
            if (enemy_type == 1 || enemy_type == 3)
                return 1;
            else
                return 3;
        }
        public bool foundPlayer()
        {
            return detect.playerfound;
        }

        public void damaged()
        {
            print("kepukul di damaged");
            //if (manage.gothit)
             hp -= 1;

            //return manage.gothit;
        }

        public bool isDead()
        {
            print(hp);
            if (hp <= 0)
                return true;
            else
                return false;
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
            /*
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
            */
        }

        public void Chase()
        {
            agent.SetDestination(player.transform.position);
        }

        public void Attack()
        {
            if (enemy_type != 3)
                return; // not range, just keep chasing.
            agent.isStopped = true;

            print("attack called");
            Debug.Log(agent.transform.position);
            GameObject bullet = Instantiate(bullet_holder, agent.transform.position, Quaternion.identity);
            AudioSource audioSource = agent.GetComponent<AudioSource>();
            AudioManager.GetInstance().PlaySoundEffect(3, 0.5f, audioSource);
            //bullet.name = "kontol";
            Rigidbody bull_rb = bullet.GetComponent<Rigidbody>();
            Vector3 direction = player.transform.position - bullet.transform.position;
            bull_rb.AddForce(direction.normalized * 1000, ForceMode.Force);
            Destroy(bullet, 3);
            shoot_interval = 4;
            //yield return new WaitForSeconds(3);
        }
    }

    public Spawner spawner;
    Scene current_scene;
    string current_scene_string;
    Dictionary<GameObject, EnemyInfo> enemydict = new Dictionary<GameObject, EnemyInfo>();
    void Start()
    {
        List<KeyValuePair<GameObject, int>> enemylist = spawner.enemy;
        current_scene = SceneManager.GetActiveScene();
        current_scene_string = current_scene.name;
        /*
        List<GameObject> patrol_points = new List<GameObject>();
        foreach (Transform point in Patrol.GetComponentsInChildren<Transform>())
        {
            patrol_points.Add(point.gameObject);
        }
        */
        int type;
        foreach (KeyValuePair<GameObject, int> enemy in enemylist)
        {
            //1 = small fast
            //2 = big and slow
            //3 = ranged
            NavMeshAgent agent = enemy.Key.GetComponent<NavMeshAgent>();
            DetectAttack detection = enemy.Key.GetComponentInChildren<DetectAttack>();
            EnemyHealthManager manage = enemy.Key.GetComponent<EnemyHealthManager>();

            type = enemy.Value;
            enemydict.Add(enemy.Key, new EnemyInfo(agent, Vector3.zero, roam_distance, 200f, 200f, dest_hold, player, type, detection, manage));
        }
        Destroy(spawner);
    }

    // Update is called once per frame

    public event System.Action OnEnd;

    void Update()
    {
        List<GameObject> deleted_enemy = new List<GameObject>();
        foreach (KeyValuePair<GameObject, EnemyInfo> singleenemy in enemydict)
        {
            bool shoot_player = singleenemy.Value.foundPlayer();
            //bool hitted = singleenemy.Value.damaged();
            bool dead = singleenemy.Value.isDead();
            singleenemy.Value.shoot_interval -= Time.deltaTime;
            //SphereCollider range = singleenemy.Key.GetComponent<SphereCollider>();

            if (dead)
            {
                deleted_enemy.Add(singleenemy.Key);
                //enemydict.Remove(singleenemy.Key);
                //Destroy(singleenemy.Key);
                continue;
            }
            
            if (shoot_player && singleenemy.Value.enemy_type == 3)
            {
                if (singleenemy.Value.shoot_interval <= 0)
                {
                    singleenemy.Value.Attack();
                    print("attacking!");
                }
            }
            else
            {
                singleenemy.Value.Chase();
            }
            
        }
        foreach(GameObject delete in deleted_enemy)
        {
            enemydict.Remove(delete);
            Destroy(delete);
        }

        if (enemydict.Count == 0) OnEnd?.Invoke();

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
}