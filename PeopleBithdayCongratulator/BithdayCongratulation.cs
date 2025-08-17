using Discord;
using Discord.WebSocket;


namespace PeopleBithdayCongratulator
{
    public class BithdayCongratulation
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public byte[] Image { get; set; }
        public SocketGuildUser BithdayBoy { get; set; }

        public BithdayCongratulation(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public BithdayCongratulation(string title, string description, byte[] image, SocketGuildUser bithdayBoy)
        {
            Title = title;
            Description = description;
            Image = image;
            BithdayBoy = bithdayBoy;
        }
    }
}
