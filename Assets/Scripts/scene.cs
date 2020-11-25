using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scene : MonoBehaviour
{

    public GameObject jugador;
    public Camera camara;
    public static int scorePlus = 0;
    public GameObject[] bloques;
    public GameObject[] fondos;
    public float puntero;
    public float generacion = 12;
    public int scoreGuardado = 0;
    public TMPro.TextMeshProUGUI score;
    public TMPro.TextMeshProUGUI cartelDerrota;
    public bool derrota;

    // Start is called before the first frame update
    void Start()
    {
        puntero = -7;
        derrota = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(jugador != null)
        {
            
            scoreGuardado = System.Convert.ToInt32(Mathf.Floor(jugador.transform.position.x - 10));
            camara.transform.position = new Vector3(
                jugador.transform.position.x,
                camara.transform.position.y,
                camara.transform.position.z
            );
            score.text = "score: " + (scoreGuardado + scorePlus);
        }
        else if(!derrota) {
            derrota = true;
            score.text = "PERDISTE EL JUEGO. PULSA ENTER PARA REINICIAR";
            score.text = "";
            cartelDerrota.text = $"PERDISTE!!!\nTU SCORE FUE DE {scoreGuardado}\n\nPULSA ENTER PARA REINICIAR...";
        }
        if(derrota)
        { 
            if (Input.GetKeyDown("return") || Input.GetKeyDown("enter"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        while (jugador != null && puntero < jugador.transform.position.x + generacion)
        {
            int indice = Random.Range(0, bloques.Length - 1);
            if (puntero < 0)
            {
                indice = bloques.Length -1; 
            }
            GameObject objeto = Instantiate(bloques[indice]);
            objeto.transform.SetParent(this.transform);
            block bloqueO = objeto.GetComponent<block>();
            objeto.transform.position = new Vector2(
                puntero + bloqueO.tamaño / 2,
                0);
            puntero += bloqueO.tamaño;
        }

    }

}
