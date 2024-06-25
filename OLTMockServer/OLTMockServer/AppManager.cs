using OLTMockServer.DataStructures;
using OLTMockServer.MockServers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer
{
    internal class AppManager : IDisposable
    {
        private readonly List<TestManager> tests;
        private readonly List<MockServer> servers;
        public static AppManager Current { get; private set; }

        public AppManager()
        {
            this.tests = new List<TestManager>();
            this.servers = new List<MockServer>();

            LoadAvailableServers();

            StartServersApiService();

            Test();

            AppManager.Current = this;
        }

        private void Test()
        {
            var vendor = new Vendor() { Name = "Mihan-srv1001", BaseUrl = "http://ks40/mzwcfService/" };
            var order = servers[0].CreateNewOrder(new OrderPattern(), true);
            servers[0].SendOrder(order, vendor);
        }

        private void StartServersApiService()
        {
            foreach (var server in servers)
            {
                if (!server.StartServer())
                {
                    //log... server not started. 'name' 
                }
                else
                {
                    //log...server started./type/name/url...
                }
            }
        }

        private void LoadAvailableServers()
        {
            servers.Add(new SnapMockServer());
        }

        internal void AddTest(TestManager newTest)
        {
            tests.Add(newTest);
        }

        public TestManager CreateTestManager(Definitions.KnownOnlineShops onlineShop)
        {
            switch (onlineShop)
            {
                case Definitions.KnownOnlineShops.Snap:
                    var server = (SnapMockServer)GetOnlineShopServer(onlineShop);
                    return new SnapTestManager(server);

                case Definitions.KnownOnlineShops.Digi:
                    return null;
                case Definitions.KnownOnlineShops.None:
                default:
                    throw new ApplicationException("Unknown online shop type");
            }
        }

        public TestManager AddTest(Definitions.KnownOnlineShops onlineShop, bool onlyCreate = false)
        {
            var testManager = CreateTestManager(onlineShop);

            if (!onlyCreate)
            {
                tests.Add(testManager);
            }

            return testManager;
        }

        private MockServer GetOnlineShopServer(Definitions.KnownOnlineShops onlineShop)
        {
            var server = servers.FirstOrDefault(s => s.OnlineShopType == onlineShop);

            if (server == null)
            {
                throw new ApplicationException("Unknown online shop type");
            }

            return server;
        }

        private int lastTestNumber;
        public int GetNextTestNumber()
        {
            return ++lastTestNumber;
        }

        public List<Definitions.KnownOnlineShops> AvailableServerTypes
        {
            get
            {
                return servers.Select(s => s.OnlineShopType).ToList();
            }
        }

        public void Dispose()
        {
            foreach (var server in servers)
            {
                server.StopServer();
            }
        }

    }
}


//public class Food { }
//public class Meat : Food { }
//public class Grass : Food { }

//public interface IAnimal<out T>
//    where T : Food
//{ }

//public abstract class Animal<TFood> : IAnimal<TFood>
//    where TFood : Food
//{ }

//public class Lion : Animal<Meat> { }

//public class Goat : Animal<Grass> { }

//public class World
//{
//    public List<IAnimal<Food>> Animals { get; set; }

//    public World()
//    {
//        Animals.Add(new Lion());
//        Animals.Add(new Goat());
//    }

//    private void Check()
//    {

//    }
//}