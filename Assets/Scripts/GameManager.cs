using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int nbPancakes;
    private float yOrigin = -4.5f;
    public GameObject pancake;
    public List<GameObject> pilePancakes;
    public static GameManager instance = null;

    void Start ()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        pilePancakes = new List<GameObject>();

        Debug.Log(MenuManager.value);

		for(int i = 0; i < nbPancakes; i++)
        {
            GameObject newPancake = GameObject.Instantiate(pancake);
            Vector3 position = new Vector3(0, yOrigin + i, 0);
            float newScale = newPancake.transform.localScale.x + i;
            newPancake.transform.position = position;
            newPancake.GetComponent<Pancake>().length = i;
            newPancake.transform.localScale = new Vector3(newScale, newPancake.transform.localScale.y, 0);
            pilePancakes.Add(newPancake);
        }

        pilePancakes = Shuffle(pilePancakes);

        ShowPile();
    }

    private void ShowPile()
    {
        for (int i = 0; i < pilePancakes.Count; i++)
        {
            Vector3 position = new Vector3(0, yOrigin + i, 0);
            pilePancakes[i].transform.position = position;
            pilePancakes[i].GetComponent<Pancake>().positionPile = i;
        }
    }

    public List<GameObject> Shuffle(List<GameObject> list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;

        while(n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            GameObject value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return list;
    }

    public void PancakeWasClicked(int posPancake)
    {
        Debug.Log("Retourne à partir de " + posPancake);
        Debug.Log(pilePancakes[posPancake].GetComponent<Pancake>().IsOk(pilePancakes.Count));
    }

    public void FlipPancakes(int posPancake)
    {
        Queue<GameObject> buffer = new Queue<GameObject>();

        for (int i = pilePancakes.Count - 1; i >= posPancake; i--)
        {
            buffer.Enqueue(pilePancakes[i]);
            pilePancakes.RemoveAt(i);
        }

        while(buffer.Count != 0)
        {
            pilePancakes.Add(buffer.Dequeue());
        }
        ShowPile();
        IsGameOver();
    }

    bool IsGameOver()
    {
        bool test = true;

        for(int i = 0;i<pilePancakes.Count;i++)
        {
   
            test = test && pilePancakes[i].GetComponent<Pancake>().IsOk(pilePancakes.Count);
        }

        if(test)
            Debug.Log("Insert victory sound here");

        return test;
    }

 
	
    void Update()
    {
       
    }

}
