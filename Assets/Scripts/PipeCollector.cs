using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollector : MonoBehaviour {

    private GameObject[] _pipeHolders;
    private float _distance = 3f;
    private float _lastPipeX;
    private float _pipeMin = -2f;
    private float _pipeMax = 2f;

    private void Awake()
    {
        _pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");
        _lastPipeX = _pipeHolders[0].transform.position.x;

        for(int i = 0; i < _pipeHolders.Length; i++)
        {
            Vector2 temp = _pipeHolders[i].transform.position;
            temp.y = Random.Range(_pipeMin, _pipeMax);
            _pipeHolders[i].transform.position = temp;

            if(_lastPipeX < _pipeHolders[i].transform.position.x)
            {
                _lastPipeX = _pipeHolders[i].transform.position.x;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PipeHolder"))
        {
            Vector3 temp = collision.transform.position;

            temp.x = _lastPipeX + _distance;
            temp.y = Random.Range(_pipeMin, _pipeMax);

            collision.transform.position = temp;

            _lastPipeX = temp.x;
        }
    }
}
