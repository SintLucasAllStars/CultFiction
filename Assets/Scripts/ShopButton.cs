public class ShopButton : GameButton
{
    public override void Press()
    {
        if (ObjectManager.Instance.InObjectMode())
        {
            ErrorMessage.Instance.AnnounceError("Please place your object before buying another one.");
            return;
        }

        base.Press();
        Shop.Instance.ToggleShop();
    }
}