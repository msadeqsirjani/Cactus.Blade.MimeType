using Cactus.Blade.MimeType;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MimeType.Test
{
    [TestClass]
    public class MimeTest
    {
        [TestMethod]
        public void MimeTypeTest1()
        {

            var name = Mime.GetMimeType("ami");

            Assert.AreEqual(name, "application/vnd.amiga.ami");

        }

        [TestMethod]
        public void MimeTypeTest2()
        {
            IMime mime = new Mime();

            var mimeType = mime.Get("3dml");

            Assert.AreEqual(mimeType, "text/vnd.in3d.3dml");
        }

        [TestMethod]
        public void MimeTypeTest3()
        {
            IMime mime = new Mime();

            var mimeType = mime["z3"];

            Assert.AreEqual(mimeType, "application/x-zmachine");
        }
    }
}
