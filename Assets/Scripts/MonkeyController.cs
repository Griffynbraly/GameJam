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
        Hungry
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
        
        CreatePath();
        
    }
    void Idle()
    {
       
    }
    void Interacting()
    {

    }

    [System.Obsolete]
    void Mayhem()
    {

        path = new List<Node>
        {
            currentNode, // Start position
            AStarManager.instance.NodesInScene()[2], // Some test node
        };

        if (path != null || path.Count == 0)
        {
            Debug.Log($"Trying to find a path from {currentNode.name} to {AStarManager.instance.NodesInScene()[fakeEnd].name}");
            //path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.NodesInScene()[fakeEnd]);

            if (path == null)
            {
                Debug.LogError("A* failed! Path is NULL.");
            }
            else if (path.Count == 0)
            {
                Debug.LogError("A* returned an EMPTY path.");
            }
            else
            {
                Debug.Log($"Path found: {string.Join(" -> ", path.ConvertAll(n => n.name))}");
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
        if (path.Count == 0)
        {
            Debug.LogError("CreatePath() ERROR: path is EMPTY.");
            return;
        }
        if (path.Count == 0)
        {
            Debug.LogError("CreatePath() ERROR: path is EMPTY.");
            return;
        }
 
        if (path.Count > 0)
        {    
            int x = 0;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, -2), speed * Time.deltaTime);

            Debug.Log("Moving towards at");
            if (Vector2.Distance(transform.position, path[x].transform.position) < 0.1f)
            {
                currentNode = path[x];
                path.RemoveAt(x);  
            }
            Debug.Log("Path has been created");
        }
    }

    private void OnDrawGizmos()
    {
        if (path == null || path.Count < 2) return; // No path or not enough points to draw a line

        Gizmos.color = Color.red; // Color for the planned path

        for (int i = 0; i < path.Count - 1; i++)
        {
            Gizmos.DrawLine(path[i].transform.position, path[i + 1].transform.position);
        }
    }

}

