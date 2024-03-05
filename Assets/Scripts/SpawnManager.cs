using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnManager : NetworkBehaviour
{
    //ArrayOfObjetcs
    public GameObject[] lillyPadObjs = null;

    // Start is called before the first frame update
    public override void OnStartServer()
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
            GameObject tempLillyPad = Instantiate(lillyPad);
            NetworkServer.Spawn(tempLillyPad);
        }
    }
}
