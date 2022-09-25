using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    [SerializeField] private Camera magnetCamera;
    [SerializeField] private int maxMagnetDistance = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = magnetCamera.ScreenPointToRay(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * maxMagnetDistance;
            if (hit.collider.tag == "Chunk")
            {
                hit.collider.GetComponent<Explosion>().Explode();
            }
        }
    }
}
