using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : MonoBehaviour
{
    [SerializeField] private Transform enemyGhost;
    [SerializeField] private GameObject enemyGhostChar;
    [SerializeField] private Renderer enemyGhostWeapon;
    private EnemyClone enemyClone;
    private float timeBetweenActions;
    private float delay;
    private float startTime;
    private Material ghostMat;
    private bool reset;
    private bool hasReset;
    
    private void OnEnable()
    {
        enemyClone = GetComponent<EnemyClone>();
        timeBetweenActions = enemyClone.timeBetweenActions;
        ghostMat = enemyGhostChar.GetComponent<SkinnedMeshRenderer>().material;
        enemyGhostWeapon.material = ghostMat;
        
        EventSystem.Subscribe(EventType.START_GHOST, StartGhost);
        EventSystem.Subscribe(EventType.END_GHOST, EndGhost);
        
        //ResetGhost();
        
        //Change material to transparent mode
        ghostMat.SetOverrideTag("RenderType", "Transparent");
        ghostMat.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
        ghostMat.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        ghostMat.SetInt("_ZWrite", 0);
        ghostMat.DisableKeyword("_ALPHATEST_ON");
        ghostMat.EnableKeyword("_ALPHABLEND_ON");
        ghostMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        ghostMat.renderQueue = (int) UnityEngine.Rendering.RenderQueue.Transparent;
    }

    void Update()
    {
        if(delay < timeBetweenActions && reset)
        {
            
            enemyClone.delay = Mathf.Lerp(delay, timeBetweenActions, (Time.time - startTime) / 10);
            ghostMat.color = Color.Lerp(Color.clear, new Color(0, 0.952482f, 1, 0.2980392f), (Time.time - startTime) / 2);
        }

        if (!reset)
        {
            ghostMat.color = Color.Lerp(ghostMat.color, Color.clear, (Time.time - startTime) / 5);
        }
        if (!reset && ghostMat.color == Color.clear && !hasReset)
        {
            StartCoroutine("Reset");
        }
    }

    private IEnumerator Reset()
    {
        float cachedDelay = enemyClone.delay;
        enemyGhost.position = transform.position;
        enemyClone.delay = 0;
        hasReset = true;
        //enemyClone.newOrders = true;
        yield return new WaitForSeconds(cachedDelay);
        enemyClone.newOrders = false;
    }

    public void StartGhost()
    {
        enemyClone.newOrders = false;
        delay = 0;
        startTime = Time.time;
        ghostMat.color = Color.clear;
        reset = true;
    }

    public void EndGhost()
    {
        startTime = Time.time;
        reset = false;
        hasReset = false;
    }

    private void OnDisable()
    {
        EventSystem.Unsubscribe(EventType.START_GHOST, StartGhost);
        EventSystem.Unsubscribe(EventType.END_GHOST, EndGhost);
    }
}
