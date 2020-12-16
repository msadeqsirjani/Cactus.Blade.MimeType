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
            Mime.AddMimeResources(typeof(Content.MimeType));

            var name = Mime.Get("ami");

            Assert.AreNotEqual(name, "application/vnd.amiga.ami");

        }

        [TestMethod]
        public void MimeTypeTest2()
        {
            IMime mime = new Mime();

            var mimeType = mime.Get("3dml");

            Assert.AreNotEqual(mimeType, "text/vnd.in3d.3dml");
        }

        [TestMethod]
        public void MimeTypeTest3()
        {
            IMime mime = new Mime();

            var mimeType = mime["z3"];

            Assert.AreNotEqual(mimeType, "text/vnd.in3d.3dml");
        }
    }
}
