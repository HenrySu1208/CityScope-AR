using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;


public class rayOriginal : MonoBehaviour
{
    [SerializeField] ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit> ();

    public RaycastHit hit;
    public Text DebugText;

    [SerializeField]
    GameObject spawnablePrefab;
    Camera arCam;
    GameObject spawnedObject;
    Vector3 webcamtest_tag_position;
    Quaternion webcamtest_tag_rotation;
    //public bool find = false;
    //public bool camera_change = false;
    bool webcamtest_position_set;

    bool object_spawned;

    void Start()
    {
        
        spawnedObject = null;
        object_spawned = false;
        
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
        
    }

    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }
        if (Input.touchCount > 0)
        {
            //Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);
            // ARSessionOrigin arOrigin = FindObjectOfType<ARSessionOrigin>();

            // Vector3 camPosition = arOrigin.transform.InverseTransformPoint(webcamtest_tag_position);

            // Ray ray = new Ray(arCam.transform.position, camPosition - arCam.transform.position);
            
            // if(m_RaycastManager.Raycast(webcamtest_tag_position, webcamtest_tag_rotation, TrackableType.PlaneWithinPolygon))

            if(m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits,  TrackableType.PlaneWithinPolygon))
            {
                var hitpose=m_Hits[0].pose;
                if(!object_spawned)
                {
                    spawnedObject = Instantiate(spawnablePrefab, hitpose.position, hitpose.rotation);
                    object_spawned = true;
                    DebugText.text = hitpose.position.ToString();
                    // spawnedObject = Instantiate(spawnablePrefab, webcamtest_tag_position, webcamtest_tag_rotation);
                    // object_spawned = true;
                    // DebugText.text = spawnedObject.transform.position.ToString();
                }
                else
                {
                    spawnedObject.transform.position=hitpose.position;
                    DebugText.text = hitpose.position.ToString();
                    // spawnedObject.transform.position=webcamtest_tag_position;
                    // DebugText.text = webcamtest_tag_position.ToString();
                    // DebugText.text = "none";
                }
            }
        }
    }
}

