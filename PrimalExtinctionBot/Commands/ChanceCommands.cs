using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using PrimalExtinciton.Handlers.Dialogue.Steps;
using PrimalExtinction.Handlers.Dialogue;
using PrimalExtinction.Handlers.Dialogue.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimalExtinctionBot.Commands
{
     public class ChanceCommands : BaseCommandModule
    {

        [Command("Flip")]
        [Description("Flips a coin and lands on 'Heads' or 'Tails'.")]
        public async Task Flip(CommandContext ctx)
        {

            var FlipResult = new Random();
            string[] Answer = { "Heads!", "Tails!" };


            int aIndex = FlipResult.Next(Answer.Length);

            var flipembed = new DiscordEmbedBuilder
            {
                Title = "Heads or Tails?",
                Description = Answer[aIndex]
            };

            var FlipMessage = await ctx.Channel.SendMessageAsync(embed: flipembed).ConfigureAwait(false);

        }


        [Command("Roll")]
        [Description("Rolls a Dice")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task Roll(CommandContext ctx)
        {
            var rollEmbed = new DiscordEmbedBuilder
            {
                Title = "What sided dice would you like to roll?",
                Description = ":four: (4) , :six: (6) , :eight: (8) , :keycap_ten: (10) , :one: (12) , or :two: (20) ?",
                Color = DiscordColor.Green
            };

            var rollMessage = await ctx.Channel.SendMessageAsync(embed: rollEmbed).ConfigureAwait(false);

            var fourEmoji = DiscordEmoji.FromName(ctx.Client, ":four:");
            var sixEmoji = DiscordEmoji.FromName(ctx.Client, ":six:");
            var eightEmoji = DiscordEmoji.FromName(ctx.Client, ":eight:");
            var tenEmoji = DiscordEmoji.FromName(ctx.Client, ":keycap_ten:");
            var twelveEmoji = DiscordEmoji.FromName(ctx.Client, ":one:");
            var twentyEmoji = DiscordEmoji.FromName(ctx.Client, ":two:");

            await rollMessage.CreateReactionAsync(fourEmoji).ConfigureAwait(false);
            await rollMessage.CreateReactionAsync(sixEmoji).ConfigureAwait(false);
            await rollMessage.CreateReactionAsync(eightEmoji).ConfigureAwait(false);
            await rollMessage.CreateReactionAsync(tenEmoji).ConfigureAwait(false);
            await rollMessage.CreateReactionAsync(twelveEmoji).ConfigureAwait(false);
            await rollMessage.CreateReactionAsync(twentyEmoji).ConfigureAwait(false);

            var interactivity = ctx.Client.GetInteractivity();

            var reactionResult = await interactivity.WaitForReactionAsync(
                x => x.Message == rollMessage &&
                x.User == ctx.User &&
               (x.Emoji == fourEmoji || x.Emoji == sixEmoji || x.Emoji == eightEmoji || x.Emoji == tenEmoji || x.Emoji == twelveEmoji || x.Emoji == twentyEmoji)).ConfigureAwait(false);

            if (reactionResult.Result.Emoji == fourEmoji)
            {
                var Dice4Result = new Random();
                string[] Answer = { "One!", "Two!", "Three!", "Four!" };


                int bIndex = Dice4Result.Next(Answer.Length);

                var d4embed = new DiscordEmbedBuilder
                {
                    Title = "Roll a four-sided dice!",
                    Description = Answer[bIndex]
                };

                var RollFourMessage = await ctx.Channel.SendMessageAsync(embed: d4embed).ConfigureAwait(false);
            }
            else if (reactionResult.Result.Emoji == sixEmoji)
            {
                var Dice6Result = new Random();
                string[] Answer = { "One!", "Two!", "Three!", "Four!", "Five!", "Six!" };


                int cIndex = Dice6Result.Next(Answer.Length);

                var d6embed = new DiscordEmbedBuilder
                {
                    Title = "Roll a six-sided dice!",
                    Description = Answer[cIndex]
                };

                var RollSixMessage = await ctx.Channel.SendMessageAsync(embed: d6embed).ConfigureAwait(false);
            }
            else if (reactionResult.Result.Emoji == eightEmoji)
            {
                var Dice8Result = new Random();
                string[] Answer = { "One!", "Two!", "Three!", "Four!", "Five!", "Six!", "Seven!", "Eight!" };


                int dIndex = Dice8Result.Next(Answer.Length);

                var d8embed = new DiscordEmbedBuilder
                {
                    Title = "Roll an eight-sided dice!",
                    Description = Answer[dIndex]
                };

                var RollEightMessage = await ctx.Channel.SendMessageAsync(embed: d8embed).ConfigureAwait(false);
            }
            else if (reactionResult.Result.Emoji == tenEmoji)
            {
                var Dice10Result = new Random();
                string[] Answer = { "One!", "Two!", "Three!", "Four!", "Five!", "Six!", "Seven!", "Eight!", "Nine!", "Ten!" };


                int eIndex = Dice10Result.Next(Answer.Length);

                var d10embed = new DiscordEmbedBuilder
                {
                    Title = "Roll a ten-sided dice!",
                    Description = Answer[eIndex]
                };

                var RollTenMessage = await ctx.Channel.SendMessageAsync(embed: d10embed).ConfigureAwait(false);
            }
            else if (reactionResult.Result.Emoji == twelveEmoji)
            {
                var Dice12Result = new Random();
                string[] Answer = { "One!", "Two!", "Three!", "Four!", "Five!", "Six!", "Seven!", "Eight!", "Nine!", "Ten!", "Eleven!", "Twelve!" };


                int fIndex = Dice12Result.Next(Answer.Length);

                var d12embed = new DiscordEmbedBuilder
                {
                    Title = "Roll a ten-sided dice!",
                    Description = Answer[fIndex]
                };

                var RollTwelveMessage = await ctx.Channel.SendMessageAsync(embed: d12embed).ConfigureAwait(false);
            }
            else if (reactionResult.Result.Emoji == twentyEmoji)
            {
                var Dice20Result = new Random();
                string[] Answer = { "One!", "Two!", "Three!", "Four!", "Five!", "Six!", "Seven!", "Eight!", "Nine!", "Ten!", "Eleven!", "Twelve!", "Thirteen!", "Fourteen!", "Fifteen!", "Sixteen!", "Seventeen!", "Eighteen!", "Nineteen!", "Twenty!" };


                int gIndex = Dice20Result.Next(Answer.Length);

                var d20embed = new DiscordEmbedBuilder
                {
                    Title = "Roll a twenty-sided dice!",
                    Description = Answer[gIndex]
                };

                var RollTwentyMessage = await ctx.Channel.SendMessageAsync(embed: d20embed).ConfigureAwait(false);
            }
            else
            {
                // Do Nothing
            }

            await rollMessage.DeleteAsync().ConfigureAwait(false);
        }
    }
}
