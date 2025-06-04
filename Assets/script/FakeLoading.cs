using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FakeLoading : MonoBehaviour
{
    public Button btnLoading;
    
    void Start()
    {
        btnLoading.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("SampleScene");
            //WriteAllTileMapToFirebase();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
