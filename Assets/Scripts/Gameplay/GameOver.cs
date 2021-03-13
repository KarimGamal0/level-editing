using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            Debug.Log("collider");
            //Destroy(other.gameObject);
            Debug.Log(other.gameObject.name);
            CharacterController controller = other.gameObject.GetComponent<CharacterController>();
            controller.enabled = false;
            controller.transform.position = new Vector3(0, 0, 0);
            controller.enabled = true;
        }
    }
}
