using UnityEngine;


public class JumpState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.2f;
    private float m_currentStateTimer = 0.0f;

    
    public override void OnEnter()
    {
        Debug.Log("Entering JumpState");

        m_stateMachine.Rb.AddForce(Vector3.up * m_stateMachine.JumpAccelerationValue,
                ForceMode.Acceleration);

        m_currentStateTimer = STATE_EXIT_TIMER;
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
    }

    public override void OnExit()
    {
        Debug.Log("Exiting JumpState");
    }

    public override bool CanEnter()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    public override bool CanExit()
    {
        return m_currentStateTimer <= 0;
    }

}
