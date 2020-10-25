using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pacman : MonoBehaviour
{
    public float speed = 2f;
    private Rigidbody2D rb;
    public int score;
    public Text scoreText;
    public Transform movePoint;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.position = new Vector2(0, -2);
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
            }

        }

        if (score < 100)
        {
            scoreText.text = score.ToString("d2");
        }
        else
        {
            scoreText.text = score.ToString();
        }

        Vector2 curr = transform.position;
        if (curr.x < -3.5f)
        {
            curr.x = 3.25f;
            transform.position = curr;
        }
        if (curr.x > 3.5f)
        {
            curr.x = -3.25f;
            transform.position = curr;
        }

    }
}
