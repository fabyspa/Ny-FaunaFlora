using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour
{
    Toggle m_Toggle;
    public Image ita;
    public Image eng;

    void Start()
    {
        //Fetch the Toggle GameObject
        m_Toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener(delegate
        {
            ToggleValueChanged(m_Toggle);
        });

        //Initialise the Text to say the first state of the Toggle
        ita.gameObject.SetActive(true);
        eng.gameObject.SetActive(false);
        m_Toggle.targetGraphic = ita;
    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change)
    {
        if (change.isOn)
        {
            ita.gameObject.SetActive(false);
            eng.gameObject.SetActive(true);
            m_Toggle.targetGraphic = eng;
        }
        else
        {
            ita.gameObject.SetActive(true);
            eng.gameObject.SetActive(false);
            m_Toggle.targetGraphic = ita;
        }
    }
}