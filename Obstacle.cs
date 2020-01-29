using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private int switcher = 1;

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x > 8)
        {
            switcher = -1;
        }else if (this.transform.position.x < -8)
        {
            switcher = 1;
        }
        this.transform.Translate(Vector2.right * switcher * Time.deltaTime);
    }
}
