using UnityEngine.UIElements.Experimental;

public class AllyUnit
{
    public int MaxMember { get; private set; }

    private int actualMember;
    public int ActualMember
    {
        get { return actualMember; }

        set
        {
            if (value > MaxMember)
            {
                actualMember = MaxMember;
            }
            else if (value < 0)
            {
                actualMember = 0;
            }
            else
            {
                actualMember = value;
            }
        }

    }

    public AllyUnit(int maxMember)
    {
        MaxMember = maxMember;

        ActualMember = 0;
    }

    public bool IsFull()
    {
        return ActualMember == MaxMember;
    }
}
