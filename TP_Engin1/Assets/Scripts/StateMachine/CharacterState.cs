using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : IState
{

    protected CharacterController m_stateMachine;
    public void OnStart(CharacterController controller)
    {
        m_stateMachine = controller;
    }

    public virtual void OnEnter()
    {
    }

    public virtual void OnFixedUpdate()
    {
    }

    public virtual void OnUpdate()
    {
    }

    public virtual void OnExit()
    {
    }

}
