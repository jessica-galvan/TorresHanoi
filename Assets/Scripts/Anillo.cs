using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anillo : MonoBehaviour
{
    [SerializeField] private int _size;

    public int Size { get {return _size;} private set {; } }
}
