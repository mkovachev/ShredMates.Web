using AutoMapper;
using ShredMates.Web.Infrastructure.Mapping;

namespace ShredMates.Tests
{
    public class TestStartup
    {
        private static bool testInitialized = false;

        public static void GetMapper()
        {
            if (!testInitialized)
            {
                Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());
                testInitialized = true;
            }
        }
    }
}
