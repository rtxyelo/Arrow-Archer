using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    [SerializeField]
    public float _speed = 5.0f;
    
    [SerializeField]
    public float _topPosition = 4.0f;

    [SerializeField]
    public float _bottomPosition = -4.0f;

    private bool _isMovingUp = true;

    private const float border = 0.3f;

    public bool IsCanMove = true;

    private void Update()
    {
        if (IsCanMove)
        {
            Move();
        }
    }

    private void Move()
    {
        float newPosition = _isMovingUp ? _topPosition : _bottomPosition;

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, newPosition, transform.position.z), _speed * Time.deltaTime);

        if (transform.position.y >= _topPosition - border)
        {
            _isMovingUp = false;
        }
        else if (transform.position.y <= _bottomPosition + border)
        {
            _isMovingUp = true;
        }
    }
}
