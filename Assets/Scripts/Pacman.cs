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
        } else if (Input.GetKey(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        } else if (Input.GetKey(KeyCode.LeftArrow))
        {
           direction = Vector2.left;
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }
        Vector2 dir = direction - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", direction.x);
        GetComponent<Animator>().SetFloat("DirY", direction.y);
    }

    void Move()
    {
        transform.localPosition += (Vector3) (direction * speed * Time.deltaTime);
    }
}
