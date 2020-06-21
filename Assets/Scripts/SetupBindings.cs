using Zenject;

public class SetupBindings : MonoInstaller
{
    public BulletPool bulletPool;
    public FeedbackSystem feedBackSystem;
    public Inventory inventory;
    public InteractionSystem interactionSystem;

    public override void InstallBindings()
    {
        Container.Bind<BulletPool>().FromInstance(bulletPool).AsSingle();
        Container.Bind<FeedbackSystem>().FromInstance(feedBackSystem).AsSingle();
        Container.Bind<Inventory>().FromInstance(inventory).AsSingle();
        Container.Bind<InteractionSystem>().FromInstance(interactionSystem).AsSingle();
    }
}