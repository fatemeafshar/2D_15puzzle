using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Block : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler 
{
    public int label;
    UnityEvent mouseDownEvent, mouseUpEvent;
    Vector3 start, end;
    public Vector3 pos;
    public Controller controller;
    void Start()
    {
        if (mouseDownEvent == null)
            mouseDownEvent = new UnityEvent();



        mouseDownEvent.AddListener(downListener);


        if (mouseUpEvent == null)
            mouseUpEvent = new UnityEvent();

        mouseUpEvent.AddListener(upListener);
    }

    void Update()
    {
        //    if (Input.GetMouseButtonDown(0) && mouseDownEvent != null)
        //    {
        //        mouseDownEvent.Invoke();}



        if (Input.GetMouseButtonUp(0) && mouseDownEvent != null)
        {
            mouseUpEvent.Invoke();


        }
    }

    void downListener() { }
      
    public void OnPointerDown(PointerEventData eventData) //downListener()
    {
        start = Input.mousePosition;
        //Debug.Log("Bing");
        //Debug.Log(start);
    }
    
    void upListener() { }
    public void OnPointerUp(PointerEventData eventData) //upListener()
    {
        end = Input.mousePosition;
        //Debug.Log(end);

        var difference = end - start;
        //Debug.Log(difference);
        int step = 30;
        if (difference.x > step) { difference.x = 1; } else if(difference.x < -step) { difference.x = -1; } else { { difference.x = 0; } }
        if (difference.y > step) { difference.y = 1; } else if (difference.y < -step) { difference.y = -1; } else { { difference.y = 0; } }
        //Debug.Log(difference);
        if (moveable && controller.checkEmpty(pos + difference) &&(difference.x == 0 || difference.y == 0))
        {
            transform.position = transform.position + (difference * 100);
            controller.setEmpty(pos);
            //difference.y = -difference.y;
            pos = pos + difference;
            //Debug.Log(label);
    }
        moveable = false;
        //Debug.Log(transform.position + (difference * 100));
        end = Vector2.zero;
        start = end;
        Debug.Log("Ping");
    }

    private Boolean moveable = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        moveable = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //moveable = false;
    }
}
