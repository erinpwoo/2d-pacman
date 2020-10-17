using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pacman : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Vector2 direction = Vector2.zero; // Pacman continues moving in direction of last input

    public int score;
    public Text scoreText;

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
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.I)) {
            direction = Vector2.up;
            transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        } else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.M))
        {
            direction = Vector2.down;
            transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
            transform.localRotation = Quaternion.Euler(0, 0, 270);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.J))
        {
            direction = Vector2.left;
            transform.localScale = new Vector3(-2.9f, 2.9f, 2.9f);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.K))
        {
            direction = Vector2.right;
            transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (score < 100)
        {
            scoreText.text = score.ToString("d2");
        } else
        {
            scoreText.text = score.ToString();
        }
        
    }

    void Move()
    {
        transform.localPosition += (Vector3) (direction * speed * Time.deltaTime);
    }
}
