﻿using System;
using System.Collections.Generic;
using Content.Blocks.MovementBlocks;
using UnityEngine;

namespace Content
{
    //TODO IMPLEMENT SRP & SPLIT INTO RIGIDBODYCONTAINER AND OTHER NEEDED CLASSES?
    //don't even need a dictionary of colliders. the Collision paramter for OnCollisionEnter literally has *both* colliders involved in the collision. just get the collider from this object and pass the message along or just call a method on one of its components
    //TODO https://discord.com/channels/489222168727519232/763495187787677697/1212488440566911007
    /// <summary>
    /// Represents a container for a ship in the game.
    /// </summary>
    /// <remarks>
    /// Purpose is to consolidate all data that is contained in each block to one location to allow for faster execution
    /// </remarks>
    public class ShipContainer : MonoBehaviour
    {
        public Rigidbody2D rb;

        /// <summary>
        /// Represents the angular acceleration of an object.
        /// </summary>
        public float AngularAcceleration;

        /// <summary>
        /// Represents the angular velocity of an object.
        /// </summary>
        public Vector2 AngularVelocity;

        /// <summary>
        /// List of block objects.
        /// </summary>
        public List<Block> Blocks;

        /// <summary>
        /// The core GameObject in the scene.
        /// </summary>
        public GameObject Core;

        /// <summary>
        /// Indicates whether the entity is a player.
        /// </summary>
        /// <value>
        /// <c>true</c> if the entity is a player; otherwise, <c>false</c>.
        /// </value>
        public bool IsPlayer;

        /// <summary>
        /// Represents the linear acceleration of an object in a 2D space.
        /// </summary>
        public Vector2 LinearAcceleration;

        /// <summary>
        /// Represents the mass of an object.
        /// </summary>
        /// <remarks>
        /// The mass is measured in kilograms.
        /// </remarks>
        public float Mass;

        /// <summary>
        /// The potential thrust contribution vector.
        /// </summary>
        public Vector2 PotentialThrustContribution;

        /// <summary>
        /// The name of the ship.
        /// </summary>
        /// <remarks>
        /// This variable stores the name of the ship.
        /// </remarks>
        public String ShipName;

        /// <summary>
        /// Represents a container object for a ship in a game.
        /// </summary>
        public ShipContainer(string shipName)
        {
            this.ShipName = shipName;
            InitializeShip();
        }

        void Start()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D not found on ship");
            }

            InitializeShip();
        }

        /// <summary>
        /// Initializes the ship by calculating linear and angular acceleration. To be used when the ship is done being edited
        /// </summary>
        public void InitializeShip()
        {
            if (IsPlayer)
            {
                if (Camera.main != null) Camera.main.transform.parent = this.transform;
                if (ShipName == null) name = "myship"; //todo temp
            }

            GameObject core = Resources.Load<GameObject>("Prefabs/Core");
            Core = Instantiate(core, this.transform, true);
            CalculateMass();
            CalculateLinearAcceleration();
            CalculateAngularAcceleration();
        }

        /// <summary>
        /// Adds a block to the editor.
        /// </summary>
        /// <param name="block">The block to be added.</param>
        /// <param name="position">the position to place this block</param>
        public void AddBlock(Block block, Vector2 position) //to be used in the editor
        {
            Block newBlock = Instantiate(block, position, Quaternion.identity);
            newBlock.transform.SetParent(this.transform);
            Blocks.Add(newBlock);
        }

        /// <summary>
        /// Updates the ship's state based on the player input.
        /// </summary>
        /// <param name="playerLinearInput">The linear input from the player.</param>
        /// <param name="playerAngularInput">The angular input from the player.</param>
        public void UpdateShip(Vector2 playerLinearInput,
            float playerAngularInput)
        {
            UpdateLinearAcceleration(playerLinearInput);
            UpdateAngularAcceleration(playerAngularInput);
        }

        /// <summary>
        /// Calculates the total mass of all blocks in the given collection.
        /// </summary>
        private void CalculateMass()
        {
            Mass = 0;
            foreach (Block block in Blocks)
            {
                Mass += block.blockConfig.mass;
            }
        }

        /// <summary>
        /// Updates the linear acceleration based on the player input.
        /// </summary>
        /// <param name="playerInput">The player input vector.</param>
        private void UpdateLinearAcceleration(Vector2 playerInput) //todo does this work?
        {
            // Use the precalculated potential thrust contribution directly
            LinearAcceleration += PotentialThrustContribution * playerInput;
        }

        /// <summary>
        /// Updates the angular acceleration of the gyroscope blocks in the given list of blocks. </summary>
        /// <param name="playerInput">The input value from the player.</param>
        /// <remarks>
        /// This method iterates through each block in the list and checks if it is a gyroscope block.
        /// If a gyroscope block is found, the angular acceleration is updated by multiplying the player input
        /// with the current angular acceleration and dividing it by the mass of the block. </remarks>
        /// /
        private void UpdateAngularAcceleration(float playerInput)
        {
            foreach (Block block in Blocks)
            {
                if (block.GetType() == typeof(GyroscopeBlock))
                {
                    AngularAcceleration +=
                        playerInput * AngularAcceleration / Mass;
                }
            }
        }

        /// <summary>
        /// Calculates the linear acceleration based on the thrust power of each thruster block in the provided collection of blocks.
        /// </summary>
        private void CalculateLinearAcceleration()
        {
            foreach (Block block in Blocks)
            {
                if (block.GetType() == typeof(ThrusterBlock))
                {
                    ThrusterBlock thrusterBlock = (ThrusterBlock)block;

                    // Calculate thrust contribution based on the block's thrust direction
                    Vector2
                        thrustDirection =
                            thrusterBlock.transform
                                .up; // Assuming the thruster's forward direction is its "thrust" direction
                    Vector2 thrustContribution = thrustDirection *
                        thrusterBlock.thrustPower / Mass;

                    // Accumulate potential thrust contributions
                    PotentialThrustContribution += thrustContribution;
                }
            }
        }


        /// <summary>
        /// Calculates the angular acceleration by adding up the rotation power of all gyroscope blocks in the given list of blocks.
        /// </summary>
        private void CalculateAngularAcceleration()
        {
            foreach (Block block in Blocks)
            {
                if (block.GetType() == typeof(GyroscopeBlock))
                {
                    AngularAcceleration +=
                        ((GyroscopeBlock)block).rotationPower;
                }
            }
        }
    }
}