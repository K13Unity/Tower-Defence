using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpdateBtnConfig", menuName = "Configs/UpdateBtnConfig")]
public class UpdateBtnConfig : ScriptableObject
{
    [SerializeField] private List<int> prices;

    public List<int> Prices => prices;  
}
