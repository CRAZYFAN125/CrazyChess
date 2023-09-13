using GameJolt.API;
using GameJolt.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSelectionMenu : MonoBehaviour
{
    private int UserID;
    private PlayerDataHandler PlayerData;
    public Button prefab;
    public TextMeshPro machID;
    // Start is called before the first frame update
    void Start()
    {
        #region Seting up player data
        UserID = GameJoltAPI.Instance.CurrentUser.ID;
        DataStore.Get($"User {UserID}", false, (string value) =>
        {
            if (string.IsNullOrEmpty(value))
                PlayerData = ConvertData<PlayerDataHandler>(value) as PlayerDataHandler;
            else
            {
                GameJoltUI.Instance.QueueNotification("Couldn't find your player data in cloud!");
                DataStore.Set($"User {UserID}", ConvertData(new PlayerDataHandler()), false, (bool value) =>
                {
                    if (value)
                    {
                        GameJoltUI.Instance.QueueNotification("Succesfully made you a new data");
                    }
                    else
                    {
                        GameJoltUI.Instance.QueueNotification("Sorry, something went wrong");
                    }
                });
            }
        });
        #endregion
        if (PlayerData.battles.Count > 0)
        {
            for (int i = 0; i < PlayerData.battles.Count; i++)
            {
                var x = Instantiate(prefab, prefab.transform.parent);
                x.gameObject.SetActive(true);
                x.transform.SetAsFirstSibling();
                x.transform.GetChild(0).GetComponent<TextMeshPro>().text = PlayerData.battles[i];
                x.onClick.AddListener(() => LoadBattle(PlayerData.battles[i]));
            }
        }
    }

    public void LoadBattle(string battleID)
    {

    }
    public void SendRequest()
    {
        DataStore.Get($"BattlesNextID", true, (string value) =>
        {
            if (!string.IsNullOrEmpty(value))
            {
                DataStore.Delete("BattlesNextID", true);
                DataStore.Set("BattlesNextID", "" + (int.Parse(value) + 1), true, (bool val) => { if (!val) { GameJoltUI.Instance.QueueNotification("Couldn't make request code. \\GameSelectionMenu.cs line 70\\"); } });
                GameJoltUI.Instance.QueueNotification("Mach Request code is " + (int.Parse(value) + 1));
            }
            else
            {
                DataStore.Set("BattlesNextID", "1", true);
                GameJoltUI.Instance.QueueNotification( "Request Code is 1");
            }
        });

    }

    object ConvertData<T>(string value) where T:class
    {
        return JsonUtility.FromJson<T>(value);
    }
    string ConvertData(object value)
    {
        return JsonUtility.ToJson(value);
    }
}
