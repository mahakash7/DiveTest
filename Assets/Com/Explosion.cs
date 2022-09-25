using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float cubeSize = 20f;
    public int cubesInRow = 5;

    float cubesPivotDistance;
    Vector3 cubesPivot;
    bool exploded;
    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    // Use this for initialization
    void Start() {

        exploded = false;
        //calculate pivot distance
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        //use this value to create pivot vector)
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

    }

    // Update is called once per frame
    void Update() {

    }

    public void Explode() {
        //make object disappear
       

        //loop 3 times to create 5x5x5 pieces in x,y,z coordinates
        for (int x = 0; x < cubesInRow; x++) {
            for (int y = 0; y < 1; y++) {
                for (int z = 0; z < cubesInRow; z++) {
                    CreatePiece(x, y, z);
                }
            }
        }

        //get explosion position
        Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        //add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders) {
            //get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) {
                //add explosion force to this body with given parameters
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }

       
        Destroy(this.gameObject);
        if (exploded == false)
        {
            exploded = true;
        }
    }

    void CreatePiece(int x, int y, int z) {

        //create piece
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        
        piece.AddComponent<CashObjectController>();
    }   
 }
