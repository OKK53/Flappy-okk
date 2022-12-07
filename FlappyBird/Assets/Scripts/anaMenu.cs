
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class anaMenu : MonoBehaviour {

    [SerializeField]
    Text puanText;
    [SerializeField]
    Text sonPuanText;
    void Start () {
        int enYuksekPuan = PlayerPrefs.GetInt("enYuksekPuanKayit");
        int sonPuan = PlayerPrefs.GetInt("puanKayit");
        //PlayerPrefs.DeleteAll();
        if (enYuksekPuan != 0)
        {
            puanText.text = "High Score = " + enYuksekPuan;
            sonPuanText.text = "Last Score = " + sonPuan;
        }
    }

    public void OyunaGit()
    {
        SceneManager.LoadScene("level1");
    }
    public void OyundanCik()
    {
        Application.Quit();
    }
}
