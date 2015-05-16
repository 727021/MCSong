using System;

namespace MCSong
{
    public class CmdWhisper : Command
    {
        public override string name { get { return "whisper"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override CommandType type { get { return CommandType.Other; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdWhisper() { }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                p.whisper = !p.whisper; p.whisperTo = "";
                if (p.whisper) Player.SendMessage(p, "All messages sent will now auto-whisper");
                else Player.SendMessage(p, "Whisper chat turned off");
            }
            else
            {
                Player who = Player.Find(message);
                if (who == null) { p.whisperTo = ""; p.whisper = false; Player.SendMessage(p, "Could not find player."); return; }

                p.whisper = true;
                p.whisperTo = who.name;
                Player.SendMessage(p, "Auto-whisper enabled.  All messages will now be sent to " + who.name + ".");
            }


        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/whisper <name> - Makes all messages act like whispers");
        }
    }
}