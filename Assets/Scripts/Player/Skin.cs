using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
public void HideSkin()
    {
        gameObject.SetActive(false);
    }
    public void ShowSkin()
    {
        gameObject.SetActive(true);
    }
}
