using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 direction = Vector2.zero; // Pacman continues moving in direction of last input

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.position = new Vector2(0, -2);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKey(KeyCode.UpArrow)) {
            direction = Vector2.up;
            transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            direction = Vector2.down;
            transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
            transform.localRotation = Quaternion.Euler(0, 0, 270);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
           direction = Vector2.left;
           transform.localScale = new Vector3(-2.9f, 2.9f, 2.9f);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = Vector2.right;
            transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void Move()
    {
        transform.localPosition += (Vector3) (direction * speed * Time.deltaTime);
    }
}
