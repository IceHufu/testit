using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ore : MonoBehaviour
{
    // Start is called before the first frame update
    public int myid;
    public gamemanager gamemanager;
    public int inprivate;
    public Animator animator;
    
    public void setup()
    {
        animator.SetBool("uped", true);
    }
}
