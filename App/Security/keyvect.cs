using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGenCSharp.App.Security {
    class keyvect {
        public static byte[] getKey() {
            return new byte[] {
                0xA1, 0xBB, 0x01, 0x32, 0xA8, 0x98, 0x97, 0xCF,
                0x0C, 0xC2, 0x12, 0x22, 0x91, 0x42, 0x55, 0xAB
            };
        }

        public static byte[] getVect() {
            return new byte[] {
                14, 114, 191, 121, 11, 4, 223, 111, 223, 121, 111, 111, 88, 32, 111, 136
            };
        }
    }
}
