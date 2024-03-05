using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    //type for rigidBody
    private Rigidbody rbPlayer;
    private Vector3 direction = Vector3.zero;

    //Player Variables
    public float speed = 10f;

    //GameObjects
    public GameObject[] spawnPoints = null;

    //Dictionary
    private Dictionary<Item.VegetableType, int> Iteminventory = new Dictionary<Item.VegetableType, int>();


    //AddToInventoryFunction
    private void AddToInventory(Item item)
    {
        Iteminventory[item.typeOfVegetable]++;
    }
    //PrintInventory
    private void PrintInventory()
    {
        string output = "";

        foreach(KeyValuePair<Item.VegetableType, int> kvp in Iteminventory)
        {
            output += string.Format("{0}: {1} ", kvp.Key, kvp.Value);
        }
        Debug.Log(output);
    }

    private void Start()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        rbPlayer = GetComponent<Rigidbody>();
        spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");

        //Populate Dictionary
        foreach (Item.VegetableType item in System.Enum.GetValues(typeof(Item.VegetableType)))
        {
            Iteminventory.Add(item, 0);
        }

    }
    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //player Movement
        float horMove = Input.GetAxis("Horizontal");
        float verMove = Input.GetAxis("Vertical");

        direction = new Vector3(horMove,0 ,verMove);


    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        rbPlayer.AddForce(direction * speed,ForceMode.Force);

        //ConstraintsOfPlayArea

        if(transform.position.z > 40)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 40);
        }
        else if(transform.position.z < -40)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -40);
        }
    }

    //RespawnFuction
    private void Respawn()
    {
        int index = 0;
        while (Physics.CheckBox(spawnPoints[index].transform.position, new Vector3(1.5f, 1.5f, 1.5f)))
        {
            index++;
        }
        rbPlayer.MovePosition(spawnPoints[index].transform.position);
    }

    //ColliderOfVeggies
    private void OnTriggerEnter(Collider other)
    {
        if (!isLocalPlayer)
        {
            return;
        } 

        if (other.CompareTag("Item"))
        {
            Item item = other.gameObject.GetComponent<Item>();
            AddToInventory(item);
            PrintInventory();
        }
        if (other.CompareTag("Booster"))
        {
            speed += 25;
            StartCoroutine(BoostDown());
        }
    }

    //RespawnCollider
    private void OnTriggerExit(Collider other)
    {
        if (!isLocalPlayer)
        {
            return;
        } 

        Respawn();
    }

  

    private IEnumerator BoostDown()
    {

        yield return new WaitForSeconds(5.0f);
        speed -= 25;
    }

   
}
