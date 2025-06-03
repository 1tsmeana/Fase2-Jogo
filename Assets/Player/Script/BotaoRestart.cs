using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoRestart : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("FaseOnibus");
    }
}
