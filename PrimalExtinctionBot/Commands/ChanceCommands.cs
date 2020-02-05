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


        [Command("Dice")]
        [Description("Gives you the command to roll for the following sided dice: 'd4', 'd6', 'd8', 'd10', 'd12', and 'd20' ")]
        public async Task Dice(CommandContext ctx)
        {

            var d4Step = new TextStep("Please use the commands '?d4' to roll a four-sided dice.", nextStep: null);
            var d6Step = new TextStep("Please use the commands '?d6' to roll a six-sided dice.", nextStep: null);
            var d8Step = new TextStep("Please use the commands '?d8' to roll an eight-sided dice.", nextStep: null);
            var d10Step = new TextStep("Please use the commands '?d10' to roll a ten-sided dice.", nextStep: null);
            var d12Step = new TextStep("Please use the commands '?d12' to roll a twelve-sided dice.", nextStep: null);
            var d20Step = new TextStep("Please use the commands '?d20' to roll a twenty-sided dice.", nextStep: null);


            var DiceStep = new ReactionStep("What sided dice do you want to roll? (Note: :one: = 12-Sided and :two: = 20-Sided)", new Dictionary<DiscordEmoji, ReactionStepData>
            {
                { DiscordEmoji.FromName(ctx.Client, ":four:"), new ReactionStepData { Content = "A 4-Sided Dice?", NextStep = d4Step } },
                { DiscordEmoji.FromName(ctx.Client, ":six:"), new ReactionStepData { Content = "A 6-Sided Dice?", NextStep = d6Step } },
                { DiscordEmoji.FromName(ctx.Client, ":eight:"), new ReactionStepData { Content = "An 8-Sided Dice?", NextStep = d8Step } },
                { DiscordEmoji.FromName(ctx.Client, ":keycap_ten:"), new ReactionStepData { Content = "A 10-Sided Dice?", NextStep = d10Step } },
                {DiscordEmoji.FromName(ctx.Client, ":one:"), new ReactionStepData {Content = "A 12-Sided Dice?", NextStep = d12Step } },
                { DiscordEmoji.FromName(ctx.Client, ":two:"), new ReactionStepData { Content = "A 20-Sided Dice?", NextStep = d20Step } }
            });

            var inputDialogueHandler = new DialogueHandler(
                ctx.Client,
                ctx.Channel,
                ctx.User,
                DiceStep
                );

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }
        }

        [Command("d4")]
        [Description("Rolls a four-sided dice")]

        public async Task d4(CommandContext ctx)
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

        [Command("d6")]
        [Description("Rolls a six-sided dice")]

        public async Task d6(CommandContext ctx)
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

        [Command("d8")]
        [Description("Rolls an eight-sided dice")]

        public async Task d8(CommandContext ctx)
        {

            var Dice8Result = new Random();
            string[] Answer = { "One!", "Two!", "Three!", "Four!", "Five!", "Six!", "Seven!", "Eight!" };


            int dIndex = Dice8Result.Next(Answer.Length);

            var d8embed = new DiscordEmbedBuilder
            {
                Title = "Roll a four-sided dice!",
                Description = Answer[dIndex]
            };

            var RollEightMessage = await ctx.Channel.SendMessageAsync(embed: d8embed).ConfigureAwait(false);
        }

        [Command("d10")]
        [Description("Rolls a ten-sided dice")]

        public async Task d10(CommandContext ctx)
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

        [Command("d12")]
        [Description("Rolls a twelve-sided dice")]

        public async Task d12(CommandContext ctx)
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

        [Command("d20")]
        [Description("Rolls a twenty-sided dice")]

        public async Task d20(CommandContext ctx)
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
    }
}
