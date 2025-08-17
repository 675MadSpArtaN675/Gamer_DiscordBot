using System.Collections;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

using Discord;
using Discord.WebSocket;
using PeopleBithdayCongratulator.Interfaces;

namespace PeopleBithdayCongratulator
{
    public class BithdayCongratulator
    {
        private IBithdayDataReader _reader;

        private HashSet<BithdayCongratulation> _congratulations;
        private DateTime _congratulationTime;
        private DateTime _now_date;

        public BithdayCongratulator(IBithdayDataReader reader, TimeOnly? time = null)
        {
            _reader = reader;

            _congratulations = new HashSet<BithdayCongratulation>();
            _now_date = DateTime.Now.Date;

            DateOnly date = new DateOnly(_now_date.Year, _now_date.Month, _now_date.Year);

            if (time is null)
                time = new TimeOnly(8, 30, 50);

            _congratulationTime = new DateTime(date, (TimeOnly)time);
        }

        public BithdayCongratulation? this[int index]
        {
            get
            {
                if (index > _congratulations.Count)
                    return _congratulations.ElementAtOrDefault(index);

                return null;
            }

            set => _congratulations.Append(value);

        }

        public BithdayCongratulation GenerateBody(string description, string imageUri, SocketGuildUser user, string title = "")
        {
            if (title == "" || title == null)
                title = _reader.ReadRandomTitle();

            if (!Regex.IsMatch(title, "^#{1, }.*"))
                title = "## " + GenerateDescription(title.Trim(), user.GlobalName);

            if (description == "")
                description = GenerateDescription(_reader.ReadRandomDescription(), user.GlobalName);

            if (imageUri == "")
                imageUri = Environment.GetEnvironmentVariable("STANDART_BITHDAY_IMAGE")?.Trim() ?? "";

            return new BithdayCongratulation(title, description, imageUri, user);
        }

        public async Task<IUserMessage> SendCongratulation(SocketTextChannel channel)
        {
            MessageComponent messageComponent;
            BithdayCongratulation congratulation;

            if (_congratulations.Count > 0)
            {
                congratulation = _congratulations.First();
                messageComponent = BuildMessage(congratulation);

                IUserMessage message = await channel.SendMessageAsync(components: messageComponent);
                _congratulations.Remove(congratulation);

                return message;
            }

            throw new Exception("No one congratulation component in list");
        }

        public async Task<List<IUserMessage>> SendCongratulations(SocketTextChannel channel)
        {
            List<IUserMessage> messages = new List<IUserMessage>();

            for (int i = 0; i < _congratulations.Count; i++)
                messages.Add(await SendCongratulation(channel));

            return messages;
        }

        private MessageComponent BuildMessage(BithdayCongratulation congratulator)
        {
            var title_builder = new ActionRowBuilder(new TextDisplayBuilder(congratulator.Title));
            var description_builder = new ActionRowBuilder(new TextDisplayBuilder(congratulator.Description));
            var date_row_builder = new ActionRowBuilder(new TextDisplayBuilder(_now_date.Date.ToLongDateString()));

            ComponentBuilderV2 builder = new ComponentBuilderV2()
                .AddComponents(title_builder, new SeparatorBuilder())
                .AddComponent(new FileComponentBuilder()
                        .WithFile(new UnfurledMediaItemProperties(congratulator.ImageUri))
                    )
                .AddComponents(new SeparatorBuilder(), description_builder, new SeparatorBuilder(), date_row_builder);

            return builder.Build();
        }

        private string GenerateDescription(string description, string new_name)
        {
            if (new_name == "")
                return description;

            return description.Replace("{username}", $"@{new_name}");
        }
    }
}
