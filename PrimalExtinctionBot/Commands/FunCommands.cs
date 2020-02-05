using PrimalExtinction.Attributes;
using PrimalExtinction.Handlers.Dialogue;
using PrimalExtinction.Handlers.Dialogue.Steps;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimalExtinciton.Handlers.Dialogue.Steps;

namespace PrimalExtinction.Commands
{
    public class FunCommands : BaseCommandModule
    {
        [Command("Ping")]
        [Description("Returns 'Pong' ~ Checks if the bot is online")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]

        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
        }

        [Command("Add")]
        [Description("Adds Two Numbers Together")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task Add(CommandContext ctx,
            [Description("First Number")] int numberOne,
            [Description("Second Number")] int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync((numberOne + numberTwo).ToString())
                .ConfigureAwait(false);
        }

        [Command("Subtract")]
        [Description("Subtracts Two Numbers From Each Other")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task Subtract(CommandContext ctx,
           [Description("First Number")] int numberOne,
           [Description("Second Number")] int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync((numberOne - numberTwo).ToString())
                .ConfigureAwait(false);
        }

        [Command("Multiply")]
        [Description("Multiplies Two Numbers Together")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task Multiply(CommandContext ctx,
           [Description("First Number")] int numberOne,
           [Description("Second Number")] int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync((numberOne * numberTwo).ToString())
                .ConfigureAwait(false);
        }

        [Command("Divide")]
        [Description("Divides Two Numbers From Each Other ~ Only works if the number would come out whole and not with a fraction or decimal. ex. 3/2 will not work but 10/2 will.")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task Divide(CommandContext ctx,
           [Description("First Number")] int numberOne,
           [Description("Second Number")] int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync((numberOne / numberTwo).ToString())
                .ConfigureAwait(false);
        }

        [Command("Status")]
        [Description("What is the Bot Creator (SnowflakeBEW) Currently Working on?")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task Status(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Is working on fixing the Poll Command Results when available. But is currently on break due to school.").ConfigureAwait(false);
        }

        [Command("ResponseMessage")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task RespondMessage(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Content);
        }

        [Command("RespondReaction")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task RespondReaction(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Emoji);

        }

        [Command("Poll")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task Poll(CommandContext ctx, TimeSpan duration, params DiscordEmoji[] emojioptions)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var options = emojioptions.Select(x => x.ToString());

            var pollembed = new DiscordEmbedBuilder
            {
                Title = "Poll",
                Description = string.Join(" ", options)
            };

            var pollMessage = await ctx.Channel.SendMessageAsync(embed: pollembed).ConfigureAwait(false);

            foreach (var option in emojioptions)
            {
                await pollMessage.CreateReactionAsync(option).ConfigureAwait(false);
            }

            var result = await interactivity.CollectReactionsAsync(pollMessage, duration).ConfigureAwait(false);
            var distinctResult = result.Distinct();

            var results = distinctResult.Select(x => $"{x.Emoji}: {x.Total}");

            await ctx.Channel.SendMessageAsync(string.Join("\n", results)).ConfigureAwait(false);
        }

        [Command("Dialogue")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task Dialogue(CommandContext ctx)
        {
            var inputStep = new TextStep("Enter something interesting!", null, 10);
            var funnyStep = new IntStep("Haha, funny", null, maxValue: 100);

            string input = string.Empty;
            int value = 0;

            inputStep.OnValidResult += (result) =>
            {
                input = result;

                if (result == "something interesting")
                {
                    inputStep.SetNextStep(funnyStep);
                }
            };

            funnyStep.OnValidResult += (result) => value = result;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(
                ctx.Client,
                userChannel,
                ctx.User,
                inputStep
            );

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            await ctx.Channel.SendMessageAsync(input).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(value.ToString()).ConfigureAwait(false);
        }


    }
}
