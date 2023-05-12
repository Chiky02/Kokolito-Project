using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Define 
{
    public void Downxd(Animator animator)
    {
        animator.SetBool("Reposo", false);
        animator.SetBool("Agachado", true);
    }
    public void NoDonwxd(Animator animator)
    {
        animator.SetBool("Reposo", true);
        animator.SetBool("Agachado", false);
    }

    public void moveY(Animator animator) {

        animator.SetBool("caminar", true);
        animator.SetBool("Reposo", false);


    }

    public void repose(Animator animator)
    {

        animator.SetBool("Reposo",true);


    }
    public void reset(Animator animator)
    {

        animator.SetBool("Reposo", true);
        animator.SetBool("correr", false);
       
        animator.SetBool("caminar", false);
    }
    public void run(Animator animator)
    {
        animator.SetBool("Reposo", false);
        animator.SetBool("correr", true);
        


    }
    public void jumpping(Animator animator)
    {
        animator.SetBool("Reposo", true);
        animator.SetBool("saltar", false);
    }
    public void notJumping(Animator animator)
    {
        animator.SetBool("Reposo", false);
        animator.SetBool("saltar", true);
    }

    public void Aimxd(Animator animator)
    {
        animator.SetBool("Reposo", false);
        animator.SetBool("Apuntando", true);
    }
    public void NoAimxd(Animator animator)
    {
        animator.SetBool("Reposo", true);
        animator.SetBool("Apuntando", false);
    }

}
