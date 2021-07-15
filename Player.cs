using Movement;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MovementBehaviour movement;
    
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            movement.Jump();
        }
    }
}