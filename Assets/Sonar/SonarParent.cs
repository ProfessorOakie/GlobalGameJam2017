using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarParent : MonoBehaviour {

    public static SonarParent instance;

    private Renderer[] rends;
    // Throwaway values to set position to at the start.
    private static Vector4 zeroPosition = new Vector4(-5000, -5000, -5000, -5000);

    // Set the last value to the time
    private static int queueCount = 20;//must be same as array size in shader
    private Queue<Vector4> positionsQueue = new Queue<Vector4>(queueCount);
    private Queue<float> intensityQueue = new Queue<float>(queueCount);


    [Tooltip("Must also change the shader on the objects.")]
    [SerializeField]
    private bool isMulti = false;

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

        for(int i = 0; i < queueCount; i++)
        {
            positionsQueue.Enqueue(zeroPosition);
            intensityQueue.Enqueue(-5000f);
        }

        // initially set _hitPt to zero
        foreach (Renderer r in rends)
        {
            if (isMulti)
                r.material.SetVectorArray("_hitPts", positionsQueue.ToArray());
            else
            {
                r.material.SetVector("_hitPt", zeroPosition);
                r.material.SetFloat("_StartTime", -1000);
                r.material.SetFloat("_Intensity", 1);
            }
        }
    }

    /// <summary>
    /// Will make a point begin glowing
    /// </summary>
    /// <param name="position">The location of the point to make glow</param>
    public void StartScan(Vector4 position, float intensity)
    {
        position.w = Time.time;
        positionsQueue.Dequeue();
        positionsQueue.Enqueue(position);

        intensityQueue.Dequeue();
        intensityQueue.Enqueue(intensity);

        foreach (Renderer r in rends)
        {
            if (isMulti)
            {
                r.material.SetVectorArray("_hitPts", positionsQueue.ToArray());
                r.material.SetFloatArray("_Intensity", intensityQueue.ToArray());
            }
            else
            {
                r.material.SetVector("_hitPt", position);
                r.material.SetFloat("_StartTime", Time.time);
                r.material.SetFloat("_Intensity", intensity);
            }
        }
    }

}
