using UnityEngine;

public class FloorCollider : MonoBehaviour
{
    public bool IsGrounded { get; private set; } = false;

    private void OnTriggerStay(Collider other)
    {
        if (!IsGrounded)
        {
            //Debug.Log("Touching ground");
        }
        IsGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Leaving ground");
        IsGrounded = false;
    }
}
