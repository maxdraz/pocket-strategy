using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction { UP, DOWN, LEFT, RIGHT };

public class PlayerGridMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    private float gridSize = 1f;
    float counter;

    public Direction playerDirection = Direction.RIGHT;

    public Direction PlayerDirection
    { 
            get
        {
            return playerDirection;
        }
    }

    private enum Orientation
    {
        Horizontal,
        Vertical
    };

    private Orientation gridOrientation = Orientation.Horizontal;
    private bool allowDiagonals = false;
    private bool correctDiagonalSpeed = true;
    private Vector2 input;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;
    private Vector2 movement;

    public Animator anim;

    public void Awake()
    {
        counter = 0f;
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (!isMoving)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (!allowDiagonals)
            {
                if (Mathf.Abs(input.x) >= Mathf.Abs(input.y))
                {
                    input.y = 0;
                }
                else
                {
                    input.x = 0;
                }
            }

            if (input != Vector2.zero)
            {
                StartCoroutine(move(transform));
            }
        }


    }

    void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(inputX, inputY);

        if (counter <= 0)
        {
            if (movement.x == -1)
            {
                anim.SetTrigger("Left");
                playerDirection = Direction.LEFT;
            }

            if (movement.x == 1)
            {
                anim.SetTrigger("Right");
                playerDirection = Direction.RIGHT;
            }


            if (movement.y == 1)
            {
                anim.SetTrigger("Up");
                playerDirection = Direction.UP;
            }


            if (movement.y == -1)
            {
                anim.SetTrigger("Down");
                playerDirection = Direction.DOWN;
            }

            counter = 0;

            counter -= Time.deltaTime;

            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }
    }


    public IEnumerator move(Transform transform)
    {
        isMoving = true;
        startPosition = transform.position;
        t = 0;

        if (gridOrientation == Orientation.Horizontal)
        {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        }


        if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0)
        {
            factor = 0.7071f;
        }
        else
        {
            factor = 1f;
        }

        while (t < 1f)
        {
            t += Time.deltaTime * (moveSpeed / gridSize) * factor;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }


        isMoving = false;
        yield return 0;
    }

}