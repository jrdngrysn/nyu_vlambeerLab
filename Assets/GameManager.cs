using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Vector3> positionList = new List<Vector3>();
    public List<GameObject> spawnerList = new List<GameObject>();
    public List<GameObject> tileList = new List<GameObject>();
    public Camera mainCamera;
    private float total;
    private static int numOfLoads = 0;
    private bool noisePlayed = false;
    private bool doorMade = false;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        noisePlayed = false;
        doorMade = false;
        if (numOfLoads > 0)
        {
            GameObject.Find("PathMakerSphere").GetComponent<Pathmaker>().setUpSpawner();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 0; i < tileList.Count; i++)
            {
                tileList[i].GetComponent<FloorScript>().resetWalls();
            }
            tileList.Clear();
            positionList.Clear();
            spawnerList.Clear();
            numOfLoads++;
            SceneManager.LoadScene("mainLabScene");
        }

        if (spawnerList.Count < 1)
        {
            if (!noisePlayed)
            {
                MusicManager.Instance.noiseSource.pitch = 1;
                MusicManager.Instance.noiseSource.PlayOneShot(MusicManager.Instance.hammerNoise);
                noisePlayed = true;
            }
            for (int i = 0; i < tileList.Count; i++)
            {
                if (!tileList[i].GetComponent<FloorScript>().tested)
                {
                    tileList[i].GetComponent<FloorScript>().testForWalls();
                }

                //tileList[i].SendMessage("DelayedReset");
            }

            if (!doorMade)
            {
               int r = Random.Range(0, tileList.Count);

                for (int i = 0; i < tileList.Count; i++)
                {
                    int w = Random.Range(0, 3);
                    if (r == i)
                    {
                        tileList[i].GetComponent<FloorScript>().walls[w].SetActive(false);
                        doorMade = true;
                    }

                }
            }


        }




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
