using UnityEngine;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;

//TODO: give this class a struct for messages, load the messages from a file maybe?

public class DialogueObject : MonoBehaviour
{
    public string characterName;

    public List<SocialEventLoader.EventMessage> receivedMessages = new List<SocialEventLoader.EventMessage>();
    public List<SocialEventLoader.EventMessage> queuedMessages = new List<SocialEventLoader.EventMessage>();
    
    //Add a function that moves messages from queued to received after advancing through time
}
