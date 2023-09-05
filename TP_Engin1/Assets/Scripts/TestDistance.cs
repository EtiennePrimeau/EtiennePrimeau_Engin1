using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDistance : MonoBehaviour
{
    //Vector2 m_currentPosAroundObject = Vector2.zero;
    //float m_currentAngle = -270.0f;

    [SerializeField] private Transform m_objectToLookAt;
    [SerializeField] private float m_rotationSpeed = 0.0f;
    [SerializeField] private float m_scrollSpeed = 0.0f;

    //private float m_elapsedTime = 0.0f;
    [SerializeField] private float m_desiredDuration = 5.0f;
    private Vector3 targetPos;
    private float lerpedAngleX = 0.0f;
    private float lerpedAngleY = 0.0f;

    [SerializeField] private Vector2 m_clampingXRotationValues = Vector2.zero;
    [SerializeField] private Vector2 m_clampingCameraDistance = Vector2.zero;
    //[SerializeField] private Vector3 m_cameraOffset = Vector3.zero;

    private void Awake()
    {
        targetPos = transform.position;
    }

    private void FixedUpdate()
    {
        //MoveCameraInFrontOfObstructionsFUpdate();
    }

    void Update()
    {

        UpdateHorizontalMovement();
        UpdateVerticalMovement();
        //UpdateCameraDistance();
        //LerpedCameraZ();
    }


    private void UpdateHorizontalMovement()
    {
        //Version réactive
        //float currentAngleX = Input.GetAxis("Mouse X") * m_rotationSpeed;
        //transform.RotateAround(m_objectToLookAt.position, m_objectToLookAt.up, currentAngleX);


        //Version lerped
        float currentAngleX = Input.GetAxis("Mouse X") * m_rotationSpeed;
        lerpedAngleX = Mathf.Lerp(lerpedAngleX, currentAngleX, 0.1f);

        transform.RotateAround(m_objectToLookAt.position, m_objectToLookAt.up, lerpedAngleX);
    }

    private void UpdateVerticalMovement()
    {
        //Différence de l'angle à chaque frame
        float currentAngleY = Input.GetAxis("Mouse Y") * m_rotationSpeed;
        //Valeur de mon transform
        var xRotationValue = transform.rotation.eulerAngles.x;
        //Résultat de ma rotation + différence
        float comparisonAngle = xRotationValue + currentAngleY;

        //Debug.Log(comparisonAngle);
        //S'assure que l'angle n'est pas converti à 360 lorsqu'il atteint 0 (0 étant à l'horizontal)
        //Permet d'avoir une limite du bas en valeur négative
        if (comparisonAngle > 180)
        {
            comparisonAngle -= 360;
        }
        //Early return si les valeurs de l'angle sortent de mon min,max (clamp)
        if ((currentAngleY < 0 && comparisonAngle < m_clampingXRotationValues.x) ||
            (currentAngleY > 0 && comparisonAngle > m_clampingXRotationValues.y))
        {
            return;
        }

        //Version lerped    //changer rotateAround aussi
        lerpedAngleY = Mathf.Lerp(lerpedAngleY, currentAngleY, 0.1f);

        transform.RotateAround(m_objectToLookAt.position, transform.right, lerpedAngleY);
    }

    private void UpdateCameraDistance()
    {

        float mouseScrollInput = Input.mouseScrollDelta.y * m_scrollSpeed;
        Vector3 cameraDistanceFromPlayer = new Vector3(0, 0, mouseScrollInput);

        //Pour clamper
        //calculer distance entre camera et joueur avec Vector3.distance
        float distanceCameraPlayer = Vector3.Distance(transform.position, m_objectToLookAt.position);
        //Debug.Log(distanceCameraPlayer);
        if ((mouseScrollInput > 0 && distanceCameraPlayer < m_clampingCameraDistance.x) ||
            (mouseScrollInput < 0 && distanceCameraPlayer > m_clampingCameraDistance.y))
        {
            return;
        }
        transform.Translate(cameraDistanceFromPlayer);

    }

    private void LerpedCameraZ()
    {
        float m_elapsedTime = 0.0f;

        float mouseScrollInput = Input.mouseScrollDelta.y * m_scrollSpeed;
        targetPos += new Vector3(0, 0, mouseScrollInput);

        m_elapsedTime += Time.deltaTime;
        float percentageComplete = m_elapsedTime / m_desiredDuration;


        transform.position = Vector3.Lerp(transform.position, targetPos, percentageComplete);

        //Marche pas avec les autres lerp 
        //Faudrait soit pouvoir utiliser translate
        //isoler Z et mathf.lerp comme les 2 autres
        ////////
        ///ou bien faire transform.position = Vector3.Lerp partout
        //
    }

    private void MoveCameraInFrontOfObstructionsFUpdate()
    {
        int layerMask = 1 << 8;
        RaycastHit hit;

        Vector3 vDiff = transform.position - m_objectToLookAt.position;
        float distance = vDiff.magnitude;

        if (Physics.Raycast(m_objectToLookAt.position, vDiff, out hit, distance, layerMask))
        {
            //Objet détecté
            Debug.DrawRay(m_objectToLookAt.position, vDiff.normalized * hit.distance, Color.yellow);

            transform.SetPositionAndRotation(hit.point, transform.rotation);
        }
        else
        {
            //Objet non détecté
            Debug.DrawRay(m_objectToLookAt.position, vDiff, Color.white);
        }
    }
}
