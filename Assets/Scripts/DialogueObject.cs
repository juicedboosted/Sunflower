using UnityEngine;

//TODO: give this class a struct for messages, load the messages from a file maybe?

public class DialogueObject : MonoBehaviour
{
    public int m_messageCount = 4;
    public string GetNextMessage()
    {
        return "This is a message from a person";
    }
}
