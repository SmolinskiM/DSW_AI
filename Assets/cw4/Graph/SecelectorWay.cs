using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecelectorWay : MonoBehaviour
{
    public PlayerMovement player;

    public Button goToGreen;
    public Button goToBlue;
    public Button goToWhite;
    public Button goToYellow;

    public GameObject green;
    public GameObject blue;
    public GameObject white;
    public GameObject yellow;

    private void Awake()
    {
        goToGreen.onClick.AddListener(delegate { player.SetPath(green); });
        goToBlue.onClick.AddListener(delegate { player.SetPath(blue); });
        goToWhite.onClick.AddListener(delegate { player.SetPath(white);});
        goToYellow.onClick.AddListener(delegate { player.SetPath(yellow); });
    }
}
