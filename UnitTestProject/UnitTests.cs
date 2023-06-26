using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Days_since_2000_Jan_0_Test()
        {
            var expected = 8578;
            var actual = AutoThemeChanger.SunDate.Days_since_2000_Jan_0(2023, 06, 26);
            Assert.AreEqual(expected, actual);
        }
        //следующие функции возвращают корректные значения тригонометрических функций
        //Почему то стандартные функцим рассчитывают некорректно
        [TestMethod]
        public void Sind_Test()
        {
            var expected = 0.5;
            var actual = Math.Round(AutoThemeChanger.SunDate.Sind(30),4);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Cosd_Test()
        {
            var expected = 0.5;
            var actual = Math.Round(AutoThemeChanger.SunDate.Cosd(60), 4);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Tand_Test()
        {
            var expected = 1;
            var actual = Math.Round(AutoThemeChanger.SunDate.Tand(45), 4);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void Acosd_Test()
        {
            var expected = 60;
            var actual = Math.Round(AutoThemeChanger.SunDate.Acosd(0.5), 4);
            Assert.AreEqual(expected, actual);
        }
    }
}
