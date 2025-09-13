using System;
using Oxide.Core.Plugins;
using System.Linq;
using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("NameFilter", "headtapper", "1.0.0")]
    [Description("Allow connections from players with usernames containing only English alphanumeric characters. ")]

    public class NameFilter : RustPlugin
    {
        #region Initialization

        private const string NameFilterBypassPermission = "namefilter.bypass";

        private void Init()
        {
            permission.RegisterPermission(NameFilterBypassPermission, this);
        }

        #endregion

        #region Localization

        protected override void LoadDefaultMessages()
        {
            lang.RegisterMessages(new Dictionary<string, string>
            {
                ["KickMessage"] = "Your name must only contain English alphanumeric characters.",
            }, this);
        }

        #endregion

        #region Hooks

        private void OnPlayerConnected(BasePlayer player)
        {
            if (!player.displayName.All(char.IsLetterOrDigit) && !permission.UserHasPermission(player.UserIDString, NameFilterBypassPermission))
                player.Kick(lang.GetMessage("KickMessage", this, player.UserIDString));
        }

        # endregion
    }
}

