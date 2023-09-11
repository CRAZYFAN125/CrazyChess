using GameJolt.API;
using GameJolt.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameJoltUI.Instance != null)
            GameJoltUI.Instance.ShowSignIn(
              (bool signInSuccess) =>
              {
                  Debug.Log(string.Format("Sign-in {0}", signInSuccess ? "successful" : "failed or user's dismissed the window"));
              },
              (bool userFetchedSuccess) =>
              {
                  Debug.Log(string.Format("User details fetched {0}", userFetchedSuccess ? "successfully" : "failed"));
              });
    }

    public void StartGame()
    {
        if (!GameJoltUI.Instance) return;
        if (GameJoltAPI.Instance.CurrentUser == null)
        {
            GameJoltUI.Instance.ShowSignIn(
              (bool signInSuccess) =>
              {
                  Debug.Log(string.Format("Sign-in {0}", signInSuccess ? "successful" : "failed or user's dismissed the window"));
              },
              (bool userFetchedSuccess) =>
              {
                  Debug.Log(string.Format("User details fetched {0}", userFetchedSuccess ? "successfully" : "failed"));
              });
            return;
        }

        SceneManager.LoadScene(1);
    }
}
