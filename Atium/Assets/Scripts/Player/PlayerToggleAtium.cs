using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToggleAtium : MonoBehaviour
{
    private bool atiumOn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (atiumOn)
            {
                EventSystem.RaiseEvent(EventType.END_GHOST);
                atiumOn = !atiumOn;
            }
            else
            {
                EventSystem.RaiseEvent(EventType.START_GHOST);
                atiumOn = !atiumOn;
            }
        }
    }
}
