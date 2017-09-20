using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teste : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameManager.Instance.ShowTeste();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.Instance.ShowTeste();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("Play");
        }
    }
}
