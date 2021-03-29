using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button button_1;
    [SerializeField] private Button button_2;
    [SerializeField] private Button button_3;
    [SerializeField] private Anillo[] anillos = new Anillo[5];

    [SerializeField] private TDA_P torre1;
    [SerializeField] private TDA_P torre2;
    [SerializeField] private TDA_P torre3;

    [SerializeField] private Text contadorTexto;
    [SerializeField] private Menu menu;

    [SerializeField] private AudioSource errorClip;

    private int cantidadAnillos;
    private TDA_P torreInicial = null;
    private TDA_P torreDestino = null;
    private Button currentButton; 
    private int movement;
    private bool segundaFase;


    void Start()
    {
        //Sacamos la cantidad maxima de anillos del array de anillos
        cantidadAnillos = anillos.Length;

        //Inicializamos las tres torres
        torre1.InicializarPila(cantidadAnillos);
        torre2.InicializarPila(cantidadAnillos);
        torre3.InicializarPila(cantidadAnillos);

        //Asignamos a la primera torre todos los anillos
        for (int i = 0; i < cantidadAnillos; i++)
        {
            torre1.Apilar(anillos[i]);
        }

        //Expresion LAMBDA
        button_1.onClick.AddListener(() => { Seleccionar(torre1); });
        button_2.onClick.AddListener(() => { Seleccionar(torre2); });
        button_3.onClick.AddListener(() => { Seleccionar(torre3); });

        CheckButtons();
    }

    private void Seleccionar(TDA_P torre)
    {
        currentButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        //Si la torre inicial esta vacia, guardamos en la variable que torre es
        if (torreInicial == null)
        {
            torreInicial = torre;
            segundaFase = true;
        } else //si no esta vacia, entonces toca la torre destino y luego limpiamos los dos
        {
            torreDestino = torre;
            Mover();
            torreInicial = null;
            torreDestino = null;
            currentButton = null;
            CheckWin();
        }
        CheckButtons();
    }

    private void Mover()
    {
        if (torreInicial.PilaVacia()) return; //Si la pila esta vacia, no moves nada

        //Si el anillo que voy a mover es menor al anillo que ya hay adentro  ó la pila esta vacia
        if (torreDestino.PilaVacia() || (torreDestino.Tope().Size > torreInicial.Tope().Size)) 
        {
            torreDestino.Apilar(torreInicial.Tope());
            torreInicial.Desapilar();
            Graficar();
            Contador();
        } else
        {
            errorClip.Play();
        }
    }

    private void CheckWin()
    {
        if(torre3.Indice() == cantidadAnillos)
        {
            menu.Winner();
        }
    }

    private void CheckButtons()
    {
        if (torreInicial == null)
        {
            if (currentButton != null)
                currentButton.interactable = true;

            button_1.interactable = torre1.PilaVacia() ? false : true;
            button_2.interactable = torre2.PilaVacia() ? false : true;
            button_3.interactable = torre3.PilaVacia() ? false : true;
        }
        else
        {
            button_1.interactable = true;
            button_2.interactable = true;
            button_3.interactable = true;
            if (currentButton != null)
                currentButton.interactable = false;
        }
    }

    private void Graficar()
    {
        RectTransform recAnillo = torreDestino.Tope().GetComponent<RectTransform>();
        RectTransform recTorreDestino = torreDestino.GetComponent<RectTransform>();
        float altura = 0f;

        switch (torreDestino.Indice())
        {
            case 1:
                altura = -375.7f;
                break;
            case 2:
                altura = -353.3f;
                break;
            case 3:
                altura = -331.1f;
                break;
            case 4:
                altura = -310.3f;
                break;
            case 5:
                altura = -288.9f;
                break;
        }

        recAnillo.anchoredPosition = new Vector3(recTorreDestino.anchoredPosition.x, altura, 0);
    }

    private void Contador()
    {
        movement++;
        contadorTexto.text = movement.ToString();
    }
}
