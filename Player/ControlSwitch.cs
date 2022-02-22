using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSwitch : MonoBehaviour
{
    public GameObject PlayInventoryObj;
    public GameObject PlayCameraObj;
    private bool Switch = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        plySwitch();
    }

    public void plySwitch()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Switch = !Switch;
            PlayInventoryObj.SetActive(!Switch);
            PlayCameraObj.SetActive(Switch);

            if(Switch)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
