using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : MonoBehaviour
{
    [SerializeField] private GameObject enemyGhost;
    [SerializeField] private Renderer enemyGhostWeapon;
    private EnemyClone enemyClone;
    private float timeBetweenActions;
    private float delay;
    private float startTime;
    private Material ghostMat;
    private bool reset = true;
    
    private void OnEnable()
    {
        enemyClone = GetComponent<EnemyClone>();
        timeBetweenActions = enemyClone.timeBetweenActions;
        ghostMat = enemyGhost.GetComponent<SkinnedMeshRenderer>().material;
        enemyGhostWeapon.material = ghostMat;
        
        ResetGhost();
        
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
            Debug.Log("dissapearing");
            ghostMat.color = Color.Lerp(ghostMat.color, Color.clear, (Time.time - startTime) / 5);
        }
    }

    public void ResetGhost()
    {
        delay = 0;
        startTime = Time.time;
        ghostMat.color = Color.clear;
        reset = true;
    }

    public void EndGhost()
    {
        startTime = Time.time;
        reset = false;
    }
}
