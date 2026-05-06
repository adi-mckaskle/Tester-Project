using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Globally accessible reference to the single active GameManager.
    // Readable everywhere, but only this class can set it.
    public static GameManager Instance { get; private set; }


    //DontDestroyOnLoad(gameObject);
}
