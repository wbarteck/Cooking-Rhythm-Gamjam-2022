using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class CustomerQueue : MonoBehaviour
{
    public GameEvent AllCoursesFinished;

    public Transform orderTargetPosition;
    public Customer customerPrefab;

    private Customer current;

    public Melody[] courses;
    private Queue<Melody> courseQueue;

    private void Awake()
    {
         courseQueue = new Queue<Melody>(courses);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Customer>(out Customer cust))
        {
            if (cust != current)
            {
                Debug.Log($"Destroying {cust.name}");
                Destroy(cust.gameObject);
            }
        }
    }

    public async void SpawnCustomer()
    {
        Debug.Log("Spawn Customer");
        // SendCustomerHome previous customer home
        SendCustomerHome();
        // if no more customers in queue, end the game
        if (courseQueue.Count == 0)
        {
            AllCoursesFinished?.Raise();
            return;
        }
        // create a customer
        Customer c = Instantiate(customerPrefab, transform.position, customerPrefab.transform.rotation) as Customer;
        c.order = courseQueue.Dequeue();
        c.SetDestination(orderTargetPosition.position);
        c.target = orderTargetPosition;
        current = c;
    }

    public void SendCustomerHome()
    {
        if (current != null)
        {
            current.SetDestination(transform.position);
        }
        current = null;
    }

}
