using System.Collections.Generic;

namespace Main
{

    public sealed class Player
    {
        public byte ID { get; private set; }
        public string Name { get; private set; }

        public Player(byte id, string name)
        {
            ID = id;
            Name = name;
        }

        public  bool Equals(byte id) => ID == id;
      
    }
}