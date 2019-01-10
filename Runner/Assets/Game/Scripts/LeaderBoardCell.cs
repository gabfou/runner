using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardCell : MonoBehaviour
{
    // public Image	fillImage;
    public Text pointText;
    public Text nameText;

    public void UpdateProperties(float points, string name)
    {

        pointText.text = points.ToString();

        nameText.text = name;
    }
}
