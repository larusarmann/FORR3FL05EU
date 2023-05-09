using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Hluturinn sem að myndavélin eltir
    public Transform player;

    // hraðinn sem myndavélin hreyfist til að elta playerinn
    public float smoothSpeed = 0.125f;


    // hvar playerinn er í myndavélinni
    public Vector3 offset;

    void FixedUpdate()
    {
        // reiknar hvar myndavélin á að vera með þvi að bæta offset á staðsetningu playersins
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        //setur staðsetninguna á myndavélinni að smoothed position
        transform.position = smoothedPosition;
    }
}
