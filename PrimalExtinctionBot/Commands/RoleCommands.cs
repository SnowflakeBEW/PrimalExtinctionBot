using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using Microsoft.EntityFrameworkCore;
using PrimalExtinction.Attributes;
using PrimalExtinctionBot.DAL;
using PrimalExtinctionBot.DAL.Models.Items;
using System;
using System.Threading.Tasks;

namespace PrimalExtinction.Commands
{
    public class RoleCommands : BaseCommandModule
    {
            [Command("Tester")]
            [Description("Grants you the Bot Tester Role")]
            [RequireCategories(ChannelCheckMode.Any, "Bots")]
            public async Task Tester(CommandContext ctx)
            {
                var joinEmbed = new DiscordEmbedBuilder
                {
                    Title = "Would you like to join the bot testers?",
                    ThumbnailUrl = ctx.Client.CurrentUser.AvatarUrl,
                    Color = DiscordColor.Green
                };

                var joinMessage = await ctx.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);

                var thumbsupEmoji = DiscordEmoji.FromName(ctx.Client, ":+1:");
                var thumbsdownEmoji = DiscordEmoji.FromName(ctx.Client, ":-1:");

                await joinMessage.CreateReactionAsync(thumbsupEmoji).ConfigureAwait(false);
                await joinMessage.CreateReactionAsync(thumbsdownEmoji).ConfigureAwait(false);

                var interactivity = ctx.Client.GetInteractivity();

                var reactionResult = await interactivity.WaitForReactionAsync(
                    x => x.Message == joinMessage &&
                    x.User == ctx.User &&
                   (x.Emoji == thumbsupEmoji || x.Emoji == thumbsdownEmoji)).ConfigureAwait(false);

                if (reactionResult.Result.Emoji == thumbsupEmoji)
                {
                    var BotTesterRole = ctx.Guild.GetRole(668117961630023690);
                    await ctx.Member.GrantRoleAsync(BotTesterRole).ConfigureAwait(false);
                }
                else if (reactionResult.Result.Emoji == thumbsdownEmoji)
                {
                    var BotTesterRole = ctx.Guild.GetRole(668117961630023690);
                    await ctx.Member.RevokeRoleAsync(BotTesterRole).ConfigureAwait(false);
                }
                else
                {
                    // Do Nothing
                }

                await joinMessage.DeleteAsync().ConfigureAwait(false);
            }


            [Command("Pingable")]
            [Description("Grants you the Pingable Role")]
            [RequireCategories(ChannelCheckMode.Any, "Bots")]
            public async Task Pingable(CommandContext ctx)
            {
                var joinEmbed = new DiscordEmbedBuilder
                {
                    Title = "Would you like to gain the Pingable role?",
                    ThumbnailUrl = ctx.Client.CurrentUser.AvatarUrl,
                    Color = DiscordColor.Green
                };

                var joinMessage = await ctx.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);

                var thumbsupEmoji = DiscordEmoji.FromName(ctx.Client, ":+1:");
                var thumbsdownEmoji = DiscordEmoji.FromName(ctx.Client, ":-1:");

                await joinMessage.CreateReactionAsync(thumbsupEmoji).ConfigureAwait(false);
                await joinMessage.CreateReactionAsync(thumbsdownEmoji).ConfigureAwait(false);

                var interactivity = ctx.Client.GetInteractivity();

                var reactionResult = await interactivity.WaitForReactionAsync(
                    x => x.Message == joinMessage &&
                    x.User == ctx.User &&
                   (x.Emoji == thumbsupEmoji || x.Emoji == thumbsdownEmoji)).ConfigureAwait(false);

                if (reactionResult.Result.Emoji == thumbsupEmoji)
                {
                    var PingableRole = ctx.Guild.GetRole(551267646415568896);
                    await ctx.Member.GrantRoleAsync(PingableRole).ConfigureAwait(false);
                }
                else if (reactionResult.Result.Emoji == thumbsdownEmoji)
                {
                    var PingableRole = ctx.Guild.GetRole(551267646415568896);
                    await ctx.Member.RevokeRoleAsync(PingableRole).ConfigureAwait(false);
                }
                else
                {
                    // Do Nothing
                }

