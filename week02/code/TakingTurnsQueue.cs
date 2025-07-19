/// <summary>
/// This queue is circular. When people are added via AddPerson, they are added to the 
/// back of the queue (FIFO). When GetNextPerson is called, the next person in the queue 
/// is returned and may be placed back in the queue depending on their turns value.
/// People with:
///   - turns <= 0 stay in the queue infinitely,
///   - turns > 1 are decremented and re-added,
///   - turns == 1 are not added back (they are done).
/// </summary>
public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    /// <summary>
    /// Adds a new person with a specified number of turns.
    /// </summary>
    /// <param name="name">The name of the person</param>
    /// <param name="turns">The number of turns (0 or less = infinite)</param>
    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    /// <summary>
    /// Retrieves the next person in the queue based on circular logic.
    /// 
    /// and people with infinite turns (turns <= 0) are never removed.
    /// 
    /// In the original logic, people with finite turns were not always removed
    /// after using all their turns. For example, Sue (with 1 turn) was being returned
    /// again even after she should have exited the queue.
    /// 
    ///   - If person has turns <= 0: re-added to queue forever.
    ///   - If person has turns > 1: decrement and re-add.
    ///   - If person has turns == 1: do not re-add.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        Person person = _people.Dequeue();

        // Copy created to preserve original number of turns for test validation
        Person returnCopy = new Person(person.Name, person.Turns);

        if (person.Turns <= 0)
        {
            // Infinite turns — always re-add to the queue
            _people.Enqueue(person);
        }
        else if (person.Turns > 1)
        {
            // Reduce turn count and re-add
            person.Turns -= 1;
            _people.Enqueue(person);
        }
        // If turns == 1: do not re-add; person exits the queue

        return returnCopy;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}
