using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarParent : MonoBehaviour {

    public static SonarParent instance;

    private Renderer[] rends;
    // Throwaway values to set position to at the start.
    private static Vector4 zeroPosition = new Vector4(-5000, -5000, -5000, -5000);

    // Use this for initialization
    void Start()
    {
        // Singleton logic
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        rends = GetComponentsInChildren<Renderer>();

        // initially set _hitPt to zero
        foreach (Renderer r in rends)
        {
            r.material.SetVector("_hitPt", zeroPosition);
            r.material.SetFloat("_StartTime", -1000);
        }
    }

    /// <summary>
    /// Will make a point begin glowing
    /// </summary>
    /// <param name="position">The location of the point to make glow</param>
    public void StartScan(Vector4 position, float intensity)
    {
        // set new values in shader
        foreach (Renderer r in rends)
        {
            r.material.SetVector("_hitPt", position);
            r.material.SetFloat("_StartTime", Time.time);
            r.material.SetFloat("_Intensity", intensity);
        }
    }

}
