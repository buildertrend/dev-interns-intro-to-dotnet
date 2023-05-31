using System.Collections.Generic;

namespace BlackjackUpdated
{

    class Card
    {
        public int Value;
        public string Name;
        public string Suite;

        public Card(int inValue, string inName, string inSuite)
        {
            Value = inValue;
            Name = inName;
            Suite = inSuite;
        }
        
    }
}
