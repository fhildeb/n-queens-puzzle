using UnityEngine;
using System.Collections;

//@Felix Hildebrandt
//Klasse welche die Kamerarotation ansteuert
public class KameraDrehung : MonoBehaviour
{

    //@Felix Hildebrandt
    //Kontinuierliche Aktualisierung der Kamerarotation
    void Update()
    {
        float speed = 20f;

        //Linke Taste gedrückt
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0f, speed * Time.deltaTime, 0f);

        }

        //Linke Taste gedrückt
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0f, -speed * Time.deltaTime, 0f);

        }
    }
}
