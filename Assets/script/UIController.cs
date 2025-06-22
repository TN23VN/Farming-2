using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("UI hướng dẫn")]
    public GameObject BackgroundTutorial;
    public Button UI_Tutorial;
    public Button btnOK1;

    [Header("UI cài đặt")]
    public GameObject BackgroundSetting;
    public Button UI_Setting;
    public Button btnOK2;
    public Button btnMusic;
    public Button btnSoundEffect;

    [Header("Audio button")]
    public AudioSource audioSource;
    public Text txtMusic;

    
    void Update()
    {
        UI_Tutorial.onClick.AddListener(() => BackgroundTutorial.SetActive(true));
        btnOK1.onClick.AddListener(() => BackgroundTutorial.SetActive(false));

        UI_Setting.onClick.AddListener(() => BackgroundSetting.SetActive(true));
        btnOK2.onClick.AddListener(() => BackgroundSetting.SetActive(false));
        
        btnMusic.onClick.AddListener(ToggleAudio);

    }

    void ToggleAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            txtMusic.text = "Nhạc nền: Tắt";
        }
        else
        {
            audioSource.UnPause();
            txtMusic.text = "Nhạc nền: Bật";
        }
    }
}
