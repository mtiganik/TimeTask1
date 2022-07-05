using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTask;

namespace TimeTask.Test
{
    public class getInputDataTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void FileNotFoundThrows()
        {
            string fileName = "noFile.txt";
            Assert.Throws<FileNotFoundException>(() => Program.getInputData(fileName));
        }

        [Test]
        public void CorrectInputPasses()
        {
            string fileName = "TD_1_CorrectInputPasses.txt";
            Assert.DoesNotThrow(() => Program.getInputData(fileName));

        }
        [Test]
        public void InvalidLineThrowsException()
        {
            string fileName = "TD_2_InvalidLineThrowsException.txt";
            Assert.Throws<IndexOutOfRangeException>(() => Program.getInputData(fileName));

        }
        [Test]
        public void WhiteSpaceInLineEndShouldPass()
        {
            string fileName = "TD_3_WhiteSpaceInLineEndShouldPass.txt";
            Assert.DoesNotThrow(() => Program.getInputData(fileName));
        }
    }
}
