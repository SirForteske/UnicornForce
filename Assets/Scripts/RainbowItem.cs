using Player;

public class RainbowItem : Item
{
    protected override void Pick()
    {
        base.Pick();
        PlayerScript.instance.AddPower(value);
    }
}
