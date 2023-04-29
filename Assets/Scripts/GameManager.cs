using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player;

    //∏∂¿ª ∏  ∏∏µÈ Ω√, ΩÃ±€≈Ê ∞Ì∑¡

    private void Awake()
    {
        Instance = this;
    }
}
