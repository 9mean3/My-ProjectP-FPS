using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectStage : MonoBehaviour
{
    [SerializeField] string scenename;

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene(scenename);
    }
}
