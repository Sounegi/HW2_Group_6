using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionArea : MonoBehaviour
{
    [field: SerializeField]
    public bool PlayerDetected { get; private set; }

    //overlapbox parameters
    [SerializeField] private Transform detectorOrigin;
    public Vector3 detectorSize = Vector3.one;
    public Vector3 detectorOriginOffset = Vector3.zero;

    public float detectionDelay = 0.3f;

    public LayerMask detectLayerMask;

    //Gizmo parameters
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;
    public bool showGizmo = true;

    private GameObject targetPlayer;

    public GameObject TargetPlayer
    {
        get => targetPlayer;
        private set
        {
            targetPlayer = value;
            PlayerDetected = targetPlayer != null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    IEnumerator DetectionCoroutine()
    {
        Debug.Log("start detect rotine");
        yield return new WaitForSeconds(detectionDelay);
        CheckDetection();
        StartCoroutine(DetectionCoroutine());
    }

    public void CheckDetection()
    {
        Debug.Log("Check Detection");
        Collider[] detectedcollider = Physics.OverlapBox((Vector3)detectorOrigin.position + detectorOriginOffset, 
            detectorSize, 
            Quaternion.identity, 
            detectLayerMask);
        if (detectedcollider != null)
        {
            foreach (var co in detectedcollider)
            {
                Debug.Log("Detected " + co.gameObject.tag);
                if (co.gameObject.tag == "Player")
                {
       
                    TargetPlayer = co.gameObject;
                    break;
                }
                else
                    TargetPlayer = null;
            }
        }
        else
        {
            TargetPlayer = null;
        }
        //Debug.Log("Detected " + targetPlayer.gameObject.tag);
    }

    private void OnDrawGizmos()
    {
        if(showGizmo && detectorOrigin != null)
        {
            Gizmos.color = gizmoIdleColor;
            if (PlayerDetected)
                Gizmos.color = gizmoDetectedColor;
            Gizmos.DrawCube((Vector3)detectorOrigin.position + detectorOriginOffset, detectorSize);
        }
        
    }
}
