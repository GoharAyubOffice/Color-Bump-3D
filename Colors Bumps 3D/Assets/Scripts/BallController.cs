using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallController : MonoBehaviour
{
    [SerializeField] private float thrust = 250f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float wallDistance = 5f;
    [SerializeField] private float minCamDisatance = 3f;
    private Vector2 LastMousePos;
    public Rigidbody camerarb;
    public float cameraSpeed;
    public AudioSource soundsource;
    private void Start()
    {
        soundsource = GetComponent<AudioSource>();
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        Vector2 deltaPos = Vector2.zero;
        if (Input.GetMouseButton(0))
        {
            Vector2 CurrentMousePos = Input.mousePosition;
            if (LastMousePos == Vector2.zero)
                LastMousePos = CurrentMousePos;
            deltaPos = CurrentMousePos - LastMousePos;
            LastMousePos = CurrentMousePos;
            Vector3 force = new Vector3(deltaPos.x, 0, deltaPos.y) * thrust;
            rb.AddForce(force);
        }
        else
        {
            LastMousePos = Vector2.zero;
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.singeleton.GameEnded)
            return;
        if(GameManager.singeleton.GameStarted)
            rb.MovePosition(transform.position + Vector3.forward * 4 * Time.fixedDeltaTime);

        cameraMovement();
    }
    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        if (transform.position.x < -wallDistance)
        {
            pos.x = -wallDistance;
        }
        else if (transform.position.x > wallDistance)
        {
            pos.x = wallDistance;
        }
        if (transform.position.z < Camera.main.transform.position.z + minCamDisatance)
        {
            pos.z = Camera.main.transform.position.z + minCamDisatance;
        }
        transform.position = pos;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.singeleton.GameEnded)
            return;

        if (collision.gameObject.tag == "Death")
           GameManager.singeleton.EndGame(false);
           soundsource.Play();
    }

    void cameraMovement()
    {
        if (Input.GetMouseButton(0))
        {
            camerarb.velocity = Vector3.forward * cameraSpeed;
        }
    }
}