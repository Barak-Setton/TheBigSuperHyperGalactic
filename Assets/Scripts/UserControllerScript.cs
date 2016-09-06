using UnityEngine;
using System.Collections;

public class UserControllerScript : MonoBehaviour {

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<ThrusterController>().Move(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
    }
}
