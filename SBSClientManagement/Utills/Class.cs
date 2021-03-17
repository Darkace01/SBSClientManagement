using SBSClientManagement.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBSClientManagement.Utills
{
    public class Genrator
    {
        public static string GenerateRandomString(int length, bool specialXter = false)
        {
            RandomStringGenerator stringGenerator = new RandomStringGenerator();

            return stringGenerator.NextString(length, true, true, true, specialXter);
        }
    }
}
