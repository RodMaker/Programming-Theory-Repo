using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldCountBar : MonoBehaviour
{
    public TextMeshProUGUI goldCountText;
    public void SetGoldCountText(int goldCount)
    {
        goldCountText.text = goldCount.ToString();
    }
}
