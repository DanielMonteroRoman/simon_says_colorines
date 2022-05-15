using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balls : MonoBehaviour
{
    public Vector3 finger;

   public bool OnClick = false;

    public int numero;

    // Update is called once per frame
    void Update()
    {

        finger = gameObject.transform.position;

        if(OnClick)
        {
            FollowTheFinger();

           numero++;
            
        }
        
    }

    private void OnMouseDown ()
    {
        OnClick = true;
    }
    private void OnMouseUp()
    {
        OnClick = false;
    }



    void FollowTheFinger()
    {
              
       gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(
       Input.mousePosition.x,
       Input.mousePosition.y,
       -Camera.main.transform.position.z));
        
        
    }



    
}
