using System;
using System.Collections.Generic;
using Content.Blocks.MovementBlocks;
using UnityEngine;

namespace Content
{
    public class ShipContainer //TODO
    {
        public Vector2 angularAcceleration;
        public Vector2 angularVelocity;
        public List<Block> blocks; //blocks will consist of an array of prefab blocks
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

        public void UpdateShip(Vector2 playerLinearInput, Vector2 playerAngularInput)
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

        private void UpdateLinearAcceleration(Vector2 inputForce)
        {
            foreach (Block block in blocks)
            {
                if (block.GetType() == typeof(ThrusterBlock))
                {
                    linearAcceleration += inputForce / mass;
                }
            }
        }

        private void UpdateAngularAcceleration(Vector2 inputForce)
        {
            foreach (Block block in blocks)
            {
                if (block.GetType() == typeof(GyroscopeBlock))
                {
                    angularAcceleration += inputForce / mass;
                }
            }
        }
    }
}