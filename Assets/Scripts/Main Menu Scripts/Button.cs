using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public int ID;

    public void OnMouseEnter()
    {
        print("yes");
        MenuManager.GetInstance().UpdatePosition(ID);
    }
}
