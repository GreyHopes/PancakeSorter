using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pancake : MonoBehaviour
{
    public int length;
    public int positionPile;
    public Pancake(Pancake p)
    {
        length = p.length;
    }

    void OnMouseDown()
    {
        GameManager.instance.FlipPancakes(positionPile);
    }

    public bool IsOk(int totalPancakes)
    {
        bool test = positionPile + length == totalPancakes - 1;
        return test;
    }
}
