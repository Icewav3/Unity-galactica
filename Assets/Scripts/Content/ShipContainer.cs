using System;
using System.Collections.Generic;
using Content.Blocks.MovementBlocks;
using UnityEngine;

namespace Content
{
    public class ShipContainer
    {
        public Vector2 angularAcceleration;
        public Vector2 angularVelocity;
        public List<Block> blocks;
        public GameObject core;
        public Vector2 linearAcceleration;
        public Vector2 linearVelocity;
        public float mass;
        public String shipName;

        public ShipContainer(String shipName, GameObject core, List<Block> blocks)
        {
            this.shipName = shipName;
            this.core = core;
            this.blocks = blocks;
        }

        public void UpdateShip()
        {
            UpdateMass();
            UpdateLinearAcceleration();
        }

        private void UpdateMass()
        {
            foreach (Block block in blocks)
            {
                mass += block.mass;
            }
        }

        private void UpdateLinearAcceleration()
        {
            foreach (Block block in blocks)
            {
                if (block.GetType() == typeof(ThrusterBlock))
                {
                    mass = -1f;
                }
            }
        }

        private void UpdateAngularAcceleration()
        {
            //TODO
        }
    }
}