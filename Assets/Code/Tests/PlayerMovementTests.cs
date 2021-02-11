using System.Collections;
using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTests
{

    private GameObject player;

    private PlayerMovement movementController;

    [SetUp]
    public void Setup()
    {
        GameObject playerController = new GameObject("TestPlayerController");
        PlayerController pc = playerController.AddComponent(typeof(PlayerController)) as PlayerController;

        GameObject playerManager = new GameObject("TestPlayerManager");
        PlayerManager pm = playerManager.AddComponent(typeof(PlayerManager)) as PlayerManager;
        pm.Controller = pc;
        pm.InitializeSingleton();

        player = new GameObject("TestPlayer");
        movementController = player.AddComponent(typeof(PlayerMovement)) as PlayerMovement;
    }

    // Ensure the 2D movement grid doesn't allow invalid rotation values
    [Test]
    public void PlayerMovementTestsInvalidRotation()
    {
        Assert.Throws<Exception>(
        () =>
        {
            movementController.Rotation = 99;
            movementController.GetChosen2DVector(Vector3.zero);
        }
        );
    }

    // Ensure the game doesn't accept invalid directional values
    [Test]
    public void PlayerMovementTestsInvalidDirection()
    {
        Assert.Throws<Exception>(
        () =>
        {
            movementController.MoveDirection("diagonal");
        }
        );
    }

    // Ensure the game doesn't accept invalid rotation values
    [Test]
    public void PlayerMovementTestsInvalidRotateDirection()
    {
        Assert.Throws<Exception>(
        () =>
        {
            movementController.Rotation = 99;
            movementController.PlayRotateAnimation("right");
        }
        );
    }

}
