using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuFunctions : MonoBehaviour {

    public Color onBtnHoverTextColor = Color.green;
    public Text playButtonText;
    private Color initialColor;

    private void Start()
    {
        initialColor = playButtonText.color;
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("main");
    }

    public void OnPlayHover()
    {
        playButtonText.color = onBtnHoverTextColor;
    }

    public void OnPlayLeaves()
    {
        playButtonText.color = initialColor;
    }

}
