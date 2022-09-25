using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashObjectController : MonoBehaviour
{
    public GameObject magnet;
    float speed = 60f;
    [SerializeField] private float distance;
    // Start is called before the first frame update
    void Start()
    {
        magnet = GameObject.FindGameObjectWithTag("Magnet");
    }

    // Update is called once per frame
    void Update()
    {
         distance = Vector3.Distance(this.transform.position, magnet.transform.position);
        
        if(distance > 2 && distance < 100 )
        {
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, magnet.transform.position, step);
        }
        else
        {
            GameManager.Instance.GameplayManager.AddCash();
            Destroy(this.gameObject);
        }
    }
}
