using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITDA_Pile
{
    void InicializarPila(int x);
    void Apilar(Anillo x);
    void Desapilar();
    bool PilaVacia();
    Anillo Tope();
}
