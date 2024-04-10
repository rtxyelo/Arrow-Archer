using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BowController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_RotationText;

    [SerializeField]
    private Image _bowImage;

    [SerializeField]
    private Sprite _bowSpriteWithArrow;    

    [SerializeField]
    private Sprite _bowSprite;

    [SerializeField]
    private ScoreCounter _scoreCounter;

    private float rotationSpeed = 0.2f;

    private Vector2 lastTouchPosition;
    
    private float arrowSpeed = 20f;

    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private BeatSoundBehaviour _beatSoundBehaviour;

    [SerializeField]
    private TouchDetection _touchDetection;

    private float maxRotationAngle = 20f;
    
    private const float border = 1E-5f;

    private bool _isActive = true;

    public bool IsActive { get => _isActive; set => _isActive = value; }

    void Update()
    {
        if (_isActive && _touchDetection.TouchDetected)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                DetectTouch();
            }

            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Shoot!");

                ShootArrow();
            }
        }
    }

    void ShootArrow()
    {
        _bowImage.sprite = _bowSprite;

        _isActive = false;

        GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation, transform);

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

        ArrowController arrowController = arrow.GetComponent<ArrowController>();

        arrowController.InTarget.AddListener(TargetHit);

        Vector2 shootDirection = transform.right;

        //rb.AddForce(arrowSpeed * shootDirection, ForceMode2D.Impulse);

        rb.velocity = arrowSpeed * shootDirection;
    }

    private void TargetHit(int score)
    {
        Debug.Log("Target is hit by " + score);

        _isActive = true;
        _bowImage.sprite = _bowSpriteWithArrow;

        _scoreCounter.IncreaseScore(score);

        if (score != 0)
            _beatSoundBehaviour.PlayBeatSound();
    }

    private void DetectTouch()
    {
        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

        float rotationZ = -touchDeltaPosition.x * rotationSpeed;

        if (Mathf.Abs(rotationZ) > 7)
        {
            rotationZ = Mathf.Sign(rotationZ) * 7;
        }

        float currentRotation = transform.localRotation.eulerAngles.z;

        if (m_RotationText != null)
            m_RotationText.text = currentRotation.ToString();

        if (currentRotation + rotationZ < 360 - maxRotationAngle && currentRotation + rotationZ > 180)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 360 - maxRotationAngle + border);
            return;
        }

        if (currentRotation + rotationZ > maxRotationAngle && currentRotation + rotationZ < 180)
        {
            transform.localRotation = Quaternion.Euler(0, 0, maxRotationAngle - border);
            return;
        }

        if (currentRotation >= 360 - maxRotationAngle || currentRotation <= maxRotationAngle)
        {
            transform.Rotate(0, 0, rotationZ, Space.Self);
        }
    }
}
