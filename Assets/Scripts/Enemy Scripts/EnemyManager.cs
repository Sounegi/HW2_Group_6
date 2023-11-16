using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class EnemyManager : MonoBehaviour {

    private static EnemyManager instance;

    private void Awake() {
        instance = this;
    }

    public static EnemyManager GetInstance() {
        return instance;
    }


    public Spawner spawner;
    Scene current_scene;
    string current_scene_string;

    int enemyCount = 0;

    void Start() {
        enemyCount = spawner.enemy.Count;
        current_scene = SceneManager.GetActiveScene();
        current_scene_string = current_scene.name;
    }

    public event System.Action OnEnd;

    public void DecreaseEnemy() {
        enemyCount--;
    }

    void Update() {
        if (enemyCount == 0) {
            print("GAME ENDED");
            OnEnd?.Invoke();
        }
            
    }
    
    /*
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(1f, 1f, 1f, 0.3f); ;// Color.yellow;
        
        foreach (KeyValuePair<GameObject, EnemyInfo> singleenemy in enemydict)
        {
            Gizmos.DrawSphere(singleenemy.Key.transform.position, 10f);
        }
    }*/
}