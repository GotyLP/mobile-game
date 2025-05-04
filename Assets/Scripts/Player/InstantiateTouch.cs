using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InstantiateTouch : MonoBehaviour
{
    [SerializeField] GameObject[] _sphere; 
    [SerializeField] LayerMask _floorMask;

    //Input.touches[i];
    private void Update()
    {
        if (Input.touchCount <= 0) return;

        //Touch currentTouch = Input.touches[0];

        //if (currentTouch.phase == TouchPhase.Began) // Iguala la variable "firstTouch" a la primer fase del del toque 
        //{
        //    Ray touchRay = Camera.main.ScreenPointToRay(currentTouch.position);
        //    RaycastHit hit;

        //    if (Physics.Raycast(touchRay, out hit, 1000000, _floorMask, QueryTriggerInteraction.Ignore))
        //    {

        //        int random = Random.Range(0, _sphere.Count);
        //        GameObject spawnedObject = Instantiate(_sphere[random].gameObject);
        //        spawnedObject.transform.position = new Vector3(hit.point.x, hit.point.y + 1.5f, hit.point.z);
        //    }

        //}
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch currentTouch = Input.touches[i];

            if (currentTouch.phase == TouchPhase.Began) // Iguala la variable "firstTouch" a la primer fase del del toque 
            {
                Ray touchRay = Camera.main.ScreenPointToRay(currentTouch.position);

                RaycastHit hit;

                    if (Physics.Raycast(touchRay, out hit, 10000, _floorMask, QueryTriggerInteraction.Ignore))
                    {
                        int random = Random.Range(0, _sphere.Length);

                        GameObject spawnedObject = Instantiate(_sphere[random]);

                        spawnedObject.transform.position = new Vector3(hit.point.x, hit.point.y + 1.5f, hit.point.z);
                    }

            }

        }

    }
 

}
