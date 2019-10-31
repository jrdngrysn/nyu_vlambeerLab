using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCarry : MonoBehaviour
{
 public static AudioCarry _MusicMan;


    void Awake()
    {
        if (_MusicMan == null)
        {
            _MusicMan = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
