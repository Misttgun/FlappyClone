using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour
{

    public static BirdScript instance;

    [SerializeField]
    private Rigidbody2D _rigidBody;
    [SerializeField]
    private Animator _animator;

    private Button _fapButton;

    private float _forwardSpeed = 3f;
    private float _bounceSpeed = 4f;

    public bool isAlive;
    private bool didFlap;

    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _flapClip, _pointClip, _dieClip;

    public int score;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        // The bird is alive
        isAlive = true;

        // Set the score to zero
        score = 0;

        // Get the flapButton and set on click listener
        _fapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        _fapButton.onClick.AddListener(() => FlapTheBird());

        // Set the camera x postion
        SetCameraX();
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        // If the bird is alive
        if(isAlive)
        {
            // Make him go forward automatically
            Vector2 temp = transform.position;
            temp.x += _forwardSpeed * Time.deltaTime;
            temp.y = Mathf.Clamp(transform.position.y, -4, 5.5f);
            transform.position = temp;

            // If the bird did flap
            if(didFlap)
            {
                // He can flap only once
                didFlap = false;

                // Make the bird go up and play animation
                _rigidBody.velocity = new Vector2(0, _bounceSpeed);
                _audioSource.PlayOneShot(_flapClip);
                _animator.SetTrigger("Flap");
            }

            // If the bird is going up, do nothing
            if(_rigidBody.velocity.y >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            // If the bird is faling, make him fall head first by applying rotation on the z axis
            else
            {
                float angle = 0;
                angle = Mathf.Lerp(0, -60, -_rigidBody.velocity.y / 7);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    /// <summary>
    /// Method which set the camera x position
    /// </summary>
    private void SetCameraX()
    {
        CameraScript.offsetX = (Camera.main.transform.position.x + transform.position.x);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Pipe"))
        {
            if(isAlive)
            {
                isAlive = false;
                _animator.SetTrigger("Bird Died");
                _audioSource.PlayOneShot(_dieClip);
                GameplayController.instance.GameOverShowScore(score);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PipeHolder"))
        {
            score++;
            GameplayController.instance.SetScore(score);
            _audioSource.PlayOneShot(_pointClip);
        }
    }

    /// <summary>
    /// Method which get the bird postion on the X axis
    /// </summary>
    /// <returns>the bird transform position X</returns>
    public float GetPositionX()
    {
        return transform.position.x;
    }

    /// <summary>
    /// Method which make the bird flap. Set didFlap to true.
    /// </summary>
    public void FlapTheBird()
    {
        didFlap = true;
    }
}
