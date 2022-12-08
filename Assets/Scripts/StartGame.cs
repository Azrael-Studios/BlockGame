using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager.Run();
    }
}
