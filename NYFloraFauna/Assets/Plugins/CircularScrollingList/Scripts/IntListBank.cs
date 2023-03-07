using AirFishLab.ScrollingList;
// The bank for providing the content for the box to display
// Must be inherit from the class BaseListBank
public class IntListBank : BaseListBank
{
    //array contenente i nomi 
    public string[] _contents;




    // This function will be invoked by the `CircularScrollingList`
    // when acquiring the content to display
    // The object returned will be converted to the type `object`
    // which will be converted back to its own type in `IntListBox.UpdateDisplayContent()`
    public override object GetListContent(int index)
    {
        return _contents[index];
    }
    public override int GetListLength()
    {
        return _contents.Length;
    }
}