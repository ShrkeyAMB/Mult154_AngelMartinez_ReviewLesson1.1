using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //type for rigidBody
    private Rigidbody rbPlayer;
    private Vector3 direction = Vector3.zero;

    //Player Variables
    public float speed = 10f;

    //GameObjects
    public GameObject spawnPoint = null;

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
        rbPlayer = GetComponent<Rigidbody>();

        //Populate Dictionary
        foreach (Item.VegetableType item in System.Enum.GetValues(typeof(Item.VegetableType)))
        {
            Iteminventory.Add(item, 0);
        }

    }
    private void Update()
    {
        //player Movement
        float horMove = Input.GetAxis("Horizontal");
        float verMove = Input.GetAxis("Vertical");

        direction = new Vector3(horMove,0 ,verMove);


    }

    private void FixedUpdate()
    {
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
        rbPlayer.MovePosition(spawnPoint.transform.position);
    }

    //ColliderOfVeggies
    private void OnTriggerEnter(Collider other)
    {
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
        Respawn();
    }

  

    private IEnumerator BoostDown()
    {
        yield return new WaitForSeconds(5.0f);
        speed -= 25;
    }

   
}
