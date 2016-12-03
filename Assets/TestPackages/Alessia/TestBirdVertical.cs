using UnityEngine;
using System.Collections;

public class TestBirdVertical : MonoBehaviour
{

    public float howWideItGoesOnXAxis = 5;
    public float howWideItGoesOnYAxis = 1;
    //public float howWideItGoesOnZAngle = 80.0f;

    Vector3 initialPosition;

    // Use this for initialization
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            var mousePos = Input.mousePosition;
            mousePos.x = mousePos.x / Screen.width - 0.5f;
            mousePos.y = mousePos.y / Screen.height - 0.5f;

            //Debug.Log(mousePos);

            transform.position = new Vector3(
                initialPosition.x + mousePos.x * howWideItGoesOnXAxis,
                initialPosition.y + Mathf.Abs(mousePos.x) * howWideItGoesOnYAxis,
                initialPosition.z);

            transform.eulerAngles = new Vector3(0, 0, mousePos.x);
        }
    }
}
