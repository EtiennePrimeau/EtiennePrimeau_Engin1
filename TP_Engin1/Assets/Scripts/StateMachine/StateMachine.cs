using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState m_currentState;
    private List<IState> m_possibleStates;

    private void Start()
    {
        m_currentState = m_possibleStates[0];
        m_currentState.OnEnter();
    }

    private void Update()
    {
        m_currentState.OnUpdate();
    }

    private void FixedUpdate()
    {
        m_currentState.OnFixedUpdate();
    }
}
