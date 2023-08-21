using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public static class Player //client
    {
        private static bool _host;
        private static int _id;
        public static void Create(int id, bool host)
        {
            _host = host;
            _id = id;
        }

        public static String Menu()
        {
            if (_host)
            {
                return "player is host";
            }
            else
            {
                return "player is not host";
            }
        }
    }
}
