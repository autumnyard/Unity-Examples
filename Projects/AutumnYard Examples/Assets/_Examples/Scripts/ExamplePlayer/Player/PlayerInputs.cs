
namespace AutumnYard.ExamplePlayer.Input
{
    public struct PlayerInputs
    {
        public readonly float h;
        public readonly float v;
        public readonly bool attackPressed;
        public readonly bool attackMaintained;
        public readonly bool defensePressed;
        public readonly bool defenseMaintained;
        public readonly bool jumpPressed;
        public readonly bool jumpMaintained;

        public PlayerInputs(
            float h, float v,
            bool attackPressed, bool attackMaintained,
            bool defensePressed, bool defenseMaintained,
            bool jumpPressed, bool jumpMaintained
            )
        {
            this.h = h;
            this.v = v;
            this.attackPressed = attackPressed;
            this.attackMaintained = attackMaintained;
            this.defensePressed = defensePressed;
            this.defenseMaintained = defenseMaintained;
            this.jumpPressed = jumpPressed;
            this.jumpMaintained = jumpMaintained;
        }

        public bool AnyInput =>
            h != 0f || v != 0f
            || attackPressed != false || attackMaintained != false
            || defensePressed != false || defenseMaintained != false
            || jumpPressed != false || jumpMaintained != false;

        public override string ToString()
            => $"" +
            $"({h},{v}) " +
            $"Attack:[{attackPressed} ({attackMaintained}) " +
            $"Defense:[{defensePressed} ({defenseMaintained})] " +
            $"Jump:[{jumpPressed} ({jumpMaintained})]], ";

        public void Print(UnityEngine.ILogger logger)
        {
            //logger.Log("Logging PlayerInputs:");
            if (h != 0f || v != 0f) logger.Log($" Movement axis. Horizontal: {h}. Vertical: {v}");
            if (attackPressed != false || attackMaintained != false) logger.Log($" Attack. Pressed: {attackPressed}. Maintained: {attackMaintained}");
            if (defensePressed != false || defenseMaintained != false) logger.Log($" Defense. Pressed: {defensePressed}. Maintained: {defenseMaintained}");
            if (jumpPressed != false || jumpMaintained != false) logger.Log($" Jump. Pressed: {jumpPressed}. Maintained: {jumpMaintained}");
        }
    }
}
