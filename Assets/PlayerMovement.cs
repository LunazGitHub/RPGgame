using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerMovement : MonoBehaviour
{

    public LayerMask movementMask;
    public Interactable focus;

    PlayerGUI playergui;
    PlayerMotor motor;
    Camera cam;

    private void Start()
    {
        playergui = GetComponent<PlayerGUI>();
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
      
        if(Input.GetMouseButtonDown(0))
        {
           
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 40, movementMask))
            {
                motor.MoveToPoint(hit.point);

                RemoveFocus();
            }
        

        }

        if (Input.GetMouseButtonDown(2))
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 150))
            {
                Interactable interactalbe = hit.collider.GetComponent<Interactable>();
                

                Debug.Log("We hit" + hit.collider.name);

                if (interactalbe != null)
                {
                    SetFocus(interactalbe);
                }
            }
        }

    }

    void SetFocus(Interactable newFocus)
    {
         focus = newFocus;
         motor.FollowTarget(newFocus);
       
    }

    void RemoveFocus()
    {
        focus = null;
        motor.StopFollowingTarget();
    }
 
}
