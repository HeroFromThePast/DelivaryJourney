using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crecer : MonoBehaviour , IReiniciable //hereda de una interfaz 
{
    [SerializeField] private Eventos eventoRecoger, eventoLleno, eventoGanarCaja,eventoReiniciar, entregaCaja, perderCaja,perderCajaG;
    [SerializeField] private float tama�oMaximo; //es el tama�o maximo de la caja
    [SerializeField] private float ritmoDeCrecimiento; //lo que crece por cada caja recolectada
    [SerializeField] private bool save; //determina si ha pasado por un punto de reinicio o no

    [SerializeField] private ParticleSystem crecer;

    [SerializeField] int contadorP;
    //[SerializeField] BoxScore puntuacion;


    private bool disparoEvento = false; //determina si se ha disparado por lo menos una vez el evento
    public float tama�oActual = 0; //contador de cuantas veces ha crecido mla caja
    public Transform tama�o; //referencia a los parametros de escala del objeto
    public Vector3 crecimiento, tama�oFinal, tama�oInicial; //vectores que determinan distintos tama�os del objeto
    // Start is called before the first frame update
    void Start()
    {
        save = false; //al inicio no pasa por un punto de reinicio
        contadorP = 1;
        crecimiento = new Vector3(ritmoDeCrecimiento, ritmoDeCrecimiento, ritmoDeCrecimiento); //se determina el vector de cada cuanto crece dependiendo del ritmo
        tama�oFinal = new Vector3(tama�oMaximo, tama�oMaximo, tama�oMaximo); //se determina el vector de tama�o final de acuerdo al tama�o maximo
        tama�o = GetComponent<Transform>(); //se obtiene el componente del transform
        tama�oInicial = new Vector3(tama�o.localScale.x,tama�o.localScale.y,tama�o.localScale.z); //se tiene una referencia del tama�o inicial, util para cuando se reinicia el objeto
        eventoRecoger.GEvent += CrecerCaja;  //se subscriben dichas funciones a dichos eventos
        eventoReiniciar.GEvent += Desaparecer;
        //perderCajaG.GEvent += PerderCaja;
    }

    void CrecerCaja()
    {
        //la caja crece mientras no ha llegado a su maximo tama�o
        if (tama�oActual < tama�oMaximo)
        {
            contadorP++;
            crecer.Play();
            tama�oActual = tama�oActual + ritmoDeCrecimiento;
            tama�o.localScale = tama�o.localScale + crecimiento;
        }
        //si ya llega a su tama�o maximo y todavia no ha disparado el evento , dipara los respectivos eventos y se marca que ya se disparo una vez
        else if (tama�oActual >= tama�oMaximo && disparoEvento == false)
        {
            eventoLleno.FireEvent();
            eventoGanarCaja.FireEvent();
            disparoEvento = true;
            //Debug.Log("se lleno la caja");
        }


    }
    //desactiva el objeto si este no ha pasado por la zona de reinicio antes que otro
    void Desaparecer()
    {
        if (save == false)
        {
            tama�o.localScale = tama�oInicial;
            tama�oActual = 0;
            
            this.gameObject.SetActive(false);
            //ganarVel.FireEvent();

        }
        else
        {
            save = false;
        }
    }
   /* void PerderCaja()
    {
        tama�o.localScale = tama�oInicial;
        tama�oActual = 0;

        this.gameObject.SetActive(false);
    }*/
    //verifica que ha pasado el objeto por el punto de inicio y otra vez pone en "0" las veces que ha disparado un evento
    //ademas devuelve el tama�o de la caja a su estado original
    public void Reiniciar()
    {
        save = true;
        disparoEvento = false;
        //Debug.Log("Llego la caja");
        tama�oActual = 0;
        tama�o.localScale = tama�oInicial;
        eventoReiniciar.FireEvent();
        entregaCaja.FireEvent();
    }
    private void OnDestroy()
    {
        eventoRecoger.GEvent -= CrecerCaja;  //se dessubscriben dichas funciones a dichos eventos
        eventoReiniciar.GEvent -= Desaparecer;
        //perderCaja.GEvent -= PerderCaja;
    }
    
}
