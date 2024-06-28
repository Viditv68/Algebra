using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClockController : MonoBehaviour
{
    public GameObject hourHand;
    public GameObject minuteHand;
    public Camera cam;
    private Vector3 mousePos;
    private Vector3 previousPos;
    private Button btn;

    int hour = 0;
    int min = 0;



    //private void Update()
    //{
    //    RaycastHit2D hit;
    //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    //    if (hit = Physics2D.Raycast(ray.origin, new Vector2(0, 0)))
    //        Debug.Log(hit.collider.name);


    //}

    private bool IsMouseOverUI()
    {
       return EventSystem.current.IsPointerOverGameObject();
    }
    
    public void IncrementHourHand() 
    {
        hourHand.transform.Rotate(0, 0, -30f);
        if (hour == 12)
            hour = 1;
        else
            hour++;
        Debug.Log(hour + ": " + min);
    }

    public void DecrementHourHand()
    {
        hourHand.transform.Rotate(0, 0, 30f);
        if (hour == 12)
            hour = 11;
        else
            hour--;
        Debug.Log(hour + ": " + min);
    }

    public void IncrementMinuteHand()
    {
        minuteHand.transform.Rotate(0, 0, -30f);
        hourHand.transform.Rotate(0, 0, -2.5f);
        min = min + 5;
        if(min == 60)
        {
            min = 0;
            if (hour == 12)
                hour = 1;
            else
                hour++;
        }
        Debug.Log(hour + ": " + min);
    }

    public void DecrementMinueHand()
    {
        int prevVal = min;
        minuteHand.transform.Rotate(0, 0, 30f);
        hourHand.transform.Rotate(0, 0, 2.5f);
        min = min - 5;
        if (min <= 0 )
        {
            if (hour == 1)
                hour = 12;
            else
                hour--;
            min = 55;
        }
        Debug.Log(hour + ": " + min);

    }



}
