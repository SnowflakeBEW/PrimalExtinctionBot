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