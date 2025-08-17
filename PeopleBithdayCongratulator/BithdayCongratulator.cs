using System.Collections;
using Discord;
using Discord.WebSocket;

namespace PeopleBithdayCongratulator
{
    public class BithdayCongratulator
    {
        private DiscordSocketClient _client;

        private HashSet<BithdayCongratulation> _congratulations;
        private DateTime _congratulationTime;
        private DateTime _now_date;

        public BithdayCongratulator(DiscordSocketClient client, TimeOnly time)
        {
            _client = client;

            _congratulations = new HashSet<BithdayCongratulation>();
            _now_date = DateTime.Now.Date;

            DateOnly date = new DateOnly(_now_date.Year, _now_date.Month, _now_date.Year);

            _congratulationTime = new DateTime(date, time);
        }

        public BithdayCongratulation GenerateTitleAndDescription(string description, byte[] image, SocketGuildUser user, string title = "")
        {
            if (title == "")
                title = "Поздравляем!";

            if (description == "")
                throw new Exception("Нет описания для игрока!");

            return new BithdayCongratulation(title, description, image, user);
        }
    }
}
