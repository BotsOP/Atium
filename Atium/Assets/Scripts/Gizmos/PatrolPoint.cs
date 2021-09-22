using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.SetActive(false);
    }
}
