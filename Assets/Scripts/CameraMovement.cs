using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    [SerializeField] Transform player;
    [SerializeField] Transform playerCollider;
    private float rotationSpeed = 8.0f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() {
        playerCollider.forward = new Vector3(player.position.x - transform.position.x, 0.0f, player.position.z - transform.position.z).normalized;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 targetDirection = playerCollider.forward * vertical + playerCollider.right * horizontal;
        if (targetDirection != Vector3.zero) {
            player.forward = Vector3.Slerp(player.forward, targetDirection.normalized, rotationSpeed * Time.deltaTime);
        }
    }
}