                await joinMessage.DeleteAsync().ConfigureAwait(false);
            }


            [Command("PollWatcher")]
            [Description("Grants you the Pingable Role")]
            [RequireCategories(ChannelCheckMode.Any, "Bots")]
            public async Task PollWatcher(CommandContext ctx)
            {
                var joinEmbed = new DiscordEmbedBuilder
                {
                    Title = "Would you like to gain the Poll-Watcher role?",
                    ThumbnailUrl = ctx.Client.CurrentUser.AvatarUrl,
                    Color = DiscordColor.Green
                };

                var joinMessage = await ctx.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);

                var thumbsupEmoji = DiscordEmoji.FromName(ctx.Client, ":+1:");
                var thumbsdownEmoji = DiscordEmoji.FromName(ctx.Client, ":-1:");

                await joinMessage.CreateReactionAsync(thumbsupEmoji).ConfigureAwait(false);
                await joinMessage.CreateReactionAsync(thumbsdownEmoji).ConfigureAwait(false);

                var interactivity = ctx.Client.GetInteractivity();

                var reactionResult = await interactivity.WaitForReactionAsync(
                    x => x.Message == joinMessage &&
                    x.User == ctx.User &&
                   (x.Emoji == thumbsupEmoji || x.Emoji == thumbsdownEmoji)).ConfigureAwait(false);

                if (reactionResult.Result.Emoji == thumbsupEmoji)
                {
                    var PollWatcherRole = ctx.Guild.GetRole(608326560650493952);
                    await ctx.Member.GrantRoleAsync(PollWatcherRole).ConfigureAwait(false);
                }
                else if (reactionResult.Result.Emoji == thumbsdownEmoji)
                {
                    var PollWatcherRole = ctx.Guild.GetRole(608326560650493952);
                    await ctx.Member.RevokeRoleAsync(PollWatcherRole).ConfigureAwait(false);
                }
                else
                {
                    // Do Nothing
                }

                await joinMessage.DeleteAsync().ConfigureAwait(false);
            }

            [Command("Available")]
            [Description("Changes Your Staff Availability")]
            [RequireCategories(ChannelCheckMode.Any, "Staff Only")]
            public async Task Available(CommandContext ctx)
            {
                var joinEmbed = new DiscordEmbedBuilder
                {
                    Title = "Would you like to become available or unavailable?",
                    ThumbnailUrl = ctx.Client.CurrentUser.AvatarUrl,
                    Color = DiscordColor.Green
                };

                var joinMessage = await ctx.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);

                var discordemojiGreenCheck = DiscordEmoji.FromName(ctx.Client, ":white_check_mark:");
                var discordemojiGreenX = DiscordEmoji.FromName(ctx.Client, ":negative_squared_cross_mark:");

                await joinMessage.CreateReactionAsync(discordemojiGreenCheck).ConfigureAwait(false);
                await joinMessage.CreateReactionAsync(discordemojiGreenX).ConfigureAwait(false);

                var interactivity = ctx.Client.GetInteractivity();

                var reactionResult = await interactivity.WaitForReactionAsync(
                    x => x.Message == joinMessage &&
                    x.User == ctx.User &&
                   (x.Emoji == discordemojiGreenCheck || x.Emoji == discordemojiGreenX)).ConfigureAwait(false);

                if (reactionResult.Result.Emoji == discordemojiGreenCheck)
                {
                    var AvailableModeratorRole = ctx.Guild.GetRole(593495351562993703);
                    await ctx.Member.GrantRoleAsync(AvailableModeratorRole).ConfigureAwait(false);
                    var UnavailableModeratorRole = ctx.Guild.GetRole(593495472166011053);
                    await ctx.Member.RevokeRoleAsync(UnavailableModeratorRole).ConfigureAwait(false);
                }

                else if (reactionResult.Result.Emoji == discordemojiGreenX)
                {
                    var UnavailableModeratorRole = ctx.Guild.GetRole(593495472166011053);
                    await ctx.Member.GrantRoleAsync(UnavailableModeratorRole).ConfigureAwait(false);
                    var AvailableModeratorRole = ctx.Guild.GetRole(593495351562993703);
                    await ctx.Member.RevokeRoleAsync(AvailableModeratorRole).ConfigureAwait(false);
                }
                else
                {
                    // Do Nothing
                }

                await joinMessage.DeleteAsync().ConfigureAwait(false);
            }
    }
}