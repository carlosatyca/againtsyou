using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public float platformSpeed;
    public int initialPoint;
    public Transform[] points;
    private int index;

    private void Start()
    {
        transform.position = points[initialPoint].position;
    }
    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, points[index].position) < 0.02)
        {
            index++;
            if(index == points.Length)
            {
                index = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[index].position, platformSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
