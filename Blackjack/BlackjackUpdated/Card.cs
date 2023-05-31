namespace BlackjackUpdated
{

    class Card
    {
        public int Value;
        public string Name;
        public string Suite;
        string topBottom = "----------\n";
        string middle = "|        |\n";

        public Card(int inValue, string inName, string inSuite)
        {
            Value = inValue;
            Name = inName;
            Suite = inSuite;
        }

        public string showArt(Card c)
        {
            string cardString = topBottom + "";
            if (c.Value < 10) {
                if (c.Suite.Equals("Hearts")){
                    cardString += "|" + c.Value + "     " + "<3|\n";
                } else if (c.Suite.Equals("Clubs"))
                {
                    cardString += "|" + c.Value + "      " + "+|\n";
                }
                else if (c.Suite.Equals("Diamonds"))
                {
                    cardString += "|" + c.Value + "     " + "<>|\n";
                }
                else
                {
                    cardString += "|" + c.Value + "      " + "@|\n";
                }

            }
            else
            {
                if (c.Suite.Equals("Hearts"))
                {
                    cardString += "|" + c.Value + "    " + "<3|\n";
                }
                else if (c.Suite.Equals("Clubs"))
                {
                    cardString += "|" + c.Value + "     " + "+|\n";
                }
                else if (c.Suite.Equals("Diamonds"))
                {
                    cardString += "|" + c.Value + "    " + "<>|\n";
                }
                else
                {
                    cardString += "|" + c.Value + "     " + "@|\n";
                }
            }
            for (int i=0; i<5; i++)
            {
                cardString += middle;
            }
            cardString += topBottom;

            return cardString;
        }
        
    }
}
