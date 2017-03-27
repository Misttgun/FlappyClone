using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCollector : MonoBehaviour
{

    // Backgrounds and grounds container
    private GameObject[] _backgrounds;
    private GameObject[] _grounds;

    // Last background and ground position on the X axis
    private float _lastBGX;
    private float _lastGroundX;

    private void Awake()
    {
        _backgrounds = GameObject.FindGameObjectsWithTag("Background");
        _grounds = GameObject.FindGameObjectsWithTag("Ground");

        _lastBGX = _backgrounds[0].transform.position.x;
        _lastGroundX = _grounds[0].transform.position.x;

        for(int i = 1; i < _backgrounds.Length; i++)
        {
            if(_lastBGX < _backgrounds[i].transform.position.x)
            {
                _lastBGX = _backgrounds[i].transform.position.x;
            }
        }

        for(int i = 1; i < _grounds.Length; i++)
        {
            if(_lastGroundX < _grounds[i].transform.position.x)
            {
                _lastGroundX = _grounds[i].transform.position.x;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Background"))
        {
            Vector3 temp = collision.transform.position;
            float width = ((BoxCollider2D)collision).size.x;

            temp.x = _lastBGX + width;

            collision.transform.position = temp;

            _lastBGX = temp.x;
        }

        if(collision.CompareTag("Ground"))
        {
            Vector3 temp = collision.transform.position;
            float width = ((BoxCollider2D)collision).size.x;

            temp.x = _lastGroundX + width;

            collision.transform.position = temp;

            _lastGroundX = temp.x;
        }
    }
}
