using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

namespace Dialogue_Event_System
{
    public class DialogueNode : Node
    {
        public string DialogueText;
        public string GUID;
        public bool EntryPoint = false;

        public IEnumerable<object> OutputSockets { get; internal set; }
    }
}

