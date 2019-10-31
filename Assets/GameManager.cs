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
    public Transform pathMakerSpherePrefab;
    public Camera mainCamera;
    private float total;
    private static int numOfLoads = 0;
    private bool noisePlayed = false;
    private bool doorMade = false;
    private Vector3 startingPos;


    private void Awake()
    {
        spawnerList = new List<GameObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        noisePlayed = false;
        doorMade = false;
        GameObject PathMakerSphere = Instantiate(pathMakerSpherePrefab.gameObject);
        startingPos = new Vector3(0, 100, 4.1f);
        //if (numOfLoads > 0)
        //{
        //    GameObject.Find("PathMakerSphere(Clone)").GetComponent<Pathmaker>().setUpSpawner();
        //}
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
            this.gameObject.transform.position = startingPos;
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
        Vector2 extremesZ = new Vector2(0, 0);

        if (positionList.Count > 0)
        {
            for (int i = 0; i < positionList.Count; i++)
            {
                averagePos.x += positionList[i].x;
                averagePos.z += positionList[i].z;

                if (positionList[i].z < extremesZ.x)
                {
                    extremesZ.x = positionList[i].z;

                } else if (positionList[i].z > extremesZ.y)
                {
                    extremesZ.y = positionList[i].z;
                }

            }
            float zVariance = extremesZ.y - extremesZ.x + 1;
            float yPos = mainCamera.transform.position.y;

            //Debug.Log(zVariance);
            if (total < positionList.Count)
            {

                yPos += (positionList.Count >> 2) / 30;

                if (yPos > zVariance)
                {
                    yPos = 50 + zVariance;
                }
            }

            total = positionList.Count;
            averagePos.x = averagePos.x / total;
            averagePos.z = averagePos.z / total;



            //mainCamera.transform.position = new Vector3(averagePos.x, yPos, averagePos.z - (yPos / 3.5f));
            //if (averagePos.z - mainCamera.transform.position.z > .1f || yPos - mainCamera.transform.position.y > .1f)
            //{
                mainCamera.transform.position = new Vector3(Lerp(mainCamera.transform.position.x, averagePos.x, .03f),
                    Lerp(mainCamera.transform.position.y, yPos, .04f), Lerp(mainCamera.transform.position.z, averagePos.z - (yPos / 3.5f), .04f));
            //}

        }

    }

    public static float Lerp(float position, float target, float amount)
    {
        float d = (target - position) * amount;
        return position + d;
    }
}
