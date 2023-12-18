using System;
using System.Collections.Generic;
using Content.Blocks.MovementBlocks;
using UnityEngine;

namespace Content
{
    public class ShipContainer
    {
        public bool isPlayer;
        public float angularAcceleration;
        public Vector2 angularVelocity;
        public List<Block> blocks; //blocks will consist of an array of prefab blocks
        public GameObject core;
        public Vector2 linearAcceleration;
        public float mass;
        public Vector2 potentialThrustContribution;
        public String shipName;

        //todo think of implementation of the arrays of attachpoints?
        //ideas - check array against the array of attach points on new object, - remove clones
        
        public ShipContainer(string shipName, GameObject core, List<Block> blocks)
        {
            this.shipName = shipName;
            this.core = core;
            this.blocks = blocks;
            CalculateLinearAcceleration();
            CalculateAngularAcceleration();
        }

        public void AddBlock(Block block) //to be used in the editor
        {
            blocks.Add(block);
        }

        public void UpdateShip(Vector2 playerLinearInput, float playerAngularInput)
        {
            CalculateMass();
            UpdateLinearAcceleration(playerLinearInput);
            UpdateAngularAcceleration(playerAngularInput);
        }

        private void CalculateMass()
        {
            foreach (Block block in blocks)
            {
                mass += block.mass;
            }
        }

        private void UpdateLinearAcceleration(Vector2 playerInput) //todo does this work?
        {
            // Use the precalculated potential thrust contribution directly
            linearAcceleration += potentialThrustContribution * playerInput;
        }

        private void UpdateAngularAcceleration(float playerInput)
        {
            foreach (Block block in blocks)
            {
                if (block.GetType() == typeof(GyroscopeBlock))
                {
                    angularAcceleration += playerInput * angularAcceleration / mass;
                }
            }
        }

        private void CalculateLinearAcceleration() //todo does this work?
        {
            foreach (Block block in blocks)
            {
                if (block.GetType() == typeof(ThrusterBlock))
                {
                    ThrusterBlock thrusterBlock = (ThrusterBlock)block;

                    // Calculate thrust contribution based on the block's thrust power
                    Vector2 thrustContribution = thrusterBlock.transform.position - core.transform.position;
                    thrustContribution.Normalize();
                    thrustContribution *= thrusterBlock.thrustPower / mass;

                    // Accumulate potential thrust contributions
                    potentialThrustContribution += thrustContribution;
                }
            }
        }

        private void CalculateAngularAcceleration()
        {
            foreach (Block block in blocks)
            {
                if (block.GetType() == typeof(GyroscopeBlock))
                {
                    angularAcceleration += ((GyroscopeBlock)block).rotationPower;
                }
            }
        }
    }
}