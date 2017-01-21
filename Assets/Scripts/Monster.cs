using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This is a singleton!
public class Monster : MonoBehaviour {

    // the only instance of Monster
    public static Monster instance = null;

    private Vector3 target;
    NavMeshAgent agent;

	void Start () {

        // Singleton logic
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        agent = GetComponent<NavMeshAgent>();
        //StartCoroutine(NewPath());
	}
	
    IEnumerator NewPath()
    {
        agent.destination = target;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(NewPath());
    }

    public void SetTarget(Vector3 position)
    {
        agent.destination = position;
    }
}
