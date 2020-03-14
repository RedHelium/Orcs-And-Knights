namespace Main
{

    public sealed class Player
    {
        public byte id { get; private set; }
        public string name { get; private set; }

        public Player(byte id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}