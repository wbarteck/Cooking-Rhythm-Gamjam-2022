using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using Sirenix.OdinInspector;

public class PlayerMovement : MonoBehaviour
{

    private Camera camera;

    [SerializeField] NavMeshAgent agent;
    private Transform nearestPoint;
    [SerializeField, ReadOnly] GridPoint[] allPoints;

    private void Awake()
    {
        camera = Camera.main;
        allPoints = FindObjectsOfType<GridPoint>();
    }


    void Update()
    {
        // dont raycast if mouse is over UI
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit))
            {
                //Find nearest Grid Point
                agent.SetDestination(FindNearestGridPoint(raycastHit.point)); //TODO realign with created "grid" system
            }

        }

        // face the right orientation
        if (agent.remainingDistance < 1f)
        {
            Vector3 desiredForward = nearestPoint?.transform.forward ?? transform.forward;
            transform.forward = Vector3.Lerp(transform.forward, desiredForward, Time.deltaTime * 5f);
        }
    }

    Vector3 FindNearestGridPoint(Vector3 mousePoint)
    {
        Vector3 gridPoint = Vector3.positiveInfinity;
        for (int i = 0; i < allPoints.Length; i++)
        {
            if (Vector3.Distance(mousePoint, allPoints[i].transform.position) < Vector3.Distance(mousePoint, gridPoint))
            {
                nearestPoint = allPoints[i].transform;
                gridPoint = allPoints[i].transform.position;
            }

        }

        return gridPoint;
    }

    public void MoveHere(Transform destination)
    {
        nearestPoint = destination;
        agent.SetDestination(destination.position);
    }
}
