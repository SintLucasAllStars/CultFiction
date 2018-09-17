public class TurretObjectManager : StarWarsManager {

    public override void InitializeObject(int index) {

        if (IsSpawnable) {
            base.InitializeObject(index);

            StartCoroutine(Grid.GridInstance.PlaceObjects(ObjectList[0].gameObject));
        }
    }
}
