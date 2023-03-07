﻿namespace AirFishLab.ScrollingList.Demo
{
    public class IntListBank : BaseListBank
    {
        public readonly int[] _contents = {
            1, 2222, 3, 4, 5, 6, 7, 8, 9, 10
        };

        public override object GetListContent(int index)
        {
            return _contents[index];
        }

        public override int GetListLength()
        {
            return _contents.Length;
        }
    }
}
