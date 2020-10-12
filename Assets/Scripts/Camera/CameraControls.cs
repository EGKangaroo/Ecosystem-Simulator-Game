using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    private Transform m_Transform; //camera tranform
    public bool useFixedUpdate = true; //use FixedUpdate() or Update()

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";

    public float keyboardMovementSpeed = 5f; //speed with keyboard movement

    public bool limitMap = true;
    public Vector2 mapLimit = new Vector2(50f, 50f);

    private Vector2 KeyboardInput
    {
        get { return new Vector2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis)); }
    }

    private void Start()
    {
        m_Transform = transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!useFixedUpdate)
            CameraUpdate();
    }

    private void FixedUpdate()
    {
        if (useFixedUpdate)
            CameraUpdate();
    }

    private void CameraUpdate()
    {
        Move();
        LimitPosition();
    }

    private void Move()
    {
        Vector3 desiredMove = new Vector3(KeyboardInput.x, 0, KeyboardInput.y);

        desiredMove *= keyboardMovementSpeed;
        desiredMove *= Time.deltaTime;
        desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
        desiredMove = m_Transform.InverseTransformDirection(desiredMove);

        m_Transform.Translate(desiredMove, Space.Self);
    }

    private void LimitPosition()
    {
        if (!limitMap)
            return;

        m_Transform.position = new Vector3(Mathf.Clamp(m_Transform.position.x, -mapLimit.x, mapLimit.x),
            m_Transform.position.y,
            Mathf.Clamp(m_Transform.position.z, -mapLimit.y, mapLimit.y));
    }
}
