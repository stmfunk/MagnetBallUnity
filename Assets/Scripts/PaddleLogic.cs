using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleLogic : MonoBehaviour
{
    [SerializeField] float screenUnits = 16f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(Input.mousePosition.x / Screen.width * screenUnits, 0.65f, 15.35f), transform.position.y);
    }
}
