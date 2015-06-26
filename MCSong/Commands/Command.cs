/*
	Copyright 2010 MCSharp team (Modified for use with MCZall/MCSong) Licensed under the
	Educational Community License, Version 2.0 (the "License"); you may
	not use this file except in compliance with the License. You may
	obtain a copy of the License at
	
	http://www.osedu.org/licenses/ECL-2.0
	
	Unless required by applicable law or agreed to in writing,
	software distributed under the License is distributed on an "AS IS"
	BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
	or implied. See the License for the specific language governing
	permissions and limitations under the License.
*/
using System;
using System.Collections.Generic;

namespace MCSong
{
    public enum CommandType
    {
        Building,
        Moderation,
        Information,
        Other
    }

    public abstract class Command
    {
        public abstract string name { get; }
        public abstract string[] aliases { get; }
        public abstract CommandType type { get; }
        public abstract bool museumUsable { get; }
        public abstract bool consoleUsable { get; }
        public abstract LevelPermission defaultRank { get; }
        public abstract void Use(Player p, string message);
        public abstract void Help(Player p);

        public static CommandList all = new CommandList();
        public static CommandList core = new CommandList();
        public static void InitAll()
        {
            // Development Commands - DO NOT USE
            all.Add(new CmdHeartbeat());// Dev-only perms
            //all.Add(new CmdListcommands());

            // Building Commands
            all.Add(new CmdAbort());
            all.Add(new CmdBind());
            all.Add(new CmdClick());
            all.Add(new CmdCmdBind());
            all.Add(new CmdCopy());
            all.Add(new CmdCopy());
            all.Add(new CmdCuboid());
            all.Add(new CmdDelete());
            all.Add(new CmdDrill());
            all.Add(new CmdFill());
            all.Add(new CmdHollow());
            all.Add(new CmdImageprint());
            all.Add(new CmdLine());
            all.Add(new CmdMegaboid());
            all.Add(new CmdMessageBlock());
            all.Add(new CmdMode());
            all.Add(new CmdOutline());
            all.Add(new CmdPaint());
            all.Add(new CmdPaste());
            all.Add(new CmdPlace());
            all.Add(new CmdPlace());
            all.Add(new CmdPlace());
            all.Add(new CmdPortal());
            all.Add(new CmdRedo());
            all.Add(new CmdReplace());
            all.Add(new CmdReplaceAll());
            all.Add(new CmdReplaceNot());
            all.Add(new CmdRestartPhysics());
            all.Add(new CmdRetrieve());
            all.Add(new CmdSpheroid());
            all.Add(new CmdSpin());
            all.Add(new CmdStairs());
            all.Add(new CmdStatic());
            all.Add(new CmdStore());
            all.Add(new CmdTree());
            all.Add(new CmdUndo());
            all.Add(new CmdWrite());

            // Moderation Commands
            all.Add(new CmdBan());
            all.Add(new CmdBanip());
            all.Add(new CmdBlockSet());
            all.Add(new CmdBotAdd());
            all.Add(new CmdBotRemove());
            all.Add(new CmdBotSummon());
            all.Add(new CmdClearBlockChanges());
            all.Add(new CmdClickdistance());
            all.Add(new CmdCmdSet());
            all.Add(new CmdCrashServer());
            all.Add(new CmdDeleteLvl());
            all.Add(new CmdDemote());
            all.Add(new CmdFixGrass());
            all.Add(new CmdFollow());
            all.Add(new CmdFreeze());
            all.Add(new CmdHacks());
            all.Add(new CmdHide());
            all.Add(new CmdHighlight());
            all.Add(new CmdImport());
            all.Add(new CmdJail());
            all.Add(new CmdJoker());
            all.Add(new CmdKick());
            all.Add(new CmdKickban());
            all.Add(new CmdLimit());
            all.Add(new CmdLoad());
            all.Add(new CmdLowlag());
            all.Add(new CmdMaintenance());
            all.Add(new CmdMap());
            all.Add(new CmdModerate());
            all.Add(new CmdMute());
            all.Add(new CmdNewLvl());
            all.Add(new CmdPause());
            all.Add(new CmdPermissionBuild());
            all.Add(new CmdPermissionVisit());
            all.Add(new CmdPossess());
            all.Add(new CmdPromote());
            all.Add(new CmdRenameLvl());
            all.Add(new CmdResetBot());
            all.Add(new CmdRestart());
            all.Add(new CmdRestore());
            all.Add(new CmdReveal());
            all.Add(new CmdSave());
            all.Add(new CmdSetRank());
            all.Add(new CmdSetspawn());
            all.Add(new CmdShutdown());
            all.Add(new CmdTempBan());
            all.Add(new CmdTrust());
            all.Add(new CmdUnban());
            all.Add(new CmdUnbanip());
            all.Add(new CmdUnload());
            all.Add(new CmdVoice());
            if (Server.useWhitelist) all.Add(new CmdWhitelist());
            all.Add(new CmdZone());

            // Information Commands
            all.Add(new CmdAbout());
            all.Add(new CmdAfk());
            all.Add(new CmdAliases());
            all.Add(new CmdBlocks());
            all.Add(new CmdClones());
            all.Add(new CmdDevs());
            all.Add(new CmdExtensions());
            all.Add(new CmdGCRules());
            all.Add(new CmdHasirc());
            all.Add(new CmdHelp());
            all.Add(new CmdHost());
            all.Add(new CmdInbox());
            all.Add(new CmdInfo());
            all.Add(new CmdLastCmd());
            all.Add(new CmdLevels());
            all.Add(new CmdMapInfo());
            all.Add(new CmdMeasure());
            all.Add(new CmdPCount());
            all.Add(new CmdPhysics());
            all.Add(new CmdPlayers());
            all.Add(new CmdRules());
            all.Add(new CmdServerReport());
            all.Add(new CmdTime());
            all.Add(new CmdUnloaded());
            all.Add(new CmdUpdate());
            all.Add(new CmdViewRanks());
            all.Add(new CmdWhoip());
            all.Add(new CmdWhowas());

            // Other Commands
            all.Add(new CmdAdminChat());
            all.Add(new CmdAgree());
            all.Add(new CmdAward());
            all.Add(new CmdAwardMod());
            all.Add(new CmdAwards());
            all.Add(new CmdBotAI());
            all.Add(new CmdBots());
            all.Add(new CmdBotSet());
            all.Add(new CmdClearchat());
            all.Add(new CmdCmdCreate());
            all.Add(new CmdCmdLoad());
            all.Add(new CmdCmdUnload());
            all.Add(new CmdCompile());
            all.Add(new CmdCTF());
            all.Add(new CmdDrop());
            all.Add(new CmdEmote());
            all.Add(new CmdFlipHeads());
            all.Add(new CmdFly());
            all.Add(new CmdGive());
            all.Add(new CmdGoto());
            all.Add(new CmdGun());
            all.Add(new CmdInvincible());
            all.Add(new CmdKill());
            all.Add(new CmdMe());
            all.Add(new CmdMissile());
            all.Add(new CmdMove());
            all.Add(new CmdMuseum());
            all.Add(new CmdOpChat());
            all.Add(new CmdPay());
            all.Add(new CmdRainbow());
            all.Add(new CmdRepeat());
            all.Add(new CmdRide());
            all.Add(new CmdPlugin());
            all.Add(new CmdRoll());
            all.Add(new CmdSay());
            all.Add(new CmdSend());
            all.Add(new CmdSlap());
            all.Add(new CmdSpawn());
            all.Add(new CmdSummon());
            all.Add(new CmdTake());
            all.Add(new CmdTColor());
            all.Add(new CmdTeam());
            all.Add(new CmdText());
            all.Add(new CmdTimer());
            all.Add(new CmdTitle());
            all.Add(new CmdTnt());
            all.Add(new CmdTp());
            all.Add(new CmdTpZone());
            all.Add(new CmdView());
            all.Add(new CmdWhisper());
            all.Add(new CmdWhois());


            core.commands = new List<Command>(all.commands);

            Scripting.Autoload();
        }
    }
}