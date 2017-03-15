using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;

    [SerializeField] private float m_MovePower = 5; // The force added to the ball to move it.
    [SerializeField] private bool m_UseTorque = true; // Whether or not to use torque to move the ball.
    [SerializeField] private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.
    [SerializeField] private float m_JumpPower = 2; // The force added to the ball when it jumps.

    private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
    private Rigidbody rb;
    private int count;
    private bool jump;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    private void Update()
    {
        jump = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Move(movement, jump);
        jump = false;
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString ();
        if (count >= 13)
        {
            winText.text = "You Win!";
        }
    }
    public void Move(Vector3 moveDirection, bool jump)
    {
        if (m_UseTorque)
        {
            // ... add torque around the axis defined by the move direction.
            rb.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * m_MovePower);
        }

        else
        {
            // Otherwise add force in the move direction.
            rb.AddForce(moveDirection * m_MovePower);
        }

        // If on the ground and jump is pressed...
        if (Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength) && jump)
        {
            // ... add force in upwards.
            rb.AddForce(Vector3.up * m_JumpPower, ForceMode.Impulse);
        }
    }
}
