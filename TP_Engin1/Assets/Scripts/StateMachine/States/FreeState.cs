using UnityEngine;



public class FreeState : CharacterState
{
    private bool m_isMovingForward = false;
    private bool m_isMovingLateral = false;
    private bool m_isMovingBackward = false;

    public override void OnEnter()
    {
        Debug.Log("Entering FreeState");
    }

    public override void OnFixedUpdate()
    {
        AddForceFromInputs();

        CapMaximumSpeed();

        //Debug.Log(m_stateMachine.Rb.velocity.magnitude);
    }

    private void AddForceFromInputs()
    {
        float inputForward = 0.0f;
        float inputRight = 0.0f;

        m_isMovingForward = false;
        m_isMovingLateral = false;
        m_isMovingBackward = false;


        if (Input.GetKey(KeyCode.W))
        {
            inputForward += 1;

            m_isMovingForward = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputForward -= 1;

            m_isMovingBackward = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputRight -= 1;

            m_isMovingLateral = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputRight += 1;

            m_isMovingLateral = true;
        }

        m_stateMachine.Rb.AddForce(inputForward * m_stateMachine.ForwardVectorForPlayer * m_stateMachine.AccelerationValue,
                ForceMode.Acceleration);
        m_stateMachine.Rb.AddForce(inputRight * m_stateMachine.RightVectorForPlayer * m_stateMachine.AccelerationValue,
                ForceMode.Acceleration);

        //Debug.Log(m_stateMachine.Rb.velocity);
    }

    private void CapMaximumSpeed()
    {
        if (m_isMovingForward)
        {
            if (m_isMovingLateral)
            {
                float diagonalMaxVelocity = (m_stateMachine.ForwardMaxVelocity + m_stateMachine.LateralMaxVelocity) / 2;
                
                if (m_stateMachine.Rb.velocity.magnitude < diagonalMaxVelocity)
                {
                    return;
                }
                m_stateMachine.Rb.velocity = Vector3.Normalize(m_stateMachine.Rb.velocity);
                m_stateMachine.Rb.velocity *= diagonalMaxVelocity;
                return;
            }
            if (m_stateMachine.Rb.velocity.magnitude < m_stateMachine.ForwardMaxVelocity)
            {
                return;
            }
            
            m_stateMachine.Rb.velocity = Vector3.Normalize(m_stateMachine.Rb.velocity);
            m_stateMachine.Rb.velocity *= m_stateMachine.ForwardMaxVelocity;
            return;
        }
        if (m_isMovingLateral)
        {
            if (m_isMovingBackward)
            {
                float diagonalMaxVelocity = (m_stateMachine.LateralMaxVelocity + m_stateMachine.BackwardMaxVelocity) / 2;

                if (m_stateMachine.Rb.velocity.magnitude < diagonalMaxVelocity)
                {
                    return;
                }
                m_stateMachine.Rb.velocity = Vector3.Normalize(m_stateMachine.Rb.velocity);
                m_stateMachine.Rb.velocity *= diagonalMaxVelocity;
                return;
            }
            if (m_stateMachine.Rb.velocity.magnitude < m_stateMachine.LateralMaxVelocity)
            {
                return;
            }
            m_stateMachine.Rb.velocity = Vector3.Normalize(m_stateMachine.Rb.velocity);
            m_stateMachine.Rb.velocity *= m_stateMachine.LateralMaxVelocity;
            return;
        }
        if (m_isMovingBackward)
        {
            if (m_stateMachine.Rb.velocity.magnitude < m_stateMachine.BackwardMaxVelocity)
            {
                return;
            }
            m_stateMachine.Rb.velocity = Vector3.Normalize(m_stateMachine.Rb.velocity);
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
        Debug.Log("Exiting FreeState");
    }

    public override bool CanEnter()
    {
        return m_stateMachine.IsInContactWithFloor();
    }
    public override bool CanExit()
    {
        return true;
    }

}
