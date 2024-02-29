using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetWork.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetWork.BL.Tests
{
    [TestClass()]
    public class MappingProfileTests
    {
        [TestMethod()]
        public void MappingProfileTest()
        {
            var cfg = new MapperConfiguration(expression => expression.AddProfile<MappingProfile>());
            cfg.AssertConfigurationIsValid();
        }
    }
}