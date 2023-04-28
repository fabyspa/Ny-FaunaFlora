using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchITAENG : MonoBehaviour
{
    Toggle m_Toggle;
    //public Text m_Text;
    public string tag1, tag2;
    void Start()
    {
        tag1 = "ITA";
        tag2 = "ENG";
        //Fetch the Toggle GameObject
        m_Toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, and output the state
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });

        SwitchActiveTag();

        //Initialize the Text to say whether the Toggle is in a positive or negative state
        //m_Text.text = "Toggle is : " + m_Toggle.isOn;
    }

    //Output the new state of the Toggle into Text when the user uses the Toggle
    void ToggleValueChanged(Toggle change)
    {
       
        //traduci in inglese
        SwitchActiveTag();
        
    }

    public void SwitchActiveTag()
    {
        foreach(GameObject c in GameObject.FindGameObjectsWithTag(tag1))
        {
            if(c.transform.parent.tag == "Bandiera")
            {

            }else
            c.GetComponent<Text>().enabled = true;
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag(tag2))
        {
            g.GetComponent<Text>().enabled = false;
        }
        var temp = tag1;
        tag1 = tag2;
        tag2 = temp;
    }

}
