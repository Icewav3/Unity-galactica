using System;
using System.Collections.Generic;
using Content.Blocks.MovementBlocks;
using UnityEngine;

namespace Content
{
    public class ShipContainer //TODO
    {
        public float angularAcceleration;
        public Vector2 angularVelocity;
        public List<Block> blocks; //blocks will consist of an array of prefab blocks
        public GameObject core;
        public Vector2 linearAcceleration;
        public Vector2 linearVelocity;
        public float mass;
        public String shipName;

        public ShipContainer(string shipName, GameObject core, List<Block> blocks)
        {
            this.shipName = shipName;
            this.core = core;
            this.blocks = blocks;
            CalculateLinearAcceleration();
            CalculateAngularAcceleration();
        }
        public void UpdateShip(Vector2 playerLinearInput, float playerAngularInput)
        {
            UpdateMass();
            UpdateLinearAcceleration(playerLinearInput);
            UpdateAngularAcceleration(playerAngularInput);
        }

        private void UpdateMass()
        {
            foreach (Block block in blocks)
            {
                mass += block.mass;
            }
        }

        private void UpdateLinearAcceleration(Vector2 playerInput)
        {
            foreach (Block block in blocks)
            {
                if (block.GetType() == typeof(ThrusterBlock))
                {
                    ThrusterBlock thrusterBlock = (ThrusterBlock)block;
                    
                    // Calculate thrust contribution based on the block's thrust power
                    Vector2 thrustContribution = playerInput * thrusterBlock.thrustPower / mass;

                    // Add the thrust contribution to linear acceleration
                    linearAcceleration += thrustContribution;
                }
            }
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
        private void CalculateLinearAcceleration()
        {
            linearVelocity += linearAcceleration;

            foreach (Block block in blocks)
            {
                if (block.GetType() == typeof(ThrusterBlock))
                {
                    ThrusterBlock thrusterBlock = (ThrusterBlock)block;
                    
                    // Adjust linear acceleration based on thrust power in a given direction relative to the core
                    Vector2 directionToThruster = thrusterBlock.transform.position - core.transform.position;
                    float dotProduct = Vector2.Dot(directionToThruster.normalized, linearAcceleration.normalized);
                    
                    // Scale the acceleration based on the alignment of the thruster and the current acceleration
                    linearAcceleration *= Mathf.Max(dotProduct, 0f);
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