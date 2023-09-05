using UnityEngine;



public class FreeState : CharacterState
{

    public override void OnEnter()
    {
    }

    public override void OnFixedUpdate()
    {
        var forwardVectorOnFloor = Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, Vector3.up);
        forwardVectorOnFloor.Normalize();
        var backwardVectorOnFloor = Vector3.ProjectOnPlane(-m_stateMachine.Camera.transform.forward, Vector3.up);
        backwardVectorOnFloor.Normalize();
        var leftVectorOnFloor = Vector3.ProjectOnPlane(-m_stateMachine.Camera.transform.right, Vector3.up);
        leftVectorOnFloor.Normalize();
        var rightVectorOnFloor = Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.right, Vector3.up);
        rightVectorOnFloor.Normalize();

        m_stateMachine.IsMovingForward = false;
        m_stateMachine.IsMovingLateral = false;
        m_stateMachine.IsMovingBackward = false;


        if (Input.GetKey(KeyCode.W))
        {
            m_stateMachine.Rb.AddForce(forwardVectorOnFloor * m_stateMachine.AccelerationValue,
                ForceMode.Acceleration);

            m_stateMachine.IsMovingForward = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_stateMachine.Rb.AddForce(backwardVectorOnFloor * m_stateMachine.AccelerationValue,
                ForceMode.Acceleration);

            m_stateMachine.IsMovingBackward = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_stateMachine.Rb.AddForce(leftVectorOnFloor * m_stateMachine.AccelerationValue,
                ForceMode.Acceleration);

            m_stateMachine.IsMovingLateral = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_stateMachine.Rb.AddForce(rightVectorOnFloor * m_stateMachine.AccelerationValue,
                ForceMode.Acceleration);

            m_stateMachine.IsMovingLateral = true;
        }

        //Debug.Log(m_rb.velocity);

        if (m_stateMachine.IsMovingForward)
        {
            m_stateMachine.Rb.velocity = m_stateMachine.Rb.velocity.normalized;
            m_stateMachine.Rb.velocity *= m_stateMachine.ForwardMaxVelocity;
            return;
        }
        if (m_stateMachine.IsMovingLateral)
        {
            m_stateMachine.Rb.velocity = m_stateMachine.Rb.velocity.normalized;
            m_stateMachine.Rb.velocity *= m_stateMachine.LateralMaxVelocity;
            return;
        }
        if (m_stateMachine.IsMovingBackward)
        {
            m_stateMachine.Rb.velocity = m_stateMachine.Rb.velocity.normalized;
            m_stateMachine.Rb.velocity *= m_stateMachine.BackwardMaxVelocity;
        }
        else
        {
            if (m_stateMachine.Rb.velocity.magnitude > 0)
            {
                m_stateMachine.Rb.velocity *= m_stateMachine.SlowingVelocity;
            }
        }

    }

    public override void OnUpdate()
    {
    }

    public override void OnExit()
    {
    }
}
