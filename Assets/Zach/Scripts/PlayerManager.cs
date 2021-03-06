﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public PlayerController PlayerOne;
    public PlayerController PlayerTwo;

    void Awake()
    {
        if (ServiceLocator.instance.playerManager == null)
        {
            ServiceLocator.instance.playerManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
