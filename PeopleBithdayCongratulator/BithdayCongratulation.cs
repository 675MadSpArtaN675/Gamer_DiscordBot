using Discord;
using Discord.WebSocket;


namespace PeopleBithdayCongratulator
{
    public class BithdayCongratulation
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public string? ImageUri { get; set; }
        public SocketGuildUser BithdayBoy { get; set; }

        public BithdayCongratulation(string title, string description, string imageUri)
        {
            Title = title;
            Description = description;
            ImageUri = imageUri;
        }

        public BithdayCongratulation(string title, string description, string imageUri, SocketGuildUser bithdayBoy)
        {
            Title = title;
            Description = description;
            ImageUri = imageUri;
            BithdayBoy = bithdayBoy;
        }
    }
}
