﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    

    public float moveSpeedWhenRoomChanged;

    public float xOffset;

    public float yOffset;

    public Room currRoom;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        RoomController.instance.nextRoomDelegate += UpdateCameraLocation;
        Debug.Log("CameraController is now subsribed.");
    }

    private void UpdateCameraLocation(Room room)
    {
        currRoom = room;
        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        
    }

    private void UpdatePosition()
    {
        if(currRoom == null)
        {
            return;
        }

        Vector3 targetPos = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeedWhenRoomChanged);
    }

    private Vector3 GetCameraTargetPosition()
    {
        if (currRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPos = currRoom.GetRoomCenter() + new Vector3(xOffset, yOffset, 0);
        targetPos.z = transform.position.z;

        return targetPos;
    }

    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
