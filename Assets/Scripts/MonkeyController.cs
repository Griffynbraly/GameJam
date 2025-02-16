using UnityEngine;
using System.Collections.Generic;
public class MonkeyController : MonoBehaviour
{
    public Node CurrentNode;
    public List<Node> path;
    [SerializeField] float speed;

    public enum StateMachine
    {
        Idle,
        Interacting,
        Mayhem,
    }

    public StateMachine stateMachine;

}
