using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drift : MonoBehaviour
{

    //GeneralVariables
    private float speed = 5f;

    //EnumOfStates
    public enum DriftDirection
    {
        LEFT = -1,
        RIGHT = 1
    }
    public DriftDirection driftDirection = DriftDirection.LEFT;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //BuildStates
        switch (driftDirection)
        {
            case DriftDirection.LEFT:
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            break;

            case DriftDirection.RIGHT:
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            break;
        }
        
        //DestroyOutOfBounds
        if(transform.position.x < -80 || transform.position.x > 80)
        {
            Destroy(gameObject);
        }
    }
}
