using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public bool tested = false;
    public GameObject[] walls = new GameObject[4];
    // Start is called before the first frame update
    void Awake()
    {
       

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void resetWalls()
    {
       
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].SetActive(false);
        }
    }

    //public void DelayedReset()
    //{
       
    //    Invoke("resetWalls", 2f);
    //    Debug.Log("delayed");
    //}

    public void testForWalls()
    {
        bool rayConnected = false;
        Vector3[] rayDirections = new Vector3[4];
        rayDirections[0] = this.transform.forward;
        rayDirections[1] = -this.transform.forward;
        rayDirections[2] = this.transform.right;
        rayDirections[3] = -this.transform.right;
        float maxDist = 7f;
        float yDist = 1f;
        float xDist = 1f;

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Ray rayCheck = new Ray(this.transform.position, rayDirections[i]);
            RaycastHit hit = new RaycastHit();

            if (!Physics.Raycast(rayCheck, out hit, maxDist)) {
                walls[i].SetActive(true);
            }
        }
        tested = true;
    }
}
