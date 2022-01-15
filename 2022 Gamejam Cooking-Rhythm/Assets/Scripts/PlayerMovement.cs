using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    public Camera camera;  //TODO fix to work with cinemachine

    public NavMeshAgent agent;

    public GridPoint[] allPoints;
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit))
            {
                //Find nearest Grid Point
                agent.SetDestination(FindNearestGridPoint(raycastHit.point)); //TODO realign with created "grid" system
            }

        }
    }

    Vector3 FindNearestGridPoint(Vector3 mousePoint)
    {
        Vector3 gridPoint = Vector3.positiveInfinity;
        for (int i = 0; i < allPoints.Length; i++)
        {
            if (Vector3.Distance(mousePoint, allPoints[i].transform.position) < Vector3.Distance(mousePoint, gridPoint))
            {
                gridPoint = allPoints[i].transform.position;
            }

        }

        return gridPoint;
    }
}
