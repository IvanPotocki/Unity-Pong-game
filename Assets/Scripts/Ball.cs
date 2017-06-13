using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ball : MonoBehaviour {

    // Balls default speed
    public float speed = 30;

    // Balls Rigidbody 2D component
    private Rigidbody2D rigidBody;

    //To play sound effects
    private AudioSource audioSource;

    // Use this for initialization
	void Start () {

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.right * speed;
	}
	
	void OnCollisionEnter2D(Collision2D col)
    {
	    // LeftPaddle or RightPaddle
        if ((col.gameObject.name == "LeftPaddle") ||
            (col.gameObject.name == "RightPaddle"))
        {
            HandlePaddleHit(col);
        }

        // WallTop or WallBottom
        if ((col.gameObject.name == "WallTop") ||
           (col.gameObject.name == "WallBottom"))
        {
            // Play Sound effect
            SoundManager.Instance.PlayOneShot
                (SoundManager.Instance.wallBloop);
        }

        // LeftGoal or RightGoal
        if ((col.gameObject.name == "LeftGoal") ||
           (col.gameObject.name == "RightGoal"))
        {
            SoundManager.Instance.PlayOneShot
                (SoundManager.Instance.goalBloop);

            //TODO score UI
            if (col.gameObject.name == "LeftGoal")
            {
                increaseTextUIScore("RightScoreUI");
            }
            else if (col.gameObject.name == "RightGoal")
            {
                increaseTextUIScore("LeftScoreUI");
            }

            // Move the ball to the center of the screen
            transform.position = new Vector2(0, 0);
        }
    }

    void HandlePaddleHit(Collision2D col)
    {
        float y = BallHitPaddleWhere(transform.position,
            col.transform.position,
            col.collider.bounds.size.y);

        Vector2 dir = new Vector2();

        // idi lijevo ili desno (ovisi koji paddle je pogođen)
        if (col.gameObject.name == "LeftPaddle")
        {
            dir = new Vector2(1, y).normalized;
        }

        if (col.gameObject.name == "RightPaddle")
        {
            dir = new Vector2(-1, y).normalized;
        }

        rigidBody.velocity = dir * speed;

        SoundManager.Instance.PlayOneShot
            (SoundManager.Instance.hitPaddleBloop);
    }

    float BallHitPaddleWhere(Vector2 ball, Vector2 paddle, float paddleHeight)
    {
        return (ball.y - paddle.y) / paddleHeight;
    }

    void increaseTextUIScore(string textUIName)
    {

        // Find the matching text UI component
        var textUIComp = GameObject.Find(textUIName)
            .GetComponent<Text>();

        // Get the string stored in it and convert to an int
        int score = int.Parse(textUIComp.text);

        // Increment the score
        score++;

        // Convert the score to a string and update the UI
        textUIComp.text = score.ToString();
    }
}
