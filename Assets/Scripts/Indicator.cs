using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public GameObject target;
    public GameObject indicator;

    Renderer rd;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //tracking player - not working
        if (rd.isVisible == false) //if player is outside of camera
        {
            Debug.Log("out of frame");
            if (indicator.activeSelf == false)
            {
                //turn on indicator
                indicator.SetActive(true);
                Debug.Log("indicator on");
            }

            Vector2 direction = target.transform.position - transform.position;

            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction);

            if (ray.collider != null)
            {
                indicator.transform.position = ray.point;
            }
        }
        else
        {
            if (indicator.activeSelf == true)
            {
                indicator.SetActive(false);
                Debug.Log("indicator off");
            }
        }
    }
}
