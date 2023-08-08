using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField] private ChickenAnimations chickenAnimations;

    [SerializeField] private GameObject worm;

    private bool isReadyToDrop = true;

    public void Eat()
    {
        chickenAnimations.PlayEat();

        worm.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Worms"))
        {
            Vector3 startPosition = new Vector3(transform.position.x,0,transform.position.z);
            
            Vector3 endPosition = new Vector3(collision.gameObject.transform.position.x,0,collision.gameObject.transform.position.z);

            transform.forward = (endPosition - startPosition).normalized;

            Debug.DrawRay(transform.position + new Vector3(0,5,0), transform.forward*10, Color.red,5);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DropArea") && isReadyToDrop)
        {
            Debug.Log("Drop Area");
            if (other.transform.parent.parent.TryGetComponent(out Incubation incubation))
            {
                incubation.TakeEgg();
            }

        }
    }
}
