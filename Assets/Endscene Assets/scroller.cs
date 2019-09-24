using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroller : MonoBehaviour
{
    public int multiplier = 100;
    public int step = 10;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetAxis("Mouse ScrollWheel")!=0)
        //{
        //    if (transform.position.y - (Input.GetAxis("Mouse ScrollWheel") * multiplier) < 1802 && transform.position.y - (Input.GetAxis("Mouse ScrollWheel") * multiplier) > -67)
        //    {
        //        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        //        transform.position= new Vector3(transform.position.x, transform.position.y - (Input.GetAxis("Mouse ScrollWheel") * multiplier), transform.position.z);
        //    }
            
        //}

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (transform.position.y +step < 1802)
                transform.position = new Vector3(transform.position.x, transform.position.y + step, transform.position.z);


        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (transform.position.y -step > -68)
                transform.position = new Vector3(transform.position.x, transform.position.y - step, transform.position.z);
        }
    }
}
