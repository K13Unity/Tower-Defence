using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeСounterUI : MonoBehaviour
{
    [SerializeField] private List<Image> image;
    [SerializeField] private Sprite red;
    [SerializeField] private Sprite white;
    [SerializeField] private int _value;


    void Start()
    {
        Display();
    }

    public int value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            Display();
        }
    }


    private void Display()
    {
        for (int i = 0; i < image.Count; i++)
        {
            if (i < value)
            {
                image[i].sprite = red;
            }
            else
            {
                image[i].sprite = white;
            }
        }
    }
}
