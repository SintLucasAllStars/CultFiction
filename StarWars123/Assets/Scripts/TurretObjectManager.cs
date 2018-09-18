public class TurretObjectManager : StarWarsManager {

    private void Start() {
        SetStarWarsManagerInstance();
    }

    protected override void SetStarWarsManagerInstance() {
        StarWarsManagerInstance = this;
    }

    protected override void InitializeObject(int index) {
        if (IsSpawnable) {
            base.InitializeObject(index);

            StartCoroutine(Grid.GridInstance.PlaceObjects(CurrentSpawnedObject));
        }
    }
}
