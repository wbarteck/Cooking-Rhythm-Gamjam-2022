using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    public Melody order;

    [SerializeField] NavMeshAgent navMeshAgent;

    public Transform target;

    private void Start()
    {
        target = GameObject.FindObjectOfType<VolumeTriggerEvent>().transform;
        navMeshAgent.SetDestination(target.position);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            GameManager.instance.PlaceOrder(order);
        }

    }
}
