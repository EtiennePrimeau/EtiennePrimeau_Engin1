using System.Collections.Generic;
using UnityEngine;


// Renommer CharacterControllerSM


public class CharacterController : MonoBehaviour
{
    [field:SerializeField] public Camera Camera { get; private set; }
    [field:SerializeField] public float AccelerationValue { get; private set; } = 10.0f;
    [field:SerializeField] public float ForwardMaxVelocity { get; private set; } = 6.0f;
    [field:SerializeField] public float LateralMaxVelocity { get; private set; } = 4.0f;
    [field:SerializeField] public float BackwardMaxVelocity { get; private set; } = 2.0f;
    [field:SerializeField] public float SlowingVelocity { get; private set; } = 0.97f;
    
    // /////////////////
    public bool IsMovingForward { get; set; } = false; //Meilleure pratique que public set ? 
    public bool IsMovingLateral { get; set; } = false;
    public bool IsMovingBackward { get; set; } = false;
    public Rigidbody Rb { get; private set; }
    
    // /////////////////

    private CharacterState m_currentState;
    private List<CharacterState> m_possibleStates;


    private void Awake()
    {
        m_possibleStates = new List<CharacterState>();
        m_possibleStates.Add(new FreeState());
    }

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody>();

        foreach (CharacterState state in m_possibleStates)
        {
            state.OnStart(this);
        }

        m_currentState = m_possibleStates[0];
        m_currentState.OnEnter();

    }
    void Update()
    {
        m_currentState.OnUpdate();

    }

    private void FixedUpdate()
    {
        m_currentState.OnFixedUpdate();

    }
}
