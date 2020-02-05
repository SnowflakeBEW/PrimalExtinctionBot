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
    public class Tickets : BaseCommandModule
    {
        [Command("Ticket")]
        [Description("See the possible commands in order to create a support/suggestoin ticket.")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task Ticket(CommandContext ctx)
        {

            var CorrectStep1 = new TextStep("Please type the command 'TDiscord' in the discord server so that we you can make a support ticket.", nextStep: null, 1);
            var CorrectStep2 = new TextStep("Please type the command 'TUser' in the discord server so that we you can make a support ticket.", nextStep: null, 1);
            var CorrectStep3 = new TextStep("Please type the command 'TGame' in the discord server so that we you can make a support ticket.", nextStep: null, 1);
            var CorrectStep4 = new TextStep("Please type the command 'TSuggestion' in the discord server so that we you can make a support ticket.", nextStep: null, 1);
            var CorrectStep5 = new TextStep("Please type the command 'TOther' in the discord server so that we you can make a support ticket.", nextStep: null, 1);
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

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(
                ctx.Client,
                userChannel,
                ctx.User,
                emojiStep
                );

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }
        }

        [Command("TDiscord")]
        [Description("Create a Discord Support Ticket.")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task TDiscord(CommandContext ctx)
        {
            var ThankStep = new TextStep("Thank you for your time! We will get back to you as soon as we can. Type anything to end the dialogue.", nextStep: null);
            var IssueStep = new TextStep("Please describe your issue so that we may get back to you with a resolution as soon as possible.", nextStep: ThankStep, 1);

            string input1 = string.Empty;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(
               ctx.Client,
               userChannel,
               ctx.User,
               IssueStep
           );


            IssueStep.OnValidResult += (result) =>
            {
                input1 = result;

            };

            var DiscordSupportChannel = await ctx.Client.GetChannelAsync(668744111485878292).ConfigureAwait(false);

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            var discordembed = new DiscordEmbedBuilder
            {
                Title = "Discord Support Ticket",
                Description = input1,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    Text = ctx.User.Username,
                }
            };

            var TicketMessage1 = await DiscordSupportChannel.SendMessageAsync(embed: discordembed).ConfigureAwait(false);
        }

        [Command("TUser")]
        [Description("Create a User Support Ticket / Report.")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task TUser(CommandContext ctx)
        {
            var ThankStep = new TextStep("Thank you for your time! We will get back to you as soon as we can. Type anything to end the dialogue.", nextStep: null);
            var IssueStep = new TextStep("Please describe your issue so that we may get back to you with a resolution as soon as possible.", nextStep: ThankStep, 1);

            string input1 = string.Empty;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(
               ctx.Client,
               userChannel,
               ctx.User,
               IssueStep
           );

            IssueStep.OnValidResult += (result) =>
            {
                input1 = result;

            };

            var ReportsChannel = await ctx.Client.GetChannelAsync(668744163730259969).ConfigureAwait(false);

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            var reportembed = new DiscordEmbedBuilder
            {
                Title = "User Support Ticket",
                Description = input1,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    Text = ctx.User.Username,
                }
            };

            var TicketMessage1 = await ReportsChannel.SendMessageAsync(embed: reportembed).ConfigureAwait(false);
        }

        [Command("TGame")]
        [Description("Create an ARK: Survival Evolved Support Ticket / Server Support Ticket.")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task TGame(CommandContext ctx)
        {
            var ThankStep = new TextStep("Thank you for your time! We will get back to you as soon as we can. Type anything to end the dialogue.", nextStep: null);
            var IssueStep = new TextStep("Please describe your issue so that we may get back to you with a resolution as soon as possible.", nextStep: ThankStep, 1);

            string input1 = string.Empty;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(
               ctx.Client,
               userChannel,
               ctx.User,
               IssueStep
           );

            IssueStep.OnValidResult += (result) =>
            {
                input1 = result;

            };

            var GameSupportChannel = await ctx.Client.GetChannelAsync(670899748634361876).ConfigureAwait(false);

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            var gameembed = new DiscordEmbedBuilder
            {
                Title = "Game Support Ticket",
                Description = input1,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    Text = ctx.User.Username,
                }
            };


            var TicketMessage1 = await GameSupportChannel.SendMessageAsync(embed: gameembed).ConfigureAwait(false);
        }

        [Command("TSuggestion")]
        [Description("Create a Suggestion Ticket.")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task TSuggestion(CommandContext ctx)
        {
            var ThankStep = new TextStep("Thank you for your time! We will get back to you as soon as we can. Type anything to end the dialogue.", nextStep: null);
            var IssueStep = new TextStep("Please describe your suggestion so that we may begin consideration / implementation.", nextStep: ThankStep, 1);

            string input1 = string.Empty;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(
               ctx.Client,
               userChannel,
               ctx.User,
               IssueStep
           );

            IssueStep.OnValidResult += (result) =>
            {
                input1 = result;

            };

            var SuggestionsChannel = await ctx.Client.GetChannelAsync(668744199612661781).ConfigureAwait(false);

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            var suggestionembed = new DiscordEmbedBuilder
            {
                Title = "Suggestion Ticket",
                Description = input1,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    Text = ctx.User.Username,
                }
            };


            var TicketMessage1 = await SuggestionsChannel.SendMessageAsync(embed: suggestionembed).ConfigureAwait(false);

        }

        [Command("TOther")]
        [Description("Create a Support / 'Other' Ticket for a Category not Otherwise Listed.")]
        [RequireCategories(ChannelCheckMode.Any, "Bots")]
        public async Task TOther(CommandContext ctx)
        {

            var ThankStep = new TextStep("Thank you for your time! We will get back to you as soon as we can. Type anything to end the dialogue.", nextStep: null);
            var IssueStep = new TextStep("Please describe your issue so that we may get back to you with a resolution as soon as possible.", nextStep: ThankStep, 1);
            var OtherIssueStep = new TextStep("What 'Category' could your issue be classified under? i.e. Monster Hunter World.", nextStep: IssueStep, 1);

            string input1 = string.Empty;
            string input2 = string.Empty;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(
               ctx.Client,
               userChannel,
               ctx.User,
               OtherIssueStep
           );

            IssueStep.OnValidResult += (result) =>
            {
                input1 = result;

            };

            OtherIssueStep.OnValidResult += (result) =>
            {
                input2 = result;
            };

            var OtherSupportChannel = await ctx.Client.GetChannelAsync(670899812354359296).ConfigureAwait(false);

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            var otherembed = new DiscordEmbedBuilder
            {
                Title = input2,
                Description = input1,
                Footer = new DiscordEmbedBuilder.EmbedFooter()
                {
                    Text = ctx.User.Username,
                }
            };

            var TicketMessage1 = await OtherSupportChannel.SendMessageAsync(embed: otherembed).ConfigureAwait(false);
        }
    }
}
