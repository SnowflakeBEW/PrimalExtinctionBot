using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using PrimalExtinciton.Handlers.Dialogue.Steps;
using PrimalExtinction.Attributes;
using PrimalExtinction.Handlers.Dialogue;
using PrimalExtinction.Handlers.Dialogue.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimalExtinctionBot.Commands
{
    public class TicketCommands
    {
        [Command("Ticket")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task Ticket(CommandContext ctx)
        {
            var CorrectStep1 = new TextStep("Please type the command 'TDiscord' so that we you can make a support ticket.", nextStep: null, 1);
            var CorrectStep2 = new TextStep("Please type the command 'TUser' so that we you can make a support ticket.", nextStep: null, 1);
            var CorrectStep3 = new TextStep("Please type the command 'TGame' so that we you can make a support ticket.", nextStep: null, 1);
            var CorrectStep4 = new TextStep("Please type the command 'TSuggestion' so that we you can make a support ticket.", nextStep: null, 1);
            var CorrectStep5 = new TextStep("Please type the command 'TOther' so that we you can make a support ticket.", nextStep: null, 1);
            var CancelStep = new TextStep("The dialogue has been canceled.", null);

            var DiscordStep = new ReactionStep("You chose support with Discord.", new Dictionary<DiscordEmoji, ReactionStepData>
            {
                { DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"), new ReactionStepData { Content = "Correct?", NextStep = CorrectStep1 } },
                { DiscordEmoji.FromName(ctx.Client, ":x:"), new ReactionStepData { Content = "Cancel?", NextStep = CancelStep }}
            });
            var UserStep = new ReactionStep("You chose support with another User.", new Dictionary<DiscordEmoji, ReactionStepData>
            {
                { DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"), new ReactionStepData { Content = "Correct?", NextStep = CorrectStep2 } },
                { DiscordEmoji.FromName(ctx.Client, ":x:"), new ReactionStepData { Content = "Cancel?", NextStep = CancelStep }}
            });
            var GameStep = new ReactionStep("You chose support in/with ARK:Survival Evolved or the server.", new Dictionary<DiscordEmoji, ReactionStepData>
            {
                { DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"), new ReactionStepData { Content = "Correct?", NextStep = CorrectStep3 } },
                { DiscordEmoji.FromName(ctx.Client, ":x:"), new ReactionStepData { Content = "Cancel?", NextStep = CancelStep }}
            });
            var SuggestionStep = new ReactionStep("You chose Suggestions.", new Dictionary<DiscordEmoji, ReactionStepData>
            {
                { DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"), new ReactionStepData { Content = "Correct?", NextStep = CorrectStep4 } },
                { DiscordEmoji.FromName(ctx.Client, ":x:"), new ReactionStepData { Content = "Cancel?", NextStep = CancelStep }}
            });
            var OtherStep = new ReactionStep("You chose support with something not currently listed.", new Dictionary<DiscordEmoji, ReactionStepData>
            {
                { DiscordEmoji.FromName(ctx.Client, ":white_check_mark:"), new ReactionStepData { Content = "Correct?", NextStep = CorrectStep5 } },
                { DiscordEmoji.FromName(ctx.Client, ":x:"), new ReactionStepData { Content = "Cancel?", NextStep = CancelStep }}
            });

            var emojiStep = new ReactionStep("What do you need support with? :one: - Discord? :two: - Another User? :three: - In/with our server on ARK: Survival Evolved? :four: - Have a suggestion? :five: Something currently not listed?", new Dictionary<DiscordEmoji, ReactionStepData>
            {
                { DiscordEmoji.FromName(ctx.Client, ":one:"), new ReactionStepData { Content = "Discord?", NextStep = DiscordStep } },
                { DiscordEmoji.FromName(ctx.Client, ":two:"), new ReactionStepData { Content = "Another User?", NextStep = UserStep } },
                { DiscordEmoji.FromName(ctx.Client, ":three:"), new ReactionStepData { Content = "In/with our server on ARK: Survival Evolved?", NextStep = GameStep } },
                { DiscordEmoji.FromName(ctx.Client, ":four:"), new ReactionStepData { Content = "Have a suggestion?", NextStep = SuggestionStep } },
                { DiscordEmoji.FromName(ctx.Client, ":five:"), new ReactionStepData { Content = "With something not currently listed?", NextStep = OtherStep } }
            });

            await ctx.Channel.SendMessageAsync().ConfigureAwait(false);
        }

        [Command("TDiscord")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task TDiscord(CommandContext ctx)
        {
            var IssueStep = new TextStep("Please describe your issue so that we may get back to you with a resolution as soon as possible.", nextStep: null, 1);
            var NameStep = new TextStep("What is your discord name and tag number? For example, Snowflake BEW#4921.", nextStep: IssueStep, 1);

            string input1 = string.Empty;
            string input2 = string.Empty;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(
               ctx.Client,
               userChannel,
               ctx.User,
               NameStep
           );

            IssueStep.OnValidResult += (result) =>
            {
                input1 = result;

            };

            NameStep.OnValidResult += (result) =>
            {
                input2 = result;
            };

            var DiscordSupportChannel = await ctx.Client.GetChannelAsync(668744111485878292).ConfigureAwait(false);

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            var discordembed = new DiscordEmbedBuilder
            {
                Title = "Discord Support Ticket",
                Description = (input2 + "||" + input1),
            };

            var TicketMessage1 = await DiscordSupportChannel.SendMessageAsync(embed: discordembed).ConfigureAwait(false);
        }

        [Command("TUser")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task TUser(CommandContext ctx)
        {
            var IssueStep = new TextStep("Please describe your issue so that we may get back to you with a resolution as soon as possible.", nextStep: null, 1);
            var NameStep = new TextStep("What is your discord name and tag number? For example, Snowflake BEW#4921.", nextStep: IssueStep, 1);

            string input1 = string.Empty;
            string input2 = string.Empty;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(
               ctx.Client,
               userChannel,
               ctx.User,
               NameStep
           );

            IssueStep.OnValidResult += (result) =>
            {
                input1 = result;

            };

            NameStep.OnValidResult += (result) =>
            {
                input2 = result;
            };

            var ReportsChannel = await ctx.Client.GetChannelAsync(668744163730259969).ConfigureAwait(false);

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            var reportembed = new DiscordEmbedBuilder
            {
                Title = "User Support Ticket",
                Description = (input2 + "||" + input1),
            };

            var TicketMessage1 = await ReportsChannel.SendMessageAsync(embed: reportembed).ConfigureAwait(false);
        }

        [Command("TGame")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task TGame(CommandContext ctx)
        {
            var IssueStep = new TextStep("Please describe your issue so that we may get back to you with a resolution as soon as possible.", nextStep: null, 1);
            var NameStep = new TextStep("What is your discord name and tag number? For example, Snowflake BEW#4921.", nextStep: IssueStep, 1);

            string input1 = string.Empty;
            string input2 = string.Empty;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(
               ctx.Client,
               userChannel,
               ctx.User,
               NameStep
           );

            IssueStep.OnValidResult += (result) =>
            {
                input1 = result;

            };

            NameStep.OnValidResult += (result) =>
            {
                input2 = result;
            };

            var GameSupportChannel = await ctx.Client.GetChannelAsync(670899748634361876).ConfigureAwait(false);

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            var gameembed = new DiscordEmbedBuilder
            {
                Title = "Game Support Ticket",
                Description = (input2 + "||" + input1),
            };


            var TicketMessage1 = await GameSupportChannel.SendMessageAsync(embed: gameembed).ConfigureAwait(false);
        }

        [Command("TSuggestion")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task TSuggestion(CommandContext ctx)
        {
            var IssueStep = new TextStep("Please describe your issue so that we may get back to you with a resolution as soon as possible.", nextStep: null, 1);
            var NameStep = new TextStep("What is your discord name and tag number? For example, Snowflake BEW#4921.", nextStep: IssueStep, 1);

            string input1 = string.Empty;
            string input2 = string.Empty;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(
               ctx.Client,
               userChannel,
               ctx.User,
               NameStep
           );

            IssueStep.OnValidResult += (result) =>
            {
                input1 = result;

            };

            NameStep.OnValidResult += (result) =>
            {
                input2 = result;
            };

            var SuggestionsChannel = await ctx.Client.GetChannelAsync(668744199612661781).ConfigureAwait(false);

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            var suggestionembed = new DiscordEmbedBuilder
            {
                Title = "Suggestion Ticket",
                Description = (input2 + "||" + input1),
            };


            var TicketMessage1 = await SuggestionsChannel.SendMessageAsync(embed: suggestionembed).ConfigureAwait(false);

        }

        [Command("TOther")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task TOther(CommandContext ctx)
        {
            var IssueStep = new TextStep("Please describe your issue so that we may get back to you with a resolution as soon as possible.", nextStep: null, 1);
            var NameStep = new TextStep("What is your discord name and tag number? For example, Snowflake BEW#4921.", nextStep: IssueStep, 1);

            string input1 = string.Empty;
            string input2 = string.Empty;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(
               ctx.Client,
               userChannel,
               ctx.User,
               NameStep
           );

            IssueStep.OnValidResult += (result) =>
            {
                input1 = result;

            };

            NameStep.OnValidResult += (result) =>
            {
                input2 = result;
            };

            var OtherSupportChannel = await ctx.Client.GetChannelAsync(670899812354359296).ConfigureAwait(false);

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            var otherembed = new DiscordEmbedBuilder
            {
                Title = "Other Support Ticket",
                Description = (input2 + "||" + input1),
            };

            var TicketMessage1 = await OtherSupportChannel.SendMessageAsync(embed: otherembed).ConfigureAwait(false);
            
        }
    }
}
