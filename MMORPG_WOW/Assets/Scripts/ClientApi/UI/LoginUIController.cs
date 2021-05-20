using UnityEngine;
using UnityEngine.UI;

namespace ClientApi.UI{
    public class LoginUIController : MonoBehaviour{
        [SerializeField] GameObject loginPlayerUi;
        [SerializeField] GameObject createPlayerUi;
        [SerializeField] Text nameField;
        void Awake(){
            var userName = PlayerPrefs.GetString("UserName", "");
            var userId = PlayerPrefs.GetString("UserId", "");
            nameField.text = userName;
            var isUserNameEmpty = IsUserIdEmpty(userId);
            loginPlayerUi.SetActive(!isUserNameEmpty);
            createPlayerUi.SetActive(isUserNameEmpty);
        }
        static bool IsUserIdEmpty(string userName){
            return userName == string.Empty;
        }
    }
}