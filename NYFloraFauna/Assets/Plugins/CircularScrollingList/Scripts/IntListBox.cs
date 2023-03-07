using AirFishLab.ScrollingList;
using UnityEngine;
using UnityEngine.UI;
// The box used for displaying the content
// Must be inherited from the class ListBox
public class IntListBox : ListBox
{
    [SerializeField]
    private Text _contentText;
    // This function is invoked by the `CircularScrollingList` for updating the list content
    // The type of the content will be converted to `object` in the `IntListBank` (Defined l
    // So it should be converted back to its own type for being used.
    // The original type of the content is `int`.
    protected override void UpdateDisplayContent(object content)
    {
        Debug.Log(content);
        _contentText.text = ((string)content).ToString();
    }
}