using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] float m_movingSpeed = 5.0f;
    [SerializeField] float m_jumpingPower = 300.0f;
    [SerializeField] int m_GravityScale = 10;
    Rigidbody2D m_rb;
    public enum PlayerNumber
    {
        first,
        second
    }
    [SerializeField]
    PlayerNumber m_playerNumber = PlayerNumber.first;
    private bool isGround;
    void Start()
    {
        gameObject.AddComponent<Rigidbody2D>();
        m_rb = GetComponent<Rigidbody2D>();
        m_rb.gravityScale = m_GravityScale;
        m_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        bool left1 = Input.GetKey(KeyCode.A);
        bool right1 = Input.GetKey(KeyCode.D);
        bool up1 = Input.GetKeyDown(KeyCode.W);

        bool left2 = Input.GetKey(KeyCode.LeftArrow);
        bool right2 = Input.GetKey(KeyCode.RightArrow);
        bool up2 = Input.GetKeyDown(KeyCode.UpArrow);

        if (m_playerNumber == PlayerNumber.first)
        {
            Move(left1, right1, up1);
        }
        else if (m_playerNumber == PlayerNumber.second)
        {
            Move(left2, right2, up2);
        }
    }
    private void Move(bool left, bool right, bool up)
    {
        float horizontal1 = 0.0f;
        if (left)
        {
            horizontal1 = -1.0f;
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (right)
        {
            horizontal1 = 1.0f;
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

        Vector2 direction1 = Vector2.right * horizontal1;
        Vector2 velocity1 = direction1 * m_movingSpeed;
        m_rb.velocity = velocity1;

        if (up && isGround)
        {
            m_rb.AddForce(Vector2.up * m_jumpingPower);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}
