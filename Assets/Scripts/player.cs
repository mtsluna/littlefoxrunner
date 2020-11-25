using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public int fuerzaSalto;
    public int velocidad;
    public bool tocandoPiso = false;
    public bool saltando = false;
    public Animator animator;
    public bool muerto = false;
    public int flag = 0;
    public float tiempo = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!muerto)
        {
            if (Input.GetKeyDown("space") && saltando == false)
            {
                animator.SetBool("saltando", true);
                saltando = true;
                this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, fuerzaSalto));
            }
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidad, this.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            animator.Play("PlayerDie");
            if (animator.GetCurrentAnimatorClipInfo(0).Length != 0)
            {
                AnimatorClipInfo[] AuxClipInfo = animator.GetCurrentAnimatorClipInfo(0);
                tiempo = animator.GetCurrentAnimatorStateInfo(0).normalizedTime * AuxClipInfo[0].clip.length;

                if (tiempo > AuxClipInfo[0].clip.length)
                {
                    print(tiempo + " " + AuxClipInfo[0].clip.length);
                    GameObject.Destroy(this.gameObject);
                }
            }
        }       
    }

    private void OnTriggerEnter2D(Collider2D collition)
    {
        animator.SetBool("saltando", false);
        saltando = false;
        if(collition.tag == "skull")
        {
            velocidad = 0;
            muerto = true;            
            //GameObject.Destroy(this.gameObject);
        }
        if (collition.tag == "gem")
        {
            scene.scorePlus += 70;
            Destroy(GameObject.FindWithTag("gem"));
        }
    }

    private void OnTriggerExit2D(Collider2D collition)
    {
        tocandoPiso = false;
    }
}
