using System;

public class Card
{
    public int Value;
    public string Name;

    public Card()
    {

    }

    public Card(int value, string name)
    {
        this.Value = value;
        this.Name = name;
    }

    public override string ToString()
    {
        return Name + " --- " + Value;
    }
}
