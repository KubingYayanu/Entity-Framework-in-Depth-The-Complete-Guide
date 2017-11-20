using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queries.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Test
{
    [TestClass]
    public class StringCipherTest
    {
        [TestMethod]
        public void EncryptAES_HexString_Test()
        {
            // arrange
            bool uppercase = true;
            string source = "1qaz!QAZ";
            string expected = uppercase ? "D3840132D0361C3B70D18B449D40FEB5" : "d3840132d0361c3b70d18b449d40feb5";

            // act
            string target = StringCipher.EncryptAES(source, uppercase);
            
            // assert
            Assert.AreEqual(expected, target);
        }

        [TestMethod]
        public void DecryptAES_HexString_Test()
        {
            // arrange
            bool uppercase = false;
            string source = uppercase ? "905D3CB89F58282EE3DA55B3F48CC8BC" : "905d3cb89f58282ee3da55b3f48cc8bc";
            string expected = "FuckYouIdiot";

            // act
            string target = StringCipher.DecryptAES(source);

            // assert
            Assert.AreEqual(expected, target);
        }


    }
}
