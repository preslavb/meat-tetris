using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{
    private TextMeshProUGUI tmpText;
    public ScoreSaver ScoreSaver;
    
    // Start is called before the first frame update
    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tmpText.text = $"Kg of Meat: {ScoreSaver.KilosOfMeat} \n" +
                       $"CO2 Emissions: {ScoreSaver.Co2Produced}";
    }
}
