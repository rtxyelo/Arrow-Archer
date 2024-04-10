using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArrowController : MonoBehaviour
{
    public UnityEvent<int> InTarget;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            string tag = collision.gameObject.tag;
            TagToPrize(tag);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        InTarget.RemoveAllListeners();
    }

    private void TagToPrize(string tag)
    {
        switch (tag)
        {
            case "Circle_1":
                InTarget.Invoke(100);
                break;

            case "Circle_2":
                InTarget.Invoke(70);
                break;

            case "Circle_3":
                InTarget.Invoke(50);
                break;

            case "Circle_4":
                InTarget.Invoke(20);
                break;

            case "Circle_5":
                InTarget.Invoke(10);
                break;

            default:
                InTarget.Invoke(0);
                break;
        }
    }
}
