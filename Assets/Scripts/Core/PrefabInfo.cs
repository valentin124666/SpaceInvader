using System;

namespace Core
{
    public class PrefabInfo : Attribute
    {
        public string Location { get; }

        public PrefabInfo(string location)
        {
            Location = location;
        }
    }
}