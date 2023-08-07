using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRDirection : MonoBehaviour
{
    Ray CamRay;
    public Camera Mycam;

    // é©êgÇÃTransform
    [SerializeField] private Transform _self;

    private Vector3 target;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        CamRay = Mycam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(CamRay.origin, CamRay.direction, Color.red);
        if (Physics.Raycast(CamRay, out hit))
        {
            mousePos = Input.mousePosition;
            target = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 30));
            _self.LookAt(target);
        }
    }
}
