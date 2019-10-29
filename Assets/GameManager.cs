using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Vector3> positionList = new List<Vector3>();
    public Camera mainCamera;
    private float total;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 averagePos = new Vector3(0,0,0);

        if (positionList.Count > 0)
        {
            for (int i = 0; i < positionList.Count; i++)
            {
                averagePos.x += positionList[i].x;
                averagePos.z += positionList[i].z;
            }
            float yPos = mainCamera.transform.position.y;

            if (total < positionList.Count)
            {

                yPos += (positionList.Count >> 2) / 20;

                if (yPos > 185)
                {
                    yPos = 185;
                }
            }

            total = positionList.Count;
            averagePos.x = averagePos.x / total;
            averagePos.z = averagePos.z / total;



            mainCamera.transform.position = new Vector3(averagePos.x, yPos, averagePos.z - (yPos / 3.5f));
        }
    }
}
