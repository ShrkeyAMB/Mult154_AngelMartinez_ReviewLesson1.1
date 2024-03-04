using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //ArrayOfObjetcs
    public GameObject[] lillyPadObjs = null;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnLillyPad", 2f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //SpawnFunction
    private void SpawnLillyPad()
    {
        foreach (GameObject lillyPad in lillyPadObjs)
        {
            Instantiate(lillyPad);
        }
    }
}
