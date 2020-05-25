using UnityEngine;
using UnityEngine.AI;

public class Navegacao : MonoBehaviour
{

    public Transform target;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        agent.SetDestination(target.transform.position);
    }

}
