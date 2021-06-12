using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool dead;
    public float moveSpeed = 2f;
    public int score;
    public TextMeshProUGUI ScoreText;
    public Button btnPlay;
    public ParticleSystem Emission;
    public string skortutan;
    public Camera mainCamera;
    private Vector2 startPos;
    public static bool isStart;
    public MapGeneration mgenerator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        skortutan = ScoreText.text;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = ("" + score);
        if (isStart)
        {
            MovePlayer();
        }

    }

    public void startToPlay()
    {
        isStart = true;
        mgenerator.init();
    }

    private float _startPosX, _endPosX, _mousePosDeltaX;

    private void MovePlayer()
    {
        if (dead) return;
        if (Input.touchCount > 0)
        {
            Touch tıklama = Input.GetTouch(0);
            if (tıklama.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(mainCamera.ScreenToWorldPoint(tıklama.position).x, transform.position.y, 0);
            }

        }

        /*if (Input.GetMouseButtonDown(0))
        {
            _startPosX = Input.mousePosition.x;
            _mousePosDeltaX = 0;
        }

        else if (Input.GetMouseButton(0))
        {
            _mousePosDeltaX = Input.mousePosition.x - _startPosX;
            _startPosX = Mathf.Lerp(_startPosX, Input.mousePosition.x, 0.05f);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            _startPosX = 0;
            _mousePosDeltaX = 0;
        }

        var playerPos = transform.position;
        var speed = 2f;
        playerPos.x += _mousePosDeltaX * Time.deltaTime * .04f * speed;
        playerPos.x = Mathf.Clamp(playerPos.x, -4f, 4f);
        transform.position = playerPos;
    }*/
    }

    void OnTriggerEnter2D(Collider2D other)
        {
            if (dead)
            {
                return;
            }

            if (other.gameObject.tag == "platform")
            {

                dead = true;
                other.isTrigger = false;
                rb.bodyType = RigidbodyType2D.Dynamic;
                Debug.Log("gg");
                rb.AddForce(Vector2.down * 10);
                rb.AddTorque(100);
                RestartAfterGame();
            }

            if (other.gameObject.tag == "rocketfuel")
            {
                score += 10;
                ScoreText.text = skortutan;
                Destroy(other.gameObject);

            }

            if (other.gameObject.tag == "collidscore")
            {
                score++;
                ScoreText.text = skortutan;
            }


        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "collidscore")
            {
                Destroy(other.gameObject, 0.2f);
            }
        }

        void RestartAfterGame()
        {
            Invoke("RestartGame", 3f);
        }

        void RestartGame()
        {
            isStart = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }

    }

