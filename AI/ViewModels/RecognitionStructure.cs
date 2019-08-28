using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI
{
    /// <summary>
    /// Item to be recognized by the Recognition Engine
    /// </summary>
    public class GrammarItem
    {
        // The grammar Item Type, Could be Command/Convarsation/Dictation Types
        public GrammarItemType Type { get; set; }

        /// <summary>
        /// The Name of the Item, this is the word or words to be recognized
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The text for the action after the Name is recognized, for example, the text to be passed to the conversation algoritm
        /// </summary>
        public string AfterRecognition { get; set; }

        /// <summary>
        /// If it is used on a specific application only, could be null to indicate that this Command/Convarsation/Dictation should be included always
        /// </summary>
        public List<string> Applications { get; set; }
    }

    public enum GrammarItemType
    {
        /// <summary>
        /// A command to be recognized and passed to an action
        /// </summary>
        Command,
        /// <summary>
        /// Part of the conversation with the Recognizer, do not actually DO anything, is just for realism
        /// </summary>
        Conversation,
        /// <summary>
        /// Do not know what this does yet
        /// </summary>
        Dictation
    }
}
