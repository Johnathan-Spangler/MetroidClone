using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Topher Overbey
 * 11/12/25
 * controls the sending of the player to the you win screen
*/

public class YouWin : MonoBehaviour
{
    public int sceneIndex;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter(Collider other)
    {//Moves myself over to the You Win Screen
        SceneManager.LoadScene(sceneIndex);
    }
}
