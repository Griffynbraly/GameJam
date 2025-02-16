using UnityEngine;
using System.Collections.Generic;
public class MonkeyController : MonoBehaviour
{
    public Node currentNode;
    public List<Node> path = new List<Node>();
    [SerializeField] float speed;
    public int energy = 10;
    public bool hungry = true;
    [SerializeField] int fakeEnd;


    public enum StateMachine
    {
        Idle,
        Mayhem,
        Interacting,
        Hungry,
    }

    public StateMachine currentState;

    private void Start()
    {
        currentState = StateMachine.Mayhem;
    }

    [System.Obsolete]
    private void Update()
    {
        switch (currentState)
        {
            case StateMachine.Idle:
                Idle();
                break;
            case StateMachine.Mayhem:
                Mayhem();
                break;
            case StateMachine.Interacting:
                Interacting();
                break;
            case StateMachine.Hungry:
                Hungry();
                break;
        }

        if (energy <= 20 && currentState != StateMachine.Idle && currentState != StateMachine.Interacting)
        {
            currentState = StateMachine.Idle;
            path.Clear();
            Debug.Log("cleared Idle");
        }
        else if (energy > 20 && currentState != StateMachine.Mayhem && currentState != StateMachine.Interacting)
        {
            currentState = StateMachine.Mayhem;
            path.Clear();
            Debug.Log("cleared mayhem");
        }
        else if (energy > 20 && hungry && currentState != StateMachine.Hungry && currentState != StateMachine.Interacting)
        {
            currentState = StateMachine.Hungry;
            path.Clear();
        }

        Debug.Log("Path incoming");
        CreatePath();
        
    }
    [System.Obsolete]
    void Idle()
    {
        if (path != null)
        {
            if (path.Count == 0)
            {
                path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.NodesInScene()[Random.Range(0, AStarManager.instance.NodesInScene().Length)]);
                Debug.Log("path = idle");
            }
        }
       
    }
    void Interacting()
    {

    }

    [System.Obsolete]
    void Mayhem()
    {
        if (path != null)
        {
            if (path.Count == 0)
            {
                path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.NodesInScene()[fakeEnd]);
                Debug.Log("path = mayhem");
            }
        }
    }
    void Hungry()
    {
        if (path.Count > 0)
        {
            
        }
    }
    void CreatePath()
    {
        Debug.Log("Im bouta make a pathhh");

        if (path == null || path.Count == 0)
        {
            Debug.LogWarning("No path available in CreatePath().");
            return;
        }
        if (path.Count > 0)
       {
            int x = 0;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, -2), speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, path[x].transform.position) < 0.1f)
            {
                currentNode = path[x];
                path.RemoveAt(x);  
            }
            Debug.Log("Path has been created");
        }
    }
}
