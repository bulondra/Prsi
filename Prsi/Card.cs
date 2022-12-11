using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Prsi
{
    // Card object
    public class Card
    {
        // Create variables
        Cards type;
        private string name;
        string texturePath;
        private ChangerType chType;
        List<Cards> canPlay;

        public Card(Cards type, string name, string texturePath, ChangerType chType, List<Cards> canPlay)
        {
            // Set variables
            this.type = type;
            this.name = name;
            this.texturePath = texturePath;
            this.chType = chType;
            this.canPlay = canPlay;
        }

        // Get card type
        public Cards getType()
        {
            return this.type;
        }

        // Get card name
        public string getName()
        {
            return this.name;
        }
        
        // Get card texture (only path to image)
        public string getTexturePath()
        {
            return this.texturePath;
        }

        // Get card changer's type (if set)
        public ChangerType getChType()
        {
            return this.chType;
        }
        
        // Get list of cards that can be played on
        public List<Cards> getCanPlay()
        {
            return this.canPlay;
        }
        
        // Set card changer's type (if necessary)
        public void setChType(ChangerType type)
        {
            this.chType = type;
        }
    }
}