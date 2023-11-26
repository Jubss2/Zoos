using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Name : MonoBehaviour
{
    string word = null;
    int wordIndex = 0;
    string alpha;
    public TextMeshProUGUI myName = null;
    // Start is called before the first frame update
   public void nameFunction(string alphabet)
    {
       
        if(wordIndex < 3) { 
            wordIndex++;
            word = word + alphabet;
            myName.text = word;

        }

    }
}
