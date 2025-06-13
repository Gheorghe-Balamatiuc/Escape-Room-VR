using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DoorOpenerTests
{
    [Test]
    public void DoorOpener_InitializesCorrectly_WhenDoorTransformProvided()
    {
        var gameObject = new GameObject();
        var doorOpener = gameObject.AddComponent<DoorOpener>();
        var doorObject = new GameObject("Door");
        doorOpener.doorTransform = doorObject.transform;

        doorOpener.Start();

        Assert.IsNotNull(doorOpener.doorTransform);
        Assert.AreEqual(doorObject.transform, doorOpener.doorTransform);
    }

    [Test]
    public void DoorOpener_InitializesCorrectly_WhenDoorTransformNotProvided()
    {
        var parentObject = new GameObject("Parent");
        var doorObject = new GameObject("Door");
        doorObject.transform.SetParent(parentObject.transform);
        var doorOpener = parentObject.AddComponent<DoorOpener>();

        doorOpener.Start();

        Assert.IsNotNull(doorOpener.doorTransform);
        Assert.AreEqual(doorObject.transform, doorOpener.doorTransform);
    }

    [Test]
    public void DoorOpener_InitializesCorrectly_WhenNoChildrenAndNoDoorTransform()
    {
        var gameObject = new GameObject();
        var doorOpener = gameObject.AddComponent<DoorOpener>();

        doorOpener.Start();

        Assert.IsNotNull(doorOpener.doorTransform);
        Assert.AreEqual(gameObject.transform, doorOpener.doorTransform);
    }

    [UnityTest]
    public IEnumerator DoorOpener_OpenDoor_StartsDoorMovement()
    {
        var gameObject = new GameObject();
        var doorOpener = gameObject.AddComponent<DoorOpener>();
        doorOpener.Start();

        doorOpener.OpenDoor();

        yield return null;

        var isOpeningField = typeof(DoorOpener).GetField("isOpening",
                                                System.Reflection.BindingFlags.NonPublic |
                                                System.Reflection.BindingFlags.Instance);
        bool isOpening = (bool)isOpeningField.GetValue(doorOpener);

        Assert.IsTrue(isOpening, "Door should be in 'opening' state after OpenDoor is called");
    }






    [UnityTest]
    public IEnumerator DoorOpener_DoorMovesToTargetPosition_WhenOpened()
    {
        var gameObject = new GameObject();
        var doorOpener = gameObject.AddComponent<DoorOpener>();
        doorOpener.openSpeed = 10f;
        doorOpener.Start();

        yield return null;

        var expectedRotation = doorOpener.doorTransform.localRotation * Quaternion.Euler(doorOpener.openEulerAngles);
        var initialPosition = doorOpener.doorTransform.localPosition;
        var expectedPosition = new Vector3(
            doorOpener.openLocalPosition.x,
            initialPosition.y,
            doorOpener.openLocalPosition.z);

        doorOpener.OpenDoor();

        float waitTime = 2f;
        float timer = 0f;

        while (timer < waitTime)
        {
            yield return null;
            timer += Time.deltaTime;
        }

        Assert.Less(Quaternion.Angle(doorOpener.doorTransform.localRotation, expectedRotation), 90.1f);
        Assert.Less(Vector3.Distance(doorOpener.doorTransform.localPosition, expectedPosition), 2.5f);
    }

}
